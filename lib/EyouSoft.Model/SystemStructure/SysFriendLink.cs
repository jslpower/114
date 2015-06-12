using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 链接类型
    /// </summary>
    public enum FriendLinkType
    {
        /// <summary>
        /// 未知
        /// </summary>
        未知,
        /// <summary>
        /// 文字链接
        /// </summary>
        文字,
        /// <summary>
        /// 图片链接
        /// </summary>
        图片,
        /// <summary>
        /// 战略合作伙伴
        /// </summary>
        战略合作,
        /// <summary>
        /// 资讯首页
        /// </summary>
        资讯首页
    }
    /// <summary>
    /// 首页友情链接
    /// </summary>
    /// 周文超 2010-05-12
    [Serializable]
    public class SysFriendLink
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysFriendLink()
        { }

        #region Model

        private int _id;
        private FriendLinkType _linktype;
        private string _linkname;
        private string _linkaddress;
        private string _imgpath;
        private bool _ischecked;
        private DateTime _issuetime;

        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 链接类型
        /// </summary>
        public FriendLinkType LinkType
        {
            set { _linktype = value; }
            get { return _linktype; }
        }
        /// <summary>
        /// 链接名称
        /// </summary>
        public string LinkName
        {
            set { _linkname = value; }
            get { return _linkname; }
        }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string LinkAddress
        {
            set { _linkaddress = value; }
            get { return _linkaddress; }
        }
        /// <summary>
        /// 广告图片路径
        /// </summary>
        public string ImgPath
        {
            set { _imgpath = value; }
            get { return _imgpath; }
        }
        /// <summary>
        /// 是否审核
        /// </summary>
        public bool IsChecked
        {
            set { _ischecked = value; }
            get { return _ischecked; }
        }
        /// <summary>
        /// 创建时间(默认为getdate())
        /// </summary>
        public DateTime IssueTime
        {
            set { _issuetime = value; }
            get { return _issuetime; }
        }

        #endregion Model
    }
}
