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

namespace EyouSoft.DAL.ToolStructure
{
    /// <summary>
    /// 描述：公司网络硬盘文件信息数据访问
    /// </summary>
    /// 创建人：蒋胜蓝 2010-05-13
    public class CompanyFileUpload : DALBase, IDAL.ToolStructure.ICompanyFileUpload
    {
        const string SQL_CompanyFileUpload_ADD = "INSERT INTO tbl_CompanyFileUpload([FileId],[CompanyId],[OperatorId],[FileName],[FileType],[FileIco],[FileSize],[FilePath],[ParentId]) VALUES(@FileId,@CompanyId,@OperatorId,@FileName,@FileType,@FileIco,@FileSize,@FilePath,@ParentId)";
        const string SQL_CompanyFileUpload_UPDATENAME = "UPDATE tbl_CompanyFileUpload SET [FileName] = @FileName WHERE FileId=@FileId";
        const string SQL_CompanyFileUpload_MOVEFILE = "UPDATE tbl_CompanyFileUpload SET [ParentId] = @ParentId WHERE FileId=@FileId";
        const string SQL_CompanyFileUpload_UPDATESIZE = "UPDATE tbl_CompanyFileUpload SET FileSize=(SELECT SUM(FileSize) FROM tbl_CompanyFileUpload WHERE ParentId=@ParentId) WHERE FileId=@ParentId";
        const string SQL_CompanyFileUpload_REMOVEFOLDER = "DELETE tbl_CompanyFileUpload WHERE FileId=@FileId OR ParentId=@FileId";
        const string SQL_CompanyFileUpload_REMOVEFILE = "DELETE tbl_CompanyFileUpload WHERE FileId=@FileId";
        const string SQL_CompanyFileUpload_SELECT = "SELECT FileId,FileName,FileType,FileIco,FileSize,FilePath,IssueTime FROM tbl_CompanyFileUpload";
        const string SQL_CompanyFileUpload_SUMSIZE = "SELECT SUM(FileSize) FROM tbl_CompanyFileUpload WHERE CompanyId=@CompanyId";

        #region ICompanyFileUpload 成员

        public virtual int Add(EyouSoft.Model.ToolStructure.CompanyFileUpload model)
        {
            DbCommand dc = this.SystemStore.GetSqlStringCommand(SQL_CompanyFileUpload_ADD);
            this.CompanyStore.AddInParameter(dc, "ID", DbType.String, Guid.NewGuid());
            this.CompanyStore.AddInParameter(dc, "CompanyId", DbType.String, model.CompanyId);
            this.CompanyStore.AddInParameter(dc, "OperatorId", DbType.String, model.OperatorId);
            this.CompanyStore.AddInParameter(dc, "FileName", DbType.String, model.FileName);
            this.CompanyStore.AddInParameter(dc, "FileType", DbType.Int16, model.FileType);
            this.CompanyStore.AddInParameter(dc, "FileIco", DbType.String, model.FileIco);
            this.CompanyStore.AddInParameter(dc, "FileSize", DbType.Int32, model.FileSize);
            this.CompanyStore.AddInParameter(dc, "FilePath", DbType.String, model.FilePath);
            this.CompanyStore.AddInParameter(dc, "ParentId", DbType.String, model.ParentId);
            return DbHelper.ExecuteSql(dc, this.CompanyStore);
        }

        public virtual int UpdateName(string FileId, string FileName)
        {
            DbCommand dc = this.SystemStore.GetSqlStringCommand(SQL_CompanyFileUpload_UPDATENAME);
            this.CompanyStore.AddInParameter(dc, "ID", DbType.String, Guid.NewGuid());
            this.CompanyStore.AddInParameter(dc, "FileName", DbType.String, FileName);
            return DbHelper.ExecuteSql(dc, this.CompanyStore);
        }

        public virtual int MoveFile(string FileId, string FolderId)
        {
            string strWhere = SQL_CompanyFileUpload_MOVEFILE + ";" + SQL_CompanyFileUpload_UPDATESIZE;
            DbCommand dc = this.SystemStore.GetSqlStringCommand(strWhere);
            this.CompanyStore.AddInParameter(dc, "FileId", DbType.String, FolderId);
            this.CompanyStore.AddInParameter(dc, "ParentId", DbType.String, FileId);            
            return DbHelper.ExecuteSql(dc, this.CompanyStore);
        }

        public virtual int RemoveFolder(string FileId)
        {
            DbCommand dc = this.SystemStore.GetSqlStringCommand(SQL_CompanyFileUpload_REMOVEFOLDER);
            this.CompanyStore.AddInParameter(dc, "FileId", DbType.String, FileId);
            return DbHelper.ExecuteSql(dc, this.CompanyStore);
        }

        public virtual int RemoveFile(string FileId, string FolderId)
        {
            string strWhere = SQL_CompanyFileUpload_REMOVEFILE + ";" + SQL_CompanyFileUpload_UPDATESIZE;
            DbCommand dc = this.SystemStore.GetSqlStringCommand(strWhere);
            this.CompanyStore.AddInParameter(dc, "FileId", DbType.String, FileId);
            this.CompanyStore.AddInParameter(dc, "ParentId", DbType.String, FolderId);
            return DbHelper.ExecuteSql(dc, this.CompanyStore);
        }

        public virtual int GetSumSize(string CompanyId)
        {
            DbCommand dc = this.SystemStore.GetSqlStringCommand(SQL_CompanyFileUpload_SUMSIZE);
            this.CompanyStore.AddInParameter(dc, "CompanyId", DbType.String, CompanyId);
            return Convert.ToInt32(DbHelper.GetSingle(dc, this.CompanyStore));
        }

        public virtual IList<EyouSoft.Model.ToolStructure.CompanyFileUpload> GetFolderList(string CompanyId)
        {
            string strWhere = SQL_CompanyFileUpload_SELECT + " where CompanyId=@CompanyId";
            DbCommand dc = this.SystemStore.GetSqlStringCommand(strWhere);
            this.CompanyStore.AddInParameter(dc, "CompanyId", DbType.String, CompanyId);
            return GetQueryList(dc);
        }

        public virtual IList<EyouSoft.Model.ToolStructure.CompanyFileUpload> GetFileList(string FolderId)
        {
            string strWhere = SQL_CompanyFileUpload_SELECT + " where ParentId=@ParentId";
            DbCommand dc = this.SystemStore.GetSqlStringCommand(strWhere);
            this.CompanyStore.AddInParameter(dc, "ParentId", DbType.String, FolderId);
            return GetQueryList(dc);
        }

        public virtual IList<EyouSoft.Model.ToolStructure.CompanyFileUpload> GetFileList(string CompanyId, string FolderId)
        {
            if (string.IsNullOrEmpty(FolderId))
            {
                string strWhere = SQL_CompanyFileUpload_SELECT + " where CompanyId=@CompanyId";
                DbCommand dc = this.SystemStore.GetSqlStringCommand(strWhere);
                this.CompanyStore.AddInParameter(dc, "CompanyId", DbType.String, CompanyId);
                return GetQueryList(dc);
            }
            else
            {
                return GetFileList(FolderId);
            }
        }

        /// <summary>
        /// 执行列表查询
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.ToolStructure.CompanyFileUpload> GetQueryList(DbCommand dc)
        {
            IList<EyouSoft.Model.ToolStructure.CompanyFileUpload> list = new List<EyouSoft.Model.ToolStructure.CompanyFileUpload>();
            using (IDataReader dr = this.SystemStore.ExecuteReader(dc))
            {
                EyouSoft.Model.ToolStructure.CompanyFileUpload model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.ToolStructure.CompanyFileUpload();
                    model.FileId = dr["ID"].ToString();
                    model.FileName = dr["FileName"].ToString();
                    if (dr["FileType"].ToString() != "")
                    {
                        model.FileType = (EyouSoft.Model.ToolStructure.CompanyFileType.FileType)int.Parse(dr["FileType"].ToString());
                    }
                    model.FileIco = dr["FileIco"].ToString();
                    if (dr["FileSize"].ToString() != "")
                    {
                        model.FileSize = int.Parse(dr["FileSize"].ToString());
                    }
                    model.FilePath = dr["FilePath"].ToString();
                    if (dr["IssueTime"].ToString() != "")
                    {
                        model.IssueTime = DateTime.Parse(dr["IssueTime"].ToString());
                    }
                    list.Add(model);
                }
            }
            return list;
        }

        #endregion
    }
}
