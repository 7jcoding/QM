using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using QM.Server.WebApi.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QM.Server.WebApi {

    class ScheduleBuilderInfoConverter : AbstractJsonConverter<BaseScheduleBuilderInfo> {
        protected override BaseScheduleBuilderInfo Create(Type objectType, JObject jObject) {
            var type = (ScheduleBuilderTypes)jObject["Type"].Value<int>();
            switch (type) {
                case ScheduleBuilderTypes.Simple:
                    return new SimpleScheduleBuilderInfo();
                case ScheduleBuilderTypes.Cron:
                    return new CronScheduleBuilderInfo();
                default:
                    throw new NotSupportedException();
            }
        }
    }

    //public class ScheduleBuilderInfoResolver : DefaultContractResolver {
    //    protected override JsonConverter ResolveContractConverter(Type objectType) {
    //        if (typeof(BaseScheduleBuilderInfo).IsAssignableFrom(objectType) && !objectType.IsAbstract)
    //            return null; // pretend TableSortRuleConvert is not specified (thus avoiding a stack overflow)
    //        return base.ResolveContractConverter(objectType);
    //    }
    //}

    //public class ScheduleBuilderInfoConverter : JsonConverter {
    //    static JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings() { ContractResolver = new ScheduleBuilderInfoResolver() };

    //    public override bool CanConvert(Type objectType) {
    //        return (objectType == typeof(BaseScheduleBuilderInfo));
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
    //        var jo = JObject.Load(reader);
    //        switch (jo["Type"].Value<int>()) {
    //            case 0: //ScheduleBuilderTypes.Simple:
    //                return JsonConvert.DeserializeObject<SimpleScheduleBuilderInfo>(jo.ToString(), SpecifiedSubclassConversion);
    //            case 1: //ScheduleBuilderTypes.Cron:
    //                return JsonConvert.DeserializeObject<CronScheduleBuilderInfo>(jo.ToString(), SpecifiedSubclassConversion);
    //            default:
    //                throw new Exception();
    //        }
    //        throw new NotImplementedException();
    //    }

    //    public override bool CanWrite {
    //        get { return false; }
    //    }

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
    //        throw new NotImplementedException(); // won't be called because CanWrite returns false
    //    }
    //}
}
