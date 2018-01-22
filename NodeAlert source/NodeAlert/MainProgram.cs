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
            if (TimeWarp.CurrentRate > 100000 && BurnAlert(648000 + Settings.AlertStartTime)) //30D
                TimeWarp.SetRate(TimeWarp.CurrentRateIndex - 1, true);
            if (TimeWarp.CurrentRate > 10000 && BurnAlert(64800 + Settings.AlertStartTime))   //3 D
                TimeWarp.SetRate(TimeWarp.CurrentRateIndex - 1, true);
            if(TimeWarp.CurrentRate > 1000 && BurnAlert(600 + Settings.AlertStartTime))   //10 Min  
                TimeWarp.SetRate(TimeWarp.CurrentRateIndex - 1, true);
            if (TimeWarp.CurrentRate > 100 && BurnAlert(300 + Settings.AlertStartTime))   //5 Min  
                TimeWarp.SetRate(TimeWarp.CurrentRateIndex - 1, true);
            if (TimeWarp.CurrentRate > 50 && BurnAlert(150 + Settings.AlertStartTime))   //2.5 Min  
                TimeWarp.SetRate(TimeWarp.CurrentRateIndex - 1, true); 
            if (TimeWarp.CurrentRate > 10 && BurnAlert(60+Settings.AlertStartTime))
                TimeWarp.SetRate(TimeWarp.CurrentRateIndex - 1, true);
            if (TimeWarp.CurrentRate != 0 && BurnAlert(5 + Settings.AlertStartTime) && !BurnAlert(Settings.AlertStartTime))
                TimeWarp.SetRate(0, true);
                

        }
        public static void IncreaseWarp(double TimeToAlarm)
        {
            double TTA = TimeToAlarm - Planetarium.GetUniversalTime();
            int w;
            
            Debug.Log("[NodeAlert] Time to Alarm = " + TTA);
            if (TTA > 216000)
                TimeWarp.SetRate(7, false);
            else
            {
                for (w = 1; w <= 7; w++)
                {
                    if (TimeWarp.CurrentRate < TTA / 7)
                    { TimeWarp.SetRate(w, true); }

                }
                
            }
        }
        public static bool endAlert()
        { bool EndAlert = false;
            if (DataManager.NodeDeltaV() < Settings.AlertEndDeltaV && DataManager.NodeDeltaV() > Settings.AlertEndDeltaV - 10 && DataManager.ThrottleOn())
                EndAlert = true;
            return EndAlert;
            
        }
        static string KACNodeAlarm ="";
        public static void CreateKACAlarm()
        {
            Debug.Log("[NodeAlert] Creating KAC Alarm...");
            if (KACWrapper.APIReady && FlightGlobals.ActiveVessel.patchedConicSolver.maneuverNodes.Count > 0 && ManoeuvreProcessor.UniversalTime - ManoeuvreProcessor.HalfBurnTime - Settings.AlertStartTime - 5 > Planetarium.GetUniversalTime())
            {
                KACWrapper.KAC.DeleteAlarm(KACNodeAlarm);
                KACNodeAlarm = KACWrapper.KAC.CreateAlarm(KACWrapper.KACAPI.AlarmTypeEnum.Maneuver, "NodeAlert", ManoeuvreProcessor.UniversalTime - ManoeuvreProcessor.HalfBurnTime - Settings.AlertStartTime);
                if(KACNodeAlarm !="")
                {
                    KACWrapper.KACAPI.KACAlarm KACNodeAlarmOBJ = KACWrapper.KAC.Alarms.First(z => z.ID == KACNodeAlarm);
                    KACNodeAlarmOBJ.AlarmAction = KACWrapper.KACAPI.AlarmActionEnum.KillWarp;
                    IncreaseWarp(KACNodeAlarmOBJ.AlarmTime);
                    
                }
            }
            else
            Debug.Log("[NodeAlert] KAC API not ready");
        }
        public static void DeleteKACAlarmOnTime()
        {
            if (KACNodeAlarm == null) KACNodeAlarm = "";
            try
            {
                if (KACNodeAlarm != "" && KACWrapper.KAC.Alarms.First(z => z.ID == KACNodeAlarm).AlarmTime - Planetarium.GetUniversalTime() < 0)
                {
                    KACWrapper.KAC.DeleteAlarm(KACNodeAlarm);
                    KACNodeAlarm = "";
                }
            }
            catch(InvalidOperationException ex)
            {
                KACNodeAlarm = "";
                Debug.Log("[NodeAlert]" + ex);
            }
        }
        public static void ForceDeleteKACAlarm()
        {

            if (KACNodeAlarm != "")
            {
                KACWrapper.KAC.DeleteAlarm(KACNodeAlarm);
                KACNodeAlarm = "";
            }
        }
    }
}