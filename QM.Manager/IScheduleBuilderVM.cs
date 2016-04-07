using QM.Server.WebApi.Entity;

namespace QM.Manager {
    public interface IScheduleBuilderVM {
        ScheduleBuilderTypes Type {
            get;
        }

        BaseScheduleBuilderInfo Info { get; }

        void Restore(BaseScheduleBuilderInfo info);
    }
}
