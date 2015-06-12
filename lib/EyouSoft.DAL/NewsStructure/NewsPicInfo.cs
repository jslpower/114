using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
namespace EyouSoft.DAL.NewsStructure
{
    /// <summary>
    /// 资讯焦点图片数据层
    /// </summary>
    /// 鲁功源 2011-03-31
    public class NewsPicInfo : DALBase, IDAL.NewsStructure.INewsPicInfo
    {
        #region SQL变量
        private const string SQL_NewsPicInfo_Delete = "delete tbl_NewsPicInfo ;";
        private const string SQL_NewsPicInfo_Add = "insert into tbl_NewsPicInfo(Category,PicPath,PicUrl,PicTitle,OperatorId,IssueTime) values({0},'{1}','{2}','{3}',{4},getdate());";
        private const string SQL_NewsPicInfo_GetAll = "select Id,Category,PicPath,PicUrl,PicTitle,OperatorId,IssueTime from tbl_NewsPicInfo";
        #endregion

        #region 数据库变量
        /// <summary>
        /// database
        /// </summary>
        private Database _db = null;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public NewsPicInfo() 
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region INewsPicInfo 成员
        /// <summary>
        /// 添加焦点图片信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(EyouSoft.Model.NewsStructure.NewsPicInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(SQL_NewsPicInfo_Delete);
            if (model.CommunityPic != null)
                strSql.AppendFormat(SQL_NewsPicInfo_Add, (int)EyouSoft.Model.NewsStructure.PicCategory.同行社区左侧, model.CommunityPic.PicPath, model.CommunityPic.PicUrl,
                    model.CommunityPic.PicTitle, model.OperatorId);
            if (model.FocusRightPic != null)
                strSql.AppendFormat(SQL_NewsPicInfo_Add, (int)EyouSoft.Model.NewsStructure.PicCategory.首页焦点新闻右侧公告, model.FocusRightPic.PicPath, model.FocusRightPic.PicUrl,
                    model.FocusRightPic.PicTitle, model.OperatorId);
            if (model.SchoolFocusPic != null)
                strSql.AppendFormat(SQL_NewsPicInfo_Add, (int)EyouSoft.Model.NewsStructure.PicCategory.同业学堂焦点, model.SchoolFocusPic.PicPath, model.SchoolFocusPic.PicUrl,
                    model.SchoolFocusPic.PicTitle, model.OperatorId);
            if (model.SchoolRightPic != null)
                strSql.AppendFormat(SQL_NewsPicInfo_Add, (int)EyouSoft.Model.NewsStructure.PicCategory.同业学堂右侧, model.SchoolRightPic.PicPath, model.SchoolRightPic.PicUrl,
                    model.SchoolRightPic.PicTitle, model.OperatorId);
            if (model.TravelFocusPic != null)
                strSql.AppendFormat(SQL_NewsPicInfo_Add, (int)EyouSoft.Model.NewsStructure.PicCategory.旅游资讯焦点, model.TravelFocusPic.PicPath, model.TravelFocusPic.PicUrl,
                    model.TravelFocusPic.PicTitle, model.OperatorId);
            if (model.FocusPic != null && model.FocusPic.Count > 0)
            {
                foreach (var item in model.FocusPic)
                {
                    strSql.AppendFormat(SQL_NewsPicInfo_Add, (int)EyouSoft.Model.NewsStructure.PicCategory.首页焦点图片, item.PicPath, item.PicUrl,item.PicTitle, model.OperatorId);
                }
            }
            if (model.CommunityMiddlePic != null && model.CommunityMiddlePic.Count > 0)
            {
                foreach (var item in model.CommunityMiddlePic)
                {
                    strSql.AppendFormat(SQL_NewsPicInfo_Add, (int)EyouSoft.Model.NewsStructure.PicCategory.同行社区中间, item.PicPath, item.PicUrl, item.PicTitle, model.OperatorId);
                }
            }
            DbCommand dc = this._db.GetSqlStringCommand(strSql.ToString());
            return DbHelper.ExecuteSqlTrans(dc, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 获取焦点图片相关的所有信息
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.BasicNewsPic> GetList()
        {
            IList<EyouSoft.Model.NewsStructure.BasicNewsPic> list = new List<EyouSoft.Model.NewsStructure.BasicNewsPic>();
            DbCommand dc = this._db.GetSqlStringCommand(SQL_NewsPicInfo_GetAll);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.NewsStructure.BasicNewsPic model = new EyouSoft.Model.NewsStructure.BasicNewsPic();
                    if (!dr.IsDBNull(dr.GetOrdinal("Category")))
                        model.Category = (EyouSoft.Model.NewsStructure.PicCategory)int.Parse(dr[dr.GetOrdinal("Category")].ToString());
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    model.PicPath = dr.IsDBNull(dr.GetOrdinal("PicPath")) ? string.Empty : dr[dr.GetOrdinal("PicPath")].ToString();
                    model.PicTitle = dr.IsDBNull(dr.GetOrdinal("PicTitle")) ? string.Empty : dr[dr.GetOrdinal("PicTitle")].ToString();
                    model.PicUrl = dr.IsDBNull(dr.GetOrdinal("PicUrl")) ? string.Empty : dr[dr.GetOrdinal("PicUrl")].ToString();
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }

        #endregion
    }

}
