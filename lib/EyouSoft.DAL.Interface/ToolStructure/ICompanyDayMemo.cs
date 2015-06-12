using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.ToolStructure
{
    /// <summary>
    /// 描述：公司备忘录数据接口
    /// </summary>
    /// 创建人：蒋胜蓝 2010-05-12
    public interface ICompanyDayMemo
    {
        /// <summary>
        /// 添加公司备忘录
        /// </summary>
        /// <param name="model">备忘录信息</param>
        /// <returns>操作结果</returns>
        int Add(EyouSoft.Model.ToolStructure.CompanyDayMemo model);

        /// <summary>
        /// 修改公司备忘录
        /// </summary>
        /// <param name="model">备忘录信息</param>
        /// <returns>操作结果</returns>
        int Update(EyouSoft.Model.ToolStructure.CompanyDayMemo model);

        /// <summary>
        /// 删除公司备忘录
        /// </summary>
        /// <param name="MemoId">备忘录编号</param>
        /// <returns>操作结果</returns>
        int Remove(string MemoId);

        /// <summary>
        /// 获取公司备忘录信息
        /// </summary>
        /// <param name="MemoId">备忘录编号</param>
        /// <returns>备忘录信息</returns>
        EyouSoft.Model.ToolStructure.CompanyDayMemo GetModel(string MemoId);
        /// <summary>
        /// 获取备忘录列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.ToolStructure.CompanyDayMemo> GetList(string CompanyId);
        /// <summary>
        /// 获取备忘录列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="MemoDay">日期</param>
        /// <returns></returns>
        IList<EyouSoft.Model.ToolStructure.CompanyDayMemo> GetList(string CompanyId, DateTime MemoDay);
        /// <summary>
        /// 获取备忘录列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="FromDay">开始日期</param>
        /// <param name="ToDay">结束日期</param>
        /// <returns></returns>
        IList<EyouSoft.Model.ToolStructure.CompanyDayMemo> GetList(string CompanyId, DateTime FromDay, DateTime ToDay);
    }
}
