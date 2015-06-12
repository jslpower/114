using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.MQStructure
{
    /// <summary>
    /// MQ订单提醒设置数据访问接口
    /// </summary>
    /// 周文超 2010-05-11
    public interface IIMAcceptMsgPeople
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="ID">订单提醒设置ID</param>
        /// <returns></returns>
        bool Exists(string ID);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">MQ订单提醒设置实体</param>
        /// <returns>返回操作是否成功</returns>
        bool Add(Model.MQStructure.IMAcceptMsgPeople model);

        /// <summary>
        /// 更新一条数据(时间不更新)
        /// </summary>
        /// <param name="model">MQ订单提醒设置实体</param>
        /// <returns>返回操作是否成功</returns>
        bool Update(Model.MQStructure.IMAcceptMsgPeople model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="ID">MQ订单提醒设置ID</param>
        /// <returns>返回操作是否成功</returns>
        bool Delete(string ID);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="OperatorId">操作员编号</param>
        /// <param name="TourAreaId">区域ID</param>
        /// <returns>返回操作是否成功</returns>
        bool Delete(string CompanyId, string OperatorId, int TourAreaId);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="ID">MQ订单提醒设置ID</param>
        /// <returns>MQ订单提醒设置对象实体</returns>
        Model.MQStructure.IMAcceptMsgPeople GetModel(string ID);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="CompanyId">公司ID(为null不作条件)</param>
        /// <param name="OperatorId">操作员ID(为null不作条件)</param>
        /// <param name="TourAreaId">区域ID(小于等于0不作条件)</param>
        /// <returns>返回MQ订单提醒设置实体集合</returns>
        IList<EyouSoft.Model.MQStructure.IMAcceptMsgPeople> GetAcceptMsgList(string CompanyId, string OperatorId, int TourAreaId);
    }
}
