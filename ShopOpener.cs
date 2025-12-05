using StardewValley;
using StardewValley.Objects;
using StardewValley.Mobile;
using StardewModdingAPI;
using HarmonyLib;

namespace Shoooop
{
    public class Blah : Mod
    {
        private static bool wasBtapped = false;

        public override void Entry(IModHelper helper)
        {
            var harmony = new Harmony(this.ModManifest.UniqueID);

            harmony.Patch(
                original: AccessTools.PropertyGetter(typeof(VirtualJoypad), nameof(VirtualJoypad.ButtonBPressed)),
                postfix: new HarmonyMethod(typeof(Blah), nameof(Blah.Postfix))
            );
        }
        private static void Postfix(ref bool __result)
        {
            var player = Game1.player;

            if (__result && !wasBtapped && Context.IsPlayerFree)
            {
                Game1.player.Money += 1000;

                if (player.CurrentItem is StardewValley.Object item)
                {
                    Game1.player.Money += 1000;

                    if (item.QualifiedItemId.Equals("(O)388"))
                    {
                        Game1.player.Money += 1000;

                        var direction = player.FacingDirection;

                        if (direction == 0)
                        {
                            Utility.TryOpenShopMenu(
                                shopId: "SeedShop",
                                ownerName: null
                            );
                        }
                        else if (direction == 1)
                        {
                            Utility.TryOpenShopMenu(
                                shopId: "Carpenter",
                                ownerName: null
                            );
                        }
                        else if (direction == 2)
                        {
                            Utility.TryOpenShopMenu(
                                shopId: "Blacksmith",
                                ownerName: null
                            );
                        }
                        else if (direction == 3)
                        {
                            Utility.TryOpenShopMenu(
                                shopId: "AdventureShop",
                                ownerName: null
                            );
                        }
                    }
                }
            }
            wasBtapped = __result;
        }
    }
}
