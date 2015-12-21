using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsIpChangeNotifyService
{
    public partial class IpChangeNotifyService : ServiceBase
    {
        private EventLog _EventLog;
        private System.Timers.Timer _Timer = new System.Timers.Timer();
        private string _LocalIP = string.Empty;
        private string _ExternalIP = string.Empty;

        public IpChangeNotifyService()
        {
            InitializeComponent();
            _EventLog = new EventLog();
            if (!EventLog.SourceExists("IPChangeNotify"))
            {
                EventLog.CreateEventSource(
                    "IPChangeNotify", "Log");
            }
            _EventLog.Source = "IPChangeNotify";
            _EventLog.Source = "Log";
            _Timer.Interval = 1000 * 60 * 60 * 1; // Check every hour
            _Timer.Elapsed += CheckIp;
        }

        private void CheckIp(object sender, System.Timers.ElapsedEventArgs e)
        {
            string externalIp = GetPublicIP();

            // Server Address
            var localIP = "127.0.0.1"; // Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(o => o. o.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).First().ToString();
            Notify(localIP, externalIp);
        }

        public static string GetPublicIP()
        {
            string url = "http://checkip.dyndns.org";
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            string a4 = a3[0];
            return a4;
        }

        protected override void OnStart(string[] args)
        {
            CheckIp(this, null);
            _Timer.Start();
        }

        protected override void OnStop()
        {
            _Timer.Stop();
        }

        private void Notify(string localIp, string externalIp)
        {
            if (_LocalIP == localIp && _ExternalIP == externalIp)
            {
                return;
            }


            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient()
            {
                Host = "mail.hoomanlogic.com",
                Port = 587, //587, 465
                EnableSsl = true,
                Timeout = 15000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("musheen@hoomanlogic.com", "Mu5h33nh3@d5!")
            };

            // Specify the e-mail sender.
            // Create a mailing address that includes a UTF8 character
            // in the display name.
            MailAddress from = new MailAddress("musheen@hoomanlogic.com", "Musheen", Encoding.UTF8);

            // Set destinations for the e-mail message.
            MailAddress to = new MailAddress("geoff.manning.sd@gmail.com");

            // Specify the message content.
            MailMessage message = new MailMessage(from, to);

            message.Subject = "IP";
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = string.Format("LOCAL: {0}\r\nPUBLIC: {1}", localIp, externalIp);
            message.BodyEncoding = Encoding.UTF8;

            // Ignore invalid remote server certificate
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate,
                            X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };

            client.Send(message);
            
            // Clean up.
            message.Dispose();

            // Remember latest result
            _LocalIP = localIp;
            _ExternalIP = externalIp;

        }

#if DEBUG
        public void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            this.OnStop();
        }
#endif
    }
}
