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

namespace EyouSoft.DAL.SystemStructure
{

    /// <summary>
    /// 区域联系人信息 数据访问
    /// </summary>
    /// 周文超 2010-05-14
    public class SysAreaContact : DALBase, IDAL.SystemStructure.ISysAreaContact
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysAreaContact()
        { }

        #region SqlString

        private const string Sql_SysAreaContact_Add = " INSERT INTO [tbl_SysAreaContact]([SaleArea],[SaleType],[ContactName],[ContactTel],[ContactMobile],[QQ],[MQ]) VALUES ('{0}',{1},'{2}','{3}','{4}','{5}','{6}'); ";
        private const string Sql_SysAreaContact_Del = " DELETE FROM [tbl_SysAreaContact] ";
        private const string Sql_SysAreaContact_Select = " SELECT [Id],[SaleArea],[SaleType],[ContactName],[ContactTel],[ContactMobile],[QQ],[MQ] FROM [tbl_SysAreaContact] ";

        #endregion

        #region 函数成员

        /// <summary>
        /// 添加区域联系人信息
        /// </summary>
        /// <param name="List">区域联系人信息集合</param>
        /// <returns></returns>
        public virtual int AddSysAreaContact(IList<EyouSoft.Model.SystemStructure.SysAreaContact> List)
        {
            if (List == null || List.Count <= 0)
                return 0;

            StringBuilder strSql = new StringBuilder();
            foreach (Model.SystemStructure.SysAreaContact model in List)
            {
                strSql.AppendFormat(Sql_SysAreaContact_Add, model.SaleArea, model.SaleType, model.ContactName, model.ContactTel, model.ContactMobile, model.QQ, model.MQ);
            }

            DbCommand dc = base.SystemStore.GetSqlStringCommand(strSql.ToString());

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 删除区域联系人信息
        /// </summary>
        /// <param name="AreaContactId">区域联系人信息ID</param>
        /// <returns>受影响行数</returns>
        public virtual int DeleteSysAreaContact(int AreaContactId)
        {
            if (AreaContactId <= 0)
                return 0;

            string strWhere = Sql_SysAreaContact_Del + " WHERE [ID] = @ID ";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);
            base.SystemStore.AddInParameter(dc, "ID", DbType.Int32, AreaContactId);

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 删除所有区域联系人
        /// </summary>
        /// <returns>受影响行数</returns>
        public virtual int DeleteSysAreaContact()
        {
            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysAreaContact_Del);

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 获取区域联系人信息
        /// </summary>
        /// <returns>区域联系人信息实体集合</returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysAreaContact> GetAreaContactList()
        {
            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysAreaContact_Select);

            return GetQueryList(dc);
        }

        #endregion

        #region 私有函数


        /// <summary>
        /// 将查询数据集转化为实体集合
        /// </summary>
        /// <param name="dc">查询命令</param>
        /// <returns>实体集合</returns>
        private IList<Model.SystemStructure.SysAreaContact> GetQueryList(DbCommand dc)
        {
            IList<EyouSoft.Model.SystemStructure.SysAreaContact> list = new List<EyouSoft.Model.SystemStructure.SysAreaContact>();
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                EyouSoft.Model.SystemStructure.SysAreaContact model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.SysAreaContact();
                    if (!dr.IsDBNull(0))
                        model.ID = dr.GetInt32(0);
                    model.SaleArea = dr[1].ToString();
                    if (!dr.IsDBNull(2))
                        model.SaleType = dr.GetByte(2);
                    model.ContactName = dr[3].ToString();
                    model.ContactTel = dr[4].ToString();
                    model.ContactMobile = dr[5].ToString();
                    model.QQ = dr[6].ToString();
                    model.MQ = dr[7].ToString();

                    list.Add(model);
                }

                model = null;
            }
            return list;
        }

        #endregion
    }
}
