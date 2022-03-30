using MahApps.Metro.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
	/// about.xaml 的交互逻辑
	/// </summary>
public partial class about : MetroWindow
    {

        public about()
        {
            InitializeComponent();
            string result = lingqi.GetHttpResponse("https://api.github.com/repos/crabtechs/crabss", 6000);
            string updatelog = lingqi.GetHttpResponse("https://v6.crabapi.cn/beta2/api/crabss/updatelog", 4000);
            string version = lingqi.GetHttpResponse("https://v6.crabapi.cn/api/crabss/version?channel=beta", 4000);
            GitHub b = JsonConvert.DeserializeObject<GitHub>(result);
            log.Text = updatelog;
            starcount.Badge = "Star:" + b.stargazers_count;
            if (version == "0.15-dev")
            {
                check.Visibility = Visibility.Hidden;
                @new.Visibility = Visibility.Visible;
                title.Content = title.Content + "(版本："+ version +")";
            }
            else
            {
                check.Visibility = Visibility.Hidden;
                updatefound.Visibility = Visibility.Visible;
                ver.Content = version;
            }
		}

        private void star_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer","https://github.com/CrabTechs/CrabSS");
        }
    }
}
