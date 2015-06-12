using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using EyouSoft.Common.Function;
using EyouSoft.Common;
using EyouSoft.Model.ScenicStructure;

namespace UserBackCenter.ScenicManage
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetImageList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            string scenicid = Utils.GetQueryStringValue("EditId");
            if (scenicid != "")
            {
                context.Response.Clear();
                context.Response.Write(GetMorePic(scenicid));
                context.Response.End();
            }
            string ImgId = Utils.GetQueryStringValue("ImgId");
            if (ImgId!="")
            {
                context.Response.Clear();
                context.Response.Write(DeleteImage(ImgId));
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region 获取景区其他图片的列表
        protected string GetMorePic(string scenicid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul>");
            IList<EyouSoft.Model.ScenicStructure.MScenicImg> PicList = GetListByImgType(ScenicImgType.其他, scenicid);
            foreach (EyouSoft.Model.ScenicStructure.MScenicImg item in PicList)
            {
                sb.Append(string.Format("<li><a href=\"{3}\" target=\"_blank\"><img src=\"{0}\" id=\"{2}\" width=\"12\" height=\"120\" class=\"hotel_pic\" /></a> <div style='width:160px; text-align:center;height:20px;line-height:20px;margin-top:-10px'> {1} <a onclick=\"deleteImage('{2}')\" href=\"javascript:void(0)\">删除</a> </div></li>", Utils.GetNewImgUrl(item.ThumbAddress, 3), Utils.GetText2(item.Description,10,false), item.ImgId,Utils.GetNewImgUrl(item.Address,3)));

            }
            sb.Append("</ul>");
            sb.Append("<div style='clear:both'></div>");
            return sb.ToString();
        }

        #endregion

        #region 根据图片类型获取图片
        /// <summary>
        /// 根据图片类型获取图片
        /// </summary>
        /// <param name="imgtype">图片类型</param>
        /// <returns></returns>
        protected IList<MScenicImg> GetListByImgType(ScenicImgType imgtype, string scenicid)
        {
            IList<MScenicImg> listdefault = new List<MScenicImg>();
            IList<MScenicImg> listScenicImg = EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().GetList(scenicid);
            if (listScenicImg != null)
            {
                if (listScenicImg.Count > 0)
                {
                    foreach (var item in listScenicImg)
                    {
                        if (item.ImgType == imgtype)
                        {
                            if (item != null)
                                listdefault.Add(item);
                        }
                    }
                }
            }
            return listdefault;
        }
        #endregion

        #region 删除图片
        protected string DeleteImage(string ImageId) 
        {
            if (EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().Delete(ImageId))
            {
                return "图片删除成功！";
            }
            else
            {
                return "图片删除失败！";
            }
        }

        #endregion
    }
}
