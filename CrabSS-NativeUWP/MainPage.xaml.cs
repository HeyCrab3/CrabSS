using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;
using System.Windows;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace CrabSS_NativeUWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            contentFrame.Navigate(typeof(home));
        }

        private void nvSample_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                contentFrame.Navigate(typeof(Settings));
                sender.Header = "设置";
            }
            else
            {
                var selectedItem = (NavigationViewItem)args.SelectedItem;
                if (selectedItem != null)
                {
                    string selectedItemTag = (string)selectedItem.Tag;
                    string selectedItemTitle = (string)selectedItem.Content;
                    sender.Header = selectedItemTitle;
                    string pageName = selectedItemTag;
                    if (pageName == "home")
                    {
                        contentFrame.Navigate(typeof(home));
                    }
                    else if (pageName == "settings")
                    {
                        contentFrame.Navigate(typeof(User));
                    }
                    else if (pageName == "serversetting")
                    {
                        contentFrame.Navigate(typeof(serversetting));
                    }
                    else if (pageName == "console")
                    {
                        contentFrame.Navigate(typeof(console));
                    }
                    else if (pageName == "plugins")
                    {
                        contentFrame.Navigate(typeof(plugins));
                    }
                    else
                    {
                        contentFrame.Navigate(typeof(home));
                    }
                }
            }
        }
    }
}
