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
    /// 创建人：鲁功源 2010-07-14
    /// 描述：嘉宾访谈数据层接口
    /// </summary>
    public class HonoredGuest:IBLL.CommunityStructure.IHonoredGuest
    {
        private readonly IDAL.CommunityStructure.IHonoredGuest dal = ComponentFactory.CreateDAL<IDAL.CommunityStructure.IHonoredGuest>();

        #region CreateInstance
        /// <summary>
        /// 创建IBLL实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CommunityStructure.IHonoredGuest CreateInstance()
        {
            EyouSoft.IBLL.CommunityStructure.IHonoredGuest op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.CommunityStructure.IHonoredGuest>();
            }
            return op1;
        }
        #endregion

        #region IHonoredGuest成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">嘉宾访谈实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Add(EyouSoft.Model.CommunityStructure.HonoredGuest model)
        {
            if (model == null)
                return false;
            model.ID = Guid.NewGuid().ToString();
            return dal.Add(model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">嘉宾访谈实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Update(EyouSoft.Model.CommunityStructure.HonoredGuest model)
        {
            if (model == null)
                return false;
            return dal.Update(model);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Ids">主键编号集合</param>
        /// <returns>true:成功 false:失败</returns>
        public bool DeleteByIds(string[] Ids)
        {
            if (Ids == null || Ids.Length == 0)
                return false;

            return dal.Delete(Ids);
        }
        /// <summary>
        /// 单个删除
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool DeleteById(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return false;
            return dal.Delete(Id);
        }
        /// <summary>
        /// 获取最新一期的嘉宾访谈
        /// </summary>
        /// <returns>嘉宾访谈实体</returns>
        public EyouSoft.Model.CommunityStructure.HonoredGuest GetNewInfo()
        {
            return dal.GetNewInfo();
        }
        /// <summary>
        /// 获取嘉宾访谈实体
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        public EyouSoft.Model.CommunityStructure.HonoredGuest GetModel(string Id)
        {
            return dal.GetModel(Id);
        }
        /// <summary>
        /// 获取指定条数的嘉宾访谈列表集合
        /// </summary>
        /// <param name="topNumber">需要返回的记录数 =0返回全部</param>
        /// <returns>嘉宾访谈列表集合</returns>
        public IList<EyouSoft.Model.CommunityStructure.HonoredGuest> GetTopNumList(int topNumber)
        {
            return dal.GetTopNumList(topNumber);
        }
        /// <summary>
        /// 分页获取嘉宾访谈列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <returns>嘉宾访谈列表集合</returns>
        public IList<EyouSoft.Model.CommunityStructure.HonoredGuest> GetPageList(int pageSize, int pageIndex, ref int recordCount, string KeyWord)
        {
            return dal.GetPageList(pageSize, pageIndex, ref recordCount, KeyWord);
        }
        #endregion
    }
}
