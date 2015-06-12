using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-01
    /// 描述：供应商业务层接口
    /// </summary>
    /// 修改人：zhengfj 2011-6-1
    /// 修改内容:GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyName, int ProvinceId, int CityId, int CompanyLevel,
    ///        int CompanyTag, EyouSoft.Model.CompanyStructure.BusinessProperties? CompanyType,bool remark);
    public interface ISupplierInfo
    {
        /// <summary>
        /// 付费版供应商修改
        /// </summary>
        /// <param name="model">供应商实体类</param>
        /// <returns>true:成功 false:失败</returns>
        bool Update(EyouSoft.Model.CompanyStructure.SupplierInfo model);
        /// <summary>
        /// 免费版供应商修改
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="CompanyName">公司名称</param>
        /// <param name="Remark">公司介绍</param>
        /// <param name="ShortRemark">公司业务优势</param>
        /// <param name="CompanyLogo">公司LOGO</param>
        /// <returns>true:成功 false:失败</returns>
        bool Update(string CompanyId, string CompanyName, string Remark, string ShortRemark, string CompanyLogo);
        /// <summary>
        /// 付费版-设置供应商相关附件（企业宣传图，产品展示图片）
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="CompanyImg">公司宣传图</param>
        /// <param name="CompanyImgThumb">公司宣传图缩略图</param>
        /// <param name="list">产品附件集合</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetProduct(string CompanyId, string CompanyImg, IList<EyouSoft.Model.CompanyStructure.ProductInfo> list);
        /*
        /// <summary>
        /// 免费版-设置供应商相关附件（企业宣传图）
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="CompanyImg">公司宣传图</param>
        /// <param name="CompanyImgThumb">公司宣传图缩略图</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetProduct(string CompanyId, string CompanyImg, string CompanyImgThumb);*/
        /// <summary>
        /// 分页获取酒店列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyName">供应商名称（用于模糊查询）</param>
        /// <param name="ProvinceId">省份编号</param>
        /// <param name="CityId">城市编号</param>
        /// <param name="CompanyLevel">酒店星级</param>
        /// <param name="CompanyTag">酒店周边环境</param>
        /// <param name="CityName">城市名称 城市名称 =""返回指定CityId下的列表</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.SupplierInfo> GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyName, int ProvinceId, int CityId, int CompanyLevel,
            int CompanyTag, string CityName);
        /// <summary>
        /// 分页获取景区列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyName">供应商名称（用于模糊查询）</param>
        /// <param name="ProvinceId">省份编号</param>
        /// <param name="CityId">城市编号</param>
        /// <param name="CompanyTag">景区主题</param>
        /// <param name="CityName">城市名称 城市名称 =""返回指定CityId下的列表</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.SupplierInfo> GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyName, int ProvinceId, int CityId, int CompanyTag, string CityName);
        /// <summary>
        /// 分页获取景区列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyName">供应商名称（用于模糊查询）</param>
        /// <param name="ProvinceId">省份编号</param>
        /// <param name="CityId">城市编号</param>
        /// <param name="CompanyTag">景区主题</param>
        /// <param name="CityName">城市名称 城市名称 =""返回指定CityId下的列表</param>
        /// <param name="remark">false:null和""不读取(true:不作条件)</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.SupplierInfo> GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyName, int ProvinceId, int CityId, int CompanyTag, string CityName, bool remark);
        /// <summary>
        /// 分页获取（车队，旅游用品，购物点）列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyName">供应商名称</param>
        /// <param name="ProvinceId">省份编号</param>
        /// <param name="CityId">城市编号</param>
        /// <param name="CompanyType">公司类型</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.SupplierInfo> GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyName, int ProvinceId, int CityId, EyouSoft.Model.CompanyStructure.BusinessProperties CompanyType);
        /// <summary>
        /// 获取供应商实体
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        EyouSoft.Model.CompanyStructure.SupplierInfo GetModel(string CompanyId);
    }
}
