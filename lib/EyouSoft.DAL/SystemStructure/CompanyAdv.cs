using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL.SystemStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-20
    /// 描述：后台登陆页广告数据层
    /// </summary>
    public class CompanyAdv:DALBase,ICompanyAdv
    {
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database=null;
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public CompanyAdv() 
        {
            this._database = base.SystemStore;
        }
        #endregion

        #region SQL定义
        const string SQL_CompanyAdv_SELECT = "SELECT ID,AdvCompanyType,AdvType,AdvTitle,AdvContent,AdvLink,IsNewOpen,SortId,ImgPath,OperatorId,OperatorName,IssueTime from tbl_CompanyAdv ";
        const string SQL_CompanyAdv_ADD = "INSERT INTO tbl_CompanyAdv(AdvCompanyType,AdvType,AdvTitle,)";
        const string SQL_CompanyAdv_DELETE = "DELETE tbl_CompanyAdv where ID=@ID";
        const string SQL_CompanyAdv_SETSORT = "UPDATE tbl_CompanyAdv set SortId=@SortId where ID=@ID";
        const string SQL_CompanyAdv_UPDATE = "Update tbl_CompanyAdv set AdvTitle=@AdvTitle,AdvContent=@AdvContent,AdvLink=@AdvLink,IsNewOpen=@IsNewOpen,AdvType=@AdvType where ID=@ID ";
        const string SQL_CompanyAdv_SELECTTOPINFO = "SELECT top {0} ID,AdvTitle,AdvLink,IsNewOpen from tbl_CompanyAdv where AdvCompanyType={1} and AdvType=0 order by SortId desc,IssueTime desc";
        const string SQL_CompanyAdv_GETINDEXPICADV = "SELECT ID,ImgPath,AdvLink from tbl_CompanyAdv where AdvType=1 and AdvCompanyType={0}";
        const string SQL_CompanyAdv_ADDPICADV = "DELETE tbl_CompanyAdv where AdvCompanyType={0} and AdvType=1;INSERT INTO tbl_CompanyAdv(AdvCompanyType,AdvType,ImgPath,AdvLink) VALUES({0},1,'{1}','{2}')";
        #endregion

        #region 成员方法
        /// <summary>
        /// 运营后台分页获取列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyType">公司类型</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SystemStructure.CompanyAdv> GetList(int pageSize,int pageIndex,ref int recordCount,EyouSoft.Model.CompanyStructure.CompanyType companyType)
        {
            IList<EyouSoft.Model.SystemStructure.CompanyAdv> list = new List<EyouSoft.Model.SystemStructure.CompanyAdv>();
            EyouSoft.Model.SystemStructure.CompanyAdv model = null;
            string tableName = "tbl_CompanyAdv";
            string fields = "ID,AdvTitle,AdvLink,IssueTime,OperatorName,SortId";
            string orderByString = "SortId DESC,IssueTime DESC";
            string primaryKey = "ID";
            string strWhere = string.Format(" AdvCompanyType={0} and AdvType=0 ", (int)companyType);
            using (IDataReader dr = DbHelper.ExecuteReader(this._database,pageSize,pageIndex,ref recordCount,tableName,primaryKey,fields,strWhere,orderByString))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.CompanyAdv();
                    model.AdvLink = dr.GetString(dr.GetOrdinal("AdvLink"));
                    model.AdvTitle = dr.GetString(dr.GetOrdinal("AdvTitle"));
                    model.ID = dr.GetInt32(dr.GetOrdinal("ID"));
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.OperatorName = dr.GetString(dr.GetOrdinal("OperatorName"));
                    model.SortId = dr.GetInt32(dr.GetOrdinal("SortId"));
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 获取指定条数的后台登陆广告
        /// </summary>
        /// <param name="topNumber">要返回的记录数</param>
        /// <param name="companyType">公司类型</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SystemStructure.CompanyAdv> GetTopNumList(int topNumber, EyouSoft.Model.CompanyStructure.CompanyType companyType)
        {
            IList<EyouSoft.Model.SystemStructure.CompanyAdv> list = new List<EyouSoft.Model.SystemStructure.CompanyAdv>();
            DbCommand dc = null;
            if (topNumber > 0)
            {
                dc = this._database.GetSqlStringCommand(string.Format(SQL_CompanyAdv_SELECTTOPINFO, topNumber,(int)companyType));
            }
            else
            {
                dc = this._database.GetSqlStringCommand(SQL_CompanyAdv_SELECT + string.Format(" where AdvCompanyType={1} and AdvType=0 Order by sortId desc,issuetime desc",(int)companyType));
            }
            if (dc == null)
                return null;
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.SystemStructure.CompanyAdv model = new EyouSoft.Model.SystemStructure.CompanyAdv();
                    model.ID = dr.GetInt32(dr.GetOrdinal("ID"));
                    model.AdvTitle=dr.GetString(dr.GetOrdinal("AdvTitle"));
                    model.AdvLink=dr.GetString(dr.GetOrdinal("AdvLink"));
                    model.IsNewOpen=dr.GetString(dr.GetOrdinal("IsNewOpen"))=="1"?true:false;
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 获取指定公司类型的后台首页图片广告
        /// </summary>
        /// <param name="companyType">公司类型</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.SystemStructure.CompanyAdv GetIndexPicAdv(EyouSoft.Model.CompanyStructure.CompanyType companyType)
        {
            EyouSoft.Model.SystemStructure.CompanyAdv model = null;
            DbCommand dc = this._database.GetSqlStringCommand(string.Format(SQL_CompanyAdv_GETINDEXPICADV, (int)companyType));
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model.ID = dr.GetInt32(0);
                    model.ImgPath = dr.GetString(1);
                    model.AdvLink = dr.GetString(2);
                }
            }
            return model;
        }
        /// <summary>
        /// 添加文字广告
        /// </summary>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Add(EyouSoft.Model.SystemStructure.CompanyAdv model)
        {
            DbCommand dc = this._database.GetStoredProcCommand("proc_CompanyAdv_Insert");
            this._database.AddInParameter(dc, "AdvTitle", DbType.String, model.AdvTitle);
            this._database.AddInParameter(dc, "AdvContent", DbType.String, model.AdvContent);
            this._database.AddInParameter(dc, "AdvLink", DbType.String, model.AdvLink);
            this._database.AddInParameter(dc, "IsNewOpen", DbType.AnsiStringFixedLength, model.IsNewOpen ? "1" : "0");
            this._database.AddInParameter(dc, "AdvCompanyType", DbType.Int16, model.AdvCompanyType);
            this._database.AddInParameter(dc, "AdvType", DbType.Int16, model.AdvType);
            this._database.AddInParameter(dc, "OperatorID", DbType.Int32, model.OperatorId);
            this._database.AddInParameter(dc, "OperatorName", DbType.String, model.OperatorName);
            this._database.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);
            this._database.AddInParameter(dc, "InternalLink", DbType.String, model.InternalLink);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 添加图片广告
        /// </summary>
        /// <param name="companyType">公司类型</param>
        /// <param name="ImgPath">图片路径</param>
        /// <param name="AdvLink">链接地址</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool AddPicAdv(EyouSoft.Model.CompanyStructure.CompanyType companyType, string ImgPath, string AdvLink)
        {
            string strSql = string.Format(SQL_CompanyAdv_ADDPICADV, (int)companyType, ImgPath, AdvLink);
            DbCommand  dc = this._database.GetSqlStringCommand(strSql);
            return DbHelper.ExecuteSqlTrans(dc, this._database)>0?true:false;
        }
       /// <summary>
        /// 修改
       /// </summary>
       /// <param name="model">广告实体类</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Update(EyouSoft.Model.SystemStructure.CompanyAdv model)
        {

            DbCommand dc = this._database.GetStoredProcCommand(SQL_CompanyAdv_UPDATE);
            this._database.AddInParameter(dc, "ID", DbType.Int32, model.ID);
            this._database.AddInParameter(dc, "AdvTitle", DbType.String, model.AdvTitle);
            this._database.AddInParameter(dc, "AdvContent", DbType.String, model.AdvContent);
            this._database.AddInParameter(dc, "AdvLink", DbType.String, string.IsNullOrEmpty(model.AdvLink)?model.InternalLink + model.ID.ToString():model.AdvLink);
            this._database.AddInParameter(dc, "IsNewOpen", DbType.AnsiStringFixedLength, model.IsNewOpen ? "1" : "0");
            this._database.AddInParameter(dc, "AdvType", DbType.Int16, model.AdvType);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Delete(int id)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyAdv_DELETE);
            this._database.AddInParameter(dc, "ID", DbType.Int32, id);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 设置排序
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="sortnumber">排序值</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool SetSort(int id, int sortnumber)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyAdv_SETSORT);
            this._database.AddInParameter(dc, "ID", DbType.Int32, id);
            this._database.AddInParameter(dc, "SortId", DbType.Int32, sortnumber);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        #endregion
    }
}
