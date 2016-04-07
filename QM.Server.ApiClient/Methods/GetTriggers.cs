using Newtonsoft.Json;
using QM.Server.WebApi.Entity;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace QM.Server.ApiClient.Methods {
    public class GetTriggers : BaseMethod<IEnumerable<TriggerInfo>> {
        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Get;
            }
        }

        public override string Model {
            get {
                return "Triggers";
            }
        }

        private static readonly JsonSerializerSettings Setting;

        static GetTriggers() {
            Setting = new JsonSerializerSettings() {
                TypeNameHandling = TypeNameHandling.Auto
            };
            //Setting.Converters.Add(new ScheduleBuilderInfoConverter());
        }

        protected override IEnumerable<TriggerInfo> Parse(byte[] result) {
            var xml = Encoding.UTF8.GetString(result);
            return JsonConvert.DeserializeObject<IEnumerable<TriggerInfo>>(xml, Setting);
        }
    }
}
