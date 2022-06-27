using CrabSS.publicFile;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
    /// peanutio.xaml 的交互逻辑
    /// </summary>

    public partial class peanutio : MetroWindow
    {
        public peanutio()
        {
            InitializeComponent();
            try
            {
				string result = lingqi.GetHttpResponse("https://peanutio.heycrab.xyz/apps/v2/getList", 3400);
				RootObject b = JsonConvert.DeserializeObject<RootObject>(result);
				list.Visibility = Visibility.Visible;
				loading.Visibility = Visibility.Hidden;
				for(int i = 0; i< b.data.Count; i++)
                {
					var ba = b.data[i];
					list.Items.Add(ba.content + "  简介：" + ba.detail +"  作者："+ ba.author.username);
                }
			}
			catch(Exception ex)
            {
				this.ShowMessageAsync(":( 出错了，但是是程序的错",ex.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
			Process.Start("https://peanutio.heycrab.xyz/?from=crabss_client");
        }
    }
}
