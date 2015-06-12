using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.TicketStructure
{
    /// <summary>
    /// 运价信息数据层
    /// </summary>
    /// Authhor:罗丽娥   2010-11-01
    public class TicketFreightInfo : EyouSoft.IBLL.TicketStructure.ITicketFreightInfo
    {
        private readonly EyouSoft.IDAL.TicketStructure.ITicketFreightInfo idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TicketStructure.ITicketFreightInfo>();
        private readonly EyouSoft.IDAL.TicketStructure.IFreightBuyLog iBuyDal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TicketStructure.IFreightBuyLog>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.TicketStructure.ITicketFreightInfo CreateInstance()
        {
            EyouSoft.IBLL.TicketStructure.ITicketFreightInfo op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.TicketStructure.ITicketFreightInfo>();
            }
            return op;
        }

        /// <summary>
        /// 添加运价信息
        /// </summary>
        /// <param name="model">运价信息实体</param>
        /// <returns>返回1：添加成功</returns>
        public bool Add(Model.TicketStructure.TicketFreightInfo model)
        {
            bool IsResult = false;

            if (model.IsEnabled)
            {
                IList<EyouSoft.Model.TicketStructure.AvailablePackInfo> list = iBuyDal.GetAvailableInfo(DateTime.Now, model.Company.ID, model.RateType, model.NoGadHomeCityId);
                if (list != null && list.Count > 0)
                {
                    EyouSoft.Model.TicketStructure.AvailablePackInfo AvailModel = (EyouSoft.Model.TicketStructure.AvailablePackInfo)list[0];
                    model.FreightBuyId = AvailModel.BuyId;

                    IList<EyouSoft.Model.TicketStructure.AvailablePackInfo> list1 = new List<EyouSoft.Model.TicketStructure.AvailablePackInfo>();
                    EyouSoft.Model.TicketStructure.AvailablePackInfo AvaiModel1 = new EyouSoft.Model.TicketStructure.AvailablePackInfo();
                    AvaiModel1.BuyId = AvailModel.BuyId;
                    AvaiModel1.PackType = AvailModel.PackType;
                    AvaiModel1.AvailableNum = 1;
                    list1.Add(AvaiModel1);
                    AvaiModel1 = null;
                    AvailModel = null;

                    IsResult = idal.AddFreightInfo(model) > 0 ? true : false;
                    if (IsResult)
                    {
                        IsResult = iBuyDal.SetAvailableNum(list1);
                    }
                    list1 = null;
                }
                list = null;
            }
            else {
                model.FreightBuyId = string.Empty;
                IsResult = idal.AddFreightInfo(model) > 0 ? true : false;
            }
            return IsResult;
        }

        /// <summary>
        /// 批量添加运价信息
        /// </summary>
        /// <param name="FreightInfoList">运价信息实体集合</param>
        /// <returns>返回1：添加成功</returns>
        public bool Add(IList<Model.TicketStructure.TicketFreightInfo> FreightInfoList)
        {
            bool IsResult = false;
            if (FreightInfoList != null && FreightInfoList.Count > 0)
            {
                EyouSoft.Model.TicketStructure.TicketFreightInfo tmpModel = (Model.TicketStructure.TicketFreightInfo)FreightInfoList[0];

                IList<EyouSoft.Model.TicketStructure.AvailablePackInfo> list = iBuyDal.GetAvailableInfo(DateTime.Now, tmpModel.Company.ID, tmpModel.RateType, tmpModel.NoGadHomeCityId).Where(item=>(item.PackType==EyouSoft.Model.TicketStructure.PackageTypes.常规)).ToList();
                tmpModel = null;

                if (list != null && list.Count > 0)
                {
                    IList<EyouSoft.Model.TicketStructure.AvailablePackInfo> list1 = new List<EyouSoft.Model.TicketStructure.AvailablePackInfo>();

                    int count = 0;
                    foreach (EyouSoft.Model.TicketStructure.AvailablePackInfo AvailModel in list)
                    {
                        int AvailableNum = 0;
                        for (int i = count; i < FreightInfoList.Count; i++)
                        {
                            if (AvailableNum < AvailModel.AvailableNum)
                            {
                                EyouSoft.Model.TicketStructure.TicketFreightInfo FreightModel = FreightInfoList[i];
                                FreightModel.FreightBuyId = FreightModel.IsEnabled ? AvailModel.BuyId : string.Empty;
                                if (FreightModel.IsEnabled)
                                {
                                    AvailableNum++;
                                }
                                count++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        EyouSoft.Model.TicketStructure.AvailablePackInfo AvailModel1 = new EyouSoft.Model.TicketStructure.AvailablePackInfo();
                        AvailModel1.BuyId = AvailModel.BuyId;
                        AvailModel1.AvailableNum = AvailableNum;
                        AvailModel1.PackType = AvailModel.PackType;
                        list1.Add(AvailModel1);
                        AvailModel1 = null;
                    }
                    IsResult = idal.AddFreightInfo(FreightInfoList) > 0 ? true : false;
                    if (IsResult)
                    {
                        IsResult = iBuyDal.SetAvailableNum(list1);
                    }
                    list1 = null;
                }
                list = null;
            }

            return IsResult;
        }

        /// <summary>
        /// 修改运价信息
        /// </summary>
        /// <param name="model">运价信息实体</param>
        /// <returns>返回1：添加成功</returns>
        public bool Update(Model.TicketStructure.TicketFreightInfo model)
        {
            #region 当前运价修改之前的实体
            Model.TicketStructure.TicketFreightInfo OldModel = GetModel(model.Id);
            if (OldModel == null)
                return false;
            #endregion

            #region 购买记录变量
            IList<EyouSoft.Model.TicketStructure.AvailablePackInfo> AvailableList = new List<EyouSoft.Model.TicketStructure.AvailablePackInfo>();//设置购买记录集合列表
            EyouSoft.Model.TicketStructure.AvailablePackInfo AvaiModel1 = null;
            #endregion

            bool IsResult = false;
            //[新增时启用状态为关闭]
            if (model.IsEnabled && String.IsNullOrEmpty(model.FreightBuyId.Trim()))
            {
                #region 可用的套餐购买集合
                IList<EyouSoft.Model.TicketStructure.AvailablePackInfo> list = iBuyDal.GetAvailableInfo(DateTime.Now, model.Company.ID, model.RateType, model.NoGadHomeCityId);
                #endregion
                if (list != null && list.Count > 0)
                {
                    //常规的运价购买记录
                    IList<EyouSoft.Model.TicketStructure.AvailablePackInfo> CGeAvailabList = list.Where(Item => (Item.PackType ==EyouSoft.Model.TicketStructure.PackageTypes.常规)).ToList();
                    //当前运价信息所属的类型的购买记录
                    IList<EyouSoft.Model.TicketStructure.AvailablePackInfo> CurrTypeAvailabList = list.Where(Item => (Item.PackType == model.BuyType)).ToList();
                    if ((CurrTypeAvailabList == null || CurrTypeAvailabList.Count == 0)&&
                        (CGeAvailabList==null || CGeAvailabList.Count==0))
                        return false; //
                    EyouSoft.Model.TicketStructure.AvailablePackInfo AvailModel = null;
                    if (CurrTypeAvailabList != null && CurrTypeAvailabList.Count > 0) //当前的购买类型的优先级高
                        AvailModel = CurrTypeAvailabList[0];
                    else
                        AvailModel = CGeAvailabList[0];
                    CurrTypeAvailabList = null;
                    CGeAvailabList = null;
                    model.FreightBuyId = AvailModel.BuyId;
                    AvaiModel1 = new EyouSoft.Model.TicketStructure.AvailablePackInfo();
                    AvaiModel1.BuyId = AvailModel.BuyId;
                    AvaiModel1.PackType = AvailModel.PackType;
                    AvaiModel1.AvailableNum = 1;//注:[此处设置为1在数据层处理为-1]
                    AvailableList.Add(AvaiModel1);
                    AvaiModel1 = null;
                    AvailModel = null;
                    list = null;
                }
                else
                    return false; //当前没有可用的运价条数
            }
            else if (model.IsEnabled != OldModel.IsEnabled && model.FreightBuyId.Trim() != "")
            {
                AvaiModel1 = new EyouSoft.Model.TicketStructure.AvailablePackInfo();
                AvaiModel1.BuyId = model.FreightBuyId;
                AvaiModel1.PackType = model.BuyType;
                if(model.IsEnabled)
                    AvaiModel1.AvailableNum = 1;//注:[此处设置为-1在数据层处理为+1]
                else
                    AvaiModel1.AvailableNum = -1;
                AvailableList.Add(AvaiModel1);
                AvaiModel1 = null;
            }
            OldModel = null;
            IsResult = idal.UpdateFreightInfo(model) > 0 ? true : false;
            if (IsResult && AvailableList.Count>0)
            {
                IsResult = iBuyDal.SetAvailableNum(AvailableList);
            }
            AvailableList = null;
            return IsResult ;
        }

        /// <summary>
        /// 复制运价信息
        /// </summary>
        /// <param name="FreightInfoId">运价ID</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Copy(string FreightInfoId)
        {
            EyouSoft.Model.TicketStructure.TicketFreightInfo model = GetModel(FreightInfoId);
            return Add(model);
        }

        /// <summary>
        /// 获取运价信息
        /// </summary>
        /// <param name="FreightInfoId">运价信息Id</param>
        /// <returns>返回运价信息实体</returns>
        public Model.TicketStructure.TicketFreightInfo GetModel(string FreightInfoId)
        {
            return idal.GetModel(FreightInfoId);
        }

        /// <summary>
        /// 运价维护列表
        /// </summary>
        /// <param name="pageSize">当前页数</param>
        /// <param name="pageIndex">每页条数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="orderIndex">排序索引：0/1运价添加时间升/降序；2/3最后修改时间升/降序；4/5套餐类型升/降序；6/7启用状态升/降序；8/9航空公司编号升/降序；</param>
        /// <param name="queryTicketFreightInfo">查询条件实体</param>
        /// <returns>返回运价信息实体集合</returns>
        public IList<Model.TicketStructure.TicketFreightInfo> GetList(int pageSize, int pageIndex, ref int recordCount, int orderIndex, Model.TicketStructure.QueryTicketFreightInfo queryTicketFreightInfo)
        {
            return idal.GetList(pageSize, pageIndex, ref recordCount, orderIndex, queryTicketFreightInfo);
        }

        /// <summary>
        /// 设置运价启用状态
        /// </summary>
        /// <param name="FreightInfoId">运价ID</param>
        /// <param name="IsEnabled">是否启用</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetIsEnabled(string FreightInfoId, bool IsEnabled)
        {
            #region 注释掉的代码块
            //if (string.IsNullOrEmpty(FreightInfoId))
            //    return false;
            //#region 购买记录变量
            //IList<EyouSoft.Model.TicketStructure.AvailablePackInfo> AvailableList = new List<EyouSoft.Model.TicketStructure.AvailablePackInfo>();//设置购买记录集合列表
            //EyouSoft.Model.TicketStructure.AvailablePackInfo AvaiModel1 = null;
            //#endregion
            //bool IsResult = false;
            //EyouSoft.Model.TicketStructure.TicketFreightInfo model = GetModel(FreightInfoId);
            //if (String.IsNullOrEmpty(model.FreightBuyId.Trim()) && IsEnabled)
            //{
            //    #region 可用的套餐购买集合
            //    IList<EyouSoft.Model.TicketStructure.AvailablePackInfo> list = iBuyDal.GetAvailableInfo(DateTime.Now, model.Company.ID, model.RateType, model.NoGadHomeCityId);
            //    #endregion
            //    if (AvailableList != null && AvailableList.Count > 0)
            //    {
            //        AvailModel = (EyouSoft.Model.TicketStructure.AvailablePackInfo)list[0];
            //        model.FreightBuyId = AvailModel.BuyId;

            //        AvailableList = new List<EyouSoft.Model.TicketStructure.AvailablePackInfo>();
            //        EyouSoft.Model.TicketStructure.AvailablePackInfo AvaiModel1 = new EyouSoft.Model.TicketStructure.AvailablePackInfo();
            //        AvaiModel1.BuyId = AvailModel.BuyId;
            //        AvaiModel1.PackType = AvailModel.PackType;
            //        AvaiModel1.AvailableNum = 1;
            //        AvailableList.Add(AvaiModel1);
            //        AvaiModel1 = null;
            //        AvailModel = null;
            //        model.IsEnabled = IsEnabled;
            //        list = null;
            //    }
            //    else
            //        return false;    //当前没有可用套餐
            //}
            //else if (!IsEnabled && model.FreightBuyId.Trim().Length>0)
            //{
            //    AvaiModel1 = new EyouSoft.Model.TicketStructure.AvailablePackInfo();
            //    AvaiModel1.BuyId = model.FreightBuyId;
            //    AvaiModel1.PackType = model.BuyType;
            //    AvaiModel1.AvailableNum = -1;//注:[此处设置为-1在数据层处理为+1]
            //    AvailableList.Add(AvaiModel1);
            //    AvaiModel1 = null;
            //    model.FreightBuyId = string.Empty;
            //}
            //IsResult = idal.SetIsEnabled(FreightInfoId, IsEnabled);
            //return IsResult;
            #endregion

            if (string.IsNullOrEmpty(FreightInfoId))
                return false;
            EyouSoft.Model.TicketStructure.TicketFreightInfo model = GetModel(FreightInfoId);
            if (model == null)
                return false;
            model.IsEnabled = IsEnabled;
            return Update(model);
        }

        /// <summary>
        /// 获取采购商运价信息集合
        /// </summary>
        /// <param name="queryTicketFreightInfo">查询条件实体</param>
        /// <returns>返回运价信息实体集合</returns>
        public IList<Model.TicketStructure.TicketFreightInfoList> GetFreightInfoList(Model.TicketStructure.QueryTicketFreightInfo queryTicketFreightInfo)
        {
            return idal.GetFreightInfoList(queryTicketFreightInfo);
        }
    }
}
