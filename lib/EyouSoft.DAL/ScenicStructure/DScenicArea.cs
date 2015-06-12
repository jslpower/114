using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Model.ScenicStructure;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Xml;
using System.Xml.Linq;
using EyouSoft.Common;
using EyouSoft.Model.SystemStructure;
using EyouSoft.Model.CompanyStructure;

namespace EyouSoft.DAL.ScenicStructure
{
    /// <summary>
    /// 景区
    /// 创建者：郑付杰
    /// 创建时间：2011/10/27
    /// </summary>
    public class DScenicArea : DALBase, EyouSoft.IDAL.ScenicStructure.IScenicArea
    {
        private readonly Database _db = null;

        public DScenicArea()
        {
            this._db = base.SystemStore;
        }

        #region IScenicArea 成员
        /// <summary>
        /// 添加景区
        /// </summary>
        /// <param name="item">景区实体</param>
        /// <returns></returns>
        public virtual bool Add(MScenicArea item)
        {
            DbCommand comm = this._db.GetStoredProcCommand("proc_ScenicArea_Add");
            #region xml
            //景区主题
            StringBuilder theme = new StringBuilder();
            if (item.ThemeId != null && item.ThemeId.Count > 0)
            {
                theme.Append("<item>");
                foreach (MScenicTheme i in item.ThemeId)
                {
                    theme.AppendFormat("<themeid>{0}</themeid>", i.ThemeId);
                }
                theme.Append("</item>");
            }
            //景区地标
            StringBuilder landmark = new StringBuilder();
            if (item.LankId != null && item.LankId.Count > 0)
            {
                landmark.Append("<item>");
                foreach (MScenicRelationLandMark obj in item.LankId)
                {
                    landmark.AppendFormat("<id>{0}</id>", obj.LandMarkId);
                }
                landmark.Append("</item>");
            }
            #endregion
            this._db.AddInParameter(comm, "@ScenicId", DbType.AnsiStringFixedLength, item.ScenicId);
            this._db.AddInParameter(comm, "@ScenicName", DbType.String, item.ScenicName);
            this._db.AddInParameter(comm, "@EnName", DbType.AnsiString, item.EnName);
            this._db.AddInParameter(comm, "@Telephone", DbType.AnsiString, item.Telephone);
            this._db.AddInParameter(comm, "@SetYear", DbType.Int32, item.SetYear);
            this._db.AddInParameter(comm, "@X", DbType.AnsiString, item.X);
            this._db.AddInParameter(comm, "@Y", DbType.AnsiString, item.Y);
            this._db.AddInParameter(comm, "@ProvinceId", DbType.Int32, item.ProvinceId);
            this._db.AddInParameter(comm, "@CityId", DbType.Int32, item.CityId);
            this._db.AddInParameter(comm, "@CountyId", DbType.Int32, item.CountyId);
            this._db.AddInParameter(comm, "@LankId", DbType.Xml, landmark.ToString());
            this._db.AddInParameter(comm, "@CnAddress", DbType.String, item.CnAddress);
            this._db.AddInParameter(comm, "@EnAddress", DbType.AnsiString, item.EnAddress);
            this._db.AddInParameter(comm, "@ThemeId", DbType.Xml, theme.ToString());
            this._db.AddInParameter(comm, "@ScenicLevel", DbType.Byte, (int)item.ScenicLevel);
            this._db.AddInParameter(comm, "@OpenTime", DbType.String, item.OpenTime);
            this._db.AddInParameter(comm, "@Description", DbType.String, item.Description);
            this._db.AddInParameter(comm, "@Traffic", DbType.String, item.Traffic);
            this._db.AddInParameter(comm, "@Facilities", DbType.String, item.Facilities);
            this._db.AddInParameter(comm, "@Notes", DbType.String, item.Notes);
            this._db.AddInParameter(comm, "@B2B", DbType.Byte, (int)item.B2B);
            this._db.AddInParameter(comm, "@B2BOrder", DbType.Int32, item.B2BOrder);
            this._db.AddInParameter(comm, "@B2C", DbType.Byte, (int)item.B2C);
            this._db.AddInParameter(comm, "@B2COrder", DbType.Int32, item.B2COrder);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.Company.ID);
            this._db.AddInParameter(comm, "@Operator", DbType.AnsiStringFixedLength, item.Operator);
            this._db.AddInParameter(comm, "@Status", DbType.Byte, (int)item.Status);
            this._db.AddInParameter(comm, "@ContactOperator", DbType.AnsiStringFixedLength, item.ContactOperator);
            if (item.Status == ExamineStatus.已审核)
            {
                this._db.AddInParameter(comm, "@ExamineOperator", DbType.Int32, item.ExamineOperator);
            }
            return DbHelper.ExecuteSql(comm, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 修改景区
        /// </summary>
        /// <param name="item">景区实体</param>
        /// <returns></returns>
        public virtual bool Update(MScenicArea item)
        {
            #region xml
            //景区主题
            StringBuilder theme = new StringBuilder();
            if (item.ThemeId != null && item.ThemeId.Count > 0)
            {
                theme.Append("<item>");
                foreach (MScenicTheme i in item.ThemeId)
                {
                    theme.AppendFormat("<themeid>{0}</themeid>", i.ThemeId);
                }
                theme.Append("</item>");
            }
            //景区地标
            StringBuilder landmark = new StringBuilder();
            if (item.LankId != null && item.LankId.Count > 0)
            {
                landmark.Append("<item>");
                foreach (MScenicRelationLandMark obj in item.LankId)
                {
                    landmark.AppendFormat("<id>{0}</id>", obj.LandMarkId);
                }
                landmark.Append("</item>");
            }
            #endregion

            DbCommand comm = this._db.GetStoredProcCommand("proc_ScenicArea_Update");

            this._db.AddInParameter(comm, "@ScenicName", DbType.String, item.ScenicName);
            this._db.AddInParameter(comm, "@EnName", DbType.AnsiString, item.EnName);
            this._db.AddInParameter(comm, "@Telephone", DbType.AnsiString, item.Telephone);
            this._db.AddInParameter(comm, "@SetYear", DbType.Int32, item.SetYear);
            this._db.AddInParameter(comm, "@X", DbType.AnsiString, item.X);
            this._db.AddInParameter(comm, "@Y", DbType.AnsiString, item.Y);
            this._db.AddInParameter(comm, "@ProvinceId", DbType.Int32, item.ProvinceId);
            this._db.AddInParameter(comm, "@CityId", DbType.Int32, item.CityId);
            this._db.AddInParameter(comm, "@CountyId", DbType.Int32, item.CountyId);
            this._db.AddInParameter(comm, "@LankId", DbType.Xml, landmark.ToString());
            this._db.AddInParameter(comm, "@CnAddress", DbType.String, item.CnAddress);
            this._db.AddInParameter(comm, "@EnAddress", DbType.AnsiString, item.EnAddress);
            this._db.AddInParameter(comm, "@ThemeId", DbType.Xml, theme.ToString());
            this._db.AddInParameter(comm, "@ScenicLevel", DbType.Byte, (int)item.ScenicLevel);
            this._db.AddInParameter(comm, "@OpenTime", DbType.String, item.OpenTime);
            this._db.AddInParameter(comm, "@Description", DbType.String, item.Description);
            this._db.AddInParameter(comm, "@Traffic", DbType.String, item.Traffic);
            this._db.AddInParameter(comm, "@Facilities", DbType.String, item.Facilities);
            this._db.AddInParameter(comm, "@Notes", DbType.String, item.Notes);
            this._db.AddInParameter(comm, "@B2B", DbType.Byte, (int)item.B2B);
            this._db.AddInParameter(comm, "@B2BOrder", DbType.Int32, item.B2BOrder);
            this._db.AddInParameter(comm, "@B2C", DbType.Byte, (int)item.B2C);
            this._db.AddInParameter(comm, "@B2COrder", DbType.Int32, item.B2COrder);
            this._db.AddInParameter(comm, "@ScenicId", DbType.AnsiStringFixedLength, item.ScenicId);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.Company.ID);
            this._db.AddInParameter(comm, "@ContactOperator", DbType.AnsiStringFixedLength, item.ContactOperator);
            this._db.AddInParameter(comm, "@StatusValue", DbType.Byte, (int)ExamineStatus.已审核);
            this._db.AddInParameter(comm, "@Status", DbType.Byte, (int)item.Status);

            return DbHelper.ExecuteSql(comm, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 修改景区审核状态
        /// </summary>
        /// <param name="scenicId">景区编号</param>
        /// <param name="operatorId">审核用户编号</param>
        /// <param name="es">状态</param>
        /// <returns></returns>
        public virtual bool UpdateExamineStatus(string scenicId, int operatorId, ExamineStatus es)
        {
            string sql = string.Format("UPDATE tbl_ScenicArea SET Status = @Status,ExamineOperator = @ID,LastUpdateTime=getdate() WHERE charindex(','+ScenicId+',',',{0},') > 0", scenicId);
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@Status", DbType.Byte, (int)es);
            this._db.AddInParameter(comm, "@ID", DbType.Int32, operatorId);

            return DbHelper.ExecuteSql(comm, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 删除景区
        /// </summary>
        /// <param name="scenicId">景区编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>1：删除成功  3：删除失败</returns>
        public virtual int Delete(string scenicId, string companyId)
        {
            DbCommand comm = this._db.GetStoredProcCommand("proc_ScenicArea_Delete");
            this._db.AddInParameter(comm, "@scenicId", DbType.AnsiStringFixedLength, scenicId);
            this._db.AddInParameter(comm, "@companyId", DbType.AnsiStringFixedLength, companyId);

            int returnNum = DbHelper.RunProcedureWithResult(comm, this._db);

            return returnNum;
        }
        /// <summary>
        /// 增加点击量
        /// </summary>
        /// <param name="scenicId">景区编号</param>
        /// <returns></returns>
        public virtual bool UpdateClickNum(string scenicId)
        {
            string sql = "UPDATE tbl_ScenicArea SET ClickNum = ClickNum + 1 WHERE ScenicId = @ScenicId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@ScenicId", DbType.AnsiStringFixedLength, scenicId);
            return DbHelper.ExecuteSql(comm, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 增加点击量
        /// </summary>
        /// <param name="Id">景区自增编号</param>
        /// <returns></returns>
        public virtual bool UpdateClickNum(long Id)
        {
            string sql = "UPDATE tbl_ScenicArea SET ClickNum = ClickNum + 1 WHERE Id = @Id";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@Id", DbType.Int64, Id);
            return DbHelper.ExecuteSql(comm, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 获取景区实体
        /// </summary>
        /// <param name="scenicId">景区编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>景区实体</returns>
        public virtual MScenicArea GetModel(string scenicId, string companyId)
        {
            #region sql
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT[ScenicId],[ScenicName],[EnName],[Telephone],[SetYear],[X],[Y],tb1.[ProvinceId],b.ProvinceName");
            sql.Append(",tb1.[CityId],c.CityName,[CountyId],d.DistrictName,[CnAddress],[EnAddress],[ScenicLevel],[OpenTime]");
            sql.Append(",[Description],[Traffic],[Facilities],[Notes],[B2B],[B2BOrder],[B2C],[B2COrder],[Status],ContactOperator");
            sql.Append(",tb1.[CompanyId],[Operator],[ClickNum],(select a.ThemeId,b.ThemeName from tbl_ScenicRelationTheme a inner join tbl_ScenicTheme b on a.ThemeId = b.ThemeId where a.ScenicId = tb1.ScenicId for xml raw,root('item')) theme");
            sql.Append(",(select ImgId,ImgType,Address,ThumbAddress,Description from tbl_ScenicImg where ScenicId = tb1.ScenicId for xml raw,root('item')) ScenicImg");
            sql.Append(",(select LandMarkId from tbl_ScenicRelationLandMark where ScenicId = tb1.ScenicId for xml raw,root('item')) landmark");
            sql.Append(",(select TypeName,isnull(cast(StartTime as varchar(50)),'') StartTime,isnull(cast(EndTime as varchar(50)),'') EndTime,RetailPrice from tbl_ScenicTickets");
            sql.AppendFormat(" where ScenicId = tb1.ScenicId and Status = {0} and ExamineStatus = {1}  for xml path,root('item')) Tickets", (int)ScenicTicketsStatus.上架, (int)ExamineStatus.已审核);
            sql.Append(" ,e.ContactName,e.ContactTel,ContactMobile");
            sql.Append(" FROM [tbl_ScenicArea] tb1 left join tbl_SysProvince b");
            sql.Append(" on tb1.ProvinceId = b.Id");
            sql.Append(" left join tbl_SysCity c");
            sql.Append(" on tb1.CityId = c.Id");
            sql.Append(" left join tbl_SysDistrictCounty d");
            sql.Append(" on tb1.CountyId = d.Id");
            sql.Append(" left join tbl_CompanyUser e");
            sql.Append(" on tb1.ContactOperator = e.Id");
            sql.Append(" WHERE tb1.ScenicId = @ScenicId AND tb1.CompanyId = @companyId ");
            #endregion
            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(comm, "@ScenicId", DbType.AnsiStringFixedLength, scenicId);
            this._db.AddInParameter(comm, "@companyId", DbType.AnsiStringFixedLength, companyId);
            string xmlStr = string.Empty;
            MScenicArea item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    #region
                    item = new MScenicArea()
                    {
                        ScenicId = reader["ScenicId"].ToString(),
                        ScenicName = reader["ScenicName"].ToString(),
                        EnName = reader.IsDBNull(reader.GetOrdinal("EnName")) ? string.Empty : reader["EnName"].ToString(),
                        Telephone = reader.IsDBNull(reader.GetOrdinal("Telephone")) ? string.Empty : reader["Telephone"].ToString(),
                        SetYear = reader.IsDBNull(reader.GetOrdinal("SetYear")) ? 0 : (int)reader["SetYear"],
                        X = reader.IsDBNull(reader.GetOrdinal("X")) ? string.Empty : reader["X"].ToString(),
                        Y = reader.IsDBNull(reader.GetOrdinal("Y")) ? string.Empty : reader["Y"].ToString(),
                        ProvinceId = (int)reader["ProvinceId"],
                        ProvinceName = reader.IsDBNull(reader.GetOrdinal("ProvinceName")) ? string.Empty : reader["ProvinceName"].ToString(),
                        CityId = (int)reader["CityId"],
                        CityName = reader.IsDBNull(reader.GetOrdinal("CityName")) ? string.Empty : reader["CityName"].ToString(),
                        ContactOperator = reader.IsDBNull(reader.GetOrdinal("ContactOperator")) ? string.Empty : reader["ContactOperator"].ToString(),
                        ContactName = reader.IsDBNull(reader.GetOrdinal("ContactName")) ? string.Empty : reader["ContactName"].ToString(),
                        ContactTel = reader.IsDBNull(reader.GetOrdinal("ContactTel")) ? string.Empty : reader["ContactTel"].ToString(),
                        ContactMobile = reader.IsDBNull(reader.GetOrdinal("ContactMobile")) ? string.Empty : reader["ContactMobile"].ToString(),
                        CountyId = (int)reader["CountyId"],
                        CountyName = reader.IsDBNull(reader.GetOrdinal("DistrictName")) ? string.Empty : reader["DistrictName"].ToString(),
                        CnAddress = reader.IsDBNull(reader.GetOrdinal("CnAddress")) ? string.Empty : reader["CnAddress"].ToString(),
                        EnAddress = reader.IsDBNull(reader.GetOrdinal("EnAddress")) ? string.Empty : reader["EnAddress"].ToString(),
                        ScenicLevel = (ScenicLevel)Enum.Parse(typeof(ScenicLevel), reader["ScenicLevel"].ToString()),
                        OpenTime = reader.IsDBNull(reader.GetOrdinal("OpenTime")) ? string.Empty : reader["OpenTime"].ToString(),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader["Description"].ToString(),
                        Traffic = reader.IsDBNull(reader.GetOrdinal("Traffic")) ? string.Empty : reader["Traffic"].ToString(),
                        Facilities = reader.IsDBNull(reader.GetOrdinal("Facilities")) ? string.Empty : reader["Facilities"].ToString(),
                        Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? string.Empty : reader["Notes"].ToString(),
                        B2B = (ScenicB2BDisplay)Enum.Parse(typeof(ScenicB2BDisplay), reader["B2B"].ToString()),
                        B2BOrder = (int)reader["B2BOrder"],
                        B2C = (ScenicB2CDisplay)Enum.Parse(typeof(ScenicB2CDisplay), reader["B2C"].ToString()),
                        B2COrder = (int)reader["B2COrder"],
                        Status = (ExamineStatus)Enum.Parse(typeof(ExamineStatus), reader["Status"].ToString()),
                        Company = new EyouSoft.Model.CompanyStructure.CompanyInfo()
                        {
                            ID = reader["CompanyId"].ToString()
                        },
                        Operator = reader["Operator"].ToString(),
                        ClickNum = (int)reader["ClickNum"]
                    };
                    #endregion
                    #region 景区主题，地标,门票,图片
                    if (!reader.IsDBNull(reader.GetOrdinal("ScenicImg")))
                    {
                        xmlStr = reader["ScenicImg"].ToString();
                        XElement xRoot = XElement.Parse(xmlStr);
                        var xRows = Utility.GetXElements(xRoot, "row");
                        item.Img = new List<MScenicImg>();
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                item.Img.Add(new MScenicImg()
                                {
                                    ImgId = Utility.GetXAttributeValue(t, "ImgId"),
                                    Address = Utility.GetXAttributeValue(t, "Address"),
                                    ThumbAddress = Utility.GetXAttributeValue(t, "ThumbAddress"),
                                    Description = Utility.GetXAttributeValue(t, "Description"),
                                    ImgType = (ScenicImgType)Enum.Parse(typeof(ScenicImgType), Utility.GetXAttributeValue(t, "ImgType"))
                                });
                            }
                        }
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Tickets")))
                    {
                        xmlStr = reader["Tickets"].ToString();
                        XElement xRoot = XElement.Parse(xmlStr);
                        var xRows = Utility.GetXElements(xRoot, "row");
                        item.TicketsList = new List<MScenicTickets>();
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                item.TicketsList.Add(new MScenicTickets()
                                {
                                    TypeName = Utility.GetXElement(t, "TypeName").Value,
                                    StartTime = Utility.GetDateTime(Utility.GetXElement(t, "StartTime").Value),
                                    EndTime = Utility.GetDateTime(Utility.GetXElement(t, "EndTime").Value),
                                    RetailPrice = Utility.GetDecimal(Utility.GetXElement(t, "RetailPrice").Value)
                                });
                            }
                        }
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("theme")))
                    {
                        xmlStr = reader["theme"].ToString();
                        XElement xRoot = XElement.Parse(xmlStr);
                        var xRows = Utility.GetXElements(xRoot, "row");
                        item.ThemeId = new List<MScenicTheme>();
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                item.ThemeId.Add(new MScenicTheme()
                                {
                                    ThemeId = Utility.GetInt(Utility.GetXAttributeValue(t, "ThemeId")),
                                    ThemeName = Utility.GetXAttributeValue(t, "ThemeName")
                                });
                            }
                        }
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("landmark")))
                    {
                        xmlStr = reader["landmark"].ToString();

                        XElement xRoot = XElement.Parse(xmlStr);
                        var xRows = Utility.GetXElements(xRoot, "row");
                        item.LankId = new List<MScenicRelationLandMark>();
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                item.LankId.Add(new MScenicRelationLandMark()
                                {
                                    ScenicId = item.ScenicId,
                                    LandMarkId = Utility.GetInt(Utility.GetXAttributeValue(t, "LandMarkId"))
                                });
                            }
                        }
                    }
                    #endregion
                }
            }
            return item;
        }

        /// <summary>
        /// 获取景区实体
        /// </summary>
        /// <param name="scenicId">景区编号</param>
        /// <param name="isTickets">TRUE:已审核的门票 false:所有门票</param>
        /// <returns>景区实体</returns>
        public virtual MScenicArea GetModel(string scenicId, bool isTickets)
        {
            #region sql
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT[ScenicId],[ScenicName],[EnName],[Telephone],[SetYear],[X],[Y],tb1.[ProvinceId],b.ProvinceName");
            sql.Append(",tb1.[CityId],c.CityName,[CountyId],d.DistrictName,[CnAddress],[EnAddress],[ScenicLevel],[OpenTime]");
            sql.Append(",[Description],[Traffic],[Facilities],[Notes],[B2B],[B2BOrder],[B2C],[B2COrder],[Status],ContactOperator");
            sql.Append(",tb1.[CompanyId],[Operator],[ClickNum],(select a.ThemeId,b.ThemeName from tbl_ScenicRelationTheme a inner join tbl_ScenicTheme b on a.ThemeId = b.ThemeId where a.ScenicId = tb1.ScenicId for xml raw,root('item')) theme");
            sql.Append(",(select ImgId,ImgType,Address,ThumbAddress,Description from tbl_ScenicImg where ScenicId = tb1.ScenicId for xml raw,root('item')) ScenicImg");
            sql.Append(",(select LandMarkId from tbl_ScenicRelationLandMark where ScenicId = tb1.ScenicId for xml raw,root('item')) landmark");
            sql.Append(",(select TypeName,isnull(cast(StartTime as varchar(50)),'') StartTime,isnull(cast(EndTime as varchar(50)),'') EndTime,RetailPrice from tbl_ScenicTickets where ScenicId = tb1.ScenicId");
            if (isTickets)
            {
                sql.AppendFormat(" and Status = {0} and ExamineStatus = {1}", (int)ScenicTicketsStatus.上架, (int)ExamineStatus.已审核);
            }
            sql.Append(" for xml path,root('item')) Tickets,e.ContactName,e.ContactTel,ContactMobile");
            sql.Append(" FROM [tbl_ScenicArea] tb1 left join tbl_SysProvince b");
            sql.Append(" on tb1.ProvinceId = b.Id");
            sql.Append(" left join tbl_SysCity c");
            sql.Append(" on tb1.CityId = c.Id");
            sql.Append(" left join tbl_SysDistrictCounty d");
            sql.Append(" on tb1.CountyId = d.Id");
            sql.Append(" left join tbl_CompanyUser e");
            sql.Append(" on tb1.ContactOperator = e.Id");
            sql.Append(" WHERE tb1.ScenicId = @ScenicId");
            #endregion
            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(comm, "@ScenicId", DbType.AnsiStringFixedLength, scenicId);
            string xmlStr = string.Empty;
            MScenicArea item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    #region
                    item = new MScenicArea()
                    {
                        ScenicId = reader["ScenicId"].ToString(),
                        ScenicName = reader["ScenicName"].ToString(),
                        EnName = reader.IsDBNull(reader.GetOrdinal("EnName")) ? string.Empty : reader["EnName"].ToString(),
                        Telephone = reader.IsDBNull(reader.GetOrdinal("Telephone")) ? string.Empty : reader["Telephone"].ToString(),
                        SetYear = reader.IsDBNull(reader.GetOrdinal("SetYear")) ? 0 : (int)reader["SetYear"],
                        X = reader.IsDBNull(reader.GetOrdinal("X")) ? string.Empty : reader["X"].ToString(),
                        Y = reader.IsDBNull(reader.GetOrdinal("Y")) ? string.Empty : reader["Y"].ToString(),
                        ProvinceId = (int)reader["ProvinceId"],
                        ProvinceName = reader.IsDBNull(reader.GetOrdinal("ProvinceName")) ? string.Empty : reader["ProvinceName"].ToString(),
                        CityId = (int)reader["CityId"],
                        CityName = reader.IsDBNull(reader.GetOrdinal("CityName")) ? string.Empty : reader["CityName"].ToString(),
                        ContactOperator = reader.IsDBNull(reader.GetOrdinal("ContactOperator")) ? string.Empty : reader["ContactOperator"].ToString(),
                        ContactName = reader.IsDBNull(reader.GetOrdinal("ContactName")) ? string.Empty : reader["ContactName"].ToString(),
                        ContactTel = reader.IsDBNull(reader.GetOrdinal("ContactTel")) ? string.Empty : reader["ContactTel"].ToString(),
                        ContactMobile = reader.IsDBNull(reader.GetOrdinal("ContactMobile")) ? string.Empty : reader["ContactMobile"].ToString(),
                        CountyId = (int)reader["CountyId"],
                        CountyName = reader.IsDBNull(reader.GetOrdinal("DistrictName")) ? string.Empty : reader["DistrictName"].ToString(),
                        CnAddress = reader.IsDBNull(reader.GetOrdinal("CnAddress")) ? string.Empty : reader["CnAddress"].ToString(),
                        EnAddress = reader.IsDBNull(reader.GetOrdinal("EnAddress")) ? string.Empty : reader["EnAddress"].ToString(),
                        ScenicLevel = (ScenicLevel)Enum.Parse(typeof(ScenicLevel), reader["ScenicLevel"].ToString()),
                        OpenTime = reader.IsDBNull(reader.GetOrdinal("OpenTime")) ? string.Empty : reader["OpenTime"].ToString(),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader["Description"].ToString(),
                        Traffic = reader.IsDBNull(reader.GetOrdinal("Traffic")) ? string.Empty : reader["Traffic"].ToString(),
                        Facilities = reader.IsDBNull(reader.GetOrdinal("Facilities")) ? string.Empty : reader["Facilities"].ToString(),
                        Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? string.Empty : reader["Notes"].ToString(),
                        B2B = (ScenicB2BDisplay)Enum.Parse(typeof(ScenicB2BDisplay), reader["B2B"].ToString()),
                        B2BOrder = (int)reader["B2BOrder"],
                        B2C = (ScenicB2CDisplay)Enum.Parse(typeof(ScenicB2CDisplay), reader["B2C"].ToString()),
                        B2COrder = (int)reader["B2COrder"],
                        Status = (ExamineStatus)Enum.Parse(typeof(ExamineStatus), reader["Status"].ToString()),
                        Company = new EyouSoft.Model.CompanyStructure.CompanyInfo()
                        {
                            ID = reader["CompanyId"].ToString()
                        },
                        Operator = reader["Operator"].ToString(),
                        ClickNum = (int)reader["ClickNum"]
                    };
                    #endregion
                    #region 景区主题，地标,门票,图片
                    if (!reader.IsDBNull(reader.GetOrdinal("ScenicImg")))
                    {
                        xmlStr = reader["ScenicImg"].ToString();
                        XElement xRoot = XElement.Parse(xmlStr);
                        var xRows = Utility.GetXElements(xRoot, "row");
                        item.Img = new List<MScenicImg>();
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                item.Img.Add(new MScenicImg()
                                {
                                    ImgId = Utility.GetXAttributeValue(t, "ImgId"),
                                    Address = Utility.GetXAttributeValue(t, "Address"),
                                    ThumbAddress = Utility.GetXAttributeValue(t, "ThumbAddress"),
                                    Description = Utility.GetXAttributeValue(t, "Description"),
                                    ImgType = (ScenicImgType)Enum.Parse(typeof(ScenicImgType), Utility.GetXAttributeValue(t, "ImgType"))
                                });
                            }
                        }
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Tickets")))
                    {
                        xmlStr = reader["Tickets"].ToString();
                        XElement xRoot = XElement.Parse(xmlStr);
                        var xRows = Utility.GetXElements(xRoot, "row");
                        item.TicketsList = new List<MScenicTickets>();
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                item.TicketsList.Add(new MScenicTickets()
                                {
                                    TypeName = Utility.GetXElement(t, "TypeName").Value,
                                    StartTime = Utility.GetDateTime(Utility.GetXElement(t, "StartTime").Value),
                                    EndTime = Utility.GetDateTime(Utility.GetXElement(t, "EndTime").Value),
                                    RetailPrice = Utility.GetDecimal(Utility.GetXElement(t, "RetailPrice").Value),
                                    ScenicName = item.ScenicName
                                });
                            }
                        }
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("theme")))
                    {
                        xmlStr = reader["theme"].ToString();
                        XElement xRoot = XElement.Parse(xmlStr);
                        var xRows = Utility.GetXElements(xRoot, "row");
                        item.ThemeId = new List<MScenicTheme>();
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                item.ThemeId.Add(new MScenicTheme()
                                {
                                    ThemeId = Utility.GetInt(Utility.GetXAttributeValue(t, "ThemeId")),
                                    ThemeName = Utility.GetXAttributeValue(t, "ThemeName")
                                });
                            }
                        }
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("landmark")))
                    {
                        xmlStr = reader["landmark"].ToString();

                        XElement xRoot = XElement.Parse(xmlStr);
                        var xRows = Utility.GetXElements(xRoot, "row");
                        item.LankId = new List<MScenicRelationLandMark>();
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                item.LankId.Add(new MScenicRelationLandMark()
                                {
                                    ScenicId = item.ScenicId,
                                    LandMarkId = Utility.GetInt(Utility.GetXAttributeValue(t, "LandMarkId"))
                                });
                            }
                        }
                    }
                    #endregion
                }
            }
            return item;
        }

        /// <summary>
        /// 获取景区实体
        /// </summary>
        /// <param name="Id">自增编号</param>
        /// <returns>景区实体</returns>
        public virtual MScenicArea GetModel(long Id)
        {
            #region sql
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT tb1.Id,[ScenicId],[ScenicName],[EnName],[Telephone],[SetYear],[X],[Y],tb1.[ProvinceId],b.ProvinceName");
            sql.Append(",tb1.[CityId],c.CityName,[CountyId],d.DistrictName,[CnAddress],[EnAddress],[ScenicLevel],[OpenTime]");
            sql.Append(",[Description],[Traffic],[Facilities],[Notes],[B2B],[B2BOrder],[B2C],[B2COrder],[Status],ContactOperator");
            sql.Append(",tb1.[CompanyId],[Operator],[ClickNum],(select a.ThemeId,b.ThemeName from tbl_ScenicRelationTheme a inner join tbl_ScenicTheme b on a.ThemeId = b.ThemeId where a.ScenicId = tb1.ScenicId for xml raw,root('item')) theme");
            sql.Append(",(select ImgId,ImgType,Address,ThumbAddress,Description from tbl_ScenicImg where ScenicId = tb1.ScenicId for xml raw,root('item')) ScenicImg");
            sql.Append(",(select LandMarkId from tbl_ScenicRelationLandMark where ScenicId = tb1.ScenicId for xml raw,root('item')) landmark");
            sql.Append(",(select TypeName,isnull(cast(StartTime as varchar(50)),'') StartTime,isnull(cast(EndTime as varchar(50)),'') EndTime,RetailPrice from tbl_ScenicTickets");
            sql.AppendFormat(" where ScenicId = tb1.ScenicId and Status = {0} and ExamineStatus = {1}  for xml path,root('item')) Tickets", (int)ScenicTicketsStatus.上架, (int)ExamineStatus.已审核);
            sql.Append(" ,e.ContactName,e.ContactTel,ContactMobile");
            sql.Append(" FROM [tbl_ScenicArea] tb1 left join tbl_SysProvince b");
            sql.Append(" on tb1.ProvinceId = b.Id");
            sql.Append(" left join tbl_SysCity c");
            sql.Append(" on tb1.CityId = c.Id");
            sql.Append(" left join tbl_SysDistrictCounty d");
            sql.Append(" on tb1.CountyId = d.Id");
            sql.Append(" left join tbl_CompanyUser e");
            sql.Append(" on tb1.ContactOperator = e.Id");
            sql.Append(" WHERE tb1.Id = @Id");
            #endregion
            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(comm, "@Id", DbType.Int64, Id);
            string xmlStr = string.Empty;
            MScenicArea item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    #region
                    item = new MScenicArea()
                    {
                        Id = long.Parse(reader["Id"].ToString()),
                        ScenicId = reader["ScenicId"].ToString(),
                        ScenicName = reader["ScenicName"].ToString(),
                        EnName = reader.IsDBNull(reader.GetOrdinal("EnName")) ? string.Empty : reader["EnName"].ToString(),
                        Telephone = reader.IsDBNull(reader.GetOrdinal("Telephone")) ? string.Empty : reader["Telephone"].ToString(),
                        SetYear = reader.IsDBNull(reader.GetOrdinal("SetYear")) ? 0 : (int)reader["SetYear"],
                        X = reader.IsDBNull(reader.GetOrdinal("X")) ? string.Empty : reader["X"].ToString(),
                        Y = reader.IsDBNull(reader.GetOrdinal("Y")) ? string.Empty : reader["Y"].ToString(),
                        ProvinceId = (int)reader["ProvinceId"],
                        ProvinceName = reader.IsDBNull(reader.GetOrdinal("ProvinceName")) ? string.Empty : reader["ProvinceName"].ToString(),
                        CityId = (int)reader["CityId"],
                        CityName = reader.IsDBNull(reader.GetOrdinal("CityName")) ? string.Empty : reader["CityName"].ToString(),
                        ContactOperator = reader.IsDBNull(reader.GetOrdinal("ContactOperator")) ? string.Empty : reader["ContactOperator"].ToString(),
                        ContactName = reader.IsDBNull(reader.GetOrdinal("ContactName")) ? string.Empty : reader["ContactName"].ToString(),
                        ContactTel = reader.IsDBNull(reader.GetOrdinal("ContactTel")) ? string.Empty : reader["ContactTel"].ToString(),
                        ContactMobile = reader.IsDBNull(reader.GetOrdinal("ContactMobile")) ? string.Empty : reader["ContactMobile"].ToString(),
                        CountyId = (int)reader["CountyId"],
                        CountyName = reader.IsDBNull(reader.GetOrdinal("DistrictName")) ? string.Empty : reader["DistrictName"].ToString(),
                        CnAddress = reader.IsDBNull(reader.GetOrdinal("CnAddress")) ? string.Empty : reader["CnAddress"].ToString(),
                        EnAddress = reader.IsDBNull(reader.GetOrdinal("EnAddress")) ? string.Empty : reader["EnAddress"].ToString(),
                        ScenicLevel = (ScenicLevel)Enum.Parse(typeof(ScenicLevel), reader["ScenicLevel"].ToString()),
                        OpenTime = reader.IsDBNull(reader.GetOrdinal("OpenTime")) ? string.Empty : reader["OpenTime"].ToString(),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader["Description"].ToString(),
                        Traffic = reader.IsDBNull(reader.GetOrdinal("Traffic")) ? string.Empty : reader["Traffic"].ToString(),
                        Facilities = reader.IsDBNull(reader.GetOrdinal("Facilities")) ? string.Empty : reader["Facilities"].ToString(),
                        Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? string.Empty : reader["Notes"].ToString(),
                        B2B = (ScenicB2BDisplay)Enum.Parse(typeof(ScenicB2BDisplay), reader["B2B"].ToString()),
                        B2BOrder = (int)reader["B2BOrder"],
                        B2C = (ScenicB2CDisplay)Enum.Parse(typeof(ScenicB2CDisplay), reader["B2C"].ToString()),
                        B2COrder = (int)reader["B2COrder"],
                        Status = (ExamineStatus)Enum.Parse(typeof(ExamineStatus), reader["Status"].ToString()),
                        Company = new EyouSoft.Model.CompanyStructure.CompanyInfo()
                        {
                            ID = reader["CompanyId"].ToString()
                        },
                        Operator = reader["Operator"].ToString(),
                        ClickNum = (int)reader["ClickNum"]
                    };
                    #endregion
                    #region 景区主题，地标,门票,图片
                    if (!reader.IsDBNull(reader.GetOrdinal("ScenicImg")))
                    {
                        xmlStr = reader["ScenicImg"].ToString();
                        XElement xRoot = XElement.Parse(xmlStr);
                        var xRows = Utility.GetXElements(xRoot, "row");
                        item.Img = new List<MScenicImg>();
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                item.Img.Add(new MScenicImg()
                                {
                                    ImgId = Utility.GetXAttributeValue(t, "ImgId"),
                                    Address = Utility.GetXAttributeValue(t, "Address"),
                                    ThumbAddress = Utility.GetXAttributeValue(t, "ThumbAddress"),
                                    Description = Utility.GetXAttributeValue(t, "Description"),
                                    ImgType = (ScenicImgType)Enum.Parse(typeof(ScenicImgType), Utility.GetXAttributeValue(t, "ImgType"))
                                });
                            }
                        }
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Tickets")))
                    {
                        xmlStr = reader["Tickets"].ToString();
                        XElement xRoot = XElement.Parse(xmlStr);
                        var xRows = Utility.GetXElements(xRoot, "row");
                        item.TicketsList = new List<MScenicTickets>();
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                item.TicketsList.Add(new MScenicTickets()
                                {
                                    TypeName = Utility.GetXElement(t, "TypeName").Value,
                                    StartTime = Utility.GetDateTime(Utility.GetXElement(t, "StartTime").Value),
                                    EndTime = Utility.GetDateTime(Utility.GetXElement(t, "EndTime").Value),
                                    RetailPrice = Utility.GetDecimal(Utility.GetXElement(t, "RetailPrice").Value),
                                    ScenicName = item.ScenicName
                                });
                            }
                        }
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("theme")))
                    {
                        xmlStr = reader["theme"].ToString();
                        XElement xRoot = XElement.Parse(xmlStr);
                        var xRows = Utility.GetXElements(xRoot, "row");
                        item.ThemeId = new List<MScenicTheme>();
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                item.ThemeId.Add(new MScenicTheme()
                                {
                                    ThemeId = Utility.GetInt(Utility.GetXAttributeValue(t, "ThemeId")),
                                    ThemeName = Utility.GetXAttributeValue(t, "ThemeName")
                                });
                            }
                        }
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("landmark")))
                    {
                        xmlStr = reader["landmark"].ToString();

                        XElement xRoot = XElement.Parse(xmlStr);
                        var xRows = Utility.GetXElements(xRoot, "row");
                        item.LankId = new List<MScenicRelationLandMark>();
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                item.LankId.Add(new MScenicRelationLandMark()
                                {
                                    ScenicId = item.ScenicId,
                                    LandMarkId = Utility.GetInt(Utility.GetXAttributeValue(t, "LandMarkId"))
                                });
                            }
                        }
                    }
                    #endregion
                }
            }
            return item;
        }

        /// <summary>
        /// 获取公司所有景区
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual IList<MScenicArea> GetList(string companyId)
        {
            string sql = "select ScenicId,ScenicName from tbl_ScenicArea where CompanyId = @companyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@companyId", DbType.AnsiStringFixedLength, companyId);
            IList<MScenicArea> list = new List<MScenicArea>();
            MScenicArea item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MScenicArea()
                    {
                        ScenicId = reader["ScenicId"].ToString(),
                        ScenicName = reader["ScenicName"].ToString()
                    });
                }
            }

            return list;
        }

        /// <summary>
        /// 景区列表(网店)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual IList<MScenicArea> GetList(int pageSize, int pageIndex, ref int recordCount, string companyId)
        {
            string tableName = "view_ScenicArea_SeniorOnlineShop_Select";
            string primaryKey = "ScenicId";
            string fields = "Id,ScenicId,ScenicName,CompanyId,Description,ScenicImg";
            string orderBy = "B2B ASC,B2BOrder ASC,LastUpdateTime DESC";
            StringBuilder query = new StringBuilder();
            //网店的景区不管状态是否审核
            query.AppendFormat(" B2B <> {0}", (int)ScenicB2BDisplay.隐藏);
            query.AppendFormat(" and CompanyId = '{0}'", companyId);

            IList<MScenicArea> list = new List<MScenicArea>();
            MScenicArea item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount,
                tableName, primaryKey, fields, query.ToString(), orderBy))
            {
                while (reader.Read())
                {
                    item = new MScenicArea()
                    {
                        Id = long.Parse(reader["Id"].ToString()),
                        ScenicId = reader["ScenicId"].ToString(),
                        ScenicName = reader["ScenicName"].ToString(),
                        Company = new EyouSoft.Model.CompanyStructure.CompanyInfo()
                        {
                            ID = reader["CompanyId"].ToString()
                        },
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader["Description"].ToString()
                    };
                    #region 景区图片
                    if (!reader.IsDBNull(reader.GetOrdinal("ScenicImg")))
                    {
                        string xml = reader["ScenicImg"].ToString();

                        XElement xRoot = XElement.Parse(xml);
                        var xRow = Utility.GetXElements(xRoot, "row");
                        item.Img = new List<MScenicImg>();
                        if (xRow != null && xRow.Any())
                        {
                            foreach (var t in xRow)
                            {
                                item.Img.Add(new MScenicImg()
                                {
                                    Address = Utility.GetXAttributeValue(t, "Address"),
                                    Description = Utility.GetXAttributeValue(t, "Description"),
                                    ImgType = (ScenicImgType)Enum.Parse(typeof(ScenicImgType), Utility.GetXAttributeValue(t, "ImgType")),
                                    ThumbAddress = Utility.GetXAttributeValue(t, "ThumbAddress"),
                                    ScenicId = item.ScenicId,
                                    ImgId = Utility.GetXAttributeValue(t, "ImgId")
                                });
                            }
                        }

                    }
                    list.Add(item);
                    #endregion
                }
            }
            return list;
        }

        /// <summary>
        /// 景区列表
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">查询条件</param>
        /// <returns>景区集合</returns>
        public virtual IList<MScenicArea> GetList(int pageSize, int pageIndex, ref int recordCount,
            MSearchSceniceArea search)
        {
            string tableName = "view_ScenicArea_SelectA";
            string fileds = "ScenicId,ScenicName,ProvinceId,Description,ScenicLevel,ProvinceName,CityId,CityName,CountyId,DistrictName,CompanyId,CompanyName,ContactName,ContactTel,ContactMobile,ContactFax,ContactQQ,Status,B2B,B2C,ClickNum,LastUpdateTime,ContactOperator,ContactOperatorName";
            string primaryKey = "ScenicId";
            string orderBy = "Status ASC,LastUpdateTime DESC";
            StringBuilder query = new StringBuilder();

            query.AppendFormat(" 1 = 1 {0}", SqlQueryStr(search));

            IList<MScenicArea> list = new List<MScenicArea>();
            MScenicArea item = null;

            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount,
                tableName, primaryKey, fileds, query.ToString(), orderBy))
            {
                while (reader.Read())
                {
                    list.Add(item = new MScenicArea()
                    {
                        ScenicId = reader["ScenicId"].ToString(),
                        ScenicName = reader["ScenicName"].ToString(),
                        ProvinceId = (int)reader["ProvinceId"],
                        ProvinceName = reader.IsDBNull(reader.GetOrdinal("ProvinceName")) ? string.Empty : reader["ProvinceName"].ToString(),
                        CityId = (int)reader["CityId"],
                        CityName = reader.IsDBNull(reader.GetOrdinal("CityName")) ? string.Empty : reader["CityName"].ToString(),
                        CountyId = (int)reader["CountyId"],
                        CountyName = reader.IsDBNull(reader.GetOrdinal("DistrictName")) ? string.Empty : reader["DistrictName"].ToString(),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader["Description"].ToString(),
                        ScenicLevel = (ScenicLevel)Enum.Parse(typeof(ScenicLevel), reader["ScenicLevel"].ToString()),
                        Company = new EyouSoft.Model.CompanyStructure.CompanyInfo()
                        {
                            ID = reader.IsDBNull(reader.GetOrdinal("CompanyId")) ? string.Empty : reader["CompanyId"].ToString(),
                            CompanyName = reader.IsDBNull(reader.GetOrdinal("CompanyName")) ? string.Empty : reader["CompanyName"].ToString(),
                            ContactInfo = new EyouSoft.Model.CompanyStructure.ContactPersonInfo()
                            {
                                ContactName = reader.IsDBNull(reader.GetOrdinal("ContactName")) ? string.Empty : reader["ContactName"].ToString(),
                                Tel = reader.IsDBNull(reader.GetOrdinal("ContactTel")) ? string.Empty : reader["ContactTel"].ToString(),
                                Mobile = reader.IsDBNull(reader.GetOrdinal("ContactMobile")) ? string.Empty : reader["ContactMobile"].ToString(),
                                Fax = reader.IsDBNull(reader.GetOrdinal("ContactFax")) ? string.Empty : reader["ContactFax"].ToString(),
                                QQ = reader.IsDBNull(reader.GetOrdinal("ContactQQ")) ? string.Empty : reader["ContactQQ"].ToString()
                            },
                        },
                        ContactName = reader.IsDBNull(reader.GetOrdinal("ContactOperatorName")) ? string.Empty : reader["ContactOperatorName"].ToString(),
                        ContactOperator = reader.IsDBNull(reader.GetOrdinal("ContactOperator")) ? string.Empty : reader["ContactOperator"].ToString(),
                        Status = (ExamineStatus)Enum.Parse(typeof(ExamineStatus), reader["Status"].ToString()),
                        B2B = (ScenicB2BDisplay)Enum.Parse(typeof(ScenicB2BDisplay), reader["B2B"].ToString()),
                        B2C = (ScenicB2CDisplay)Enum.Parse(typeof(ScenicB2CDisplay), reader["B2C"].ToString()),
                        ClickNum = (int)reader["ClickNum"]
                    });
                }
            }

            return list;
        }

        /// <summary>
        /// 指定条数获取景区列表
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        public virtual IList<MScenicArea> GetList(int topNum, string companyId, MSearchSceniceArea search)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select top({0}) Id,ScenicId,ScenicName,CityName,Address,ThumbAddress,Description from view_ScenicArea_Select ", topNum);
            sql.AppendFormat(" where Status = {0} ", (int)ExamineStatus.已审核);
            sql.Append(SqlQueryStr(search));
            if (!string.IsNullOrEmpty(companyId))
            {
                sql.AppendFormat(" and CompanyId = '{0}'", companyId);
            }
            if (search != null && (search.B2B != null || search.B2C != null))
            {
                if (search.B2B != null)
                {
                    sql.Append(" Order by B2BOrder ASC,CompanyLev desc,LastUpdateTime DESC");
                }
                else if (search.B2C != null)
                {
                    sql.Append(" Order by B2COrder ASC,CompanyLev desc,LastUpdateTime DESC");
                }
            }
            else
            {
                sql.Append(" Order by B2B ASC,B2BOrder ASC,CompanyLev desc,LastUpdateTime DESC");
            }

            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());

            IList<MScenicArea> list = new List<MScenicArea>();
            MScenicArea item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(
                        item = new MScenicArea()
                        {
                            Id = long.Parse(reader["Id"].ToString()),
                            ScenicId = reader["ScenicId"].ToString(),
                            ScenicName = reader["ScenicName"].ToString(),
                            CityName = reader.IsDBNull(reader.GetOrdinal("CityName")) ? string.Empty : reader["CityName"].ToString(),
                            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader["Description"].ToString(),
                            Img = new List<MScenicImg>() { 
                                new MScenicImg(){ 
                                    Address = reader.IsDBNull(reader.GetOrdinal("Address"))?string.Empty:reader["Address"].ToString(),
                                    ThumbAddress = reader.IsDBNull(reader.GetOrdinal("ThumbAddress"))?string.Empty:reader["ThumbAddress"].ToString()
                                }
                            }
                        });
                }
            }

            return list;
        }
        /// <summary>
        /// 景区列表(UserPublicCenter)
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public virtual IList<MScenicArea> GetPublicList(int pageSize, int pageIndex, ref int recordCount,
            MSearchSceniceArea search)
        {
            string tableName = "view_ScenicArea_Select_Public";
            string orderBy = "B2B ASC,B2BOrder ASC,CompanyLev desc,LastUpdateTime DESC";
            string fileds = "Id,ScenicId,ScenicName,Address,ThumbAddress,Description,CompanyId,CompanyLev";
            string primaryKey = "ScenicId";

            StringBuilder query = new StringBuilder();
            query.AppendFormat(" 1 = 1 {0}", SqlQueryStr(search));
            //前台不调用B2B为隐藏的类型


            IList<MScenicArea> list = new List<MScenicArea>();
            MScenicArea item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount,
                tableName, primaryKey, fileds, query.ToString(), orderBy))
            {
                while (reader.Read())
                {
                    list.Add(
                        item = new MScenicArea()
                        {
                            Id = long.Parse(reader["Id"].ToString()),
                            ScenicId = reader["ScenicId"].ToString(),
                            ScenicName = reader["ScenicName"].ToString(),
                            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader["Description"].ToString(),
                            Img = new List<MScenicImg>() { 
                                new MScenicImg(){ 
                                    Address = reader.IsDBNull(reader.GetOrdinal("Address"))?string.Empty:reader["Address"].ToString(),
                                    ThumbAddress = reader.IsDBNull(reader.GetOrdinal("ThumbAddress"))?string.Empty:reader["ThumbAddress"].ToString()
                                }
                            },
                            Company = new EyouSoft.Model.CompanyStructure.CompanyInfo()
                            {
                                ID = reader["CompanyId"].ToString(),
                                CompanyLev = (EyouSoft.Model.CompanyStructure.CompanyLev)Enum.Parse(typeof(EyouSoft.Model.CompanyStructure.CompanyLev), reader["CompanyLev"].ToString())
                            },
                        });
                }
            }

            return list;
        }
        /// <summary>
        /// 景区列表(包含门票信息,主题信息)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">查询条件</param>
        /// <returns>景区集合</returns>
        public virtual IList<MScenicArea> GetListAndTickets(int pageSize, int pageIndex, ref int recordCount, MSearchSceniceArea search)
        {
            string tableName = "view_ScenicArea_Select";
            string primaryKey = "ScenicId";
            string fileds = "ScenicId,ScenicName,ProvinceName,CityName,DistrictName,Status,LastUpdateTime,ScenicLevel,ClickNum,X,Y,CnAddress,ProvinceId,CityId,CountyId,Description,ThemeId,Tickets,CompanyId,Address,ThumbAddress,CompanyLev";
            string orderBy = string.Empty;

            #region 排序方式

            if (search != null)
            {
                switch (search.Type)
                {
                    case 1:
                        orderBy = "B2B ASC";
                        break;
                    case 2:
                        orderBy = "ClickNum DESC";
                        break;
                    case 3:
                        orderBy = "ScenicLevel DESC";
                        break;
                    default:
                        orderBy = "Status ASC,LastUpdateTime DESC";
                        break;
                }
            }
            else
            {
                orderBy = "Status ASC,LastUpdateTime DESC";
            }

            #endregion

            StringBuilder query = new StringBuilder();
            //用户后台默认不能查看隐藏的属性
            query.AppendFormat(" B2B <> {0}", (int)ScenicB2BDisplay.隐藏);
            query.AppendFormat(SqlQueryStr(search));

            IList<MScenicArea> list = new List<MScenicArea>();
            MScenicArea item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount,
                tableName, primaryKey, fileds, query.ToString(), orderBy))
            {
                while (reader.Read())
                {
                    item = new MScenicArea()
                    {
                        ScenicId = reader["ScenicId"].ToString(),
                        ScenicName = reader["ScenicName"].ToString(),
                        Status = (ExamineStatus)Enum.Parse(typeof(ExamineStatus), reader["Status"].ToString()),
                        ScenicLevel = (ScenicLevel)Enum.Parse(typeof(ScenicLevel), reader["ScenicLevel"].ToString()),
                        ClickNum = (int)reader["ClickNum"],
                        X = reader.IsDBNull(reader.GetOrdinal("X")) ? string.Empty : reader["X"].ToString(),
                        Y = reader.IsDBNull(reader.GetOrdinal("Y")) ? string.Empty : reader["Y"].ToString(),
                        CnAddress = reader.IsDBNull(reader.GetOrdinal("CnAddress")) ? string.Empty : reader["CnAddress"].ToString(),
                        ProvinceId = (int)reader["ProvinceId"],
                        ProvinceName = reader.IsDBNull(reader.GetOrdinal("ProvinceName")) ? string.Empty : reader["ProvinceName"].ToString(),
                        CityName = reader.IsDBNull(reader.GetOrdinal("CityName")) ? string.Empty : reader["CityName"].ToString(),
                        CountyName = reader.IsDBNull(reader.GetOrdinal("DistrictName")) ? string.Empty : reader["DistrictName"].ToString(),
                        CityId = (int)reader["CityId"],
                        CountyId = (int)reader["CountyId"],
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader["Description"].ToString(),
                        Company = new EyouSoft.Model.CompanyStructure.CompanyInfo()
                        {
                            ID = reader.IsDBNull(reader.GetOrdinal("CompanyId")) ? string.Empty : reader["CompanyId"].ToString(),
                            CompanyLev = (CompanyLev)Enum.Parse(typeof(CompanyLev), reader["CompanyLev"].ToString())
                        },
                        Img = new List<MScenicImg>() { 
                                new MScenicImg(){ 
                                    Address = reader.IsDBNull(reader.GetOrdinal("Address"))?string.Empty:reader["Address"].ToString(),
                                    ThumbAddress = reader.IsDBNull(reader.GetOrdinal("ThumbAddress"))?string.Empty:reader["ThumbAddress"].ToString()
                                }
                            }
                    };

                    #region 主题

                    if (!reader.IsDBNull(reader.GetOrdinal("ThemeId")))
                    {
                        string themeXml = reader["ThemeId"].ToString();
                        XElement xRoot = XElement.Parse(themeXml);
                        var xRows = Utility.GetXElements(xRoot, "row");
                        item.ThemeId = new List<MScenicTheme>();
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                item.ThemeId.Add(new MScenicTheme()
                                {
                                    ThemeId = Utility.GetInt(Utility.GetXAttributeValue(t, "ThemeId")),
                                    ThemeName = Utility.GetXAttributeValue(t, "ThemeName")
                                });
                            }
                        }
                    }

                    #endregion

                    #region 门票
                    if (!reader.IsDBNull(reader.GetOrdinal("Tickets")))
                    {
                        string ticketsXml = reader["Tickets"].ToString();

                        XmlDocument xml = new XmlDocument();
                        xml.LoadXml(ticketsXml);
                        //门票数量
                        int count = xml.DocumentElement.ChildNodes.Count;
                        item.TicketsList = new List<MScenicTickets>();
                        MScenicTickets tickets = null;
                        for (int i = 0; i < count; i++)
                        {
                            //select TicketsId,TypeName,MarketPrice,isnull(cast(StartTime as varchar(50)),'') StartTime,isnull(cast(EndTime as varchar(50)),'') EndTime,Payment,Status,RetailPrice,ExamineStatus,WebsitePrices,DistributionPrice from tbl_ScenicTickets 
                            tickets = new MScenicTickets()
                            {
                                TicketsId = xml.DocumentElement.ChildNodes[i].ChildNodes[0].InnerText,
                                TypeName = xml.DocumentElement.ChildNodes[i].ChildNodes[1].InnerText,
                                MarketPrice = decimal.Parse(xml.DocumentElement.ChildNodes[i].ChildNodes[2].InnerText),
                                Payment = (ScenicPayment)Enum.Parse(typeof(ScenicPayment), xml.DocumentElement.ChildNodes[i].ChildNodes[5].InnerText),
                                Status = (ScenicTicketsStatus)Enum.Parse(typeof(ScenicTicketsStatus), xml.DocumentElement.ChildNodes[i].ChildNodes[6].InnerText),
                                RetailPrice = !string.IsNullOrEmpty(xml.DocumentElement.ChildNodes[i].ChildNodes[7].InnerText) ? decimal.Parse(xml.DocumentElement.ChildNodes[i].ChildNodes[7].InnerText) : 0,
                                ExamineStatus = (ExamineStatus)Enum.Parse(typeof(ExamineStatus), xml.DocumentElement.ChildNodes[i].ChildNodes[8].InnerText),
                                WebsitePrices = !string.IsNullOrEmpty(xml.DocumentElement.ChildNodes[i].ChildNodes[9].InnerText) ? decimal.Parse(xml.DocumentElement.ChildNodes[i].ChildNodes[9].InnerText) : 0,
                                DistributionPrice = !string.IsNullOrEmpty(xml.DocumentElement.ChildNodes[i].ChildNodes[10].InnerText) ? decimal.Parse(xml.DocumentElement.ChildNodes[i].ChildNodes[10].InnerText) : 0
                            };
                            if (!string.IsNullOrEmpty(xml.DocumentElement.ChildNodes[i].ChildNodes[3].InnerText))
                            {
                                tickets.StartTime = DateTime.Parse((xml.DocumentElement.ChildNodes[i].ChildNodes[3].InnerText));
                            }
                            if (!string.IsNullOrEmpty(xml.DocumentElement.ChildNodes[i].ChildNodes[4].InnerText))
                            {
                                tickets.EndTime = DateTime.Parse((xml.DocumentElement.ChildNodes[i].ChildNodes[4].InnerText));
                            }
                            item.TicketsList.Add(tickets);
                        }
                    }
                    #endregion

                    list.Add(item);
                }
            }
            return list;
        }

        #endregion

        #region private
        /// <summary>
        /// 生成条件(没有带where关键字,平且and开头)
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private string SqlQueryStr(MSearchSceniceArea search)
        {
            StringBuilder query = new StringBuilder();
            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.YiNuo))
                {
                    //用户后台-旅游资源-景区门票
                    query.AppendFormat(" and CompanyId <> '{0}'", search.YiNuo);
                }
                if (!string.IsNullOrEmpty(search.ScenicName))
                {
                    query.AppendFormat(" and ScenicName like '%{0}%'", search.ScenicName);
                }
                if (search.Level != null)
                {
                    query.AppendFormat(" and ScenicLevel = {0}", (int)search.Level);
                }
                if (search.ProvinceId != null)
                {
                    query.AppendFormat(" and ProvinceId = {0}", search.ProvinceId);
                }
                if (search.CityId != null)
                {
                    query.AppendFormat(" and CityId = {0}", search.CityId);
                }
                if (search.CountyId != null)
                {
                    query.AppendFormat(" and CountyId = {0}", search.CountyId);
                }
                if (!string.IsNullOrEmpty(search.ScenicName))
                {
                    query.AppendFormat(" and ScenicName like '%{0}%'", search.ScenicName);
                }
                if (search.Status != null)
                {
                    query.AppendFormat(" and Status = {0}", (int)search.Status);
                }
                if (search.ThemeId != null)
                {
                    query.AppendFormat(" and cast(ThemeId as xml).exist('/item/row[@ThemeId=\"{0}\"]') = 1", search.ThemeId);
                }
                if (!string.IsNullOrEmpty(search.CompanyId))
                {
                    query.AppendFormat(" and CompanyId = '{0}'", search.CompanyId);
                }
                if (search.B2B != null)
                {
                    query.AppendFormat(" and B2B = {0}", (int)search.B2B);
                }
                if (search.B2C != null)
                {
                    query.AppendFormat(" and B2C = {0}", (int)search.B2C);
                }
                if (search.IsQH != null)
                {
                    if (!(bool)search.IsQH)
                    {
                        query.AppendFormat(" and B2B <> {0}", (int)ScenicB2BDisplay.隐藏);
                    }
                }
                if (search.B2Bs != null && search.B2Bs.Length > 0)
                {
                    if (search.B2Bs.Length == 1)
                    {
                        if (search.B2Bs[0].HasValue && search.B2Bs[0].Value != ScenicB2BDisplay.隐藏)
                        {
                            query.AppendFormat(" and B2B = {0}", (int)search.B2Bs[0].Value);
                        }
                    }
                    else
                    {
                        string strTmp = string.Empty;
                        foreach (var t in search.B2Bs)
                        {
                            if (!t.HasValue || t.Value == ScenicB2BDisplay.隐藏)
                                continue;

                            strTmp += (int)t.Value + ",";
                        }
                        if (!string.IsNullOrEmpty(strTmp))
                        {
                            strTmp = strTmp.TrimEnd(',');
                            query.AppendFormat(" and B2B in ({0}) ", strTmp);
                        }

                    }
                }
            }
            return query.ToString();
        }

        #endregion
    }
}
