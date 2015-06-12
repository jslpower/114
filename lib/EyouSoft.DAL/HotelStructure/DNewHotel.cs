using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IDAL.HotelStructure;
using EyouSoft.Model.HotelStructure;
using EyouSoft.Common.DAL;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.HotelBI;

namespace EyouSoft.DAL.HotelStructure
{
    /// <summary>
    /// 二次整改酒店管理数据访问层类
    /// </summary>
    /// 创建人：mk 2011-5-10
    public class DNewHotel : DALBase, IDNewHotel
    {
        #region 静态成员变量

        private const string SQL_INSERT = "INSERT INTO tbl_NewHotelInfo(OrderId,HotelName,CityName,CityAreaType,HotelStar,MenShiPrice,TeamPrice,QQ,OperateId,OperateTime) VALUES(@OrderId,@HotelName,@CityName,@CityAreaType,@HotelStar,@MenShiPrice,@TeamPrice,@QQ,@OperateId,getdate())";
        private const string SQL_UPDATE = "UPDATE tbl_NewHotelInfo SET OrderId = @OrderId,HotelName =@HotelName,CityName=@CityName,CityAreaType =@CityAreaType,HotelStar=@HotelStar,MenShiPrice=@MenShiPrice,TeamPrice=@TeamPrice,QQ=@QQ,OperateId=@OperateId,OperateTime=getdate() where Id = @Id";
        private const string SQL_DELETE = "Delete tbl_NewHotelInfo where id in({0})";
        private const string SQL_SELECT_BYID = "SELECT * FROM tbl_NewHotelInfo WHERE Id ={0}";
        private const string SQL_SELECT_BYTYPE = "SELECT top {0} * FROM tbl_NewHotelInfo WHERE CityAreaType={1}  order by id desc";
        private const string SQL_SELECT_HuaDong = "SELECT top {0} * FROM tbl_NewHotelInfo WHERE CityAreaType={1}  AND cityName ='上海'  union all SELECT top {0} * FROM tbl_NewHotelInfo WHERE CityAreaType={1}  AND cityName ='杭州'  union all SELECT top {0} * FROM tbl_NewHotelInfo WHERE CityAreaType={1}  AND cityName ='苏州'  union all SELECT top {0} * FROM tbl_NewHotelInfo WHERE CityAreaType={1}  AND cityName ='南京'  union all SELECT top {0} * FROM tbl_NewHotelInfo WHERE CityAreaType={1}  AND cityName ='无锡' order by id desc ";
        private const string SQL_SELECT_GangAao = "SELECT top {0} * FROM tbl_NewHotelInfo WHERE CityAreaType={1}  order by id desc ";

        #endregion

        private Database _db = null;


        /// <summary>
        /// 构造函数
        /// </summary>
        public DNewHotel()
        {
            this._db = base.HotelStore;
        }

        #region IDNewHotel 成员

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(MNewHotelInfo model)
        {
            DbCommand dc = this._db.GetSqlStringCommand(SQL_INSERT);
            this._db.AddInParameter(dc, "OrderId", DbType.Int32, GetNextOrderId());
            this._db.AddInParameter(dc, "HotelName", DbType.String, model.HotelName);
            this._db.AddInParameter(dc, "CityName", DbType.String, model.CityName);
            this._db.AddInParameter(dc, "CityAreaType", DbType.Byte, (int)model.CityAreaType);
            this._db.AddInParameter(dc, "HotelStar", DbType.Byte, (int)model.HotelStar);
            this._db.AddInParameter(dc, "MenShiPrice", DbType.Decimal, model.MenShiPrice);
            this._db.AddInParameter(dc, "TeamPrice", DbType.String, model.TeamPrice);
            this._db.AddInParameter(dc, "QQ", DbType.Decimal, model.QQ);
            this._db.AddInParameter(dc, "OperateId", DbType.Int32, (int)model.OperateId);
            return DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 获取下个序号
        /// </summary>
        /// <returns></returns>
        private int GetNextOrderId()
        {
            string maxOrderId = "select max(orderId) from tbl_NewHotelInfo";
            DbCommand dc = this._db.GetSqlStringCommand(maxOrderId);
            object result = DbHelper.GetSingle(dc, this._db);
            if (result == null)
                return 1;
            else
                return (int)result + 1;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(MNewHotelInfo model)
        {
            DbCommand dc = this._db.GetSqlStringCommand(SQL_UPDATE);
            this._db.AddInParameter(dc, "OrderId", DbType.Int32, model.OrderId);
            this._db.AddInParameter(dc, "HotelName", DbType.String, model.HotelName);
            this._db.AddInParameter(dc, "CityName", DbType.String, model.CityName);
            this._db.AddInParameter(dc, "CityAreaType", DbType.Byte, (int)model.CityAreaType);
            this._db.AddInParameter(dc, "HotelStar", DbType.Byte, (int)model.HotelStar);
            this._db.AddInParameter(dc, "MenShiPrice", DbType.Decimal, model.MenShiPrice);
            this._db.AddInParameter(dc, "TeamPrice", DbType.String, model.TeamPrice);
            this._db.AddInParameter(dc, "QQ", DbType.Decimal, model.QQ);
            this._db.AddInParameter(dc, "OperateId", DbType.Int32, (int)model.OperateId);
            this._db.AddInParameter(dc, "Id", DbType.Int32, (int)model.Id);
            return DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool Delete(params int[] ids)
        {
            StringBuilder strQuery = new StringBuilder();
            foreach (var item in ids)
            {
                strQuery.AppendFormat("{0},", item);
            }
            DbCommand dc = this._db.GetSqlStringCommand(string.Format(SQL_DELETE, strQuery.ToString().TrimEnd(',')));
            return DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MNewHotelInfo GetModel(int id)
        {
            MNewHotelInfo model = null;
            StringBuilder strSql = new StringBuilder();

            strSql.AppendFormat(SQL_SELECT_BYID, id);

            DbCommand dc = this._db.GetSqlStringCommand(strSql.ToString());

            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    model = new MNewHotelInfo();
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    model.OrderId = dr.GetInt32(dr.GetOrdinal("OrderId"));
                    model.HotelName = dr["HotelName"].ToString();
                    model.CityName = dr["CityName"].ToString();
                    model.CityAreaType = (CityAreaType)int.Parse(dr[dr.GetOrdinal("CityAreaType")].ToString());
                    model.HotelStar = (HotelStarType)int.Parse(dr[dr.GetOrdinal("HotelStar")].ToString());
                    model.QQ = dr["QQ"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("MenShiPrice")))
                        model.MenShiPrice = dr.GetDecimal(dr.GetOrdinal("MenShiPrice"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TeamPrice")))
                        model.TeamPrice = dr.GetDecimal(dr.GetOrdinal("TeamPrice"));
                    model.OperateId = dr.GetInt32(dr.GetOrdinal("OperateId"));
                    model.OperateTime = dr.GetDateTime(dr.GetOrdinal("OperateTime"));
                }
            }
            return model;
        }

        /// <summary>
        /// 根据类别获取分页数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="hotelType"></param>
        /// <returns></returns>
        public IList<MNewHotelInfo> GetList(int pageSize, int pageIndex, ref int recordCount)
        {
            IList<MNewHotelInfo> list = new List<MNewHotelInfo>();
            MNewHotelInfo model = null;
            string tableName = "tbl_NewHotelInfo";
            string fields = "*";
            string primaryKey = "ID";
            string orderByString = "Id DESC";
            string strWhere = "";//查询条件

            using (IDataReader dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderByString))
            {
                while (dr.Read())
                {
                    model = new MNewHotelInfo();
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    model.OrderId = dr.GetInt32(dr.GetOrdinal("OrderId"));
                    model.HotelName = dr["HotelName"].ToString();
                    model.CityName = dr["CityName"].ToString();
                    model.CityAreaType = (CityAreaType)int.Parse(dr[dr.GetOrdinal("CityAreaType")].ToString());
                    model.HotelStar = (HotelStarType)int.Parse(dr[dr.GetOrdinal("HotelStar")].ToString());
                    model.QQ = dr["QQ"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("MenShiPrice")))
                        model.MenShiPrice = dr.GetDecimal(dr.GetOrdinal("MenShiPrice"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TeamPrice")))
                        model.TeamPrice = dr.GetDecimal(dr.GetOrdinal("TeamPrice"));
                    model.OperateId = dr.GetInt32(dr.GetOrdinal("OperateId"));
                    model.OperateTime = dr.GetDateTime(dr.GetOrdinal("OperateTime"));
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }

        /// <summary>
        ///  获取首页显示的酒店信息
        /// </summary>
        /// <param name="cityAreaType">城市区域类型</param>
        /// <param name="cityName">城市名称</param>
        /// <param name="topNumber">个数</param>
        /// <returns>IList</returns>
        public IList<MNewHotelInfo> GetList(CityAreaType hotelType, string cityName, int topNumber)
        {
            IList<MNewHotelInfo> list = new List<MNewHotelInfo>();
            MNewHotelInfo model = null;
            StringBuilder strSql = new StringBuilder();


            if (!string.IsNullOrEmpty(cityName))
            {
                strSql.AppendFormat(SQL_SELECT_BYTYPE, topNumber, (int)hotelType);
                strSql.Append(string.Format(" and  CityName = '{0}'", cityName));
            }
            else
            {
                if((int)hotelType==0)
                    strSql.AppendFormat(SQL_SELECT_HuaDong, topNumber, (int)hotelType);
                if ((int)hotelType == 1)
                    strSql.AppendFormat(SQL_SELECT_GangAao, topNumber, (int)hotelType);
            }


            DbCommand dc = this._db.GetSqlStringCommand(strSql.ToString());

            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    model = new MNewHotelInfo();
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    model.OrderId = dr.GetInt32(dr.GetOrdinal("OrderId"));
                    model.HotelName = dr["HotelName"].ToString();
                    model.CityName = dr["CityName"].ToString();
                    model.CityAreaType = (CityAreaType)int.Parse(dr[dr.GetOrdinal("CityAreaType")].ToString());
                    model.HotelStar = (HotelStarType)int.Parse(dr[dr.GetOrdinal("HotelStar")].ToString());
                    model.QQ = dr["QQ"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("MenShiPrice")))
                        model.MenShiPrice = dr.GetDecimal(dr.GetOrdinal("MenShiPrice"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TeamPrice")))
                        model.TeamPrice = dr.GetDecimal(dr.GetOrdinal("TeamPrice"));
                    model.OperateId = dr.GetInt32(dr.GetOrdinal("OperateId"));
                    model.OperateTime = dr.GetDateTime(dr.GetOrdinal("OperateTime"));
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }

        #endregion
    }
}
