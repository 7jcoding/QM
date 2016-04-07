namespace QM.Server.WebApi.Entity {
    public enum TriggerState {
        //
        // 摘要:
        //     Indicates that the Quartz.ITrigger is in the "normal" state.
        Normal = 0,
        //
        // 摘要:
        //     Indicates that the Quartz.ITrigger is in the "paused" state.
        Paused = 1,
        //
        // 摘要:
        //     Indicates that the Quartz.ITrigger is in the "complete" state.
        //
        // 备注:
        //     "Complete" indicates that the trigger has not remaining fire-times in its schedule.
        Complete = 2,
        //
        // 摘要:
        //     Indicates that the Quartz.ITrigger is in the "error" state.
        //
        // 备注:
        //     A Quartz.ITrigger arrives at the error state when the scheduler attempts to fire
        //     it, but cannot due to an error creating and executing its related job. Often
        //     this is due to the Quartz.IJob's class not existing in the classpath.
        //     When the trigger is in the error state, the scheduler will make no attempts to
        //     fire it.
        Error = 3,
        //
        // 摘要:
        //     Indicates that the Quartz.ITrigger is in the "blocked" state.
        //
        // 备注:
        //     A Quartz.ITrigger arrives at the blocked state when the job that it is associated
        //     with has a Quartz.DisallowConcurrentExecutionAttribute and it is currently executing.
        Blocked = 4,
        //
        // 摘要:
        //     Indicates that the Quartz.ITrigger does not exist.
        None = 5
    }
}
