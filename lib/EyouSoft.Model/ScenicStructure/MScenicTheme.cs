using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ScenicStructure
{
    /// <summary>
    /// 景区主题
    /// 创建者：郑付杰
    /// 创建时间：2011/10/27
    /// </summary>
    [Serializable]
    public class MScenicTheme
    {
        public MScenicTheme() { }
        /// <summary>
        /// 主题编号
        /// </summary>
        public int ThemeId { get; set; }
        /// <summary>
        /// 主题名称
        /// </summary>
        public string ThemeName { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public string IsDelete { get; set; }
    }
}
