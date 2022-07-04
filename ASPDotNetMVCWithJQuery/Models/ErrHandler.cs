using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace ASPDotNetMVCWithJQuery.Models
{
    public class ErrHandler
    {
        public static void WriteLog(string Message, string emailtype)
        {
            String StartUpPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            try
            {

                string loc = StartUpPath + "/Logs/" + DateTime.Today.ToString("dd-MM-yy");
                if (!Directory.Exists(loc))
                {
                    Directory.CreateDirectory(loc);
                }
                string path = loc + "/" + emailtype + ".txt";

                using (StreamWriter w = File.AppendText(path))
                {
                    w.Write("\r\nLog Entry : :");
                    w.Write("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    w.WriteLine("-:-" + Message);
                    // w.WriteLine("_______________________________________");
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}