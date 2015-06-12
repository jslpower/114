using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Cache.Facade;
using EyouSoft.Component.Factory;
using EyouSoft.IBLL.CommunityStructure;

namespace EyouSoft.BLL.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-16
    /// 描述：供求信息业务层
    /// </summary>
    public class ExchangeList : IBLL.CommunityStructure.IExchangeList
    {
        private readonly IDAL.CommunityStructure.IExchangeList dal = ComponentFactory.CreateDAL<IDAL.CommunityStructure.IExchangeList>();

        #region CreateInstance
        /// <summary>
        /// 创建IBLL实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CommunityStructure.IExchangeList CreateInstance()
        {
            EyouSoft.IBLL.CommunityStructure.IExchangeList op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.CommunityStructure.IExchangeList>();
            }
            return op1;
        }
        #endregion


        #region IExchangeList接口
        /// <summary>
        /// 添加供求信息
        /// </summary>
        /// <param name="model">供求信息实体</param>
        /// <param name="ProvinceIds">发布到的省份数组</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Add(EyouSoft.Model.CommunityStructure.ExchangeList model, int[] ProvinceIds)
        {
            if (model == null)
                return false;

            model.CityContactList = new List<EyouSoft.Model.CommunityStructure.ExchangeCityContact>();
            model.CityContactList.Clear();
            IList<Model.SystemStructure.SysCity> listCity = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetProvinceCityList(ProvinceIds);
            if (listCity != null)
            {
                foreach (Model.SystemStructure.SysCity CityModel in listCity)
                {
                    if (CityModel.CityId != model.CityId)
                        model.CityContactList.Add(new EyouSoft.Model.CommunityStructure.ExchangeCityContact(model.ID, CityModel.ProvinceId, CityModel.CityId));
                }
            }
            model.CityContactList.Add(new EyouSoft.Model.CommunityStructure.ExchangeCityContact(model.ID, model.ProvinceId, model.CityId));
            return dal.Add(model);
        }
        /// <summary>
        /// 修改供求信息
        /// </summary>
        /// <param name="model">供求信息实体</param>
        /// <param name="ProvinceIds">发布到的省份数组</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Update(EyouSoft.Model.CommunityStructure.ExchangeList model, int[] ProvinceIds)
        {
            if (model == null)
                return false;

            model.CityContactList = new List<EyouSoft.Model.CommunityStructure.ExchangeCityContact>();
            model.CityContactList.Clear();
            IList<Model.SystemStructure.SysCity> listCity = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetProvinceCityList(ProvinceIds);
            if (listCity != null)
            {
                foreach (Model.SystemStructure.SysCity CityModel in listCity)
                {
                    if (CityModel.CityId != model.CityId)
                        model.CityContactList.Add(new EyouSoft.Model.CommunityStructure.ExchangeCityContact(model.ID, CityModel.ProvinceId, CityModel.CityId));
                }
            }
            model.CityContactList.Add(new EyouSoft.Model.CommunityStructure.ExchangeCityContact(model.ID, model.ProvinceId, model.CityId));
            return dal.Update(model);
        }
        /// <summary>
        /// 获取供求信息实体
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>供求信息实体</returns>
        public EyouSoft.Model.CommunityStructure.ExchangeList GetModel(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return null;
            return dal.GetModel(ID);
        }
        /// <summary>
        /// 前台用户删除供求信息【同时删除浏览过或者收藏过该信息的记录】
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Delete(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return false;
            return dal.Delete(ID);
        }
        /// <summary>
        /// 运营后台删除供求信息【同时删除浏览过或者收藏过该信息的记录】
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool ManageDel(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return false;
            return dal.Delete(ID);
        }
        /// <summary>
        /// 设置置顶状态
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <param name="IsTop">是否置顶</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetTop(string ID, bool IsTop)
        {
            if (string.IsNullOrEmpty(ID))
                return false;
            return dal.SetTop(ID, IsTop);
        }
        /// <summary>
        /// 设置审核状态
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <param name="IsCheck">是否审核</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetCheck(string ID, bool IsCheck)
        {
            if (string.IsNullOrEmpty(ID))
                return false;
            return dal.SetCheck(ID, IsCheck);
        }
        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="IDs">主键编号集合</param>
        /// <returns>true:成功 false:失败</returns>
        public bool BatchCheck(string[] IDs)
        {
            if (IDs == null || IDs.Length == 0)
                return false;
            return dal.BatchCheck(IDs);
        }
        /// <summary>
        /// 更新浏览次数
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetReadCount(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return false;
            return dal.SetReadCount(ID);
        }
        /// <summary>
        /// 更新回复次数
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetWriteBackCount(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return false;
            return dal.SetWriteBackCount(ID);
        }
        /// <summary>
        /// 删除供求信息图片
        /// </summary>
        /// <param name="ID">图片主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool DeletePhoto(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return false;
            return dal.DeletePhoto(ID);
        }
        /// <summary>
        /// 设置用户浏览记录
        /// </summary>
        /// <param name="model">供求浏览实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetVisited(EyouSoft.Model.CommunityStructure.ExchangeVisited model)
        {
            if (model == null)
                return false;
            return dal.SetVisited(model);
        }
        /// <summary>
        /// 获取指定条数的供求信息
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <param name="ExchangeType">供求信息类别 =null返回全部</param>
        /// <param name="CityId">城市编号 =0返回全部</param>
        /// <param name="IsTop">是否置顶 =null返回全部</param>
        /// <returns>供求信息列表集合</returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetTopList(int TopNum, EyouSoft.Model.CommunityStructure.ExchangeType? ExchangeType, int CityId, bool? IsTop)
        {
            return dal.GetTopNumList(TopNum, ExchangeType, null, IsTop, 0, CityId, null, string.Empty, string.Empty);
        }
        /// <summary>
        /// 分业获取指定类别指定城市的供求信息
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="ExchangeType">供求信息类别 =null返回全部</param>
        /// <param name="CityId">城市编号 =0返回全部</param>
        /// <returns>供求信息列表集合</returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CommunityStructure.ExchangeType? ExchangeType, int CityId)
        {
            return dal.GetList(pageSize, pageIndex, ref recordCount, ExchangeType, null, null, 0, CityId, null, string.Empty, string.Empty, null, null, null, null, null, true);
        }
        /// <summary>
        /// 根据查询条件分业获取供求信息
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="IsTop">是否置顶 =null返回全部</param>
        /// <returns>供求信息列表集合</returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetList(int pageSize, int pageIndex, ref int recordCount, bool? IsTop)
        {
            return dal.GetList(pageSize, pageIndex, ref recordCount, null, null, IsTop, 0, 0, null, string.Empty, string.Empty, null, null,
                null, null, null, true);
        }
        /// <summary>
        /// 根据查询条件分业获取供求信息
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="ExchangeType">供求类别 =null返回全部</param>
        /// <param name="Tag">供求标签 =null返回全部</param>
        /// <param name="IssueTime">发布时间 =null更早</param>
        /// <param name="ProvinceId">发布到省份编号 =0返回全部</param>
        /// <param name="CityId">发布到城市编号 =0返回全部</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <param name="StartDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns>供求信息列表集合</returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetSerachList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CommunityStructure.ExchangeType? ExchangeType,
            EyouSoft.Model.CommunityStructure.ExchangeTag? Tag, DateTime? IssueTime, int ProvinceId, int CityId, string KeyWord, DateTime? StartDate, DateTime? EndDate)
        {
            return dal.GetList(pageSize, pageIndex, ref recordCount, ExchangeType, Tag, null, ProvinceId, CityId, IssueTime, string.Empty, KeyWord, StartDate, EndDate, null, null, null, true);
        }
        /// <summary>
        /// 得到MQ内嵌页供求列表（共40条，前五条显示置顶的）
        /// </summary>
        /// <param name="ProvinceIds">某几个省份下的供求信息</param>
        /// <param name="Stime">时间（全部=0,今天=1,昨天=2,前天=3,更早=4</param>
        /// <param name="ExchangeTitle">要搜索的标题关键字</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetList(string ProvinceIds, int Stime, string ExchangeTitle)
        {
            return dal.GetList(ProvinceIds, Stime, ExchangeTitle);
        }
        /// <summary>
        /// 运营后台分页获取供求信息列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="ExchangeType">供求类别 =null返回全部</param>
        /// <param name="ProvinceId">发布到省份编号 =0返回全部</param>
        /// <param name="CityId">发布到城市编号 =0返回全部</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <param name="StartDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <param name="IsCheck">是否已审核 =null返回全部</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetSerachList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CommunityStructure.ExchangeType? ExchangeType,
             int ProvinceId, int CityId, string KeyWord, DateTime? StartDate, DateTime? EndDate, bool? IsCheck)
        {
            return dal.GetList(pageSize, pageIndex, ref recordCount, ExchangeType, null, null, ProvinceId, CityId, null, string.Empty,
                KeyWord, StartDate, EndDate, null, null, null, IsCheck);
        }
        /// <summary>
        /// 获取指定用户发布的供求信息列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="ExchangeType">供求类别 =null返回全部</param>
        /// <param name="Tag">供求标签 =null返回全部</param>
        /// <param name="ProvinceId">省份编号 =0返回全部</param>
        /// <param name="CityId">城市编号 =0返回全部</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <param name="OperatorId">发布人编号</param>
        /// <param name="StartDate">开始时间 =null返回全部</param>
        /// <param name="EndDate">结束时间 =null返回全部</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetListByOperator(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CommunityStructure.ExchangeType? ExchangeType,
            EyouSoft.Model.CommunityStructure.ExchangeTag? Tag, int ProvinceId, int CityId, string KeyWord, string OperatorId, DateTime? StartDate, DateTime? EndDate)
        {
            return dal.GetList(pageSize, pageIndex, ref recordCount, ExchangeType, Tag, null, ProvinceId, CityId, null, OperatorId,
                KeyWord, StartDate, EndDate, null, null, null, null);
        }
        /// <summary>
        /// 前台供求信息分页列表查询
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="ExchangeType">供求类别 =null返回全部</param>
        /// <param name="Tag">供求标签 =null返回全部</param>
        /// <param name="ProvinceId">发布到省份编号 =0返回全部</param>
        /// <param name="CityId">发布到城市编号 =0返回全部</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <param name="SearchDateType">发布时间（今天，近三天，本周，本月）</param>
        /// <returns>供求信息列表集合</returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetWebSerachList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CommunityStructure.ExchangeType? ExchangeType,
           EyouSoft.Model.CommunityStructure.ExchangeTag? Tag, int ProvinceId, int CityId, string KeyWord, EyouSoft.Model.CommunityStructure.SearchDateType SearchDateType)
        {
            IList<EyouSoft.Model.CommunityStructure.ExchangeList> list = null;
            switch (SearchDateType)
            {
                case EyouSoft.Model.CommunityStructure.SearchDateType.本月:
                    list = dal.GetList(pageSize, pageIndex, ref recordCount, ExchangeType, Tag, null, ProvinceId, CityId, null, string.Empty,
                        KeyWord, null, null, null, null, true, true);
                    break;
                case EyouSoft.Model.CommunityStructure.SearchDateType.本周:
                    list = dal.GetList(pageSize, pageIndex, ref recordCount, ExchangeType, Tag, null, ProvinceId, CityId, null, string.Empty,
                        KeyWord, null, null, null, true, null, true);
                    break;
                case EyouSoft.Model.CommunityStructure.SearchDateType.今天:
                    list = dal.GetList(pageSize, pageIndex, ref recordCount, ExchangeType, Tag, null, ProvinceId, CityId, null, string.Empty,
                       KeyWord, null, null, 0, null, null, true);
                    break;
                case EyouSoft.Model.CommunityStructure.SearchDateType.近三天:
                    list = dal.GetList(pageSize, pageIndex, ref recordCount, ExchangeType, Tag, null, ProvinceId, CityId, null, string.Empty,
                       KeyWord, null, null, 3, null, null, true);
                    break;
                default:
                    list = dal.GetList(pageSize, pageIndex, ref recordCount, ExchangeType, Tag, null, ProvinceId, CityId, null, string.Empty,
                       KeyWord, null, null, null, null, null, true);
                    break;
            }
            return list;
        }
        /// <summary>
        /// 获取供求信息标签列表
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.CommunityStructure.Tag> GetExchangeTags()
        {
            IList<EyouSoft.Model.CommunityStructure.Tag> list = new List<EyouSoft.Model.CommunityStructure.Tag>();
            foreach (string tag in Enum.GetNames(typeof(EyouSoft.Model.CommunityStructure.ExchangeTag)))
            {
                int tagValue = (int)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeTag), tag);
                list.Add(new EyouSoft.Model.CommunityStructure.Tag(tagValue));
            }
            return list;
        }
        /// <summary>
        /// 分页获取供求信息浏览记录
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="OperatorId">添加人编号 =""返回全部</param>
        /// <returns>供求信息浏览记录列表</returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeListBase> GetVisitedList(int pageSize, int pageIndex, ref int recordCount, string OperatorId)
        {
            return dal.GetVisitedList(pageSize, pageIndex, ref recordCount, OperatorId);
        }
        /// <summary>
        /// 获取指定条数的供求信息浏览记录
        /// </summary>
        /// <param name="topNumber">需要返回的记录数</param>
        /// <param name="OperatorId">添加人编号 =""返回全部</param>
        /// <returns>供求信息浏览记录列表</returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeListBase> GetTopVisitedList(int topNumber, string OperatorId)
        {
            return dal.GetTopVisitedList(topNumber, OperatorId);
        }

        /// <summary>
        /// 判断用户是否发布过这个标题的供求信息
        /// </summary>
        /// <param name="strUserId">用户Id</param>
        /// <param name="strTitle">供求信息标题</param>
        /// <param name="strExchangeId">要排除的供求Id，为空不排除</param>
        /// <returns>返回是否存在</returns>
        public bool ExistsSameTitle(string strUserId, string strTitle, string strExchangeId)
        {
            if (string.IsNullOrEmpty(strUserId) || string.IsNullOrEmpty(strTitle))
                return false;

            return dal.ExistsSameTitle(strUserId, strTitle, strExchangeId, null, null);
        }

        /// <summary>
        /// 获取用户当天发布的供求信息数量
        /// </summary>
        /// <param name="strUserId">用户Id</param>
        /// <returns>返回是否存在</returns>
        public int GetExchangeListCount(string strUserId)
        {
            if (string.IsNullOrEmpty(strUserId))
                return 0;

            string start = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
            string end = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            return dal.GetExchangeListCount(strUserId, DateTime.Parse(start), DateTime.Parse(end));
        }

        #endregion

        #region zhengfj
        /// <summary>
        /// 添加供求信息
        /// </summary>
        /// <param name="model">供求实体</param>
        /// <param name="ProvinceIds">发布到的省份数组</param>
        /// <param name="isCreateOfferId">是否生成供求编号</param>
        /// <returns>true:成功 false:失败</returns>
        private bool AddExchangeList(EyouSoft.Model.CommunityStructure.ExchangeList model, int[] ProvinceIds, bool isCreateOfferId)
        {
            if (model == null)
                return false;

            if (isCreateOfferId)
            {
                model.ID = Guid.NewGuid().ToString();
            }

            model.CityContactList = new List<EyouSoft.Model.CommunityStructure.ExchangeCityContact>();
            model.CityContactList.Clear();
            IList<Model.SystemStructure.SysCity> listCity = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetProvinceCityList(ProvinceIds);
            if (listCity != null)
            {
                foreach (Model.SystemStructure.SysCity CityModel in listCity)
                {
                    if (CityModel.CityId != model.CityId)
                        model.CityContactList.Add(new EyouSoft.Model.CommunityStructure.ExchangeCityContact(model.ID, CityModel.ProvinceId, CityModel.CityId));
                }
            }
            model.CityContactList.Add(new EyouSoft.Model.CommunityStructure.ExchangeCityContact(model.ID, model.ProvinceId, model.CityId));
            return dal.AddExchangeList(model);
        }

        /// <summary>
        /// 添加供求信息
        /// </summary>
        /// <param name="model">供求实体</param>
        /// <param name="ProvinceIds">发布到的省份数组</param>
        /// <returns>true:成功 false:失败</returns>
        public bool AddExchangeList(EyouSoft.Model.CommunityStructure.ExchangeList model, int[] ProvinceIds)
        {
            return AddExchangeList(model, ProvinceIds,true);
        }

        /// <summary>
        /// 修改供求信息
        /// </summary>
        /// <param name="model">供求实体</param>
        /// <param name="ProvinceIds">省份数组</param>
        /// <returns>true:成功 false:失败</returns>
        public bool UpdateExchangeList(EyouSoft.Model.CommunityStructure.ExchangeList model, int[] ProvinceIds)
        {
            if (model == null)
                return false;

            model.CityContactList = new List<EyouSoft.Model.CommunityStructure.ExchangeCityContact>();
            model.CityContactList.Clear();
            IList<Model.SystemStructure.SysCity> listCity = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetProvinceCityList(ProvinceIds);
            if (listCity != null)
            {
                foreach (Model.SystemStructure.SysCity CityModel in listCity)
                {
                    if (CityModel.CityId != model.CityId)
                        model.CityContactList.Add(new EyouSoft.Model.CommunityStructure.ExchangeCityContact(model.ID, CityModel.ProvinceId, CityModel.CityId));
                }
            }
            model.CityContactList.Add(new EyouSoft.Model.CommunityStructure.ExchangeCityContact(model.ID, model.ProvinceId, model.CityId));
            return dal.UpdateExchangeList(model);
        }
        /// <summary>
        /// 获取当天的供求信息量
        /// </summary>
        /// <param name="way">true:总供求量 false:当天供求量</param>
        /// <returns>当天供求量总数</returns>
        public int GetExchangeListCount(bool way)
        {
            return dal.GetExchangeListCount(way);
        }
        /// <summary>
        /// 获取各分类的供求总数
        /// </summary>
        /// <param name="way">true:总供求量 false:当天供求量</param>
        /// <returns>字典</returns>
        public Dictionary<EyouSoft.Model.CommunityStructure.ExchangeType, int> GetExchangeTypeCount(bool way)
        {
            return dal.GetExchangeTypeCount(way);
        }
        /// <summary>
        /// 分页获取供求信息(请指定SearchInfo的BeforeOrAfter属性)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="info">搜索实体</param>
        /// <returns>供求信息列表</returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CommunityStructure.SearchInfo info)
        {
            if (info == null)
                return null;
            return dal.GetList(pageSize, pageIndex, ref recordCount, info);
        }
        /// <summary>
        /// 根据类别，标签获取知道条数的供求信息
        /// </summary>
        /// <param name="topNum">指定条数</param>
        /// <param name="exchangeType">类别</param>
        /// <param name="exchangeTag">标签</param>
        /// <param name="way">true:最新供求 false:同类其他供求</param>
        /// <returns>供求信息集合</returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetTopList(int topNum, EyouSoft.Model.CommunityStructure.ExchangeType exchangeType, EyouSoft.Model.CommunityStructure.ExchangeTag exchangeTag, bool way)
        {
            return dal.GetTopList(topNum, exchangeType, exchangeTag, way);
        }


        /// <summary>
        /// 指定条数获取最新供求信息
        /// </summary>
        /// <param name="topNum">指定条数</param>
        /// <returns>供求信息集合</returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetTopList(int topNum)
        {
            return dal.GetTopList(topNum);
        }

        #endregion

        /// <summary>
        /// 写入QQ群消息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">QQ群消息业务实体</param>
        /// <returns>1：成功 其它失败</returns>
        public int QG_InsertQQGroupMessage(EyouSoft.Model.CommunityStructure.MQQGroupMessageInfo info)
        {
            info.MessageId = Guid.NewGuid().ToString();
            return dal.QG_InsertQQGroupMessage(info);
        }
        /// <summary>
        /// 获取QQ群消息业务实体
        /// </summary>
        /// <param name="messageId">消息编号</param>
        /// <returns></returns>
        public EyouSoft.Model.CommunityStructure.MQQGroupMessageInfo QG_GetQQGroupMessageInfo(string messageId)
        {
            if (string.IsNullOrEmpty(messageId)) return null;
            return dal.QG_GetQQGroupMessageInfo(messageId);
        }
        /// <summary>
        /// 设置QQ群消息状态
        /// </summary>
        /// <param name="messageId">消息编号</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public bool QG_SetQQGroupMessageStatus(string messageId, EyouSoft.Model.CommunityStructure.QQGroupMessageStatus status)
        {
            if (string.IsNullOrEmpty(messageId)) return true;

            bool isToOffer = false;
            EyouSoft.Model.CommunityStructure.MQQGroupMessageInfo info = QG_GetQQGroupMessageInfo(messageId);

            if (info != null)
            {
                EyouSoft.Model.CommunityStructure.ExchangeList offer = new EyouSoft.Model.CommunityStructure.ExchangeList();
                offer.AttatchPath = string.Empty;
                offer.CityId = 0;
                offer.CompanyId = string.Empty;
                offer.CompanyName = string.Empty;
                offer.ContactName = string.Empty;
                offer.ContactTel = string.Empty;
                offer.ExchangeTag = EyouSoft.Model.CommunityStructure.ExchangeTag.无;
                offer.ExchangeText = info.Content;
                offer.ExchangeTitle = info.Title;
                offer.ID = info.MessageId;
                offer.IssueTime = DateTime.Now;
                offer.OperatorId = string.Empty;
                offer.OperatorMQ = string.Empty;
                offer.OperatorName = string.Empty;
                offer.ProvinceId = 0;
                offer.ExchangeCategory = EyouSoft.Model.CommunityStructure.ExchangeCategory.QGroup;
                offer.TopicClassID = EyouSoft.Model.CommunityStructure.ExchangeType.其他;

                isToOffer = AddExchangeList(offer, null, false);
            }

            if (isToOffer)
            {
                return dal.QG_SetQQGroupMessageStatus(messageId, status);
            }

            return false;
        }

        /// <summary>
        /// 设置QQ群消息供求标题
        /// </summary>
        /// <param name="offerId">供求信息编号</param>
        /// <param name="title">供求标题</param>
        /// <returns></returns>
        public bool QG_SetQQGroupOfferTitle(string offerId, string title)
        {
            if (string.IsNullOrEmpty(offerId)) return false;
            return dal.QG_SetQQGroupOfferTitle(offerId, title);
        }

    }
}
