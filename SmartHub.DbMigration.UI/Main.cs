using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using SmartHub.DbMigration.UI.Models;

namespace SmartHub.DbMigration.UI
{
    public partial class Main : Form
    {
        private const string MigrateCommand = "migrate.exe --conn \"{0}\" --provider {1} --assembly \"{2}\"";
        private const string Provider = "sqlserver2008";

        private const string MigrateUpToVersion = "{0} --task migrate:up --version={1}";
        private const string MigrateDownToVersion = "{0} --task rollback:toversion --version={1}";

        private readonly Process _console;

        private string _connectionString;
        private int _currentDbVersionRowIndex;

        private List<List<DbVersionRecord>> _loadedMigrations;

        public Main()
        {
            InitializeComponent();

            _console = new Process();
            _console.StartInfo.FileName = "cmd.exe";
            _console.StartInfo.UseShellExecute = false;
            _console.StartInfo.RedirectStandardInput = true;
            _console.StartInfo.RedirectStandardError = true;
            _console.StartInfo.RedirectStandardOutput = true;
            _console.StartInfo.CreateNoWindow = true;

            _console.Start();
            _console.BeginErrorReadLine();
            _console.BeginOutputReadLine();

            LoadProfiles();
            LoadMigrations();

            ddlProfiles.Focus();
        }

        internal void LoadProfiles()
        {
            var profiles = Profiles.ReadConfigFile(true);

            ddlProfiles.DataSource = profiles;

            var selectedProfile = profiles.FirstOrDefault(x => x.IsDefault);
            ddlProfiles.SelectedIndex = selectedProfile != null ? profiles.IndexOf(selectedProfile) : 0;

            if (selectedProfile != null && selectedProfile.Connection != null)
            {
                UpdateConnection(selectedProfile.Connection);
            }

            BuildConnectionString();
        }

        private void Console_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            AppendConsole(e.Data, Color.Red);
        }

        private void Controle_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            AppendConsole(e.Data, Color.Lime);
        }

        private void UpdateConnection(ConnectionRecord connection)
        {
            txtServer.Text = connection.Server;
            txtDatabase.Text = connection.Database;
            txtUser.Text = connection.User;
            txtPassword.Text = connection.Password;
        }

        private void LoadMigrations()
        {
            var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Select(x => Assembly.LoadFrom(x))
                .Union(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.exe").Select(x => Assembly.LoadFrom(x)));
            ddlMigration.Items.Clear();

            _loadedMigrations = new List<List<DbVersionRecord>>();

            foreach (var assembly in assemblies)
            {
                var migrations = assembly.GetTypes()
                    .Where(x => x.GetCustomAttribute<MigrationAttribute>() != null)
                    .Select(x => x.GetCustomAttribute<MigrationAttribute>())
                    .ToList();

                if (migrations.Any())
                {
                    //Insert version 0
                    migrations.Insert(0, new MigrationAttribute(0, "Empty / No migrations"));

                    _loadedMigrations.Add(migrations
                        .OrderBy(x => x.Version)
                        .Select(x => new DbVersionRecord
                        {
                            VersionNumber = x.Version,
                            Description = x.Description
                        })
                        .ToList());

                    var assemblyShortName = assembly.GetName().Name;

                    ddlMigration.Items.Add(new DbMigrationRecord
                    {
                        AssemblyName = $"{assemblyShortName}.dll",
                        MigrationName = $"{assemblyShortName} ({migrations.OrderByDescending(x => x.Version).First().Version})"
                    });
                }
            }

            if (ddlMigration.Items.Count == 0)
                ddlMigration.Items.Add(new DbMigrationRecord { AssemblyName = 0.ToString(), MigrationName = "- No migration(s) found -" });

            ddlMigration.Items.Add(new DbMigrationRecord { AssemblyName = (-1).ToString(), MigrationName = "- Refresh this list -" });
            ddlMigration.SelectedIndex = 0;
        }

        private DbVersionRecord[] GetDatabaseDbVersions()
        {
            var versions = new List<DbVersionRecord>();
            string commandText = "IF exists(SELECT 1 from sys.all_columns where object_id = object_id(N'VersionInfo')) " +
                                        "begin select * from VersionInfo end " +
                                        "else begin select cast(0 as bigint) as [Version], NULL as [AppliedOn], 'Empty / No migrations' as [Description] end";

            using (var connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    connection.Open();

                    using (var dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            versions.Add(new DbVersionRecord
                            {
                                VersionNumber = dr.GetInt64(dr.GetOrdinal("Version")),
                                Description = dr.GetString(dr.GetOrdinal("Description")),
                                AppliedOn = dr["AppliedOn"] == DBNull.Value ? null : (DateTime?)dr.GetDateTime(dr.GetOrdinal("AppliedOn"))
                            });
                        }
                    }
                }
            }

            if (!versions.Any())
                versions.Add(new DbVersionRecord { VersionNumber = 0, Description = "Empty / No migrations" });

            return versions.ToArray();
        }

        private void BuildConnectionString()
        {
            _connectionString = null;
            grdMigrationMap.DataSource = null;

            if (!string.IsNullOrWhiteSpace(txtServer.Text) && !string.IsNullOrWhiteSpace(txtDatabase.Text) && !string.IsNullOrWhiteSpace(txtUser.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                _connectionString = $"Data Source={txtServer.Text};Initial Catalog={txtDatabase.Text};User ID={txtUser.Text};Password={txtPassword.Text}";
            }
        }

        private string GetMigrateCommand()
        {
            return string.Format(MigrateCommand, _connectionString, Provider, ((DbMigrationRecord)ddlMigration.SelectedItem).AssemblyName);
        }

        private void RunCommand(string command)
        {
            txtConsole.Clear();

            _console.OutputDataReceived -= Controle_OutputDataReceived;
            _console.OutputDataReceived += Controle_OutputDataReceived;

            _console.ErrorDataReceived -= Console_ErrorDataReceived;
            _console.ErrorDataReceived += Console_ErrorDataReceived;

            _console.StandardInput.WriteLine(command);
        }

        private void AppendConsole(string txt, Color txtColour)
        {
            if (txtConsole.IsDisposed)
                return;

            if (InvokeRequired)
            {
                Invoke(new Action<string, Color>(AppendConsole), new object[] { txt, txtColour });
                return;
            }

            txtConsole.SelectionColor = txtColour;
            txtConsole.AppendText(Environment.NewLine + txt);
        }

        private bool MigrationReadyCheck(bool targetCheck = false)
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                MessageBox.Show("Connection string is required.", "Error");
                return false;
            }

            if (targetCheck && grdMigrationMap.SelectedCells.Count == 0)
            {
                MessageBox.Show("Target migration is required.", "Error");
                return false;
            }

            return true;
        }

        private void ShowProfilesForm(params Action<Profiles>[] postActions)
        {
            var frmProfiles = new Profiles();
            if (postActions != null && postActions.Any())
            {
                foreach (var action in postActions)
                {
                    action(frmProfiles);
                }
            }

            frmProfiles.ShowDialog(this);
        }

        private void BtnCheckCurrent_Click(object sender, EventArgs e)
        {
            if (!MigrationReadyCheck())
                return;

            try
            {
                var selectedMigrations = _loadedMigrations[ddlMigration.SelectedIndex];
                var dbVersions = GetDatabaseDbVersions();
                //Reset the grid
                grdMigrationMap.DataSource = null;

                //Map AppliedOn
                selectedMigrations.ForEach(x =>
                {
                    x.AppliedOn = null; //Reset
                    var matchedDbVersion = dbVersions.FirstOrDefault(y => y.VersionNumber == x.VersionNumber);
                    if (matchedDbVersion != null)
                        x.AppliedOn = matchedDbVersion.AppliedOn;
                });

                _currentDbVersionRowIndex = selectedMigrations.FindIndex(x => x.VersionNumber == dbVersions.OrderByDescending(y => y.VersionNumber).First().VersionNumber);

                grdMigrationMap.DataSource = selectedMigrations;
                grdMigrationMap.Rows[_currentDbVersionRowIndex].Selected = true;
                grdMigrationMap.CurrentCell = grdMigrationMap.Rows[_currentDbVersionRowIndex].Cells[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void DdlMigration_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdMigrationMap.DataSource = null;
            if (ddlMigration.SelectedIndex == ddlMigration.Items.Count - 1)
            {
                LoadMigrations();
            }
        }

        private void TxtServer_TextChanged(object sender, EventArgs e)
        {
            BuildConnectionString();
        }

        private void TxtDatabase_TextChanged(object sender, EventArgs e)
        {
            BuildConnectionString();
        }

        private void TxtUser_TextChanged(object sender, EventArgs e)
        {
            BuildConnectionString();
        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {
            BuildConnectionString();
        }

        private void BtnMigrate_Click(object sender, EventArgs e)
        {
            if (!MigrationReadyCheck(true))
                return;

            if (grdMigrationMap.CurrentRow.Index == _currentDbVersionRowIndex)
                return;

            var selectedVersion = (long)grdMigrationMap.CurrentRow.Cells["VersionNumber"].Value;
            var command = string.Format(
                grdMigrationMap.CurrentCell.RowIndex < _currentDbVersionRowIndex ? MigrateDownToVersion : MigrateUpToVersion,
                GetMigrateCommand(),
                selectedVersion);

            grdMigrationMap.DataSource = null;
            RunCommand(command);
        }

        private void GrdMigrationMap_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Regular);
            e.CellStyle.ForeColor = Color.Green;
            e.CellStyle.BackColor = Color.White;

            if (e.RowIndex < _currentDbVersionRowIndex)
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Italic);
                e.CellStyle.ForeColor = Color.Gray;
                e.CellStyle.BackColor = Color.White;
            }

            if (e.RowIndex == _currentDbVersionRowIndex)
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Regular);
                e.CellStyle.ForeColor = Color.White;
                e.CellStyle.BackColor = Color.Green;
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadMigrations();
        }

        private void MniAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"DbMigration v{GetType().Assembly.GetName().Version}", "Info");
        }

        private void MniProfiles_Manage_Click(object sender, EventArgs e)
        {
            ShowProfilesForm();
        }

        private void DdlProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProfiles.SelectedIndex == ddlProfiles.Items.Count - 1)
            {
                ShowProfilesForm((x) => x.AddNewProfile());
                return;
            }

            var selectedProfile = ddlProfiles.SelectedItem as ProfileRecord;
            if (selectedProfile != null && selectedProfile.Connection != null)
                UpdateConnection(selectedProfile.Connection);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            txtConsole.Dispose();
            _console.StandardInput.WriteLine("exit");
        }
    }
}
