using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.BackgroundServices
{
    public interface IAdvServices
    {
        /// <summary>
        /// 获取广告位置
        /// </summary>
        /// <returns></returns>
        IList<string> GetPostion();
        /// <summary>
        /// 同步数据
        /// </summary>
        void RunUpdate();
    }
}
