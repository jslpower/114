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
    /// 在线用户 数据访问
    /// </summary>
    /// 周文超 2010-05-14
    public class SysOnlineUser : DALBase, IDAL.SystemStructure.ISysOnlineUser
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysOnlineUser()
        { }

        #region SqlString

        private const string Sql_SysOnlineUser_Add = " INSERT INTO [tbl_SysOnlineUser] ([ID],[UserId],[CompanyId],[LoginTime]) VALUES (@ID,@UserId,@CompanyId,@LoginTime) ";
        private const string Sql_SysOnlineUser_Del = " DELETE FROM [tbl_SysOnlineUser] WHERE DATEDIFF(hh,LoginTime,getdate())>12";
        private const string Sql_SysOnlineUser_Select = " SELECT [ID],[UserId],[CompanyId],[LoginTime] FROM [tbl_SysOnlineUser] ";

        #endregion

        #region 函数成员

        /// <summary>
        /// 新增在线用户
        /// </summary>
        /// <param name="model">在线用户实体</param>
        /// <returns>返回受影响行数</returns>
        public virtual int AddSysOnlineUser(EyouSoft.Model.SystemStructure.SysOnlineUser model)
        {
            if (model == null)
                return 0;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysOnlineUser_Add);
            base.SystemStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, Guid.NewGuid().ToString());
            base.SystemStore.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, model.UserId);
            base.SystemStore.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            base.SystemStore.AddInParameter(dc, "LoginTime", DbType.DateTime, model.LoginTime);

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 删除在线用户
        /// </summary>
        /// <returns>返回操作是否成功</returns>
        public virtual bool DeleteSysOnlineUser()
        {
            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysOnlineUser_Del);

            int Result = DbHelper.ExecuteSql(dc, base.SystemStore);
            if (Result > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        /// <returns>在线用户实体集合</returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysOnlineUser> GetOnlineUserList()
        {
            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysOnlineUser_Select);
            return GetQueryList(dc);
        }

        #endregion

        #region 私有函数   根据查询命令返回在线用户列表

        /// <summary>
        /// 根据查询命令返回在线用户列表
        /// </summary>
        /// <param name="dc">查询命令</param>
        /// <returns>返回在线用户列表</returns>
        private IList<EyouSoft.Model.SystemStructure.SysOnlineUser> GetQueryList(DbCommand dc)
        {
            IList<EyouSoft.Model.SystemStructure.SysOnlineUser> list = new List<EyouSoft.Model.SystemStructure.SysOnlineUser>();
            using (IDataReader dr = base.TourStore.ExecuteReader(dc))
            {
                EyouSoft.Model.SystemStructure.SysOnlineUser model = null;
                while (dr.Read())
                {
                    model = new Model.SystemStructure.SysOnlineUser();
                    model.ID = dr[0].ToString();
                    model.UserId = dr[1].ToString();
                    model.CompanyId = dr[2].ToString();
                    if (dr.IsDBNull(3))
                        model.LoginTime = dr.GetDateTime(3);

                    list.Add(model);
                }

                model = null;
            }
            return list;
        }

        #endregion
    }
}
