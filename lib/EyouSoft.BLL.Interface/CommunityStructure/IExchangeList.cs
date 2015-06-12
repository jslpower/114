using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.AOPHandler;
namespace EyouSoft.IBLL.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-16
    /// 描述：供求信息业务层接口
    /// </summary>
    public interface IExchangeList
    {

        #region zhengfj
        /// <summary>
        /// 添加供求信息
        /// </summary>
        /// <param name="model">供求实体</param>
        /// <param name="ProvinceIds">发布到的省份数组</param>
        /// <returns>true:成功 false:失败</returns>
        [CommonLogHandler(LogTitle = EyouSoft.BusinessLogWriter.ServiceLog.LOG_ExchangeList_INSERT_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.ServiceLog.LOG_ExchangeList_INSERT,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.ServiceLog),
            LogAttribute = @"[{""index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.ServiceLog.LOG_ExchangeList_INSERT_CODE)]
        bool AddExchangeList(EyouSoft.Model.CommunityStructure.ExchangeList model, int[] ProvinceIds);
        /// <summary>
        /// 修改供求信息
        /// </summary>
        /// <param name="model">供求实体</param>
        /// <param name="ProvinceIds">省份数组</param>
        /// <returns>true:成功 false:失败</returns>
        [CommonLogHandler(
        LogTitle = EyouSoft.BusinessLogWriter.ServiceLog.LOG_ExchangeList_UPDATE_TITLE,
        LogMessage = EyouSoft.BusinessLogWriter.ServiceLog.LOG_ExchangeList_UPDATE,
        LogWriterType = typeof(EyouSoft.BusinessLogWriter.ServiceLog),
        LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]",
        EventCode = EyouSoft.BusinessLogWriter.ServiceLog.LOG_ExchangeList_UPDATE_CODE)]
        bool UpdateExchangeList(EyouSoft.Model.CommunityStructure.ExchangeList model, int[] ProvinceIds);
        /// <summary>
        /// 获取当天的供求信息量
        /// </summary>
        /// <param name="way">true:总供求量 false:当天供求量</param>
        /// <returns>当天供求量总数</returns>
        int GetExchangeListCount(bool way);
        /// <summary>
        /// 获取各分类的供求总数
        /// </summary>
        /// <param name="way">true:总供求量 false:当天供求量</param>
        /// <returns>字典</returns>
        Dictionary<EyouSoft.Model.CommunityStructure.ExchangeType, int> GetExchangeTypeCount(bool way);
        /// <summary>
        /// 分页获取供求信息
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="info">搜索实体</param>
        /// <returns>供求信息列表</returns>
        IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CommunityStructure.SearchInfo info);

        /// <summary>
        /// 根据类别，标签获取知道条数的供求信息
        /// </summary>
        /// <param name="topNum">指定条数</param>
        /// <param name="exchangeType">类别</param>
        /// <param name="exchangeTag">标签</param>
        /// <param name="way">true:最新供求 false:同类其他供求</param>
        /// <returns>供求信息集合</returns>
        IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetTopList(int topNum, EyouSoft.Model.CommunityStructure.ExchangeType exchangeType,
            EyouSoft.Model.CommunityStructure.ExchangeTag exchangeTag,bool way);

        /// <summary>
        /// 指定条数获取最新供求信息
        /// </summary>
        /// <param name="topNum">指定条数</param>
        /// <returns>供求信息集合</returns>
        IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetTopList(int topNum);

        #endregion

        /// <summary>
        /// 添加供求信息
        /// </summary>
        /// <param name="model">供求信息实体</param>
        /// <param name="ProvinceIds">发布到的省份数组</param>
        /// <returns>true:成功 false:失败</returns>
        [CommonLogHandler(
        LogTitle = EyouSoft.BusinessLogWriter.ServiceLog.LOG_ExchangeList_INSERT_TITLE,
        LogMessage = EyouSoft.BusinessLogWriter.ServiceLog.LOG_ExchangeFavor_INSERT,
        LogWriterType = typeof(EyouSoft.BusinessLogWriter.ServiceLog),
        LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]",
        EventCode = EyouSoft.BusinessLogWriter.ServiceLog.LOG_ExchangeFavor_INSERT_CODE)]
        bool Add(EyouSoft.Model.CommunityStructure.ExchangeList model, int[] ProvinceIds);
        /// <summary>
        /// 修改供求信息
        /// </summary>
        /// <param name="model">供求信息实体</param>
        /// <param name="ProvinceIds">发布到的省份数组</param>
        /// <returns>true:成功 false:失败</returns>
        [CommonLogHandler(
        LogTitle = EyouSoft.BusinessLogWriter.ServiceLog.LOG_ExchangeList_UPDATE_TITLE,
        LogMessage = EyouSoft.BusinessLogWriter.ServiceLog.LOG_ExchangeList_UPDATE,
        LogWriterType = typeof(EyouSoft.BusinessLogWriter.ServiceLog),
        LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]",
        EventCode = EyouSoft.BusinessLogWriter.ServiceLog.LOG_ExchangeList_UPDATE_CODE)]
        bool Update(EyouSoft.Model.CommunityStructure.ExchangeList model, int[] ProvinceIds);
        /// <summary>
        /// 获取供求信息实体
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>供求信息实体</returns>
        EyouSoft.Model.CommunityStructure.ExchangeList GetModel(string ID);
        /// <summary>
        /// 前台用户删除供求信息【同时删除浏览过或者收藏过该信息的记录】
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        [CommonLogHandler(
         LogTitle = EyouSoft.BusinessLogWriter.ServiceLog.LOG_ExchangeList_DELETE_TITLE,
         LogMessage = EyouSoft.BusinessLogWriter.ServiceLog.LOG_ExchangeList_DELETE,
         LogWriterType = typeof(EyouSoft.BusinessLogWriter.ServiceLog),
         LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""val""}]",
         EventCode = EyouSoft.BusinessLogWriter.ServiceLog.LOG_ExchangeList_DELETE_CODE)]
        bool Delete(string ID);
        /// <summary>
        /// 运营后台删除供求信息【同时删除浏览过或者收藏过该信息的记录】
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        [CommonLogHandler(
         LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_ExchangeList_DELETE_TITLE,
         LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_ExchangeList_DELETE,
         LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
         LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""val""}]",
         EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_ExchangeList_DELETE_CODE)]
        bool ManageDel(string ID);
        /// <summary>
        /// 设置置顶状态
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <param name="IsTop">是否置顶</param>
        /// <returns>true:成功 false:失败</returns>
        [CommonLogHandler(
         LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_ExchangeList_SETTOP_TITLE,
         LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_ExchangeList_SETTOP,
         LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
         LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""val""},{""Index"":1,""Attribute"":""IsTop"",""AttributeType"":""val""}]",
         EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_ExchangeList_SETTOP_CODE)]
        bool SetTop(string ID, bool IsTop);
        /// <summary>
        /// 设置审核状态
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <param name="IsCheck">是否审核</param>
        /// <returns>true:成功 false:失败</returns>
        [CommonLogHandler(
         LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_ExchangeList_SETISCHECK_TITLE,
         LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_ExchangeList_SETISCHECK,
         LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
         LogAttribute = @"[{""Index"":0,""Attribute"":""ID"",""AttributeType"":""val""},{""Index"":1,""Attribute"":""IsCheck"",""AttributeType"":""val""}]",
         EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_ExchangeList_SETISCHECK_CODE)]
        bool SetCheck(string ID, bool IsCheck);
        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="IDs">主键编号集合</param>
        /// <returns>true:成功 false:失败</returns>
        bool BatchCheck(string[] IDs);
        /// <summary>
        /// 更新浏览次数
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetReadCount(string ID);
        /// <summary>
        /// 更新回复次数
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetWriteBackCount(string ID);
        /// <summary>
        /// 删除供求信息图片
        /// </summary>
        /// <param name="ID">图片主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        bool DeletePhoto(string ID);
        /// <summary>
        /// 设置用户浏览记录
        /// </summary>
        /// <param name="model">供求浏览实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetVisited(EyouSoft.Model.CommunityStructure.ExchangeVisited model);
        /// <summary>
        /// 获取指定条数的供求信息
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <param name="ExchangeType">供求信息类别 =null返回全部</param>
        /// <param name="CityId">城市编号 =0返回全部</param>
        /// <param name="IsTop">是否置顶 =null返回全部</param>
        /// <returns>供求信息列表集合</returns>
        IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetTopList(int TopNum, EyouSoft.Model.CommunityStructure.ExchangeType? ExchangeType, int CityId, bool? IsTop);
        /// <summary>
        /// 分业获取指定类别指定城市的供求信息
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="ExchangeType">供求信息类别 =null返回全部</param>
        /// <param name="CityId">城市编号 =0返回全部</param>
        /// <returns>供求信息列表集合</returns>
        IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CommunityStructure.ExchangeType? ExchangeType, int CityId);
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
        IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetSerachList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CommunityStructure.ExchangeType? ExchangeType,
            EyouSoft.Model.CommunityStructure.ExchangeTag? Tag, DateTime? IssueTime, int ProvinceId, int CityId, string KeyWord,DateTime? StartDate,DateTime? EndDate);
        /// <summary>
        /// 根据查询条件分业获取供求信息
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="IsTop">是否置顶 =null返回全部</param>
        /// <returns>供求信息列表集合</returns>
        IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetList(int pageSize, int pageIndex, ref int recordCount, bool? IsTop);
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
        IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetSerachList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CommunityStructure.ExchangeType? ExchangeType,
             int ProvinceId, int CityId, string KeyWord, DateTime? StartDate, DateTime? EndDate,bool? IsCheck);
        /// <summary>
        /// 得到MQ内嵌页供求列表（共40条，前五条显示置顶的）
        /// </summary>
        /// <param name="ProvinceIds">某几个省份下的供求信息</param>
        /// <param name="Stime">时间（全部=0,今天=1,昨天=2,前天=3,更早=4</param>
        /// <param name="ExchangeTitle">要搜索的标题关键字</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetList(string ProvinceIds, int Stime, string ExchangeTitle);
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
        IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetListByOperator(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CommunityStructure.ExchangeType? ExchangeType,
            EyouSoft.Model.CommunityStructure.ExchangeTag? Tag, int ProvinceId, int CityId, string KeyWord, string OperatorId, DateTime? StartDate, DateTime? EndDate);
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
        IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetWebSerachList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CommunityStructure.ExchangeType? ExchangeType,
           EyouSoft.Model.CommunityStructure.ExchangeTag? Tag, int ProvinceId, int CityId, string KeyWord, EyouSoft.Model.CommunityStructure.SearchDateType SearchDateType);
        /// <summary>
        /// 获取供求信息标签列表
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.CommunityStructure.Tag> GetExchangeTags();
        /// <summary>
        /// 分页获取供求信息浏览记录
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="OperatorId">添加人编号 =""返回全部</param>
        /// <returns>供求信息浏览记录列表</returns>
        IList<EyouSoft.Model.CommunityStructure.ExchangeListBase> GetVisitedList(int pageSize, int pageIndex, ref int recordCount, string OperatorId);
        /// <summary>
        /// 获取指定条数的供求信息浏览记录
        /// </summary>
        /// <param name="topNumber">需要返回的记录数</param>
        /// <param name="OperatorId">添加人编号 =""返回全部</param>
        /// <returns>供求信息浏览记录列表</returns>
        IList<EyouSoft.Model.CommunityStructure.ExchangeListBase> GetTopVisitedList(int topNumber, string OperatorId);

        /// <summary>
        /// 判断用户是否发布过这个标题的供求信息
        /// </summary>
        /// <param name="strUserId">用户Id</param>
        /// <param name="strTitle">供求信息标题</param>
        /// <param name="strExchangeId">要排除的供求Id，为空不排除</param>
        /// <returns>返回是否存在</returns>
        bool ExistsSameTitle(string strUserId, string strTitle, string strExchangeId);

        /// <summary>
        /// 获取用户当天发布的供求信息数量
        /// </summary>
        /// <param name="strUserId">用户Id</param>
        /// <returns>返回是否存在</returns>
        int GetExchangeListCount(string strUserId);

        /// <summary>
        /// 写入QQ群消息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">QQ群消息业务实体</param>
        /// <returns>1：成功 其它失败</returns>
        int QG_InsertQQGroupMessage(EyouSoft.Model.CommunityStructure.MQQGroupMessageInfo info);
        /// <summary>
        /// 获取QQ群消息业务实体
        /// </summary>
        /// <param name="messageId">消息编号</param>
        /// <returns></returns>
        EyouSoft.Model.CommunityStructure.MQQGroupMessageInfo QG_GetQQGroupMessageInfo(string messageId);
        /// <summary>
        /// 设置QQ群消息状态
        /// </summary>
        /// <param name="messageId">消息编号</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        bool QG_SetQQGroupMessageStatus(string messageId, EyouSoft.Model.CommunityStructure.QQGroupMessageStatus status);
        /// <summary>
        /// 设置QQ群消息供求标题
        /// </summary>
        /// <param name="offerId">供求信息编号</param>
        /// <param name="title">供求标题</param>
        /// <returns></returns>
        bool QG_SetQQGroupOfferTitle(string offerId, string title);
    }
}
