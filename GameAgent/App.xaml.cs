using Enums.GameAgent;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace GameAgent
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Count() > 1)
            {
                
                AgentOrder orderType = (AgentOrder)int.Parse(args[1]);

                switch (orderType)
                {
                    case AgentOrder.Start:

                       

                        string GameFileName = args[2];
                        string startUpArgs = args[3].Replace('+', ' ');

                        ProcessStartInfo procStartInfo = new ProcessStartInfo();
                        Process procExecuting = new Process();

                        procStartInfo.UseShellExecute = true;
                        procStartInfo.FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GameFileName);
                        procStartInfo.Verb = "runas";
                        procStartInfo.Arguments = startUpArgs;


                        procExecuting = Process.Start(procStartInfo);


                        Thread.Sleep(200);
                        procExecuting.WaitForExit();
                        Thread.Sleep(200);


                        break;
                }
            }
            
            Current.Shutdown();

        }

    }
}
