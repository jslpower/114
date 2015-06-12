using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace EyouSoft.Common
{
    public class EnumObj
    {
        public EnumObj()
        {
        }

        public EnumObj(string text, string value)
        {
            this.Text = text;
            this.Value = value;
        }
        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        public static List<EnumObj> GetList(Type type)
        {
            if (type.IsEnum != true)
            {    //不是枚举的要报错
                throw new InvalidOperationException();
            }

            //建立列表
            List<EnumObj> list = new List<EnumObj>();

            //获得特性Description的类型信息
            //Type typeDescription = typeof(DescriptionAttribute);

            //获得枚举的字段信息（因为枚举的值实际上是一个static的字段的值）
            System.Reflection.FieldInfo[] fields = type.GetFields();

            //检索所有字段
            foreach (FieldInfo field in fields)
            {
                //过滤掉一个不是枚举值的，记录的是枚举的源类型
                if (field.FieldType.IsEnum == true)
                {
                    EnumObj obj = new EnumObj();

                    // 通过字段的名字得到枚举的值
                    // 枚举的值如果是long的话，ToChar会有问题，但这个不在本文的讨论范围之内
                    obj.Value = ((int)type.InvokeMember(field.Name, BindingFlags.GetField, null, null, null)).ToString();
                    obj.Text = field.Name;
                    ////获得这个字段的所有自定义特性，这里只查找Description特性
                    //object[] arr = field.GetCustomAttributes(typeDescription, true);
                    //if (arr.Length > 0)
                    //{
                    //    //因为Description这个自定义特性是不允许重复的，所以我们只取第一个就可以了！
                    //    DescriptionAttribute aa = (DescriptionAttribute)arr[0];
                    //    //获得特性的描述值，也就是‘男’‘女’等中文描述
                    //    dr["Text"] = aa.Description;
                    //}
                    //else
                    //{
                    //    //如果没有特性描述（-_-!忘记写了吧~）那么就显示英文的字段名
                    //    dr["Text"] = field.Name;
                    //}
                    list.Add(obj);
                }
            }

            return list;
        }
    }
}
