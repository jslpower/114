using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL.CompanyStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.CompanyStructure
{
    /// <summary>
    /// 创建人：张志瑜 2010-05-27
    /// 描述：公司价格等级数据层
    /// </summary>
    public class CompanyPriceStand : DALBase, ICompanyPriceStand
    {
        #region SQL定义
        private const string SQL_CompanyPriceStand_SELECT_List = "SELECT id,CommonPriceStandID,PriceStandName,CompanyID,IsSystem FROM tbl_CompanyPriceStand WHERE CompanyID=@CompanyID";
        #endregion

        /// <summary>
        /// 所在的数据库
        /// </summary>
        protected Database _database = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        public CompanyPriceStand()
        {
            this._database = base.CompanyStore;
        }

        /// <summary>
        /// 获取指定公司的价格等级列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.CompanyPriceStand> GetList(string companyId)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyPriceStand_SELECT_List);
            this._database.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, companyId);
            IList<EyouSoft.Model.CompanyStructure.CompanyPriceStand> list = new List<EyouSoft.Model.CompanyStructure.CompanyPriceStand>();
            using (IDataReader dr = DbHelper.ExecuteReader(dc,this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CompanyStructure.CompanyPriceStand model = new EyouSoft.Model.CompanyStructure.CompanyPriceStand();
                    model.ID = dr.GetString(dr.GetOrdinal("ID"));
                    model.CommonPriceStandID = dr.GetString(dr.GetOrdinal("CommonPriceStandId"));
                    model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    model.IsSystem = dr.GetString(dr.GetOrdinal("IsSystem")) == "1" ? true : false;
                    model.PriceStandName = dr.GetString(dr.GetOrdinal("PriceStandName"));
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 设置公司价格等级信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="items">公用价格等级(系统基础数据)[设置公共价格等级ID和等级名称即可]信息业务实体集合</param>
        /// <returns></returns>
        public virtual bool SetCompanyPriceStand(string companyId, IList<EyouSoft.Model.CompanyStructure.CommonPriceStand> items)
        {
            DbCommand dc = this._database.GetStoredProcCommand("proc_CompanyPriceStand_Update");
            this._database.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, companyId);
            StringBuilder XMLRoot = new StringBuilder("<ROOT>");
            if (items != null && items.Count > 0)
            {
                foreach (EyouSoft.Model.CompanyStructure.CommonPriceStand model in items)
                {
                    XMLRoot.AppendFormat("<CommPriceInfo ID=\"{0}\" CommonPriceStandID=\"{1}\" PriceStandName=\"{2}\" />", Guid.NewGuid().ToString(), model.ID, EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(model.PriceStandName));
                }
            }
            XMLRoot.Append("</ROOT>");
            this._database.AddInParameter(dc, "XMLRoot", DbType.String, XMLRoot.ToString());
            int rows = DbHelper.RunProcedureWithResult(dc, this._database);
            if (rows > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 判断指定公司,指定公共的价格等级ID有无在(团队/线路价格等级)使用(true:在使用  false:未使用)
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="commonPriceStandId">公共价格等级ID</param>
        /// <returns></returns>
        public virtual bool IsUsing(string companyId, string commonPriceStandId)
        {
            DbCommand dc = this._database.GetStoredProcCommand("proc_CompanyPriceStand_Select_IsUsing");
            this._database.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, companyId);
            this._database.AddInParameter(dc, "CommonPriceStandId", DbType.AnsiStringFixedLength, commonPriceStandId);            
            int rows = DbHelper.RunProcedureWithResult(dc, this._database);
            if (rows > 0)
                return true;
            else
                return false;
        }
    }
}
