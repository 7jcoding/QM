using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace QM.Common {
    public static class DatamapParser {
        public static readonly Dictionary<Type, Func<JobDataMap, string, object>> SupportTypes = new Dictionary<Type, Func<JobDataMap, string, object>>() {
            {typeof(bool), (datamap, key)=>{
                return datamap.GetBooleanValue(key);
            }},
            {typeof(char), (datamap, key)=>{
                return datamap.GetChar(key);
            }},
            {typeof(DateTime), (datamap, key)=>{
                return datamap.GetDateTimeValue(key);
            }},
            {typeof(DateTimeOffset), (datamap, key)=>{
                return datamap.GetDateTimeOffsetValue(key);
            }},
            {typeof(double), (datamap, key)=>{
                return datamap.GetDoubleValue(key);
            }},
            {typeof(Single), (datamap, key)=>{
                return datamap.GetFloatValue(key);
            }},
            {typeof(int), (datamap, key)=>{
                return datamap.GetInt(key);
            }},
            {typeof(long), (datamap, key)=>{
                return datamap.GetLongValue(key);
            }},
            {typeof(TimeSpan), (datamap, key)=>{
                return datamap.GetTimeSpanValue(key);
            }},
            {typeof(decimal), (datamap, key)=>{
                return datamap.GetDecimal(key);
            }},
            {typeof(string), (datamap, key)=>{
                return datamap.GetString(key);
            }}
        };

        public static T Parse<T>(this JobDataMap datamap) where T : class , new() {
            var instance = (T)Activator.CreateInstance(typeof(T));
            var ps = GetSupportProperties<T>();
            foreach (var p in ps) {
                var pt = p.Key.PropertyType;
                if (pt.IsGenericType && pt.GetGenericTypeDefinition().Equals(typeof(Nullable<>))) {
                    pt = new NullableConverter(pt).UnderlyingType;
                }
                var func = SupportTypes[pt];
                object value = null;
                try {
                    value = func(datamap, p.Key.Name);
                } catch {

                }

                if (value != null)
                    p.Key.SetValue(instance, value);
            }

            return instance;
        }

        public static decimal GetDecimal(this JobDataMap datamap, string key) {
            object obj = datamap.Get(key);

            if (obj is string) {
                return Decimal.Parse((string)obj);
            } else
                return (decimal)obj;
        }


        public static Dictionary<PropertyInfo, object> GetSupportProperties(Type type) {
            var instance = Activator.CreateInstance(type);
            var ps = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            return ps.Where(p => SupportTypes.ContainsKey(p.PropertyType) ||
                (p.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) &&
                SupportTypes.ContainsKey(new NullableConverter(p.PropertyType).UnderlyingType))
                ).ToDictionary(p => p, p => p.GetValue(instance));
        }

        public static Dictionary<PropertyInfo, object> GetSupportProperties<T>() where T : class, new() {
            var type = typeof(T);
            return GetSupportProperties(type);
        }
    }
}
