using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SystemStructure;

namespace EyouSoft.IBLL.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-31
    /// 描述：后台登陆页广告 业务逻辑接口
    /// </summary>
    public interface ICompanyAdv
    {
        /// <summary>
        ///  运营后台分页获取列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyType">公司类型</param>
        /// <returns></returns>
        IList<CompanyAdv> GetList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CompanyStructure.CompanyType companyType);
        /// <summary>
        /// 获取指定条数的后台登陆广告
        /// </summary>
        /// <param name="topNumber">要返回的记录数</param>
        /// <param name="companyType">公司类型</param>
        /// <returns></returns>
        IList<CompanyAdv> GetTopNumList(int topNumber, EyouSoft.Model.CompanyStructure.CompanyType companyType);
        /// <summary>
        /// 获取指定公司类型的后台首页图片广告
        /// </summary>
        /// <param name="companyType">公司类型</param>
        /// <returns></returns>
        CompanyAdv GetIndexPicAdv(EyouSoft.Model.CompanyStructure.CompanyType companyType);

        /// <summary>
        /// 添加文字广告
        /// </summary>
        /// <returns>false:失败 true:成功</returns>
        bool Add(CompanyAdv model);
        /// <summary>
        /// 添加图片广告
        /// </summary>
        /// <param name="companyType">公司类型</param>
        /// <param name="ImgPath">图片路径</param>
        /// <param name="AdvLink">链接地址</param>
        /// <returns>false:失败 true:成功</returns>
        bool AddPicAdv(EyouSoft.Model.CompanyStructure.CompanyType companyType, string ImgPath, string AdvLink);
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns>false:失败 true:成功</returns>
        bool Update(CompanyAdv model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <returns>false:失败 true:成功</returns>
        bool Delete(int id);
        /// <summary>
        /// 设置排序
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <param name="sortnumber">排序值</param>
        /// <returns>false:失败 true:成功</returns>
        bool SetSort(int id, int sortnumber);
    }
}
