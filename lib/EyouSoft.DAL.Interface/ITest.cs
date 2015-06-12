using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL
{
    /// <summary>
    /// 数据接口层

    /// </summary>
    public interface IDALTest
    {
        /// <summary>
        /// 方法A
        /// </summary>
        /// <returns></returns>
        int TestConn();
        /// <summary>
        /// 方法B
        /// </summary>
        /// <returns></returns>
        int TestData();

        /// <summary>
        /// WQZ TEST
        /// </summary>
        /// <returns></returns>
        object WT();
    }
}
