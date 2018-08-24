using System;

namespace PhuKienDienThoai.Logs
{
    ///<summary>class dùng để ghi log</summary>
    public class Logger
    {
        public static void Log(string line)
        {
            try
            {
                var dir = Environment.CurrentDirectory;
                System.IO.StreamWriter file = new System.IO.StreamWriter(dir + @"\Error.log", true);
                file.Write(DateTime.Now.ToString("dd/MM/yyyy") + ": " + line + Environment.NewLine);
                file.Close();
            }
            catch (System.Exception) { }
        }
    }
}