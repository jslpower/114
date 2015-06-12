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
    /// 描述：轮换广告数据层
    /// </summary>
    public class HighShopAdv:DALBase,IHighShopAdv
    {
        #region 构造函数
        /// <summary>
        /// 当前所在数据库
        /// </summary>
        private Database _database = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public HighShopAdv() 
        {
            this._database = base.CompanyStore;
        }
        #endregion

        #region SQL定义
        const string SQL_HighShopAdv_DELETEBYCOMPANYID = " DELETE tbl_HighShopAdv where ID='{0}' AND CompanyID=@CompanyID ;";
        const string SQL_HighShopAdv_INSERT = " INSERT INTO tbl_HighShopAdv(ID,CompanyID,OperatorID,ImagePath,LinkAddress,SortID) VALUES('{0}','{1}','{2}','{3}','{4}',{5}) ;";
        const string SQL_HighShopAdv_DELETEBYID = "DELETE tbl_HighShopAdv where ID=@ID";
        const string SQL_HighShopAdv_GETLISTBYCOMPANYID = "SELECT {0} ID,ImagePath,LinkAddress,sortID from tbl_HighShopAdv where CompanyID=@CompanyID order by sortID desc,IssueTime desc";
        const string SQL_HighShopAdv_GETMODEL = "SELECT ID,ImagePath,LinkAddress from tbl_HighShopAdv where ID=@ID";
        /// <summary>
        /// 删除前移动文件记录
        /// </summary>
        const string SQL_DELETEDFILE_UPDATEMOVE = "IF (SELECT COUNT(*) FROM [tbl_HighShopAdv] WHERE [ID]='{0}' AND [ImagePath]='{1}')=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [ImagePath] FROM [tbl_HighShopAdv] WHERE [ID]='{0}' AND ImagePath<>'';";
        const string SQL_DELETEDFILE_DELETEMOVE = "INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [ImagePath] FROM [tbl_HighShopAdv] WHERE [ID]=@ID AND ImagePath<>'';";
        #endregion

        #region IHighShopAdv成员
        /// <summary>
        /// 修改网店轮换广告
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="list">轮换广告列表集合</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Update(string CompanyID, IList<EyouSoft.Model.ShopStructure.HighShopAdv> list)
        {
            StringBuilder strSql = new StringBuilder();
            foreach (EyouSoft.Model.ShopStructure.HighShopAdv model in list)
            {
                if (model != null)
                {
                    strSql.AppendFormat(SQL_DELETEDFILE_UPDATEMOVE, model.ID, model.ImagePath);
                    strSql.AppendFormat(SQL_HighShopAdv_DELETEBYCOMPANYID, model.ID);
                    strSql.AppendFormat(SQL_HighShopAdv_INSERT,Guid.NewGuid().ToString(), model.CompanyID, model.OperatorID, model.ImagePath, model.LinkAddress, model.SortID);
                }
            }
            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            this._database.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, CompanyID);
            return DbHelper.ExecuteSqlTrans(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool DeletePic(string ID)
        {           
            DbCommand dc = this._database.GetSqlStringCommand(SQL_DELETEDFILE_DELETEMOVE + SQL_HighShopAdv_DELETEBYID);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 获取后台轮换广告列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyID">公司编号 为空返回全部，否则返回指定公司的轮换广告集合</param>
        /// <returns>轮换广告列表集合</returns>
        public virtual IList<EyouSoft.Model.ShopStructure.HighShopAdv> GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyID)
        {
            IList<EyouSoft.Model.ShopStructure.HighShopAdv> list = new List<EyouSoft.Model.ShopStructure.HighShopAdv>();
            string tableName = "tbl_HighShopAdv";
            string fields = " ID,ImagePath,LinkAddress,SortID,CompanyID,IssueTime ";
            string primaryKey = "ID";
            string orderByString = "SortID DESC,IssueTime DESC";
            string strWhere=string.Empty;
            if(!string.IsNullOrEmpty(CompanyID))
            {
                strWhere=string.Format(" CompanyID='{0}' ",CompanyID);
            }
            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere, orderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.ShopStructure.HighShopAdv model = new EyouSoft.Model.ShopStructure.HighShopAdv();
                    model.ID = dr.GetString(0);
                    model.ImagePath = dr.GetString(1);
                    model.LinkAddress = dr.GetString(2);
                    model.SortID = dr.GetByte(3);
                    model.CompanyID = dr.GetString(4);
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 获取指定编号的轮换广告信息
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <returns>轮换广告列表集合</returns>
        public virtual EyouSoft.Model.ShopStructure.HighShopAdv GetModel(string CompanyID)
        {
            EyouSoft.Model.ShopStructure.HighShopAdv model = null;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopAdv_GETMODEL);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, CompanyID);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.ShopStructure.HighShopAdv();
                    model.ID = dr.GetString(0);
                    model.ImagePath = dr.GetString(1);
                    model.LinkAddress = dr.GetString(2);
                }
            }
            return model;
        }
        /// <summary>
        /// 前台获取指定公司的轮换广告列表
        /// </summary>
        /// <param name="TopNumber">需要获取的记录数</param>
        /// <param name="CompanyID">公司编号</param>
        /// <returns>轮换广告列表集合</returns>
        public virtual IList<EyouSoft.Model.ShopStructure.HighShopAdv> GetWebList(int TopNumber,string CompanyID)
        {
            IList<EyouSoft.Model.ShopStructure.HighShopAdv> list = new List<EyouSoft.Model.ShopStructure.HighShopAdv>();
            DbCommand dc = this._database.GetSqlStringCommand(string.Format(SQL_HighShopAdv_GETLISTBYCOMPANYID,TopNumber>0?string.Format(" top {0} ",TopNumber):string.Empty));
            this._database.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, CompanyID);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.ShopStructure.HighShopAdv model = new EyouSoft.Model.ShopStructure.HighShopAdv();
                    model.ID = dr.GetString(0);
                    model.ImagePath = dr.GetString(1);
                    model.LinkAddress = dr.GetString(2);                    
                    model.SortID = dr.GetByte(dr.GetOrdinal("SortID"));
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        #endregion
    }
}
