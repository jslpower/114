using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.ShopStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-01
    /// 描述：轮换广告接口
    /// </summary>
    public interface IHighShopAdv
    {
        /// <summary>
        /// 修改网店轮换广告
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="list">轮换广告列表集合</param>
        /// <returns>false:失败 true:成功</returns>
        bool Update(string CompanyID, IList<EyouSoft.Model.ShopStructure.HighShopAdv> list);
        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        bool DeletePic(string ID);
        /// <summary>
        /// 获取指定编号的轮换广告信息
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <returns>轮换广告列表集合</returns>
        EyouSoft.Model.ShopStructure.HighShopAdv GetModel(string CompanyID);
        /// <summary>
        /// 获取后台轮换广告列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyID">公司编号 为空返回全部，否则返回指定公司的轮换广告集合</param>
        /// <returns>轮换广告列表集合</returns>
        IList<EyouSoft.Model.ShopStructure.HighShopAdv> GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyID);
        /// <summary>
        /// 前台获取指定公司的轮换广告列表
        /// </summary>
        /// <param name="TopNumber">需要获取的记录数</param>
        /// <param name="CompanyID">公司编号</param>
        /// <returns>轮换广告列表集合</returns>
        IList<EyouSoft.Model.ShopStructure.HighShopAdv> GetWebList(int TopNumber,string CompanyID);
    }
}
