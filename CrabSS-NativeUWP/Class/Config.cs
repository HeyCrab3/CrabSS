using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrabSS_NativeUWP.Class
{
    internal class Config
    {
		public class RootObject
		{
			public string ConfVersion { get; set; } // 目前配置版本是1.1，如果版本低于1.1将会启动迁移程序，如果高于1.1程序拒绝启动
            public string ServerUUID { get; set; } // 服务器UUID
            public string core { get; set; } // 服务器核心
			public string jvav { get; set; } // j v a v
            public string minRamSize { get; set; } // 最小内存
            public string maxRamSize { get; set; } // 最大内存
            // 上面俩都是M做单位，希望不会有哪个倒霉蛋输成G然后让自己的电脑阿姆斯特朗式螺旋升天
        }
	}
}
