using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSP;
using KSP.UI.Screens;
using UnityEngine;
using System.Text.RegularExpressions;

namespace NodeAlert
{

    public class GUIHandler
    {

        private KSP.UI.Screens.ApplicationLauncherButton AppButton;
        public bool ButtonPressed;
        string AST = Settings.AlertStartTime.ToString();
        string ButtonTexture;
        DataManager DataManager = new DataManager();
        public GUIHandler()
        { }
        

        #region ApplicationButton
        public void createButton(string RelButtonPath)
        {
            ButtonPressed = false;
            if (AppButton == null && HighLogic.CurrentGame != null)
            {
                
                AppButton = KSP.UI.Screens.ApplicationLauncher.Instance.AddModApplication(
                  () => { ButtonPressed = true; Settings.LastButtonState = true; },
                  () => { ButtonPressed = false; Settings.LastButtonState = false; },
                  null, null, null, null,
                  KSP.UI.Screens.ApplicationLauncher.AppScenes.ALWAYS,
                  DataManager.buttonTexture(RelButtonPath));
                ButtonTexture = "SpaceCenter";
            }
        }
        public void createEnabledButton(string RelButtonPath)
        {
            ButtonPressed = true;
            if (AppButton == null && HighLogic.CurrentGame != null)
            {

                AppButton = KSP.UI.Screens.ApplicationLauncher.Instance.AddModApplication(
                  () => { ButtonPressed = false; Settings.LastButtonState = false; },
                  () => { ButtonPressed = true; Settings.LastButtonState = true; },
                  null, null, null, null,
                  KSP.UI.Screens.ApplicationLauncher.AppScenes.ALWAYS,
                  DataManager.buttonTexture(RelButtonPath));
                ButtonTexture = "SpaceCenter";
            }
        }

        public void ButtonTextureChanger()
        {
            if (DataManager.Scene(GameScenes.SPACECENTER) && ButtonTexture != "SpaceCenter")
            { AppButton.SetTexture(DataManager.buttonTexture("files/Button.png"));
                
                ButtonTexture = "SpaceCenter";
            }
            
            else if (ButtonPressed == true && ButtonTexture != "On" && !DataManager.Scene(GameScenes.SPACECENTER))
            { AppButton.SetTexture(DataManager.buttonTexture("files/ButtonON.png"));
                ButtonTexture = "On";
                
            }
            else if (ButtonPressed == false && ButtonTexture != "Off" && !DataManager.Scene(GameScenes.SPACECENTER))
            { AppButton.SetTexture(DataManager.buttonTexture("files/ButtonOFF.png"));
                ButtonTexture = "Off";
               
            }
           
            
           
        }
        bool buttonExist;
        public bool buttonEnabled()
        {
            if (ButtonPressed == false)
            { buttonExist = false; }
            else
            { buttonExist = true; }
            return buttonExist;
        }
        public void deleteButton()
        { KSP.UI.Screens.ApplicationLauncher.Instance.RemoveModApplication(AppButton); }
        #endregion

        public void SettingsMenu()
        {
            
            GUILayout.BeginArea(new Rect(20, 40, 380, 320));
            Settings.StopWarp = GUILayout.Toggle(Settings.StopWarp, "Stop timewarp");
            Settings.AlertSound = GUILayout.Toggle(Settings.AlertSound, "Play alert sound");
            Settings.AlternativeSound = GUILayout.Toggle(Settings.AlternativeSound,"Play alternative sound");
            Settings.EndBurnAlert = GUILayout.Toggle(Settings.EndBurnAlert, "Alert at the end of the Burn");
            //AlertStartTime///////////////////////////////////////////////
            GUILayout.Label("Alert start time (seconds before burn)");
            Settings.AlertStartTime = Mathf.RoundToInt(GUILayout.HorizontalSlider(Settings.AlertStartTime, 3, 60));
            GUILayout.Label((String.Format("Alert starts and timewarp stops {0} s before burn.", Settings.AlertStartTime)));
            //AlertEndDeltaV
            GUILayout.Label("   ");
            GUILayout.Label("Alert when the node ends (deltaV remaining)");
            Settings.AlertEndDeltaV = Mathf.RoundToInt(GUILayout.HorizontalSlider(Settings.AlertEndDeltaV, 5, 100));
            GUILayout.Label((String.Format("Alert starts when there is {0} m/s delta V remaining", Settings.AlertEndDeltaV)));
            GUILayout.EndArea();
            GUI.DragWindow();


        }
    }
}