using System;
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
using System.Text;
using EyouSoft.Common;
using EyouSoft.Common.Function;
namespace UserPublicCenter.SupplierInfo.UserControl
{
    public partial class SWFControl : System.Web.UI.UserControl
    {
        protected string ImageServerPath = Domain.FileSystem;
        protected string ImagePath = ImageManage.GetImagerServerUrl(1);
        private StringBuilder strJavaScript = new StringBuilder();
        protected int pic_width=420; //图片宽度
        protected int pic_height=170;//图片高度
        protected void Page_Load(object sender, EventArgs e)
        {
            strJavaScript.Append("<script type=\"text/javascript\">");
            strJavaScript.AppendFormat("var show_text={0};", IsShowTitle ? 1 : 0);
            strJavaScript.Append("var imag = new Array();");
            strJavaScript.Append("var link = new Array();");
            strJavaScript.Append("var text = new Array();");
            switch (SwfType)
            { 
                case 1: //行业资讯
                case 3: //同业学堂
                    IList<EyouSoft.Model.CommunityStructure.InfoArticle> list = EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance().GetTopNumPicList(TopNumber,
                        EyouSoft.Model.CommunityStructure.TopicClass.行业资讯, null, null, true);
                    if (list != null && list.Count > 0)
                    {
                        for (int i = 0; i < list.Count;i++)
                        {
                            EyouSoft.Model.CommunityStructure.InfoArticle model = new EyouSoft.Model.CommunityStructure.InfoArticle();
                            model=list[i];
                            strJavaScript.AppendFormat("imag[{0}]=\"{1}{2}\";", i+1,ImageServerPath,model.ImgPath);
                            strJavaScript.AppendFormat("link[{0}]=\"/SupplierInfo/{1}?Id={2}\";", i + 1,model.TopicClassId==EyouSoft.Model.CommunityStructure.TopicClass.行业资讯?
                                "ArticleInfo.aspx" : "SchoolIntroductionInfo.aspx",model.ID);
                            strJavaScript.AppendFormat("text[{0}]=\"{1}\";",i+1,model.ArticleTitle);
                            model = null;
                        }
                    }
                    list = null;
                    break;
                case 2: // 嘉宾访谈
                    IList<EyouSoft.Model.CommunityStructure.HonoredGuest> GuestList = EyouSoft.BLL.CommunityStructure.HonoredGuest.CreateInstance().GetTopNumList(TopNumber);
                    if (GuestList != null && GuestList.Count > 0)
                    {
                        for (int i = 0; i < GuestList.Count; i++)
                        {
                            EyouSoft.Model.CommunityStructure.HonoredGuest guestmodel = new EyouSoft.Model.CommunityStructure.HonoredGuest();
                            guestmodel = GuestList[i];
                            strJavaScript.AppendFormat("imag[{0}]=\"{1}{2}\";", i + 1, ImageServerPath, guestmodel.ImgPath);
                            strJavaScript.AppendFormat("link[{0}]=\"/SupplierInfo/HonoredGuestInfo.aspx?Id={1}\";", i + 1,guestmodel.ID);
                            strJavaScript.AppendFormat("text[{0}]=\"{1}\";", i + 1, guestmodel.Title);
                            guestmodel = null;
                        }
                    }
                    break;
            }
            strJavaScript.Append("</script>");
            Page.Header.Controls.Add(new LiteralControl(strJavaScript.ToString()));
        }
        /// <summary>
        /// 显示条数
        /// </summary>
        public int TopNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 是否显示标题
        /// </summary>
        public bool IsShowTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 栏目类型（1：行业资讯 2：嘉宾访谈 3：同业学堂）
        /// </summary>
        public int SwfType
        {
            get;
            set;
        }
        /// <summary>
        /// Swf高度
        /// </summary>
        public int SwfHeight
        {
            set 
            {
                pic_height = value;
            }
        }
        /// <summary>
        /// Swf宽度
        /// </summary>
        public int SwfWidth
        {
            set 
            {
                pic_width = value;
            }
        }
    }
}