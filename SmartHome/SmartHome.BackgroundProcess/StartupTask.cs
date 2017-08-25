﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace SmartHome.BackgroundProcess
{
    public sealed class StartupTask : IBackgroundTask
    {
        private BackgroundTaskDeferral _defferal;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            _defferal = taskInstance.GetDeferral();

            while (true)
            {
                System.Diagnostics.Debug.WriteLine("Tick");
                await Task.Delay(10);
            }
        }
    }
}
