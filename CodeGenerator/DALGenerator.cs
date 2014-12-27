using System.Data;
using System.Text;

namespace CodeGenerator
{
    class DALGenerator
    {
        public static string GenerateDAL(string tableName)
        {
            string queryText = @"SELECT TOP 0 * FROM " + tableName;
            DataTable datab = DBConnection.ExecuteDataTable(queryText);

            StringBuilder builder = new StringBuilder();

            builder.Append("public class ").Append(tableName).AppendLine("DAL");
            builder.AppendLine("{");

            // 新增
            builder.AppendLine("    // 新增");
            builder.Append("    public static int Insert(").Append(tableName).AppendLine(" obj)");
            builder.AppendLine("    {");
            builder.AppendLine("        if(obj == null)");
            builder.AppendLine("            return 0;");
            builder.AppendLine();
            builder.Append("        string queryText = ");
            builder.Append("@\"INSERT INTO ").Append(tableName);
            builder.Append("(");
            string tabColumns = FormatTable("", ", ", datab);
            builder.Append(tabColumns);
            builder.AppendLine(")");
            builder.Append("\tVALUES");
            builder.Append("(");
            tabColumns = FormatTable("@", ", ", datab);
            builder.Append(tabColumns);
            builder.AppendLine(")\";");
            builder.AppendLine();

            builder.Append("        SqlParameter[] sqlParams = ");
            builder.AppendLine("new SqlParameter[] {");
            tabColumns = FormatTable("\tnew SqlParameter(\"@", 
                                     "\", DBHelper.ConvertToDBValue(obj.", ")),\n", datab);
            builder.Append(tabColumns);
            builder.AppendLine(")) };");
            builder.AppendLine();
            builder.Append("        return ");
            builder.AppendLine("DBHelper.ExecuteNonQuery(queryText, sqlParams);");
            builder.AppendLine("    }");
            builder.AppendLine();

            // Select
            builder.AppendLine("    // 查询");
            builder.Append("    public static ").Append(tableName).Append("[] ");
            builder.AppendLine("Select(string searchCondition, SqlParameter[] sqlParams)");
            builder.AppendLine("    {");
            builder.AppendLine("        //// 查询条件未考虑软删除，请根据实际情况修改");
            builder.Append("        string queryText = ");
            builder.Append("@\"SELECT * FROM ").Append(tableName).AppendLine(" WHERE 1=1 \";");
            builder.AppendLine("        System.Data.DataTable datab;");
            builder.AppendLine();
            builder.AppendLine("        if(searchCondition == null)");
            builder.AppendLine("        {");
            builder.AppendLine("\tdatab = DBHelper.ExecuteDataTable(queryText);");
            builder.AppendLine("        }");
            builder.AppendLine("        else");
            builder.AppendLine("        {");
            builder.AppendLine("\tqueryText += searchCondition;");
            builder.AppendLine("\tdatab = DBHelper.ExecuteDataTable(queryText, sqlParams);");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        if(datab.Rows.Count <= 0)");
            builder.AppendLine("\treturn null;");
            builder.AppendLine();
            builder.Append("        ").Append(tableName).Append("[] objList = ");
            builder.Append("new ").Append(tableName).AppendLine("[datab.Rows.Count];");
            builder.AppendLine("        for(int i = 0; i < datab.Rows.Count; i++)");
            builder.AppendLine("        {");
            builder.Append("\t").Append(tableName).Append(" temp = ");
            builder.Append("new ").Append(tableName).AppendLine("();");
            tabColumns = FormatTable("\ttemp.", " = (", ")DBHelper.ConvertToCSValue(datab.Rows[i][\"",
                                     "\"]);\n", datab);
            builder.AppendLine(tabColumns);
            builder.AppendLine("\tobjList[i] = temp;");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        return objList;");
            builder.AppendLine("    }");
            builder.AppendLine();

            // Update
            builder.AppendLine("    // 更新");
            builder.Append("    public static int Update(");
            builder.Append(tableName).AppendLine(" obj)");
            builder.AppendLine("    {");
            builder.AppendLine("        if(obj == null)");
            builder.AppendLine("\treturn 0;");
            builder.AppendLine();
            builder.Append("        string queryText = ");
            builder.Append("@\"UPDATE ").Append(tableName).Append(" SET ");
            tabColumns = FormatTable(" ", " = @", ",", datab);
            builder.AppendLine(tabColumns);
            builder.Append("\tWHERE Id = @Id\";").AppendLine("    // 需要替换为实际主键");
            builder.AppendLine("        SqlParameter[] sqlParams = new SqlParameter[] {");
            tabColumns = FormatTable("\tnew SqlParameter(\"@", "\", obj.", "),\n", datab);
            builder.Append(tabColumns);
            builder.AppendLine(") };");
            builder.AppendLine();
            builder.AppendLine("        return DBHelper.ExecuteNonQuery(queryText, sqlParams);");
            builder.AppendLine("    }");
            builder.AppendLine();

            // Delete
            builder.AppendLine("    // 删除");
            builder.Append("    public static int Delete(").Append(tableName).AppendLine(" obj)");
            builder.AppendLine("    {");
            builder.AppendLine("        if(obj == null)");
            builder.AppendLine("\treturn 0;");
            builder.AppendLine();
            builder.AppendLine("        //// 默认进行软删除，并且默认表中最后一列为软删除标志，请根据实际情况修改");
            builder.Append("        string queryText = ");
            builder.Append("@\"UPDATE ").Append(tableName);
            builder.Append(" SET ").Append(datab.Columns[datab.Columns.Count - 1]).AppendLine(" = 1");
            builder.Append("\tWHERE Id = @Id\";").AppendLine("    // 需要替换为实际主键");
            builder.Append("        SqlParameter sqlParams = ");
            builder.AppendLine("new SqlParameter(\"@Id\", obj.Id);");
            builder.AppendLine();
            builder.AppendLine("        return DBHelper.ExecuteNonQuery(queryText, sqlParams);");
            builder.AppendLine("    }");
            builder.AppendLine();

            builder.AppendLine("}");

            return builder.ToString();
        }

        private static string FormatTable(string prefix, string postfix, DataTable datab)
        {
            StringBuilder builder = new StringBuilder();
            int columnCount = datab.Columns.Count;
            for(int i = 0; i < columnCount - 1; i++)
            {
                builder.Append(prefix).Append(datab.Columns[i]).Append(postfix);
            }
            builder.Append(prefix).Append(datab.Columns[columnCount - 1]);

            return builder.ToString();
        }

        private static string FormatTable(string prefix, string midfix, string postfix, DataTable datab)
        {
            StringBuilder builder = new StringBuilder();
            int columnCount = datab.Columns.Count;
            for (int i = 0; i < columnCount - 1; i++)
            {
                builder.Append(prefix).Append(datab.Columns[i]);
                builder.Append(midfix).Append(datab.Columns[i]);
                builder.Append(postfix);
            }
            builder.Append(prefix).Append(datab.Columns[columnCount - 1]);
            builder.Append(midfix).Append(datab.Columns[columnCount - 1]);

            return builder.ToString();
        }

        private static string FormatTable(string prefix, string mid1fix, string mid2fix, 
                                          string postfix, DataTable datab)
        {
            StringBuilder builder = new StringBuilder();
            int columnCount = datab.Columns.Count;
            for (int i = 0; i < columnCount; i++)
            {
                builder.Append(prefix).Append(datab.Columns[i]);
                builder.Append(mid1fix).Append(ModelGenerator.GetDataType(datab.Columns[i]));
                builder.Append(mid2fix).Append(datab.Columns[i]);
                builder.Append(postfix);
            }

            return builder.ToString();
        }
    }
}
