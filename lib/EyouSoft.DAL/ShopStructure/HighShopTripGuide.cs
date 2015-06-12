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
    /// 创建人：鲁功源 2010-06-01
    /// 描述：出游指南数据库
    /// </summary>
    public class HighShopTripGuide:DALBase,IHighShopTripGuide
    {
        #region 构造函数
        /// <summary>
        /// 当前所在数据库
        /// </summary>
        private Database _database = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public HighShopTripGuide() 
        {
            this._database = base.CompanyStore;
        }
        #endregion

        #region SQL定义
        const string SQL_HighShopTripGuide_ADD = "INSERT INTO tbl_HighShopTripGuide(ID,CompanyID,OperatorID,TypeID,Title,ContentText,ImagePath) VALUES(@ID,@CompanyID,@OperatorID,@TypeID,@Title,@ContentText,@ImagePath)";
        const string SQL_HighShopTripGuide_UPDATE = "UPDATE tbl_HighShopTripGuide set TypeID=@TypeID,Title=@Title,ContentText=@ContentText,ImagePath=@ImagePath,UpdateTime=getdate() WHERE ID=@ID";
        const string SQL_HighShopTripGuide_DELETE = "DELETE tbl_HighShopTripGuide WHERE ID=@ID";
        const string SQL_HighShopTripGuide_GETMODEL = "SELECT ID,TypeID,Title,ContentText,ImagePath,UpdateTime,IsTop,TopTime from tbl_HighShopTripGuide WHERE ID=@ID";
        const string SQL_HighShopTripGuide_SETTOP = "UPDATE tbl_HighShopTripGuide set IsTop=@IsTop,TopTime=(CASE WHEN @IsTop='1' THEN GETDATE() ELSE NULL END) where ID=@ID";
        const string SQL_HighShopTripGuide_GETTOPLIST = "SELECT {0} ID,TypeID,Title,ContentText,ImagePath,UpdateTime,IsTop,TopTime from tbl_HighShopTripGuide ";
        #endregion

        #region IHighShopTripGuide成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">出游指南实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Add(EyouSoft.Model.ShopStructure.HighShopTripGuide model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopTripGuide_ADD);
            model.ID = Guid.NewGuid().ToString();
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            this._database.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, model.CompanyID);
            this._database.AddInParameter(dc, "OperatorID", DbType.AnsiStringFixedLength, model.OperatorID);
            this._database.AddInParameter(dc, "TypeID", DbType.Byte,(int)model.TypeID);
            this._database.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._database.AddInParameter(dc, "ContentText", DbType.String, model.ContentText);
            this._database.AddInParameter(dc, "ImagePath", DbType.String, model.ImagePath);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">出游指南实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Update(EyouSoft.Model.ShopStructure.HighShopTripGuide model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopTripGuide_UPDATE);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            this._database.AddInParameter(dc, "TypeID", DbType.Byte, (int)model.TypeID);
            this._database.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._database.AddInParameter(dc, "ContentText", DbType.String, model.ContentText);
            this._database.AddInParameter(dc, "ImagePath", DbType.String, model.ImagePath);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Delete(string ID)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopTripGuide_DELETE);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 获取出游指南实体
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>存在返回出游指南实体,不存在返回NULL</returns>
        public virtual EyouSoft.Model.ShopStructure.HighShopTripGuide GetModel(string ID)
        {
            EyouSoft.Model.ShopStructure.HighShopTripGuide model=null;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopTripGuide_GETMODEL);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                { 
                    model=new EyouSoft.Model.ShopStructure.HighShopTripGuide();
                    model.ID=dr.GetString(0);
                    if (!dr.IsDBNull(1))
                        model.TypeID=(EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType)int.Parse(dr.GetByte(1).ToString());
                    model.Title=dr.GetString(2);
                    model.ContentText=dr.IsDBNull(3)?string.Empty:dr.GetString(3);
                    model.ImagePath=dr.IsDBNull(4)?string.Empty:dr.GetString(4);
                    model.IssueTime = dr.IsDBNull(5) ? DateTime.Now : dr.GetDateTime(5);
                    model.IsTop = dr.IsDBNull(6) ? false : dr.GetString(6) == "1" ? true : false;
                    model.TopTime = dr.IsDBNull(7) ? DateTime.MinValue : dr.GetDateTime(7);
                }
            }
            return model;
        }
        /// <summary>
        /// 获取后台出游指南列表集合
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyID">公司编号 为空返回全部否则返回指定公司的所有记录</param>
        /// <param name="KeyWord">需要匹配的关键字</param> 
        /// <param name="typeList">类别ID,若不包含ID,则返回全部</param>
        /// <returns>出游指南列表集合</returns>
        public virtual IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyID, string KeyWord, params EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType[] typeList)
        {
            IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> list = new List<EyouSoft.Model.ShopStructure.HighShopTripGuide>();
            string tableName = "tbl_HighShopTripGuide";
            string fields = "ID,TypeID,Title,ContentText,ImagePath,UpdateTime,IsTop,TopTime";
            string primaryKey = "ID";
            string orderByString = "IsTop desc,TopTime desc,UpdateTime desc";

            #region 查询条件
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(CompanyID))
            {
                strWhere.AppendFormat(" AND CompanyID='{0}' ", CompanyID);
            }
            if (!string.IsNullOrEmpty(KeyWord))
            {
                strWhere.AppendFormat(" AND title like'%{0}%'", KeyWord);
            }
            if (typeList != null && typeList.Length > 0)
            {
                string sqlTmp = "";
                foreach (EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType type in typeList)
                {
                    sqlTmp += string.Format("{0},", Convert.ToInt32(type));
                }
                strWhere.AppendFormat(" AND TypeID IN ({0})", sqlTmp.TrimEnd(",".ToCharArray()));
            }
            #endregion

            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.ShopStructure.HighShopTripGuide model = new EyouSoft.Model.ShopStructure.HighShopTripGuide();
                    model.ID = dr.GetString(0);
                    if (!dr.IsDBNull(1))
                        model.TypeID = (EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType)dr.GetByte(1);
                    model.Title = dr.GetString(2);
                    model.ContentText = dr.IsDBNull(3) ? "" :dr.GetString(3);
                    model.ImagePath = dr.IsDBNull(4) ? "" : dr.GetString(4);
                    model.IssueTime = dr.GetDateTime(5);
                    model.IsTop = dr.IsDBNull(6) ? false : dr.GetString(6) == "1" ? true : false;
                    model.TopTime = dr.IsDBNull(7) ? DateTime.Now : dr.GetDateTime(7);
                    list.Add(model);
                    model = null;
                }
            }
            return list;        
        }
        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="IsTop">置顶状态 TRUE或FALSE</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool SetTop(string ID, bool IsTop)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopTripGuide_SETTOP);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            this._database.AddInParameter(dc, "IsTop", DbType.AnsiStringFixedLength, IsTop ? "1" : "0");
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 获取前台指定条数的出游指南列表
        /// </summary>
        /// <param name="TopNumber">需要返回的条数 =0返回全部 >0返回指定条数的记录</param>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="TypeID">类别编号 =0返回所有类别 >0返回指定类别的数据</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> GetWebList(int TopNumber, string CompanyID, int TypeID,string KeyWord)
        {
            IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> list = new List<EyouSoft.Model.ShopStructure.HighShopTripGuide>();
            StringBuilder strSql = new StringBuilder();
            if (TopNumber > 0)
            {
                strSql.AppendFormat(SQL_HighShopTripGuide_GETTOPLIST, "Top " + TopNumber.ToString());
            }
            else
            {
                strSql.AppendFormat(SQL_HighShopTripGuide_GETTOPLIST, string.Empty);
            }
            if (!string.IsNullOrEmpty(CompanyID))
            {
                strSql.AppendFormat(" where companyID='{0}' ", CompanyID);
            }
            if (TypeID > 0)
            {
                if (strSql.Length > 0)
                    strSql.AppendFormat(" and TypeID={0}", TypeID);
                else
                    strSql.AppendFormat(" where TypeID={0}", TypeID);
            }
            if (!string.IsNullOrEmpty(KeyWord))
            {
                if (strSql.Length > 0)
                    strSql.AppendFormat(" and Title like'%{0}%'", KeyWord);
                else
                    strSql.AppendFormat(" where Title like'%{0}%'", KeyWord);
            }
            strSql.Append(" order by IsTop desc,TopTime desc,UpdateTime desc");

            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.ShopStructure.HighShopTripGuide model = new EyouSoft.Model.ShopStructure.HighShopTripGuide();
                    model.ID = dr.GetString(0);
                    if (!dr.IsDBNull(1))
                        model.TypeID = (EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType)dr.GetByte(1);
                    model.Title = dr.GetString(2);
                    model.ContentText = dr.IsDBNull(3) ? "" : dr.GetString(3);
                    model.ImagePath = dr.IsDBNull(4) ? "" : dr.GetString(4);
                    model.IssueTime = dr.GetDateTime(5);
                    model.IsTop = dr.IsDBNull(6) ? false : dr.GetString(6) == "1" ? true : false;
                    model.TopTime = dr.IsDBNull(7) ? DateTime.Now : dr.GetDateTime(7);
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        #endregion
    }
}
