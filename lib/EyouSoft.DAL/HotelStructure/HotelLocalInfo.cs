using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;

namespace EyouSoft.DAL.HotelStructure
{
    /// <summary>
    /// 酒店系统-首页酒店信息实体
    /// </summary>
    /// 创建人：luofx 2010-12-1
    public class HotelLocalInfo : DALBase, EyouSoft.IDAL.HotelStructure.IHotelLocalInfo
    {
        #region SQL语句
        private const string Sql_Select_HotelCity = "SELECT distinct [CityCode],(SELECT CityName FROM tbl_HotelCity b WHERE b.[CityCode]=a.[CityCode]) AS CityName FROM [dbo].[tbl_HotelLocalInfo] a WHERE [ShowType]=@ShowType Order by IssueTime DESC";
        private const string Sql_Model_LocalHotelInfo = "SELECT [ID],[HotelCode],[HotelName],[ShortDesc],[Rank],[MarketingPrice],[CityCode],[HotelImg],[ShowType],[SortId],[IsTop] ,[IssueTime] FROM [dbo].[tbl_HotelLocalInfo] WHERE [id]=@ID";
        #endregion
        private Database _db = null;

        #region Public Methods
        /// <summary>
        /// 构造函数
        /// </summary>
        public HotelLocalInfo()
        {
            this._db = base.HotelStore;
        }
        /// <summary>
        /// 根据类型获取酒店系统的首页酒店板块信息集合
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="CityCode">城市三字码(为空时，不做条件)</param>
        /// <param name="topNum">数据条数 0:获取所有</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.HotelStructure.HotelLocalInfo> GetList(EyouSoft.Model.HotelStructure.HotelShowType type, string CityCode, int topNum)
        {
            IList<EyouSoft.Model.HotelStructure.HotelLocalInfo> lists = null;
            EyouSoft.Model.HotelStructure.HotelLocalInfo model = null;
            string StrSql = string.Empty;
            if (topNum > 0)
            {
                StrSql = string.Format("SELECT TOP {0} [ID],[HotelCode],[HotelName],[ShortDesc],[Rank],[MarketingPrice],[CityCode],[HotelImg],[ShowType],[SortId],[IsTop] ,[IssueTime] FROM [dbo].[tbl_HotelLocalInfo] WHERE [ShowType]={1} ", topNum, (int)type);
            }
            else
            {
                StrSql = string.Format("SELECT [ID],[HotelCode],[HotelName],[ShortDesc],[Rank],[MarketingPrice],[CityCode],[HotelImg],[ShowType],[SortId],[IsTop] ,[IssueTime] FROM [dbo].[tbl_HotelLocalInfo] WHERE [ShowType]={0} ", (int)type);
            }
            if (!string.IsNullOrEmpty(CityCode))
            {
                StrSql = StrSql + " AND [CityCode]=@CityCode";
            }
            StrSql = StrSql + " ORDER BY [IsTop] DESC,[SortId] DESC,IssueTime DESC";
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            this._db.AddInParameter(dc, "ShowType", DbType.Byte, (int)type);
            if (!string.IsNullOrEmpty(CityCode))
            {
                this._db.AddInParameter(dc, "CityCode", DbType.AnsiString, CityCode);
            }
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                lists = new List<EyouSoft.Model.HotelStructure.HotelLocalInfo>();
                while (dr.Read())
                {
                    model = new EyouSoft.Model.HotelStructure.HotelLocalInfo();
                    InputModelValue(model, dr);
                    lists.Add(model);
                    model = null;
                }
            }
            return lists;
        }
        /// <summary>
        /// 根据类型获取所有的酒店信息集合(分页)
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="RecordCount"></param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.HotelStructure.HotelLocalInfo> GetList(int PageSize, int PageIndex, ref int RecordCount, EyouSoft.Model.HotelStructure.HotelShowType type)
        {
            IList<EyouSoft.Model.HotelStructure.HotelLocalInfo> lists = null;
            string StrTableName = "tbl_HotelLocalInfo";
            string StrFiledName = " [ID],[HotelCode],[HotelName],[ShortDesc],[Rank],[MarketingPrice],[CityCode],[HotelImg],[ShowType],[SortId],[IsTop] ,[IssueTime] ";
            string StrWhereQuery = string.Format("[ShowType]='{0}' ", (int)type);
            string StrOrderBy = " IsTop,IssueTime DESC";
            #region List赋值
            using (IDataReader dr = DbHelper.ExecuteReader(this._db, PageSize, PageIndex, ref RecordCount, StrTableName, "ID", StrFiledName, StrWhereQuery, StrOrderBy))
            {
                lists = new List<EyouSoft.Model.HotelStructure.HotelLocalInfo>();
                EyouSoft.Model.HotelStructure.HotelLocalInfo model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.HotelStructure.HotelLocalInfo();
                    InputModelValue(model, dr);
                    lists.Add(model);
                    model = null;
                }
            }
            #endregion
            return lists;
        }
        /// <summary>
        /// 根据酒店模块类型获取城市信息集合
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.HotelStructure.HotelCity> GetList(EyouSoft.Model.HotelStructure.HotelShowType type)
        {
            IList<EyouSoft.Model.HotelStructure.HotelCity> lists = null;
            EyouSoft.Model.HotelStructure.HotelCity model = null;
            DbCommand dc = this._db.GetSqlStringCommand(Sql_Select_HotelCity);
            this._db.AddInParameter(dc, "ShowType", DbType.Byte, (int)type);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                lists = new List<EyouSoft.Model.HotelStructure.HotelCity>();
                while (dr.Read())
                {
                    model = new EyouSoft.Model.HotelStructure.HotelCity();
                    model.CityCode = dr.GetString(dr.GetOrdinal("CityCode"));
                    model.CityName = dr.GetString(dr.GetOrdinal("CityName"));
                    lists.Add(model);
                    model = null;
                }
            }
            return lists;
        }
        /// <summary>
        /// 获取酒店系统-首页酒店板块信息实体
        /// </summary>
        /// <param name="Id">首页酒店板块信息主键Id</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.HotelStructure.HotelLocalInfo GetModel(string Id)
        {
            EyouSoft.Model.HotelStructure.HotelLocalInfo model = null;
            DbCommand dc = this._db.GetSqlStringCommand(Sql_Model_LocalHotelInfo);
            this._db.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, Id);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.HotelStructure.HotelLocalInfo();
                    InputModelValue(model, dr);
                }
            }
            return model;
        }
        /// <summary>
        /// 添加酒店系统首页酒店板块信息
        /// </summary>
        /// <param name="HotelList">首页酒店信息IList集合</param>
        /// <returns></returns>
        public virtual int Add(IList<EyouSoft.Model.HotelStructure.HotelLocalInfo> HotelList)
        {
            int AddCount = 0;
            DbCommand dc = this._db.GetStoredProcCommand("proc_HotelLocalInfo_Insert");
            StringBuilder StrBuild = new StringBuilder();
            #region 生成XML
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.HotelStructure.HotelLocalInfo model in HotelList)
            {
                StrBuild.AppendFormat("<LocalHotelInfo CityCode=\"{0}\"", model.CityCode);
                StrBuild.AppendFormat(" HotelCode=\"{0}\" ", model.HotelCode);
                StrBuild.AppendFormat(" HotelImg=\"{0}\" ", model.HotelImg);
                StrBuild.AppendFormat(" HotelName=\"{0}\" ", model.HotelName);
                StrBuild.AppendFormat(" IsTop=\"{0}\" ", model.IsTop ? "1" : "0");
                StrBuild.AppendFormat(" MarketingPrice=\"{0}\" ", model.MarketingPrice);
                StrBuild.AppendFormat(" Rank=\"{0}\" ", (int)model.Rank);
                StrBuild.AppendFormat(" ShortDesc=\"{0}\" ", model.ShortDesc);
                StrBuild.AppendFormat(" ShowType=\"{0}\" ", (int)model.ShowType);
                StrBuild.AppendFormat(" SortId=\"{0}\" />", model.SortId);
            }
            StrBuild.Append("</ROOT>");
            #endregion
            this._db.AddInParameter(dc, "LocalHotelInfoXML", DbType.String, StrBuild.ToString());
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null))
            {
                AddCount = int.Parse(Result.ToString());
            }
            return AddCount;
        }
        /// <summary>
        /// 删除首页酒店信息
        /// </summary>
        /// <param name="Ids">酒店信息实体主键编号Id</param>
        /// <returns></returns>
        public virtual int Delete(params string[] Ids)
        {
            int AddCount = 0;
            string StrSql = "DELETE FROM [tbl_HotelLocalInfo] WHERE [ID] IN({0})";
            string StrId = string.Empty;
            foreach (string id in Ids)
            {
                StrId += "'" + id.ToString() + "',";
            }
            StrSql = string.Format(StrSql, StrId.TrimEnd(','));
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            AddCount = DbHelper.ExecuteSql(dc, this._db);
            return AddCount;
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public virtual bool SetTop(string id)
        //{

        //}
        #endregion

        #region Private Methods
        /// <summary>
        /// 给酒店信息板块model赋值
        /// </summary>
        /// <param name="model">酒店首页信息板块实体</param>
        /// <param name="dr">IDataReader</param>
        private void InputModelValue(EyouSoft.Model.HotelStructure.HotelLocalInfo model, IDataReader dr)
        {
            model.CityCode = dr.IsDBNull(dr.GetOrdinal("CityCode")) ? "" : dr.GetString(dr.GetOrdinal("CityCode"));
            model.HotelCode = dr.GetString(dr.GetOrdinal("HotelCode"));
            model.HotelImg = dr.IsDBNull(dr.GetOrdinal("HotelImg")) ? "" : dr.GetString(dr.GetOrdinal("HotelImg"));
            model.HotelName = dr.GetString(dr.GetOrdinal("HotelName"));
            model.Id = dr.GetString(dr.GetOrdinal("Id"));
            model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
            model.IsTop = dr.GetString(dr.GetOrdinal("IsTop")) == "0" ? false : true;
            model.MarketingPrice = dr.GetDecimal(dr.GetOrdinal("MarketingPrice"));
            model.Rank = (EyouSoft.HotelBI.HotelRankEnum)Enum.Parse(typeof(EyouSoft.HotelBI.HotelRankEnum), dr.GetByte(dr.GetOrdinal("Rank")).ToString());
            model.ShortDesc = dr.IsDBNull(dr.GetOrdinal("ShortDesc")) ? "" : dr.GetString(dr.GetOrdinal("ShortDesc"));
            model.ShowType = (EyouSoft.Model.HotelStructure.HotelShowType)Enum.Parse(typeof(EyouSoft.Model.HotelStructure.HotelShowType), dr.GetByte(dr.GetOrdinal("ShowType")).ToString());
            model.SortId = dr.GetInt32(dr.GetOrdinal("SortId"));
        }
        #endregion
    }
}
