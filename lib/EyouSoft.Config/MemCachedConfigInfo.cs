using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Config
{
    /// <summary>
    /// MemCached配置信息类文件
    /// </summary>
    public class MemCachedConfigInfo : IConfigInfo
    {
        private bool _applyMemCached;
        /// <summary>
        /// 是否应用MemCached
        /// </summary>
        public bool ApplyMemCached
        {
            get
            {
                return _applyMemCached;
            }
            set
            {
                _applyMemCached = value;
            }
        }

        private string _serverList;
        /// <summary>
        /// 链接地址
        /// </summary>
        public string ServerList
        {
            get
            {
                return _serverList;
            }
            set
            {
                _serverList = value;
            }
        }

        private string _poolName;
        /// <summary>
        /// 链接池名称
        /// </summary>
        public string PoolName
        {
            get
            {
                return string.IsNullOrEmpty(_poolName) ? "EyouSoft_MemCache" : _poolName;
            }
            set
            {
                _poolName = value;
            }
        }

        private int _intConnections;
        /// <summary>
        /// 初始化链接数
        /// </summary>
        public int IntConnections
        {
            get
            {
                return _intConnections > 0 ? _intConnections : 3;
            }
            set
            {
                _intConnections = value;
            }
        }

        private int _minConnections;
        /// <summary>
        /// 最少链接数
        /// </summary>
        public int MinConnections
        {
            get
            {
                return _minConnections > 0 ? _minConnections : 3;
            }
            set
            {
                _minConnections = value;
            }
        }

        private int _maxConnections;
        /// <summary>
        /// 最大连接数
        /// </summary>
        public int MaxConnections
        {
            get
            {
                return _maxConnections > 0 ? _maxConnections : 5;
            }
            set
            {
                _maxConnections = value;
            }
        }

        private int _socketConnectTimeout;
        /// <summary>
        /// Socket链接超时时间
        /// </summary>
        public int SocketConnectTimeout
        {
            get
            {
                return _socketConnectTimeout > 1000 ? _socketConnectTimeout : 1000;
            }
            set
            {
                _socketConnectTimeout = value;
            }
        }

        private int _socketTimeout;
        /// <summary>
        /// socket超时时间
        /// </summary>
        public int SocketTimeout
        {
            get
            {
                return _socketTimeout > 1000 ? _maintenanceSleep : 3000;
            }
            set
            {
                _socketTimeout = value;
            }
        }

        private int _maintenanceSleep;
        /// <summary>
        /// 维护线程休息时间
        /// </summary>
        public int MaintenanceSleep
        {
            get
            {
                return _maintenanceSleep > 0 ? _maintenanceSleep : 30;
            }
            set
            {
                _maintenanceSleep = value;
            }
        }

        private bool _failOver;
        /// <summary>
        /// 链接失败后是否重启,详情参见http://baike.baidu.com/view/1084309.htm
        /// </summary>
        public bool FailOver
        {
            get
            {
                return _failOver;
            }
            set
            {
                _failOver = value;
            }
        }

        private bool _nagle;
        /// <summary>
        /// 是否用nagle算法启动socket
        /// </summary>
        public bool Nagle
        {
            get
            {
                return _nagle;
            }
            set
            {
                _nagle = value;
            }
        }
    }  
}
