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

namespace CrabSS
{
	/// <summary>
	/// about.xaml 的交互逻辑
	/// </summary>

	public class GitHub
	{
	public string html_url { get; set; }
	public string description { get; set; }
	public string fork { get; set; }
	public string url { get; set; }
	public string forks_url { get; set; }
	public string keys_url { get; set; }
	public string collaborators_url { get; set; }
	public string teams_url { get; set; }
	public string hooks_url { get; set; }
	public string issue_events_url { get; set; }
	public string events_url { get; set; }
	public string assignees_url { get; set; }
	public string branches_url { get; set; }
	public string tags_url { get; set; }
	public string blobs_url { get; set; }
	public string git_tags_url { get; set; }
	public string git_refs_url { get; set; }
	public string trees_url { get; set; }
	public string statuses_url { get; set; }
	public string languages_url { get; set; }
	public string stargazers_url { get; set; }
	public string contributors_url { get; set; }
	public string subscribers_url { get; set; }
	public string subscription_url { get; set; }
	public string commits_url { get; set; }
	public string git_commits_url { get; set; }
	public string comments_url { get; set; }
	public string issue_comment_url { get; set; }
	public string contents_url { get; set; }
	public string compare_url { get; set; }
	public string merges_url { get; set; }
	public string archive_url { get; set; }
	public string downloads_url { get; set; }
	public string issues_url { get; set; }
	public string pulls_url { get; set; }
	public string milestones_url { get; set; }
	public string notifications_url { get; set; }
	public string labels_url { get; set; }
	public string releases_url { get; set; }
	public string deployments_url { get; set; }
	public string created_at { get; set; }
	public string updated_at { get; set; }
	public string pushed_at { get; set; }
	public string git_url { get; set; }
	public string ssh_url { get; set; }
	public string clone_url { get; set; }
	public string svn_url { get; set; }
	public string homepage { get; set; }
	public string size { get; set; }
	public string stargazers_count { get; set; }
	public string watchers_count { get; set; }
	public string language { get; set; }
	public string has_issues { get; set; }
	public string has_projects { get; set; }
	public string has_downloads { get; set; }
	public string has_wiki { get; set; }
	public string has_pages { get; set; }
	public string forks_count { get; set; }
	public string mirror_url { get; set; }
	public string archived { get; set; }
	public string disabled { get; set; }
	public string open_issues_count { get; set; }
	public string license { get; set; }
	public string allow_forking { get; set; }
	public string is_template { get; set; }
	public string visibility { get; set; }
	public string forks { get; set; }
	public string open_issues { get; set; }
	public string watchers { get; set; }
	public string default_branch { get; set; }
	public string temp_clone_token { get; set; }
	public string network_count { get; set; }
	public string subscribers_count { get; set; }
}
public partial class about : MetroWindow
    {

        public about()
        {
            InitializeComponent();
			string result = lingqi.GetHttpResponse("https://api.github.com/repos/crabtechs/crabss", 11620000);
			GitHub b = JsonConvert.DeserializeObject<GitHub>(result);
			starcount.Badge = "Star:" + b.stargazers_count;
		}

        private void star_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer","https://github.com/CrabTechs/CrabSS");
        }
    }
}
