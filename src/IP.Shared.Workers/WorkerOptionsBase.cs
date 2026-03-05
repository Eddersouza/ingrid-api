namespace IP.Shared.Workers;

public abstract class WorkerOptionsBase
{
    public string CronSchedule { get; set; } = string.Empty;
    public bool Enabled { get; set; } = false;
    public string JobGroup { get; set; } = string.Empty;
    public Guid JobId { get; set; }
    public string JobName { get; set; } = string.Empty;
    public bool OnlyOnce { get; set; } = false;
    public int? StartAfterMinutesOfDeploy { get; set; } = null;

}