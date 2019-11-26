using BackupAdmin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BackupJob
{
    class Program
    {
        public static Configs config { get; set; }

        public static void Main(string[] args)
        {
            config = ConfigManager.GetConfigs();

            try
            {
                if (string.IsNullOrEmpty(config.SourceFolder) || string.IsNullOrEmpty(config.TargetFolder) || config.LatestBackupTime == null)
                    throw new Exception("empty configs detected. Config: " + JsonConvert.SerializeObject(config));

                CopyFiles(config.SourceFolder, config.TargetFolder);

                ConfigManager.UpdateLastBackupConfig(DateTime.Now);

                Logger.Log(nameof(Main), $"Files backed up successfully");
            }
            catch (Exception ex)
            {
                List<string> logDetails = new List<string> {
                    $"config: { JsonConvert.SerializeObject(config)}",
                    $"Exception: { JsonConvert.SerializeObject(ex)}"
                };
                Logger.Log(nameof(Main), logDetails);

                MessageBox.Show("Something went wrong during XRay backup process. Please contact Daniel about this issue");
            }
        }

        private static void CopyFiles(string directoryPath, string destinationPath)
        {
            DirectoryInfo dir = new DirectoryInfo(directoryPath);
            DirectoryInfo[] subDirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }
            else
            {
                var lastWriteTime = Directory.GetLastWriteTime(directoryPath);

                if (Directory.GetLastWriteTime(destinationPath) != lastWriteTime)
                    Directory.SetLastWriteTime(destinationPath, lastWriteTime);
            }

            var newFiles = dir.GetFiles().Where(p => p.LastWriteTime > config.LatestBackupTime).ToList();
            foreach (FileInfo newfile in newFiles)
            {
                string temppath = Path.Combine(destinationPath, newfile.Name);
                newfile.CopyTo(temppath, true);
            }

            foreach (DirectoryInfo subdir in subDirs)
            {
                string temppath = Path.Combine(destinationPath, subdir.Name);
                CopyFiles(subdir.FullName, temppath);
            }
        }
    }
}
