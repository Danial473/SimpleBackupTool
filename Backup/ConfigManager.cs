using Newtonsoft.Json;
using System;
using System.IO;

namespace BackupAdmin
{
    public static class ConfigManager
    {
        private const string pathConfigFileName = "PathConfig.json";
        private const string dateConfigFileName = "DateConfig.json";

        public static Configs GetConfigs()
        {
            try
            {
                string path, date;
                PathConfig pathConfig = null;
                DateConfig dateConfig = new DateConfig();

                if (File.Exists(pathConfigFileName))
                {
                    path = File.ReadAllText(pathConfigFileName);
                    if (!string.IsNullOrEmpty(path))
                        pathConfig = JsonConvert.DeserializeObject<PathConfig>(path);
                }
                if (File.Exists(dateConfigFileName))
                {
                    date = File.ReadAllText(dateConfigFileName);
                    if (!string.IsNullOrEmpty(date))
                        dateConfig = JsonConvert.DeserializeObject<DateConfig>(date);
                }

                return new Configs
                {
                    SourceFolder = pathConfig?.SourceFolder,
                    TargetFolder = pathConfig?.TargetFolder,
                    LatestBackupTime = dateConfig?.LatestBackupTime
                };
            }
            catch (Exception ex)
            {
                Logger.Log(nameof(GetConfigs), $"Exception: { JsonConvert.SerializeObject(ex)}");
                throw ex;
            }
        }

        public static void UpdatePathConfig(PathConfig config)
        {
            try
            {
                File.WriteAllText(pathConfigFileName, JsonConvert.SerializeObject(config));
                Logger.Log(nameof(UpdatePathConfig), $"Update path successful");
            }
            catch (Exception ex)
            {
                Logger.Log(nameof(UpdatePathConfig), $"Exception: { JsonConvert.SerializeObject(ex)}");
                throw ex;
            }
        }

        public static void UpdateLastBackupConfig(DateTime lastUpdateDateTime)
        {
            try
            {
                File.WriteAllText(dateConfigFileName, JsonConvert.SerializeObject(new DateConfig { LatestBackupTime = lastUpdateDateTime }));
            }
            catch (Exception ex)
            {
                Logger.Log(nameof(UpdateLastBackupConfig), $"Exception: { JsonConvert.SerializeObject(ex)}");
                throw ex;
            }
        }
    }
}
