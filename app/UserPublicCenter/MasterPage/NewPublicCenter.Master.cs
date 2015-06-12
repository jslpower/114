using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace UserPublicCenter.MasterPage
{
    public partial class NewPublicCenter : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        private int headmenuindex;
        /// <summary>
        /// 当前导航菜单索引 
        /// </summary>
        public int HeadMenuIndex
        {
            get { return headmenuindex; }
            set { headmenuindex = value; }
        }

        /// <summary>
        /// 头部搜索关键字
        /// </summary>
        private string _SearchKeyWord;
        public string SearchKeyWord
        {
            get
            {
                return _SearchKeyWord;               
            }
            set { _SearchKeyWord = value; }
        }




    }
}
