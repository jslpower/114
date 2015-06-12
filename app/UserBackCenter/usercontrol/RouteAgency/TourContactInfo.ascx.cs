using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common;
using System.Text;

namespace UserBackCenter.usercontrol.RouteAgency
{
    public partial class TourContactInfo : System.Web.UI.UserControl
    {
        private string _containerid;
        public string ContainerID
        {
            set { _containerid = value; }
            get { return _containerid; }
        }
        private string _releasetype;
        /// <summary>
        /// 判断是标准线路还是标准团队
        /// </summary>
        public string ReleaseType
        {
            set { _releasetype = value; }
            get { return _releasetype; }
        }
        private string _companyid;
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }

        private string _contactname;
        /// <summary>
        /// 默认联系人
        /// </summary>
        public string ContactName
        {
            set { _contactname = value; }
            get { return _contactname; }
        }

        private string _contacttel;
        /// <summary>
        /// 默认联系人电话
        /// </summary>
        public string ContactTel
        {
            set { _contacttel = value; }
            get { return _contacttel; }
        }

        private string _contactmqid;
        /// <summary>
        /// 默认联系人MQID
        /// </summary>
        public string ContactMQID
        {
            set { _contactmqid = value; }
            get { return _contactmqid; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}