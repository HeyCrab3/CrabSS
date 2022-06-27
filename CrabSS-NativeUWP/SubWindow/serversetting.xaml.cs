using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static CrabSS_NativeUWP.Class.Config;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace CrabSS_NativeUWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class serversetting : Page
    {
        public serversetting()
        {
            this.InitializeComponent();
            zhubo.Title = "正在加载配置文件";
            zhubo.IsOpen = true;
            // 加载 config.json
            try
            {
                string json = File.ReadAllText("config.json");
                var config = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(json);
                jvav.Text = config.jvav;
                min.Text = config.minRamSize;
                max.Text = config.maxRamSize;
                core.Text = config.core;
                uuid.Text = "服务器UUID：" + config.ServerUUID + " (配置版本:" + config.ConfVersion + ")";
                zhubo.Title = "已完成！";
                zhubo.IsOpen = false;
            }
            catch (Exception ex)
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = XamlRoot;
                dialog.Title = ":( 发生了错误";
                dialog.PrimaryButtonText = "好的";
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.Content = "主播你没开过服务器？或者你把配置给删了？主播~看看你的配置吧（如果主播没开过可以走了）\n" + ex;
                dialog.ShowAsync();
                string id = Guid.NewGuid().ToString();
                uuid.Text = "服务器UUID：" + id;
                zhubo.Title = "已完成！";
                zhubo.IsOpen = false;
            }
            try
            {
                string path = @"plugins";
                string[] files = Directory.GetFiles(path, "*.jar");
                foreach (string file in files)
                {
                    plugins.Items.Add(file);
                }
                count.Text = "这个服务器一共有 " + plugins.Items.Count + " 个插件";
            }
            catch (Exception ex)
            {
                count.Text = "这个服务器一共有 " + plugins.Items.Count + " 个插件";
            }
        }

        private async void choose_Click(object sender, RoutedEventArgs e)
        {
            zhubo.Title = "正在复制核心";
            zhubo.IsOpen = true;
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.FileTypeFilter.Add(".jar");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // 将选中的文件复制到程序目录
                var local = Windows.Storage.ApplicationData.Current.LocalFolder;
                var fileName = file.Name;
                var filePath = local.Path + "\\" + fileName;
                await file.CopyAsync(local, fileName, Windows.Storage.NameCollisionOption.ReplaceExisting);
                // 将文件路径显示在文本框中
                core.Text = filePath;
                zhubo.Title = "已完成！";
                zhubo.IsOpen = false;
            }
            else
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = XamlRoot;
                dialog.Title = "主播你还没选择Jar包呢";
                dialog.PrimaryButtonText = "哦我魔怔了";
                dialog.CloseButtonText = "哦我魔怔了";
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.Content = "主播你没有选择jar文件或者你把窗口关掉了";
                await dialog.ShowAsync();
                zhubo.Title = "主播，出错了";
                zhubo.IsOpen = false;
            }
        }

        private async void jvab_Click(object sender, RoutedEventArgs e)
        {
            zhubo.Title = "写入 Java";
            zhubo.IsOpen = true;
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.FileTypeFilter.Add(".exe");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                jvav.Text = file.Name;
                zhubo.Title = "已完成！";
                zhubo.IsOpen = false;
            }
            else
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = XamlRoot;
                dialog.Title = "主播你还没选择Java呢";
                dialog.PrimaryButtonText = "哦我魔怔了";
                dialog.CloseButtonText = "哦我魔怔了";
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.Content = "主播你魔怔到把java都吃了是吧😅";
                await dialog.ShowAsync();
                zhubo.Title = "主播，出错了";
                zhubo.IsOpen = false;
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            // 数据全部写入config.json
            zhubo.Title = "正在写入配置";
            zhubo.IsOpen = true;
            string json = "{\"ConfVersion\":\"1.1" + "\",\"ServerUUID\":\"" + uuid.Text + "\",\"core\":\"" + @core.Text + "\",\"jvav\":\"" + jvav.Text + "\",\"minRamSize\":\"" + min.Text + "\",\"maxRamSize\":\"" + max.Text + "\"}";
            string path = Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\config.json";
            File.WriteAllText(path, json);
            zhubo.Title = "已完成！";
            zhubo.IsOpen = false;
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            zhubo.Title = "正在生成脚本";
            zhubo.IsOpen = true;
            // 生成start.cmd
            string start = jvav.Text + " -Xms" + min.Text + "M -Xmx" + max.Text + "M -jar" + core.Text;
            string path = Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\start.cmd";
            File.WriteAllText(path,start);
            zhubo.Title = "已完成！";
            zhubo.IsOpen = false;
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = XamlRoot;
            dialog.Title = "确认？";
            dialog.PrimaryButtonText = "我放弃这台服务器";
            dialog.CloseButtonText = "啊我魔怔了";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = "主播确认吗？这个操作不可逆！";
            var result = await dialog.ShowAsync();
            zhubo.Title = "等待确认...";
            zhubo.IsOpen = true;
            if (result == ContentDialogResult.Primary)
            {
                zhubo.Title = "删除文件...";
                zhubo.IsOpen = true;
                File.Delete("*.jar");
                File.Delete("*.properites");
                File.Delete("*.cmd");
                File.Delete("*.json");
                File.Delete(@"plugins\*.*");
            }
            else if (result == ContentDialogResult.Secondary)
            {
                zhubo.Title = "已完成！";
                zhubo.IsOpen = false;
            }
        }

        private void Console_Click(object sender, RoutedEventArgs e)
        {
            // 打开控制台
            zhubo.Title = "正在打开控制台";
            zhubo.IsOpen = true;
            // 调出来 console 这个 Page
            Frame.Navigate(typeof(console));
            zhubo.Title = "已完成！";
            zhubo.IsOpen = false;
        }

        private void refe_Click(object sender, RoutedEventArgs e)
        {
            plugins.Items.Clear();
            try
            {
                string path = @"plugins";
                string[] files = Directory.GetFiles(path, "*.jar");
                foreach (string file in files)
                {
                    plugins.Items.Add(file);
                }
                count.Text = "这个服务器一共有 " + plugins.Items.Count + " 个插件";
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = XamlRoot;
                dialog.Title = ":) 成功";
                dialog.PrimaryButtonText = "好的";
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.Content = "已刷新插件列表";
                dialog.ShowAsync();
            }
            catch (Exception ex)
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = XamlRoot;
                dialog.Title = ":( 发生了错误";
                dialog.PrimaryButtonText = "好的";
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.Content = "主播你没开过服务器？\n" + ex;
                dialog.ShowAsync();
                count.Text = "这个服务器一共有 " + plugins.Items.Count + " 个插件";
            }
        }

        private async void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var picker = new Windows.Storage.Pickers.FileOpenPicker();
                picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
                picker.FileTypeFilter.Add(".jar");
                Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
                if (file != null)
                {
                    plugins.Items.Clear();
                    var local = Windows.Storage.ApplicationData.Current.LocalFolder;
                    var fileName = file.Name;
                    var filePath = local.Path + "\\plugins\\" + fileName;
                    await file.CopyAsync(local, fileName, Windows.Storage.NameCollisionOption.ReplaceExisting);
                    string path = @"plugins";
                    string[] files = Directory.GetFiles(path, "*.jar");
                    foreach (string filea in files)
                    {
                        plugins.Items.Add(filea);
                    }
                    count.Text = "这个服务器一共有 " + plugins.Items.Count + " 个插件";
                    ContentDialog dialog = new ContentDialog();
                    dialog.XamlRoot = XamlRoot;
                    dialog.Title = ":) 成功";
                    dialog.PrimaryButtonText = "好的";
                    dialog.DefaultButton = ContentDialogButton.Primary;
                    dialog.Content = "已复制插件到服务器";
                    dialog.ShowAsync();
                }
                try
                {
                    String path = @"plugins";
                    string[] files = Directory.GetFiles(path, "*.jar");
                    int i = 0;
                    foreach (string filea in files)
                    {
                        i++;
                    }
                }
                catch (Exception ex)
                {
                    ContentDialog dialog = new ContentDialog();
                    dialog.XamlRoot = XamlRoot;
                    dialog.Title = ":( 发生了错误，但不是程序的错";
                    dialog.PrimaryButtonText = "好的";
                    dialog.DefaultButton = ContentDialogButton.Primary;
                    dialog.Content = "不是程序的错，别找螃蟹\n" + ex;
                    dialog.ShowAsync();
                }
            }
            catch (Exception ex)
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = XamlRoot;
                dialog.Title = ":( 发生了错误，但是是程序的错";
                dialog.PrimaryButtonText = "好的";
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.Content = "请立刻在GitHub上提交Issue！\n" + ex;
                dialog.ShowAsync();
            }
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            var selected = plugins.SelectedItem;
            if (selected != null)
            {
                try
                {
                    // 删除插件
                    var local = Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\plugins\\";
                    var path = Path.Combine(local, selected.ToString());
                    File.Delete(path);
                    plugins.Items.Remove(selected);
                    count.Text = "这个服务器一共有 " + plugins.Items.Count + " 个插件";
                }
                catch (Exception ex)
                {
                    ContentDialog dialog = new ContentDialog();
                    dialog.XamlRoot = XamlRoot;
                    dialog.Title = ":( 发生了错误，但不是程序的错";
                    dialog.PrimaryButtonText = "好的";
                    dialog.DefaultButton = ContentDialogButton.Primary;
                    dialog.Content = "不是程序的错，别找螃蟹\n" + ex;
                    dialog.ShowAsync();
                }
            }
        }
    }
}
