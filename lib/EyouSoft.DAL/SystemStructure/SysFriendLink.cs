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
    /// 首页友情链接 数据访问
    /// </summary>
    /// 周文超 2010-05-14
    public class SysFriendLink : DALBase, IDAL.SystemStructure.ISysFriendLink
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysFriendLink()
        { }

        #region SqlString

        private const string Sql_SysFriendLink_Add = " INSERT INTO [tbl_SysFriendLink]([LinkType],[LinkName],[LinkAddress],[ImgPath],[IsChecked] ,[IssueTime]) VALUES (@LinkType,@LinkName,@LinkAddress,@ImgPath,@IsChecked,@IssueTime);SELECT @@IDENTITY; ";
        private const string Sql_SysFriendLink_Update = " UPDATE [tbl_SysFriendLink] SET [LinkType] = @LinkType,[LinkName] = @LinkName,[LinkAddress] = @LinkAddress,[ImgPath] = @ImgPath WHERE [ID] = @ID ";
        private const string Sql_SysFriendLink_Select = " SELECT [ID],[LinkType],[LinkName],[LinkAddress],[ImgPath],[IsChecked],[IssueTime] FROM [tbl_SysFriendLink] ";
        /// <summary>
        /// 删除前移动文件记录
        /// </summary>
        private const string SQL_DELETEDFILE_UPDATEMOVE = "IF (SELECT COUNT(*) FROM [tbl_SysFriendLink] WHERE [ImgPath]=@ImgPath AND [ID]=@ID)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [ImgPath] FROM [tbl_SysFriendLink] WHERE ID=@ID;";
        private const string SQL_DELETEDFILE_DELETEMOVE = "IF (SELECT COUNT(*) FROM [tbl_SysFriendLink] WHERE [ImgPath]='{1}' AND [ID]='{0}')=1 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [ImgPath] FROM [tbl_SysFriendLink] WHERE ID='{0}';";

        #endregion

        #region 函数成员

        /// <summary>
        /// 新增友情链接
        /// </summary>
        /// <param name="model">友情链接实体</param>
        /// <returns>返回新友情链接的ID</returns>
        public virtual int AddSysFriendLink(EyouSoft.Model.SystemStructure.SysFriendLink model)
        {
            if (model == null)
                return 0;

            if (string.IsNullOrEmpty(model.ImgPath))
                model.ImgPath = string.Empty;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SysFriendLink_Add);

            #region 参数赋值

            base.SystemStore.AddInParameter(dc, "LinkType", DbType.Int16, (int)model.LinkType);
            base.SystemStore.AddInParameter(dc, "LinkName", DbType.String, model.LinkName);
            base.SystemStore.AddInParameter(dc, "LinkAddress", DbType.String, model.LinkAddress);
            base.SystemStore.AddInParameter(dc, "ImgPath", DbType.String, model.ImgPath);
            base.SystemStore.AddInParameter(dc, "IsChecked", DbType.AnsiStringFixedLength, model.IsChecked ? "1" : "0");
            base.SystemStore.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);

            #endregion

            object obj = DbHelper.GetSingle(dc, base.SystemStore);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 修改友情链接
        /// </summary>
        /// <param name="model">友情链接实体</param>
        /// <returns>返回受影响的行数</returns>
        public virtual int UpdateSysFriendLink(EyouSoft.Model.SystemStructure.SysFriendLink model)
        {
            if (model == null)
                return 0;

            if (string.IsNullOrEmpty(model.ImgPath))
                model.ImgPath = string.Empty;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(SQL_DELETEDFILE_UPDATEMOVE + Sql_SysFriendLink_Update);

            #region 参数赋值

            base.SystemStore.AddInParameter(dc, "LinkType", DbType.Byte, model.LinkType);
            base.SystemStore.AddInParameter(dc, "LinkName", DbType.String, model.LinkName);
            base.SystemStore.AddInParameter(dc, "LinkAddress", DbType.String, model.LinkAddress);
            base.SystemStore.AddInParameter(dc, "ImgPath", DbType.String, model.ImgPath);
            base.SystemStore.AddInParameter(dc, "ID", DbType.Int32, model.ID);

            #endregion

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 删除友情链接
        /// </summary>
        /// <param name="FriendLinkIds">友情链接ID</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool DeleteSysFriendLink(string FriendLinkIds)
        {
            if (string.IsNullOrEmpty(FriendLinkIds))
                return false;

            string strWhere = " SELECT ID,ImgPath FROM [tbl_SysFriendLink] WHERE [ID] in ({0})";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(string.Format(strWhere, FriendLinkIds));
            StringBuilder sb = new StringBuilder();
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {                
                while (dr.Read())
                {
                    if (!dr.IsDBNull(1) && !string.IsNullOrEmpty(dr.GetString(1)))
                    {
                        sb.AppendFormat(SQL_DELETEDFILE_DELETEMOVE, dr.GetInt32(0).ToString(), dr.GetString(1));
                    }
                }
            }
            sb.AppendFormat(" DELETE FROM [tbl_SysFriendLink] WHERE [ID] in ({0}) ; ", FriendLinkIds);

            dc = base.SystemStore.GetSqlStringCommand(sb.ToString());

            int Result = DbHelper.ExecuteSql(dc, base.SystemStore);
            if (Result > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 根据ID获取友情链接信息
        /// </summary>
        /// <param name="FriendLinkId">友情链接ID</param>
        /// <returns>友情链接实体</returns>
        public virtual EyouSoft.Model.SystemStructure.SysFriendLink GetSysFriendLinkModel(int FriendLinkId)
        {
            if (FriendLinkId <= 0)
                return null;

            Model.SystemStructure.SysFriendLink model = new EyouSoft.Model.SystemStructure.SysFriendLink();

            string strWhere = Sql_SysFriendLink_Select + " where ID = @ID ";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);
            base.SystemStore.AddInParameter(dc, "ID", DbType.Int32, FriendLinkId);

            DataTable dt = DbHelper.DataTableQuery(dc, base.SystemStore);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["ID"].ToString()))
                        model.ID = int.Parse(dr["ID"].ToString());
                    if (!string.IsNullOrEmpty(dr["LinkType"].ToString()))
                        model.LinkType = (Model.SystemStructure.FriendLinkType)int.Parse(dr["LinkType"].ToString());
                    model.LinkName = dr["LinkName"].ToString();
                    model.LinkAddress = dr["LinkAddress"].ToString();
                    model.ImgPath = dr["ImgPath"].ToString();
                    if (!string.IsNullOrEmpty(dr["IsChecked"].ToString()))
                    {
                        if (dr["IsChecked"].ToString() == "1")
                            model.IsChecked = true;
                        else
                            model.IsChecked = false;
                    }
                    if (!string.IsNullOrEmpty(dr["IssueTime"].ToString()))
                        model.IssueTime = DateTime.Parse(dr["IssueTime"].ToString());
                }
            }
            if (dt != null) dt.Dispose();
            dt = null;

            return model;
        }

        /// <summary>
        /// 获取友情链接
        /// </summary>
        /// <param name="LinkType">链接类型，小于等于0不作为条件</param>
        /// <returns>返回友情链接实体集合</returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysFriendLink> GetSysFriendLinkList(EyouSoft.Model.SystemStructure.FriendLinkType LinkType)
        {
            string strWhere = Sql_SysFriendLink_Select;
            if ((int)LinkType > 0)
                strWhere += string.Format(" where LinkType = {0} ", (int)LinkType);

            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);

            return GetQueryList(dc);
        }

        /// <summary>
        /// 获取友情链接
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引:0/1添加时间升/降序</param>
        /// <param name="LinkType">类型</param>
        /// <returns>返回友情链接实体集合</returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysFriendLink> GetSysFriendLinkList(int PageSize, int PageIndex
            , ref int RecordCount, int OrderIndex, EyouSoft.Model.SystemStructure.FriendLinkType? LinkType)
        {
            IList<EyouSoft.Model.SystemStructure.SysFriendLink> list = new List<EyouSoft.Model.SystemStructure.SysFriendLink>();
            string strFiles = " [ID],[LinkType],[LinkName],[LinkAddress],[ImgPath],[IsChecked],[IssueTime] ";
            string strWhere = string.Empty;
            string strOrder = string.Empty;
            if (LinkType != null)
                strWhere += string.Format(" [LinkType] = {0} ", (int)LinkType);

            switch (OrderIndex)
            {
                case 0: strOrder = " [IssueTime] asc "; break;
                case 1: strOrder = " [IssueTime] desc "; break;
                default: strOrder = " [IssueTime] desc "; break;
            }

            using (IDataReader dr = DbHelper.ExecuteReader(base.SystemStore, PageSize, PageIndex, ref RecordCount, "tbl_SysFriendLink", "ID", strFiles, strWhere, strOrder))
            {
                EyouSoft.Model.SystemStructure.SysFriendLink model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.SysFriendLink();

                    if (!string.IsNullOrEmpty(dr["ID"].ToString()))
                        model.ID = int.Parse(dr["ID"].ToString());
                    if (!string.IsNullOrEmpty(dr["LinkType"].ToString()))
                        model.LinkType = (Model.SystemStructure.FriendLinkType)int.Parse(dr["LinkType"].ToString());
                    model.LinkName = dr["LinkName"].ToString();
                    model.LinkAddress = dr["LinkAddress"].ToString();
                    model.ImgPath = dr["ImgPath"].ToString();
                    if (!string.IsNullOrEmpty(dr["IsChecked"].ToString()))
                    {
                        if (dr["IsChecked"].ToString() == "1")
                            model.IsChecked = true;
                        else
                            model.IsChecked = false;
                    }
                    if (!string.IsNullOrEmpty(dr["IssueTime"].ToString()))
                        model.IssueTime = DateTime.Parse(dr["IssueTime"].ToString());

                    list.Add(model);
                }
                model = null;
            }
            return list;
        }
      
        #endregion

        #region 私有函数   根据查询命令返回友情链接实体集合

        /// <summary>
        /// 根据查询命令返回友情链接实体集合
        /// </summary>
        /// <param name="dc">查询命令</param>
        /// <returns>友情链接实体集合</returns>
        private IList<EyouSoft.Model.SystemStructure.SysFriendLink> GetQueryList(DbCommand dc)
        {
            IList<EyouSoft.Model.SystemStructure.SysFriendLink> list = new List<EyouSoft.Model.SystemStructure.SysFriendLink>();
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                EyouSoft.Model.SystemStructure.SysFriendLink model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.SysFriendLink();

                    if (!string.IsNullOrEmpty(dr["ID"].ToString()))
                        model.ID = int.Parse(dr["ID"].ToString());
                    if (!string.IsNullOrEmpty(dr["LinkType"].ToString()))
                        model.LinkType = (Model.SystemStructure.FriendLinkType)int.Parse(dr["LinkType"].ToString());
                    model.LinkName = dr["LinkName"].ToString();
                    model.LinkAddress = dr["LinkAddress"].ToString();
                    model.ImgPath = dr["ImgPath"].ToString();
                    if (!string.IsNullOrEmpty(dr["IsChecked"].ToString()))
                    {
                        if (dr["IsChecked"].ToString() == "1")
                            model.IsChecked = true;
                        else
                            model.IsChecked = false;
                    }
                    if (!string.IsNullOrEmpty(dr["IssueTime"].ToString()))
                        model.IssueTime = DateTime.Parse(dr["IssueTime"].ToString());

                    list.Add(model);
                }
                model = null;
            }
            return list;
        }

        #endregion
    }
}
