using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-25
    /// 描述：公司资讯业务层接口
    /// </summary>
    public interface ICompanyAffiche
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="Model">公司资讯实体</param>
        /// <returns>false:失败 true:成功</returns>
        bool Add(EyouSoft.Model.CompanyStructure.CompanyAffiche Model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Model">公司资讯实体</param>
        /// <returns>false:失败 true:成功</returns>
        bool Update(EyouSoft.Model.CompanyStructure.CompanyAffiche Model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        bool Delete(int ID);
        /// <summary>
        /// 设置置顶
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="IsTop">是否置顶</param>
        /// <returns>false:失败 true:成功</returns>
        bool SetIsTop(int ID, bool IsTop);
        /// <summary>
        /// 更新阅读次数
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        bool SetClicks(int ID);
        /// <summary>
        /// 设置热门
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="IsHot">是否热门</param>
        /// <returns>false:失败 true:成功</returns>
        bool SetIsHot(int ID, bool IsHot);
        /// <summary>
        /// 获取资讯实体类
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>资讯实体</returns>
        EyouSoft.Model.CompanyStructure.CompanyAffiche GetModel(int ID);
        /// <summary>
        /// 分页获取指定公司指定资讯类别的列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyID">公司编号 =""返回所有公司，否则返回指定公司的数据</param>
        /// <param name="afficheType">资讯类别 =null返回所有类别，否则返回指定类别的数据</param>
        /// <param name="IsHot">是否热门 =null返回所有</param>
        /// <param name="IsTop">是否置顶 =null返回所有</param>
        /// <param name="IsPicNews">是否图片新闻 =null返回所有</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.CompanyAffiche> GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyID, EyouSoft.Model.CompanyStructure.CompanyAfficheType? afficheType, bool? IsHot, bool? IsTop, bool? IsPicNews);
    }
}
