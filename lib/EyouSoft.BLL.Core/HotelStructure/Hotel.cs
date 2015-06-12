using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;
using System.Xml.Linq;
using EyouSoft.HotelBI;
using EyouSoft.Common;

namespace EyouSoft.BLL.HotelStructure
{
    /// <summary>
    /// 酒店业务逻辑
    /// </summary>
    /// 周文超 2010-12-03
    public class Hotel : IBLL.HotelStructure.IHotel
    {
        private readonly EyouSoft.IDAL.HotelStructure.IHotel dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.HotelStructure.IHotel>();

        #region create instance
        /// <summary>
        /// 构造系统用户业务逻辑接口
        /// </summary>
        /// <returns>系统用户业务逻辑接口</returns>
        public static IBLL.HotelStructure.IHotel CreateInstance()
        {
            IBLL.HotelStructure.IHotel op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.HotelStructure.IHotel>();
            }
            return op;
        }
        #endregion

        #region IHotel 成员

        /// <summary>
        /// 获取酒店集合
        /// </summary>
        /// <param name="QueryModel">酒店查询实体</param>
        /// <param name="RespPageInfo">酒店查询返回的分页信息</param>
        /// <param name="ErrModel">请求错误信息</param>
        /// <returns>酒店实体集合</returns>
        public IList<EyouSoft.Model.HotelStructure.HotelInfo> GetHotelList(HotelBI.MultipleSeach QueryModel, ref EyouSoft.Model.HotelStructure.RespPageInfo RespPageInfo, out HotelBI.ErrorInfo ErrModel)
        {
            ErrModel = null;
            if (QueryModel == null)
            {
                return null;
            }

            RespPageInfo = RespPageInfo ?? new EyouSoft.Model.HotelStructure.RespPageInfo()
            {
                CurrentPage = 1,
                PageSize = 10,
                TotalNum = 0,
                TotalPageNum = 0
            };

            RespPageInfo.CurrentPage = QueryModel.PageNo;
            RespPageInfo.PageSize = QueryModel.NumOfEachPage;            

            return this.GetLocalCacheHotels(QueryModel, RespPageInfo, out ErrModel);

            //return GetHotelListByXML(GetResponseXML(QueryModel.MultiRequestXML, out ErrModel), ref RespPageInfo);
        }

        /// <summary>
        /// 获取某酒店信息
        /// </summary>
        /// <param name="QueryModel">单酒店查询实体</param>
        /// <param name="ErrModel">请求错误信息</param>
        /// <returns>酒店信息实体</returns>
        public EyouSoft.Model.HotelStructure.HotelInfo GetHotelModel(HotelBI.SingleSeach QueryModel, out HotelBI.ErrorInfo ErrModel)
        {
            ErrModel = null;
            if (QueryModel == null)
                return null;

            return GetHotelModelByXML(GetResponseXML(QueryModel.SingleRequestXML, out ErrModel));
        }

        #endregion

        #region 私有函数  将接口返回的XML转换为平台酒店实体

        /// <summary>
        /// 向接口发送请求，返回接口返回的XML
        /// </summary>
        /// <param name="SearchXML">要发送到接口的XML</param>
        /// <param name="ErrModel">请求错误信息</param>
        /// <returns>接口返回的XML</returns>
        private string GetResponseXML(string SearchXML, out HotelBI.ErrorInfo ErrModel)
        {
            ErrModel = null;
            if (string.IsNullOrEmpty(SearchXML))
                return string.Empty;

            string strXML = HotelBI.Utils.CreateRequest(SearchXML);
            ErrModel = HotelBI.Utils.ResponseErrorHandling(strXML);
            if (ErrModel.ErrorType != EyouSoft.HotelBI.ErrorType.None)
                return string.Empty;

            return strXML;
        }

        #region 解析单、多酒店查询返回XML

        /// <summary>
        /// 将接口返回的XMl转换为平台酒店实体集合
        /// </summary>
        /// <param name="InterfaceReturnXML">接口返回的XMl</param>
        /// <param name="RespPageInfo">酒店查询返回的分页信息</param>
        /// <returns>平台酒店实体集合</returns>
        private IList<EyouSoft.Model.HotelStructure.HotelInfo> GetHotelListByXML(string InterfaceReturnXML
            , ref EyouSoft.Model.HotelStructure.RespPageInfo RespPageInfo)
        {
            IList<EyouSoft.Model.HotelStructure.HotelInfo> list = null;
            if (string.IsNullOrEmpty(InterfaceReturnXML))
                return list;

            list = new List<EyouSoft.Model.HotelStructure.HotelInfo>();
            XElement root = XElement.Parse(InterfaceReturnXML);
            XElement HotelAvailRS = HotelBI.Utils.GetXElement(root, "HotelAvailRS");

            GetRespPageInfo(HotelAvailRS, ref RespPageInfo);

            XElement RoomStays = HotelBI.Utils.GetXElement(HotelAvailRS, "RoomStays");
            IEnumerable<XElement> HotelRoots = HotelBI.Utils.GetXElements(RoomStays, "RoomStay");
            if (HotelRoots == null || HotelRoots.Count() <= 0)
                return list;

            EyouSoft.Model.HotelStructure.HotelInfo model = null;
            foreach (XElement tmpHotelRoot in HotelRoots)
            {
                model = new EyouSoft.Model.HotelStructure.HotelInfo();
                model.InterfaceResponseXML = InterfaceReturnXML;
                AnalysisXML(tmpHotelRoot, ref model);

                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 将接口返回的XML转换为平台酒店实体
        /// </summary>
        /// <param name="InterfaceReturnXML">接口返回的XML</param>
        /// <returns>平台酒店实体</returns>
        private EyouSoft.Model.HotelStructure.HotelInfo GetHotelModelByXML(string InterfaceReturnXML)
        {
            EyouSoft.Model.HotelStructure.HotelInfo model = null;
            if (string.IsNullOrEmpty(InterfaceReturnXML))
                return model;

            model = new EyouSoft.Model.HotelStructure.HotelInfo();
            model.InterfaceResponseXML = InterfaceReturnXML;
            XElement root = XElement.Parse(InterfaceReturnXML);
            XElement HotelAvailRS = HotelBI.Utils.GetXElement(root, "HotelAvailRS");
            XElement RoomStays = HotelBI.Utils.GetXElement(HotelAvailRS, "RoomStays");
            XElement HotelRoot = HotelBI.Utils.GetXElement(RoomStays, "RoomStay");
            AnalysisXML(HotelRoot, ref model);
            HotelRoot = null;
            root = null;

            return model;
        }

        #endregion

        #region 具体实现解析XML的函数

        /// <summary>
        /// 解析XML构造酒店实体
        /// </summary>
        /// <param name="HotelRoot">酒店XML</param>
        /// <param name="model">酒店实体</param>
        private void AnalysisXML(XElement HotelRoot, ref EyouSoft.Model.HotelStructure.HotelInfo model)
        {
            if (HotelRoot == null || model == null)
                return;

            XElement BasicProperty = HotelBI.Utils.GetXElement(HotelRoot, "BasicProperty");
            GetHotelInfoByXML(BasicProperty, ref model);
            GetHotelPositionInfoByXML(BasicProperty, ref model);
            GetHotelImagesByXML(BasicProperty, ref model);
            GetRoomTypeByXML(HotelRoot, ref model);
        }

        #region 解析XML获取多酒店查询返回的分页信息

        /// <summary>
        /// 解析XML获取多酒店查询返回的分页信息
        /// </summary>
        /// <param name="HotelAvailRS">酒店查询返回节点</param>
        /// <param name="RespPageInfo">多酒店查询返回的分页信息实体</param>
        private void GetRespPageInfo(XElement HotelAvailRS, ref EyouSoft.Model.HotelStructure.RespPageInfo RespPageInfo)
        {
            if (RespPageInfo == null)
                RespPageInfo = new EyouSoft.Model.HotelStructure.RespPageInfo();

            if (HotelAvailRS == null)
                return;

            XElement RespPage = HotelBI.Utils.GetXElement(HotelAvailRS, "RespPageInfo");
            RespPageInfo.CurrentPage = EyouSoft.Common.Utility.GetInt(HotelBI.Utils.GetXElement(RespPage, "CurrentPage").Value.Trim());
            RespPageInfo.TotalNum = EyouSoft.Common.Utility.GetInt(HotelBI.Utils.GetXElement(RespPage, "TotalNum").Value.Trim());
            RespPageInfo.TotalPageNum = EyouSoft.Common.Utility.GetInt(HotelBI.Utils.GetXElement(RespPage, "TotalPageNum").Value.Trim());
        }

        #endregion

        #region 解析XML获取酒店基本信息

        /// <summary>
        /// 解析XML获取酒店基本信息
        /// </summary>
        /// <param name="BasicProperty">酒店基本信息XML节点</param>
        /// <param name="model">酒店信息实体</param>
        private void GetHotelInfoByXML(XElement BasicProperty, ref EyouSoft.Model.HotelStructure.HotelInfo model)
        {
            if (BasicProperty == null || model == null)
                return;

            model.HotelName = HotelBI.Utils.GetXAttributeValue(BasicProperty, "HotelName").Trim();
            model.HotelCode = HotelBI.Utils.GetXAttributeValue(BasicProperty, "HotelCode").Trim();
            model.District = HotelBI.Utils.GetXElement(BasicProperty, "District").Value.Trim();
            string rank = HotelBI.Utils.GetXElement(BasicProperty, "Rank").Value.Trim();
            if (!string.IsNullOrEmpty(rank))
                model.Rank = (HotelBI.HotelRankEnum)Enum.Parse(typeof(HotelBI.HotelRankEnum), "_" + rank);
            model.Fitment = HotelBI.Utils.GetXElement(BasicProperty, "Fitment").Value.Trim();
            model.Tel = HotelBI.Utils.GetXElement(BasicProperty, "Tel").Value.Trim();
            model.Floor = EyouSoft.Common.Utility.GetInt(HotelBI.Utils.GetXElement(BasicProperty, "Floor").Value.Trim());
            model.Opendate = HotelBI.Utils.GetXElement(BasicProperty, "Opendate").Value.Trim();
            model.RoomQuantity = EyouSoft.Common.Utility.GetInt(HotelBI.Utils.GetXElement(BasicProperty, "RoomQuantity").Value.Trim());
            model.LongDesc = HotelBI.Utils.GetXElement(BasicProperty, "LongDesc").Value.Trim();
            model.ShortDesc = HotelBI.Utils.GetXElement(BasicProperty, "ShortDesc").Value.Trim();
            model.MinRate = EyouSoft.Common.Utility.GetDecimal(HotelBI.Utils.GetXElement(BasicProperty, "MinRate").Value.Trim());
            model.CityCode = HotelBI.Utils.GetXAttributeValue(BasicProperty, "CityCode").Trim();
            model.CountryCode = HotelBI.Utils.GetXAttributeValue(BasicProperty, "CountryCode").Trim();
        }

        #endregion

        #region 解析XML获取酒店地理位置信息

        /// <summary>
        /// 解析XML获取酒店地理位置信息
        /// </summary>
        /// <param name="BasicProperty">酒店基本信息XML节点</param>
        /// <param name="model">酒店信息实体</param>
        private void GetHotelPositionInfoByXML(XElement BasicProperty, ref EyouSoft.Model.HotelStructure.HotelInfo model)
        {
            if (BasicProperty == null || model == null)
                return;

            if (model.HotelPosition == null)
                model.HotelPosition = new EyouSoft.Model.HotelStructure.HotelPositionInfo();

            model.HotelPosition.Address = HotelBI.Utils.GetXElement(BasicProperty, "Address").Value.Trim();
            model.HotelPosition.Longitude = EyouSoft.Common.Utility.GetDouble(HotelBI.Utils.GetXAttributeValue(HotelBI.Utils.GetXElement(BasicProperty, "Position"), "Longitude"));
            model.HotelPosition.Latitude = EyouSoft.Common.Utility.GetDouble(HotelBI.Utils.GetXAttributeValue(HotelBI.Utils.GetXElement(BasicProperty, "Position"), "Latitude"));
            model.HotelPosition.POR = HotelBI.Utils.GetXElement(BasicProperty, "POR").Value.Trim();
        }

        #endregion

        #region 解析XML获取酒店图片信息

        /// <summary>
        /// 解析XML获取酒店图片信息
        /// </summary>
        /// <param name="BasicProperty">酒店基本信息XML节点</param>
        /// <param name="model">酒店信息实体</param>
        private void GetHotelImagesByXML(XElement BasicProperty, ref EyouSoft.Model.HotelStructure.HotelInfo model)
        {
            if (BasicProperty == null || model == null)
                return;

            if (model.HotelImages == null)
                model.HotelImages = new List<EyouSoft.Model.HotelStructure.HotelImagesInfo>();

            IEnumerable<XElement> Images = HotelBI.Utils.GetXElements(HotelBI.Utils.GetXElement(BasicProperty, "Images"), "Image");
            if (Images.Count() > 0)
            {
                EyouSoft.Model.HotelStructure.HotelImagesInfo imageModel = null;

                foreach (XElement eTMP in Images)
                {
                    if (eTMP == null)
                        continue;

                    imageModel = new EyouSoft.Model.HotelStructure.HotelImagesInfo();
                    imageModel.Category = HotelBI.Utils.GetXAttributeValue(eTMP, "Category");
                    imageModel.ImageURL = HotelBI.Utils.GetXElement(eTMP, "URL").Value.Trim();

                    model.HotelImages.Add(imageModel);
                }

                imageModel = null;
            }
            Images = null;
        }

        #endregion

        #region 解析XML获取酒店房型价格信息

        private void GetRoomTypeByXML(XElement HotelRoot, ref EyouSoft.Model.HotelStructure.HotelInfo model)
        {
            if (HotelRoot == null || model == null)
                return;

            model.RoomTypeList = new List<EyouSoft.Model.HotelStructure.RoomTypeInfo>();
            //价格计划
            IEnumerable<XElement> RatePlans = HotelBI.Utils.GetXElements(HotelBI.Utils.GetXElement(HotelRoot, "RatePlans"), "RatePlan");
            //房型
            IEnumerable<XElement> RoomTypes = HotelBI.Utils.GetXElements(HotelBI.Utils.GetXElement(HotelRoot, "RoomTypes"), "RoomType");
            //房型价格
            IEnumerable<XElement> RoomRates = HotelBI.Utils.GetXElements(HotelBI.Utils.GetXElement(HotelRoot, "RoomRates"), "RoomRate");

            if (RatePlans.Count() > 0 && RoomTypes.Count() > 0 && RoomRates.Count() > 0)
            {
                GetRoomTypeByForXML(RatePlans, RoomTypes, RoomRates, ref model);
            }
        }

        #endregion

        #region 循环XML节点，取得房型信息

        /// <summary>
        /// 循环XML节点，取得房型信息
        /// </summary>
        /// <param name="RatePlans">价格计划节点集合</param>
        /// <param name="RoomTypes">房型信息节点集合</param>
        /// <param name="RoomRates">房型价格信息节点集合</param>
        /// <param name="model">酒店实体</param>
        private void GetRoomTypeByForXML(IEnumerable<XElement> RatePlans, IEnumerable<XElement> RoomTypes, IEnumerable<XElement> RoomRates
            , ref EyouSoft.Model.HotelStructure.HotelInfo model)
        {
            if (RatePlans == null || RatePlans.Count() <= 0 || RoomTypes == null || RoomTypes.Count() <= 0 || RoomRates == null || RoomRates.Count() <= 0 || model == null)
                return;

            model.RoomTypeList = new List<EyouSoft.Model.HotelStructure.RoomTypeInfo>();
            string strTmpRatePlan = string.Empty;//价格计划代码
            string strTmpRoomType = string.Empty;//房型代码
            string strTmpVendorCode = string.Empty;//供应商代码
            EyouSoft.Model.HotelStructure.RoomTypeInfo tmpRoomTypeInfo = null;
            foreach (XElement tmpRatePlan in RatePlans)
            {
                foreach (XElement tmpRoomType in RoomTypes)
                {
                    strTmpRatePlan = HotelBI.Utils.GetXAttributeValue(tmpRatePlan, "RatePlanCode").Trim();//价格计划代码
                    strTmpVendorCode = HotelBI.Utils.GetXAttributeValue(tmpRatePlan, "VendorCode").Trim();//供应商代码
                    strTmpRoomType = HotelBI.Utils.GetXAttributeValue(tmpRoomType, "RoomTypeCode").Trim();//房型代码

                    foreach (XElement tmpRoomRate in RoomRates)
                    {
                        //根据价格计划代码、房型代码、供应商代码确定价格
                        if (strTmpRatePlan == HotelBI.Utils.GetXAttributeValue(tmpRoomRate, "RatePlanCode").Trim()
                            && strTmpVendorCode == HotelBI.Utils.GetXAttributeValue(tmpRoomRate, "VendorCode").Trim()
                            && strTmpRoomType == HotelBI.Utils.GetXAttributeValue(tmpRoomRate, "RoomTypeCode").Trim()
                            )
                        {

                            tmpRoomTypeInfo = new EyouSoft.Model.HotelStructure.RoomTypeInfo();
                            GetGetRoomTypeInfoByXML(strTmpRatePlan, strTmpRoomType, strTmpVendorCode, tmpRoomType, tmpRoomRate, ref tmpRoomTypeInfo);

                            model.RoomTypeList.Add(tmpRoomTypeInfo);
                        }
                    }
                }
            }
        }

        #endregion

        #region 解析XML获取房型基本信息

        /// <summary>
        /// 解析XML获取房型基本信息
        /// </summary>
        /// <param name="strTmpRatePlan">当前价格计划代码</param>
        /// <param name="strTmpRoomType">当前房型代码</param>
        /// <param name="strTmpVendorCode">当前供应商代码</param>
        /// <param name="tmpRoomType">当前房型节点</param>
        /// <param name="tmpRoomRate">当前房型价格信息节点</param>
        /// <param name="tmpRoomTypeInfo">当前房型实体</param>
        private void GetGetRoomTypeInfoByXML(string strTmpRatePlan, string strTmpRoomType, string strTmpVendorCode, XElement tmpRoomType
            , XElement tmpRoomRate, ref EyouSoft.Model.HotelStructure.RoomTypeInfo tmpRoomTypeInfo)
        {
            tmpRoomTypeInfo = new EyouSoft.Model.HotelStructure.RoomTypeInfo();
            tmpRoomTypeInfo.RatePlanCode = strTmpRatePlan;
            tmpRoomTypeInfo.RoomTypeCode = strTmpRoomType;
            tmpRoomTypeInfo.RoomTypeName = HotelBI.Utils.GetXAttributeValue(tmpRoomType, "RoomTypeName").Trim();
            tmpRoomTypeInfo.Category = HotelBI.Utils.GetXAttributeValue(tmpRoomType, "Category").Trim();
            tmpRoomTypeInfo.BedType = HotelBI.Utils.GetXAttributeValue(tmpRoomType, "BedType").Trim();
            tmpRoomTypeInfo.NoSmoking = HotelBI.Utils.GetXAttributeValue(tmpRoomType, "NoSmoking").Trim().ToLower() == "y" ? true : false;
            tmpRoomTypeInfo.RoomDescription = HotelBI.Utils.GetXElement(tmpRoomType, "RoomDescription").Value.Trim();
            tmpRoomTypeInfo.VendorCode = strTmpVendorCode;
            tmpRoomTypeInfo.VendorName = HotelBI.Utils.GetXAttributeValue(tmpRoomRate, "VendorName").Trim();

            GetRoomRateInfoByXML(tmpRoomRate, ref tmpRoomTypeInfo);
            GetRoomRateListByXML(strTmpRatePlan, strTmpRoomType, strTmpVendorCode, tmpRoomRate, ref tmpRoomTypeInfo);
        }

        #endregion

        #region 解析XML获取房型价格基本信息

        /// <summary>
        /// 解析XML获取房型价格基本信息
        /// </summary>
        /// <param name="tmpRoomRate">当前房型价格信息节点</param>
        /// <param name="tmpRoomTypeInfo">当前房型实体</param>
        private void GetRoomRateInfoByXML(XElement tmpRoomRate, ref EyouSoft.Model.HotelStructure.RoomTypeInfo tmpRoomTypeInfo)
        {
            EyouSoft.Model.HotelStructure.RoomRateInfo tmpRoomRateInfo = new EyouSoft.Model.HotelStructure.RoomRateInfo();
            string strTmpStartDate = HotelBI.Utils.GetXAttributeValue(tmpRoomRate, "StartDate").Trim();
            if (!string.IsNullOrEmpty(strTmpStartDate))
                tmpRoomRateInfo.StartDate = DateTime.Parse(strTmpStartDate);
            string strTmpEndDate = HotelBI.Utils.GetXAttributeValue(tmpRoomRate, "EndDate").Trim();
            if (!string.IsNullOrEmpty(strTmpEndDate))
                tmpRoomRateInfo.EndDate = DateTime.Parse(strTmpEndDate);
            string strTmpPayment = HotelBI.Utils.GetXAttributeValue(tmpRoomRate, "Payment").Trim();
            if (!string.IsNullOrEmpty(strTmpPayment))
                tmpRoomRateInfo.Payment = (EyouSoft.HotelBI.HBEPaymentType)Enum.Parse(typeof(EyouSoft.HotelBI.HBEPaymentType), strTmpPayment);
            tmpRoomRateInfo.Internet = HotelBI.Utils.GetXElement(tmpRoomRate, "Internet").Value.Trim().ToLower() == "y" ? true : false;
            tmpRoomRateInfo.Quantity = EyouSoft.Common.Utility.GetInt(HotelBI.Utils.GetXElement(tmpRoomRate, "Quantity").Value.Trim());
            tmpRoomRateInfo.MaxGuestNum = EyouSoft.Common.Utility.GetInt(HotelBI.Utils.GetXElement(tmpRoomRate, "MaxGuestNum").Value.Trim());
            tmpRoomRateInfo.AmountPrice = EyouSoft.Common.Utility.GetDecimal(HotelBI.Utils.GetXAttributeValue(HotelBI.Utils.GetXElement(tmpRoomRate, "Total"), "AmountPrice").Trim());
            string strAvailabilityStatus = HotelBI.Utils.GetXAttributeValue(tmpRoomRate, "AvailabilityStatus").Trim();
            if (!string.IsNullOrEmpty(strAvailabilityStatus))
                tmpRoomRateInfo.AvailabilityStatus = (EyouSoft.Model.HotelStructure.RoomState)Enum.Parse(typeof(EyouSoft.Model.HotelStructure.RoomState), strAvailabilityStatus);

            GetBookPolicyByXML(tmpRoomRate, ref tmpRoomRateInfo);

            tmpRoomTypeInfo.RoomRate = tmpRoomRateInfo;
        }

        #endregion

        #region 解析XML获取房型价格详细信息集合

        /// <summary>
        /// 解析XML获取房型价格详细信息集合
        /// </summary>
        /// <param name="strTmpRatePlan">当前价格计划代码</param>
        /// <param name="strTmpRoomType">当前房型代码</param>
        /// <param name="strTmpVendorCode">当前供应商代码</param>
        /// <param name="tmpRoomRate">当前房型价格信息节点</param>
        /// <param name="tmpRoomTypeInfo">当前房型实体</param>
        private void GetRoomRateListByXML(string strTmpRatePlan, string strTmpRoomType, string strTmpVendorCode,
            XElement tmpRoomRate, ref EyouSoft.Model.HotelStructure.RoomTypeInfo tmpRoomTypeInfo)
        {
            tmpRoomTypeInfo.RoomRate.RateInfos = new List<EyouSoft.Model.HotelStructure.RateInfo>();
            EyouSoft.Model.HotelStructure.RateInfo tmpRateInfo = null;
            //价格详细信息集合
            IEnumerable<XElement> Rates = HotelBI.Utils.GetXElements(HotelBI.Utils.GetXElement(tmpRoomRate, "Rates"), "Rate");
            foreach (XElement tmpRate in Rates)
            {
                string strTmpRateStartDate = HotelBI.Utils.GetXAttributeValue(tmpRate, "StartDate").Trim();
                string strTmpRateEndDate = HotelBI.Utils.GetXAttributeValue(tmpRate, "EndDate").Trim();
                if (string.IsNullOrEmpty(strTmpRateStartDate) || string.IsNullOrEmpty(strTmpRateEndDate))
                    continue;

                DateTime DateRateS = DateTime.Parse(strTmpRateStartDate);
                DateTime DateRateE = DateTime.Parse(strTmpRateEndDate);

                tmpRateInfo = new EyouSoft.Model.HotelStructure.RateInfo();
                tmpRateInfo.AmountPrice = EyouSoft.Common.Utility.GetDecimal(HotelBI.Utils.GetXAttributeValue(tmpRate, "AmountPrice").Trim());
                tmpRateInfo.DisplayPrice = EyouSoft.Common.Utility.GetDecimal(HotelBI.Utils.GetXAttributeValue(tmpRate, "DisplayPrice").Trim());
                tmpRateInfo.FeeFix = EyouSoft.Common.Utility.GetDecimal(HotelBI.Utils.GetXAttributeValue(tmpRate, "FeeFix").Trim());
                tmpRateInfo.FeePercent = EyouSoft.Common.Utility.GetDecimal(HotelBI.Utils.GetXAttributeValue(tmpRate, "FeePercent").Trim());
                tmpRateInfo.Fee = EyouSoft.Common.Utility.GetDecimal(HotelBI.Utils.GetXAttributeValue(tmpRate, "Fee").Trim());
                tmpRateInfo.FreeMeal = EyouSoft.Common.Utility.GetInt(HotelBI.Utils.GetXElement(tmpRate, "FreeMeal").Value.Trim());

                GetExcessiveFeeByXML(strTmpRatePlan, strTmpRoomType, strTmpVendorCode, tmpRoomRate, DateRateS, DateRateE, ref tmpRateInfo);
                GetCommissionByXML(strTmpRatePlan, strTmpRoomType, strTmpVendorCode, tmpRoomRate, DateRateS, DateRateE, ref tmpRateInfo);

                EyouSoft.Model.HotelStructure.RateInfo tmpRateInfo1 = null;
                TimeSpan TSSE = DateRateE - DateRateS;
                for (int i = 0; i <= TSSE.Days; i++)
                {
                    tmpRateInfo1 = new EyouSoft.Model.HotelStructure.RateInfo();
                    tmpRateInfo1.AmountPrice = tmpRateInfo.AmountPrice;
                    tmpRateInfo1.DisplayPrice = tmpRateInfo.DisplayPrice;
                    tmpRateInfo1.FeeFix = tmpRateInfo.FeeFix;
                    tmpRateInfo1.FeePercent = tmpRateInfo.FeePercent;
                    tmpRateInfo1.Fee = tmpRateInfo.Fee;
                    tmpRateInfo1.FreeMeal = tmpRateInfo.FreeMeal;
                    tmpRateInfo1.CommissionType = tmpRateInfo.CommissionType;
                    tmpRateInfo1.Fix = tmpRateInfo.Fix;
                    tmpRateInfo1.Percent = tmpRateInfo.Percent;
                    tmpRateInfo1.Tax = tmpRateInfo.Tax;
                    tmpRateInfo1.CurrData = DateRateS.AddDays(i);
                    tmpRoomTypeInfo.RoomRate.RateInfos.Add(tmpRateInfo1);
                }
            }
        }

        #endregion

        #region 解析XML获取房型返佣信息

        /// <summary>
        /// 解析XML获取房型返佣信息
        /// </summary>
        /// <param name="strTmpRatePlan">当前价格计划代码</param>
        /// <param name="strTmpRoomType">当前房型代码</param>
        /// <param name="strTmpVendorCode">当前供应商代码</param>
        /// <param name="tmpRoomRate">当前房型价格信息节点</param>
        /// <param name="DateRateS">当前房型的价格详细开始日期</param>
        /// <param name="DateRateE">当前房型的价格详细结束日期</param>
        /// <param name="tmpRateInfo">房型价格详细信息实体</param>
        private void GetCommissionByXML(string strTmpRatePlan, string strTmpRoomType, string strTmpVendorCode,
            XElement tmpRoomRate, DateTime DateRateS, DateTime DateRateE, ref EyouSoft.Model.HotelStructure.RateInfo tmpRateInfo)
        {
            //返佣信息集合
            IEnumerable<XElement> Commissions = HotelBI.Utils.GetXElements(HotelBI.Utils.GetXElement(tmpRoomRate, "Commissions"), "Commission");
            foreach (XElement tmpCommission in Commissions)
            {
                string strTmpCommissionStartDate = HotelBI.Utils.GetXAttributeValue(tmpCommission, "StartDate").Trim();
                string strTmpCommissionEndDate = HotelBI.Utils.GetXAttributeValue(tmpCommission, "EndDate").Trim();
                if (string.IsNullOrEmpty(strTmpCommissionStartDate) || string.IsNullOrEmpty(strTmpCommissionEndDate))
                    continue;

                DateTime DateCommissionS = DateTime.Parse(strTmpCommissionStartDate);
                DateTime DateCommissionE = DateTime.Parse(strTmpCommissionEndDate);
                TimeSpan TSS = DateRateS - DateCommissionS;
                TimeSpan TSE = DateRateE - DateCommissionE;

                //价格计划代码、房型代码、供应商代码都匹配后计算日期
                //比开始时间大、比结束时间小
                if (strTmpRatePlan == HotelBI.Utils.GetXAttributeValue(tmpCommission, "RatePlanCode").Trim()
                    && strTmpVendorCode == HotelBI.Utils.GetXAttributeValue(tmpCommission, "VendorCode").Trim()
                    && strTmpRoomType == HotelBI.Utils.GetXAttributeValue(tmpCommission, "RoomTypeCode").Trim()
                    && TSS.Days >= 0 && TSE.Days <= 0
                    )
                {
                    string strCommissionType = HotelBI.Utils.GetXAttributeValue(tmpCommission, "CommissionType").Trim();
                    if (!string.IsNullOrEmpty(strCommissionType))
                        tmpRateInfo.CommissionType = (EyouSoft.HotelBI.HBECommissionType)Enum.Parse(typeof(EyouSoft.HotelBI.HBECommissionType), strCommissionType);
                    tmpRateInfo.Fix = EyouSoft.Common.Utility.GetDecimal(HotelBI.Utils.GetXElement(tmpCommission, "Fix").Value.Trim());
                    tmpRateInfo.Percent = EyouSoft.Common.Utility.GetDecimal(HotelBI.Utils.GetXElement(tmpCommission, "Percent").Value.Trim());
                    tmpRateInfo.Tax = EyouSoft.Common.Utility.GetDecimal(HotelBI.Utils.GetXElement(tmpCommission, "Tax").Value.Trim());
                }
            }
        }

        #endregion

        #region 解析XML获取额外收费项目信息

        /// <summary>
        /// 解析XML获取额外收费项目信息
        /// </summary>
        /// <param name="strTmpRatePlan">当前价格计划代码</param>
        /// <param name="strTmpRoomType">当前房型代码</param>
        /// <param name="strTmpVendorCode">当前供应商代码</param>
        /// <param name="tmpRoomRate">当前房型价格信息节点</param>
        /// <param name="DateRateS">当前房型的价格详细开始日期</param>
        /// <param name="DateRateE">当前房型的价格详细结束日期</param>
        /// <param name="tmpRateInfo">房型价格详细信息实体</param>
        public void GetExcessiveFeeByXML(string strTmpRatePlan, string strTmpRoomType, string strTmpVendorCode,
            XElement tmpRoomRate, DateTime DateRateS, DateTime DateRateE, ref EyouSoft.Model.HotelStructure.RateInfo tmpRateInfo)
        {
            //额外收费项目信息集合
            IEnumerable<XElement> Fees = HotelBI.Utils.GetXElements(HotelBI.Utils.GetXElement(tmpRoomRate, "Fees"), "Fee");
            tmpRateInfo.ExcessiveFees = new List<EyouSoft.Model.HotelStructure.ExcessiveFee>();
            EyouSoft.Model.HotelStructure.ExcessiveFee FeeModel = null;
            foreach (XElement tmpFee in Fees)
            {
                string strTmpFeeStartDate = HotelBI.Utils.GetXAttributeValue(tmpFee, "StartDate").Trim();
                string strTmpFeeEndDate = HotelBI.Utils.GetXAttributeValue(tmpFee, "EndDate").Trim();
                if (string.IsNullOrEmpty(strTmpFeeStartDate) || string.IsNullOrEmpty(strTmpFeeEndDate))
                    continue;

                DateTime DateFeeS = DateTime.Parse(strTmpFeeStartDate);
                DateTime DateFeeE = DateTime.Parse(strTmpFeeEndDate);
                TimeSpan TSS = DateRateS - DateFeeS;
                TimeSpan TSE = DateRateE - DateFeeE;

                //价格计划代码、房型代码、供应商代码都匹配后计算日期
                //比开始时间大、比结束时间小
                if (strTmpRatePlan == HotelBI.Utils.GetXAttributeValue(tmpFee, "RatePlanCode").Trim()
                    && strTmpVendorCode == HotelBI.Utils.GetXAttributeValue(tmpFee, "VendorCode").Trim()
                    && strTmpRoomType == HotelBI.Utils.GetXAttributeValue(tmpFee, "RoomTypeCode").Trim()
                    && TSS.Days >= 0 && TSE.Days <= 0
                    )
                {
                    FeeModel = new EyouSoft.Model.HotelStructure.ExcessiveFee();

                    FeeModel.FeeName = HotelBI.Utils.GetXAttributeValue(tmpFee, "FeeName").Trim();
                    FeeModel.FeeCode = HotelBI.Utils.GetXAttributeValue(tmpFee, "FeeCode").Trim();
                    FeeModel.ChargeFrequence = HotelBI.Utils.GetXElement(tmpFee, "ChargeFrequence").Value.Trim();
                    FeeModel.Amount = EyouSoft.Common.Utility.GetDecimal(HotelBI.Utils.GetXElement(tmpFee, "Amount").Value.Trim());
                    FeeModel.ChargeUnit = HotelBI.Utils.GetXElement(tmpFee, "ChargeUnit").Value.Trim();
                    FeeModel.Comments = HotelBI.Utils.GetXElement(tmpFee, "Comments").Value.Trim();
                    FeeModel.Count = EyouSoft.Common.Utility.GetInt(HotelBI.Utils.GetXElement(tmpFee, "Count").Value.Trim());

                    tmpRateInfo.ExcessiveFees.Add(FeeModel);
                }
            }
        }

        #endregion

        #region 解析XML获取预定规则和要求信息

        /// <summary>
        /// 解析XML获取预定规则和要求信息
        /// </summary>
        /// <param name="tmpRoomRate">房型价格信息节点</param>
        /// <param name="tmpRoomRateInfo">房型价格信息实体</param>
        public void GetBookPolicyByXML(XElement tmpRoomRate, ref EyouSoft.Model.HotelStructure.RoomRateInfo tmpRoomRateInfo) 
        {
            if (tmpRoomRate == null || tmpRoomRateInfo == null)
                return;

            /*
             * --由于接口数据不明确，所以预定规则和要求信息现在只取第一条数据，以后要做调整
             */
            IEnumerable<XElement> BookPolicys = HotelBI.Utils.GetXElements(HotelBI.Utils.GetXElement(tmpRoomRate, "BookingPolicies"), "BookPolicy");
            if (BookPolicys != null && BookPolicys.Count() > 0)
            {
                tmpRoomRateInfo.BookPolicy = new EyouSoft.Model.HotelStructure.BookPolicy();
                XElement BookPolicy = HotelBI.Utils.GetXElement(BookPolicys.First(Item => (true)), "BookPolicy");
                tmpRoomRateInfo.BookPolicy.PolicyCode = HotelBI.Utils.GetXAttributeValue(BookPolicy, "PolicyCode").Trim();
                tmpRoomRateInfo.BookPolicy.PolicyType = HotelBI.Utils.GetXAttributeValue(BookPolicy, "PolicyType").Trim();
                tmpRoomRateInfo.BookPolicy.Status = HotelBI.Utils.GetXElement(BookPolicy, "Status").Value.Trim().ToLower() == "y" ? true : false;
                tmpRoomRateInfo.BookPolicy.ShortDesc = HotelBI.Utils.GetXElement(BookPolicy, "ShortDesc").Value.Trim();
                tmpRoomRateInfo.BookPolicy.LongDesc = HotelBI.Utils.GetXElement(BookPolicy, "LongDesc").Value.Trim();
            }
        }

        #endregion

        #endregion

        #endregion

        #region private members local cache hotels
        /// <summary>
        /// 获取本地缓存酒店信息集合
        /// </summary>
        /// <param name="searchInfo">查询信息</param>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="errInfo">错误信息</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HotelStructure.HotelInfo> GetLocalCacheHotels(HotelBI.MultipleSeach searchInfo, EyouSoft.Model.HotelStructure.RespPageInfo pageInfo, out HotelBI.ErrorInfo errInfo)
        {
            errInfo = new EyouSoft.HotelBI.ErrorInfo();
            IList<EyouSoft.Model.HotelStructure.HotelInfo> items = null;
            EyouSoft.Model.HotelStructure.MLocalHotelSearchInfo localSearchInfo = new EyouSoft.Model.HotelStructure.MLocalHotelSearchInfo();
            localSearchInfo.BedType = searchInfo.BedType;
            localSearchInfo.CheckInDate = EyouSoft.Common.Utility.GetDateTime(searchInfo.CheckInDate);
            localSearchInfo.CheckOutDate = EyouSoft.Common.Utility.GetDateTime(searchInfo.CheckOutDate);
            localSearchInfo.CityCode = searchInfo.CityCode;
            localSearchInfo.Fitment = searchInfo.Fitment;
            localSearchInfo.HotelName = searchInfo.HotelChineseName;
            localSearchInfo.HotelNameEn = searchInfo.HotelEnglishName;           
            //localSearchInfo.SpecialRoomName = searchInfo.RoomView;
            localSearchInfo.SpecialRoomName = searchInfo.RoomName;
            localSearchInfo.LandMarkName = searchInfo.LandMark;
            localSearchInfo.District = searchInfo.District;
            localSearchInfo.OrderBy = searchInfo.OrderBy;
            localSearchInfo.PriceMaxRate = searchInfo.PriceMaxRate;
            localSearchInfo.PriceMinRate = searchInfo.PriceMinRate;
            if (searchInfo.HotelRank != HotelRankEnum._00)
            {
                localSearchInfo.Rank = searchInfo.HotelRank.ToString().Replace("_", "");
            }
            if (searchInfo.Internet != BoolEnum.none)
            {
                localSearchInfo.IsInternet = searchInfo.Internet == BoolEnum.Y ? true : false;
            }

            int pageSize=pageInfo.PageSize;
            int pageIndex=pageInfo.CurrentPage;
            int recordCount=pageInfo.TotalNum;

            items = dal.GetHotels(pageSize, pageIndex, ref recordCount, localSearchInfo);

            pageInfo.TotalNum = recordCount;

            return items;
        }
        #endregion
    }
}
