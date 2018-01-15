using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP;
using KerbalEngineer.Flight.Readouts.Orbital.ManoeuvreNode;
using KerbalEngineer.Helpers;

namespace NodeAlert
{
   
    public class MainProgram : MonoBehaviour
    {
        static DataManager DataManager = new DataManager();     
        //Seconds before Burn
        public static bool BurnAlert(double start)
        {
            
            bool BurnAlarm;
            double TTB = ManoeuvreProcessor.UniversalTime - ManoeuvreProcessor.HalfBurnTime - Planetarium.GetUniversalTime();
            if (TTB >= 0 && TTB <= start)
            { BurnAlarm = true; }
            else
            { BurnAlarm = false; }
            return BurnAlarm;
            
        }

        public static void DecreaseWarp()
        {
            if (TimeWarp.CurrentRate > 100000 && BurnAlert(648000)) //30D
                TimeWarp.SetRate(TimeWarp.CurrentRateIndex - 1, true);
            if (TimeWarp.CurrentRate > 10000 && BurnAlert(64800))   //3 D
                TimeWarp.SetRate(TimeWarp.CurrentRateIndex - 1, true);
            if(TimeWarp.CurrentRate > 1000 && BurnAlert(600))   //10 Min  
                TimeWarp.SetRate(TimeWarp.CurrentRateIndex - 1, true);
            if(TimeWarp.CurrentRate > 50 && BurnAlert(30+Settings.AlertStartTime))
                TimeWarp.SetRate(TimeWarp.CurrentRateIndex - 1, true);
            if (BurnAlert(5 + Settings.AlertStartTime) && !BurnAlert(Settings.AlertStartTime))
                TimeWarp.SetRate(0, true);
                

        }
        public static bool endAlert()
        { bool EndAlert = false;
            if (DataManager.NodeDeltaV() < Settings.AlertEndDeltaV && DataManager.NodeDeltaV() > Settings.AlertEndDeltaV - 10 && DataManager.ThrottleOn())
                EndAlert = true;
            return EndAlert;
            
        }
    }
}