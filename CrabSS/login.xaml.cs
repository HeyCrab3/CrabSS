using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    /// login.xaml 的交互逻辑
    /// </summary>
    public partial class login : MetroWindow
    {
        public login()
        {
            InitializeComponent();
        }

        private async void loginbtn_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("username",email.Text);
            data.Add("pwd",pwd.Password);
            var result = lingqi.Post("https://v6.crabapi.cn/beta2/api/login",data);
            LoginInfo b = JsonConvert.DeserializeObject<LoginInfo>(result);
            if (b.code != 0)
            {
                this.ShowMessageAsync("登陆失败",b.msg);
            }
            else
            {
                this.ShowMessageAsync("操作成功",b.msg);
            }
        }
    }
}
