using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IBLL;
using EyouSoft.Component.Factory;
using EyouSoft.IBLL.SystemStructure;


namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-31
    /// 描述：后台登陆页广告业务逻辑层
    /// </summary>
    public class CompanyAdv:ICompanyAdv
    {
        private readonly IDAL.SystemStructure.ICompanyAdv dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.ICompanyAdv>();

        #region Cache别名定义
        /// <summary>
        /// 分页广告
        /// </summary>
        const string CacheGetList = "{false}_getlist";
        /// <summary>
        /// 指定条数的广告
        /// </summary>
        const string CacheGetTopNumberList = "{false}_gettopnumberlist";
        /// <summary>
        /// 首页图片广告
        /// </summary>
        const string CacheIndexPicAdv = "{false}_indexpicadv";
        #endregion

        #region CreateInstance
        /// <summary>
        /// 创建后台登陆页广告业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.SystemStructure.ICompanyAdv CreateInstance()
        {
            EyouSoft.IBLL.SystemStructure.ICompanyAdv op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.SystemStructure.ICompanyAdv>();
            }
            return op1;
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 运营后台分页获取列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyType">公司类型</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.CompanyAdv> GetList(int pageSize, int pageIndex, ref int recordCount,EyouSoft.Model.CompanyStructure.CompanyType companyType)
        {
            IList<EyouSoft.Model.SystemStructure.CompanyAdv> list = null;
            list = (IList<EyouSoft.Model.SystemStructure.CompanyAdv>)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.System.CategoryAdv + string.Format(CacheGetList, (int)companyType));
            if (list == null)
            { 
                list=dal.GetList(pageSize,pageIndex,ref recordCount,companyType);
                if (list != null && list.Count > 0)
                    EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.System.CategoryAdv + string.Format(CacheGetList, (int)companyType), list);
            }
            return list;
        }
        /// <summary>
        /// 获取指定条数的后台登陆广告
        /// </summary>
        /// <param name="topNumber">要返回的记录数 =false时返回全部 >false时返回指定条数的记录</param>
        /// <param name="companyType">公司类型</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.CompanyAdv> GetTopNumList(int topNumber,EyouSoft.Model.CompanyStructure.CompanyType companyType)
        {
            IList<EyouSoft.Model.SystemStructure.CompanyAdv> list = null;
            list = (IList<EyouSoft.Model.SystemStructure.CompanyAdv>)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.System.CategoryAdv + string.Format(CacheGetTopNumberList, (int)companyType));
            if (list == null)
            { 
                list=dal.GetTopNumList(topNumber,companyType);
                if (list != null && list.Count > 0)
                    EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.System.CategoryAdv + string.Format(CacheGetTopNumberList, (int)companyType), list);
            }
            return list;
        }
        /// <summary>
        /// 获取指定公司类型的后台首页图片广告
        /// </summary>
        /// <param name="companyType">公司类型</param>
        /// <returns></returns>
        public EyouSoft.Model.SystemStructure.CompanyAdv GetIndexPicAdv(EyouSoft.Model.CompanyStructure.CompanyType companyType)
        {
            EyouSoft.Model.SystemStructure.CompanyAdv model=null;
            model = (EyouSoft.Model.SystemStructure.CompanyAdv)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.System.CategoryAdv + string.Format(CacheIndexPicAdv, (int)companyType));
            if (model == null)
            {
                model=dal.GetIndexPicAdv(companyType);
                if(model!=null)
                    EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.System.CategoryAdv + string.Format(CacheIndexPicAdv, (int)companyType), model);
            }
            return model;
        }
        /// <summary>
        /// 添加文字广告
        /// </summary>
        /// <returns>false:失败 true:成功</returns>
        public bool Add(EyouSoft.Model.SystemStructure.CompanyAdv model)
        {
            if (model == null)
                return false;
            bool falg= dal.Add(model);
            if (falg)
            {
                //清除缓存
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.CategoryAdv + string.Format(CacheGetList, (int)model.AdvCompanyType));
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.CategoryAdv + string.Format(CacheGetTopNumberList, (int)model.AdvCompanyType));
            }
            return falg;
        }
        /// <summary>
        /// 添加图片广告
        /// </summary>
        /// <param name="companyType">公司类型</param>
        /// <param name="ImgPath">图片路径</param>
        /// <param name="AdvLink">链接地址</param>
        /// <returns>false:失败 true:成功</returns>
        public bool AddPicAdv(EyouSoft.Model.CompanyStructure.CompanyType companyType, string ImgPath, string AdvLink)
        {
            bool flag= dal.AddPicAdv(companyType, ImgPath, AdvLink);
            if (flag)
            {
                //清除缓存
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.CategoryAdv + string.Format(CacheGetList, (int)companyType));
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.CategoryAdv + string.Format(CacheIndexPicAdv, (int)companyType));
            }
            return flag;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns>false:失败 true:成功</returns>
        public bool Update(EyouSoft.Model.SystemStructure.CompanyAdv model)
        {
            if (model == null)
                return false;
            bool flag= dal.Update(model);
            if (flag)
            {
                //清除缓存
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.CategoryAdv + string.Format(CacheGetList, (int)model.AdvCompanyType));
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.CategoryAdv + string.Format(CacheGetTopNumberList, (int)model.AdvCompanyType));
            }
            return flag;
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
