using Quartz;

namespace JobAReferences {
    public abstract class ThirdPirtyBaseJob : IJob {
        public abstract void Execute(IJobExecutionContext context);
    }
}
