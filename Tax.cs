using StardewValley;
using StardewModdingAPI;
using HarmonyLib;
using System;

namespace Taxxxxxxx
{
    public class ImNotGonnaPayThat : Mod
    {
        public override void Entry(IModHelper helper)
        {
            var harmony = new Harmony(this.ModManifest.UniqueID);

            harmony.Patch(
                original: AccessTools.Method(typeof(Game1), nameof(Game1.OnDayStarted)),
                postfix: new HarmonyMethod(typeof(ImNotGonnaPayThat), nameof(ImNotGonnaPayThat.Postfix))
            );
        }
        private static void Postfix()
        {
            if (Game1.dayOfMonth == 28)
            {
                int currentMoney = Game1.player.Money;

                int cut = (int)Math.Ceiling(currentMoney * 0.20);

                if (cut > currentMoney)
                {
                    cut = currentMoney;
                }
                int moneyNow = currentMoney - cut;

                Game1.player.Money = moneyNow;
            }
        }
    }
}
