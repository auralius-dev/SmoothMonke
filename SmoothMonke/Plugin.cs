using BepInEx;
using System;
using UnityEngine;
using Utilla;

namespace SmoothMonke
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        void Start(){ Utilla.Events.GameInitialized += OnGameInitialized; }
        
        // Waits until after GT systems are intialized before attepting to do anything.
        // This is common practice now to prevent errors
        void OnGameInitialized(object sender, EventArgs e)  
        {
            foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>()) // Use FindObjectsOfTypeAll to get all objects, including disabled objects, such as the cave map. Then just loop through them all.
            {
                try
                {
                    {
                        foreach (Material mat in obj.GetComponent<Renderer>().materials) // Cycle through all materials on an object.
                        {
                            mat.mainTexture.filterMode = FilterMode.Bilinear; // Changing the FilterMode from point, to bilinear. Trilinear also works, but bilinear is all that's needed.
                        }
                    }
                }
                catch {}
            }
        }
    }
}
