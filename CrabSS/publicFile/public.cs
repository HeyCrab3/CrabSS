using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 公共定义文件
namespace CrabSS
{
    internal class @public
    {
        public class settinginfo//配置文件模块，用来读取用户个性化设置
        {
            public string Version { get; set; }
            public string WindowTitle { get; set; }
            public string ColorTheme { get; set; }
            public int minRamSize { get; set; }
            public int maxRamSize { get; set; }
            public string CustomJavaPath { get; set; }
            public bool isFrpOn { get; set; }
            public string background { get; set; }
        }
		public class Author
		{
			public string _id { get; set; }
			public string email { get; set; }
			public string password { get; set; }
			public string username { get; set; }
		}

		public class Data
		{
			public string _id { get; set; }
			public Author author { get; set; }
			public string content { get; set; }
			public string detail { get; set; }
			public string link { get; set; }
		}

		public class RootObject
		{
			public string code { get; set; }
			public List<Data> data { get; set; }
			public string msg { get; set; }
		}
	}
}
