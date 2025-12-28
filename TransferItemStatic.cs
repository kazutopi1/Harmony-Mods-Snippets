using StardewValley;
using StardewValley.Menus;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using System.Collections.Generic;
using StardewValley.Objects;

namespace GlobalChest
{
    public class GCS : Mod
    {
        public static Chest chest;

        public override void Entry(IModHelper helper)
        {
            if (Constants.TargetPlatform != GamePlatform.Android) { return; }

            helper.Events.Input.ButtonPressed += StoreOrTake;
            helper.Events.GameLoop.GameLaunched += ChestInstance;
        }
        private void ChestInstance(object sender, GameLaunchedEventArgs e)
        { chest = new Chest(); }

        private void StoreOrTake(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button == SButton.F1 && Context.IsPlayerFree && Context.IsWorldReady)
            {
                OpenChest();
            }
        }
        private void OpenChest()
        {
            Game1.activeClickableMenu = new ItemGrabMenu(
                inventory: chest.Items,
                reverseGrab: false,
                showReceivingMenu: true,
                highlightFunction: null,
                behaviorOnItemSelectFunction: chest.grabItemFromInventory,
                message: null,
                behaviorOnItemGrab: chest.grabItemFromChest,
                snapToBottom: false,
                canBeExitedWithKey: true,
                playRightClickSound: true,
                allowRightClick: true,
                showOrganizeButton: true,
                storageCapacity: 36,
                numRows: 3,
                source: 1,
                sourceItem: chest
            );
        }
    }
}
