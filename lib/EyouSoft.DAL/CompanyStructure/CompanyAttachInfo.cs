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
    /// 描述：单位附件信息(宣传图片 企业LOGO 证件 承诺书等) 数据访问
    /// </summary>
    public class CompanyAttachInfo : DALBase, ICompanyAttachInfo
    {
        #region SQL变量
        /// <summary>
        /// 获取附件信息
        /// </summary>
        private const string SQL_CompanyAttachInfo_SELECT = "SELECT FieldName,FieldValue FROM tbl_CompanyAttachInfo WHERE CompanyId=@CompanyId";
        /// <summary>
        /// 修改公司附件单个信息
        /// </summary>
        private const string SQL_CompanyAttachInfo_UPDATE = @"IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]=@FieldName)=0 INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,@FieldName,@FieldValue);ELSE UPDATE [tbl_CompanyAttachInfo] SET [FieldValue]=@FieldValue WHERE [CompanyId]=@CompanyId AND [FieldName]=@FieldName;";
        /// <summary>
        /// 删除前移动文件记录
        /// </summary>
        private const string SQL_DELETEDFILE_MOVE = "IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]=@FieldName AND [FieldValue]=@FieldValue)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]=@FieldName AND FieldValue<>'';";
        #endregion

        /// <summary>
        /// 所在的数据库
        /// </summary>
        protected Database _database = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        public CompanyAttachInfo()
        {
            this._database = base.CompanyStore;
        }

        /// <summary>
        /// 设置公司附件信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="fieldName">要修改的字段名称</param>
        /// <param name="fieldValue">要修改的字段值</param>
        /// <returns></returns>
        protected virtual bool SetCompanyAttachInfo(string companyId, string fieldName, string fieldValue)
        {
            StringBuilder SQL = new StringBuilder();
            SQL.Append(" BEGIN TRAN CompanyAttach ");

            //移动之前删除文件
            SQL.AppendFormat(" {0} ", SQL_DELETEDFILE_MOVE);
            SQL.Append(" IF @@ERROR <> 0 BEGIN ROLLBACK TRAN;RETURN; END ");
            SQL.AppendFormat(" {0} ", SQL_CompanyAttachInfo_UPDATE);
            SQL.Append(" IF @@ERROR <> 0 BEGIN ROLLBACK TRAN;RETURN; END ");

            SQL.Append(" COMMIT TRAN CompanyAttach ");

            DbCommand dc = this._database.GetSqlStringCommand(SQL.ToString());
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._database.AddInParameter(dc, "FieldName", DbType.String, fieldName);
            this._database.AddInParameter(dc, "FieldValue", DbType.String, fieldValue);
            if (DbHelper.ExecuteSql(dc, this._database) > 0)
                return true;
            else
                return false;
        }
        #region 成员方法
        /// <summary>
        /// 设置公司LOGO
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="companyLogo">公司logo实体</param>
        /// <returns></returns>
        public virtual bool SetCompanyLogo(string companyId, EyouSoft.Model.CompanyStructure.CompanyLogo companyLogo)
        {
            return this.SetCompanyAttachInfo(companyId, "CompanyLogo", companyLogo.ImagePath);
        }

        /// <summary>
        /// 设置公司宣传图片
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="companyImg">公司宣传图片实体</param>
        /// <returns></returns>
        public virtual bool SetCompanyImage(string companyId, EyouSoft.Model.CompanyStructure.CompanyImg companyImg)
        {
            return this.SetCompanyAttachInfo(companyId, "CompanyImg", companyImg.ImagePath);
        }

        /// <summary>
        /// 设置公司高级网店头部
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="ShopBanner">公司高级网店头部实体</param>
        /// <returns></returns>
        public virtual bool SetCompanyShopBanner(string companyId, EyouSoft.Model.CompanyStructure.CompanyShopBanner ShopBanner)
        {
            StringBuilder SQL = new StringBuilder();
            IDictionary<string, string> parameter = new Dictionary<string, string>();

            switch (ShopBanner.BannerType)
            {
                case EyouSoft.Model.CompanyStructure.ShopBannerType.Default:
                    SQL.Append(this.BuildSQL(0, "CompanyLogo", ShopBanner.CompanyLogo, parameter));
                    SQL.Append(this.BuildSQL(1, "CSBannerBg", ShopBanner.BannerBackground, parameter));
                    break;
                case EyouSoft.Model.CompanyStructure.ShopBannerType.Personalize:
                    SQL.Append(this.BuildSQL(0, "CompanyShopBanner", ShopBanner.ImagePath, parameter));
                    break;
            }

            SQL.Append(this.BuildSQL(2, "CSBannerType", Convert.ToInt32(ShopBanner.BannerType).ToString(), parameter));

            DbCommand dc = this._database.GetSqlStringCommand(SQL.ToString());

            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            foreach (string key in parameter.Keys)
                this._database.AddInParameter(dc, key, DbType.String, parameter[key]);

            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }

        /// <summary>
        /// 设置企业名片
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="card">企业名片实体类</param>
        /// <returns></returns>
        public virtual bool SetCompanyCard(string companyId, EyouSoft.Model.CompanyStructure.CardInfo card)
        {
            StringBuilder SQL = new StringBuilder();
            IDictionary<string, string> parameter = new Dictionary<string, string>();

            SQL.Append(this.BuildSQL(0, "CompanyCard", card.ImagePath, parameter));
            SQL.Append(this.BuildSQL(2, "CompanyCardLink", card.ImageLink, parameter));

            DbCommand dc = this._database.GetSqlStringCommand(SQL.ToString());

            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            foreach (string key in parameter.Keys)
                this._database.AddInParameter(dc, key, DbType.String, parameter[key]);

            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }

        /// <summary>
        /// 设置公司MQ广告
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="mqAdv">公司MQ广告实体</param>
        /// <returns></returns>
        public virtual bool SetCompanyMQAdv(string companyId, EyouSoft.Model.CompanyStructure.CompanyMQAdv mqAdv)
        {
            StringBuilder SQL = new StringBuilder();
            IDictionary<string, string> parameter = new Dictionary<string, string>();

            SQL.Append(this.BuildSQL(0, "CompanyMQAdv", mqAdv.ImagePath, parameter));
            SQL.Append(this.BuildSQL(2, "CompanyMQAdvLink", mqAdv.ImageLink, parameter));

            DbCommand dc = this._database.GetSqlStringCommand(SQL.ToString());

            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            foreach (string key in parameter.Keys)
                this._database.AddInParameter(dc, key, DbType.String, parameter[key]);

            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 获得公司附件信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.CompanyStructure.CompanyAttachInfo GetModel(string companyId)
        {
            var model = new EyouSoft.Model.CompanyStructure.CompanyAttachInfo();
            model.CompanyPublicityPhoto = new List<Model.CompanyStructure.CompanyPublicityPhoto>();
            var photoList = new List<Model.CompanyStructure.CompanyPublicityPhoto>();

            model.CompanyId = companyId;

            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyAttachInfo_SELECT);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (rdr.Read())
                {
                    string FieldName = rdr.IsDBNull(rdr.GetOrdinal("FieldName")) == true ? "" : rdr.GetString(rdr.GetOrdinal("FieldName"));
                    string FieldValue = rdr.IsDBNull(rdr.GetOrdinal("FieldValue")) == true ? "" : rdr.GetString(rdr.GetOrdinal("FieldValue"));
                    switch (FieldName)
                    {
                        case "CompanyImg":
                            model.CompanyImg.ImagePath = FieldValue;
                            break;
                        case "CompanyImgLink":
                            model.CompanyImg.ImageLink = FieldValue;
                            break;
                        case "CompanyLogo":
                            model.CompanyLogo.ImagePath = FieldValue;
                            model.CompanyShopBanner.CompanyLogo = FieldValue;
                            break;
                        case "CompanyLogoThumb":
                            model.CompanyLogo.ThumbPath = FieldValue;
                            break;
                        case "LicenceImg":
                            model.BusinessCertif.LicenceImg = FieldValue;
                            break;
                        case "BusinessCertImg":
                            model.BusinessCertif.BusinessCertImg = FieldValue;
                            break;
                        case "TaxRegImg":
                            model.BusinessCertif.TaxRegImg = FieldValue;
                            break;
                        case "CommitmentImg":
                            model.CommitmentImg = FieldValue;
                            break;
                        case "CompanyCard":
                            model.CompanyCard.ImagePath = FieldValue;
                            break;
                        case "CompanyCardLink":
                            model.CompanyCard.ImageLink = FieldValue;
                            break;
                        case "CompanySignet":
                            model.CompanySignet = FieldValue;
                            break;
                        case "CompanyMQAdv":
                            model.CompanyMQAdv.ImagePath = FieldValue;
                            break;
                        case "CompanyMQAdvLink":
                            model.CompanyMQAdv.ImageLink = FieldValue;
                            break;
                        case "CompanyShopBanner":
                            model.CompanyShopBanner.ImagePath = FieldValue;
                            break;
                        case "TradeAward":
                            model.TradeAward = FieldValue;
                            break;
                        case "CSBannerBg":
                            model.CompanyShopBanner.BannerBackground = FieldValue;
                            break;
                        case "CSBannerType":
                            model.CompanyShopBanner.BannerType = (EyouSoft.Model.CompanyStructure.ShopBannerType)Enum.Parse(typeof(EyouSoft.Model.CompanyStructure.ShopBannerType), FieldValue);
                            break;

                        #region 2011-12-20 线路改版新加字段

                        case "WarrantImg":
                            model.BusinessCertif.WarrantImg = FieldValue;
                            break;
                        case "PersonCardImg":
                            model.BusinessCertif.PersonCardImg = FieldValue;
                            break;
                        case "PublicityPhoto1":
                            if (photoList.Any(item => item.PhotoIndex == 1))
                                photoList.Find(item => item.PhotoIndex == 1).ImagePath = FieldValue;
                            else
                                photoList.Add(new Model.CompanyStructure.CompanyPublicityPhoto { PhotoIndex = 1, ImagePath = FieldValue });
                            break;
                        case "PublicityPhoto1Thumb":
                            if (photoList.Any(item => item.PhotoIndex == 1))
                                photoList.Find(item => item.PhotoIndex == 1).ThumbPath = FieldValue;
                            else
                                photoList.Add(new Model.CompanyStructure.CompanyPublicityPhoto { PhotoIndex = 1, ThumbPath = FieldValue });
                            break;
                        case "PublicityPhoto2":
                            if (photoList.Any(item => item.PhotoIndex == 2))
                                photoList.Find(item => item.PhotoIndex == 2).ImagePath = FieldValue;
                            else
                                photoList.Add(new Model.CompanyStructure.CompanyPublicityPhoto { PhotoIndex = 2, ImagePath = FieldValue });
                            break;
                        case "PublicityPhoto2Thumb":
                            if (photoList.Any(item => item.PhotoIndex == 2))
                                photoList.Find(item => item.PhotoIndex == 2).ThumbPath = FieldValue;
                            else
                                photoList.Add(new Model.CompanyStructure.CompanyPublicityPhoto { PhotoIndex = 2, ThumbPath = FieldValue });
                            break;
                        case "PublicityPhoto3":
                            if (photoList.Any(item => item.PhotoIndex == 3))
                                photoList.Find(item => item.PhotoIndex == 3).ImagePath = FieldValue;
                            else
                                photoList.Add(new Model.CompanyStructure.CompanyPublicityPhoto { PhotoIndex = 3, ImagePath = FieldValue });
                            break;
                        case "PublicityPhoto3Thumb":
                            if (photoList.Any(item => item.PhotoIndex == 3))
                                photoList.Find(item => item.PhotoIndex == 3).ThumbPath = FieldValue;
                            else
                                photoList.Add(new Model.CompanyStructure.CompanyPublicityPhoto { PhotoIndex = 3, ThumbPath = FieldValue });
                            break;

                        #endregion
                    }
                }
            }

            model.CompanyPublicityPhoto = photoList;
            return model;
        }

        /// <summary>
        /// 构造SQL语句,公司字段名称为CompanyId
        /// </summary>
        /// <param name="index">字段索引 0:第1条sql语句  1:中间的sql语句  2:最后1条sql语句  3:即为第1条,又为最后1条sql语句</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="parameter">key为字段名称</param>
        /// <returns></returns>
        private string BuildSQL(int index, string fieldName, string fieldValue, IDictionary<string, string> parameter)
        {
            StringBuilder SQL = new StringBuilder();

            if (index == 0 || index == 3)
                SQL.Append(" BEGIN TRAN CompanyAttach ");
            //移动之前删除文件
            SQL.AppendFormat(" IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='{0}' AND [FieldValue]=@{0})=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='{0}' AND FieldValue<>''; ", fieldName);
            SQL.Append(" IF @@ERROR <> 0 BEGIN ROLLBACK TRAN;RETURN; END ");

            SQL.AppendFormat(" IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='{0}')=0 INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'{0}',@{0});ELSE UPDATE [tbl_CompanyAttachInfo] SET [FieldValue]=@{0} WHERE [CompanyId]=@CompanyId AND [FieldName]='{0}'; ", fieldName);
            SQL.Append(" IF @@ERROR <> 0 BEGIN ROLLBACK TRAN;RETURN; END ");

            if (index == 2 || index == 3)
                SQL.Append(" COMMIT TRAN CompanyAttach ");

            parameter.Add(fieldName, fieldValue);

            return SQL.ToString();
        }

        /// <summary>
        /// 设置公司所有的附件信息(必须保证每个不进行修改的字段,都需要保持原来本身的指)
        /// </summary>
        /// <param name="model">公司所有的附件信息</param>
        /// <returns></returns>
        public virtual bool SetCompanyAttachInfo(EyouSoft.Model.CompanyStructure.CompanyAttachInfo model)
        {
            StringBuilder SQL = new StringBuilder();
            IDictionary<string, string> parameter = new Dictionary<string, string>();

            SQL.Append(this.BuildSQL(0, "CompanyImg", model.CompanyImg.ImagePath, parameter));
            SQL.Append(this.BuildSQL(1, "CompanyImgLink", model.CompanyImg.ImageLink, parameter));
            SQL.Append(this.BuildSQL(1, "CompanyLogo", model.CompanyLogo.ImagePath, parameter));
            SQL.Append(this.BuildSQL(1, "CompanyLogoThumb", model.CompanyLogo.ThumbPath, parameter));
            SQL.Append(this.BuildSQL(1, "LicenceImg", model.BusinessCertif.LicenceImg, parameter));
            SQL.Append(this.BuildSQL(1, "BusinessCertImg", model.BusinessCertif.BusinessCertImg, parameter));
            SQL.Append(this.BuildSQL(1, "TaxRegImg", model.BusinessCertif.TaxRegImg, parameter));
            SQL.Append(this.BuildSQL(1, "CommitmentImg", model.CommitmentImg, parameter));
            SQL.Append(this.BuildSQL(1, "CompanyCard", model.CompanyCard.ImagePath, parameter));
            SQL.Append(this.BuildSQL(1, "CompanyCardLink", model.CompanyCard.ImageLink, parameter));
            SQL.Append(this.BuildSQL(1, "CompanySignet", model.CompanySignet, parameter));
            SQL.Append(this.BuildSQL(1, "CompanyMQAdv", model.CompanyMQAdv.ImagePath, parameter));
            SQL.Append(this.BuildSQL(1, "CompanyMQAdvLink", model.CompanyMQAdv.ImageLink, parameter));
            SQL.Append(this.BuildSQL(1, "CompanyShopBanner", model.CompanyShopBanner.ImagePath, parameter));
            SQL.Append(this.BuildSQL(1, "TradeAward", model.TradeAward, parameter));
            SQL.Append(this.BuildSQL(1, "CSBannerBg", model.CompanyShopBanner.BannerBackground, parameter));
            SQL.Append(this.BuildSQL(2, "CSBannerType", Convert.ToInt32(model.CompanyShopBanner.BannerType).ToString(), parameter));

            DbCommand dc = this._database.GetSqlStringCommand(SQL.ToString());

            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            foreach (string key in parameter.Keys)
                this._database.AddInParameter(dc, key, DbType.String, parameter[key]);

            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        #endregion
    }
}
