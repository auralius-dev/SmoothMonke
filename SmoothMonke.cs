using BepInEx;
using HarmonyLib;
using UnityEngine;
using System.Reflection;
using GorillaLocomotion;

namespace SmoothMonke
{
    [BepInPlugin("org.auralius.monkeytag.texturefiltering", "Smooth Monke", "1.0.0.0")]
    [BepInProcess("Gorilla Tag.exe")]
    public class MonkePlugin : BaseUnityPlugin
    {
        private void Awake() => new Harmony("com.auralius.monkeytag.texturefiltering").PatchAll(Assembly.GetExecutingAssembly());

        [HarmonyPatch(typeof(Player))]
        [HarmonyPatch("Awake")]
        private class SmoothMonke_Patch
        {
            private static void Postfix(Player __instance)
            {
                foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>()) // Use FindObjectsOfTypeAll to get all disabled objects, such as the cave map. Then just loop through it all.
                {
                    try
                    {
                        foreach (Material mat in obj.GetComponent<Renderer>().materials) // Cycle through all materials on an object.
                        {
                            mat.mainTexture.filterMode = FilterMode.Bilinear; // Changing the FilterMode from point, to bilinear. Trilinear also works, but bilinear is all that's needed.
                        }
                    }
                    catch {
                        // Getting the materials on some game objects can cause an error. Unavoidable to my knowledge, as testing if it will error causes the error. So just skip that object and keep going. 
                    }
                }
            }
        }
    }
}