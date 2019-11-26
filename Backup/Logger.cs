using System;
using System.Collections.Generic;
using System.IO;

namespace BackupAdmin
{
    public class Logger
    {
        public static void Log(string eventName, List<string> LogDetails)
        {
            LogDetails.Add($"{nameof(eventName)}: {eventName}");
            File.WriteAllLines($"{Constants.LogFileName}{DateTime.Now.ToString(Constants.LogTimeFormat)}", LogDetails);
        }

        public static void Log(string eventName, string LogDetail)
        {
            Log(eventName, new List<string> { LogDetail });
        }
    }
}
