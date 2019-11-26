using System;

namespace BackupAdmin
{
    public class Configs
    {
        public string SourceFolder { get; set; }
        public string TargetFolder { get; set; } 
        public DateTime? LatestBackupTime { get; set; } 
    }
}
