using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NodeAlert
{

    public class SoundPlayer : MonoBehaviour
    {
        GameObject player;
        FXGroup audioSource = null;
        DataManager dataManager = new DataManager();
        
        public void PlaySound()
        {
            if (!audioSource.audio.isPlaying)
            {
                
                if (audioSource != null)
                {
                    audioSource.audio.Play(); 
                }
            }
        }

        public void LoadSound(string ID,string URL)
        {
                player = new GameObject();
                player.name = "NodeAlertPlayer" + ID;

                audioSource = new FXGroup(ID);
                audioSource.audio = player.AddComponent<AudioSource>() as AudioSource;
            audioSource.audio.clip = GameDatabase.Instance.GetAudioClip(Pathfinder.dllFolderName()+URL);
            
                audioSource.audio.volume = GameSettings.SHIP_VOLUME;
                Debug.Log("Loaded");
                

        }
    }
}