using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL.CommunityStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源  2010-07-16
    /// 描述：供求收藏数据层
    /// </summary>
    public class ExchangeFavor:DALBase,IDAL.CommunityStructure.IExchangeFavor
    {
         /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database=null;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExchangeFavor() 
        {
            this._database = base.SystemStore;
        }
        #endregion

        #region SQL定义
        private const string SQL_ExchangeFavor_ADD = "Delete tbl_ExchangeFavor where ExchangeId=@ExchangeId and OperatorId=@OperatorId ;INSERT INTO tbl_ExchangeFavor([ExchangeId],[CompanyId],[OperatorId]) VALUES(@ExchangeId,@CompanyId,@OperatorId)";
        private const string SQL_ExchangeFavor_DELETE = "DELETE tbl_ExchangeFavor where ID in(@IDs)";
        #endregion

        #region IExchangeFavor成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">供求收藏实体</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Add(EyouSoft.Model.CommunityStructure.ExchangeBase model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_ExchangeFavor_ADD);
            this._database.AddInParameter(dc, "ExchangeId", DbType.AnsiStringFixedLength, model.ExchangeId);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._database.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Ids">主键编号字符（，分割）</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Delete(string Ids)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_ExchangeFavor_DELETE);
            this._database.AddInParameter(dc, "IDs", DbType.String, Ids);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 分页获取供求收藏信息列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyId">公司编号 =""返回全部</param>
        /// <param name="UserId">用户编号 =""返回全部</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.ExchangeFavor> GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyId, string UserId)
        {
            IList<EyouSoft.Model.CommunityStructure.ExchangeFavor> list = new List<EyouSoft.Model.CommunityStructure.ExchangeFavor>();
            string tableName = "tbl_ExchangeFavor";
            StringBuilder fields= new StringBuilder("ID,ExchangeId,(select ExchangeTitle from tbl_ExchangeList where ID=tbl_ExchangeFavor.ExchangeId) as ExchangeTitle");
            fields.Append(",(select TopicClassID from tbl_ExchangeList where ID=tbl_ExchangeFavor.ExchangeId) as TopicClassID");
            fields.Append(",(select ExchangeTagId from tbl_ExchangeList where ID=tbl_ExchangeFavor.ExchangeId) as ExchangeTagId");
            fields.Append(",(select ProvinceId from tbl_ExchangeList where ID=tbl_ExchangeFavor.ExchangeId) as ProvinceId");
            fields.Append(",(select WriteBackCount from tbl_ExchangeList where ID=tbl_ExchangeFavor.ExchangeId) as WriteBackCount");
            fields.Append(",(select IssueTime from tbl_ExchangeList where ID=tbl_ExchangeFavor.ExchangeId) as IssueTime");
            string primaryKey = "ID";
            string OrderByString = " ID DESC";
            #region 生成查询条件
            StringBuilder strWhere=new StringBuilder(" 1=1 ");
            if(!string.IsNullOrEmpty(CompanyId))
            {
                strWhere.AppendFormat(" and CompanyId='{0}' ",CompanyId);
            }
            if(!string.IsNullOrEmpty(UserId))
            {
                strWhere.AppendFormat(" and OperatorId='{0}' ",UserId);
            }
            #endregion

            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields.ToString(), strWhere.ToString(), OrderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CommunityStructure.ExchangeFavor model = new EyouSoft.Model.CommunityStructure.ExchangeFavor();
                    model.ID = dr.GetInt32(0);
                    model.ExchangeId = dr.IsDBNull(1) ? string.Empty : dr.GetString(1);
                    model.ExchangeTitle = dr.IsDBNull(2) ? string.Empty : dr.GetString(2);
                    if (!dr.IsDBNull(3))
                        model.TopicClassID = (EyouSoft.Model.CommunityStructure.ExchangeType)int.Parse(dr.GetByte(3).ToString());
                    if (!dr.IsDBNull(4))
                        model.ExchangeTag = (EyouSoft.Model.CommunityStructure.ExchangeTag)int.Parse(dr.GetByte(4).ToString());
                    model.ProvinceId = dr.IsDBNull(5) ? 0 : dr.GetInt32(5);
                    model.WriteBackCount = dr.IsDBNull(6) ? 0 : dr.GetInt32(6);
                    model.IssueTime = dr.IsDBNull(7) ? DateTime.MaxValue : dr.GetDateTime(7);
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        #endregion

    }
}
