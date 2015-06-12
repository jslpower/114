using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IBLL;
using EyouSoft.Component.Factory;
using EyouSoft.IDAL;
using EyouSoft.Model;

using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace EyouSoft.BLL
{
    /// <summary>
    /// 业层层
    /// </summary>
    public class TestBLL : ITest
    {
        IDALTest DAL = (IDALTest)ComponentFactory.CreateDAL<EyouSoft.IDAL.IDALTest>();
        /// <summary>
        /// 创建IBLL对象
        /// </summary>
        /// <returns></returns>
        public static ITest CreateInstance()
        {
            ITest op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<ITest>();
            }
            return op1;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public TestBLL()
        {}
        /// <summary>
        /// test
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int TestConn(int i)
        {
            System.Web.HttpContext.Current.Response.Write("<BR>TestBLL业务数据执行");
            EyouSoft.Cache.Facade.EyouSoftCache.Add("adasdf", "MEMCACHE LOAD", DateTime.Now.AddMinutes(1));
            return 0;
        }
        /// <summary>
        /// test
        /// </summary>
        /// <returns></returns>
        public int TestData()
        {
            //EyouSoft.Cache.Facade.CacheObject<EyouSoft.Model.CompanyStructure.Company> model = new EyouSoft.Cache.Facade.CacheObject<EyouSoft.Model.CompanyStructure.Company>();
            //EyouSoft.Model.CompanyStructure.Company model1 = new EyouSoft.Model.CompanyStructure.Company();
            //model1.CompanyName = "adff";
            //model1.ID = "111111";
            //model.Data = model1;
            //model.UpdateTime = DateTime.Now;
            //EyouSoft.Cache.Facade.EyouSoftCache.Add("asdfa11", model);
            return 1;// this.DAL.TestData();
        }
        /// <summary>
        /// test
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetCache(string key)
        {
            return EyouSoft.Cache.Facade.EyouSoftCache.GetCache(key);
        }

        public virtual object WT()
        {
            return DAL.WT();
        }
    }
}
