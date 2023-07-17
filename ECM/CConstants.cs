namespace ECM
{
    public class Constants
    {
        private static string dbName = "db.accdb";
        private static string dbPath = System.IO.Directory.GetCurrentDirectory() + "\\Database\\" + dbName;
        private static string connectionString = "provider=microsoft.ace.oledb.12.0;data source=" + dbPath + "; ole db services=-1";
        public static string DataBaseName
        {
            get
            {
                return dbName;
            }
        }

        public static string DataBasePath
        {
            get
            {
                return dbPath;
            }
        }

        public static string ConnectionString
        {
            get
            {
                return connectionString;
            }
        }
    }
}
