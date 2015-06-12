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

namespace EyouSoft.DAL.AdvStructure
{
    /// <summary>
    /// 广告数据访问类
    /// </summary>
    /// Author:汪奇志 2010-07-13
    /// 修改：华磊 2011-05-13 添加表tbl_SysAdvImg中的缩略图字段
    public class Adv:DALBase,EyouSoft.IDAL.AdvStructure.IAdv
    {
        #region static constants
        //static constants
        private const string SQL_DELETE_DeleteAdv = "DELETE FROM tbl_SysAdv WHERE Id=@AdvId";
        private const string SQL_UPDATE_SetAdvSort = "UPDATE tbl_SysAdvAreaControl SET SortId=@SortId WHERE AdvId=@AdvId AND AreaId=@RelationId AND AreaType=@AdvType";
        private const string SQL_SELECT_GetAdvRelation = "SELECT AreaId FROM tbl_SysAdvAreaControl WHERE AdvId=@AdvId";
        private const string SQL_SELECT_GetAdvInfo = "SELECT A.*,B.DisplayType,C.AdvImg,C.AdvThumbnail FROM tbl_SysAdv AS A INNER JOIN tbl_SysAdvArea AS B ON A.AreaId=B.AreaId LEFT OUTER JOIN tbl_SysAdvImg AS C ON A.Id=C.AdvId WHERE A.Id=@AdvId";
        private const string SQL_SELECT_GetPositionInfo = "SELECT * FROM tbl_SysAdvArea WHERE AreaId=@PositionId";
        private const string SQL_SELECT_GetPositions = "SELECT * FROM tbl_SysAdvArea WHERE CatalogId=@Catalog AND IsDisabled='0'";
        private const string SQL_UPDATE_SetAdvLink = "UPDATE tbl_SysAdv SET AdvLink=@AdvLink WHERE CompanyId=@CompanyId AND AreaId IN (SELECT ID FROM tbl_SysAdvArea WHERE DisplayType IN (5,6,7))";
        /// <summary>
        /// 删除前移动文件记录
        /// </summary>
        private const string SQL_DELETEDFILE_UPDATEMOVE = "IF (SELECT COUNT(*) FROM [tbl_SysAdvImg] WHERE AdvId=@AdvId AND [AdvImg]=@AdvImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [AdvImg] FROM [tbl_SysAdvImg] WHERE [AdvId]=@AdvId AND AdvImg<>''";
        private const string SQL_DELETEDFILE_DELETEMOVE = "INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [AdvImg] FROM [tbl_SysAdvImg] WHERE [AdvId]=@AdvId AND AdvImg<>'';";
        #endregion

        #region protected member
        /// <summary>
        /// 创建广告关系XMLDocument 
        /// </summary>
        /// <param name="relations">关系集合</param>
        /// <returns>System.String</returns>
        protected string CreateRelationXML(IList<int> relations)
        {
            StringBuilder xmlDoc = new StringBuilder();

            xmlDoc.Append("<ROOT>");

            if (relations != null && relations.Count > 0)
            {
                foreach (int tmp in relations)
                {
                    xmlDoc.AppendFormat("<RelationInfo RelationId=\"{0}\" />", tmp.ToString());
                }
            }

            xmlDoc.Append("</ROOT>");

            return xmlDoc.ToString();
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 发布广告
        /// </summary>
        /// <param name="info">广告信息业务实体</param>
        /// <returns>1:成功 0:失败</returns>
        public virtual int InsertAdv(EyouSoft.Model.AdvStructure.AdvInfo info)
        {
            DbCommand cmd = base.SystemStore.GetStoredProcCommand("proc_SysAdv_InsertAdv");

            base.SystemStore.AddInParameter(cmd, "PositionId", DbType.Int32, info.Position);
            base.SystemStore.AddInParameter(cmd, "CategoryId", DbType.Int32, info.Category);
            base.SystemStore.AddInParameter(cmd, "Title", DbType.String, info.Title);
            base.SystemStore.AddInParameter(cmd, "Remark", DbType.String, info.Remark);
            base.SystemStore.AddInParameter(cmd, "RedirectURL", DbType.String, info.RedirectURL);
            base.SystemStore.AddInParameter(cmd, "ImgPath", DbType.String, info.ImgPath);
            base.SystemStore.AddInParameter(cmd, "AdvThumbnail", DbType.String, info.AdvThumb);
            base.SystemStore.AddInParameter(cmd, "CompanyId", DbType.String, info.CompanyId);
            base.SystemStore.AddInParameter(cmd, "CompanyName", DbType.String, info.CompanyName);
            base.SystemStore.AddInParameter(cmd, "ContactInfo", DbType.String, info.ContactInfo);
            base.SystemStore.AddInParameter(cmd, "StartDate", DbType.DateTime, info.StartDate);
            base.SystemStore.AddInParameter(cmd, "EndDate", DbType.DateTime, info.EndDate);
            base.SystemStore.AddInParameter(cmd, "OperatorId", DbType.Int32, info.OperatorId);
            base.SystemStore.AddInParameter(cmd, "OperatorName", DbType.String, info.OperatorName);
            base.SystemStore.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            base.SystemStore.AddInParameter(cmd, "Range", DbType.Int32, info.Range);
            base.SystemStore.AddInParameter(cmd, "Relation", DbType.String, this.CreateRelationXML(info.Relation));
            base.SystemStore.AddOutParameter(cmd, "Result", DbType.Int32,4);
            base.SystemStore.AddOutParameter(cmd, "AdvId", DbType.Int32, 4);

            DbHelper.ExecuteSql(cmd, base.SystemStore);

            int result = Convert.ToInt32(base.SystemStore.GetParameterValue(cmd, "Result"));

            if (result==1)
            {
                info.AdvId = Convert.ToInt32(base.SystemStore.GetParameterValue(cmd, "AdvId"));
            }

            return result;
        }

        /// <summary>
        /// 更新广告
        /// </summary>
        /// <param name="info">广告信息业务实体</param>
        /// <returns>1:成功 0:失败</returns>
        public virtual int UpdateAdv(EyouSoft.Model.AdvStructure.AdvInfo info)
        {
            DbCommand cmd = base.SystemStore.GetSqlStringCommand(SQL_DELETEDFILE_UPDATEMOVE);

            EyouSoft.Model.AdvStructure.AdvDisplayType DisplayType = GetPositionInfo(info.Position).DisplayType;
            if (DisplayType != EyouSoft.Model.AdvStructure.AdvDisplayType.单位图片广告
                && DisplayType != EyouSoft.Model.AdvStructure.AdvDisplayType.单位图文广告
                && DisplayType != EyouSoft.Model.AdvStructure.AdvDisplayType.单位文字广告)
            {
                base.SystemStore.AddInParameter(cmd, "AdvId", DbType.Int32, info.AdvId);
                base.SystemStore.AddInParameter(cmd, "AdvImg", DbType.String, info.ImgPath);
                DbHelper.ExecuteSql(cmd, base.SystemStore);
            }
            
            cmd = base.SystemStore.GetStoredProcCommand("proc_SysAdv_UpdateAdv");

            base.SystemStore.AddInParameter(cmd, "AdvId", DbType.Int32, info.AdvId);
            base.SystemStore.AddInParameter(cmd, "PositionId", DbType.Int32, info.Position);
            base.SystemStore.AddInParameter(cmd, "CategoryId", DbType.Int32, info.Category);
            base.SystemStore.AddInParameter(cmd, "Title", DbType.String, info.Title);
            base.SystemStore.AddInParameter(cmd, "Remark", DbType.String, info.Remark);
            base.SystemStore.AddInParameter(cmd, "RedirectURL", DbType.String, info.RedirectURL);
            base.SystemStore.AddInParameter(cmd, "ImgPath", DbType.String, info.ImgPath);
            base.SystemStore.AddInParameter(cmd, "AdvThumbnail", DbType.String, info.AdvThumb);
            base.SystemStore.AddInParameter(cmd, "CompanyId", DbType.String, info.CompanyId);
            base.SystemStore.AddInParameter(cmd, "CompanyName", DbType.String, info.CompanyName);
            base.SystemStore.AddInParameter(cmd, "ContactInfo", DbType.String, info.ContactInfo);
            base.SystemStore.AddInParameter(cmd, "StartDate", DbType.DateTime, info.StartDate);
            base.SystemStore.AddInParameter(cmd, "EndDate", DbType.DateTime, info.EndDate);
            base.SystemStore.AddInParameter(cmd, "OperatorId", DbType.Int32, info.OperatorId);
            base.SystemStore.AddInParameter(cmd, "OperatorName", DbType.String, info.OperatorName);
            base.SystemStore.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            base.SystemStore.AddInParameter(cmd, "Range", DbType.Int32, info.Range);
            base.SystemStore.AddInParameter(cmd, "Relation", DbType.String, this.CreateRelationXML(info.Relation));
            base.SystemStore.AddOutParameter(cmd, "Result", DbType.Int32, 4);

            DbHelper.ExecuteSql(cmd, base.SystemStore);

            return Convert.ToInt32(base.SystemStore.GetParameterValue(cmd, "Result"));
        }

        /// <summary>
        /// 更新单位广告链接
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="AdvLink">广告链接</param>
        /// <returns>1:成功 0:失败</returns>
        public int UpdateAdv(string CompanyId, string AdvLink)
        {
            DbCommand cmd = base.SystemStore.GetSqlStringCommand(SQL_UPDATE_SetAdvLink);

            base.SystemStore.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            base.SystemStore.AddInParameter(cmd, "AdvLink", DbType.String, AdvLink);

            return DbHelper.ExecuteSql(cmd, base.SystemStore) > 0 ? 1 : 0;
        }
        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="advId">广告编号</param>
        /// <returns></returns>
        public virtual bool DeleteAdv(int advId)
        {
            EyouSoft.Model.AdvStructure.AdvInfo adv = GetAdvInfo(advId);
            if (adv != null)
            {
                DbCommand cmd = base.SystemStore.GetSqlStringCommand("A");

                EyouSoft.Model.AdvStructure.AdvDisplayType DisplayType = GetPositionInfo(adv.Position).DisplayType;
                if (DisplayType != EyouSoft.Model.AdvStructure.AdvDisplayType.单位图片广告
                    && DisplayType != EyouSoft.Model.AdvStructure.AdvDisplayType.单位图文广告
                    && DisplayType != EyouSoft.Model.AdvStructure.AdvDisplayType.单位文字广告)
                {
                    cmd = base.SystemStore.GetSqlStringCommand(SQL_DELETEDFILE_DELETEMOVE + SQL_DELETE_DeleteAdv);
                }
                else
                {
                    cmd = base.SystemStore.GetSqlStringCommand(SQL_DELETE_DeleteAdv);
                }

                base.SystemStore.AddInParameter(cmd, "AdvId", DbType.Int32, advId);

                return DbHelper.ExecuteSql(cmd, base.SystemStore) > 0 ? true : false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否有效
        /// </summary>
        /// <param name="info">广告信息业务实体</param>
        /// <returns></returns>
        public virtual bool IsValid(EyouSoft.Model.AdvStructure.AdvInfo info)
        {
            DbCommand cmd = base.SystemStore.GetStoredProcCommand("proc_SysAdv_IsValid");

            base.SystemStore.AddInParameter(cmd, "AdvId", DbType.Int32, info.AdvId);
            base.SystemStore.AddInParameter(cmd, "PositionId", DbType.Int32, info.Position);
            base.SystemStore.AddInParameter(cmd, "StartDate", DbType.DateTime, info.StartDate);
            base.SystemStore.AddInParameter(cmd, "EndDate", DbType.DateTime, info.EndDate);
            base.SystemStore.AddInParameter(cmd, "Range", DbType.Int32, info.Range);
            base.SystemStore.AddInParameter(cmd, "Relation", DbType.String, this.CreateRelationXML(info.Relation));
            base.SystemStore.AddOutParameter(cmd, "Result", DbType.Int32, 4);

            DbHelper.ExecuteSql(cmd, base.SystemStore);

            return Convert.ToInt32(base.SystemStore.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 获取广告信息
        /// </summary>
        /// <param name="advId">广告编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.AdvStructure.AdvInfo GetAdvInfo(int advId)
        {
            EyouSoft.Model.AdvStructure.AdvInfo info = null;
            DbCommand cmd = base.SystemStore.GetSqlStringCommand(SQL_SELECT_GetAdvInfo);
            base.SystemStore.AddInParameter(cmd, "AdvId", DbType.Int32, advId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.SystemStore))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.AdvStructure.AdvInfo();

                    info.AdvId = advId;
                    EyouSoft.Model.AdvStructure.AdvDisplayType displayType = (EyouSoft.Model.AdvStructure.AdvDisplayType)rdr.GetByte(rdr.GetOrdinal("DisplayType"));
                    info.AdvType = displayType == EyouSoft.Model.AdvStructure.AdvDisplayType.MQ广告 ? EyouSoft.Model.AdvStructure.AdvType.MQ : EyouSoft.Model.AdvStructure.AdvType.城市;
                    info.Category = (EyouSoft.Model.AdvStructure.AdvCategory)rdr.GetByte(rdr.GetOrdinal("ClassId"));
                    info.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    info.CompanyName = rdr["CompanyName"].ToString();
                    info.ContactInfo = rdr["ContactInfo"].ToString();
                    info.EndDate = rdr.GetDateTime(rdr.GetOrdinal("EndDate"));
                    info.ImgPath = rdr["AdvImg"].ToString();
                    info.AdvThumb = rdr["AdvThumbnail"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.OperatorId = rdr.GetInt32(rdr.GetOrdinal("OperatorId"));
                    info.OperatorName = rdr["OperatorName"].ToString();
                    info.Position = (EyouSoft.Model.AdvStructure.AdvPosition)rdr.GetInt32(rdr.GetOrdinal("AreaId"));
                    info.Range = (EyouSoft.Model.AdvStructure.AdvRange)rdr.GetByte(rdr.GetOrdinal("AdvArea"));
                    info.RedirectURL = rdr["AdvLink"].ToString();
                    //info.Relation=
                    info.Remark = rdr["AdvRemark"].ToString();
                    info.SortId = 0;
                    info.StartDate = rdr.GetDateTime(rdr.GetOrdinal("StartDate"));
                    info.Title = rdr["AdvDescript"].ToString();
                }
            }

            /*if (info != null)
            {
                info.Relation = this.GetAdvRelation(info.AdvId);
            }*/

            return info;
        }

        /// <summary>
        /// 设置广告排序
        /// </summary>
        /// <param name="advId">广告编号</param>
        /// <param name="advType">广告投放类型</param>
        /// <param name="relationId">关联编号</param>
        /// <param name="sortId">排序编号</param>
        /// <returns></returns>
        public virtual bool SetAdvSort(int advId, EyouSoft.Model.AdvStructure.AdvType advType, int relationId, int sortId)
        {
            DbCommand cmd = base.SystemStore.GetSqlStringCommand(SQL_UPDATE_SetAdvSort);

            base.SystemStore.AddInParameter(cmd, "SortId", DbType.Int32, sortId);
            base.SystemStore.AddInParameter(cmd, "AdvId", DbType.Int32, advId);
            base.SystemStore.AddInParameter(cmd, "RelationId", DbType.Int32, relationId);
            base.SystemStore.AddInParameter(cmd, "AdvType", DbType.Byte, advType);

            return DbHelper.ExecuteSql(cmd, base.SystemStore) == 1 ? true : false;
        }

        /// <summary>
        /// 获取广告关系集合
        /// </summary>
        /// <param name="advId">广告编号</param>
        /// <returns></returns>
        public virtual IList<int> GetAdvRelation(int advId)
        {
            IList<int> relations = new List<int>();
            DbCommand cmd = base.SystemStore.GetSqlStringCommand(SQL_SELECT_GetAdvRelation);
            base.SystemStore.AddInParameter(cmd, "AdvId", DbType.Int32, advId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.SystemStore))
            {
                while (rdr.Read())
                {
                    relations.Add(rdr.GetInt32(0));
                }
            }

            return relations;
        }

        /// <summary>
        /// 获取广告位置信息
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public virtual EyouSoft.Model.AdvStructure.AdvPositionInfo GetPositionInfo(EyouSoft.Model.AdvStructure.AdvPosition position)
        {
            EyouSoft.Model.AdvStructure.AdvPositionInfo info = null;
            DbCommand cmd = base.SystemStore.GetSqlStringCommand(SQL_SELECT_GetPositionInfo);
            base.SystemStore.AddInParameter(cmd, "PositionId", DbType.Int32, position);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.SystemStore))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.AdvStructure.AdvPositionInfo();

                    info.AdvCount = rdr.GetInt32(rdr.GetOrdinal("AdvCount"));
                    info.Catalog = (EyouSoft.Model.AdvStructure.AdvCatalog)rdr.GetInt32(rdr.GetOrdinal("CatalogId"));
                    info.DisplayType = (EyouSoft.Model.AdvStructure.AdvDisplayType)rdr.GetByte(rdr.GetOrdinal("DisplayType"));
                    info.Position = position;
                }
            }

            return info;
        }

        /// <summary>
        /// 按照指定条件获取广告信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="position">广告位置 为空时不做为查询条件</param>
        /// <param name="advType">广告投放类型</param>
        /// <param name="relationId">关联编号(城市或单位类型编号)</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="category">广告类别 为null时不做为查询条件</param>
        /// <param name="startDate">有效期起始时间 为null时不做为查询条件</param>
        /// <param name="endDate">有效期截止时间 为null时不做为查询条件</param>
        /// <param name="catalog">广告栏目(频道)</param>
        /// <param name="title">广告标题 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.AdvStructure.AdvInfo> GetAdvs(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.AdvStructure.AdvPosition? position
            , EyouSoft.Model.AdvStructure.AdvType advType, int relationId
            , string companyName, EyouSoft.Model.AdvStructure.AdvCategory? category, DateTime? startDate, DateTime? endDate
            , EyouSoft.Model.AdvStructure.AdvCatalog? catalog,string title)
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> advs = new List<EyouSoft.Model.AdvStructure.AdvInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "tbl_SysAdv";
            string primaryKey = "Id";
            string orderByString = "IssueTime ASC";
            StringBuilder fields =new StringBuilder();
            fields.Append("*");
            fields.Append(",(SELECT AdvImg FROM tbl_SysAdvImg WHERE AdvId=tbl_SysAdv.Id) AS AdvImg");
            fields.Append(",(SELECT AdvThumbnail FROM tbl_SysAdvImg WHERE AdvId=tbl_SysAdv.Id) AS AdvThumb");
            fields.AppendFormat(",(SELECT SortId FROM tbl_SysAdvAreaControl WHERE AdvId=tbl_SysAdv.Id AND AreaType={0} AND AreaId={1}) AS SortId ", (int)advType, relationId);
            fields.Append(",(SELECT MQ FROM tbl_CompanyUser WHERE CompanyId=tbl_SysAdv.CompanyId AND IsAdmin='1' ) AS ContactMQ");

            #region 拼接查询条件
            cmdQuery.Append(" 1=1 ");
            if(relationId > 0)
                cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_SysAdvAreaControl WHERE AdvId=tbl_SysAdv.Id AND AreaType={0} AND AreaId={1}) ", (int)advType, relationId);

            if (position.HasValue)
            {
                cmdQuery.AppendFormat(" AND AreaId={0} ", (int)position.Value);
            }
            
            if (!string.IsNullOrEmpty(companyName))
            {
                cmdQuery.AppendFormat(" AND CompanyName LIKE '%{0}%' ", companyName);
            }

            if (category.HasValue)
            {
                cmdQuery.AppendFormat(" AND ClassId={0} ", (int)category.Value);
            }

            if (startDate.HasValue||endDate.HasValue)
            {
                /*cmdQuery.Append(" AND(1=0 ");
                if (startDate.HasValue)
                {
                    cmdQuery.AppendFormat(" OR '{0}' BETWEEN StartDate AND EndDate ", startDate.Value);
                }
                if (endDate.HasValue)
                {
                    cmdQuery.AppendFormat(" OR '{0}' BETWEEN StartDate AND EndDate ", endDate.Value);
                }
                cmdQuery.Append(")");*/

                cmdQuery.Append(" AND (EndDate>='9999-12-31' ");

                cmdQuery.Append(" OR (1=1 ");
                if (startDate.HasValue)
                {
                    cmdQuery.AppendFormat(" AND StartDate>='{0}' ", startDate.Value);
                }
                if (endDate.HasValue)
                {
                    cmdQuery.AppendFormat(" AND EndDate<='{0}' ", endDate.Value);
                }
                cmdQuery.Append(" ) ");

                cmdQuery.Append(" ) ");   
            }
            

            if (catalog.HasValue)
            {
                cmdQuery.AppendFormat(" AND AreaId IN(SELECT AreaId FROM tbl_SysAdvArea WHERE CatalogId={0}) ", (int)catalog.Value);
            }

            if (!string.IsNullOrEmpty(title))
            {
                cmdQuery.AppendFormat(" AND AdvDescript LIKE '%{0}%' ", title);
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(base.SystemStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields.ToString(), cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.AdvStructure.AdvInfo tmp = new EyouSoft.Model.AdvStructure.AdvInfo();
                    tmp.AdvId = rdr.GetInt32(rdr.GetOrdinal("Id"));
                    tmp.AdvType = advType;
                    tmp.Category = (EyouSoft.Model.AdvStructure.AdvCategory)rdr.GetByte(rdr.GetOrdinal("ClassId"));
                    tmp.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    tmp.CompanyName = rdr["CompanyName"].ToString();
                    tmp.ContactInfo = rdr["ContactInfo"].ToString();
                    tmp.EndDate = rdr.GetDateTime(rdr.GetOrdinal("EndDate"));
                    tmp.ImgPath = rdr["AdvImg"].ToString();
                    tmp.AdvThumb = rdr["AdvThumb"].ToString();
                    tmp.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    tmp.OperatorId = rdr.GetInt32(rdr.GetOrdinal("OperatorId"));
                    tmp.OperatorName = rdr["OperatorName"].ToString();
                    tmp.Position = (EyouSoft.Model.AdvStructure.AdvPosition)rdr.GetInt32(rdr.GetOrdinal("AreaId"));
                    tmp.Range = (EyouSoft.Model.AdvStructure.AdvRange)rdr.GetByte(rdr.GetOrdinal("AdvArea"));
                    tmp.RedirectURL = rdr["AdvLink"].ToString();
                    //tmp.Relation
                    tmp.Remark = rdr.IsDBNull(rdr.GetOrdinal("AdvRemark"))
                                     ? string.Empty
                                     : rdr.GetString(rdr.GetOrdinal("AdvRemark"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("SortId"))) tmp.SortId = rdr.GetInt32(rdr.GetOrdinal("SortId"));
                    tmp.StartDate = rdr.GetDateTime(rdr.GetOrdinal("StartDate"));
                    tmp.Title = rdr["AdvDescript"].ToString();
                    tmp.ContactMQ = rdr["ContactMQ"].ToString();

                    advs.Add(tmp);
                }
            }

            return advs;
        }

        /// <summary>
        /// 获取指定位置的广告信息集合
        /// </summary>
        /// <param name="advType">广告投放类型</param>
        /// <param name="relationId">关联编号(城市或单位类型编号)</param>
        /// <param name="position">广告位置</param>
        /// <param name="date">日期</param>
        /// <param name="expression">指定返回行数的数值表达式</param>
        /// <param name="isPlatformAd">是否取广告类别为同业114广告</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.AdvStructure.AdvInfo> GetAdvs(EyouSoft.Model.AdvStructure.AdvType advType, int relationId, EyouSoft.Model.AdvStructure.AdvPosition position, DateTime date, int expression, bool isPlatformAd)
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> advs = new List<EyouSoft.Model.AdvStructure.AdvInfo>();

            StringBuilder cmdText = new StringBuilder();
            cmdText.Append(" SELECT TOP (@Expression) A.Id,A.AdvDescript,A.AdvLink,B.AdvImg,B.AdvThumbnail,A.CompanyId,A.CompanyName,A.IssueTime,A.AdvRemark,D.MQ FROM tbl_SysAdv AS A ");
            cmdText.Append(" LEFT OUTER JOIN tbl_SysAdvImg AS B ON A.Id=B.AdvId ");
            cmdText.Append(" INNER JOIN tbl_SysAdvAreaControl AS C ON A.Id=C.AdvId AND C.AreaType=@AdvType AND C.AreaId=@RelationId ");
            cmdText.Append(" LEFT OUTER JOIN tbl_CompanyUser AS D ON A.CompanyId=D.CompanyId AND IsAdmin='1' ");
            cmdText.Append(" WHERE A.AreaId=@PositionId AND @Date BETWEEN A.StartDate AND A.EndDate ");

            if (isPlatformAd)
            {
                cmdText.Append(" AND A.ClassId=3 ");
            }
            else
            {
                cmdText.Append(" AND A.ClassId<3 ");
            }

            cmdText.Append(" ORDER BY C.SortId ASC,A.IssueTime ASC ");

            DbCommand cmd = base.SystemStore.GetSqlStringCommand(cmdText.ToString());
            base.SystemStore.AddInParameter(cmd, "Expression", DbType.Int32, expression);
            base.SystemStore.AddInParameter(cmd, "AdvType", DbType.Byte, advType);
            base.SystemStore.AddInParameter(cmd, "RelationId", DbType.Int32, relationId);
            base.SystemStore.AddInParameter(cmd, "PositionId", DbType.Int32, position);
            base.SystemStore.AddInParameter(cmd, "Date", DbType.DateTime, date);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.SystemStore))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.AdvStructure.AdvInfo tmp = new EyouSoft.Model.AdvStructure.AdvInfo();

                    tmp.AdvId = rdr.GetInt32(0);
                    tmp.Title = rdr[1].ToString();
                    tmp.RedirectURL = rdr[2].ToString();
                    tmp.ImgPath = rdr[3].ToString();

                    tmp.AdvThumb = rdr[4].ToString();

                    tmp.CompanyId = rdr.GetString(5);
                    tmp.CompanyName = rdr[6].ToString();
                    tmp.IssueTime = rdr.GetDateTime(7);
                    tmp.Remark = rdr[8].ToString();
                    tmp.ContactMQ=rdr[9].ToString();

                    advs.Add(tmp);
                }
            }

            return advs;
        }

        /// <summary>
        /// 获取广告位置信息集合
        /// </summary>
        /// <param name="catalog">广告栏目</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.AdvStructure.AdvPositionInfo> GetPositions(EyouSoft.Model.AdvStructure.AdvCatalog catalog)
        {
            IList<EyouSoft.Model.AdvStructure.AdvPositionInfo> positions = new List<EyouSoft.Model.AdvStructure.AdvPositionInfo>();
            DbCommand cmd = base.SystemStore.GetSqlStringCommand(SQL_SELECT_GetPositions);
            base.SystemStore.AddInParameter(cmd, "Catalog", DbType.Int32, catalog);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.SystemStore))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.AdvStructure.AdvPositionInfo tmp = new EyouSoft.Model.AdvStructure.AdvPositionInfo();

                    tmp.AdvCount = rdr.GetInt32(rdr.GetOrdinal("AdvCount"));
                    tmp.Catalog = (EyouSoft.Model.AdvStructure.AdvCatalog)rdr.GetInt32(rdr.GetOrdinal("CatalogId"));
                    tmp.DisplayType = (EyouSoft.Model.AdvStructure.AdvDisplayType)rdr.GetByte(rdr.GetOrdinal("DisplayType"));
                    tmp.Position = (EyouSoft.Model.AdvStructure.AdvPosition)rdr.GetInt32(rdr.GetOrdinal("AreaId"));

                    positions.Add(tmp);
                }
            }

            return positions;
        }
        #endregion
    }
}
