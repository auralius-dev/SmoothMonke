using BepInEx;
using System;
using UnityEngine;

namespace SmoothMonke
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public void Awake() => Utilla.Events.GameInitialized += OnGameInitialized;

        // Waits until after GT systems are intialized before attepting to do anything.
        // This is common practice now to prevent errors
        void OnGameInitialized(object sender, EventArgs e)  
        {
            foreach (Renderer renderer in Resources.FindObjectsOfTypeAll<Renderer>()) // Use FindObjectsOfTypeAll to get all objects, including disabled objects, such as the cave map. Then just loop through them all.
            {
                if (renderer.sharedMaterial != null && renderer.sharedMaterial.mainTexture != null)
                {
                    // Save the original names for the material and it's texture, then change the filter mode.
                    string objectName = renderer.sharedMaterial.mainTexture.name;
                    string materialName = renderer.sharedMaterial.name;
                    renderer.sharedMaterial.mainTexture.filterMode = FilterMode.Bilinear;

                    // Reset the names for the material and it's texture to the names we saved prior to the filtering change. Removing this might cause slippery objects to not function as intended.
                    renderer.sharedMaterial.mainTexture.name = objectName;
                    renderer.sharedMaterial.name = materialName;
                }
            }
        }
    }
}
