using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataAPI.Utils
{
    public static class Routine
    {
        public static void LogFile(string ExepetionName, string EventName, string controlName, int ErroLine, string FromName)

        {
            string LogFlesPath = "LogFile.txt";
            StreamWriter Log;
            if (!File.Exists(LogFlesPath))
            {
                Log = new StreamWriter(LogFlesPath);
            }
            else
            {
                Log = File.AppendText(LogFlesPath);
            }

            Log.WriteLine("Date:" + DateTime.UtcNow);
            Log.WriteLine("Erreur:" + ExepetionName);
            Log.WriteLine("Event:" + EventName);
            Log.WriteLine("Contrlo:" + controlName);
            Log.WriteLine("Ligne:" + ErroLine);
            Log.WriteLine("Feuille:" + FromName);

            Log.Close();
        }

        public static int ErreurLine(Exception e)
        {
            int lineNumber = 0;
            try
            {
                lineNumber = Convert.ToInt32(e.StackTrace.Substring(e.StackTrace.LastIndexOf(":ligne") + 5));
            }
            catch (Exception)
            {


            }
            return lineNumber;
        }

    }
}
