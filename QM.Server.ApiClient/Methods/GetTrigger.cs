using Newtonsoft.Json;
using QM.Server.ApiClient.Attributes;
using QM.Server.WebApi.Entity;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;

namespace QM.Server.ApiClient.Methods {
    public class GetTrigger : BaseMethod<TriggerInfo> {

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

        [Param]
        public string Group { get; set; }

        [Param, Required]
        public string Name { get; set; }

        private static readonly JsonSerializerSettings Setting;

        static GetTrigger() {
            Setting = new JsonSerializerSettings() {
                TypeNameHandling = TypeNameHandling.Auto
            };
            //Setting.Converters.Add(new ScheduleBuilderInfoConverter());
        }

        protected override TriggerInfo Parse(byte[] result) {
            var xml = Encoding.UTF8.GetString(result);
            return JsonConvert.DeserializeObject<TriggerInfo>(xml, Setting);
        }
    }
}
