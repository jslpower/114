using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.CommunityStructure
{
    /// <summary>
    /// 顾问团队数据访问层
    /// </summary>
    /// 周文超 2010-07-15
    public class CommunityAdvisor : DALBase, IDAL.CommunityStructure.ICommunityAdvisor
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CommunityAdvisor() { }

        #region SqlString

        private const string Sql_CommunityAdvisor_Add = " INSERT INTO [tbl_CommunityAdvisor] ([ContactName],[Sex],[Age],[CompanyName],[ContactTel],[QQ],[Job],[ImgPath],[Achieve],[Honour],[IsCheck],[IsShow],[OperatorId],[SysOperatorId]) VALUES (@ContactName,@Sex,@Age,@CompanyName,@ContactTel,@QQ,@Job,@ImgPath,@Achieve,@Honour,@IsCheck,@IsShow,@OperatorId,@SysOperatorId) ";
        private const string Sql_CommunityAdvisor_Update = " UPDATE [tbl_CommunityAdvisor] SET [ContactName] = @ContactName,[Sex] = @Sex,[Age] = @Age,[CompanyName] = @CompanyName,[ContactTel] = @ContactTel,[QQ] = @QQ,[Job] = @Job,[ImgPath] = @ImgPath,[Achieve] = @Achieve,[Honour] = @Honour,[IsCheck] = @IsCheck,[OperatorId] = @OperatorId,[SysOperatorId] = @SysOperatorId WHERE [ID] = @ID ";
        private const string Sql_CommunityAdvisor_Delete = " DELETE FROM [tbl_CommunityAdvisor] ";
        private const string Sql_CommunityAdvisor_Set = " UPDATE [tbl_CommunityAdvisor] SET [IsCheck] = @IsCheck,[SysOperatorId] = @SysOperatorId WHERE [ID] in (@ID) ";
        private const string Sql_CommunityAdvisor_SetIsShow = " UPDATE [tbl_CommunityAdvisor] SET [IsShow] = @IsShow,[SysOperatorId] = @SysOperatorId WHERE [ID] in (@ID) ";
        private const string Sql_CommunityAdvisor_Select = " SELECT {0} [ID],[ContactName],[Sex],[Age],[CompanyName],[ContactTel],[QQ],[Job],[ImgPath],[Achieve],[Honour],[IsCheck],[IsShow],[OperatorId],[SysOperatorId] FROM [tbl_CommunityAdvisor]  ";
        /// <summary>
        /// 删除前移动文件记录
        /// </summary>
        private const string SQL_DELETEDFILE_UPDATEMOVE = "IF (SELECT COUNT(*) FROM [tbl_CommunityAdvisor] WHERE [ID]=@ID AND [ImgPath]=@ImgPath)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [ImgPath] FROM [tbl_CommunityAdvisor] WHERE [ID]=@ID AND ImgPath<>'';";
        private const string SQL_DELETEDFILE_DELETEMOVE = "INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [ImgPath] FROM [tbl_CommunityAdvisor] WHERE ID in ({0}) AND ImgPath<>'';";

        #endregion

        #region ICommunityAdvisor 成员

        /// <summary>
        /// 添加顾问团队
        /// </summary>
        /// <param name="model">顾问团队实体</param>
        /// <returns>返回受影响行数</returns>
        public virtual int AddCommunityAdvisor(EyouSoft.Model.CommunityStructure.CommunityAdvisor model)
        {
            if (model == null)
                return 0;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_CommunityAdvisor_Add);

            #region 参数赋值

            base.SystemStore.AddInParameter(dc, "ContactName", DbType.String, model.ContactName);
            base.SystemStore.AddInParameter(dc, "Sex", DbType.String, model.Sex ? "1" : "0");
            base.SystemStore.AddInParameter(dc, "Age", DbType.Int32, model.Age);
            base.SystemStore.AddInParameter(dc, "CompanyName", DbType.String, model.CompanyName);
            base.SystemStore.AddInParameter(dc, "ContactTel", DbType.String, model.ContactTel);
            base.SystemStore.AddInParameter(dc, "QQ", DbType.String, model.QQ);
            base.SystemStore.AddInParameter(dc, "Job", DbType.String, model.Job);
            base.SystemStore.AddInParameter(dc, "ImgPath", DbType.String, model.ImgPath);
            base.SystemStore.AddInParameter(dc, "Achieve", DbType.String, model.Achieve);
            base.SystemStore.AddInParameter(dc, "Honour", DbType.String, model.Honour);
            base.SystemStore.AddInParameter(dc, "IsCheck", DbType.String, model.IsCheck ? "1" : "0");
            base.SystemStore.AddInParameter(dc, "IsShow", DbType.String, model.IsShow ? "1" : "0");
            base.SystemStore.AddInParameter(dc, "OperatorId", DbType.String, model.OperatorId);
            base.SystemStore.AddInParameter(dc, "SysOperatorId", DbType.Int32, model.SysOperatorId);

            #endregion

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 修改顾问团队
        /// </summary>
        /// <param name="model">顾问团队实体</param>
        /// <returns>返回受影响行数</returns>
        public virtual int UpdateCommunityAdvisor(EyouSoft.Model.CommunityStructure.CommunityAdvisor model)
        {
            if (model == null)
                return 0;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(SQL_DELETEDFILE_UPDATEMOVE + Sql_CommunityAdvisor_Update);

            #region 参数赋值

            base.SystemStore.AddInParameter(dc, "ContactName", DbType.String, model.ContactName);
            base.SystemStore.AddInParameter(dc, "Sex", DbType.String, model.Sex ? "1" : "0");
            base.SystemStore.AddInParameter(dc, "Age", DbType.Int32, model.Age);
            base.SystemStore.AddInParameter(dc, "CompanyName", DbType.String, model.CompanyName);
            base.SystemStore.AddInParameter(dc, "ContactTel", DbType.String, model.ContactTel);
            base.SystemStore.AddInParameter(dc, "QQ", DbType.String, model.QQ);
            base.SystemStore.AddInParameter(dc, "Job", DbType.String, model.Job);
            base.SystemStore.AddInParameter(dc, "ImgPath", DbType.String, model.ImgPath);
            base.SystemStore.AddInParameter(dc, "Achieve", DbType.String, model.Achieve);
            base.SystemStore.AddInParameter(dc, "Honour", DbType.String, model.Honour);
            base.SystemStore.AddInParameter(dc, "IsCheck", DbType.String, model.IsCheck ? "1" : "0");
            base.SystemStore.AddInParameter(dc, "OperatorId", DbType.String, model.OperatorId);
            base.SystemStore.AddInParameter(dc, "SysOperatorId", DbType.Int32, model.SysOperatorId);
            base.SystemStore.AddInParameter(dc, "ID", DbType.Int32, model.ID);

            #endregion

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 删除顾问团队
        /// </summary>
        /// <param name="AdvisorIds">顾问团队ID集合</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool DeleteCommunityAdvisor(string AdvisorIds)
        {
            if (string.IsNullOrEmpty(AdvisorIds))
                return false;

            string strWhere = SQL_DELETEDFILE_DELETEMOVE + Sql_CommunityAdvisor_Delete + " where ID in ({0}) ";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(string.Format(strWhere,AdvisorIds));

            return DbHelper.ExecuteSql(dc, base.SystemStore) > 0 ? true : false;
        }

        /// <summary>
        /// 审核顾问团队申请
        /// </summary>
        /// <param name="AdvisorIds">顾问团队ID集合</param>
        /// <param name="IsCheck">审核状态</param>
        /// <param name="SysOperatorId">后台操作用户ID</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool SetIsCheck(string AdvisorIds, bool IsCheck, int SysOperatorId)
        {
            if (string.IsNullOrEmpty(AdvisorIds) || SysOperatorId <= 0)
                return false;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_CommunityAdvisor_Set);
            base.SystemStore.AddInParameter(dc, "IsCheck", DbType.AnsiStringFixedLength, IsCheck ? "1" : "0");
            base.SystemStore.AddInParameter(dc, "SysOperatorId", DbType.Int32, SysOperatorId);
            base.SystemStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, AdvisorIds);
            return DbHelper.ExecuteSql(dc, base.SystemStore) > 0 ? true : false;
        }

        /// <summary>
        /// 设置顾问团队前台是否显示
        /// </summary>
        /// <param name="AdvisorIds">顾问团队ID集合</param>
        /// <param name="IsShow">前台是否显示</param>
        /// <param name="SysOperatorId">后台操作用户ID</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool SetIsShow(string AdvisorIds, bool IsShow, int SysOperatorId)
        {
            if (string.IsNullOrEmpty(AdvisorIds) || SysOperatorId <= 0)
                return false;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_CommunityAdvisor_SetIsShow);
            base.SystemStore.AddInParameter(dc, "IsShow", DbType.AnsiStringFixedLength, IsShow ? "1" : "0");
            base.SystemStore.AddInParameter(dc, "SysOperatorId", DbType.Int32, SysOperatorId);
            base.SystemStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, AdvisorIds);
            return DbHelper.ExecuteSql(dc, base.SystemStore) > 0 ? true : false;
        }

        /// <summary>
        /// 获取顾问团队
        /// </summary>
        /// <param name="TopNum">取得顾问团队的数量(小于等于0取所有)</param>
        /// <param name="IsShow">是否前台显示 =null返回全部</param>
        /// <returns>返回顾问团队实体集合</returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.CommunityAdvisor> GetCommunityAdvisorList(int TopNum,bool? IsShow)
        {
            IList<EyouSoft.Model.CommunityStructure.CommunityAdvisor> List = new List<EyouSoft.Model.CommunityStructure.CommunityAdvisor>();
            string StrSql = string.Format(Sql_CommunityAdvisor_Select, TopNum > 0 ? string.Format(" top {0}", TopNum) : string.Empty);
            if (IsShow.HasValue)
            {
                StrSql += string.Format(" where IsShow='{0}' ", IsShow.Value ? "1" : "0");
            }
            StrSql += " order by ID desc";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(StrSql);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                EyouSoft.Model.CommunityStructure.CommunityAdvisor model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.CommunityStructure.CommunityAdvisor();
                    if (!string.IsNullOrEmpty(dr["ID"].ToString()))
                        model.ID = int.Parse(dr["ID"].ToString());
                    model.ContactName = dr["ContactName"].ToString();
                    if (!string.IsNullOrEmpty(dr["Sex"].ToString()) && dr["Sex"].ToString().Equals("1"))
                        model.Sex = true;
                    if (!string.IsNullOrEmpty(dr["Age"].ToString()))
                        model.Age = int.Parse(dr["Age"].ToString());
                    model.CompanyName = dr["CompanyName"].ToString();
                    model.ContactTel = dr["ContactTel"].ToString();
                    model.QQ = dr["QQ"].ToString();
                    model.Job = dr["Job"].ToString();
                    model.ImgPath = dr["ImgPath"].ToString();
                    model.Achieve = dr["Achieve"].ToString();
                    model.Honour = dr["Honour"].ToString();
                    if (!string.IsNullOrEmpty(dr["IsCheck"].ToString()) && dr["IsCheck"].ToString().Equals("1"))
                        model.IsCheck = true;
                    model.OperatorId = dr["OperatorId"].ToString();
                    if (!string.IsNullOrEmpty(dr["SysOperatorId"].ToString()))
                        model.SysOperatorId = int.Parse(dr["SysOperatorId"].ToString());
                    if (!string.IsNullOrEmpty(dr["IsShow"].ToString()) && dr["IsShow"].ToString().Equals("1"))
                        model.IsShow = true;

                    List.Add(model);
                }
            }

            return List;
        }

        /// <summary>
        /// 分页获取顾问团队
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="IsShow">是否前台显示 =null返回全部</param>
        /// <returns>返回顾问团队实体集合</returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.CommunityAdvisor> GetCommunityAdvisorList(int PageSize, int PageIndex, ref int RecordCount,bool? IsShow)
        {
            IList<EyouSoft.Model.CommunityStructure.CommunityAdvisor> List = new List<EyouSoft.Model.CommunityStructure.CommunityAdvisor>();
            string strFiles = " [ID],[ContactName],[Sex],[Age],[CompanyName],[ContactTel],[QQ],[Job],[ImgPath],[Achieve],[Honour],[IsCheck],[IsShow],[OperatorId],[SysOperatorId] ";
            string strOrder = " ID desc ";
            string strWhere = string.Empty;
            if (IsShow.HasValue)
            {
                strWhere = string.Format(" IsShow='{0}' ",IsShow.Value?"1":"0");
            }
            using (IDataReader dr = DbHelper.ExecuteReader(base.SystemStore, PageSize, PageIndex, ref RecordCount, "tbl_CommunityAdvisor", "[ID]", strFiles, strWhere, strOrder))
            {
                EyouSoft.Model.CommunityStructure.CommunityAdvisor model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.CommunityStructure.CommunityAdvisor();
                    if (!string.IsNullOrEmpty(dr["ID"].ToString()))
                        model.ID = int.Parse(dr["ID"].ToString());
                    model.ContactName = dr["ContactName"].ToString();
                    if (!string.IsNullOrEmpty(dr["Sex"].ToString()) && dr["Sex"].ToString().Equals("1"))
                        model.Sex = true;
                    if (!string.IsNullOrEmpty(dr["Age"].ToString()))
                        model.Age = int.Parse(dr["Age"].ToString());
                    model.CompanyName = dr["CompanyName"].ToString();
                    model.ContactTel = dr["ContactTel"].ToString();
                    model.QQ = dr["QQ"].ToString();
                    model.Job = dr["Job"].ToString();
                    model.ImgPath = dr["ImgPath"].ToString();
                    model.Achieve = dr["Achieve"].ToString();
                    model.Honour = dr["Honour"].ToString();
                    if (!string.IsNullOrEmpty(dr["IsCheck"].ToString()) && dr["IsCheck"].ToString().Equals("1"))
                        model.IsCheck = true;
                    model.OperatorId = dr["OperatorId"].ToString();
                    if (!string.IsNullOrEmpty(dr["SysOperatorId"].ToString()))
                        model.SysOperatorId = int.Parse(dr["SysOperatorId"].ToString());
                    if (!string.IsNullOrEmpty(dr["IsShow"].ToString()) && dr["IsShow"].ToString().Equals("1"))
                        model.IsShow = true;

                    List.Add(model);
                }
            }

            return List;
        }

        /// <summary>
        /// 获取顾问团队实体
        /// </summary>
        /// <param name="CommunityAdvisorId">顾问团队</param>
        /// <returns>获取顾问团队实体</returns>
        public virtual Model.CommunityStructure.CommunityAdvisor GetCommunityAdvisor(int CommunityAdvisorId)
        {
            if (CommunityAdvisorId <= 0)
                return null;

            EyouSoft.Model.CommunityStructure.CommunityAdvisor model = new EyouSoft.Model.CommunityStructure.CommunityAdvisor();
            string strSql = string.Format(" SELECT [ID],[ContactName],[Sex],[Age],[CompanyName],[ContactTel],[QQ],[Job],[ImgPath],[Achieve],[Honour],[IsCheck],[IsShow],[OperatorId],[SysOperatorId] FROM [tbl_CommunityAdvisor] where ID = {0} ", CommunityAdvisorId);
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strSql);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.CommunityStructure.CommunityAdvisor();
                    if (!string.IsNullOrEmpty(dr["ID"].ToString()))
                        model.ID = int.Parse(dr["ID"].ToString());
                    model.ContactName = dr["ContactName"].ToString();
                    if (!string.IsNullOrEmpty(dr["Sex"].ToString()) && dr["Sex"].ToString().Equals("1"))
                        model.Sex = true;
                    if (!string.IsNullOrEmpty(dr["Age"].ToString()))
                        model.Age = int.Parse(dr["Age"].ToString());
                    model.CompanyName = dr["CompanyName"].ToString();
                    model.ContactTel = dr["ContactTel"].ToString();
                    model.QQ = dr["QQ"].ToString();
                    model.Job = dr["Job"].ToString();
                    model.ImgPath = dr["ImgPath"].ToString();
                    model.Achieve = dr["Achieve"].ToString();
                    model.Honour = dr["Honour"].ToString();
                    if (!string.IsNullOrEmpty(dr["IsCheck"].ToString()) && dr["IsCheck"].ToString().Equals("1"))
                        model.IsCheck = true;
                    model.OperatorId = dr["OperatorId"].ToString();
                    if (!string.IsNullOrEmpty(dr["SysOperatorId"].ToString()))
                        model.SysOperatorId = int.Parse(dr["SysOperatorId"].ToString());
                    if (!string.IsNullOrEmpty(dr["IsShow"].ToString()) && dr["IsShow"].ToString().Equals("1"))
                        model.IsShow = true;
                }
            }
            return model;
        }

        #endregion
    }
}
