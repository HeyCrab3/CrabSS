using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrabSS.publicFile
{
    internal interface Log
    {
        public static void log(string msg){
            string path = Environment.CurrentDirectory + "\\log";
            try
            {
                if (!Directory.Exists(path))//判断是否有该文件            
                    Directory.CreateDirectory(path);
                string logFileName = path + "\\Analysis.log";//生成日志文件
                if (!File.Exists(logFileName))//判断日志文件是否为当天
                    File.Create(logFileName).Close();//创建文件

                StreamWriter writer = File.AppendText(logFileName);//文件中添加文件流
                writer.WriteLine("");
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + msg);
                writer.Flush();
                writer.Close();
            }
            catch (Exception e)
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string logFileName = path + "\\Analysis" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                if (!File.Exists(logFileName))
                    File.Create(logFileName);
                StreamWriter writer = File.AppendText(logFileName);
                writer.WriteLine("");
                writer.WriteLine(DateTime.Now.ToString("日志记录错误HH:mm:ss") + " " + e.Message + " " + msg);
                writer.Flush();
                writer.Close();
            }
        }
    }
}
