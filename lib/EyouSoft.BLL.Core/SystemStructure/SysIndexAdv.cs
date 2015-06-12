using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;
using EyouSoft.IBLL.SystemStructure;


namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-12
    /// 描述：首页轮换广告业务逻辑层
    /// </summary>
    public class SysIndexAdv:ISysIndexAdv
    {
        private readonly IDAL.SystemStructure.ISysIndexAdv dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.ISysIndexAdv>();

        #region CreateInstance
        /// <summary>
        /// 创建首页轮换广告业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.SystemStructure.ISysIndexAdv CreateInstance()
        {
            EyouSoft.IBLL.SystemStructure.ISysIndexAdv op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.SystemStructure.ISysIndexAdv>();
            }
            return op1;
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns>false:失败 true:成功</returns>
        public bool Add(EyouSoft.Model.SystemStructure.SysIndexAdv model)
        {
            if (model == null)
                return false;
            return dal.Add(model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">首页广告实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Update(EyouSoft.Model.SystemStructure.SysIndexAdv model)
        {
            if (model == null)
                return false;
            return dal.Update(model);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Delete(int id)
        {
            if (id==0)
                return false;
            return dal.Delete(id);
        }
        /// <summary>
        /// 运营后台分页获取列表
        /// </summary>
        /// <param name="siteId">分站ID >0时返回该分站下的下广告 =0时返回全部</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码数</param>
        /// <param name="recordeCount">总记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.SysIndexAdv> GetList(int siteId, int pageSize, int pageIndex, ref int recordeCount)
        {
            return dal.GetList(siteId, pageSize, pageIndex, ref recordeCount);
        }
        /// <summary>
        /// 获取指定条数的前台轮换广告列表
        /// </summary>
        /// <param name="siteId">分站ID >0时返回该分站下的轮换广告信息 =0时返回全部</param>
        /// <param name="topnumber">指定返回条数 >0时返回指定条数记录 =0返回全部</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.SysIndexAdv> GetTopList(int siteId, int topnumber)
        {
            return dal.GetTopList(siteId, topnumber);
        }
        /// <summary>
        /// 获取广告实体
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>返回广告实体</returns>
        public EyouSoft.Model.SystemStructure.SysIndexAdv GetModel(int id)
        {
            if (id == 0)
                return null;
            return dal.GetModel(id);
        }
        /// <summary>
        /// 设置排序
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <param name="sortnumber">排序值</param>
        /// <returns>false:失败 true:成功</returns>
        public bool SetSort(int id, int sortnumber)
        {
            return dal.SetSort(id, sortnumber);
        }
        #endregion
    }
}
