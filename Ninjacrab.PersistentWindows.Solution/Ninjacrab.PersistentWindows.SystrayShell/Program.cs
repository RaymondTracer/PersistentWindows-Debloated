﻿using System;
using System.Threading;
using System.Windows.Forms;
using Ninjacrab.PersistentWindows.Common;

namespace Ninjacrab.PersistentWindows.SystrayShell
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static PersistentWindowProcessor pwp;
        [STAThread]
        static void Main()
        {
#if (!DEBUG)
            Mutex singleInstMutex = new Mutex(true, Application.ProductName);
            if (!singleInstMutex.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show($"Only one inst of {Application.ProductName} can be run!");
                //Application.Exit();
                return;
            }
            else
            {
                singleInstMutex.ReleaseMutex();
            }
#endif

            StartSplashForm();

            pwp = new PersistentWindowProcessor();
            pwp.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new SystrayForm();
            Application.Run();
        }

        static void StartSplashForm()
        {
            var thread = new Thread(() => TimedSplashForm());
            thread.IsBackground = false;
            thread.Name = "StartSplashForm";
            thread.Start();
        }

        static void TimedSplashForm()
        {
            var thread = new Thread(() => Application.Run(new SplashForm()));
            thread.IsBackground = false;
            thread.Name = "TimedSplashForm";
            thread.Start();
            Thread.Sleep(5000);
            thread.Abort();
        }

        static public void Stop()
        {
            pwp.Stop();
        }
    }
}
