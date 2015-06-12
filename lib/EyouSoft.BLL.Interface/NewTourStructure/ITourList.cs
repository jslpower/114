using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.NewTourStructure;

namespace EyouSoft.IBLL.NewTourStructure
{
    /// <summary>
    /// 团队计划,订单接口
    /// 创建者：曹胡生
    /// 创建时间：2011-12-20
    /// </summary>
    public interface ITourList
    {
        /// <summary>
        /// 添加团队计划,订单
        /// </summary>
        /// <param name="item">团队计划,订单实体</param>
        /// <returns></returns>
        bool AddTourList(MTourList item);

        /// <summary>
        /// 获取团队计划,订单实体
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <returns>团队计划,订单实体</returns>
        MTourList GetModel(string TourId);

        /// <summary>
        /// 获得专线商订单
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>团队订单集合</returns>
        IList<EyouSoft.Model.NewTourStructure.MTourList> GetZXList(int pageSize, int pageCurrent, ref int recordCount, string companyId, EyouSoft.Model.NewTourStructure.MTourListSearch search);

        /// <summary>
        /// 获得地接社订单
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>团队订单集合</returns>
        IList<EyouSoft.Model.NewTourStructure.MTourList> GetDJList(int pageSize, int pageCurrent, ref int recordCount, string companyId, EyouSoft.Model.NewTourStructure.MTourListSearch search);

        /// <summary>
        /// 团队订单(组团社)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>团队订单集合</returns>
        IList<EyouSoft.Model.NewTourStructure.MTourList> GetZTList(int pageSize, int pageCurrent, ref int recordCount, string companyId, EyouSoft.Model.NewTourStructure.MTourListSearch search);

        /// <summary>
        /// 团队订单-运营后台
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>团队订单集合</returns>
        IList<MTourList> GetList(int pageSize, int pageCurrent, ref int recordCount, MTourListSearch search);

        /// <summary>
        /// 团队订单状态更改
        /// </summary>
        /// <param name="Status">更改的状态</param> 
        /// <param name="TourId"></param>
        /// <returns></returns>
        bool TourOrderStatusChange(TourOrderStatus Status, string TourId);

        /// <summary>
        /// 运营前台 单团订单 专线或地接修改
        /// </summary>
        /// <param name="model">修改实体</param>
        /// <returns></returns>
        bool OrderModifyZXDJ(MTourList model);

        /// <summary>
        /// 运营前台 单团订单 组团修改
        /// </summary>
        /// <param name="model">修改实体</param>
        /// <returns></returns>
        bool OrderModifyZT(MTourList model);

        /// <summary>
        /// 专线商订单统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        Dictionary<string, int> GetOrderBusinessCount(string companyId);
        /// <summary>
        /// 地接社订单统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        Dictionary<string, int> GetOrderGroundCount(string companyId);
        /// <summary>
        /// 组团社订单统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        Dictionary<string, int> GetOrderTravelCount(string companyId);
    }
}
