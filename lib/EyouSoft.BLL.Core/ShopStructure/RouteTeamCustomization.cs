using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;
using EyouSoft.IBLL.ShopStructure;

namespace EyouSoft.BLL.ShopStructure
{
    /// <summary>
    /// 创建人：luofx 2010-11-09
    /// 描述：高级网店-团队制定BLL
    /// </summary>
    public class RouteTeamCustomization : EyouSoft.IBLL.ShopStructure.IRouteTeamCustomization
    {
        /// <summary>
        /// 底层调用接口
        /// </summary>
        private readonly IDAL.ShopStructure.IRouteTeamCustomization dal = ComponentFactory.CreateDAL<IDAL.ShopStructure.IRouteTeamCustomization>();
        #region CreateInstance
        /// <summary>
        /// 创建高级网店团队制定业务逻辑的实例
        /// </summary>
        /// <returns></returns>
        public static IRouteTeamCustomization CreateInstance()
        {
            EyouSoft.IBLL.ShopStructure.IRouteTeamCustomization op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.ShopStructure.IRouteTeamCustomization>();
            }
            return op1;
        }

        #endregion

        /// <summary>
        /// 新增团队制定
        /// </summary>
        /// <param name="model">高级网店团队制定实体</param>
        /// <returns>true:成功；false：失败</returns>
        public bool Add(EyouSoft.Model.ShopStructure.RouteTeamCustomization model)
        {
            return dal.Add(model);
        }
        /// <summary>
        /// 团队制定信息删除
        /// </summary>
        /// <param name="Id">团队制定编号（主键）</param>
        /// <param name="CompanyId">所属公司编号</param>
        /// <returns>true:成功；false：失败</returns>
        public bool Delete(int Id, string CompanyId)
        {
            return dal.Delete(Id, CompanyId);
        }
        /// <summary>
        /// 获取团队制定实体
        /// </summary>
        /// <param name="Id">团队制定的编号（主键）</param>
        /// <returns></returns>
        public EyouSoft.Model.ShopStructure.RouteTeamCustomization GetModel(int Id)
        {
            return dal.GetModel(Id);
        }
        /// <summary>
        /// 获取团队制定信息
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ShopStructure.RouteTeamCustomization> GetList(string CompanyId)
        {
            return dal.GetList(CompanyId);
        }
        /// <summary>
        /// 获取团队制定信息(分页)
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns>团队制定信息集合</returns>
        public IList<EyouSoft.Model.ShopStructure.RouteTeamCustomization> GetList(int PageSize, int PageIndex, ref int RecordCount, string CompanyId)
        {
            return dal.GetList(PageSize, PageIndex, ref RecordCount, CompanyId);
        }
    }
}
