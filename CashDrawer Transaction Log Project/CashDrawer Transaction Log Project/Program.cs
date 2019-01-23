using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CashDrawer_Transaction_Log_Project
{
    class Program
    {
        static void Main(string[] args )
        {
            string logName = "";
            for (int i = 0; i < args[1].Length; i++){
                if (i != 7 && i != 8){
                    logName += args[1][i];
                }
            }
            logName += "-Transactions.log";
            string logPath = "c:\\temp\\transactionLogs\\" + logName;
            StreamWriter append_file = File.AppendText(logPath);
            for (int i = 0; i < args.Length; i++){
                if (i < args.Length - 1){
                    append_file.Write(args[i] + " ");
                }else {
                    append_file.WriteLine(args[i]);
                }
            }
            append_file.Close();
        }
    }
}
