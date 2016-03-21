using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace QM.Server.ApiClient {
    /// <summary>
    /// 枚举扩展方法类
    /// </summary>
    public static class EnumHelper {


        /// <summary>
        /// 获取 DescriptionAttribute 中的 Description
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TA"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TA GetAttribute<T, TA>(T value)
            where T : struct, IComparable, IConvertible, IFormattable
            where TA : Attribute {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException("不是一个有效的枚举类型");

            var field = type.GetField(value.ToString());

            return field.GetCustomAttributes(false).OfType<TA>().FirstOrDefault();
        }
    }
}