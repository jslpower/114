using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.ShopStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-01
    /// 描述：旅游资源推荐接口
    /// </summary>
    public interface IHighShopResource
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">旅游资源推荐实体</param>
        /// <returns>false:失败 true:成功</returns>
        bool Add(EyouSoft.Model.ShopStructure.HighShopResource model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">旅游资源推荐实体</param>
        /// <returns>false:失败 true:成功</returns>
        bool Update(EyouSoft.Model.ShopStructure.HighShopResource model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        bool Delete(string ID);
        /// <summary>
        /// 设置置顶
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <param name="IsTop">置顶状态</param>
        /// <returns>false:失败 true:成功</returns>
        bool SetTop(string ID, bool IsTop);
        /// <summary>
        /// 获取旅游资源推荐实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        EyouSoft.Model.ShopStructure.HighShopResource GetModel(string ID);
        /// <summary>
        /// 分页获取旅游资源推荐列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyID">公司编号 为空返回全部否则返回指定公司的数据</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <returns>旅游资源推荐列表集合</returns>
        IList<EyouSoft.Model.ShopStructure.HighShopResource> GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyID, string KeyWord);
        /// <summary>
        /// 获取首页旅游资源推荐列表
        /// </summary>
        /// <param name="topNumber">需要返回的总记录数 =0返回全部 >0返回指定条数的记录</param>
        /// <param name="CompanyID">公司编号</param>
        /// <returns>旅游资源推荐列表集合</returns>
        IList<EyouSoft.Model.ShopStructure.HighShopResource> GetIndexList(int topNumber, string CompanyID);
    }
}
