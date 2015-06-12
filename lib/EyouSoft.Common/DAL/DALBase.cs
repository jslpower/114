using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.Common.DAL
{
    /// <summary>
    /// 数据层访问基类
    /// 读取配置文件，生成数据库可用连接
    /// </summary>
    public class DALBase
    {
        private readonly Database _systemstore = DatabaseFactory.CreateDatabase("SystemStore");
        private readonly Database _tourstore = DatabaseFactory.CreateDatabase("TourStore");
        private readonly Database _companystore = DatabaseFactory.CreateDatabase("CompanyStore");
        private readonly Database _smsstore = DatabaseFactory.CreateDatabase("SMSStore");
        private readonly Database _mqstore = DatabaseFactory.CreateDatabase("MQStore");
        private readonly Database _userstore = DatabaseFactory.CreateDatabase("UserStore");
        private readonly Database _logstore = DatabaseFactory.CreateDatabase("LogStore");
        private readonly Database _mysqlstore = DatabaseFactory.CreateDatabase("MySQLStore");        
        
        /// <summary>
        /// 系统库
        /// </summary>
        protected Database SystemStore
        {
            get
            {
                return _systemstore;
            }
        }

        /// <summary>
        /// 团队库
        /// </summary>
        protected Database TourStore
        {
            get
            {
                return _tourstore;
            }
        }

        /// <summary>
        /// 公司库
        /// </summary>
        protected Database CompanyStore
        {
            get
            {
                return _companystore;
            }
        }

        /// <summary>
        /// 短信库
        /// </summary>
        protected Database SMSStore
        {
            get
            {
                return _smsstore;
            }
        }

        /// <summary>
        /// MQ库
        /// </summary>
        protected Database MQStore
        {
            get
            {
                return _mqstore;
            }
        }

        /// <summary>
        /// 用户库
        /// </summary>
        protected Database UserStore
        {
            get
            {
                return _userstore;
            }
        }

        /// <summary>
        /// 日志库
        /// </summary>
        protected Database LogStore
        {
            get
            {
                return _logstore;
            }
        }

        /// <summary>
        /// MQ库
        /// </summary>
        protected Database MySQLStore
        {
            get
            {
                return _mysqlstore;
            }
        }
        /// <summary>
        /// 社区库
        /// </summary>
        protected Database DiscuzStore
        {
            get
            {
                return DatabaseFactory.CreateDatabase("DiscuzStore");
            }
        }
        /// <summary>
        /// 机票库
        /// </summary>
        protected Database TicketStore
        {
            get 
            {
                return DatabaseFactory.CreateDatabase("TicketStore");
            }
        }

        /// <summary>
        /// 客户池库
        /// </summary>
        protected Database PoolStore
        {
            get { return DatabaseFactory.CreateDatabase("PoolStore"); }
        }

        /// <summary>
        /// 酒店库
        /// </summary>
        protected Database HotelStore
        {
            get
            {
                return DatabaseFactory.CreateDatabase("HotelStore");
            }
        }

        /// <summary>
        /// 各系统数据同步对应关系库
        /// </summary>
        protected Database OpenStore
        {
            get
            {
                return DatabaseFactory.CreateDatabase("OpenStore");
            }
        }
    }
}
