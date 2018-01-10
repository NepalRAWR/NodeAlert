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
        GUIHandler GUIHandler = new GUIHandler();
        DataManager DataManager = new DataManager();
        SoundPlayer BeepAlarm = new SoundPlayer();
        
        
        public void Start()
        {
            GUIHandler.createButton("files/button.png");
            BeepAlarm.LoadSound("Alert","files/Alert");


        }
        
        public void Update()
        {
            
            GUIHandler.ButtonTextureChanger();
            if (GUIHandler.ButtonPressed)

            {
                MainProgram.DecreaseWarp();
                if(MainProgram.BurnAlert(3))
                { BeepAlarm.PlaySound(); }
            }
           



        }

        public void OnGUI()
        {


        }
        public void OnDisable()
        {
            GUIHandler.deleteButton();
        }

    }
}
