using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.ShopStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-01
    /// 描述：友情链接接口
    /// </summary>
    public interface IHighShopFriendLink
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">友情链接实体</param>
        /// <returns>false:失败 true:成功</returns>
        bool Add(EyouSoft.Model.ShopStructure.HighShopFriendLink model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">友情链接实体</param>
        /// <returns>false:失败 true:成功</returns>
        bool Update(EyouSoft.Model.ShopStructure.HighShopFriendLink model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        bool Delete(string ID);
        /// <summary>
        /// 获取指定公司的友情链接列表
        /// </summary>
        /// <param name="CompanyID">公司编号 为空返回全部否则返回指定公司的记录</param>
        /// <returns>友情链接列表</returns>
        IList<EyouSoft.Model.ShopStructure.HighShopFriendLink> GetList(string CompanyID);
    }
}
