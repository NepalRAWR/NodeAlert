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
            if (TimeWarp.CurrentRate >= 7 && BurnAlert(64800)) //3Days
            { TimeWarp.SetRate(6, true); }
            if (TimeWarp.CurrentRate >= 6 && BurnAlert(900))  //10Min
            { TimeWarp.SetRate(5, true); }
            if (TimeWarp.CurrentRateIndex >= 5 && BurnAlert(90))
            { TimeWarp.SetRate(4, true); }
            if (BurnAlert(10))
            { TimeWarp.SetRate(0, true, true); }
        }
    }
}