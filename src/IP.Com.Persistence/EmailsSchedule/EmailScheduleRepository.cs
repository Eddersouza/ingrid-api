namespace IP.Com.Persistence.EmailsSchedule;

public interface IEmailScheduleRepository :
    ICreationRepository<EmailSchedule>,
    IQueryableRepository<EmailSchedule>,
    IUpdatableRepository<EmailSchedule>;

internal class EmailScheduleRepository(ComDbContext appContext) :
    RepositoryBase<EmailSchedule>(appContext),
    IEmailScheduleRepository;