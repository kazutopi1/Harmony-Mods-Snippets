using StardewValley;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace Blauh
{
    public class Yoinks : Mod
    {
        public MoneyBank moneyBank;

        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.SaveLoaded += GlobalMoney;
            helper.Events.GameLoop.ReturnedToTitle += SaveMoneyData;
        }
        private void GlobalMoney(object sender, SaveLoadedEventArgs e)
        {
            moneyBank = Helper.Data.ReadJsonFile<MoneyBank>("money.json") ?? new MoneyBank();
            if (moneyBank.Money == 0)
            {
                moneyBank.Money = Game1.player.Money;
            }
            else if (moneyBank.Money != null)
            {
                Game1.player.Money = moneyBank.Money;
            }
            Helper.Data.WriteJsonFile("money.json", moneyBank);
        }
        private void SaveMoneyData(object sender, ReturnedToTitleEventArgs e)
        {
            moneyBank.Money = Game1.player.Money;
            Helper.Data.WriteJsonFile("money.json", moneyBank);
        }
    }
    public class MoneyBank
    {
        public int Money { get; set; }
    }
}
