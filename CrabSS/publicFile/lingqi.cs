/**
 * 灵启引擎相关模块
 * 测试版本
 * 许可证：GNU
 * :)
 */
using LitJson;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static CrabSS.@public;
using CrabSS.Properite;
using COSXML.Auth;

namespace CrabSS.publicFile
{
    internal class lingqi
    {
        public static QCloudCredentialProvider cosCredentialProvider;
        public class settinginfo //把我整不会了😅
        {
        }

        public static string DecodeCrabDriveShareLink(string sharelink)
        {
            return "";
        }
        public static string Post(string url, Dictionary<string, string> dic)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            #region 添加Post 参数
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;

        }
        public static string GetHttpResponse(string url, int Timeout)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = null;
            request.Timeout = Timeout;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4844.51 Safari/537.36 Edg/99.0.1150.36";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        public static void GetCOSKey(MetroWindow win)
        {
            Auth auth = new();
            string data = lingqi.GetHttpResponse("https://v6.crabapi.cn/auth/coskey?auth_token=" + auth.authtoken, 3400);
            COSData b = JsonConvert.DeserializeObject<COSData>(data);
            if (b.code == -1)
            {
                win.ShowMessageAsync(":( 发生了错误，但是是程序的错","在程序内部配置的 auth_token 变量无效，无法正常获取更新地址！\n请联系螃蟹修复这个 Bug");
            }
            else
            {
                cosCredentialProvider = new DefaultQCloudCredentialProvider(b.accessID, b.accessKey, 203400);
            }
        }
    }
}
