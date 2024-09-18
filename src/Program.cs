// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using QuartzNetExample;


IServiceCollection services;
IServiceProvider provider;
//Qurtz
ISchedulerFactory schedulerFactory;
IScheduler _scheduler;

Console.WriteLine("Output from listners in debug console.");

services = new ServiceCollection();
schedulerFactory = new StdSchedulerFactory();

_scheduler = await schedulerFactory.GetScheduler();
//Register services
services.AddTransient<SimpleJob>();
services.AddSingleton<IEmailService, EmailService>();

provider = services.BuildServiceProvider();

//Setup job
_scheduler.JobFactory = new MyJobFactory(provider);
await _scheduler.Start();

_scheduler.ListenerManager.AddTriggerListener(new TriggerListener());
_scheduler.ListenerManager.AddJobListener(new JobListener());
_scheduler.ListenerManager.AddSchedulerListener(new SchedulerListener());

IJobDetail job = JobBuilder.Create<SimpleJob>()
                            .UsingJobData("username", "devhow")
                            .UsingJobData("password", "Security!!")
                            .WithIdentity("simplejob", "quartzexamples")
                            .StoreDurably()
                            .RequestRecovery()
                            .Build();

job.JobDataMap.Put("user", new JobUserParameter { Username = "devhow", Password = "Security!!" });

ITrigger trigger = TriggerBuilder.Create()
                                 .WithIdentity("testtrigger", "quartzexamples")
                                 .StartNow()
                                 .WithSimpleSchedule(z => z.WithIntervalInSeconds(5).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires())
                                 .Build();


await _scheduler.ScheduleJob(job, trigger);



//Run until the user presses a key
Console.WriteLine("Press key to terminate the job.");
Console.ReadKey();