using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
using KSP;

namespace NodeAlert
{
    
    public class DataManager
    {
        

        public DataManager()
        { }

        public Texture2D buttonTexture(string path)
        {
            var texture = new Texture2D(36, 36, TextureFormat.RGBA32, false);
            texture.LoadImage(File.ReadAllBytes(Pathfinder.dllPath() + path));
            return texture;
        }
        bool currentScene;
        public bool Scene(GameScenes thisScene)
        {

            if (thisScene == HighLogic.LoadedScene)
            { currentScene = true; }
            else
            { currentScene = false; }
            return currentScene;
        }
        public bool ThrottleOn()
        {
            bool Throttle;
            if (FlightInputHandler.state.mainThrottle != 0)
            {Throttle = true; }
            else
            {Throttle = false; }
            return Throttle;
        }
        public double NodeDeltaV()
        {
            
            var Node = FlightGlobals.ActiveVessel.patchedConicSolver.maneuverNodes [0];
            double deltaV = Node.GetBurnVector(FlightGlobals.ship_orbit).magnitude;
            
            return deltaV;
        }

        
        public AudioClip BeepSound(string path)
        {

            AudioClip returnClip;
            string wwwURL = "File:///" + Pathfinder.dllPath() + path;
            Debug.Log(wwwURL);
            wwwURL = wwwURL.Replace(@"\" , "/");
            
            WWW www = new WWW(wwwURL);
            Debug.Log(www.ToString());
            Debug.Log(wwwURL);
            
            returnClip = www.GetAudioClip(false);
            return returnClip;
            
        }


    }
}