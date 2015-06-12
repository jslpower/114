using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL.CompanyStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-25
    /// 描述：公司资讯数据层
    /// </summary>
    public class CompanyAffiche : DALBase, ICompanyAffiche
    {
        #region Sql变量定义
        private const string SQL_CompanyAffiche_INSERT = "INSERT INTO tbl_CompanyAffiche(CompanyType,CompanyId,OperatorName,OperatorID,AfficheClass,AfficheTitle,AfficheInfo,IsHot,IsPicNews,PicPath,IsTop) VALUES(@CompanyType,@CompanyId,@OperatorName,@OperatorID,@AfficheClass,@AfficheTitle,@AfficheInfo,@IsHot,@IsPicNews,@PicPath,@IsTop)";
        private const string SQL_CompanyAffiche_UPDATE = "UPDATE tbl_CompanyAffiche SET AfficheClass=@AfficheClass,AfficheTitle=@AfficheTitle,AfficheInfo=@AfficheInfo,IsHot=@IsHot,IsPicNews=@IsPicNews,PicPath=@PicPath,IsTop=@IsTop WHERE ID=@ID";
        private const string SQL_CompanyAffiche_DELETE = "DELETE tbl_CompanyAffiche WHERE ID=@ID";
        private const string SQL_CompanyAffiche_SETISTOP = "UPDATE tbl_CompanyAffiche SET IsTop=@IsTop WHERE ID=@ID";
        private const string SQL_CompanyAffiche_SETCLICKS = "UPDATE tbl_CompanyAffiche SET Clicks=Clicks+1 WHEREID=@ID";
        private const string SQL_CompanyAffiche_SETISHOT = "UPDATE tbl_CompanyAffiche SET IsHot=@IsHot WHERE ID=@ID";
        private const string SQL_CompanyAffiche_GETMODEL = "SELECT ID,OperatorName,CompanyID,AfficheClass,AfficheTitle,AfficheInfo,IsHot,IsTop,PicPath,IssueTime from tbl_CompanyAffiche WHERE ID=@ID";
        #endregion

        #region 构造函数
        /// <summary>
        /// 所在的数据库
        /// </summary>
        protected Database _database = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        public CompanyAffiche()
        {
            this._database = base.CompanyStore;
        }
        #endregion

        #region ICompanyAffiche成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="Model">公司资讯实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Add(EyouSoft.Model.CompanyStructure.CompanyAffiche Model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyAffiche_INSERT);
            this._database.AddInParameter(dc, "CompanyType", DbType.Byte, (int)Model.CompanyType);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, Model.CompanyId);
            this._database.AddInParameter(dc, "OperatorName", DbType.String, Model.OperatorName);
            this._database.AddInParameter(dc, "OperatorID", DbType.AnsiStringFixedLength, Model.OperatorID);
            this._database.AddInParameter(dc, "AfficheClass", DbType.Byte, (int)Model.AfficheClass);
            this._database.AddInParameter(dc, "AfficheTitle", DbType.String, Model.AfficheTitle);
            this._database.AddInParameter(dc, "AfficheInfo", DbType.String, Model.AfficheInfo);
            this._database.AddInParameter(dc, "IsHot", DbType.AnsiStringFixedLength, Model.IsHot ? "1" : "0");
            this._database.AddInParameter(dc, "IsPicNews", DbType.AnsiStringFixedLength, Model.IsPicNews ? "1" : "0");
            this._database.AddInParameter(dc, "PicPath", DbType.String, Model.PicPath);
            this._database.AddInParameter(dc, "IsTop", DbType.AnsiStringFixedLength, Model.IsTop ? "1" : "0");
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Model">公司资讯实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Update(EyouSoft.Model.CompanyStructure.CompanyAffiche Model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyAffiche_UPDATE);
            this._database.AddInParameter(dc, "ID", DbType.Int32, Model.ID);
            this._database.AddInParameter(dc, "AfficheClass", DbType.Byte, (int)Model.AfficheClass);
            this._database.AddInParameter(dc, "AfficheTitle", DbType.String, Model.AfficheTitle);
            this._database.AddInParameter(dc, "AfficheInfo", DbType.String, Model.AfficheInfo);
            this._database.AddInParameter(dc, "IsHot", DbType.AnsiStringFixedLength, Model.IsHot ? "1" : "0");
            this._database.AddInParameter(dc, "IsPicNews", DbType.AnsiStringFixedLength, Model.IsPicNews ? "1" : "0");
            this._database.AddInParameter(dc, "PicPath", DbType.String, Model.PicPath);
            this._database.AddInParameter(dc, "IsTop", DbType.AnsiStringFixedLength, Model.IsTop ? "1" : "0");
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Delete(int ID)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyAffiche_DELETE);
            this._database.AddInParameter(dc, "ID", DbType.Int32, ID);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 设置置顶
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="IsTop">是否置顶</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool SetIsTop(int ID, bool IsTop)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyAffiche_SETISTOP);
            this._database.AddInParameter(dc, "ID", DbType.Int32, ID);
            this._database.AddInParameter(dc, "IsTop", DbType.AnsiStringFixedLength, IsTop ? "1" : "0");
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 更新阅读次数
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool SetClicks(int ID)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyAffiche_SETCLICKS);
            this._database.AddInParameter(dc, "ID", DbType.Int32, ID);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 设置热门
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="IsHot">是否热门</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool SetIsHot(int ID, bool IsHot)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyAffiche_SETISHOT);
            this._database.AddInParameter(dc, "ID", DbType.Int32, ID);
            this._database.AddInParameter(dc, "IsHot", DbType.AnsiStringFixedLength, IsHot ? "1" : "0");
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 获取资讯实体类
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>资讯实体</returns>
        public virtual EyouSoft.Model.CompanyStructure.CompanyAffiche GetModel(int ID)
        {
            EyouSoft.Model.CompanyStructure.CompanyAffiche model = null;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyAffiche_GETMODEL);
            this._database.AddInParameter(dc, "ID", DbType.Int32, ID);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.CompanyStructure.CompanyAffiche();
                    model.ID = dr.GetInt32(0);
                    model.OperatorName = dr.GetString(1);
                    model.CompanyId = dr.GetString(2);
                    if (!dr.IsDBNull(3))
                        model.AfficheClass = (EyouSoft.Model.CompanyStructure.CompanyAfficheType)int.Parse(dr.GetByte(3).ToString());
                    model.AfficheTitle = dr.GetString(4);
                    model.AfficheInfo = dr.GetString(5);
                    model.IsHot = dr.GetString(6) == "1" ? true : false;
                    model.IsTop = dr.GetString(7) == "1" ? true : false;
                    model.PicPath = dr.GetString(8);
                    model.IssueTime = dr.GetDateTime(9);
                }
            }
            return model;
        }
        /// <summary>
        /// 分页获取指定公司指定资讯类别的列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyID">公司编号 =""返回所有公司，否则返回指定公司的数据</param>
        /// <param name="afficheType">资讯类别 =null返回所有类别，否则返回指定类别的数据</param>
        /// <param name="IsHot">是否热门 =null返回所有</param>
        /// <param name="IsTop">是否置顶 =null返回所有</param>
        /// <param name="IsPicNews">是否图片新闻 =null返回所有</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.CompanyAffiche> GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyID, EyouSoft.Model.CompanyStructure.CompanyAfficheType? afficheType, bool? IsHot, bool? IsTop, bool? IsPicNews)
        {
            IList<EyouSoft.Model.CompanyStructure.CompanyAffiche> list = new List<EyouSoft.Model.CompanyStructure.CompanyAffiche>();
            string tableName = "tbl_CompanyAffiche";
            string fields = "ID,OperatorName,CompanyID,AfficheClass,AfficheTitle,AfficheInfo,IsHot,IsTop,PicPath,IssueTime";
            string primaryKey = "ID";
            string orderByString = "IsTop DESC,IsHot DESC,IssueTime DESC";

            #region 生成查询条件
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(CompanyID))
            {
                strWhere.AppendFormat(" CompanyID='{0}' ", CompanyID);
            }
            if (afficheType != null)
            {
                int afficheClass = (int)afficheType;
                if (strWhere.Length > 0)
                    strWhere.AppendFormat(" AND AfficheClass={0} ", afficheClass);
                else
                    strWhere.AppendFormat(" AfficheClass={0} ", afficheClass);
            }
            if (IsHot != null)
            {
                string IsHotNum = IsHot.Value ? "1" : "0";
                if (strWhere.Length > 0)
                    strWhere.AppendFormat(" AND IsHot='{0}' ", IsHotNum);
                else
                    strWhere.AppendFormat(" IsHot='{0}' ", IsHotNum);
            }
            if (IsTop != null)
            {
                string IsTopNum = IsTop.Value ? "1" : "0";
                if (strWhere.Length > 0)
                    strWhere.AppendFormat(" AND IsTop='{0}' ", IsTopNum);
                else
                    strWhere.AppendFormat(" IsTop='{0}' ", IsTopNum);
            }
            if (IsPicNews != null)
            {
                string IsPicNewsNum = IsPicNews.Value ? "1" : "0";
                if (strWhere.Length > 0)
                    strWhere.AppendFormat(" AND IsPicNews='{0}' ", IsPicNewsNum);
                else
                    strWhere.AppendFormat(" IsPicNews='{0}' ", IsPicNewsNum);
            }
            #endregion

            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CompanyStructure.CompanyAffiche model = new EyouSoft.Model.CompanyStructure.CompanyAffiche();
                    model.ID = dr.GetInt32(0);
                    model.OperatorName = dr.GetString(1);
                    model.CompanyId = dr.GetString(2);
                    if (!dr.IsDBNull(3))
                        model.AfficheClass = (EyouSoft.Model.CompanyStructure.CompanyAfficheType)int.Parse(dr.GetByte(3).ToString());
                    model.AfficheTitle = dr.GetString(4);
                    model.AfficheInfo = dr.GetString(5);
                    model.IsHot = dr.GetString(6) == "1" ? true : false;
                    model.IsTop = dr.GetString(7) == "1" ? true : false;
                    model.PicPath = dr.GetString(8);
                    model.IssueTime = dr.GetDateTime(9);
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        #endregion
    }
}
