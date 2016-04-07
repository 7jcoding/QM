using QM.Server.WebApi.Entity;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace QM.Server.ApiClient.Methods {
    public class SaveTrigger : BaseMethod<TriggerSaveState> {
        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Put;
            }
        }

        public override string Model {
            get {
                return "Triggers";
            }
        }

        [Required]
        public string JobName { get; set; }

        public string JobGroup { get; set; }

        [Required]
        public string TriggerName { get; set; }

        public string TriggerGroup { get; set; }

        public int? Priority { get; set; }

        public string TriggerDesc { get; set; }

        public string Calendar { get; set; }

        public BaseScheduleBuilderInfo ScheduleBuilderInfo { get; set; }

        protected override object GetSendData() {
            return new TriggerInfo() {
                Calendar = this.Calendar,
                Desc = this.TriggerDesc,
                JobGroup = this.JobGroup,
                JobName = this.JobName,
                Priority = this.Priority ?? 5,
                TriggerGroup = this.TriggerGroup,
                TriggerName = this.TriggerName,
                ScheduleBuilderInfo = this.ScheduleBuilderInfo
            };
        }

        public SaveTrigger() {

        }
    }
}
