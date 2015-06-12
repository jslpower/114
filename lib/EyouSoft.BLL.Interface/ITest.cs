using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.AOPHandler;

namespace EyouSoft.IBLL
{
    /// <summary>
    /// 业层接口层

    /// </summary>
    public interface ITest
    {
        /// <summary>
        /// 方法A
        /// </summary>
        /// <returns></returns>  
        [MyLogHandler(MessageTemplate = "TestBLL.TestConn", Order = 1)]
        [MyHandler(MessageTemplate = "TestBLL.TestConn", Order = 2)]        
        int TestConn(int i);
        /// <summary>
        /// 方法B
        /// </summary>
        /// <returns></returns>
        int TestData();
        /// <summary>
        /// 方法C
        /// </summary>
        /// <returns></returns>
        object GetCache(string key);

        object WT();
    }

    /// <summary>
    /// 业层接口层

    /// </summary>
    public interface ITestBLL
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
        /// 方法C
        /// </summary>
        /// <returns></returns>
        object GetCache(string key);
    }
}
