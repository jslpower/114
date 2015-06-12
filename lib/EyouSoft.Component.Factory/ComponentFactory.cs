using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Common;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.InterceptionExtension;
namespace EyouSoft.Component.Factory
{
    /// <summary>
    /// 对象创建工厂
    /// </summary>
    public class ComponentFactory
    {

        /// <summary>
        /// 业务层配置信息
        /// </summary>
        private static readonly string BLL_CORE_PATH = "";

        /// <summary>
        /// 数据层配置信息
        /// </summary>
        private static readonly string DAL_CORE_PATH = "";

        /// <summary>
        /// 创建业层对象(建立AOP)
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <returns></returns>
        public static TInterface Create<TInterface>()
        {
            IUnityContainer container = GetConatiner();
            return container.Resolve<TInterface>();
        }

        /// <summary>
        /// 创建业务层对象
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <returns></returns>
        public static TInterface CreateBLL<TInterface>()
        {
            IUnityContainer container = GetConatiner();
            return container.Resolve<TInterface>();
        }

        /// <summary>
        /// 创建数据层对象
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <returns></returns>
        public static TInterface CreateDAL<TInterface>()
        {
            IUnityContainer container = GetConatiner();
            return container.Resolve<TInterface>();
        }

        /// <summary>
        /// 创建业务层对象
        /// </summary>
        /// <param name="MethodName">耦合方法名称(项目内命名空间+类名+方法名)</param>
        /// <returns></returns>
        public static object CreateBLL(string MethodName)
        {
            return CreateInstanceObject(MethodName, BLL_CORE_PATH);
        }

        /// <summary>
        /// 创建数据层对象
        /// </summary>
        /// <param name="MethodName">耦合方法名称(项目内命名空间+类名+方法名)</param>
        /// <returns></returns>
        public static object CreateDAL(string MethodName)
        {
            return CreateInstanceObject(MethodName, DAL_CORE_PATH);
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="MethodName"></param>
        /// <param name="CORE_PATH"></param>
        /// <returns></returns>
        private static object CreateInstanceObject(string MethodName, string CORE_PATH)
        {
            string InstancePath = CORE_PATH + "." + MethodName;
            return DataAccess.CreateObject(CORE_PATH, InstancePath);
        }

        public static IUnityContainer GetConatiner()
        {
            return (IUnityContainer)System.Web.HttpContext.Current.Application["container"];
        }
    }
}
