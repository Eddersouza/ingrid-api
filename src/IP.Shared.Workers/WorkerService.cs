using Quartz;

namespace IP.Shared.Workers;

public class WorkerService
{
    public static void Configure<T>(
           QuartzOptions quartzOptions,
           WorkerOptionsBase workerOptions) where T : IJob
    {
        JobKey jobKey = JobKey.Create(workerOptions.JobName, workerOptions.JobGroup);
        quartzOptions
            .AddJob<T>(jobBuilder =>
                jobBuilder.WithIdentity(jobKey));

        bool onlyonce = workerOptions.OnlyOnce;
        bool startAfterDeploy = workerOptions.StartAfterMinutesOfDeploy != null;
        int startAfterMinutesOfDeploy = workerOptions.StartAfterMinutesOfDeploy ?? 0;

        if (onlyonce)
            quartzOptions
                .AddTrigger(trigger => trigger
                    .ForJob(jobKey)
                    .WithIdentity($"trigger-only-once-{workerOptions.JobName}-{workerOptions.JobGroup}")
                    .StartNow()
                    .Build());

        if (!onlyonce)
            quartzOptions
                .AddTrigger(trigger => trigger
                    .ForJob(jobKey)
                    .WithIdentity($"trigger-{workerOptions.JobName}-{workerOptions.JobGroup}")
                    .WithCronSchedule(workerOptions.CronSchedule)
                    .Build());

        if (!onlyonce && startAfterDeploy)
            quartzOptions
                .AddTrigger(trigger => trigger
                    .ForJob(jobKey)
                    .WithIdentity($"trigger-after-{startAfterMinutesOfDeploy}-{workerOptions.JobName}-{workerOptions.JobGroup}")
                    .StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.Now.AddMinutes(startAfterMinutesOfDeploy)))
                    .Build());
    }

    public static async Task StartNow<T>(
        ISchedulerFactory schedulerFactory,
        WorkerOptionsBase workerOptions) where T : IJob
    {
        IScheduler scheduler = await schedulerFactory.GetScheduler();

        JobKey jobKey = JobKey.Create($"{workerOptions.JobName}-{DateTime.Now.Ticks}", workerOptions.JobGroup);

        // define the job and tie it to our HelloJob class
        var job = JobBuilder.Create<T>()
            .WithIdentity(jobKey)
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity($"trigger-{workerOptions.JobName}-{workerOptions.JobGroup}-{DateTime.Now.Ticks}")
            .StartNow()
            .Build();

        await scheduler.ScheduleJob(job, trigger);
    }
}