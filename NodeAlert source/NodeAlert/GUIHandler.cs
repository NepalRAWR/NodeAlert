using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSP;
using KSP.UI.Screens;
namespace NodeAlert
{

    public class GUIHandler
    {

        private KSP.UI.Screens.ApplicationLauncherButton AppButton;
        public bool ButtonPressed;
        
        string ButtonTexture;
        DataManager DataManager = new DataManager();
        public GUIHandler()
        { }
        

        #region ApplicationButton
        public void createButton(string RelButtonPath)
        {
            
            if (AppButton == null && HighLogic.CurrentGame != null)
            {
                
                AppButton = KSP.UI.Screens.ApplicationLauncher.Instance.AddModApplication(
                  () => { ButtonPressed = true; },
                  () => { ButtonPressed = false; },
                  null, null, null, null,
                  KSP.UI.Screens.ApplicationLauncher.AppScenes.ALWAYS,
                  DataManager.buttonTexture(RelButtonPath));
                ButtonTexture = "SpaceCenter";
            }
        }
        
        public void ButtonTextureChanger()
        {
            if (DataManager.Scene(GameScenes.SPACECENTER) && ButtonTexture != "SpaceCenter")
            { AppButton.SetTexture(DataManager.buttonTexture("/files/Button.png"));
                ButtonTexture = "SpaceCenter";
            }
            else if (ButtonPressed == true && ButtonTexture != "On" && !DataManager.Scene(GameScenes.SPACECENTER))
            { AppButton.SetTexture(DataManager.buttonTexture("/files/ButtonON.png"));
                ButtonTexture = "On";
                
            }
            else if (ButtonPressed == false && ButtonTexture != "Off" && !DataManager.Scene(GameScenes.SPACECENTER))
            { AppButton.SetTexture(DataManager.buttonTexture("/files/ButtonOFF.png"));
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

        
    }
}