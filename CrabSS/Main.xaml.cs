using CrabSS.publicFile;
using LitJson;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Toolkit.Uwp.Notifications;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static CrabSS.@public;
namespace CrabSS
{
    /// <summary>
    /// Main.xaml 的交互逻辑
    /// </summary>
    public partial class Main : MetroWindow
    {
        public Main()
        {
            InitializeComponent();
            if (!File.Exists("settings.json"))
            {//初始化Json部分
                try
                {
                    settinginfo settings = new settinginfo();
                    string json = JsonMapper.ToJson(settings); //using LitJson
                    StreamWriter sw = new StreamWriter(System.Environment.CurrentDirectory + "\\settings.json");
                    sw.Write(json);
                    sw.Close();
                    string strJson = File.ReadAllText("settings.json", Encoding.UTF8);
                    JObject oJson = JObject.Parse(strJson); //using Newtonsoft.Json.Linq
                    oJson["Version"] = "0.15r2";
                    oJson["WindowTitle"] = "CrabSS Beta";
                    oJson["minRamSize"] = 512;
                    oJson["maxRamSize"] = 2048;
                    oJson["CustomJavaPath"] = "";
                    oJson["isFrpOn"] = false;
                    oJson["background"] = "default";
                    string strConvert = Convert.ToString(oJson); //将json装换为string
                    File.WriteAllText("settings.json", strConvert); //将内容写进json文件中
                    sw.Dispose();
                }
                catch (Exception ex)
                {
                    this.ShowMessageAsync(":( 出错了，但是是程序的错", ex.ToString());
                    Close();
                }
            }
            else
            {
                settinginfo readed = new settinginfo();
                using (StreamReader readFile = File.OpenText("settings.json"))
                {
                    using (JsonTextReader reader = new JsonTextReader(readFile)) //using Newtonsoft.Json
                    {
                        JObject oJson = (JObject)JToken.ReadFrom(reader);
                        string ver = readed.Version = oJson["Version"].ToString();
                        string title = readed.WindowTitle = oJson["WindowTitle"].ToString();
                        string CustomJavaPath = readed.CustomJavaPath = oJson["CustomJavaPath"].ToString();
                        int minRamSize = readed.minRamSize;
                        int maxRamSize = readed.maxRamSize;
                        string background = readed.background;
                        bool isFrpOn = readed.isFrpOn;
                        string V = ver;
                        reader.Close();
                        /** bg.Text = background;
                        if (background != "default"){
                             MainWindow mw = new();
                             mw.Background = Background;
                         }**/
                    }
                }
                try {
                    broadcast.Content = lingqi.GetHttpResponse("https://v6.crabapi.cn/api/crabss/broadcast?channel=beta&encode=text", 4000);
                }
                catch {
                    broadcast.Content = "无网络或API故障";
                }
                string hikotoko = lingqi.GetHttpResponse("https://v1.hitokoto.cn/?encode=text", 4000);
                Title = "CrabSS 总控中心 | " + hikotoko;
                try
                {
                    String path = @"plugins";
                    string[] files = Directory.GetFiles(path, "*.jar");
                    int i = 0;
                    foreach (string file in files)
                    {
                        i++;
                    }
                    plugins.Title = $"插件管理 | 总数{i.ToString()}";
                }
                catch (Exception ex)
                {
                    this.ShowMessageAsync(":( 发生错误，但不是程序的错", ex.ToString());
                }
               //string ver2 = lingqi.GetHttpResponse("https://v6.crabapi.cn/api/crabss/version?channel=beta", 40000);
            }
        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            你被骗了 rickroll = new();
            rickroll.Show();
        }

        private void server_Click(object sender, RoutedEventArgs e)
        {
            kaifu kaifu = new();
            kaifu.Show();
        }

        private void plugins_Click(object sender, RoutedEventArgs e)
        {
            plugins p = new();
            p.Show();   
        }

        private void peanutio_Click(object sender, RoutedEventArgs e)
        {
            peanutio p = new();
            p.ShowDialog();
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private async void webconsole_Click(object sender, RoutedEventArgs e)
        {
            MessageDialogResult clickresult = await this.ShowMessageAsync("即将进入高能区域", "您正在尝试打开实验性功能！\n我们信任您并且您知道参与试验的要求和风险。\n这些风险包括但不限于以下几点：\n(1) 可能的数据丢失风险\n(2) 可能的系统崩溃\n(3) 可能的程序意外错误\n请您在使用之前，一定要慎重再慎重！", MessageDialogStyle.AffirmativeAndNegative);
            if (clickresult == MessageDialogResult.Negative)//取消
            {
                return;
            }
            else//确认
            {
                webconsole tile = new webconsole();
                tile.Show();
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            about tile = new();
            tile.Show();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            login login = new();
            login.Show();
        }
    }
}
