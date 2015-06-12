using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;

namespace UserPublicCenter.visa
{
    /// <summary>
    /// 陈蝉鸣 
    /// 2011-5-13
    /// 签证详细页面
    /// </summary>

    public partial class visaDetail : EyouSoft.Common.Control.FrontPage
    {
        protected EyouSoft.Model.VisaStructure.Visa Mvisa = null;//签证信息实体对象
        protected EyouSoft.Model.VisaStructure.Country Mcoun = null;//国家信息实体对象
        //签证资料
        protected string[] typeValue = new string[] { "没有该信息", "没有该信息", "没有该信息", "没有该信息", "没有该信息", "没有该信息" };
        protected void Page_Load(object sender, EventArgs e)
        {
            (this.Master as UserPublicCenter.MasterPage.NewPublicCenter).HeadMenuIndex = 1;//设置导航条
           
            EyouSoft.IBLL.VisaStructure.IVisaBll Bvisa = EyouSoft.BLL.VisaStructure.VisaBll.CreateInstance();
            int stateId = Utils.GetInt(Utils.GetQueryStringValue("stateId"), 0);
            if (stateId != 0)
            {
                Mvisa = Bvisa.GetVisaInfoByCountry(stateId);
                IList<EyouSoft.Model.VisaStructure.Country> CounList = Bvisa.GetCountryList();//获取所有国家
                Mcoun = (EyouSoft.Model.VisaStructure.Country)CounList.Where(p => p.Id == stateId).FirstOrDefault(); //获取当前查看国的实体信息
                
                //获取不同类别的签证资料
                if (Mvisa != null)
                {
                    if (Mvisa.FileInfos != null)
                    {
                        int len = Mvisa.FileInfos.Count;
                        for (int k = 0; k < len; k++ )
                        {
                            switch (Mvisa.FileInfos[k].FileType)
                            {
                                case EyouSoft.Model.VisaStructure.FileType.个人身份证明:
                                    typeValue[0] = Mvisa.FileInfos[k].FileInfo;
                                    break;
                                case EyouSoft.Model.VisaStructure.FileType.资产证明:
                                    typeValue[1] = Mvisa.FileInfos[k].FileInfo;
                                    break;
                                case EyouSoft.Model.VisaStructure.FileType.工作证明:
                                    typeValue[2] = Mvisa.FileInfos[k].FileInfo;
                                    break;
                                case EyouSoft.Model.VisaStructure.FileType.学生及儿童:
                                    typeValue[3] = Mvisa.FileInfos[k].FileInfo;
                                    break;
                                case EyouSoft.Model.VisaStructure.FileType.老人:
                                    typeValue[4] = Mvisa.FileInfos[k].FileInfo;
                                    break;
                                case EyouSoft.Model.VisaStructure.FileType.其他:
                                    typeValue[5] = Mvisa.FileInfos[k].FileInfo;
                                    break;
                            }
                        }
                    }
                }                
            }

            else
            {
                Panel1.Visible = false;
                Panel2.Visible = true;
            }
            if (Mcoun != null)
            {
                this.Title = "旅游签证_" + Mcoun.CountryCn + "_最全旅游签证_同业114旅游签证频道";
            }
            else
            {
                this.Title = "旅游签证_最全旅游签证_同业114旅游签证频道";
            }
           


        }

    }
}
