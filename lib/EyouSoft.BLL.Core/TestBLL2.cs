using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IBLL;
using EyouSoft.Component.Factory;
using EyouSoft.IDAL;

namespace EyouSoft.BLL.Test
{
    /// <summary>
    /// 业层层
    /// </summary>
    public class TestBLL2 : ITestBLL
    {
        IDALTest testDAL = (IDALTest)ComponentFactory.CreateDAL("Test.TestBLL2");

        public TestBLL2()
        {}

        public static ITestBLL CreateInstance()
        {
            ITestBLL op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<ITestBLL>();
            }
            return op1;
        }

        public int TestConn()
        {
            System.Web.HttpContext.Current.Response.Write("<BR>TestBLL2业务数据执行");
            return testDAL.TestConn();
        }

        public int TestData()
        {
            return testDAL.TestData();
        }

        public object GetCache(string key)
        {
            return EyouSoft.Cache.Facade.EyouSoftCache.GetCache(key);
        }

    }
}
