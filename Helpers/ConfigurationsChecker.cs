using System;
using System.Configuration;
using System.IO;

namespace Cappario
{
    public static class ConfigurationsChecker
    {
        public static void Check()
        {
            CheckFileCapparioExists();
            CheckForNullValues();
        }

        private static void CheckFileCapparioExists()
        {
            if(!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\Cappario.xlsx"))
            {
                Console.WriteLine("The file Cappario.xlsx is missing from the main folder");
                Environment.Exit(0);
            }
        }

        private static void CheckForNullValues()
        {
            var NumberOfConfigValues = ConfigurationManager.AppSettings.Count;
            for (int e = 0; e < NumberOfConfigValues; e++)
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get(e)))
                {
                    Console.WriteLine(ConfigurationManager.AppSettings.GetKey(e) + " in the config file is null");
                    Environment.Exit(0);
                }
            }
        }
    }
}