using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.TicketStructure
{
    /// <summary>
    /// 常旅客业务逻辑层
    /// Author:刘咏梅
    /// CreateTime：2010-10-29
    /// </summary>
    public class TicketVisitor : EyouSoft.IBLL.TicketStructure.ITicketVisitor
    {
        private readonly EyouSoft.IDAL.TicketStructure.ITicketVistorInfo idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TicketStructure.ITicketVistorInfo>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.TicketStructure.ITicketVisitor CreateInstance()
        {
            EyouSoft.IBLL.TicketStructure.ITicketVisitor op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.TicketStructure.ITicketVisitor>();
            }
            return op;
        }

        #region ITicketVisitor 成员
        /// <summary>
        /// 添加常旅客
        /// </summary>
        /// <param name="TicketVisitorInfo">常旅客实体</param>
        /// <returns></returns>
        public bool AddTicketVisitorInfo(EyouSoft.Model.TicketStructure.TicketVistorInfo TicketVisitorInfo)
        {
            if (TicketVisitorInfo == null) return false;
            TicketVisitorInfo.Id = Guid.NewGuid().ToString();
            TicketVisitorInfo.IssueTime = DateTime.Now;

            return AddTicketVisitorList(new List<EyouSoft.Model.TicketStructure.TicketVistorInfo>() { TicketVisitorInfo }) == 1 ? true : false;

        }
        /// <summary>
        /// 批量添加常旅客
        /// </summary>
        /// <param name="TicketVisitorList">常旅客集合</param>
        /// <returns></returns>
        public int AddTicketVisitorList(IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> TicketVisitorList)
        {
            if (TicketVisitorList == null || TicketVisitorList.Count < 1) return 0;

            int count = TicketVisitorList.Count;
            for (int i = count; i > 0; i--)
            {
                if (TicketVisitorList[i - 1] == null) { TicketVisitorList.RemoveAt(i - 1); continue; }

                TicketVisitorList[i - 1].IssueTime = DateTime.Now;
                TicketVisitorList[i - 1].Id = Guid.NewGuid().ToString();
            }

            if (TicketVisitorList == null || TicketVisitorList.Count < 1) return 0;

            return idal.AddVistorList(TicketVisitorList);
        }
        /// <summary>
        /// 修改常旅客
        /// </summary>
        /// <param name="TicketVisitorInfo">常旅客实体</param>
        /// <returns></returns>
        public bool UpdateTicketVisitorInfo(EyouSoft.Model.TicketStructure.TicketVistorInfo TicketVisitorInfo)
        {
            if (TicketVisitorInfo == null || string.IsNullOrEmpty(TicketVisitorInfo.Id)) return false;

            return idal.Update(TicketVisitorInfo) > 0 ? true : false;
        }
        /// <summary>
        /// 删除常旅客
        /// </summary>
        /// <param name="VisitorIdList">常旅客ID</param>
        /// <returns></returns>
        public bool DeleteTicketVisitorInfo(params string[] VisitorIdList)
        {
            if (VisitorIdList == null || VisitorIdList.Length < 1) return false;

            return idal.Delete(VisitorIdList);
        }
        /// <summary>
        /// 机票-查询常旅客，得到常旅客列表
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="VisitorName">常旅客姓名</param>
        /// <param name="VisitorType">常旅客类型</param>
        /// <param name="PageSize">每页显示的记录</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="RecordCount">总记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> GetTicketVisitorList(string CompanyId, string VisitorName, EyouSoft.Model.TicketStructure.TicketVistorType? VisitorType, int PageSize, int PageIndex, ref int RecordCount)
        {
            var searchInfo = new EyouSoft.Model.TicketStructure.MVisitorSearchInfo()
            {
                Type = EyouSoft.Model.TicketStructure.TicketDataType.机票常旅客,
                VType = VisitorType,
                Name = VisitorName
            };

            return _GetVisitors(PageSize, PageIndex, ref RecordCount, CompanyId, true, searchInfo);
        }
        /// <summary>
        /// 查询酒店常旅客，得到常旅客列表
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="VisitorName">常旅客姓名</param>
        /// <param name="VisitorType">常旅客类型</param>
        /// <param name="PageSize">每页显示的记录</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="RecordCount">总记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> GetHotelVistorList(string CompanyId, string VisitorName, EyouSoft.Model.TicketStructure.TicketVistorType? VisitorType, int PageSize, int PageIndex, ref int RecordCount)
        {
            var searchInfo = new EyouSoft.Model.TicketStructure.MVisitorSearchInfo()
            {
                Type = EyouSoft.Model.TicketStructure.TicketDataType.酒店常旅客,
                VType = VisitorType,
                Name = VisitorName
            };

            return _GetVisitors(PageSize, PageIndex, ref RecordCount, CompanyId, true, searchInfo);
        }
        /// <summary>
        /// 判断常旅客是否已经存在(机票)
        /// </summary>
        /// <param name="CardNo">证件号码</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="VisitorId">常旅客ID，若为新增则传空字符串</param>
        /// <returns></returns>
        public bool VisitorIsExists(string CardNo, string CompanyId, string VisitorId)
        {
            return idal.IsExist(CardNo, CompanyId, VisitorId);
        }
        /// <summary>
        /// 判断酒店常旅客是否存在(酒店)
        /// </summary>
        /// <param name="ChinaName">中文名字</param>
        /// <param name="EnglishName">英文名字</param>
        /// <param name="ContactTel">联系电话</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="VisitorId">常旅客ID，若为新增则传空字符串</param>
        /// <returns></returns>
        public bool HotelVistorIsExist(string ChinaName, string EnglishName, string ContactTel, string CompanyId, string VisitorId)
        {
            return idal.IsExist(ChinaName, EnglishName, ContactTel, CompanyId,VisitorId);
        }
        /// <summary>
        /// 机票-匹配常旅客姓名，根据姓名模糊查询常旅客集合
        /// </summary>
        /// <param name="VistorName">常旅客姓名</param>
        /// <param name="CompanyId">公司ID</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> GetListByKeyWord(string VistorName, string CompanyId)
        {
            var searchInfo = new EyouSoft.Model.TicketStructure.MVisitorSearchInfo()
            {
                Type = EyouSoft.Model.TicketStructure.TicketDataType.机票常旅客,
                Name = VistorName
            };

            int recordCount = 0;

            return _GetVisitors(0, 0, ref recordCount, CompanyId, false, searchInfo);
        }
        /// <summary>
        /// 酒店-匹配常旅客姓名，根据姓名模糊查询常旅客集合
        /// </summary>
        /// <param name="VistorName">常旅客姓名</param>
        /// <param name="CompanyId">公司ID</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> GetHotelListByName(string VistorName, string CompanyId)
        {
            var searchInfo = new EyouSoft.Model.TicketStructure.MVisitorSearchInfo()
            {
                Type = EyouSoft.Model.TicketStructure.TicketDataType.酒店常旅客,
                Name = VistorName
            };

            int recordCount = 0;

            return _GetVisitors(0, 0, ref recordCount, CompanyId, false, searchInfo);
        }
        /// <summary>
        /// 获取常旅客实体
        /// </summary>
        /// <param name="VisitorId">常旅客ID</param>
        /// <returns></returns>
        public EyouSoft.Model.TicketStructure.TicketVistorInfo GetTicketVisitorInfo(string VisitorId)
        {
            if (string.IsNullOrEmpty(VisitorId)) return null;

            return idal.GetModel(VisitorId);
        }

        /// <summary>
        /// 获取常旅客信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> GetVisitors(int pageSize, int pageIndex, ref int recordCount, string companyId, EyouSoft.Model.TicketStructure.MVisitorSearchInfo searchInfo)
        {
            return _GetVisitors(pageSize, pageIndex, ref recordCount, companyId, true, searchInfo);
        }

        /// <summary>
        /// 获取常旅客信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> GetVisitors(string companyId, EyouSoft.Model.TicketStructure.MVisitorSearchInfo searchInfo)
        {
            int recordCount = 0;
            return _GetVisitors(0, 0, ref recordCount, companyId, false, searchInfo);
        }
        #endregion

        #region private members
        /// <summary>
        /// 获取常旅客信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="isPaging">是否分页</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> _GetVisitors(int pageSize, int pageIndex, ref int recordCount, string companyId, bool isPaging, EyouSoft.Model.TicketStructure.MVisitorSearchInfo searchInfo)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return idal.GetVisitors(pageSize, pageIndex, ref recordCount, companyId, isPaging, searchInfo);
        }
        #endregion
    }
}
