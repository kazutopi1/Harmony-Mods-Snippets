using StardewValley;
using StardewModdingAPI;
using HarmonyLib;

namespace InfHealth
{
    public class HealthInf : Mod
    {
        public override void Entry(IModHelper helper)
        {
            var harmony = new Harmony(this.ModManifest.UniqueID);

            harmony.Patch(
                original: AccessTools.Method(typeof(Farmer), nameof(Farmer.takeDamage)),
                postfix: new HarmonyMethod(typeof(HealthInf), nameof(HealthInf.Postfix))
            );
        }
        private static void Postfix(Farmer __instance)
        {
            __instance.health = __instance.maxHealth;
        }
    }
}
