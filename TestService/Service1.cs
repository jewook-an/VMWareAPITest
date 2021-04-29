using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TestService
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;
        private const string folderPath = "C:\\temp\\scan\\";
        private const string filePath = folderPath + "service.txt";
        StreamWriter sw;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //if (Environment.UserInteractive)
            //{
            //    Service1 testService = new Service1();
            //    testService.TestStartupAndStop(args);
            //}
            //else
            //{
            //    // Put the body of your old Main method here.
            //    ServiceBase[] ServicesToRun;
            //    ServicesToRun = new ServiceBase[]
            //    {
            //        new Service1()
            //    };
            //    ServiceBase.Run(ServicesToRun);
            //}
            System.Diagnostics.Debugger.Launch();

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            writeLog("Serivce OnStart!");

            //timer = new Timer();
            //timer.Interval = 1000;
            //timer.Elapsed += Timer_Elapsed;
            //timer.Start();

            ServiceController KnoxService = new ServiceController("NCCHrKnoxSyncService");
            if (KnoxService.Status == ServiceControllerStatus.Running)
            {
                KnoxService.Stop();
            }
            KnoxService.Start();
        }

        internal void TestStartupAndStop(string[] args)
        {
            Console.WriteLine("TestStartupAndStop");
            this.OnStart(args);
            writeLog("TestStartupAndStop");

            Console.ReadLine();
            this.OnStop();
        }

        private void writeLog(string strMsg)
        {
            sw = new StreamWriter(filePath, true);
            sw.WriteLine(strMsg);
            sw.Close();

        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            sw = new StreamWriter(filePath, true);
            sw.WriteLine(DateTime.Now.ToString());
            sw.Close();
        }

        protected override void OnStop()
        {
            writeLog("Service Onstop!");
        }
    }
}
