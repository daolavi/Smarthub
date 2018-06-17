namespace SmartHub.DbMigration
{
    using FluentMigrator.Builders.Alter.Table;
    using FluentMigrator.Builders.Create.Table;
    using FluentMigrator.Builders.Delete;

    public static class Extensions
    {
        private const string IdCol = "Id";
        private const string ForeignKeyTemplate = "FK_{0}_{1}_{2}";

        public static ICreateTableColumnOptionOrWithColumnSyntax WithInt32IdColumn(this ICreateTableWithColumnOrSchemaOrDescriptionSyntax table)
        {
            return table.WithColumn(IdCol).AsInt32().PrimaryKey().Identity();
        }

        public static ICreateTableColumnOptionOrForeignKeyCascadeOrWithColumnSyntax WithInt32ForeignKeyColumn(this ICreateTableColumnOptionOrWithColumnSyntax table, string colName, string sourceTable, string targetTable)
        {
            var fk = BuildForeignKeyName(sourceTable, targetTable, colName);
            return table.WithColumn(colName).AsInt32().ForeignKey(fk, targetTable, IdCol);
        }

        public static ICreateTableColumnOptionOrForeignKeyCascadeOrWithColumnSyntax WithInt64ForeignKeyColumn(this ICreateTableColumnOptionOrWithColumnSyntax table, string colName, string sourceTable, string targetTable)
        {
            var fk = BuildForeignKeyName(sourceTable, targetTable, colName);
            return table.WithColumn(colName).AsInt64().ForeignKey(fk, targetTable, IdCol);
        }

        public static ICreateTableColumnOptionOrForeignKeyCascadeOrWithColumnSyntax ForeignKeyWithFormat(this ICreateTableColumnOptionOrWithColumnSyntax table, string colName, string sourceTable, string targetTable)
        {
            var fk = BuildForeignKeyName(sourceTable, targetTable, colName);
            return table.ForeignKey(fk, targetTable, IdCol);
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax AsMaxString(this ICreateTableColumnAsTypeSyntax createTableColumnAsTypeSyntax)
        {
            return createTableColumnAsTypeSyntax.AsString(int.MaxValue);
        }

        public static IAlterTableColumnOptionOrAddColumnOrAlterColumnOrForeignKeyCascadeSyntax AddInt32ForeignKeyColumn(this IAlterTableAddColumnOrAlterColumnSyntax table, string colName, string sourceTable, string targetTable)
        {
            var fk = BuildForeignKeyName(sourceTable, targetTable, colName);
            return table.AddColumn(colName).AsInt32().ForeignKey(fk, targetTable, IdCol);
        }

        public static IAlterTableColumnOptionOrAddColumnOrAlterColumnOrForeignKeyCascadeSyntax AddInt64ForeignKeyColumn(this IAlterTableAddColumnOrAlterColumnSyntax table, string colName, string sourceTable, string targetTable)
        {
            var fk = BuildForeignKeyName(sourceTable, targetTable, colName);
            return table.AddColumn(colName).AsInt64().ForeignKey(fk, targetTable, IdCol);
        }

        public static IAlterTableColumnOptionOrAddColumnOrAlterColumnSyntax AsMaxString(this IAlterTableColumnAsTypeSyntax column)
        {
            return column.AsString(int.MaxValue);
        }

        public static void ForeignKey(this IDeleteExpressionRoot delete, string colName, string sourceTable, string targetTable)
        {
            var fk = BuildForeignKeyName(sourceTable, targetTable, colName);
            delete.ForeignKey(fk).OnTable(sourceTable);
        }

        private static string BuildForeignKeyName(string sourceTable, string targetTable, string colName)
        {
            return string.Format(ForeignKeyTemplate, sourceTable, targetTable, colName);
        }
    }
}
