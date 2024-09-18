using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzNetExample;

public interface IEmailService
{
    void Send(string receiver, string subject, string body);
}
public class EmailService : IEmailService
{
    public void Send(string receiver, string subject, string body)
    {
        Console.WriteLine($"{DateTime.Now.ToString("hh-mm-ss")} Sending email to {receiver} with subject {subject} and body {body}");
    }
}
