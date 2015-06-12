using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IBLL;
using EyouSoft.Component.Factory;
using EyouSoft.IDAL;
using EyouSoft.Model;


namespace EyouSoft.BLL.ToolStructure
{
    /// <summary>
    /// 描述：公司备忘录业务
    /// </summary>
    /// 创建人：蒋胜蓝 2010-05-13
    public class CompanyDayMemo : EyouSoft.IBLL.ToolStructure.ICompanyDayMemo
    {
        IDAL.ToolStructure.ICompanyDayMemo DAL = ComponentFactory.CreateDAL<IDAL.ToolStructure.ICompanyDayMemo>();

        /// <summary>
        /// 创建公司备忘录业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static IBLL.ToolStructure.ICompanyDayMemo CreateInstance()
        {
            IBLL.ToolStructure.ICompanyDayMemo op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.ToolStructure.ICompanyDayMemo>();
            }
            return op;
        }

        /// <summary>
        /// 添加公司备忘录
        /// </summary>
        /// <param name="model">备忘录信息</param>
        /// <returns>操作结果</returns>
        public bool Add(EyouSoft.Model.ToolStructure.CompanyDayMemo model)
        {
            return DAL.Add(model) > 0 ? true : false;
        }

        /// <summary>
        /// 修改公司备忘录
        /// </summary>
        /// <param name="model">备忘录信息</param>
        /// <returns>操作结果</returns>
        public bool Update(EyouSoft.Model.ToolStructure.CompanyDayMemo model)
        {
            return DAL.Update(model) > 0 ? true : false;
        }

        /// <summary>
        /// 删除公司备忘录
        /// </summary>
        /// <param name="MemoId">备忘录编号</param>
        /// <returns>操作结果</returns>
        public bool Remove(string MemoId)
        {
            return DAL.Remove(MemoId) > 0 ? true : false; ;
        }

        /// <summary>
        /// 获取公司备忘录信息
        /// </summary>
        /// <param name="MemoId">备忘录编号</param>
        /// <returns>备忘录信息</returns>
        public EyouSoft.Model.ToolStructure.CompanyDayMemo GetModel(string MemoId)
        {
            return DAL.GetModel(MemoId);
        }
        /// <summary>
        /// 获取备忘录列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ToolStructure.CompanyDayMemo> GetList(string CompanyId)
        {
            return DAL.GetList(CompanyId);
        }
        /// <summary>
        /// 获取备忘录列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="MemoDay">日期</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ToolStructure.CompanyDayMemo> GetList(string CompanyId, DateTime MemoDay)
        {
            return DAL.GetList(CompanyId, MemoDay);
        }
        /// <summary>
        /// 获取备忘录列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="FromDay">开始日期</param>
        /// <param name="ToDay">结束日期</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ToolStructure.CompanyDayMemo> GetList(string CompanyId, DateTime FromDay, DateTime ToDay)
        {
            return DAL.GetList(CompanyId, FromDay, ToDay);
        }
    }
}
