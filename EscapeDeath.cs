using StardewValley;
using StardewModdingAPI;
using HarmonyLib;

namespace Totem
{
    public class OfUndying : Mod
    {
        public override void Entry(IModHelper helper)
        {
            var harmony = new Harmony(this.ModManifest.UniqueID);

            harmony.Patch(
                original: AccessTools.Method(typeof(Farmer), nameof(Farmer.Update)),
                prefix: new HarmonyMethod(typeof(OfUndying), nameof(OfUndying.Prefix))
            );
        }
        public static bool Prefix(Farmer __instance)
        {
            if (__instance.health <= 0 && !Game1.killScreen && Game1.timeOfDay < 2600)
            {
                if (__instance.hasItemInInventoryNamed("Wood"))
                {
                    __instance.health = 50;
                    return false;
                }
            }
            return true;
        }
    }
}
