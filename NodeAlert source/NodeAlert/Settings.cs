using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NodeAlert
{
    
    public static class Settings
    {
        public static bool StopWarp { get; set; }
        public static bool AlertSound { get; set; }
        public static bool AlternativeSound { get; set; }
        public static bool EndBurnAlert { get; set; }
        

        static private int alertStartTime;
        public static int AlertStartTime
        {
            get { return alertStartTime;
               ;
            }
            set
            {
                if (value < 3 || value > 60)
                    throw new ArgumentOutOfRangeException("Alert Start Time must be between 3 and 60.");
                alertStartTime = value;
            }
        }
        static private int alertEndDeltaV;
        public static int AlertEndDeltaV
        {
            get
            {
                return alertEndDeltaV;
                ;
            }
            set
            {
                if (value < 5 || value > 100)
                    throw new ArgumentOutOfRangeException("Alert Start Time must be between 5 and 100.");
                alertEndDeltaV = value;
            }
        }
        static public bool LastButtonState { get; set; }
        static public bool KACAlarmMode { get; set; }

        private static float volumeMultiplicator;
        public static float VolumeMulitplicator
        {   get {   return volumeMultiplicator; }
            set {   if (value < 0.5 || value > 3)
                        throw new ArgumentOutOfRangeException("VolumeMultiplicator must be between 0.5 and 3.");
                    volumeMultiplicator = value;
            }
        }
    }
    
    
    static public class SettingsLoader
    {
        static SaveManager cfg = new SaveManager();
        static DataManager dM = new DataManager();

        public static void LoadAll()
        {
            cfg.Load("files/NodeAlert.cfg");
            Settings.StopWarp = cfg.getBoolValue("StopWarp");
            Settings.AlertSound = cfg.getBoolValue("AlertSound");
            Settings.AlternativeSound = cfg.getBoolValue("AlternativeSound");
            Settings.EndBurnAlert = cfg.getBoolValue("EndBurnAlert");
            Settings.AlertStartTime = cfg.getIntValue("AlertStartTime");
            Settings.LastButtonState = cfg.getBoolValue("LastButtonState");
            Settings.AlertEndDeltaV = cfg.getIntValue("AlertEndDeltaV");
            Settings.VolumeMulitplicator = cfg.getFloatValue("VolumeMultiplicator");
            Settings.KACAlarmMode = cfg.getBoolValue("KACAlarmMode");
        }
        public static void SaveAll()
        {
            
            cfg.setBoolValue("StopWarp", Settings.StopWarp);
            cfg.setBoolValue("AlertSound", Settings.AlertSound);
            cfg.setBoolValue("AlternativeSound", Settings.AlternativeSound);
            cfg.setBoolValue("EndBurnAlert",Settings.EndBurnAlert);
            cfg.setIntValue("AlertStartTime",Settings.AlertStartTime);
            if (!dM.Scene(GameScenes.SPACECENTER))
            cfg.setBoolValue("LastButtonState", Settings.LastButtonState);
            cfg.setIntValue("AlertEndDeltaV", Settings.AlertEndDeltaV);
            cfg.setFloatValue("VolumeMultiplicator", Settings.VolumeMulitplicator);
            cfg.setBoolValue("KACAlarmMode", Settings.KACAlarmMode);
            cfg.Save("files/NodeAlert.cfg");

        }
    }
}