using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Simpl;
using Quartz.Spi;

namespace QuartzNetExample;

public class MyJobFactory : SimpleJobFactory 
{
    IServiceProvider _provider;

    public MyJobFactory(IServiceProvider serviceProvider)
    {
        _provider = serviceProvider;
    }


    public override IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        try
        {

            // this will inject dependencies that the job requires
            return (IJob)_provider.GetService(bundle.JobDetail.JobType);
        }
        catch (Exception e)
        {
            throw new SchedulerException(string.Format("Problem while instantiating job '{0}' from the Aspnet Core IOC.", bundle.JobDetail.Key), e);
        }

    }
}
