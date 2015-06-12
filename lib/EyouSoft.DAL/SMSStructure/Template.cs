using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.Model;
using EyouSoft.IDAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.SMSStructure
{
    /// <summary>
    /// 短信中心-常用短语及常用短语类型数据访问类
    /// </summary>
    /// Author:汪奇志 2010-06-10
    public class Template:DALBase,EyouSoft.IDAL.SMSStructure.ITemplate
    {
        #region static constants
        //static constants
        private const string SQL_INSERT_INSERTCATEGORY = "INSERT INTO [SMS_CommonWordClass]([CompanyID] ,[UserID] ,[ClassName]) VALUES(@COMPANYID,@USERID,@CLASSNAME);SELECT @@IDENTITY";
        private const string SQL_SELECT_GETCATEGORYS = "SELECT [ID] ,[CompanyID] ,[UserID] ,[ClassName] ,[IssueTime]  FROM [SMS_CommonWordClass] WHERE [CompanyID]=@COMPANYID";
        private const string SQL_INSERT_INSERTTEMPLATE = "INSERT INTO [SMS_CommonWords] ([ID],[CompanyID] ,[UserID] ,[ClassID] ,[WordContent]) VALUES(@ID,@COMPANYID,@USERID,@CLASSID,@WORDCONTENT)";
        private const string SQL_DELETE_DELETECATEGORY = "DELETE FROM [SMS_CommonWordClass] WHERE [Id]=@CategoryId";
        private const string SQL_DELETE_DELETETEMPLATE = "DELETE FROM [SMS_CommonWords] WHERE [Id]=@TEMPLATEID";
        private const string SQL_SELECT_GETTEMPLATEINFO = "SELECT [ID] ,[CompanyID] ,[UserID] ,[ClassID] ,[WordContent] ,[IssueTime]  FROM [SMS_CommonWords] WHERE [ID]=@TEMPLATEID";
        private const string SQL_UPDATE_UPDATETEMPLATE = "UPDATE [SMS_CommonWords] SET [ClassID]=@CATEGORYID,[WordContent]=@CONTENT WHERE [Id]=@TEMPLATEID";
        #endregion static constants

        #region 成员方法
        /// <summary>
        /// 插入常用短语类型信息,返回0插入失败,>0时为插入的类型编号
        /// </summary>
        /// <param name="categoryInfo">常用短语类型业务实体</param>
        /// <returns></returns>
        public virtual int InsertCategory(EyouSoft.Model.SMSStructure.TemplateCategoryInfo categoryInfo)
        {
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_INSERT_INSERTCATEGORY);
            base.SMSStore.AddInParameter(cmd, "COMPANYID", DbType.String, categoryInfo.CompanyId);
            base.SMSStore.AddInParameter(cmd, "USERID", DbType.String, categoryInfo.UserId);
            base.SMSStore.AddInParameter(cmd, "CLASSNAME", DbType.String, categoryInfo.CategoryName);

            object obj = DbHelper.GetSingle(cmd, base.SMSStore);

            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }



        /// <summary>
        /// 删除常用短语类型信息
        /// </summary>
        /// <param name="CategoryId">类型编号</param>
        /// <returns></returns>
        public virtual bool DeleteCategory(int CategoryId)
        {
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_DELETE_DELETECATEGORY);

            base.SMSStore.AddInParameter(cmd, "CategoryId", DbType.Int32, CategoryId);

            return DbHelper.ExecuteSql(cmd, base.SMSStore) > 0 ? true : false;
        }

        /// <summary>
        /// 根据指定的公司编号获取公司的所有常用短语类型信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SMSStructure.TemplateCategoryInfo> GetCategorys(string companyId)
        {
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_SELECT_GETCATEGORYS);
            base.SMSStore.AddInParameter(cmd, "COMPANYID", DbType.AnsiStringFixedLength, companyId);

            IList<EyouSoft.Model.SMSStructure.TemplateCategoryInfo> categorys = new List<EyouSoft.Model.SMSStructure.TemplateCategoryInfo>();

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.SMSStore))
            {
                while (rdr.Read())
                {
                    categorys.Add(new EyouSoft.Model.SMSStructure.TemplateCategoryInfo(rdr.GetInt32(rdr.GetOrdinal("Id"))
                        , rdr["ClassName"].ToString()
                        , rdr.GetString(rdr.GetOrdinal("CompanyID"))
                        , rdr.GetString(rdr.GetOrdinal("UserID"))
                        , rdr.GetDateTime(rdr.GetOrdinal("IssueTime"))));
                }
            }

            return categorys;
        }

        /// <summary>
        /// 插入常用短语
        /// </summary>
        /// <param name="templateInfo">常用短语业务实体</param>
        /// <returns></returns>
        public virtual bool InsertTemplate(EyouSoft.Model.SMSStructure.TemplateInfo templateInfo)
        {
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_INSERT_INSERTTEMPLATE);

            base.SMSStore.AddInParameter(cmd, "ID", DbType.AnsiStringFixedLength, templateInfo.TemplateId);
            base.SMSStore.AddInParameter(cmd, "COMPANYID", DbType.String, templateInfo.CompanyId);
            base.SMSStore.AddInParameter(cmd, "USERID", DbType.String, templateInfo.UserId);
            base.SMSStore.AddInParameter(cmd, "CLASSID", DbType.String, templateInfo.CategoryId);
            base.SMSStore.AddInParameter(cmd, "WORDCONTENT", DbType.String, templateInfo.Content);

            return DbHelper.ExecuteSql(cmd,base.SMSStore) == 1 ? true : false;
        }

        /// <summary>
        /// 根据指定条件获取常用短语信息
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="keyword">关键字 为空时不做为查询条件</param>
        /// <param name="categoryId">类型编号 -1时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SMSStructure.TemplateInfo> GetTemplates(int pageSize, int pageIndex, ref int recordCount, string companyId, string keyword, int categoryId)
        {
            IList<EyouSoft.Model.SMSStructure.TemplateInfo> templates = new List<EyouSoft.Model.SMSStructure.TemplateInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "view_SMS_Template";
            string primaryKey = "Id";
            string orderByString = "IssueTime DESC";
            string fields = "ID, CompanyID, UserID, ClassID, WordContent, IssueTime, ClassName";

            cmdQuery.AppendFormat(" CompanyID='{0}' ", companyId);

            if (!string.IsNullOrEmpty(keyword))
            {
                cmdQuery.AppendFormat(" AND (WordContent LIKE '%{0}%')", keyword);
            }

            if (categoryId != -1)
            {
                cmdQuery.AppendFormat(" AND ClassID={0}", categoryId.ToString());
            }

            using (IDataReader rdr = DbHelper.ExecuteReader(base.SMSStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    templates.Add(new EyouSoft.Model.SMSStructure.TemplateInfo(rdr.GetString(rdr.GetOrdinal("ID"))
                        , rdr.GetString(rdr.GetOrdinal("CompanyID"))
                        , rdr.GetString(rdr.GetOrdinal("UserID"))
                        , rdr.GetInt32(rdr.GetOrdinal("ClassID"))
                        , rdr["ClassName"].ToString()
                        , rdr["WordContent"].ToString()
                        , rdr.GetDateTime(rdr.GetOrdinal("IssueTime"))));
                }
            }

            return templates;
        }

        /// <summary>
        /// 删除常用短语
        /// </summary>
        /// <param name="templateId">常用短语编号</param>
        /// <returns></returns>
        public virtual bool DeleteTemplate(string templateId)
        {
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_DELETE_DELETETEMPLATE);
            base.SMSStore.AddInParameter(cmd, "TEMPLATEID", DbType.AnsiStringFixedLength, templateId);

            return DbHelper.ExecuteSql(cmd, base.SMSStore) > 0 ? true : false;
        }

        /// <summary>
        /// 获取常用短语信息
        /// </summary>
        /// <param name="templateId">常用短语编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.SMSStructure.TemplateInfo GetTemplateInfo(string templateId)
        {
            EyouSoft.Model.SMSStructure.TemplateInfo templateInfo = null;
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_SELECT_GETTEMPLATEINFO);
            base.SMSStore.AddInParameter(cmd, "TEMPLATEID", DbType.AnsiStringFixedLength, templateId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.SMSStore))
            {
                if (rdr.Read())
                {
                    templateInfo = new EyouSoft.Model.SMSStructure.TemplateInfo(rdr.GetString(rdr.GetOrdinal("ID"))
                        , rdr.GetString(rdr.GetOrdinal("CompanyID"))
                        , rdr.GetString(rdr.GetOrdinal("UserID"))
                        , rdr.GetInt32(rdr.GetOrdinal("ClassID"))
                        , string.Empty
                        , rdr["WordContent"].ToString()
                        , rdr.GetDateTime(rdr.GetOrdinal("IssueTime")));
                }
            }

            return templateInfo;
        }

        /// <summary>
        /// 更新常用短语
        /// </summary>
        /// <param name="templateInfo">常用短语业务实体</param>
        /// <returns></returns>
        public virtual bool UpdateTemplate(EyouSoft.Model.SMSStructure.TemplateInfo templateInfo)
        {
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_UPDATE_UPDATETEMPLATE);
            base.SMSStore.AddInParameter(cmd, "CATEGORYID", DbType.Int32, templateInfo.CategoryId);
            base.SMSStore.AddInParameter(cmd, "CONTENT", DbType.String, templateInfo.Content);
            base.SMSStore.AddInParameter(cmd, "TEMPLATEID", DbType.String, templateInfo.TemplateId);

            return DbHelper.ExecuteSql(cmd, base.SMSStore) > 0 ? true : false;
        }
        #endregion
    }
}
