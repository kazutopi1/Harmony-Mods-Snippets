using StardewValley;
using StardewValley.Buffs;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using MonoGame.Framework;
using HarmonyLib;
using Microsoft.Xna.Framework.Graphics;

namespace Atttttackk
{
    public class ThisWillWorkIPromise : Mod
    {
        private const int Threshhold = 50; // takeDamage() is called multiple times in a single hit thats why its set to 50

        private static int hitCount = 0;

        public override void Entry(IModHelper helper)
        {
            var harmony = new Harmony(this.ModManifest.UniqueID);

            harmony.Patch(
                original: AccessTools.Method(typeof(Farmer), nameof(Farmer.takeDamage)),
                postfix: new HarmonyMethod(typeof(ThisWillWorkIPromise), nameof(ThisWillWorkIPromise.Postfix))
            );
        }
        private static Buff BuffSetup()
        {
            Buff Retaliate = new Buff(
                id: "df.retaliate",
                displayName: "Retaliate",
                iconTexture: Game1.content.Load<Texture2D>("TileSheets/BuffsIcons"),
                iconSheetIndex: 11,
                duration: 1_000,
                effects: new BuffEffects()
                {
                    CriticalPowerMultiplier = { 2 },
                    CriticalChanceMultiplier = { 1000 },
                    WeaponSpeedMultiplier = { 10 },
                }
            );
            return Retaliate;
        }
        private static void Postfix(Farmer __instance, int damage)
        {
            hitCount += 1;

            if (hitCount >= Threshhold)
            {
                __instance.applyBuff(BuffSetup());
                hitCount = 0;
            }
        }
    }
}
