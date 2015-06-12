using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.ShopStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-01
    /// 描述：出游指南接口
    /// </summary>
    public interface IHighShopTripGuide
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">出游指南实体</param>
        /// <returns>false:失败 true:成功</returns>
        bool Add(EyouSoft.Model.ShopStructure.HighShopTripGuide model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">出游指南实体</param>
        /// <returns>false:失败 true:成功</returns>
        bool Update(EyouSoft.Model.ShopStructure.HighShopTripGuide model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        bool Delete(string ID);
        /// <summary>
        /// 获取出游指南实体
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>存在返回出游指南实体,不存在返回NULL</returns>
        EyouSoft.Model.ShopStructure.HighShopTripGuide GetModel(string ID);
        /// <summary>
        /// 获取后台出游指南列表集合
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyID">公司编号 为空返回全部否则返回指定公司的所有记录</param>
        /// <param name="KeyWord">需要匹配的关键字</param> 
        /// <param name="typeList">类别ID,若不包含ID,则返回全部</param>
        /// <returns>出游指南列表集合</returns>
        IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyID, string KeyWord, params EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType[] typeList);
        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="IsTop">置顶状态 TRUE或FALSE</param>
        /// <returns>false:失败 true:成功</returns>
        bool SetTop(string ID, bool IsTop);
        /// <summary>
        /// 获取前台指定条数的出游指南列表
        /// </summary>
        /// <param name="TopNumber">需要返回的条数 =0返回全部 >0返回指定条数的记录</param>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="TypeID">类别编号 =0返回所有类别 >0返回指定类别的数据</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <returns></returns>
        IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> GetWebList(int TopNumber, string CompanyID, int TypeID,string KeyWord);
    }
}
