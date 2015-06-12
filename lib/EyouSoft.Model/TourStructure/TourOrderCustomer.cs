using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TourStructure
{
    /// <summary>
    /// 订单游客信息
    /// </summary>
    /// 周文超 2010-05-11
    [Serializable]
    public class TourOrderCustomer
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TourOrderCustomer()
        { }

        #region Model

        private string _id;
        private string _companyid;
        private string _orderid;
        private string _visitorname;
        private CradType _cradtype;
        private string _cradnumber;
        private bool _sex;
        private bool _visitortype;
        private string _contacttel;
        private string _remark;
        private DateTime _issuetime;
        private string _siteno;

        /// <summary>
        /// 主键ID
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 所属公司ID
        /// </summary>
        public string CompanyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 所属订单号
        /// </summary>
        public string OrderID
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 游客姓名
        /// </summary>
        public string VisitorName
        {
            set { _visitorname = value; }
            get { return _visitorname; }
        }
        /// <summary>
        /// 证件类型
        /// </summary>
        public CradType CradType
        {
            set { _cradtype = value; }
            get { return _cradtype; }
        }
        /// <summary>
        /// 证件编号
        /// </summary>
        public string CradNumber
        {
            set { _cradnumber = value; }
            get { return _cradnumber; }
        }
        /// <summary>
        /// 游客性别 true or 1为男,false or 0为女
        /// </summary>
        public bool Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 游客类型 0:儿童 1:成人 默认1
        /// </summary>
        public bool VisitorType
        {
            set { _visitortype = value; }
            get { return _visitortype; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTel
        {
            set { _contacttel = value; }
            get { return _contacttel; }
        }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime
        {
            set { _issuetime = value; }
            get { return _issuetime; }
        }
        /// <summary>
        /// 座位号(长度最大为3)
        /// </summary>
        public string SiteNo
        {
            set { _siteno = value; }
            get { return _siteno; }
        }

        #region 扩展属性

        private string _tourid;
        private string _tourno;
        private string _routename;
        private string _companyname;

        /// <summary>
        /// 团队ID
        /// </summary>
        public string TourId
        {
            set { _tourid = value; }
            get { return _tourid; }
        }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourNo
        {
            set { _tourno = value; }
            get { return _tourno; }
        }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName
        {
            set { _routename = value; }
            get { return _routename; }
        }
        /// <summary>
        /// 所属公司名称
        /// </summary>
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }

        #endregion

        #endregion Model
    }

    /// <summary>
    /// 证件类型枚举
    /// </summary>
    public enum CradType
    {
        /// <summary>
        /// 请选择证件
        /// </summary>
        请选择证件 = 0,
        /// <summary>
        /// 身份证
        /// </summary>
        身份证,
        /// <summary>
        /// 户口本
        /// </summary>
        户口本,
        /// <summary>
        /// 军官证
        /// </summary>
        军官证,
        /// <summary>
        /// 护照
        /// </summary>
        护照,
        /// <summary>
        /// 边境通行证
        /// </summary>
        边境通行证,
        /// <summary>
        /// 其他
        /// </summary>
        其他

    }
}
