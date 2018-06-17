using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SmartHub.DbMigration.UI.Models;

namespace SmartHub.DbMigration.UI
{
    public partial class Profiles : Form
    {
        private static readonly string ConfigFilePath = Path.Combine(Environment.CurrentDirectory, "config.bin");

        public Profiles()
        {
            InitializeComponent();
            LoadProfiles();
        }

        internal static List<ProfileRecord> ReadConfigFile(bool appendAddNew = false)
        {
            var profiles = new List<ProfileRecord>();
            if (File.Exists(ConfigFilePath))
            {
                try
                {
                    profiles = ReadFromBinaryFile<List<ProfileRecord>>(ConfigFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message +
                        Environment.NewLine +
                        "Probably corrupted config file." +
                        Environment.NewLine +
                        "Config file is now reset.",
                        "Oops"
                    );
                    WriteToBinaryFile(ConfigFilePath, profiles);
                }
            }
            else
            {
                WriteToBinaryFile(ConfigFilePath, profiles);
            }

            if (!profiles.Any())
            {
                profiles.Add(new ProfileRecord { Name = "- No Profile(s) found -" });
            }

            if (appendAddNew)
            {
                profiles.Add(new ProfileRecord { Name = "- Add new Profile -" });
            }

            return profiles;
        }

        internal void AddNewProfile()
        {
            ddlProfiles.SelectedIndex = -1;
            ddlProfiles.Enabled = false;
            btnAddNew.Enabled = false;
            btnDelete.Text = "Cancel";
            ResetConnectionInputs();
        }

        /// <summary>
        /// Writes the given object instance to a binary file.
        /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
        /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the XML file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the XML file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        private static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        /// <summary>
        /// Reads an object instance from a binary file.
        /// </summary>
        /// <typeparam name="T">The type of object to read from the XML.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the binary file.</returns>
        private static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

        private void LoadProfiles(bool selectedLatest = false)
        {
            ddlProfiles.Enabled = true;
            btnAddNew.Enabled = true;
            btnDelete.Text = "Delete";

            var profiles = ReadConfigFile(true);

            ddlProfiles.DataSource = profiles;
            ddlProfiles.SelectedIndex = selectedLatest ? ddlProfiles.SelectedIndex = profiles.FindIndex(x => x.Id == profiles.OrderByDescending(y => y.UpdatedOn).First().Id) : 0;
            ddlProfiles.Focus();
        }

        private void EditProfile()
        {
            var selectedProfile = ddlProfiles.SelectedItem as ProfileRecord;
            if (selectedProfile != null && selectedProfile.Connection != null)
            {
                ResetInputs(selectedProfile.Name, selectedProfile.IsDefault, selectedProfile.Connection);
            }
        }

        private void ResetInputs(string profileName, bool isDefault, ConnectionRecord connection)
        {
            ResetConnectionInputs(profileName, isDefault, connection.Server, connection.Database, connection.User, connection.Password);
        }

        private void ResetConnectionInputs(string profileName = null, bool isDefault = false, string server = null, string db = null, string user = null, string password = null)
        {
            txtName.Text = profileName;
            chkDefault.Checked = isDefault;
            txtServer.Text = server;
            txtDatabase.Text = db;
            txtUser.Text = user;
            txtPassword.Text = password;
        }

        private void Profiles_FormClosed(object sender, FormClosedEventArgs e)
        {
            var parent = Owner as Main;
            if (parent != null)
                parent.LoadProfiles();
        }

        private void DdlProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProfiles.SelectedIndex == ddlProfiles.Items.Count - 1)
            {
                AddNewProfile();
                return;
            }

            EditProfile();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Connection Name is required", "Error");
                return;
            }

            var profiles = ReadConfigFile();
            var inputConnection = new ConnectionRecord
            {
                Server = txtServer.Text,
                Database = txtDatabase.Text,
                User = txtUser.Text,
                Password = txtPassword.Text
            };

            //Add
            if (ddlProfiles.SelectedIndex < 0)
            {
                if (chkDefault.Checked)
                {
                    profiles.ForEach(x => x.IsDefault = false);
                }

                profiles.Add(new ProfileRecord
                {
                    Name = txtName.Text,
                    Connection = inputConnection,
                    UpdatedOn = DateTime.Now,
                    IsDefault = chkDefault.Checked
                });
            }

            //Edit
            else
            {
                profiles.ForEach(x =>
                {
                    x.IsDefault = false;
                    if (x.Id == (Guid)ddlProfiles.SelectedValue)
                    {
                        x.Name = txtName.Text;
                        x.Connection = inputConnection;
                        x.UpdatedOn = DateTime.Now;
                        x.IsDefault = chkDefault.Checked;
                    }
                });
            }

            WriteToBinaryFile(ConfigFilePath, profiles);

            LoadProfiles(true);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (ddlProfiles.SelectedIndex < 0)
            {
                btnAddNew.Enabled = true;
                ddlProfiles.Enabled = true;
                ddlProfiles.SelectedIndex = 0;
                btnDelete.Text = "Delete";
            }
            else
            {
                var deleteConfirm = MessageBox.Show("Delete?", "Confirm", MessageBoxButtons.YesNo);
                if (deleteConfirm == DialogResult.Yes)
                {
                    var profiles = ReadConfigFile();
                    profiles.RemoveAll(x => x.Id == (Guid)ddlProfiles.SelectedValue);
                    WriteToBinaryFile(ConfigFilePath, profiles);
                    LoadProfiles();
                }
            }
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            AddNewProfile();
        }
    }
}
