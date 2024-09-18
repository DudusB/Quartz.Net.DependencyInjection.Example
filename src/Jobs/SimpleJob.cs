using Quartz;

namespace QuartzNetExample;

public class SimpleJob : IJob
{
    IEmailService _emailService;
    public SimpleJob(IEmailService emailService)
    {
        try
        {
            _emailService = emailService;

        }
        catch (Exception e)
        {

            throw new InvalidOperationException("Error constructing job.", e);
        }
    }
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            _emailService.Send("info@devhow.net", "Quartz.net DI", "Dependency injection in quartz");

        }
        catch (Exception e)
        {

            throw new JobExecutionException($"Error executing task.",e );
        }
    }
}
