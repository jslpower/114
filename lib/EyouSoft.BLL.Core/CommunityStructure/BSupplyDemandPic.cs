using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IBLL.CommunityStructure;
using EyouSoft.IDAL.CommunityStructure;
using EyouSoft.Component.Factory;
using EyouSoft.Model.CommunityStructure;

namespace EyouSoft.BLL.CommunityStructure
{
    /// <summary>
    /// 二次整改供求焦点图片业务逻辑层
    /// </summary>
    /// 创建人：mk 2011-5-10
    public class BSupplyDemandPic : IBSupplyDemandPic
    {
        private readonly IDSupplyDemandPic dal = ComponentFactory.CreateDAL<IDSupplyDemandPic>();

        #region CreateInstance

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <returns></returns>
        public static IBSupplyDemandPic CreateInstance()
        {
            IBSupplyDemandPic op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBSupplyDemandPic>();
            }
            return op;
        }

        #endregion

        #region IBSupplyDemandPic 成员

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(List<MSupplyDemandPic> list)
        {
            if (list == null || list.Count <= 0)
                return false;
            return dal.Add(list);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IList<MSupplyDemandPic> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
