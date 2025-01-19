using Lórum.Screens;

namespace Lórum.Game
{
    public class Roller
    {
        public static int rollerTime2 = (int)GameTable.RollerTime;
        private static int N;

        public static void PlayerRoller()
        {
            rollerTime2 = (int)GameTable.RollerTime;
            if (rollerTime2 >= 11) GameTable.RollerTime = 11;
            switch (rollerTime2)
            {
                case 2:
                    Computer1.GépJáték1();
                    break;
                case 5:
                    Computer2.GépJáték2();
                    break;
                case 8:
                    Computer3.GépJáték3();
                    break;
                case 10:
                    GameTable.EnabledCard = true;
                    Computer1.Executed = false;
                    Computer2.Executed = false;
                    Computer3.Executed = false;
                    Main.Új_játék_engedve = true;
                    Main.Passz_Engedve = true;
                    var lehetSegesKartyak = 0;
                    for (N = 1; N <= 8; N++)
                        if (Main.KöverkezőLap(Main.Player1CardId[N]))
                            lehetSegesKartyak++;

                    Main.SegítségÁr = lehetSegesKartyak == 0 ? 0 : Main.rnd.Next(1, 5);
                    break;
            }
        }
    }
}