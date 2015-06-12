using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-16
    /// 描述：供求信息数据层接口
    /// </summary>
    public interface IExchangeList
    {

        #region zhengfj
        /// <summary>
        /// 添加供求信息
        /// </summary>
        /// <param name="model">供求实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool AddExchangeList(EyouSoft.Model.CommunityStructure.ExchangeList model);

        /// <summary>
        /// 修改供求信息
        /// </summary>
        /// <param name="model">供求实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool UpdateExchangeList(EyouSoft.Model.CommunityStructure.ExchangeList model);

        /// <summary>
        /// 获取供求信息量
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
        /// 根据类别，标签获取指定条数的供求信息
        /// </summary>
        /// <param name="topNum">指定条数</param>
        /// <param name="exchangeType">类别</param>
        /// <param name="exchangeTag">标签</param>
        /// <param name="way">true:最新供求 false:同类其他供求</param>
        /// <returns>供求信息集合</returns>
        IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetTopList(int topNum, EyouSoft.Model.CommunityStructure.ExchangeType exchangeType,
            EyouSoft.Model.CommunityStructure.ExchangeTag exchangeTag, bool way);

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
        /// <returns>true:成功 false:失败</returns>
        bool Add(EyouSoft.Model.CommunityStructure.ExchangeList model);
        /// <summary>
        /// 修改供求信息
        /// </summary>
        /// <param name="model">供求信息实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Update(EyouSoft.Model.CommunityStructure.ExchangeList model);
        /// <summary>
        /// 获取供求信息实体
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>供求信息实体</returns>
        EyouSoft.Model.CommunityStructure.ExchangeList GetModel(string ID);
        /// <summary>
        /// 删除供求信息【同时删除浏览过或者收藏过该信息的记录】
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        bool Delete(string ID);
        /// <summary>
        /// 设置置顶状态
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <param name="IsTop">是否置顶</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetTop(string ID, bool IsTop);
        /// <summary>
        /// 设置审核状态
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <param name="IsCheck">是否审核</param>
        /// <returns>true:成功 false:失败</returns>
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
        /// 分页获取供求信息
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="ExchangeType">供求类别 =null返回全部</param>
        /// <param name="Tag">供求标签 =null返回全部</param>
        /// <param name="IsTop">是否置顶 =null返回全部</param>
        /// <param name="ProvinceId">省份编号 =0返回全部</param>
        /// <param name="CityId">城市编号 =0返回全部</param>
        /// <param name="IssueTime">添加时间 =null返回全部</param>
        /// <param name="OperatorId">用户编号 =""返回全部</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="Days">最近天数 =null返回全部</param>
        /// <param name="IsCurrWeek">是否本周 =null返回全部</param>
        /// <param name="IsCurrMonth">是否本月 =null返回全部</param>
        /// <param name="IsCheck">是否已审核 =null返回全部</param>
        /// <returns>供求信息列表</returns>
        IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CommunityStructure.ExchangeType? ExchangeType,
            EyouSoft.Model.CommunityStructure.ExchangeTag? Tag, bool? IsTop, int ProvinceId, int CityId, DateTime? IssueTime, string OperatorId, string KeyWord, DateTime? StartDate, DateTime? EndDate,
            int? Days, bool? IsCurrWeek, bool? IsCurrMonth, bool? IsCheck);
        /// <summary>
        /// 得到MQ内嵌页供求列表（共40条，前五条显示置顶的）
        /// </summary>
        /// <param name="ProvinceIds">某几个省份下的供求信息</param>
        /// <param name="Stime">时间（全部=0,今天=1,昨天=2,前天=3,更早=4</param>
        /// <param name="ExchangeTitle">要搜索的标题关键字</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetList(string ProvinceIds, int Stime, string ExchangeTitle);
        /// <summary>
        /// 返回指定条数的供求信息列表
        /// </summary>
        /// <param name="TopNumber">需要返回的记录数</param>
        /// <param name="ExchangeType">供求类别 =null返回全部</param>
        /// <param name="Tag">供求标签 =null返回全部</param>
        /// <param name="IsTop">是否置顶 =null返回全部</param>
        /// <param name="ProvinceId">省份编号 =0返回全部</param>
        /// <param name="CityId">城市编号 =0返回全部</param>
        /// <param name="IssueTime">添加时间 =null返回全部</param>
        /// <param name="OperatorId">用户编号 =""返回全部</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <returns>供求信息列表</returns>
        IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetTopNumList(int TopNumber, EyouSoft.Model.CommunityStructure.ExchangeType? ExchangeType,
            EyouSoft.Model.CommunityStructure.ExchangeTag? Tag, bool? IsTop, int ProvinceId, int CityId, DateTime? IssueTime, string OperatorId, string KeyWord);
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
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>返回是否存在</returns>
        bool ExistsSameTitle(string strUserId, string strTitle, string strExchangeId, DateTime? startTime
            , DateTime? endTime);

        /// <summary>
        /// 获取用户某个时间段内发布的供求信息数量
        /// </summary>
        /// <param name="strUserId">用户Id</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>返回是否存在</returns>
        int GetExchangeListCount(string strUserId, DateTime? startTime, DateTime? endTime);

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
