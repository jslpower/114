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
    /// 创建人：鲁功源 2010-07-01
    /// 描述：供应商数据层
    /// ----------------------
    /// 修改人 ：毛坤 2011-5-10 分页获取列表方法增加了len(remark)>0的查询过滤
    /// </summary>
    /// 修改人：zhengfj 2011-6-1
    /// 修改内容:GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyName, int ProvinceId, int CityId, int CompanyLevel,
    ///        int CompanyTag, EyouSoft.Model.CompanyStructure.BusinessProperties? CompanyType,bool remark);
    public class SupplierInfo : DALBase, IDAL.CompanyStructure.ISupplierInfo
    {
        #region 构造函数
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database;
        /// <summary>
        /// 构造函数
        /// </summary>
        public SupplierInfo()
        {
            this._database = base.CompanyStore;
        }
        #endregion

        #region SQL变量定义
        private const string SQL_SupplierInfo_ALLUPDATE = "UPDATE tbl_CompanyInfo SET CompanyName=@CompanyName,CompanyLevel=@CompanyLevel,ProvinceId=@ProvinceId,CityId=@CityId,Remark=@Remark,ShortRemark=@ShortRemark,ContactName=@ContactName,ContactTel=@ContactTel,ContactFax=@ContactFax,ContactMQ=@ContactMQ,CompanyAddress=@CompanyAddress,WebSite=@WebSite,ContactMobile=@ContactMobile,ContactEmail=@ContactEmail WHERE Id=@CompanyId ;";
        private const string SQL_SupplierInfo_SHORTUPDATE = "UPDATE tbl_CompanyInfo SET CompanyName=@CompanyName,Remark=@Remark,ShortRemark=@ShortRemark WHERE Id=@CompanyId ";
        private const string SQL_SupplierInfo_SETCOMPANYIMG = "DELETE tbl_CompanyAttachInfo WHERE CompanyId=@CompanyId AND FieldName='CompanyImg'; INSERT INTO  tbl_CompanyAttachInfo(CompanyId,FieldName,FieldValue) VALUES(@CompanyId,'CompanyImg',@CompanyImg);";
        private const string SQL_SupplierInfo_SETPRODUCT = "INSERT INTO tbl_CompanyProduct(CompanyId,ProductImg,ProductImgLink,ProductRemark) VALUES(@CompanyId,'{0}','{1}','{2}');";
        private const string SQL_SupplierInfo_GETMODEL = "SELECT ID,CompanyName,CompanyLevel,CompanyBrand,CompanyAddress,Remark,ContactName,ContactTel,ContactFax,(select FieldValue from tbl_CompanyAttachInfo where CompanyId=@CompanyId and FieldName='CompanyLogo') as CompanyLogo,WebSite,(SELECT IsEnabled FROM tbl_CompanyPayService WHERE CompanyId=tbl_CompanyInfo.Id AND ServiceId=2) AS IsHighShop,(SELECT TOP 1 TypeId FROM tbl_CompanyTypeList WHERE CompanyId=@CompanyId) AS CompanyType,ContactEmail,ContactMobile,ProvinceId,CityId,ShortRemark,(select FieldValue from tbl_CompanyAttachInfo where CompanyId=@CompanyId and FieldName='CompanyImg') as CompanyImg,ContactMQ,ContactQQ,ContactMSN,ContactMobile from tbl_CompanyInfo where id=@CompanyId";
        private const string SQL_SupplierInfo_DELCOMPANYTAGS = "DELETE tbl_CompanyTag WHERE CompanyID=@CompanyId and ClassId=@ClassId;";
        private const string SQL_SupplierInfo_ADDCOMPANYTAGS = "INSERT INTO tbl_CompanyTag(CompanyId,TagName,TagId,ClassId) values(@CompanyId,'{0}',{1},@ClassId);";
        private const string SQL_ProductInfo_DELETE = "delete tbl_CompanyProduct where companyid=@CompanyId AND ID='{0}';";
        /// <summary>
        /// 删除前移动文件记录
        /// </summary>
        private const string SQL_DELETEDFILE_MOVE = "IF (SELECT COUNT(*) FROM [tbl_CompanyProduct] WHERE [CompanyId]=@CompanyId AND [ProductImg]='{1}' AND ID='{0}')=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [ProductImg] FROM [tbl_CompanyProduct] WHERE [CompanyId]=@CompanyId AND ID='{0}' AND ProductImg<>'';";
        #endregion

        #region ISupplierInfo成员
        /// <summary>
        /// 付费版供应商修改
        /// </summary>
        /// <param name="model">供应商实体类</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Update(EyouSoft.Model.CompanyStructure.SupplierInfo model)
        {
            StringBuilder strSql = new StringBuilder(SQL_SupplierInfo_ALLUPDATE);
            if (model.CompanyTag != null && model.CompanyTag.Count > 0)
            {
                strSql.Append(SQL_SupplierInfo_DELCOMPANYTAGS);
                foreach (EyouSoft.Model.CompanyStructure.CompanyTag Tag in model.CompanyTag)
                {
                    strSql.AppendFormat(SQL_SupplierInfo_ADDCOMPANYTAGS, Tag.FieldName, Tag.FieldId);
                }
            }
            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            this._database.AddInParameter(dc, "CompanyName", DbType.String, model.CompanyName);
            this._database.AddInParameter(dc, "CompanyLevel", DbType.Byte, model.CompanyLevel);
            this._database.AddInParameter(dc, "ProvinceId", DbType.Int32, model.ProvinceId);
            this._database.AddInParameter(dc, "CityId", DbType.Int32, model.CityId);
            this._database.AddInParameter(dc, "Remark", DbType.String, model.Remark);
            this._database.AddInParameter(dc, "ShortRemark", DbType.String, model.ShortRemark);
            this._database.AddInParameter(dc, "ContactName", DbType.String, model.ContactInfo.ContactName);
            this._database.AddInParameter(dc, "ContactTel", DbType.String, model.ContactInfo.Tel);
            this._database.AddInParameter(dc, "ContactFax", DbType.String, model.ContactInfo.Fax);
            this._database.AddInParameter(dc, "ContactMQ", DbType.String, model.ContactInfo.MQ);
            this._database.AddInParameter(dc, "CompanyAddress", DbType.String, model.CompanyAddress);
            this._database.AddInParameter(dc, "WebSite", DbType.String, model.WebSite);
            this._database.AddInParameter(dc, "ContactMobile", DbType.String, model.ContactInfo.Mobile);
            this._database.AddInParameter(dc, "ContactEmail", DbType.String, model.ContactInfo.Email);
            this._database.AddInParameter(dc, "ClassId", DbType.Byte, (int)EyouSoft.Model.SystemStructure.SysFieldType.周边环境);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.ID);

            return DbHelper.ExecuteSqlTrans(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 免费版供应商修改
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="CompanyName">公司名称</param>
        /// <param name="Remark">公司介绍</param>
        /// <param name="ShortRemark">公司业务优势</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Update(string CompanyId, string CompanyName, string Remark, string ShortRemark)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SupplierInfo_SHORTUPDATE);
            this._database.AddInParameter(dc, "CompanyName", DbType.String, CompanyName);
            this._database.AddInParameter(dc, "Remark", DbType.String, Remark);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._database.AddInParameter(dc, "ShortRemark", DbType.String, ShortRemark);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 设置供应商相关附件（企业宣传图，产品展示图片）
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="CompanyImg">公司宣传图</param>
        /// <param name="CompanyImgThumb">公司宣传图缩略图</param>
        /// <param name="list">产品附件集合</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool SetProduct(string CompanyId, string CompanyImg, string CompanyImgThumb, IList<EyouSoft.Model.CompanyStructure.ProductInfo> list)
        {
            StringBuilder strSql = new StringBuilder();
            if (CompanyImg.Trim().Length > 0)
            {
                strSql.Append(SQL_SupplierInfo_SETCOMPANYIMG);
            }
            if (list != null)
            {
                foreach (EyouSoft.Model.CompanyStructure.ProductInfo model in list)
                {
                    strSql.AppendFormat(SQL_DELETEDFILE_MOVE, model.ID.ToString(), model.ImagePath);
                    strSql.AppendFormat(SQL_ProductInfo_DELETE, model.ID.ToString());
                    strSql.AppendFormat(SQL_SupplierInfo_SETPRODUCT, model.ImagePath, model.ImageLink, model.ProductRemark);
                }
            }
            if (strSql.Length > 0)
            {
                DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
                this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
                if (CompanyImg.Trim().Length > 0)
                {
                    this._database.AddInParameter(dc, "CompanyImg", DbType.String, CompanyImg);
                    //this._database.AddInParameter(dc, "CompanyImgThumb", DbType.String, CompanyImgThumb);
                }
                return DbHelper.ExecuteSqlTrans(dc, this._database) > 0 ? true : false;
            }
            return false;
        }
        /// <summary>
        /// 分页获取供应商列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyName">供应商名称（用于模糊查询）</param>
        /// <param name="ProvinceId">省份编号</param>
        /// <param name="CityId">城市编号</param>
        /// <param name="CompanyLevel">酒店星级</param>
        /// <param name="CompanyTag">景区主题或者酒店周边环境</param>
        /// <param name="CompanyType">公司性质</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.SupplierInfo> GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyName, int ProvinceId, int CityId, int CompanyLevel,
            int CompanyTag, EyouSoft.Model.CompanyStructure.BusinessProperties? CompanyType)
        {
            IList<EyouSoft.Model.CompanyStructure.SupplierInfo> list = new List<EyouSoft.Model.CompanyStructure.SupplierInfo>();
            string tableName = "tbl_CompanyInfo";
            string fields = "ID,CompanyName,CompanyLevel,CompanyBrand,CompanyAddress,Remark,ContactName,ContactTel,ContactFax,(select FieldValue from tbl_CompanyAttachInfo where CompanyId=tbl_CompanyInfo.id and FieldName='CompanyLogo') as CompanyImgThumb,WebSite,(SELECT IsEnabled FROM tbl_CompanyPayService WHERE CompanyId=tbl_CompanyInfo.Id AND ServiceId=2) AS IsHighShop,ProvinceId,CityId,ShortRemark,(select TagId,TagName from tbl_CompanyTag where CompanyId=tbl_CompanyInfo.id for xml path,root('root')) AS CompanyTag";
            string primaryKey = "ID";
            string orderByString = "(case when(select count(*) from tbl_CompanyPayService where companyid=tbl_CompanyInfo.id and ServiceId=2 and isEnabled='1')>0 then 1 else 0 end) desc,IssueTime DESC";
            Dictionary<string, string> CompanyTagList = new Dictionary<string, string>();

            #region 生成查询条件
            StringBuilder strWhere = new StringBuilder(" IsCheck='1' AND IsDeleted='0'And len(Remark)>0 And CompanyType in(2,3,4,5,6)");
            if (!string.IsNullOrEmpty(CompanyName))
            {
                strWhere.AppendFormat(" AND CompanyName like'%{0}%' ", CompanyName);
            }
            if (ProvinceId > 0)
            {
                strWhere.AppendFormat(" AND ProvinceId={0} ", ProvinceId);
            }
            if (CityId > 0)
            {
                strWhere.AppendFormat(" AND CityId={0} ", CityId);
            }
            if (CompanyLevel > -1)
            {
                EyouSoft.Model.CompanyStructure.HotelLevel hotelLevel = (EyouSoft.Model.CompanyStructure.HotelLevel)CompanyLevel;
                if (hotelLevel == EyouSoft.Model.CompanyStructure.HotelLevel.三星或同级 ||
                    hotelLevel == EyouSoft.Model.CompanyStructure.HotelLevel.准三星)
                {
                    strWhere.AppendFormat(" AND (CompanyLevel={0} or CompanyLevel={1})", (int)EyouSoft.Model.CompanyStructure.HotelLevel.三星或同级, (int)EyouSoft.Model.CompanyStructure.HotelLevel.准三星);
                }
                else if (hotelLevel == EyouSoft.Model.CompanyStructure.HotelLevel.四星或同级 ||
                    hotelLevel == EyouSoft.Model.CompanyStructure.HotelLevel.准四星)
                {
                    strWhere.AppendFormat(" AND (CompanyLevel={0} or CompanyLevel={1})", (int)EyouSoft.Model.CompanyStructure.HotelLevel.四星或同级, (int)EyouSoft.Model.CompanyStructure.HotelLevel.准四星);
                }
                else
                {
                    strWhere.AppendFormat(" AND CompanyLevel={0} ", CompanyLevel);
                }
            }
            if (CompanyTag > 0 && CompanyType != null)
            {
                int classId = -1;
                if (CompanyType.Value == EyouSoft.Model.CompanyStructure.BusinessProperties.酒店)
                {
                    classId = (int)EyouSoft.Model.SystemStructure.SysFieldType.周边环境;
                }
                if (CompanyType.Value == EyouSoft.Model.CompanyStructure.BusinessProperties.景区)
                {
                    classId = (int)EyouSoft.Model.SystemStructure.SysFieldType.景点主题;
                }
                strWhere.AppendFormat(" AND id in(select companyId from tbl_CompanyTag Where TagId={0} and classId={1}) ", CompanyTag, classId);
            }
            if (CompanyType != null)
            {
                strWhere.AppendFormat(" AND CompanyType={0} ", (int)CompanyType.Value);
            }
            #endregion

            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CompanyStructure.SupplierInfo model = new EyouSoft.Model.CompanyStructure.SupplierInfo();
                    model.ID = dr.GetString(0);
                    model.CompanyName = dr.IsDBNull(1) ? string.Empty : dr.GetString(1);
                    if (!dr.IsDBNull(2))
                        model.CompanyLevel = int.Parse(dr[2].ToString());
                    model.CompanyBrand = dr.IsDBNull(3) ? string.Empty : dr.GetString(3);
                    model.CompanyAddress = dr.IsDBNull(4) ? string.Empty : dr.GetString(4);
                    model.Remark = dr.IsDBNull(5) ? string.Empty : dr.GetString(5);
                    model.ContactInfo.ContactName = dr.IsDBNull(6) ? string.Empty : dr.GetString(6);
                    model.ContactInfo.Tel = dr.IsDBNull(7) ? string.Empty : dr.GetString(7);
                    model.ContactInfo.Fax = dr.IsDBNull(8) ? string.Empty : dr.GetString(8);
                    model.CompanyImgThumb = dr.IsDBNull(9) ? string.Empty : dr.GetString(9);
                    model.WebSite = dr.IsDBNull(10) ? string.Empty : dr.GetString(10);
                    if (!dr.IsDBNull(11) && dr.GetString(11) == "1")
                    {
                        model.State.CompanyService.SetServiceItem(
                            new EyouSoft.Model.CompanyStructure.CompanyServiceItem()
                            {
                                IsEnabled = true,
                                Service = EyouSoft.Model.CompanyStructure.SysService.HighShop
                            });
                    }
                    else
                    {
                        model.State.CompanyService.SetServiceItem(
                            new EyouSoft.Model.CompanyStructure.CompanyServiceItem()
                            {
                                IsEnabled = false,
                                Service = EyouSoft.Model.CompanyStructure.SysService.HighShop
                            });
                    }
                    model.ProvinceId = dr.GetInt32(12);
                    model.CityId = dr.GetInt32(13);
                    model.ShortRemark = dr.IsDBNull(14) ? string.Empty : dr.GetString(14);
                    if (CompanyType.HasValue && CompanyType.Value == EyouSoft.Model.CompanyStructure.BusinessProperties.酒店 && !dr.IsDBNull(15))
                        CompanyTagList.Add(model.ID, dr.GetString(15));
                    list.Add(model);
                    model = null;
                }
                for (int i = 0; i < list.Count; i++)
                {
                    if (CompanyTagList.ContainsKey(list[i].ID))
                    {
                        IList<EyouSoft.Model.CompanyStructure.CompanyTag> taglist = new List<EyouSoft.Model.CompanyStructure.CompanyTag>();
                        System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                        xmlDoc.LoadXml(CompanyTagList[list[i].ID]);
                        System.Xml.XmlNodeList TagList = xmlDoc.GetElementsByTagName("TagId");
                        System.Xml.XmlNodeList TagNameList = xmlDoc.GetElementsByTagName("TagName");
                        for (int j = 0; j < TagList.Count; j++)
                        {
                            EyouSoft.Model.CompanyStructure.CompanyTag tag = new EyouSoft.Model.CompanyStructure.CompanyTag();
                            tag.FieldId = Convert.ToInt32(TagList[j].FirstChild.Value);
                            tag.FieldName = TagNameList[j].FirstChild.Value;
                            taglist.Add(tag);
                        }
                        list[i].CompanyTag = taglist;
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 分页获取供应商列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyName">供应商名称（用于模糊查询）</param>
        /// <param name="ProvinceId">省份编号</param>
        /// <param name="CityId">城市编号</param>
        /// <param name="CompanyLevel">酒店星级</param>
        /// <param name="CompanyTag">景区主题或者酒店周边环境</param>
        /// <param name="CompanyType">公司性质</param>
        /// <param name="remark">false:null和""不读取(true:不作条件)</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.SupplierInfo> GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyName, int ProvinceId, int CityId, int CompanyLevel,
             int CompanyTag, EyouSoft.Model.CompanyStructure.BusinessProperties? CompanyType, bool remark)
        {
            IList<EyouSoft.Model.CompanyStructure.SupplierInfo> list = new List<EyouSoft.Model.CompanyStructure.SupplierInfo>();
            string tableName = "tbl_CompanyInfo";
            string fields = "ID,CompanyName,CompanyLevel,CompanyBrand,CompanyAddress,Remark,ContactName,ContactTel,ContactFax,(select FieldValue from tbl_CompanyAttachInfo where CompanyId=tbl_CompanyInfo.id and FieldName='CompanyLogo') as CompanyImgThumb,WebSite,(SELECT IsEnabled FROM tbl_CompanyPayService WHERE CompanyId=tbl_CompanyInfo.Id AND ServiceId=2) AS IsHighShop,ProvinceId,CityId,ShortRemark,(select TagId,TagName from tbl_CompanyTag where CompanyId=tbl_CompanyInfo.id for xml path,root('root')) AS CompanyTag";
            string primaryKey = "ID";
            string orderByString = "(case when(select count(*) from tbl_CompanyPayService where companyid=tbl_CompanyInfo.id and ServiceId=2 and isEnabled='1')>0 then 1 else 0 end) desc,IssueTime DESC";
            Dictionary<string, string> CompanyTagList = new Dictionary<string, string>();

            #region 生成查询条件
            StringBuilder strWhere = new StringBuilder(" IsCheck='1' AND IsDeleted='0'And len(Remark)>0 And CompanyType in(2,3,4,5,6)");
            if (!remark)
            {
                strWhere.Append(" AND ISNULL(Remark,'') <> ''");
            }
            if (!string.IsNullOrEmpty(CompanyName))
            {
                strWhere.AppendFormat(" AND CompanyName like'%{0}%' ", CompanyName);
            }
            if (ProvinceId > 0)
            {
                strWhere.AppendFormat(" AND ProvinceId={0} ", ProvinceId);
            }
            if (CityId > 0)
            {
                strWhere.AppendFormat(" AND CityId={0} ", CityId);
            }
            if (CompanyLevel > -1)
            {
                EyouSoft.Model.CompanyStructure.HotelLevel hotelLevel = (EyouSoft.Model.CompanyStructure.HotelLevel)CompanyLevel;
                if (hotelLevel == EyouSoft.Model.CompanyStructure.HotelLevel.三星或同级 ||
                    hotelLevel == EyouSoft.Model.CompanyStructure.HotelLevel.准三星)
                {
                    strWhere.AppendFormat(" AND (CompanyLevel={0} or CompanyLevel={1})", (int)EyouSoft.Model.CompanyStructure.HotelLevel.三星或同级, (int)EyouSoft.Model.CompanyStructure.HotelLevel.准三星);
                }
                else if (hotelLevel == EyouSoft.Model.CompanyStructure.HotelLevel.四星或同级 ||
                    hotelLevel == EyouSoft.Model.CompanyStructure.HotelLevel.准四星)
                {
                    strWhere.AppendFormat(" AND (CompanyLevel={0} or CompanyLevel={1})", (int)EyouSoft.Model.CompanyStructure.HotelLevel.四星或同级, (int)EyouSoft.Model.CompanyStructure.HotelLevel.准四星);
                }
                else
                {
                    strWhere.AppendFormat(" AND CompanyLevel={0} ", CompanyLevel);
                }
            }
            if (CompanyTag > 0 && CompanyType != null)
            {
                int classId = -1;
                if (CompanyType.Value == EyouSoft.Model.CompanyStructure.BusinessProperties.酒店)
                {
                    classId = (int)EyouSoft.Model.SystemStructure.SysFieldType.周边环境;
                }
                if (CompanyType.Value == EyouSoft.Model.CompanyStructure.BusinessProperties.景区)
                {
                    classId = (int)EyouSoft.Model.SystemStructure.SysFieldType.景点主题;
                }
                strWhere.AppendFormat(" AND id in(select companyId from tbl_CompanyTag Where TagId={0} and classId={1}) ", CompanyTag, classId);
            }
            if (CompanyType != null)
            {
                strWhere.AppendFormat(" AND CompanyType={0} ", (int)CompanyType.Value);
            }
            #endregion

            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CompanyStructure.SupplierInfo model = new EyouSoft.Model.CompanyStructure.SupplierInfo();
                    model.ID = dr.GetString(0);
                    model.CompanyName = dr.IsDBNull(1) ? string.Empty : dr.GetString(1);
                    if (!dr.IsDBNull(2))
                        model.CompanyLevel = int.Parse(dr[2].ToString());
                    model.CompanyBrand = dr.IsDBNull(3) ? string.Empty : dr.GetString(3);
                    model.CompanyAddress = dr.IsDBNull(4) ? string.Empty : dr.GetString(4);
                    model.Remark = dr.IsDBNull(5) ? string.Empty : dr.GetString(5);
                    model.ContactInfo.ContactName = dr.IsDBNull(6) ? string.Empty : dr.GetString(6);
                    model.ContactInfo.Tel = dr.IsDBNull(7) ? string.Empty : dr.GetString(7);
                    model.ContactInfo.Fax = dr.IsDBNull(8) ? string.Empty : dr.GetString(8);
                    model.CompanyImgThumb = dr.IsDBNull(9) ? string.Empty : dr.GetString(9);
                    model.WebSite = dr.IsDBNull(10) ? string.Empty : dr.GetString(10);
                    if (!dr.IsDBNull(11) && dr.GetString(11) == "1")
                    {
                        model.State.CompanyService.SetServiceItem(
                            new EyouSoft.Model.CompanyStructure.CompanyServiceItem()
                            {
                                IsEnabled = true,
                                Service = EyouSoft.Model.CompanyStructure.SysService.HighShop
                            });
                    }
                    else
                    {
                        model.State.CompanyService.SetServiceItem(
                            new EyouSoft.Model.CompanyStructure.CompanyServiceItem()
                            {
                                IsEnabled = false,
                                Service = EyouSoft.Model.CompanyStructure.SysService.HighShop
                            });
                    }
                    model.ProvinceId = dr.GetInt32(12);
                    model.CityId = dr.GetInt32(13);
                    model.ShortRemark = dr.IsDBNull(14) ? string.Empty : dr.GetString(14);
                    if (CompanyType.HasValue && CompanyType.Value == EyouSoft.Model.CompanyStructure.BusinessProperties.酒店 && !dr.IsDBNull(15))
                        CompanyTagList.Add(model.ID, dr.GetString(15));
                    list.Add(model);
                    model = null;
                }
                for (int i = 0; i < list.Count; i++)
                {
                    if (CompanyTagList.ContainsKey(list[i].ID))
                    {
                        IList<EyouSoft.Model.CompanyStructure.CompanyTag> taglist = new List<EyouSoft.Model.CompanyStructure.CompanyTag>();
                        System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                        xmlDoc.LoadXml(CompanyTagList[list[i].ID]);
                        System.Xml.XmlNodeList TagList = xmlDoc.GetElementsByTagName("TagId");
                        System.Xml.XmlNodeList TagNameList = xmlDoc.GetElementsByTagName("TagName");
                        for (int j = 0; j < TagList.Count; j++)
                        {
                            EyouSoft.Model.CompanyStructure.CompanyTag tag = new EyouSoft.Model.CompanyStructure.CompanyTag();
                            tag.FieldId = Convert.ToInt32(TagList[j].FirstChild.Value);
                            tag.FieldName = TagNameList[j].FirstChild.Value;
                            taglist.Add(tag);
                        }
                        list[i].CompanyTag = taglist;
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取供应商实体
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.CompanyStructure.SupplierInfo GetModel(string CompanyId)
        {
            EyouSoft.Model.CompanyStructure.SupplierInfo model = null;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SupplierInfo_GETMODEL);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.CompanyStructure.SupplierInfo();
                    model.ID = dr.GetString(0);
                    model.CompanyName = dr.IsDBNull(1) ? string.Empty : dr.GetString(1);
                    if (!dr.IsDBNull(2))
                        model.CompanyLevel = int.Parse(dr[2].ToString());
                    model.CompanyBrand = dr.IsDBNull(3) ? string.Empty : dr.GetString(3);
                    model.CompanyAddress = dr.IsDBNull(4) ? string.Empty : dr.GetString(4);
                    model.Remark = dr.IsDBNull(5) ? string.Empty : dr.GetString(5);
                    model.ContactInfo.ContactName = dr.IsDBNull(6) ? string.Empty : dr.GetString(6);
                    model.ContactInfo.Tel = dr.IsDBNull(7) ? string.Empty : dr.GetString(7);
                    model.ContactInfo.Fax = dr.IsDBNull(8) ? string.Empty : dr.GetString(8);
                    model.ContactInfo.MQ = dr.IsDBNull(19) ? string.Empty : dr.GetString(19);
                    model.ContactInfo.QQ = dr.IsDBNull(20) ? string.Empty : dr.GetString(20);
                    model.ContactInfo.MSN = dr.IsDBNull(21) ? string.Empty : dr.GetString(21);
                    model.ContactInfo.Mobile = dr.IsDBNull(22) ? string.Empty : dr.GetString(22);
                    model.CompanyImgThumb = dr.IsDBNull(9) ? string.Empty : dr.GetString(9);
                    model.WebSite = dr.IsDBNull(10) ? string.Empty : dr.GetString(10);
                    if (!dr.IsDBNull(11) && dr.GetString(11) == "1")
                    {
                        model.State.CompanyService.SetServiceItem(
                            new EyouSoft.Model.CompanyStructure.CompanyServiceItem()
                            {
                                IsEnabled = true,
                                Service = EyouSoft.Model.CompanyStructure.SysService.HighShop
                            });
                    }
                    if (!dr.IsDBNull(12))
                        model.CompanyRole.SetRole((EyouSoft.Model.CompanyStructure.CompanyType)int.Parse(dr.GetByte(12).ToString()));
                    if (!dr.IsDBNull(13))
                        model.ContactInfo.Email = dr.GetString(13);
                    if (!dr.IsDBNull(14))
                        model.ContactInfo.Mobile = dr.GetString(14);
                    model.ProvinceId = dr.GetInt32(15);
                    model.CityId = dr.GetInt32(16);
                    if (!dr.IsDBNull(17))
                        model.ShortRemark = dr.GetString(17);
                    if (!dr.IsDBNull(18))
                        model.CompanyImg = dr.GetString(18);
                    model.ProductInfo = GetProductInfos(model.ID);
                    model.CompanyTag = GetCompanyTags(model.ID, EyouSoft.Model.SystemStructure.SysFieldType.周边环境);
                }
            }
            return model;
        }
        #endregion

        #region 私有成员方法
        /// <summary>
        /// 获取供应商的产品附件列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.CompanyStructure.ProductInfo> GetProductInfos(string CompanyId)
        {
            IList<EyouSoft.Model.CompanyStructure.ProductInfo> list = new List<EyouSoft.Model.CompanyStructure.ProductInfo>();
            string strSql = "select ID,ProductImg,ProductImgLink,ProductRemark from tbl_CompanyProduct where CompanyId=@CompanyId";
            DbCommand dc = this._database.GetSqlStringCommand(strSql);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CompanyStructure.ProductInfo model = new EyouSoft.Model.CompanyStructure.ProductInfo();
                    model.ID = dr.GetInt32(0);
                    model.ImagePath = dr.GetString(1);
                    model.ImageLink = dr.GetString(2);
                    model.ProductRemark = dr.GetString(3);
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 获取供应商的标签(景点主题，酒店周边环境)
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="fieldtype"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.CompanyStructure.CompanyTag> GetCompanyTags(string CompanyId, EyouSoft.Model.SystemStructure.SysFieldType fieldtype)
        {
            IList<EyouSoft.Model.CompanyStructure.CompanyTag> list = new List<EyouSoft.Model.CompanyStructure.CompanyTag>();
            string strSql = "select TagId,TagName from tbl_CompanyTag where CompanyId=@CompanyId and ClassId=@ClassId ";
            DbCommand dc = this._database.GetSqlStringCommand(strSql);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._database.AddInParameter(dc, "ClassId", DbType.Int32, (int)fieldtype);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CompanyStructure.CompanyTag model = new EyouSoft.Model.CompanyStructure.CompanyTag();
                    model.FieldId = dr.GetInt32(0);
                    model.FieldName = dr.GetString(1);
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        #endregion
    }
}
