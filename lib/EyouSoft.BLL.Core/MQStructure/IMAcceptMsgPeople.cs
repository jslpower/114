using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model;
using EyouSoft.IBLL;
using EyouSoft.IDAL;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.MQStructure
{
    /// <summary>
    /// MQ订单提醒设置
    /// </summary>
    /// 周文超 2010-06-12
    public class IMAcceptMsgPeople : IBLL.MQStructure.IIMAcceptMsgPeople
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMAcceptMsgPeople() { }

        private readonly IDAL.MQStructure.IIMAcceptMsgPeople dal = ComponentFactory.CreateDAL<IDAL.MQStructure.IIMAcceptMsgPeople>();

        /// <summary>
        /// 构造MQ订单提醒设置接口
        /// </summary>
        /// <returns></returns>
        public static IBLL.MQStructure.IIMAcceptMsgPeople CreateInstance()
        {
            IBLL.MQStructure.IIMAcceptMsgPeople op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.MQStructure.IIMAcceptMsgPeople>();
            }
            return op;
        }

        #region IIMAcceptMsgPeople 成员

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="ID">订单提醒设置ID</param>
        /// <returns></returns>
        public bool Exists(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return false;

            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">MQ订单提醒设置实体</param>
        /// <returns>返回操作是否成功</returns>
        public bool Add(EyouSoft.Model.MQStructure.IMAcceptMsgPeople model)
        {
            if (model == null)
                return false;

            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据(时间不更新)
        /// </summary>
        /// <param name="model">MQ订单提醒设置实体</param>
        /// <returns>返回操作是否成功</returns>
        public bool Update(EyouSoft.Model.MQStructure.IMAcceptMsgPeople model)
        {
            if (model == null)
                return false;

            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="ID">MQ订单提醒设置ID</param>
        /// <returns>返回操作是否成功</returns>
        public bool Delete(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return false;

            return dal.Delete(ID);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="OperatorId">操作员编号</param>
        /// <param name="TourAreaId">区域ID</param>
        /// <returns>返回操作是否成功</returns>
        public bool Delete(string CompanyId, string OperatorId, int TourAreaId)
        {
            return dal.Delete(CompanyId, OperatorId, TourAreaId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="ID">MQ订单提醒设置ID</param>
        /// <returns>MQ订单提醒设置对象实体</returns>
        public EyouSoft.Model.MQStructure.IMAcceptMsgPeople GetModel(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return null;

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="OperatorId">操作员ID</param>
        /// <param name="TourAreaId">区域ID</param>
        /// <returns>返回MQ订单提醒设置实体集合</returns>
        public IList<EyouSoft.Model.MQStructure.IMAcceptMsgPeople> GetAcceptMsgList(string CompanyId, string OperatorId, int TourAreaId)
        {
            return dal.GetAcceptMsgList(CompanyId, OperatorId, TourAreaId);
        }

        #endregion
    }
}
