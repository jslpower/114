using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.Model;
using EyouSoft.IDAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.TicketStructure
{
    /// <summary>
    /// 常旅客信息数据DAL
    /// </summary>
    /// 创建人：luofx 2010-10-28
    public class TicketVistorInfo : DALBase, IDAL.TicketStructure.ITicketVistorInfo
    {
        #region private const SQL
        /// <summary>
        /// 获取model时
        /// </summary>
        private const string Sql_Select_TicketVistorInfo_ByID = "SELECT * FROM [tbl_TicketVistorInfo] WHERE [Id]=@ID ";
        /*/// <summary>
        /// 机票单个新增
        /// </summary>
        private const string Sql_Insert_TicketVistorInfo = "if NOT EXISTS(SELECT 1 FROM [tbl_TicketVistorInfo] WHERE [CompanyId]=@CompanyId AND [CardNo]=@CardNo) INSERT INTO tbl_TicketVistorInfo ([id],[CompanyId],[CardType],[CardNo],[ChinaName],[EnglishName],[ContactSex],[ContactTel],[Remark],[NationCode],[VistorType],[DataType]) VALUES (@ID,@CompanyId,@CardType,@CardNo,@ChinaName,@EnglishName,@ContactSex,@ContactTel,@Remark,@NationCode,@VistorType,@DataType) ";
        /// <summary>
        /// 酒店单个新增
        /// </summary>
        private const string Sql_Insert_HotelVistorInfo = "if NOT EXISTS(SELECT 1 FROM [tbl_TicketVistorInfo] WHERE [CompanyId]=@CompanyId AND [ContactTel]=@ContactTel AND [EnglishName]=@EnglishName AND [ChinaName]=@ChinaName AND [DataType]='2') INSERT INTO tbl_TicketVistorInfo ([id],[CompanyId],[CardType],[CardNo],[ChinaName],[EnglishName],[ContactSex],[ContactTel],[Remark],[NationCode],[VistorType],[DataType]) VALUES (@ID,@CompanyId,@CardType,@CardNo,@ChinaName,@EnglishName,@ContactSex,@ContactTel,@Remark,@NationCode,@VistorType,@DataType) ";
        /// <summary>
        /// 修改机票常旅客
        /// </summary>
        private const string Sql_Update_TicketVistorInfo = " UPDATE tbl_TicketVistorInfo SET [CardType]=@CardType,[CardNo]=@CardNo,[ChinaName]=@ChinaName,[EnglishName]=@EnglishName,[ContactSex]=@ContactSex,[ContactTel]=@ContactTel,[Remark]=@Remark,[NationCode]=@NationCode,[VistorType]=@VistorType WHERE [ID]=@ID AND CompanyId=@CompanyId ";*/
        /// <summary>
        /// 删除常旅客
        /// </summary>
        private const string Sql_Delete_TicketVistorInfo = " DELETE FROM tbl_TicketVistorInfo WHERE [ID]=@ID ";

        const string SQL_INSERT_Insert = @" INSERT INTO [tbl_TicketVistorInfo]([ID],[CompanyId],[CardType],[CardNo],[ChinaName],[EnglishName],[ContactSex],[ContactTel],[Remark],[NationCode],[VistorType],[IssueTime],[DataType],[Mobile],[MailingAddress],[ZipCode],[Birthday],[IDCard],[Passport],[CountryId],[ProvinceId],[CityId],[CountyId]) VALUES (@ID{0},@CompanyId{0},@CardType{0},@CardNo{0},@ChinaName{0},@EnglishName{0},@ContactSex{0},@ContactTel{0},@Remark{0},@NationCode{0},@VistorType{0},@IssueTime{0},@DataType{0},@Mobile{0},@MailingAddress{0},@ZipCode{0},@Birthday{0},@IDCard{0},@Passport{0},@CountryId{0},@ProvinceId{0},@CityId{0},@CountyId{0});";
        const string SQL_UPDATE_Update = @" UPDATE [tbl_TicketVistorInfo] 
                SET [CardType] = @CardType,
                [CardNo] = @CardNo,
                [ChinaName] = @ChinaName,
                [EnglishName] = @EnglishName,
                [ContactSex] = @ContactSex,
                [ContactTel] =@ContactTel,
                [Remark] = @Remark,
                [NationCode] =@NationCode,
                [VistorType] = @VistorType,
                [DataType] = @DataType,
                [Mobile] = @Mobile,
                [MailingAddress] = @MailingAddress,
                [ZipCode] =@ZipCode,
                [Birthday] = @Birthday,
                [IDCard] =@IDCard,
                [Passport] = @Passport,
                [CountryId] = @CountryId,
                [ProvinceId] = @ProvinceId,
                [CityId] = @CityId,
                [CountyId] = @CountyId 
                WHERE [Id]=@Id ";
   
        #region constructor
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public TicketVistorInfo() { this._db = this.TicketStore; }
        #endregion

        #endregion

        #region Public Methods
        /// <summary>
        /// 获取常旅客实体
        /// </summary>
        /// <param name="TicketVistorId">常旅客Id</param>
        /// <returns>返回获取常旅客实体</returns>
        public virtual EyouSoft.Model.TicketStructure.TicketVistorInfo GetModel(string TicketVistorId)
        {
            EyouSoft.Model.TicketStructure.TicketVistorInfo model = null;
            DbCommand dc = this._db.GetSqlStringCommand(Sql_Select_TicketVistorInfo_ByID);
            this._db.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, TicketVistorId);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TicketStructure.TicketVistorInfo();
                    InputModelValue(model, dr);
                }
            }
            return model;
        }
        /*/// <summary>
        /// 获取常旅客信息
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="CompanyId">公司id</param>
        /// <param name="VistorName">常旅客姓名</param>
        /// <param name="VistorType">旅客类型（0：成人，1：儿童，2：婴儿），为null时不做条件</param>
        /// <param name="DataType">数据来源</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> GetTicketVistorList(int PageSize, int PageIndex, ref int RecordCount, string CompanyId, string VistorName, EyouSoft.Model.TicketStructure.TicketVistorType? VistorType,EyouSoft.Model.TicketStructure.TicketDataType? DataType)
        {
            IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> ResultList = new List<EyouSoft.Model.TicketStructure.TicketVistorInfo>();
            string StrTableName = "tbl_TicketVistorInfo";
            string StrFiledName = "*";
            string StrWhereQuery = string.Format("[CompanyId]='{0}' ", CompanyId);
            if (DataType.HasValue)
            {
                StrWhereQuery += string.Format(" AND [DataType]='{1}' ", (int)DataType.Value);
            }
            string StrOrderBy = " IssueTime ASC";

            #region StrWhereQuery处理
            if (!string.IsNullOrEmpty(VistorName))
            {
                if (IsLetter(VistorName))
                {
                    StrWhereQuery += string.Format(" AND EnglishName LIKE '%{0}%' ", VistorName);
                }
                else
                {
                    StrWhereQuery += string.Format(" AND ChinaName LIKE '%{0}%' ", VistorName);
                }
            }
            if (VistorType != null)
            {
                StrWhereQuery += string.Format(" AND VistorType='{0}' ", (int)VistorType);
            }
            #endregion

            #region List赋值
            using (IDataReader dr = DbHelper.ExecuteReader(this._db, PageSize, PageIndex, ref RecordCount, StrTableName, "ID", StrFiledName, StrWhereQuery, StrOrderBy))
            {
                EyouSoft.Model.TicketStructure.TicketVistorInfo model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TicketStructure.TicketVistorInfo();
                    InputModelValue(model, dr);
                    ResultList.Add(model);
                }
                model = null;
            }
            #endregion
            return ResultList;
        }*/
        /*/// <summary>
        /// 根据输入的旅客姓名模糊查找常旅客信息
        /// </summary>
        /// <param name="VistorName">常旅客姓名</param>
        /// <param name="CompanyId">公司Id</param>
        /// <param name="DataType">数据来源</param>
        /// <returns>返回获取常旅客信息集合</returns>
        public virtual IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> GetTicketVistorListByName(string VistorName, string CompanyId, EyouSoft.Model.TicketStructure.TicketDataType? DataType)
        {
            IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> ResultList = new List<EyouSoft.Model.TicketStructure.TicketVistorInfo>();
            DbCommand dc = null;
            string StrSql = string.Format(" SELECT * FROM tbl_TicketVistorInfo WHERE [CompanyId]=@CompanyId ");
            if (DataType.HasValue)
            {
                StrSql += string.Format(" AND DataType='{0}' ",(int)DataType.Value);
            }
            if (!string.IsNullOrEmpty(VistorName))
            {
                if (IsLetter(VistorName))
                {
                    StrSql += " AND EnglishName LIKE @VistorName ";
                    VistorName = "%" + VistorName + "%";
                    dc = this._db.GetSqlStringCommand(StrSql);
                    this._db.AddInParameter(dc, "VistorName", DbType.AnsiString, VistorName);
                }
                else
                {
                    StrSql += " AND ChinaName LIKE @VistorName ";
                    VistorName = "%" + VistorName + "%";
                    dc = this._db.GetSqlStringCommand(StrSql);
                    this._db.AddInParameter(dc, "VistorName", DbType.String, VistorName);
                }
            }
            dc = this._db.GetSqlStringCommand(StrSql);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            #region List赋值
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                EyouSoft.Model.TicketStructure.TicketVistorInfo model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TicketStructure.TicketVistorInfo();
                    InputModelValue(model, dr);
                    ResultList.Add(model);
                }
                model = null;
            }
            #endregion
            dc = null;
            return ResultList;
        }*/
        /// <summary>
        /// 判断常旅客是否存在
        /// </summary>
        /// <param name="CardNo">常旅客证件号</param>
        /// <param name="CompanyId">公司Id</param>
        /// <param name="VistorId">常旅客信息主键Id,若为新增,则传空</param>
        /// <returns>true：成功，false：失败</returns>
        public virtual bool IsExist(string CardNo, string CompanyId, string VistorId)
        {
            bool IsTrue = false;
            string StrSql = "SELECT 1 FROM tbl_TicketVistorInfo WHERE [CompanyId]=@CompanyId AND [CardNo]=@CardNo ";
            DbCommand dc = null;
            if (!string.IsNullOrEmpty(VistorId))
            {
                StrSql += " AND [ID]<>@ID ";
            }
            dc = this._db.GetSqlStringCommand(StrSql);
            if (!string.IsNullOrEmpty(VistorId))
            {
                this._db.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, VistorId);
            }
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._db.AddInParameter(dc, "CardNo", DbType.String, CardNo);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    IsTrue = true;
                }
            }
            return IsTrue;
        }
        /// <summary>
        /// 判断酒店常旅客是否存在
        /// </summary>
        /// <param name="ChinaName">中文名字</param>
        /// <param name="EnglishName">英文名字</param>
        /// <param name="ContactTel">联系电话</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="VistorId">常旅客信息主键Id,若为新增,则传空</param>
        /// <returns></returns>
        public virtual bool IsExist(string ChinaName, string EnglishName, string ContactTel, string CompanyId, string VistorId)
        {
            bool IsTrue = false;
            string StrSql = "SELECT 1 FROM tbl_TicketVistorInfo WHERE [CompanyId]=@CompanyId AND [ContactTel]=@ContactTel AND [ChinaName]=@ChinaName AND [EnglishName]=@EnglishName ";
            if (!string.IsNullOrEmpty(VistorId))
            {
                StrSql += " AND [ID]<>@ID ";
            }
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            if (!string.IsNullOrEmpty(VistorId))
            {
                this._db.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, VistorId);
            }
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._db.AddInParameter(dc, "ContactTel", DbType.String, ContactTel);
            this._db.AddInParameter(dc, "ChinaName", DbType.String, ChinaName);
            this._db.AddInParameter(dc, "EnglishName", DbType.String, EnglishName);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    IsTrue = true;
                }
            }
            return IsTrue;
        }

        /*/// <summary>
        /// 添加常旅客
        /// </summary>
        /// <param name="model">常旅客信息实体</param>
        /// <returns>1：成功，0：失败</returns>
        public virtual int Add(EyouSoft.Model.TicketStructure.TicketVistorInfo model)
        {
            int EffectCount = 0;
            string StrSql = string.Empty;
            #region 判断常旅客类型
            switch (model.DataType)
            {
                case EyouSoft.Model.TicketStructure.TicketDataType.酒店常旅客:
                    StrSql = Sql_Insert_HotelVistorInfo;
                    break;
                case EyouSoft.Model.TicketStructure.TicketDataType.机票常旅客:
                default:
                    StrSql = Sql_Insert_TicketVistorInfo;
                    break;
            }
            #endregion
            if (model != null)
            {
                DbCommand dc = this._db.GetSqlStringCommand(StrSql);
                this._db.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.Id);
                this._db.AddInParameter(dc, "CardNo", DbType.String, model.CardNo);
                this._db.AddInParameter(dc, "CardType", DbType.Int32, (int)model.CardType);
                this._db.AddInParameter(dc, "ChinaName", DbType.String, model.ChinaName);
                this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
                this._db.AddInParameter(dc, "ContactSex", DbType.AnsiStringFixedLength, (int)model.ContactSex);
                this._db.AddInParameter(dc, "ContactTel", DbType.String, model.ContactTel);
                this._db.AddInParameter(dc, "EnglishName", DbType.AnsiStringFixedLength, model.EnglishName);
                this._db.AddInParameter(dc, "NationCode", DbType.Int32, model.NationInfo.NationId);
                this._db.AddInParameter(dc, "Remark", DbType.String, model.Remark);
                this._db.AddInParameter(dc, "VistorType", DbType.Byte, (int)model.VistorType);
                //1：机票；2：酒店
                this._db.AddInParameter(dc, "DataType", DbType.AnsiStringFixedLength, (int)model.DataType);
                EffectCount = DbHelper.ExecuteSql(dc, this._db);
            }
            return EffectCount;
        }*/

        /// <summary>
        /// 批量添加常旅客
        /// </summary>
        /// <param name="VistorList">常旅客信息集合</param>
        /// <returns>大于等于1：成功，0：失败</returns>
        public virtual int AddVistorList(IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> VistorList)
        {
            int i = 0;
            StringBuilder cmdText = new StringBuilder();
            DbCommand cmd = _db.GetSqlStringCommand("SELECT 1");
            foreach (var item in VistorList)
            {
                cmdText.AppendFormat(SQL_INSERT_Insert, i.ToString());

                _db.AddInParameter(cmd, "ID" + i.ToString(), DbType.AnsiStringFixedLength, item.Id);
                _db.AddInParameter(cmd, "CompanyId" + i.ToString(), DbType.AnsiStringFixedLength, item.CompanyId);
                _db.AddInParameter(cmd, "CardType" + i.ToString(), DbType.Int32, item.CardType);
                _db.AddInParameter(cmd, "CardNo" + i.ToString(), DbType.String, item.CardNo);
                _db.AddInParameter(cmd, "ChinaName" + i.ToString(), DbType.String, item.ChinaName);
                _db.AddInParameter(cmd, "EnglishName" + i.ToString(), DbType.String, item.EnglishName);
                _db.AddInParameter(cmd, "ContactSex" + i.ToString(), DbType.AnsiStringFixedLength, (int)item.ContactSex);
                _db.AddInParameter(cmd, "ContactTel" + i.ToString(), DbType.String, item.ContactTel);
                _db.AddInParameter(cmd, "Remark" + i.ToString(), DbType.String, item.Remark);
                _db.AddInParameter(cmd, "NationCode" + i.ToString(), DbType.String, item.NationInfo.NationId);
                _db.AddInParameter(cmd, "VistorType" + i.ToString(), DbType.Byte, item.VistorType);
                _db.AddInParameter(cmd, "IssueTime" + i.ToString(), DbType.DateTime, item.IssueTime);
                _db.AddInParameter(cmd, "DataType" + i.ToString(), DbType.AnsiStringFixedLength, (int)item.DataType);
                _db.AddInParameter(cmd, "Mobile" + i.ToString(), DbType.String, item.Mobile);
                _db.AddInParameter(cmd, "MailingAddress" + i.ToString(), DbType.String, item.Address);
                _db.AddInParameter(cmd, "ZipCode" + i.ToString(), DbType.String, item.ZipCode);
                if (item.BirthDay.HasValue)
                    _db.AddInParameter(cmd, "Birthday" + i.ToString(), DbType.DateTime, item.BirthDay);
                else
                    _db.AddInParameter(cmd, "Birthday" + i.ToString(), DbType.DateTime, DBNull.Value);
                _db.AddInParameter(cmd, "IDCard" + i.ToString(), DbType.String, item.IdCardCode);
                _db.AddInParameter(cmd, "Passport" + i.ToString(), DbType.String, item.PassportCode);
                _db.AddInParameter(cmd, "CountryId" + i.ToString(), DbType.Int32, item.CountryId);
                _db.AddInParameter(cmd, "ProvinceId" + i.ToString(), DbType.Int32, item.ProvinceId);
                _db.AddInParameter(cmd, "CityId" + i.ToString(), DbType.Int32, item.CityId);
                _db.AddInParameter(cmd, "CountyId" + i.ToString(), DbType.Int32, item.DistrictId);

                i++;
            }

            cmd.CommandText = cmdText.ToString();

            return DbHelper.ExecuteSql(cmd, this._db);
        }
        /// <summary>
        /// 修改常旅客
        /// </summary>
        /// <param name="model">常旅客信息实体</param>
        /// <returns>1：成功，0：失败</returns>
        public virtual int Update(EyouSoft.Model.TicketStructure.TicketVistorInfo model)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_Update);

            _db.AddInParameter(cmd, "ID" , DbType.AnsiStringFixedLength, model.Id);
            _db.AddInParameter(cmd, "CardType" , DbType.Int32, model.CardType);
            _db.AddInParameter(cmd, "CardNo" , DbType.String, model.CardNo);
            _db.AddInParameter(cmd, "ChinaName" , DbType.String, model.ChinaName);
            _db.AddInParameter(cmd, "EnglishName" , DbType.String, model.EnglishName);
            _db.AddInParameter(cmd, "ContactSex" , DbType.AnsiStringFixedLength, (int)model.ContactSex);
            _db.AddInParameter(cmd, "ContactTel" , DbType.String, model.ContactTel);
            _db.AddInParameter(cmd, "Remark" , DbType.String, model.Remark);
            _db.AddInParameter(cmd, "NationCode" , DbType.String, model.NationInfo.NationId);
            _db.AddInParameter(cmd, "VistorType" , DbType.Byte, model.VistorType);
            _db.AddInParameter(cmd, "DataType" , DbType.AnsiStringFixedLength, (int)model.DataType);
            _db.AddInParameter(cmd, "Mobile" , DbType.String, model.Mobile);
            _db.AddInParameter(cmd, "MailingAddress" , DbType.String, model.Address);
            _db.AddInParameter(cmd, "ZipCode" , DbType.String, model.ZipCode);
            if (model.BirthDay.HasValue)
                _db.AddInParameter(cmd, "Birthday" , DbType.DateTime, model.BirthDay);
            else
                _db.AddInParameter(cmd, "Birthday" , DbType.DateTime, DBNull.Value);
            _db.AddInParameter(cmd, "IDCard" , DbType.String, model.IdCardCode);
            _db.AddInParameter(cmd, "Passport" , DbType.String, model.PassportCode);
            _db.AddInParameter(cmd, "CountryId" , DbType.Int32, model.CountryId);
            _db.AddInParameter(cmd, "ProvinceId" , DbType.Int32, model.ProvinceId);
            _db.AddInParameter(cmd, "CityId" , DbType.Int32, model.CityId);
            _db.AddInParameter(cmd, "CountyId" , DbType.Int32, model.DistrictId);

            return DbHelper.ExecuteSql(cmd, this._db);
        }
        /// <summary>
        /// 删除常旅客信息
        /// </summary>
        /// <param name="TicketVistorIds">常旅客主键Id</param>
        /// <returns>true：成功，false：失败</returns>
        public virtual bool Delete(params string[] TicketVistorIds)
        {
            bool IsTrue = false;
            if (TicketVistorIds == null || TicketVistorIds.Length == 0) return IsTrue;
            int EffectCount = 0;
            DbCommand dc = null;
            foreach (string id in TicketVistorIds)
            {
                dc = base.TourStore.GetSqlStringCommand(Sql_Delete_TicketVistorInfo);
                this._db.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, id);
                EffectCount = DbHelper.ExecuteSql(dc, this._db);
                EffectCount += EffectCount;
            }
            if (EffectCount > 0) IsTrue = true;
            return IsTrue;
        }

        /// <summary>
        /// 获取常旅客信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="isPaging">是否分页</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> GetVisitors(int pageSize, int pageIndex, ref int recordCount, string companyId,bool isPaging, EyouSoft.Model.TicketStructure.MVisitorSearchInfo searchInfo)
        {
            IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> items = new List<EyouSoft.Model.TicketStructure.TicketVistorInfo>();
            string tableName = "tbl_TicketVistorInfo";
            string fileds = "*";
            string orderby = " IssueTime ASC";
            StringBuilder query = new StringBuilder();

            #region 拼接查询
            query.AppendFormat(" CompanyId='{0}' ", companyId);

            if (searchInfo != null)
            {
                if (searchInfo.Type.HasValue)
                {
                    query.AppendFormat(" AND [DataType]='{0}' ", (int)searchInfo.Type.Value);
                }

                if (!string.IsNullOrEmpty(searchInfo.Name))
                {
                    if (IsLetter(searchInfo.Name))
                    {
                        query.AppendFormat(" AND EnglishName LIKE '%{0}%' ", searchInfo.Name);
                    }
                    else
                    {
                        query.AppendFormat(" AND ChinaName LIKE '%{0}%' ", searchInfo.Name);
                    }
                }

                if (searchInfo.VType.HasValue)
                {
                    query.AppendFormat(" AND VistorType='{0}' ", (int)searchInfo.VType);
                }

                if(!string.IsNullOrEmpty(searchInfo.KeyWord))
                {
                    query.AppendFormat(
                        " AND isnull(ChinaName,'') + isnull(EnglishName,'') + isnull(Mobile,'') like '%{0}%' ",
                        searchInfo.KeyWord);
                }
            }
            #endregion

            using (IDataReader dr = isPaging ? DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, tableName, "ID", fileds, query.ToString(), orderby)
                : DbHelper.ExecuteReader(_db, tableName, fileds, query.ToString(), orderby))
            {
                while (dr.Read())
                {
                    var item = new EyouSoft.Model.TicketStructure.TicketVistorInfo();
                    InputModelValue(item, dr);
                    items.Add(item);
                }
            }

            return items;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// 常旅客实体赋值
        /// </summary>
        /// <param name="model">常旅客实体</param>
        /// <param name="dr">IDataReader</param>
        private void InputModelValue(EyouSoft.Model.TicketStructure.TicketVistorInfo model, IDataReader dr)
        {
            model.Id = dr.GetString(dr.GetOrdinal("Id"));
            model.CardNo = dr.GetString(dr.GetOrdinal("CardNo"));
            model.CardType = (EyouSoft.Model.TicketStructure.TicketCardType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.TicketCardType), dr["CardType"].ToString());
            model.ChinaName = dr.IsDBNull(dr.GetOrdinal("ChinaName")) ? string.Empty : dr.GetString(dr.GetOrdinal("ChinaName"));
            model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
            if (!string.IsNullOrEmpty(dr["ContactSex"].ToString()))
            {
                model.ContactSex = (EyouSoft.Model.CompanyStructure.Sex)Enum.Parse(typeof(EyouSoft.Model.CompanyStructure.Sex), dr["ContactSex"].ToString());
            }
            model.ContactTel = dr.IsDBNull(dr.GetOrdinal("ContactTel")) ? string.Empty : dr.GetString(dr.GetOrdinal("ContactTel"));
            model.EnglishName = dr.IsDBNull(dr.GetOrdinal("EnglishName")) ? string.Empty : dr.GetString(dr.GetOrdinal("EnglishName"));
            model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));            
            model.Remark = dr.IsDBNull(dr.GetOrdinal("Remark")) ? string.Empty : dr.GetString(dr.GetOrdinal("Remark"));
            model.VistorType = (EyouSoft.Model.TicketStructure.TicketVistorType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.TicketVistorType), dr["VistorType"].ToString());

            //model.NationInfo.CountryName = dr.IsDBNull(dr.GetOrdinal("CountryName")) ? string.Empty : dr.GetString(dr.GetOrdinal("CountryName"));
            //model.NationInfo.CountryCode = dr.IsDBNull(dr.GetOrdinal("CountryCode")) ? string.Empty : dr.GetString(dr.GetOrdinal("CountryCode"));
            //model.NationInfo.NationId = dr.IsDBNull(dr.GetOrdinal("NationId")) ? 0 : dr.GetInt32(dr.GetOrdinal("NationId"));

            model.DataType = (EyouSoft.Model.TicketStructure.TicketDataType)int.Parse(dr.GetString(dr.GetOrdinal("DataType")));
            model.Mobile = dr["Mobile"].ToString();
            model.Address = dr["MailingAddress"].ToString();
            model.ZipCode = dr["ZipCode"].ToString();
            if (!dr.IsDBNull(dr.GetOrdinal("Birthday")))
                model.BirthDay = dr.GetDateTime(dr.GetOrdinal("Birthday"));
            model.IdCardCode = dr["IDCard"].ToString();
            model.PassportCode = dr["Passport"].ToString();
            model.CountryId = dr.GetInt32(dr.GetOrdinal("CountryId"));
            model.ProvinceId = dr.GetInt32(dr.GetOrdinal("ProvinceId"));
            model.CityId = dr.GetInt32(dr.GetOrdinal("CityId"));
            model.DistrictId = dr.GetInt32(dr.GetOrdinal("CountyId"));
        }

        /// <summary>
        /// 判断首字符是否为字母
        /// </summary>
        /// <param name="CheckString"></param>
        /// <returns></returns>
        private bool IsLetter(string CheckString)
        {
            bool IsTrue = false;
            if (!string.IsNullOrEmpty(CheckString))
            {
                Regex rx = new Regex("^[\u4e00-\u9fa5]$");
                IsTrue = rx.IsMatch(CheckString.Substring(0, 1));
                rx = null;
            }
            return !IsTrue;
        }
        #endregion

    }
}
