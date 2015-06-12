using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using EyouSoft.Common.DAL;
using System.Xml.Linq;
using EyouSoft.Common;

namespace EyouSoft.DAL.HotelStructure
{
    /// <summary>
    /// 酒店数据访问类
    /// </summary>
    /// Author:汪奇志 2011-05-13
    public class DHotel:EyouSoft.Common.DAL.DALBase,EyouSoft.IDAL.HotelStructure.IHotel
    {
        #region static constants
        //static constants
        
        #endregion

        #region constructor
        /// <summary>
        /// database
        /// </summary>
        private Database _db = null;

        /// <summary>
        /// default constructor
        /// </summary>
        public DHotel() { this._db = this.HotelStore; }
        #endregion

        #region private members
        /// <summary>
        /// parse hotel img by xml
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HotelStructure.HotelImagesInfo> ParseHotelImgByXml(string xml)
        {
            //Columns:ImgCategory,FilePath
            if (string.IsNullOrEmpty(xml)) return null;
            IList<EyouSoft.Model.HotelStructure.HotelImagesInfo> items = new List<EyouSoft.Model.HotelStructure.HotelImagesInfo>();

            XElement xRoot = XElement.Parse(xml);
            var xRows = EyouSoft.Common.Utility.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                items.Add(new EyouSoft.Model.HotelStructure.HotelImagesInfo()
                {
                    Category = Utility.GetXAttributeValue(xRow, "ImgCategory"),
                    ImageURL = Utility.GetXAttributeValue(xRow, "FilePath")
                });
            }
            

            return items;
        }

        /// <summary>
        /// parse hotel room by xml
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HotelStructure.RoomTypeInfo> ParseHotelRoomByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<EyouSoft.Model.HotelStructure.RoomTypeInfo> items = new List<EyouSoft.Model.HotelStructure.RoomTypeInfo>();

            XElement xRoot = XElement.Parse(xml);
            var xRows = Utility.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HotelStructure.RoomTypeInfo item = new EyouSoft.Model.HotelStructure.RoomTypeInfo();
                item.RoomTypeName = Utility.GetXAttributeValue(xRow, "RoomName");
                item.BedType = Utility.GetXAttributeValue(xRow, "BedType");
                item.RoomTypeCode = Utility.GetXAttributeValue(xRow, "RoomTypeCode").Trim();
                item.RoomTypeId = Utility.GetXAttributeValue(xRow, "RoomTypeId");
                item.IsInternet = Utility.GetXAttributeValue(xRow, "IsInternet") == "1" ? true : false;
                item.VendorCode = Utility.GetXAttributeValue(xRow, "VendorCode").Trim();
                item.RatePlanCode = Utility.GetXAttributeValue(xRow, "RatePlanCode").Trim();

                item.RoomRate = new EyouSoft.Model.HotelStructure.RoomRateInfo();

                item.RoomRate.Internet = item.IsInternet;
                item.RoomRate.RateInfos = new List<EyouSoft.Model.HotelStructure.RateInfo>();
                EyouSoft.Model.HotelStructure.RateInfo rateItem = new EyouSoft.Model.HotelStructure.RateInfo();
                rateItem.AmountBeforeTax = Utility.GetDecimal(Utility.GetXAttributeValue(xRow, "AmountBeforeTax"));
                rateItem.AmountPrice = Utility.GetDecimal(Utility.GetXAttributeValue(xRow, "AmountPrice"));
                rateItem.DisplayPrice = Utility.GetDecimal(Utility.GetXAttributeValue(xRow, "DisplayPrice"));
                rateItem.FreeMeal = Utility.GetInt(Utility.GetXAttributeValue(xRow, "FreeMeal"));

                string ratePlanCommXML = Utility.GetXAttributeValue(xRow, "RatePlanCommXML");

                if (!string.IsNullOrEmpty(ratePlanCommXML))
                {
                    XElement xRatePlanCommRoot = XElement.Parse(ratePlanCommXML);
                    XElement xRatePlanCommRow = Utility.GetXElement(xRatePlanCommRoot, "row");

                    rateItem.CommissionType = (EyouSoft.HotelBI.HBECommissionType)Enum.Parse(typeof(EyouSoft.HotelBI.HBECommissionType), Utility.GetXAttributeValue(xRatePlanCommRow, "Commisiontype"));
                    rateItem.Percent = Utility.GetDecimal(Utility.GetXAttributeValue(xRatePlanCommRow, "Percent"));
                    rateItem.Fix = Utility.GetDecimal(Utility.GetXAttributeValue(xRatePlanCommRow, "Fix"));
                }

                item.RoomRate.RateInfos.Add(rateItem);

                items.Add(item);
            }

            return items;
        }

        /// <summary>
        /// ToHotelRand
        /// </summary>
        /// <param name="rankStr"></param>
        /// <returns></returns>
        private EyouSoft.HotelBI.HotelRankEnum ToHotelRanK(string rankStr)
        {
            if (string.IsNullOrEmpty(rankStr)) return EyouSoft.HotelBI.HotelRankEnum._00;
            return (EyouSoft.HotelBI.HotelRankEnum)Enum.Parse(typeof(EyouSoft.HotelBI.HotelRankEnum), "_" + rankStr);
        }
        #endregion

        #region IHotel 成员
        /// <summary>
        /// 获取酒店信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HotelStructure.HotelInfo> GetHotels(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HotelStructure.MLocalHotelSearchInfo searchInfo)
        {
            DateTime ratePlanStartDate = searchInfo != null ? searchInfo.RatePlanStartDate : DateTime.Today;
            IList<EyouSoft.Model.HotelStructure.HotelInfo> items = new List<EyouSoft.Model.HotelStructure.HotelInfo>();
            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "tbl_Hotel";
            string primaryKey = "HotelId";
            string orderByString = "CreateTime DESC";
            StringBuilder fields = new StringBuilder();

            #region fields
            fields.Append(" HotelId,HotelCode,AppCode,HotelName,HotelNameEn,CityCode,Latitude,Longitude,District,RankCode,LongDesc ");
            fields.Append(" ,(SELECT ImgCategory,FilePath FROM tbl_HotelImg AS A WHERE A.HotelId=tbl_Hotel.HotelId FOR XML RAW,ROOT('root')) AS HotelImgsXML ");
            /*,(SELECT A.VendorCode,A.RatePlanCode,A.Payment,A.AmountPrice,A.AmountBeforeTax,A.DisplayPrice 
,(SELECT B.[Percent],B.Fix,B.Commisiontype FROM tbl_HotelRatePlanComm AS B WHERE A.RoomTypeId=B.RoomTypeId AND A.VendorCode=B.VendorCode AND A.RatePlanCode=B.RatePlanCode {2:@RatePlanCommQuery} FOR XML RAW,ROOT('root')) AS RatePlanCommXML
,C.RoomName,C.BedType,C.RoomTypeCode,C.RoomTypeId,C.IsInternet 
FROM tbl_HotelRate AS A INNER JOIN tbl_HotelRoomType AS C ON A.RoomTypeId=C.RoomTypeId AND {1:@RoomTypeQuery}
WHERE A.HotelId=tbl_Hotel.HotelId AND {0:@RoomRateQuery} FOR XML RAW,ROOT('root')) AS HotelRoomsXML*/

            StringBuilder roomTypeQuery = new StringBuilder();
            if (searchInfo.IsInternet.HasValue 
                || !string.IsNullOrEmpty(searchInfo.SpecialRoomName) 
                || !string.IsNullOrEmpty(searchInfo.BedType))
            {
                if (searchInfo.IsInternet.HasValue)
                {
                    roomTypeQuery.AppendFormat(" AND  C.IsInternet='{0}'", searchInfo.IsInternet.Value ? "1" : "0");
                }
                if (!string.IsNullOrEmpty(searchInfo.SpecialRoomName))
                {
                    roomTypeQuery.AppendFormat(" AND C.RoomName LIKE '%{0}%' ", searchInfo.SpecialRoomName);
                }
                if (!string.IsNullOrEmpty(searchInfo.BedType))
                {
                    roomTypeQuery.AppendFormat(" AND C.BedType='{0}' ", searchInfo.BedType);
                }                
            }

            StringBuilder roomRateQuery = new StringBuilder();
            StringBuilder roomRatePlanQuery = new StringBuilder();
            
            roomRateQuery.AppendFormat(" AND '{0}' BETWEEN A.StartDate AND A.EndDate ", ratePlanStartDate);
            roomRatePlanQuery.AppendFormat(" AND '{0}' BETWEEN B.StartDate AND B.EndDate ", ratePlanStartDate);

            if (searchInfo != null)
            {
                if (searchInfo.PriceMaxRate.HasValue)
                {
                    roomRateQuery.AppendFormat(" AND A.AmountPrice<='{0}' ", searchInfo.PriceMaxRate.Value);
                }
                if (searchInfo.PriceMinRate.HasValue)
                {
                    roomRateQuery.AppendFormat(" AND A.AmountPrice>='{0}' ", searchInfo.PriceMinRate.Value);
                }
            }

            fields.AppendFormat(" ,(SELECT A.VendorCode,A.RatePlanCode,A.Payment,A.AmountPrice,A.AmountBeforeTax,A.DisplayPrice,A.FreeMeal ,(SELECT B.[Percent],B.Fix,B.Commisiontype FROM tbl_HotelRatePlanComm AS B WHERE A.RoomTypeId=B.RoomTypeId AND A.VendorCode=B.VendorCode AND A.RatePlanCode=B.RatePlanCode {2} FOR XML RAW,ROOT('root')) AS RatePlanCommXML ,C.RoomName,C.BedType,C.RoomTypeCode,C.RoomTypeId,C.IsInternet FROM tbl_HotelRate AS A INNER JOIN tbl_HotelRoomType AS C ON A.RoomTypeId=C.RoomTypeId AND C.IsDelete='0' {1} WHERE A.HotelId=tbl_Hotel.HotelId {0} FOR XML RAW,ROOT('root')) AS HotelRoomsXML ", roomRateQuery.ToString()
                , roomTypeQuery.ToString()
                , roomRatePlanQuery.ToString());
            #endregion

            #region 拼接查询条件
            cmdQuery.Append(" IsDelete='0' ");
            if (searchInfo != null)
            {
                if (!string.IsNullOrEmpty(searchInfo.CityCode))
                {
                    cmdQuery.AppendFormat(" AND CityCode='{0}' ", searchInfo.CityCode);
                }
                if (!string.IsNullOrEmpty(searchInfo.District))
                {
                    cmdQuery.AppendFormat(" AND District='{0}' ", searchInfo.District);
                }
                if (!string.IsNullOrEmpty(searchInfo.Fitment))
                {
                    cmdQuery.AppendFormat(" AND Fitment LIKE '{0}%' ", searchInfo.Fitment);
                }
                if (!string.IsNullOrEmpty(searchInfo.HotelName))
                {
                    cmdQuery.AppendFormat(" AND HotelName LIKE '%{0}%' ", searchInfo.HotelName);
                }
                if (!string.IsNullOrEmpty(searchInfo.HotelNameEn))
                {
                    cmdQuery.AppendFormat(" AND HotelNameEn LIKE '%{0}%' ", searchInfo.HotelNameEn);
                }
                if (!string.IsNullOrEmpty(searchInfo.Rank))
                {
                    cmdQuery.AppendFormat(" AND RankCode='{0}' ", searchInfo.Rank);
                }
                if (!string.IsNullOrEmpty(searchInfo.LandMarkName))
                {
                    cmdQuery.AppendFormat(" AND EXISTS (SELECT 1 FROM tbl_HotelLandMark AS A WHERE A.HotelId=tbl_Hotel.HotelId AND A.LandMarkName='{0}' ) ", searchInfo.LandMarkName);
                }
                if (searchInfo.IsInternet.HasValue
                    || !string.IsNullOrEmpty(searchInfo.SpecialRoomName)
                    || !string.IsNullOrEmpty(searchInfo.BedType))
                {
                    cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_HotelRoomType AS A WHERE A.HotelId=tbl_Hotel.HotelId AND A.IsDelete='0' ");

                    if (searchInfo.IsInternet.HasValue)
                    {
                        cmdQuery.AppendFormat(" AND  A.IsInternet='{0}'", searchInfo.IsInternet.Value ? "1" : "0");
                    }
                    if (!string.IsNullOrEmpty(searchInfo.SpecialRoomName))
                    {
                        cmdQuery.AppendFormat(" AND A.RoomName LIKE '%{0}%' ", searchInfo.SpecialRoomName);
                    }
                    if (!string.IsNullOrEmpty(searchInfo.BedType))
                    {
                        cmdQuery.AppendFormat(" AND A.BedType='{0}' ", searchInfo.BedType);
                    }                    
                    cmdQuery.Append(" ) ");
                }

                if (searchInfo.PriceMaxRate.HasValue
                    || searchInfo.PriceMinRate.HasValue)
                {
                    cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_HotelRate AS A WHERE A.HotelId=tbl_Hotel.HotelId ");
                    if (searchInfo.PriceMaxRate.HasValue)
                    {
                        cmdQuery.AppendFormat(" AND A.AmountPrice<={0} ", searchInfo.PriceMaxRate.Value);
                    }
                    if (searchInfo.PriceMinRate.HasValue)
                    {
                        cmdQuery.AppendFormat(" AND A.AmountPrice>={0} ", searchInfo.PriceMinRate.Value);
                    }
                    cmdQuery.AppendFormat(" AND '{0}' BETWEEN A.StartDate AND A.EndDate ", ratePlanStartDate);
                    cmdQuery.AppendFormat(")");
                }
            }
            #endregion

            #region 排序
            if (searchInfo != null)
            {
                switch (searchInfo.OrderBy)
                {
                    case EyouSoft.HotelBI.HotelOrderBy.Default: break;
                    case EyouSoft.HotelBI.HotelOrderBy.PRICEHTL: orderByString = "SortPrice DESC"; break;
                    case EyouSoft.HotelBI.HotelOrderBy.PRICELTH: orderByString = "SortPrice ASC"; break;
                    case EyouSoft.HotelBI.HotelOrderBy.STARHTL: orderByString = "RankCode DESC"; break;
                    case EyouSoft.HotelBI.HotelOrderBy.STARLTH: orderByString = "RankCode ASC"; break;
                }
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields.ToString(), cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.HotelStructure.HotelInfo item = new EyouSoft.Model.HotelStructure.HotelInfo();

                    item.AppCode = (EyouSoft.Model.HotelStructure.AppCode)rdr.GetByte(rdr.GetOrdinal("AppCode"));
                    item.CityCode = rdr["CityCode"].ToString().Trim();
                    item.District = rdr["District"].ToString();
                    item.HotelCode = rdr["HotelCode"].ToString().Trim();
                    item.HotelName = rdr["HotelName"].ToString();
                    item.HotelId = rdr.GetString(rdr.GetOrdinal("HotelId"));
                    item.LongDesc = rdr["LongDesc"].ToString();
                    item.ShortDesc = rdr["LongDesc"].ToString();
                    item.Rank = ToHotelRanK(rdr["RankCode"].ToString());
                    item.HotelPosition = new EyouSoft.Model.HotelStructure.HotelPositionInfo();
                    item.HotelPosition.Latitude = Utility.GetDouble(rdr["Latitude"].ToString());
                    item.HotelPosition.Longitude = Utility.GetDouble(rdr["Longitude"].ToString());
                    item.RoomTypeList = this.ParseHotelRoomByXml(rdr["HotelRoomsXML"].ToString());
                    item.HotelImages = this.ParseHotelImgByXml(rdr["HotelImgsXML"].ToString());

                    items.Add(item);
                }
            }

            return items;
        }
        #endregion
    }
}
