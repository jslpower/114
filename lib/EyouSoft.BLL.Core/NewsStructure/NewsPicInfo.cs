using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.NewsStructure
{
    /// <summary>
    /// 资讯焦点图片业务层
    /// </summary>
    /// 鲁功源 2011-04-01
    public class NewsPicInfo:IBLL.NewsStructure.INewsPicInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public NewsPicInfo() { }
        private readonly IDAL.NewsStructure.INewsPicInfo dal = ComponentFactory.CreateDAL<IDAL.NewsStructure.INewsPicInfo>();

        /// <summary>
        /// 构造资讯焦点图片接口
        /// </summary>
        /// <returns></returns>
        public static IBLL.NewsStructure.INewsPicInfo CreateInstance()
        {
            IBLL.NewsStructure.INewsPicInfo op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.NewsStructure.INewsPicInfo>();
            }
            return op;
        }

        #region INewsPicInfo 成员
        /// <summary>
        /// 添加资讯焦点图片
        /// </summary>
        /// <param name="model">资讯焦点图片实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Add(EyouSoft.Model.NewsStructure.NewsPicInfo model)
        {
            if (model == null)
                return false;
            return dal.Add(model);
        }
        /// <summary>
        /// 获取资讯焦点图片
        /// </summary>
        /// <returns></returns>
        public EyouSoft.Model.NewsStructure.NewsPicInfo GetModel()
        {
            IList<EyouSoft.Model.NewsStructure.BasicNewsPic> list = dal.GetList();
            if (list == null || list.Count == 0)
                return null;
            EyouSoft.Model.NewsStructure.NewsPicInfo model = new EyouSoft.Model.NewsStructure.NewsPicInfo();
            model.FocusPic = list.Where(item => item.Category == EyouSoft.Model.NewsStructure.PicCategory.首页焦点图片).ToList();
            model.CommunityPic = list.SingleOrDefault(item => item.Category == EyouSoft.Model.NewsStructure.PicCategory.同行社区左侧);
            model.CommunityMiddlePic = list.Where(item => item.Category == EyouSoft.Model.NewsStructure.PicCategory.同行社区中间).ToList();
            model.FocusRightPic = list.SingleOrDefault(item => item.Category == EyouSoft.Model.NewsStructure.PicCategory.首页焦点新闻右侧公告);
            model.SchoolFocusPic = list.SingleOrDefault(item => item.Category == EyouSoft.Model.NewsStructure.PicCategory.同业学堂焦点);
            model.SchoolRightPic = list.SingleOrDefault(item => item.Category == EyouSoft.Model.NewsStructure.PicCategory.同业学堂右侧);
            model.TravelFocusPic = list.SingleOrDefault(item => item.Category == EyouSoft.Model.NewsStructure.PicCategory.旅游资讯焦点);
            list = null;
            return model;
        }

        #endregion
    }

}
