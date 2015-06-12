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

namespace EyouSoft.DAL.MQStructure
{
    /// <summary>
    /// 创建人:张志瑜 2010-05-21
    /// 描述:诚信体系-公司的信用分值与次数汇总信息数据访问类
    /// </summary>
    public class RateScore : DALBase, EyouSoft.IDAL.CreditSystemStructure.IRateScore
    {
        #region IRateScore 成员
        //static constants
        private const string SQL_RateScoreTotal_SELECT = "SELECT ScorePoint,ScoreType FROM tbl_RateScoreTotal WHERE CompanyId=@COMPANYID;";
        private const string SQL_RateCountTotal_SELECT = "SELECT ScoreNumber,ScoreType FROM tbl_RateCountTotal WHERE CompanyId=@COMPANYID;";
        private const string SQL_CompanyInfo_SELECT = "SELECT IsCertificateCheck,IsCreditCheck FROM tbl_CompanyInfo WHERE Id=@COMPANYID;";
        private const string SQL_CompanyAttachInfo_SELECT = "SELECT FieldName,FieldValue FROM tbl_CompanyAttachInfo WHERE CompanyId=@COMPANYID AND FieldName IN ('LicenceImg', 'BusinessCertImg', 'TaxRegImg');";        
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _databse = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        public RateScore()
        {
            this._databse = base.CompanyStore;
        }

        /// <summary>
        /// 获取指定公司的信用分值与次数汇总信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="certificateScore">1份证书所获得的分值</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.CreditSystemStructure.RateDetailTotal GetCollectsInfo(string companyId, double certificateScore)
        {
            EyouSoft.Model.CreditSystemStructure.RateDetailTotal info = new EyouSoft.Model.CreditSystemStructure.RateDetailTotal();
            bool isRateCertificateCheck = false;   //诚信档案中的证书有无审核通过
            info.CompanyId = companyId;
            //注意:using里面的顺序是和GetSqlStringCommand中参数的sql语句顺序有关,因此不要随意修改每个sql语句之间的顺序
            DbCommand dc = this._databse.GetSqlStringCommand(SQL_RateScoreTotal_SELECT + SQL_RateCountTotal_SELECT + SQL_CompanyInfo_SELECT + SQL_CompanyAttachInfo_SELECT);
            this._databse.AddInParameter(dc, "COMPANYID", DbType.AnsiStringFixedLength, companyId);
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this._databse))
            {
                #region 分值信息
                while (rdr.Read())
                {
                    double scores = Convert.ToDouble(rdr.GetInt32(rdr.GetOrdinal("ScorePoint")));
                    int scoreType = rdr.GetInt32(rdr.GetOrdinal("ScoreType"));

                    switch (scoreType)
                    {
                        case 1:
                            //交易总分值
                            info.RateCreditDetail.BuyScore = scores;
                            break;
                        case 2:
                            //评价总分值
                            info.RateCreditDetail.JudgeScore = scores;
                            break;
                        case 3:
                            //服务品质总星星数
                            info.RateCreditDetail.ServiceQualityNum = scores;
                            break;
                        case 4:
                            //性价比总星星数
                            info.RateCreditDetail.PriceQualityNum = scores;
                            break;
                        case 5:
                            //旅游内容安排总星星数
                            info.RateCreditDetail.TravelPlanNum = scores;
                            break;
                        case 6:
                            //活跃总分值
                            info.ActiveScore = scores;
                            break;
                        case 7:
                            //推荐总分值
                            info.HoldUpScore = scores;
                            break;
                    }
                }
                #endregion 分值信息

                #region 次数信息
                while (rdr.Read())
                {
                    int times = rdr.GetInt32(rdr.GetOrdinal("ScoreNumber"));
                    int timesType = rdr.GetInt32(rdr.GetOrdinal("ScoreType"));

                    switch (timesType)
                    {
                        case 1:
                            //交易总次数
                            info.RateCreditDetail.BuyCount = times;
                            break;
                        case 2:
                            //好评总次数
                            info.RateCreditDetail.GoodCount = times;
                            break;
                        case 3:
                            //中评总次数
                            info.RateCreditDetail.MiddleCount = times;
                            break;
                        case 4:
                            //差评总次数
                            info.RateCreditDetail.BadCount = times;
                            break;
                        case 5:
                            //活跃总次数
                            info.ActivityCount = times;
                            break;
                        case 6:
                            //推荐总次数
                            info.HoldUpCount = times;
                            break;
                    }
                }
                #endregion 次数信息

                #region 承诺书以及证书信息有无审核
                if (rdr.Read())
                {
                    info.IsHasPromises = Convert.ToBoolean(rdr.GetString(rdr.GetOrdinal("IsCreditCheck")));
                    isRateCertificateCheck = Convert.ToBoolean(rdr.GetString(rdr.GetOrdinal("IsCertificateCheck")));
                }
                #endregion 承诺书信息

                #region 证书信息
                if (isRateCertificateCheck)
                {
                    while (rdr.Read())
                    {
                        //double scores = Convert.ToDouble(rdr.GetInt32(rdr.GetOrdinal("")));
                        double scores = certificateScore;
                        string cType = rdr.GetString(rdr.GetOrdinal("FieldName"));
                        string cPath = rdr.GetString(rdr.GetOrdinal("FieldValue"));

                        switch (cType)
                        {
                            case "LicenceImg": //营业执照
                                info.RateCreditDetail.RateCertificate.ManageScore = scores;
                                info.RateCreditDetail.RateCertificate.ManageSrc = cPath;
                                break;
                            case "BusinessCertImg": //经营许可证
                                info.RateCreditDetail.RateCertificate.LicenceScore = scores;
                                info.RateCreditDetail.RateCertificate.LicenceSrc = cPath;
                                break;
                            case "TaxRegImg":  //税务登录证
                                info.RateCreditDetail.RateCertificate.TaxScore = scores;
                                info.RateCreditDetail.RateCertificate.TaxSrc = cPath;
                                break;
                        }
                    }
                }
                #endregion 证书信息

                //计算总的分值
                info.CreditScore = info.RateCreditDetail.BuyScore + info.RateCreditDetail.JudgeScore + info.RateCreditDetail.RateCertificate.TotalScore;
            }

            return info;
        }

        #endregion IRateScore 成员
    }
}
