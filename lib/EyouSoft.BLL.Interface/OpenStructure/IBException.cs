using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.OpenStructure
{
    /// <summary>
    /// 同步各平台数据异常信息业务逻辑接口
    /// </summary>
    /// 周文超 2011-04-02
    public interface IBException
    {
        /// <summary>
        /// 添加异常信息
        /// </summary>
        /// <param name="model">异常信息实体</param>
        /// <returns>返回1成功，其他失败</returns>
        int AddMException(Model.OpenStructure.MExceptionInfo model);

        /// <summary>
        /// 添加异常信息
        /// </summary>
        /// <param name="list">异常信息实体集合</param>
        /// <returns>返回1成功，其他失败</returns>
        int AddMException(IList<Model.OpenStructure.MExceptionInfo> list);

        //Model.OpenStructure.MExceptionInfo GetMException(int ExceptionId);

        //IList<Model.OpenStructure.MExceptionInfo> GetMExceptionList();
    }
}
