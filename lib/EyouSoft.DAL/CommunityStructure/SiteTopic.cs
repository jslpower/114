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
    /// 创建人：鲁功源  2010-07-13
    /// 描述：供求栏目固定文字数据层
    /// </summary>
    public class SiteTopic:DALBase,IDAL.CommunityStructure.ISiteTopic
    {
         /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public SiteTopic() 
        {
            this._database = base.SystemStore;
        }
        #endregion

        #region SQL定义
        private const string SQL_SiteTopic_SetSiteTopic = "if not exists (select 1 from tbl_SiteTopic where fieldname=@fieldname) begin insert into tbl_SiteTopic(fieldname,fieldvalue) values(@fieldname,@fieldvalue) end else begin update tbl_SiteTopic set fieldvalue=@fieldvalue where fieldname=@fieldname end";
        private const string SQL_SiteTopic_GetSiteTopic = "select fieldvalue from tbl_SiteTopic where fieldname=@fieldname";
        #endregion

        #region ISiteTopic成员
        /// <summary>
        /// 设置供求栏目
        /// </summary>
        /// <param name="fieldKey">供求栏目key</param>
        /// <param name="fieldValue">供求栏目value</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool SetSiteTopic(string fieldKey, string fieldValue)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SiteTopic_SetSiteTopic);
            this._database.AddInParameter(dc, "fieldname", DbType.String, fieldKey);
            this._database.AddInParameter(dc, "fieldvalue", DbType.String, fieldValue);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 获取指定栏目的值
        /// </summary>
        /// <param name="FieldKey">栏目名称(key)</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual string GetSiteTopic(string FieldKey)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SiteTopic_GetSiteTopic);
            this._database.AddInParameter(dc, "fieldname", DbType.String, FieldKey);
            object obj = DbHelper.GetSingle(dc, this._database);
            return obj == null ? string.Empty : obj.ToString();
        }
        #endregion

    }
}
