using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;


namespace NodeAlert
{
    [KSPAddon(KSPAddon.Startup.FlightAndKSC, false)]
    public class MainClass : MonoBehaviour
    {   //variables
        
        //functions
        public GUIHandler GUIHandler = new GUIHandler();
        DataManager DataManager = new DataManager();
        SoundPlayer BeepAlarm = new SoundPlayer();
        Rect Options = new Rect(100, 100, 400, 400);
        
        public void Start()
        {

            SettingsLoader.LoadAll();
            if(Settings.LastButtonState == true && !DataManager.Scene(GameScenes.SPACECENTER))
            { GUIHandler.createEnabledButton("files/button.png"); }
            else
            GUIHandler.createButton("files/button.png");
            if(Settings.AlternativeSound)
                BeepAlarm.LoadSound("Alert","files/Alert2");
            else
                BeepAlarm.LoadSound("Alert", "files/Alert");






        }
        
        public void Update()
        {
            GUIHandler.ButtonTextureChanger();
            if (GUIHandler.ButtonPressed && !DataManager.Scene(GameScenes.SPACECENTER) && FlightGlobals.ActiveVessel.patchedConicSolver.maneuverNodes.Count > 0)

            {
                if(Settings.StopWarp) MainProgram.DecreaseWarp();
                if (Settings.AlertSound && MainProgram.BurnAlert(Settings.AlertStartTime) && MainProgram.BurnAlert(Settings.AlertStartTime - 3) == false && DataManager.ThrottleOn() == false)
                {BeepAlarm.PlaySound(); }
                if (Settings.EndBurnAlert && MainProgram.endAlert())
                    BeepAlarm.PlaySound();
                
            }
           



        }

        public void OnGUI()
        {
            if (GUIHandler.ButtonPressed && DataManager.Scene(GameScenes.SPACECENTER))
            {
                GUI.skin = HighLogic.Skin;
                Options = GUI.Window(0,Options,OptionsWindow, "Settings");
                
            }

        }
        public void OptionsWindow(int WindowID)
        {
            
            GUIHandler.SettingsMenu();
            
        }
        public void OnDisable()
        {
            SettingsLoader.SaveAll();
            GUIHandler.deleteButton();
        }

    }
}
