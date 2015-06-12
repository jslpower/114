using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL.ShopStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.ShopStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-02
    /// 描述：友情链接数据层
    /// </summary>
    public class HighShopFriendLink:DALBase,IHighShopFriendLink
    {
        #region 构造函数
        /// <summary>
        /// 当前所在数据库
        /// </summary>
        private Database _database = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public HighShopFriendLink() 
        {
            this._database = base.CompanyStore;
        }
        #endregion

        #region SQL定义
        const string SQL_HighShopFriendLink_ADD = "INSERT INTO tbl_HighShopFriendLink(ID,CompanyID,OperatorID,LinkName,LinkAddress,SortID) VALUES(@ID,@CompanyID,@OperatorID,@LinkName,@LinkAddress,@SortID)";
        const string SQL_HighShopFriendLink_UPDATE = "UPDATE tbl_HighShopFriendLink set LinkName=@LinkName,LinkAddress=@LinkAddress,SortID=@SortID WHERE ID=@ID";
        const string SQL_HighShopFriendLink_DELETE = "DELETE tbl_HighShopFriendLink WHERE ID=@ID";
        const string SQL_HighShopFriendLink_SELECT = "SELECT ID,LinkName,LinkAddress,SortID from tbl_HighShopFriendLink {0} order by SortID desc,IssueTime desc";
        #endregion

        #region IHighShopFriendLink成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">友情链接实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Add(EyouSoft.Model.ShopStructure.HighShopFriendLink model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopFriendLink_ADD);
            model.ID = Guid.NewGuid().ToString();
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            this._database.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, model.CompanyID);
            this._database.AddInParameter(dc, "OperatorID", DbType.AnsiStringFixedLength, model.OperatorID);
            this._database.AddInParameter(dc, "LinkName", DbType.String, model.LinkName);
            this._database.AddInParameter(dc, "LinkAddress", DbType.String, model.LinkAddress);
            this._database.AddInParameter(dc, "SortID", DbType.Int32, model.SortID);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">友情链接实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Update(EyouSoft.Model.ShopStructure.HighShopFriendLink model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopFriendLink_UPDATE);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            this._database.AddInParameter(dc, "LinkName", DbType.String, model.LinkName);
            this._database.AddInParameter(dc, "LinkAddress", DbType.String, model.LinkAddress);
            this._database.AddInParameter(dc, "SortID", DbType.Int32, model.SortID);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Delete(string ID)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopFriendLink_DELETE);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 获取指定公司的友情链接列表
        /// </summary>
        /// <param name="CompanyID">公司编号 为空返回全部否则返回指定公司的记录</param>
        /// <returns>友情链接列表</returns>
        public virtual IList<EyouSoft.Model.ShopStructure.HighShopFriendLink> GetList(string CompanyID)
        {
            IList<EyouSoft.Model.ShopStructure.HighShopFriendLink> list = new List<EyouSoft.Model.ShopStructure.HighShopFriendLink>();
            DbCommand dc = null;
            if (string.IsNullOrEmpty(CompanyID))
            {
                dc = this._database.GetSqlStringCommand(string.Format(SQL_HighShopFriendLink_SELECT, string.Empty));
            }
            else
            {
                dc = this._database.GetSqlStringCommand(string.Format(SQL_HighShopFriendLink_SELECT, " where CompanyID=@CompanyID "));
            }
            if (dc != null)
            {
                this._database.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, CompanyID);
                using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
                {
                    while (dr.Read())
                    {
                        EyouSoft.Model.ShopStructure.HighShopFriendLink model = new EyouSoft.Model.ShopStructure.HighShopFriendLink();
                        model.ID = dr.GetString(0);
                        model.LinkName = dr.GetString(1);
                        model.LinkAddress = dr.GetString(2);
                        if(!dr.IsDBNull(3))
                            model.SortID = dr.GetInt32(3);
                        list.Add(model);
                        model = null;
                    }
                }
            }
            return list;
        }
        #endregion
    }
}
