using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;


namespace EyouSoft.BLL.SMSStructure
{
    /// <summary>
    /// 短信中心-常用短语及常用短语类型业务逻辑类
    /// </summary>
    /// Author:汪奇志 201-06-10
    public class Template:EyouSoft.IBLL.SMSStructure.ITemplate
    {
        private readonly IDAL.SMSStructure.ITemplate dal = ComponentFactory.CreateDAL<EyouSoft.IDAL.SMSStructure.ITemplate>();

        #region CreateInstance
        /// <summary>
        /// 创建短信中心-常用短语及常用短语类型业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.SMSStructure.ITemplate CreateInstance()
        {
            EyouSoft.IBLL.SMSStructure.ITemplate op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.SMSStructure.ITemplate>();
            }
            return op1;
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 插入常用短语类型信息,返回0插入失败,>0时为插入的类型编号
        /// </summary>
        /// <param name="categoryInfo">常用短语类型业务实体</param>
        /// <returns></returns>
        public int InsertCategory(EyouSoft.Model.SMSStructure.TemplateCategoryInfo categoryInfo)
        {
            return dal.InsertCategory(categoryInfo);
        }

        /// <summary>
        /// 删除常用短语类型信息
        /// <param name="CategoryId">类型编号</param>
        /// </summary>
        /// <returns></returns>
        public bool DeleteCategory(int CategoryId)
        {
            return dal.DeleteCategory(CategoryId);
        }

        /// <summary>
        /// 根据指定的公司编号获取公司的所有常用短语类型信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SMSStructure.TemplateCategoryInfo> GetCategorys(string companyId)
        {
            return dal.GetCategorys(companyId);
        }

        /// <summary>
        /// 插入常用短语
        /// </summary>
        /// <param name="templateInfo">常用短语业务实体</param>
        /// <returns></returns>
        public bool InsertTemplate(EyouSoft.Model.SMSStructure.TemplateInfo templateInfo)
        {
            templateInfo.TemplateId = Guid.NewGuid().ToString();
            return dal.InsertTemplate(templateInfo);
        }

        /// <summary>
        /// 根据指定条件获取常用短语信息
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="keyword">关键字 为空时不做为查询条件</param>
        /// <param name="categoryId">类型编号 -1时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SMSStructure.TemplateInfo> GetTemplates(int pageSize, int pageIndex, ref int recordCount, string companyId, string keyword, int categoryId)
        {
            return dal.GetTemplates(pageSize, pageIndex, ref recordCount, companyId, keyword, categoryId);
        }

        /// <summary>
        /// 删除常用短语
        /// </summary>
        /// <param name="templateId">常用短语编号</param>
        /// <returns></returns>
        public bool DeleteTemplate(string templateId)
        {
            return dal.DeleteTemplate(templateId);
        }

        /// <summary>
        /// 获取常用短语信息
        /// </summary>
        /// <param name="templateId">常用短语编号</param>
        /// <returns></returns>
        public EyouSoft.Model.SMSStructure.TemplateInfo GetTemplateInfo(string templateId)
        {
            return dal.GetTemplateInfo(templateId);
        }

        /// <summary>
        /// 更新常用短语
        /// </summary>
        /// <param name="templateInfo">常用短语业务实体</param>
        /// <returns></returns>
        public bool UpdateTemplate(EyouSoft.Model.SMSStructure.TemplateInfo templateInfo)
        {
            return dal.UpdateTemplate(templateInfo);
        }
        #endregion
    }
}
