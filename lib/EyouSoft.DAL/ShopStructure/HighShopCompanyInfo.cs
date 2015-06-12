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
    /// 描述：高级网店详细信息数据层
    /// </summary>
    public class HighShopCompanyInfo:DALBase,IHighShopCompanyInfo
    {
        #region 构造函数
        /// <summary>
        /// 当前所在数据库
        /// </summary>
        private Database _database = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public HighShopCompanyInfo() 
        {
            this._database = base.CompanyStore;
        }
        #endregion

        #region SQL定义
        const string SQL_HighShopCompanyInfo_SETABOUTUS = "if exists(select 1 from tbl_HighShopCompanyInfo where CompanyID=@CompanyID) begin	update tbl_HighShopCompanyInfo set CompanyInfo=@CompanyInfo where CompanyID=@CompanyID end else begin	insert into tbl_HighShopCompanyInfo(CompanyID,CompanyInfo) values(@CompanyID,@CompanyInfo) end";
        const string SQL_HighShopCompanyInfo_SELECTBYID = "SELECT CompanyID,CompanyInfo,TemplateId,ShopCopyRight,GoogleMapKey from tbl_HighShopCompanyInfo where CompanyID=@CompanyID";
        const string SQL_HighShopCompanyInfo_SETCOPYRIGHT = "if exists(select 1 from tbl_HighShopCompanyInfo where CompanyID=@CompanyID) begin	update tbl_HighShopCompanyInfo set ShopCopyRight=@ShopCopyRight where CompanyID=@CompanyID end else begin	insert into tbl_HighShopCompanyInfo(CompanyID,ShopCopyRight) values(@CompanyID,@ShopCopyRight) end";
        //const string SQL_HighShopCompanyInfo_SETCARDLINK = "if exists(select 1 from tbl_HighShopCompanyInfo where CompanyID=@ompanyID) begin	update tbl_HighShopCompanyInfo set CompanyCardLink=@CompanyCardLink where CompanyID=@CompanyID end else begin	insert into tbl_HighShopCompanyInfo(CompanyID,CompanyCardLink) values(@CompanyID,@CompanyCardLink) end";
        //const string SQL_HighShopCompanyInfo_SETLOGO = "if exists(select 1 from tbl_CompanyAttachInfo where CompanyID=@CompanyID) begin	update tbl_CompanyAttachInfo set CompanyLogo=@CompanyLogo where CompanyID=@CompanyID end else begin	insert into tbl_CompanyAttachInfo(CompanyID,CompanyLogo) values(@CompanyID,@CompanyLogo) end";
        const string SQL_HighShopCompanyInfo_SETTEMPLATE = "if exists(select 1 from tbl_HighShopCompanyInfo where CompanyID=@CompanyID) begin	update tbl_HighShopCompanyInfo set TemplateId=@TemplateID where CompanyID=@CompanyID end else begin	insert into tbl_HighShopCompanyInfo(CompanyID,TemplateId) values(@CompanyID,@TemplateID) end";
        #endregion

        #region IHighShopCompanyInfo成员
        /// <summary>
        /// 设置关于我们
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="AboutText">关于我们内容</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool SetAboutUs(string CompanyID, string AboutText)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopCompanyInfo_SETABOUTUS);
            this._database.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, CompanyID);
            this._database.AddInParameter(dc, "CompanyInfo", DbType.String, AboutText);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 获取高级网店的详细信息
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <returns>高级网店的详细信息实体</returns>
        public virtual EyouSoft.Model.ShopStructure.HighShopCompanyInfo GetModel(string CompanyID)
        {
            EyouSoft.Model.ShopStructure.HighShopCompanyInfo model = null;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopCompanyInfo_SELECTBYID);
            this._database.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, CompanyID);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.ShopStructure.HighShopCompanyInfo();
                    model.CompanyID = dr.GetString(0);
                    if (!dr.IsDBNull(1))
                        model.CompanyInfo = dr.GetString(1);
                    else
                        model.CompanyInfo = "";
                    if (!dr.IsDBNull(2))
                        model.TemplateId = int.Parse(dr[2].ToString());
                    if (!dr.IsDBNull(3))
                        model.ShopCopyRight = dr.GetString(3);
                    else
                        model.ShopCopyRight = "";

                    model.GoogleMapKey = dr["GoogleMapKey"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 设置版权
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="CopyRightText">版权内容</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool SetCopyRight(string CompanyID, string CopyRightText)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopCompanyInfo_SETCOPYRIGHT);
            this._database.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, CompanyID);
            this._database.AddInParameter(dc, "ShopCopyRight", DbType.String, CopyRightText);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /*
        /// <summary>
        /// 设置名片
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="CardLink">名片链接地址</param>
        /// <returns>0:失败 1:成功</returns>
        public virtual int SetCardLink(string CompanyID, string CardLink)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopCompanyInfo_SETCARDLINK);
            this._database.AddInParameter(dc, "CompanyID", DbType.String, CompanyID);
            this._database.AddInParameter(dc, "CompanyCardLink", DbType.String, CardLink);
            return DbHelper.ExecuteSql(dc, this._database);
        }
        /// <summary>
        /// 设置LOGO
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="Logo">LOGO地址</param>
        /// <returns>0:失败 1:成功</returns>
        public virtual int SetLogo(string CompanyID, string Logo)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopCompanyInfo_SETLOGO);
            this._database.AddInParameter(dc, "CompanyID", DbType.String, CompanyID);
            this._database.AddInParameter(dc, "CompanyLogo", DbType.String, Logo);
            return DbHelper.ExecuteSql(dc, this._database);
        }*/
        /// <summary>
        /// 设置自定义模板
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="TemplateID">模板编号</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool SetTemplate(string CompanyID, int TemplateID)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_HighShopCompanyInfo_SETTEMPLATE);
            this._database.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, CompanyID);
            this._database.AddInParameter(dc, "TemplateID", DbType.Byte, Convert.ToByte(TemplateID));
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;  
        }
        #endregion
    }
}
