namespace ECM
{
    class CUtilities
    {
        public static string GetStateInString(bool state)
        {
            string res = "";
            if (state)
            {
                res = "مقفلة";
            }
            else
            {
                res = "مفتوحة";
            }
            return res;
        }
        public static bool CheckDataBaseFile()
        {
            bool res = false;

            try
            {
                res = System.IO.File.Exists(Constants.DataBasePath);
            }
            catch { }
            return res;
        }

        public static bool TestDataBaseEngine()
        {
            bool res = false;
            return res;
        }

        public static bool InstallDB()
        {
            bool res = false;
            return res;
        }

        public static bool Backup()
        {
            bool res = false;
            return res;
        }

        public static bool Restore()
        {
            bool res = false;
            return res;
        }
    }
}
