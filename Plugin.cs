using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace Hat
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("ch.offbeatwit.baboonapi.plugin", "2.1.0")]
    public class Plugin : BaseUnityPlugin
    {
        private Harmony _harmony = new(PluginInfo.PLUGIN_GUID);
        private void Awake()
        {
            Logger = base.Logger;
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            _harmony.PatchAll();
        }

        internal new static ManualLogSource Logger { get; set; }
    }

    [HarmonyPatch(typeof(GameController), nameof(GameController.loadAssetBundleResources))]
    class GameControllerStartPatch
    {
        [HarmonyPrefix]
        static void PatchHat()
        {
            GlobalVariables.chosen_hat = 1;
            Plugin.Logger.LogInfo("Hat patched!");
        }
    }
}
