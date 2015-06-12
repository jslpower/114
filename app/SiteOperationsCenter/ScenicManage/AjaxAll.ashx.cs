using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Text;
using EyouSoft.ControlCommon.Control;
using EyouSoft.Model.CompanyStructure;
using System.Collections.Generic;
using EyouSoft.Model.ScenicStructure;
using EyouSoft.Common;

namespace SiteOperationsCenter
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Ajax : EyouSoft.Common.Control.YunYingPage, IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            //this.BaseInit(context);
            bool loginIs = new EyouSoft.Security.Membership.UserProvider().GetMaster() != null ? true : false;
            if (!loginIs)
            {
                context.Response.Write("NoLogin");
                context.Response.End();
                return;
            }


            //参数数组（所有ajax的参数都用$分开）
            //string argument = context.Request.QueryString["arg"];
            string argument = Utils.GetQueryStringValue("arg");
            //执行类型
            string type = Utils.GetQueryStringValue("type");
            string[] argumentlist = { "" };
            if (!string.IsNullOrEmpty(argument))
            {
                if (argument.Contains('$'))
                    argumentlist = argument.Split('$');
                else
                    argumentlist[0] = argument;
            }
            switch (type)
            {
                case "BindContact"://绑定公司用户
                    BindContact(argumentlist);
                    break;

                case "BinLankId"://根据城市获取地标
                    BinLankId(argumentlist);
                    break;
                case "UploadScenicimage"://图片上传
                    UploadImage();
                    break;

                case "DelImage"://删除图片
                    DelImageById(argumentlist);
                    break;

                case "DeleteScenic":
                    DeleteScenic(argumentlist);
                    break;
                case "ListExamineStatue"://批量审核
                    ListExamineStatue(argumentlist);
                    break;
            }


        }

        #region 批量审核
        protected void ListExamineStatue(string[] argumentlist)
        {
            StringBuilder result = new StringBuilder("");

            string[] argList = new string[argumentlist.Length - 1];
            for (var i = 0; i < argumentlist.Length; i++)
            {
                if (i < (argumentlist.Length - 1))
                    argList[i] = argumentlist[i];
            }

            //if (!EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().UpdateExamineStatus(Utils.GetInt(argumentlist.Last()), ExamineStatus.已审核, argList))
            if (!EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().UpdateExamineStatus(Utils.GetInt(argumentlist[argumentlist.Length - 1]), ExamineStatus.已审核, argList))
            {
                result.Append("失败");
            }
            HttpContext.Current.Response.Write(result);
        }

        #endregion


        #region   删除景区 1：删除成功 2：门票存在，不能删除 3：删除失败
        protected void DeleteScenic(string[] argumentlist)
        {
            var result = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().Delete(argumentlist[0], argumentlist[1]);
            if (result == 1)
            {
                HttpContext.Current.Response.Write("1");
            }
            else if (result == 2)
            {
                HttpContext.Current.Response.Write("2");
            }
            else
                HttpContext.Current.Response.Write("3");
        }
        #endregion

        #region   删除图片
        protected void DelImageById(string[] argumentlist)
        {
            if (EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().Delete(argumentlist[0]))
            {
                HttpContext.Current.Response.Write("删除成功");
            }
            else
            {
                HttpContext.Current.Response.Write("error");
            }
        }
        #endregion

        #region 图片上传
        /// <summary>
        /// 图片上传
        /// </summary>
        protected void UploadImage()
        {

        }

        #endregion

        #region 绑定地标区域
        /// <summary>
        /// 绑定地标区域
        /// </summary>
        protected void BinLankId(string[] argumentlist)
        {
            System.Collections.Generic.IList<EyouSoft.Model.SystemStructure.MSystemLandMark> LandMarklist = EyouSoft.BLL.SystemStructure.BSystemLandMark.CreateInstance().GetList(Convert.ToInt32(argumentlist[0]));
            if (LandMarklist.Count > 0)
            {
                StringBuilder strLandMark = new StringBuilder("[");
                foreach (var model in LandMarklist)
                {
                    strLandMark.Append("{\"Por\":\"" + model.Por + "\",\"Id\":\"" + model.Id + "\"},");
                }
                strLandMark.Remove(strLandMark.Length - 1, 1);
                strLandMark.Append("]");
                HttpContext.Current.Response.Write(strLandMark);
            }
            else
                HttpContext.Current.Response.Write("error");

        }
        #endregion

        #region 绑定公司用户
        /// <summary>
        /// 绑定公司用户
        /// </summary>
        protected void BindContact(string[] argumentlist)
        {
            if (!string.IsNullOrEmpty(argumentlist[0]))
            {
                EyouSoft.Model.CompanyStructure.QueryParamsUser modelUser = new EyouSoft.Model.CompanyStructure.QueryParamsUser { IsShowAdmin = true };
                IList<CompanyUserBase> CompanyUserList = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetList(argumentlist[0], modelUser);

                if (CompanyUserList.Count > 0)
                {
                    StringBuilder strB = new StringBuilder("[");
                    string Operator = string.Empty;
                    foreach (var model in CompanyUserList)
                    {
                        if (model.IsAdmin)
                        {
                            Operator += model.UserName;
                        }
                        strB.Append("{\"UserName\":\"" + model.ContactInfo.ContactName + "|" + model.UserName + "\",\"UserId\":\"" + model.ID + "\"},");

                    }
                    strB.Remove(strB.Length - 1, 1);
                    strB.Append("]");

                    HttpContext.Current.Response.Write(strB + "$" + Operator);
                }
                else
                {
                    //HttpContext.Current.Response.Write("{\"errorMsg\":\"没有该公司编号的成员，请检查公司编号是否有误\"}");
                    HttpContext.Current.Response.Write("error");
                }
            }
            else
            {
                HttpContext.Current.Response.Write("请输入公司编号");
            }
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
