using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;

namespace UserPublicCenter.visa
{
    /// <summary>
    /// 陈蝉鸣 
    /// 2011-5-13
    /// 签证列表页面
    /// </summary>
    public partial class visaList : EyouSoft.Common.Control.FrontPage
    {
        IList<EyouSoft.Model.VisaStructure.Country> CounList = null;
        public string HotNameList;//热点国家
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Initialize();
                this.Title = "旅游签证_最全旅游签证_同业114旅游签证频道";

            }

            (this.Master as UserPublicCenter.MasterPage.NewPublicCenter).HeadMenuIndex = 1;//设置导航条
            string counName = Utils.GetQueryStringValue("q");//所搜索的国家名字                
            if (counName != "")
            {
                
                    if (CounList == null)
                    {
                        EyouSoft.IBLL.VisaStructure.IVisaBll Bvisa = EyouSoft.BLL.VisaStructure.VisaBll.CreateInstance();
                        CounList = Bvisa.GetCountryList();
                    }                   
               
                //根据输入获取提示
                IList<EyouSoft.Model.VisaStructure.Country> Lists = CounList.Where(p => p.CountryCn.StartsWith(counName) || p.CountryJp.StartsWith(counName) || p.CountryEn.StartsWith(counName)).ToList<EyouSoft.Model.VisaStructure.Country>();
                StringBuilder builder = new StringBuilder();
                if (Lists.Count > 0 && Lists != null)
                {
                    foreach (EyouSoft.Model.VisaStructure.Country item in Lists)
                    {
                        builder.AppendFormat("{0}|{1}\n", item.CountryCn, item.Id.ToString());
                    }
                }
                Response.Clear();
                Response.Write(builder.ToString());
                Response.End();
            }
        }

        //初始化页面
        protected void Initialize()
        {
            //获取所有的国家
            EyouSoft.IBLL.VisaStructure.IVisaBll Bvisa = EyouSoft.BLL.VisaStructure.VisaBll.CreateInstance();
            CounList = Bvisa.GetCountryList();
            //获取热点国家
            IList<EyouSoft.Model.VisaStructure.Country> HotList = null;
            string[] HotName = new string[] { "美国", "加拿大", "德国", "法国", "英国", "日本", "韩国", "泰国", "新加坡"};
            HotList = Bvisa.GetHotCountryListByName(HotName);
            StringBuilder HotBuild = new StringBuilder();
            foreach(EyouSoft.Model.VisaStructure.Country item in HotList)
            {
                HotBuild.Append(string.Format("<a href='{0}'>{1}</a>", EyouSoft.Common.URLREWRITE.visa.GetVisaUrl(item.Id.ToString()), item.CountryCn));
            }
            HotNameList = HotBuild.ToString();
            
            //获取欧洲     
            IList<EyouSoft.Model.VisaStructure.Country> EuropeList = CounList.Where(p => p.Areas == EyouSoft.Model.VisaStructure.Areas.欧洲).ToList<EyouSoft.Model.VisaStructure.Country>();
            this.Europe.DataSource = EuropeList;
            this.Europe.DataBind();
            //获取亚洲
            IList<EyouSoft.Model.VisaStructure.Country> AsiaList = CounList.Where(p => p.Areas == EyouSoft.Model.VisaStructure.Areas.亚洲).ToList<EyouSoft.Model.VisaStructure.Country>();
            this.Asia.DataSource = AsiaList;
            this.Asia.DataBind();
            //获取美洲
            IList<EyouSoft.Model.VisaStructure.Country> AmericaList = CounList.Where(p => p.Areas == EyouSoft.Model.VisaStructure.Areas.美洲).ToList<EyouSoft.Model.VisaStructure.Country>();
            this.America.DataSource = AmericaList;
            this.America.DataBind();
            //获取非洲
            IList<EyouSoft.Model.VisaStructure.Country> AfricaList = CounList.Where(p => p.Areas == EyouSoft.Model.VisaStructure.Areas.非洲).ToList<EyouSoft.Model.VisaStructure.Country>();
            this.Africa.DataSource = AfricaList;
            this.Africa.DataBind();
        }
    }
}
