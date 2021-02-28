using GreenBottle.Characters;

namespace GreenBottle.Items.Scrolls
{
    public class Teleportation : Scroll
    {
        public Teleportation()
        {
            Name = "Teleportation Scroll";
            Icon = "?";
            Type = "Scroll";
        }

        public static void Cast(Player player, DungeonMap map)
        {
            map.ChangeTileIcon(player.X, player.Y);

            map.PlacePlayer(player);
            //Console.WriteLine("you read a Telportation Scroll");
            //ActivityLog.AddToLog("you read a Telportation Scroll");
        }
    }
}
