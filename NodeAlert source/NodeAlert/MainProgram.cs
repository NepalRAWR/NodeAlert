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
            
            if (TimeWarp.CurrentRateIndex >= 7 && BurnAlert(64800)) //3Days
            { TimeWarp.SetRate(6, true);
                return;
            }
            if (TimeWarp.CurrentRateIndex >= 6 && BurnAlert(900))  //10Min
            { TimeWarp.SetRate(4, true); }
            if (TimeWarp.CurrentRateIndex >= 5 && BurnAlert(90+Settings.AlertStartTime))
            { TimeWarp.SetRate(3, true); }

            if (BurnAlert(5 + Settings.AlertStartTime) && !BurnAlert(Settings.AlertStartTime))
            { TimeWarp.SetRate(0, true, true); }
        }
        public static bool endAlert()
        { bool EndAlert = false;
            if (DataManager.NodeDeltaV() < Settings.AlertEndDeltaV && DataManager.NodeDeltaV() > Settings.AlertEndDeltaV - 10 && DataManager.ThrottleOn())
                EndAlert = true;
            return EndAlert;
            
        }
    }
}