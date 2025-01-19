using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Lórum.Content;
using Lórum.Game;
using Lórum.Screens;
using Lórum.Screens.CardManager;

namespace Lórum
{
    public class Main
    {
        public static List<Card> card = new List<Card>();
        public static int helpcard;
        public static int wins;
        public static int Loses;
        public static int Lorums;
        public static int dead1;
        public static int dead2;
        public static int PointsWin;
        public static int PointsLose;
        private static int Utolsó_Győztes;
        public static int kitettPiros, kitettZöld, kitettMakk, kitettTök;
        public static string Player2Text, Player3Text, Player4Text;
        public static bool JátékFolyamatban;
        public static bool Új_játék_engedve = true;
        public static bool Passz_Engedve;
        public static bool Adás_Engedve;
        public static readonly Random rnd = new Random();
        private static int i;
        private static int _n;
        public static bool LórumMakk;
        public static bool LórumPiros;
        public static bool LórumTök;
        public static bool LórumZöld;
        public static int Makk;
        public static int Pakli = 1;
        public static int Piros;
        public static int[] Player1CardId = new int[9];
        public static int[] Player2CardId = new int[9];
        public static int[] Player3CardId = new int[9];
        public static int[] Player4CardId = new int[9];
        public static int Tök;
        public static int Zöld;
        public static int Pontok1 = 75;
        public static int Pontok2 = 75;
        public static int Pontok3 = 75;
        public static int Pontok4 = 75;
        public static int SegítségÁr;
        public static int FirstPlayer;
        public static int AiMathValue = 24;
        public static int AiMathValue2 = 5;
        public static int Dealer1;
        public static int Dealer2, Dealer3;
        public static int Kezdőjátékos = 1;
        public static int KezdőLap;
        public static bool Játék_befejezve;
        public static int Kitettmakk;
        public static int Kitettpiros;
        public static int Kitetttök;
        public static int Kitettzöld;
        public static bool Lórum;
        public static bool Showcard;
        public int[] Asztal = new int[5];

        /// <summary>
        ///     Beállít néhány induló beállítást
        /// </summary>
        private static void init()
        {
            Trace.Listeners.Add(new TextWriterTraceListener());
            Trace.AutoFlush = true;
        }

        /// <summary>
        ///     Kiíratja a logokat azonos formátumban
        /// </summary>
        /// <param name="message"></param>
        /// Üzenet kiíratáshoz
        public static void Tracer(string message)
        {
            Trace.WriteLine(string.Concat("{0} - {1}", DateTime.Now, message));
        }

        /// <summary>
        ///     Véletlenszerűen kiosztja a kártyákat a játékosoknak és sorba is redezi őket
        /// </summary>
        private static void KartyaKiosztas()
        {
            Piros = 0;
            Zöld = 0;
            Makk = 0;
            Tök = 0;
            var kartyak = Enumerable.Range(1, 32).ToList().OrderBy(x => rnd.Next()).ToArray();
            for (var n = 0; n <= 31; n++)
            {
                if (n < 8)
                    Player1CardId[kartyak[n] / 8 + kartyak[n] % 8] = kartyak[n];
                if (n >= 8 && n < 16)
                    Player2CardId[kartyak[n] / 8 + kartyak[n] % 8] = kartyak[n];
                if (n >= 16 && n < 24)
                    Player3CardId[kartyak[n] / 8 + kartyak[n] % 8] = kartyak[n];
                if (n >= 24)
                    Player4CardId[kartyak[n] / 8 + kartyak[n] % 8] = kartyak[n];
            }

            Array.Sort(Player1CardId);
            Array.Sort(Player2CardId);
            Array.Sort(Player3CardId);
            Array.Sort(Player4CardId);
        }

        /// <summary>
        ///     Beállítja a kezdő értékeket a táblán, hogy a következő lap ellenőrző függvény eltudja dönteni, hogy az
        ///     következik-e.
        /// </summary>
        /// <param name="kezdőLap"></param>
        /// A kezdőlap
        public static void KezdésMegálapítás(int kezdőLap) //Kezdőlapok beállítása
        {
            switch (kezdőLap)
            {
                case 1:
                    Zöld = 16;
                    Makk = 24;
                    Tök = 32;
                    break;
                case 2:
                    Zöld = 9;
                    Makk = 17;
                    Tök = 25;
                    break;
                case 3:
                    Zöld = 10;
                    Makk = 18;
                    Tök = 26;
                    break;
                case 4:
                    Zöld = 11;
                    Makk = 19;
                    Tök = 27;
                    break;
                case 5:
                    Zöld = 12;
                    Makk = 20;
                    Tök = 28;
                    break;
                case 6:
                    Zöld = 13;
                    Makk = 21;
                    Tök = 29;
                    break;
                case 7:
                    Zöld = 14;
                    Makk = 22;
                    Tök = 30;
                    break;
                case 8:
                    Zöld = 15;
                    Makk = 23;
                    Tök = 31;
                    break;
                case 9:
                    Piros = 8;
                    Makk = 16;
                    Tök = 32;
                    break;
                case 10:
                    Piros = 1;
                    Makk = 17;
                    Tök = 25;
                    break;
                case 11:
                    Piros = 2;
                    Makk = 18;
                    Tök = 26;
                    break;
                case 12:
                    Piros = 3;
                    Makk = 19;
                    Tök = 27;
                    break;
                case 13:
                    Piros = 4;
                    Makk = 20;
                    Tök = 28;
                    break;
                case 14:
                    Piros = 5;
                    Makk = 21;
                    Tök = 29;
                    break;
                case 15:
                    Piros = 6;
                    Makk = 22;
                    Tök = 30;
                    break;
                case 16:
                    Piros = 7;
                    Makk = 23;
                    Tök = 31;
                    break;
                case 17:
                    Piros = 8;
                    Zöld = 16;
                    Tök = 32;
                    break;
                case 18:
                    Piros = 1;
                    Zöld = 9;
                    Tök = 25;
                    break;
                case 19:
                    Piros = 2;
                    Zöld = 10;
                    Tök = 26;
                    break;
                case 20:
                    Piros = 3;
                    Zöld = 11;
                    Tök = 27;
                    break;
                case 21:
                    Piros = 4;
                    Zöld = 12;
                    Tök = 28;
                    break;
                case 22:
                    Piros = 5;
                    Zöld = 13;
                    Tök = 29;
                    break;
                case 23:
                    Piros = 6;
                    Zöld = 14;
                    Tök = 30;
                    break;
                case 24:
                    Piros = 7;
                    Zöld = 15;
                    Tök = 31;
                    break;
                case 25:
                    Piros = 8;
                    Zöld = 16;
                    Makk = 24;
                    break;
                case 26:
                    Piros = 1;
                    Zöld = 9;
                    Makk = 17;
                    break;
                case 27:
                    Piros = 2;
                    Zöld = 10;
                    Makk = 18;
                    break;
                case 28:
                    Piros = 3;
                    Zöld = 11;
                    Makk = 19;
                    break;
                case 29:
                    Piros = 4;
                    Zöld = 12;
                    Makk = 20;
                    break;
                case 30:
                    Piros = 5;
                    Zöld = 13;
                    Makk = 21;
                    break;
                case 31:
                    Piros = 6;
                    Zöld = 14;
                    Makk = 22;
                    break;
                case 32:
                    Piros = 7;
                    Zöld = 15;
                    Makk = 23;
                    break;
            }
        }

        public static bool KöverkezőLap(int kártyaszám)
        {
            switch (kártyaszám)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    if (Piros == 0) return true;
                    if ((Piros + 1 == kártyaszám) & (kártyaszám != 8)) return true;
                    if ((kártyaszám == 1) & (Piros == 8)) return true;
                    if ((Piros == 7) & (kártyaszám == 8)) return true;
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    if (Zöld == 0) return true;
                    if ((Zöld + 1 == kártyaszám) & (kártyaszám != 16)) return true;
                    if ((kártyaszám == 9) & (Zöld == 16)) return true;
                    if ((Zöld == 15) & (kártyaszám == 16)) return true;
                    break;
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                    if (Makk == 0) return true;
                    if ((Makk + 1 == kártyaszám) & (kártyaszám != 24)) return true;
                    if ((kártyaszám == 17) & (Makk == 24)) return true;
                    if ((Makk == 23) & (kártyaszám == 24)) return true;
                    break;
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                    if (Tök == 0) return true;
                    if ((Tök + 1 == kártyaszám) & (kártyaszám != 32)) return true;
                    if ((kártyaszám == 25) & (Tök == 32)) return true;
                    if ((Tök == 31) & (kártyaszám == 32)) return true;
                    break;
            }

            return false;
        }

        public static string KátyaNév(int cardNumber)
        {
            switch (cardNumber)
            {
                case 1:
                    return "Piros Hetes";
                case 2:
                    return "Piros Nyolcas";
                case 3:
                    return "Piros Kilences";
                case 4:
                    return "Piros Tízes";
                case 5:
                    return "Piros Alsó";
                case 6:
                    return "Piros Felső";
                case 7:
                    return "Piros Csikó";
                case 8:
                    return "Piros Ász";
                case 9:
                    return "Zöld Hetes";
                case 10:
                    return "Zöld Nyolcas";
                case 11:
                    return "Zöld Kilences";
                case 12:
                    return "Zöld Tízes";
                case 13:
                    return "Zöld Alsó";
                case 14:
                    return "Zöld Felső";
                case 15:
                    return "Zöld Csikó";
                case 16:
                    return "Zöld Ász";
                case 17:
                    return "Makk Hetes";
                case 18:
                    return "Makk Nyolcas";
                case 19:
                    return "Makk Kilences";
                case 20:
                    return "Makk Tízes";
                case 21:
                    return "Makk Alsó";
                case 22:
                    return "Makk Felső";
                case 23:
                    return "Makk Csikó";
                case 24:
                    return "Makk Ász";
                case 25:
                    return "Tök Hetes";
                case 26:
                    return "Tök Nyolcas";
                case 27:
                    return "Tök Kilences";
                case 28:
                    return "Tök Tízes";
                case 29:
                    return "Tök Alsó";
                case 30:
                    return "Tök Felső";
                case 31:
                    return "Tök Csikó";
                case 32:
                    return "Tök Ász";
                case 0:
                    return "------";
            }

            return null;
        }

        public static void Jatekinditas() //Új játék indítása
        {
            if (!Új_játék_engedve) return;
            if (!GameTable.firtRun && !OptionsMenuScreen.hardmode)
                Kezdőjátékos++;
            if (OptionsMenuScreen.randomStartingplayer)
                Kezdőjátékos = rnd.Next(1, 5);
            if (OptionsMenuScreen.WinnerStartingplayer)
                switch (Utolsó_Győztes)
                {
                    case 1:
                        Kezdőjátékos = 1;
                        break;
                    case 2:
                        Kezdőjátékos = 2;
                        break;
                    case 3:
                        Kezdőjátékos = 3;
                        break;
                    case 4:
                        Kezdőjátékos = 4;
                        break;
                    default:
                        Kezdőjátékos = rnd.Next(1, 5);
                        break;
                }

            card.Clear();
            ComputerAI.lórumlapok[1] = 0;
            ComputerAI.lórumlapok[2] = 0;
            ComputerAI.lórumlapok[3] = 0;
            ComputerAI.lórumlapok[4] = 0;
            GameTable.player2showspeech = false;
            GameTable.player3showspeech = false;
            GameTable.player4showspeech = false;
            GameTable.firtRun = false;
            Játék_befejezve = false;
            kitettPiros = 0;
            kitettZöld = 0;
            kitettMakk = 0;
            kitettTök = 0;
            GameTable.VisitEnemies = false;
            Passz_Engedve = true;
            Játék_befejezve = false;
            JátékFolyamatban = false;
            Kitettpiros = 0;
            Kitettzöld = 0;
            Kitettmakk = 0;
            Kitetttök = 0; //Mindenből 0 van a táblán
            KartyaKiosztas(); //Kiosztás
            GameTable.GameEndead = false;
            if (Pontok1 <= 0)
            {
                Pontok1 = 75;
                dead2++;
                Statfrissítés();
            }

            if (Pontok2 <= 0)
            {
                Pontok2 = 75;
                dead1++;
                Statfrissítés();
            }

            if (Pontok3 <= 0)
            {
                Pontok3 = 75;
                dead1++;
                Statfrissítés();
            }

            if (Pontok4 <= 0)
            {
                Pontok4 = 75; //Pontok feltöltése ha 0
                dead1++;
                Statfrissítés();
            }

            if (Kezdőjátékos == 5)
                Kezdőjátékos = 1;
            Showcard = true; //Beállítás
            Lórum = true;
            LórumPiros = true;
            LórumZöld = true;
            LórumMakk = true;
            LórumTök = true;
            GameTable.JátékosKártyákSzáma = 8;
            GameTable.Játékos2KártyákSzáma = 8;
            GameTable.Játékos3KártyákSzáma = 8;
            GameTable.Játékos4KártyákSzáma = 8; //Mindenkinek 8 lapja van
            GameTable.VisibleArrow = true;
            Computer1.Executed = false;
            Computer2.Executed = false;
            Computer3.Executed = false;
            Adás_Engedve = true;
            switch (Kezdőjátékos)
            {
                case 1:
                    GameTable.player2showspeech_mode = 1;
                    GameTable.player3showspeech_mode = 1;
                    GameTable.player4showspeech_mode = 1;
                    GameTable.EnabledCard = true;
                    SegítségÁr = rnd.Next(1, 5); //Segítség árának generálása
                    GameTable.EnabledCard = true;
                    GameTable.RollerTime = 10;
                    JátékFolyamatban = true;
                    break;
                case 2:
                    Player2Text = "Megvenni a kezdést " + Dealer1 + " pontért";
                    GameTable.RollerTime = 1;
                    GameTable.EnabledCard = false;
                    break;
                case 3:
                    Player3Text = "Megvenni a kezdést " + Dealer2 + " pontért";
                    GameTable.RollerTime = 4;
                    GameTable.EnabledCard = false;
                    break;
                case 4:
                    Player4Text = "Megvenni a kezdést " + Dealer3 + " pontért";
                    GameTable.RollerTime = 7;
                    GameTable.EnabledCard = false;
                    break;
            }

            for (var i = 1; i <= 8; i++)
                AddCard(Player1CardId[i], 156 + (i - 1) * 60, 440, 100, 155, true);
            AddCard(0, 175, 188, 95, 152, false);
            AddCard(0, 297, 188, 95, 152, false);
            AddCard(0, 415, 188, 95, 152, false);
            AddCard(0, 534, 188, 95, 152, false);
        }

        public static void AddCard(int value, int posx, int posy, int width, int height, bool active)
        {
            var cards = new Card();
            cards.Initialize(value, posx, posy, width, height, active);
            card.Add(cards);
        }

        public void JátékosKártya1()
        {
            if (!KöverkezőLap(Player1CardId[1])) return;
            SetTable();
            GameTable.player2showspeech_mode = 0;
            GameTable.player3showspeech_mode = 0;
            GameTable.player4showspeech_mode = 0;
            GameTable.player2showspeech = false;
            GameTable.player3showspeech = false;
            GameTable.player4showspeech = false;
            switch (Player1CardId[1])
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    Piros = Player1CardId[1];
                    if ((Zöld == 0) & (Makk == 0) & (Tök == 0))
                    {
                        KezdésMegálapítás(Player1CardId[1]);
                        FirstPlayer = 1;
                        KezdőLap = Player1CardId[1];
                    }

                    GameTable.JátékosKártyákSzáma--;
                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumPiros = false;
                    kitettPiros++;
                    card[8].image = GameTable.KártyaSzám(Player1CardId[1]);
                    card[8].Active = true;
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    Zöld = Player1CardId[1];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Makk == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[1];
                        KezdésMegálapítás(Player1CardId[1]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumZöld = false;
                    kitettZöld++;
                    card[9].image = GameTable.KártyaSzám(Player1CardId[1]);
                    card[9].Active = true;
                    break;
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                    Makk = Player1CardId[1];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Zöld == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[1];
                        KezdésMegálapítás(Player1CardId[1]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumMakk = false;
                    kitettMakk++;
                    card[10].image = GameTable.KártyaSzám(Player1CardId[1]);
                    card[10].Active = true;
                    break;
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                    Tök = Player1CardId[1];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Zöld == 0) & (Makk == 0))
                    {
                        KezdőLap = Player1CardId[1];
                        KezdésMegálapítás(Player1CardId[1]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumTök = false;
                    kitettTök++;
                    card[11].image = GameTable.KártyaSzám(Player1CardId[1]);
                    card[11].Active = true;
                    break;
            }

            Player1CardId[1] = 0;
            GameTable.RollerTime = 0;
            GameTable.EnabledCard = false;
            card[0].Active = false;
        }

        public void JátékosKártya2()
        {
            if (!KöverkezőLap(Player1CardId[2])) return;
            SetTable();
            GameTable.player2showspeech_mode = 0;
            GameTable.player3showspeech_mode = 0;
            GameTable.player4showspeech_mode = 0;
            GameTable.player2showspeech = false;
            GameTable.player3showspeech = false;
            GameTable.player4showspeech = false;
            switch (Player1CardId[2])
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    Piros = Player1CardId[2];
                    if ((Zöld == 0) & (Makk == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[2];
                        KezdésMegálapítás(Player1CardId[2]);
                        FirstPlayer = 1;
                    }

                    GameTable.JátékosKártyákSzáma--;
                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumPiros = false;

                    kitettPiros++;
                    card[8].image = GameTable.KártyaSzám(Player1CardId[2]);
                    card[8].Active = true;
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    Zöld = Player1CardId[2];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Makk == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[2];
                        KezdésMegálapítás(Player1CardId[2]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumZöld = false;

                    kitettZöld++;
                    card[9].image = GameTable.KártyaSzám(Player1CardId[2]);
                    card[9].Active = true;
                    break;
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                    Makk = Player1CardId[2];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Zöld == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[2];
                        KezdésMegálapítás(Player1CardId[2]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumMakk = false;

                    kitettMakk++;
                    card[10].image = GameTable.KártyaSzám(Player1CardId[2]);
                    card[10].Active = true;
                    break;
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                    Tök = Player1CardId[2];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Zöld == 0) & (Makk == 0))
                    {
                        KezdőLap = Player1CardId[2];
                        KezdésMegálapítás(Player1CardId[2]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumTök = false;

                    kitettTök++;
                    card[11].image = GameTable.KártyaSzám(Player1CardId[2]);
                    card[11].Active = true;
                    break;
            }

            Player1CardId[2] = 0;
            GameTable.RollerTime = 0;
            GameTable.EnabledCard = false;
            card[1].Active = false;
        }

        public void JátékosKártya3()
        {
            if (!KöverkezőLap(Player1CardId[3])) return;
            SetTable();
            GameTable.player2showspeech_mode = 0;
            GameTable.player3showspeech_mode = 0;
            GameTable.player4showspeech_mode = 0;
            GameTable.player2showspeech = false;
            GameTable.player3showspeech = false;
            GameTable.player4showspeech = false;
            switch (Player1CardId[3])
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    Piros = Player1CardId[3];
                    if ((Zöld == 0) & (Makk == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[3];
                        KezdésMegálapítás(Player1CardId[3]);
                        FirstPlayer = 1;
                    }

                    GameTable.JátékosKártyákSzáma--;
                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumPiros = false;

                    kitettPiros++;
                    card[8].image = GameTable.KártyaSzám(Player1CardId[3]);
                    card[8].Active = true;
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    Zöld = Player1CardId[3];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Makk == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[3];
                        KezdésMegálapítás(Player1CardId[3]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumZöld = false;

                    kitettZöld++;
                    card[9].image = GameTable.KártyaSzám(Player1CardId[3]);
                    card[9].Active = true;
                    break;
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                    Makk = Player1CardId[3];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Zöld == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[3];
                        KezdésMegálapítás(Player1CardId[3]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumMakk = false;

                    kitettMakk++;
                    card[10].image = GameTable.KártyaSzám(Player1CardId[3]);
                    card[10].Active = true;
                    break;
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                    Tök = Player1CardId[3];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Zöld == 0) & (Makk == 0))
                    {
                        KezdőLap = Player1CardId[3];
                        KezdésMegálapítás(Player1CardId[3]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumTök = false;

                    kitettTök++;
                    card[11].image = GameTable.KártyaSzám(Player1CardId[3]);
                    card[11].Active = true;
                    break;
            }

            Player1CardId[3] = 0;
            GameTable.RollerTime = 0;
            GameTable.EnabledCard = false;
            card[2].Active = false;
        }

        public void JátékosKártya4()
        {
            if (!KöverkezőLap(Player1CardId[4])) return;
            SetTable();
            GameTable.player2showspeech_mode = 0;
            GameTable.player3showspeech_mode = 0;
            GameTable.player4showspeech_mode = 0;
            GameTable.player2showspeech = false;
            GameTable.player3showspeech = false;
            GameTable.player4showspeech = false;
            switch (Player1CardId[4])
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    Piros = Player1CardId[4];
                    if ((Zöld == 0) & (Makk == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[4];
                        KezdésMegálapítás(Player1CardId[4]);
                        FirstPlayer = 1;
                    }

                    GameTable.JátékosKártyákSzáma--;
                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumPiros = false;

                    kitettPiros++;
                    card[8].image = GameTable.KártyaSzám(Player1CardId[4]);
                    card[8].Active = true;
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    Zöld = Player1CardId[4];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Makk == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[4];
                        KezdésMegálapítás(Player1CardId[4]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumZöld = false;

                    kitettZöld++;
                    card[9].image = GameTable.KártyaSzám(Player1CardId[4]);
                    card[9].Active = true;
                    break;
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                    Makk = Player1CardId[4];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Zöld == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[4];
                        KezdésMegálapítás(Player1CardId[4]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumMakk = false;

                    kitettMakk++;
                    card[10].image = GameTable.KártyaSzám(Player1CardId[4]);
                    card[10].Active = true;
                    break;
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                    Tök = Player1CardId[4];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Zöld == 0) & (Makk == 0))
                    {
                        KezdőLap = Player1CardId[4];
                        KezdésMegálapítás(Player1CardId[4]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumTök = false;

                    kitettTök++;
                    card[11].image = GameTable.KártyaSzám(Player1CardId[4]);
                    card[11].Active = true;
                    break;
            }

            Player1CardId[4] = 0;
            GameTable.RollerTime = 0;
            GameTable.EnabledCard = false;
            card[3].Active = false;
        }

        public void JátékosKártya5()
        {
            if (!KöverkezőLap(Player1CardId[5])) return;
            SetTable();
            GameTable.player2showspeech_mode = 0;
            GameTable.player3showspeech_mode = 0;
            GameTable.player4showspeech_mode = 0;
            GameTable.player2showspeech = false;
            GameTable.player3showspeech = false;
            GameTable.player4showspeech = false;
            switch (Player1CardId[5])
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    Piros = Player1CardId[5];
                    if ((Zöld == 0) & (Makk == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[5];
                        KezdésMegálapítás(Player1CardId[5]);
                        FirstPlayer = 1;
                    }

                    GameTable.JátékosKártyákSzáma--;
                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumPiros = false;

                    kitettPiros++;
                    card[8].image = GameTable.KártyaSzám(Player1CardId[5]);
                    card[8].Active = true;
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    Zöld = Player1CardId[5];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Makk == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[5];
                        KezdésMegálapítás(Player1CardId[5]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumZöld = false;

                    kitettZöld++;
                    card[9].image = GameTable.KártyaSzám(Player1CardId[5]);
                    card[9].Active = true;
                    break;
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                    Makk = Player1CardId[5];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Zöld == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[5];
                        KezdésMegálapítás(Player1CardId[5]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumMakk = false;

                    kitettMakk++;
                    card[10].image = GameTable.KártyaSzám(Player1CardId[5]);
                    card[10].Active = true;
                    break;
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                    Tök = Player1CardId[5];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Zöld == 0) & (Makk == 0))
                    {
                        KezdőLap = Player1CardId[5];
                        KezdésMegálapítás(Player1CardId[5]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumTök = false;

                    kitettTök++;
                    card[11].image = GameTable.KártyaSzám(Player1CardId[5]);
                    card[11].Active = true;
                    break;
            }

            Player1CardId[5] = 0;
            GameTable.RollerTime = 0;
            GameTable.EnabledCard = false;
            card[4].Active = false;
        }

        public void JátékosKártya6()
        {
            if (!KöverkezőLap(Player1CardId[6])) return;
            SetTable();
            GameTable.player2showspeech_mode = 0;
            GameTable.player3showspeech_mode = 0;
            GameTable.player4showspeech_mode = 0;
            GameTable.player2showspeech = false;
            GameTable.player3showspeech = false;
            GameTable.player4showspeech = false;
            switch (Player1CardId[6])
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    Piros = Player1CardId[6];
                    if ((Zöld == 0) & (Makk == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[6];
                        KezdésMegálapítás(Player1CardId[6]);
                        FirstPlayer = 1;
                    }

                    GameTable.JátékosKártyákSzáma--;
                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumPiros = false;

                    kitettPiros++;
                    card[8].image = GameTable.KártyaSzám(Player1CardId[6]);
                    card[8].Active = true;
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    Zöld = Player1CardId[6];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Makk == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[6];
                        KezdésMegálapítás(Player1CardId[6]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumZöld = false;

                    kitettZöld++;
                    card[9].image = GameTable.KártyaSzám(Player1CardId[6]);
                    card[9].Active = true;
                    break;
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                    Makk = Player1CardId[6];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Zöld == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[6];
                        KezdésMegálapítás(Player1CardId[6]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumMakk = false;

                    kitettMakk++;
                    card[10].image = GameTable.KártyaSzám(Player1CardId[6]);
                    card[10].Active = true;
                    break;
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                    Tök = Player1CardId[6];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Zöld == 0) & (Makk == 0))
                    {
                        KezdőLap = Player1CardId[6];
                        KezdésMegálapítás(Player1CardId[6]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumTök = false;

                    kitettTök++;
                    card[11].image = GameTable.KártyaSzám(Player1CardId[6]);
                    card[11].Active = true;
                    break;
            }

            Player1CardId[6] = 0;
            GameTable.RollerTime = 0;
            GameTable.EnabledCard = false;
            card[5].Active = false;
        }

        public void JátékosKártya7()
        {
            if (!KöverkezőLap(Player1CardId[7])) return;
            SetTable();
            GameTable.player2showspeech_mode = 0;
            GameTable.player3showspeech_mode = 0;
            GameTable.player4showspeech_mode = 0;
            GameTable.player2showspeech = false;
            GameTable.player3showspeech = false;
            GameTable.player4showspeech = false;
            switch (Player1CardId[7])
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    Piros = Player1CardId[7];
                    if ((Zöld == 0) & (Makk == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[7];
                        KezdésMegálapítás(Player1CardId[7]);
                        FirstPlayer = 1;
                    }

                    GameTable.JátékosKártyákSzáma--;
                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumPiros = false;

                    kitettPiros++;
                    card[8].image = GameTable.KártyaSzám(Player1CardId[7]);
                    card[8].Active = true;
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    Zöld = Player1CardId[7];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Makk == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[7];
                        KezdésMegálapítás(Player1CardId[7]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumZöld = false;

                    kitettZöld++;
                    card[9].image = GameTable.KártyaSzám(Player1CardId[7]);
                    card[9].Active = true;
                    break;
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                    Makk = Player1CardId[7];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Zöld == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[7];
                        KezdésMegálapítás(Player1CardId[7]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumMakk = false;

                    kitettMakk++;
                    card[10].image = GameTable.KártyaSzám(Player1CardId[7]);
                    card[10].Active = true;
                    break;
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                    Tök = Player1CardId[7];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Zöld == 0) & (Makk == 0))
                    {
                        KezdőLap = Player1CardId[7];
                        KezdésMegálapítás(Player1CardId[7]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumTök = false;

                    kitettTök++;
                    card[11].image = GameTable.KártyaSzám(Player1CardId[7]);
                    card[11].Active = true;
                    break;
            }

            Player1CardId[7] = 0;
            GameTable.RollerTime = 0;
            GameTable.EnabledCard = false;
            card[6].Active = false;
        }

        public void JátékosKártya8()
        {
            if (!KöverkezőLap(Player1CardId[8])) return;
            SetTable();
            GameTable.player2showspeech_mode = 0;
            GameTable.player3showspeech_mode = 0;
            GameTable.player4showspeech_mode = 0;
            GameTable.player2showspeech = false;
            GameTable.player3showspeech = false;
            GameTable.player4showspeech = false;
            switch (Player1CardId[8])
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    Piros = Player1CardId[8];
                    if ((Zöld == 0) & (Makk == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[8];
                        KezdésMegálapítás(Player1CardId[8]);
                        FirstPlayer = 1;
                    }

                    GameTable.JátékosKártyákSzáma--;
                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumPiros = false;

                    kitettPiros++;
                    card[8].image = GameTable.KártyaSzám(Player1CardId[8]);
                    card[8].Active = true;
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    Zöld = Player1CardId[8];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Makk == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[8];
                        KezdésMegálapítás(Player1CardId[8]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumZöld = false;

                    kitettZöld++;
                    card[9].image = GameTable.KártyaSzám(Player1CardId[8]);
                    card[9].Active = true;
                    break;
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                    Makk = Player1CardId[8];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Zöld == 0) & (Tök == 0))
                    {
                        KezdőLap = Player1CardId[8];
                        KezdésMegálapítás(Player1CardId[8]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumMakk = false;

                    kitettMakk++;
                    card[10].image = GameTable.KártyaSzám(Player1CardId[8]);
                    card[10].Active = true;
                    break;
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                    Tök = Player1CardId[8];
                    GameTable.JátékosKártyákSzáma--;
                    if ((Piros == 0) & (Zöld == 0) & (Makk == 0))
                    {
                        KezdőLap = Player1CardId[8];
                        KezdésMegálapítás(Player1CardId[8]);
                        FirstPlayer = 1;
                    }

                    if (GameTable.JátékosKártyákSzáma == 0) Player1Win();
                    LórumTök = false;

                    kitettTök++;
                    card[11].image = GameTable.KártyaSzám(Player1CardId[8]);
                    card[11].Active = true;
                    break;
            }

            Player1CardId[8] = 0;
            GameTable.RollerTime = 0;
            GameTable.EnabledCard = false;
            card[7].Active = false;
        }

        public static void Pass()
        {
            if (!Passz_Engedve) return;
            if (Piros == 0 && Zöld == 0 && Makk == 0 && Tök == 0)
            {
                GameTable.player2showspeech_mode = 0;
                GameTable.player3showspeech_mode = 0;
                GameTable.player4showspeech_mode = 0;
                GameTable.player2showspeech = false;
                GameTable.player3showspeech = false;
                GameTable.player4showspeech = false;
                switch (Kezdőjátékos)
                {
                    case 1:
                        GameTable.SoundEffect[1].Play();
                        return;
                    case 2:
                        Új_játék_engedve = false;
                        Adás_Engedve = false;
                        Passz_Engedve = false;
                        JátékFolyamatban = true;
                        //////////////////////////////
                        GameTable.RollerTime = 1;
                        Computer1.Executed = false;
                        Computer2.Executed = false;
                        Computer3.Executed = false;
                        return;
                    case 3:
                        Passz_Engedve = false;
                        Új_játék_engedve = false;
                        Adás_Engedve = false;
                        JátékFolyamatban = true;
                        /////////////////////////////
                        GameTable.RollerTime = 4;
                        Computer2.Executed = false;
                        Computer3.Executed = false;
                        return;

                    case 4:
                        Passz_Engedve = false;
                        Új_játék_engedve = false;
                        Adás_Engedve = false;
                        JátékFolyamatban = true;
                        /////////////////////////////////
                        Computer3.Executed = false;
                        GameTable.RollerTime = 7;
                        return;
                }
            }

            var lehetSegesKartyak = 0;
            for (_n = 1; _n <= 8; _n++)
            {
                if (!KöverkezőLap(Player1CardId[_n])) continue;
                lehetSegesKartyak++;
            }

            if (lehetSegesKartyak == 0)
            {
                GameTable.RollerTime = 1;
                SetTable();
            }
            else
            {
                GameTable.SoundEffect[1].Play();
            }
        }

        public static void SetTable()
        {
            Új_játék_engedve = false;
            Passz_Engedve = false;
            Adás_Engedve = false;
            JátékFolyamatban = true;
            GameTable.showwish = false;
        }

        public static void TextMover()
        {
            if (GameTable.TextY >= 800)
                GameTable.GameEndead = false;
            else
                GameTable.TextY++;
        }

        public static void Player1Win()
        {
            Utolsó_Győztes = 1;
            Játék_befejezve = true;
            Új_játék_engedve = true;
            GameTable.TextY = -100;
            JátékFolyamatban = false;
            Adás_Engedve = false;
            Passz_Engedve = false;
            GameTable.GameEndead = true;
            Kezdőjátékos++;
            switch (rnd.Next(1, 3))
            {
                case 1:
                    GameTable.SoundEffect[3].Play();
                    break;
                case 2:
                    GameTable.SoundEffect[4].Play();
                    break;
            }

            if (!LórumMakk && !LórumPiros && !LórumTök && !LórumZöld)
            {
                GameTable.GameEndedTitle = 0;
                Pontok1 += GameTable.Játékos2KártyákSzáma + GameTable.Játékos3KártyákSzáma +
                           GameTable.Játékos4KártyákSzáma;
                Pontok2 -= GameTable.Játékos2KártyákSzáma;
                Pontok3 -= GameTable.Játékos3KártyákSzáma;
                Pontok4 -= GameTable.Játékos4KártyákSzáma;
                PointsWin += GameTable.Játékos2KártyákSzáma + GameTable.Játékos3KártyákSzáma +
                             GameTable.Játékos4KártyákSzáma;
                wins++;
            }
            else
            {
                GameTable.GameEndedTitle = 2;
                Pontok1 += (GameTable.Játékos2KártyákSzáma + GameTable.Játékos3KártyákSzáma +
                            GameTable.Játékos4KártyákSzáma) * 2;
                Pontok2 -= GameTable.Játékos2KártyákSzáma * 2;
                Pontok3 -= GameTable.Játékos3KártyákSzáma * 2;
                Pontok4 -= GameTable.Játékos4KártyákSzáma * 2;
                PointsWin += (GameTable.Játékos2KártyákSzáma + GameTable.Játékos3KártyákSzáma +
                              GameTable.Játékos4KártyákSzáma) * 2;
                wins++;
                Lorums++;
            }

            Statfrissítés();
        }

        public static void Player2Win()
        {
            Utolsó_Győztes = 2;
            Játék_befejezve = true;
            GameTable.TextY = -100;
            Új_játék_engedve = true;
            JátékFolyamatban = false;
            Adás_Engedve = false;
            Passz_Engedve = false;
            GameTable.GameEndead = true;
            Kezdőjátékos++;
            switch (rnd.Next(1, 3))
            {
                case 1:
                    GameTable.SoundEffect[5].Play();
                    break;
                case 2:
                    GameTable.SoundEffect[6].Play();
                    break;
            }

            if (!LórumMakk && !LórumPiros && !LórumTök && !LórumZöld)
            {
                GameTable.GameEndedTitle = 1;
                Pontok1 -= GameTable.JátékosKártyákSzáma;
                Pontok2 += GameTable.JátékosKártyákSzáma + GameTable.Játékos3KártyákSzáma +
                           GameTable.Játékos4KártyákSzáma;
                Pontok3 -= GameTable.Játékos3KártyákSzáma;
                Pontok4 -= GameTable.Játékos4KártyákSzáma;
                Loses++;
                PointsLose += GameTable.JátékosKártyákSzáma + GameTable.Játékos3KártyákSzáma +
                              GameTable.Játékos4KártyákSzáma;
            }
            else
            {
                GameTable.GameEndedTitle = 3;
                Pontok1 -= GameTable.JátékosKártyákSzáma * 2;
                Pontok2 += (GameTable.JátékosKártyákSzáma + GameTable.Játékos3KártyákSzáma +
                            GameTable.Játékos4KártyákSzáma) * 2;
                Pontok3 -= GameTable.Játékos3KártyákSzáma * 2;
                Pontok4 -= GameTable.Játékos4KártyákSzáma * 2;
                Loses++;
                PointsLose += (GameTable.JátékosKártyákSzáma + GameTable.Játékos3KártyákSzáma +
                               GameTable.Játékos4KártyákSzáma) * 2;
            }

            Statfrissítés();
        }

        public static void Player3Win()
        {
            Utolsó_Győztes = 3;
            Játék_befejezve = true;
            GameTable.TextY = -100;
            Új_játék_engedve = true;
            JátékFolyamatban = false;
            Adás_Engedve = false;
            Passz_Engedve = false;
            GameTable.GameEndead = true;
            Kezdőjátékos++;
            switch (rnd.Next(1, 3))
            {
                case 1:
                    GameTable.SoundEffect[5].Play();
                    break;
                case 2:
                    GameTable.SoundEffect[6].Play();
                    break;
            }

            if (!LórumMakk && !LórumPiros && !LórumTök && !LórumZöld)
            {
                GameTable.GameEndedTitle = 1;
                Pontok1 -= GameTable.JátékosKártyákSzáma;
                Pontok2 -= GameTable.Játékos2KártyákSzáma;
                Pontok3 += GameTable.JátékosKártyákSzáma + GameTable.Játékos2KártyákSzáma +
                           GameTable.Játékos4KártyákSzáma;
                Pontok4 -= GameTable.Játékos4KártyákSzáma;
                Loses++;
                PointsLose += GameTable.JátékosKártyákSzáma + GameTable.Játékos2KártyákSzáma +
                              GameTable.Játékos4KártyákSzáma;
            }
            else
            {
                GameTable.GameEndedTitle = 3;
                Pontok1 -= GameTable.JátékosKártyákSzáma * 2;
                Pontok2 -= GameTable.Játékos2KártyákSzáma * 2;
                Pontok3 += (GameTable.JátékosKártyákSzáma + GameTable.Játékos2KártyákSzáma +
                            GameTable.Játékos4KártyákSzáma) * 2;
                Pontok4 -= GameTable.Játékos4KártyákSzáma * 2;
                Loses++;
                PointsLose += (GameTable.JátékosKártyákSzáma + GameTable.Játékos2KártyákSzáma +
                               GameTable.Játékos4KártyákSzáma) * 2;
            }

            Statfrissítés();
        }

        public static void Player4Win()
        {
            Utolsó_Győztes = 4;
            Játék_befejezve = true;
            GameTable.TextY = -100;
            Új_játék_engedve = true;
            JátékFolyamatban = false;
            Adás_Engedve = false;
            Passz_Engedve = false;
            GameTable.GameEndead = true;
            Kezdőjátékos++;
            switch (rnd.Next(1, 3))
            {
                case 1:
                    GameTable.SoundEffect[5].Play();
                    break;
                case 2:
                    GameTable.SoundEffect[6].Play();
                    break;
            }

            if (!LórumMakk && !LórumPiros && !LórumTök && !LórumZöld)
            {
                GameTable.GameEndedTitle = 1;
                Pontok1 -= GameTable.JátékosKártyákSzáma;
                Pontok2 -= GameTable.Játékos2KártyákSzáma;
                Pontok3 -= GameTable.Játékos3KártyákSzáma;
                Pontok4 += GameTable.JátékosKártyákSzáma + GameTable.Játékos2KártyákSzáma +
                           GameTable.Játékos3KártyákSzáma;
                Loses++;
                PointsLose += GameTable.JátékosKártyákSzáma + GameTable.Játékos2KártyákSzáma +
                              GameTable.Játékos3KártyákSzáma;
            }
            else
            {
                GameTable.GameEndedTitle = 3;
                Pontok1 -= GameTable.JátékosKártyákSzáma * 2;
                Pontok2 -= GameTable.Játékos2KártyákSzáma * 2;
                Pontok3 -= GameTable.Játékos3KártyákSzáma * 2;
                Pontok4 += (GameTable.JátékosKártyákSzáma + GameTable.Játékos2KártyákSzáma +
                            GameTable.Játékos3KártyákSzáma) * 2;
                Loses++;
                PointsLose += (GameTable.JátékosKártyákSzáma + GameTable.Játékos2KártyákSzáma +
                               GameTable.Játékos3KártyákSzáma) * 2;
            }

            Statfrissítés();
        }

        public static void Statfrissítés()
        {
            if (Global.SaveDevice.IsReady)
                // save a file asynchronously. this will trigger IsBusy to return true
                // for the duration of the save process.
                Global.SaveDevice.SaveAsync(
                    Global.ContainerName,
                    Global.fileName_awards,
                    stream =>
                    {
                        using (var writer = new StreamWriter(stream))
                        {
                            writer.WriteLine(Encryptor.Encrypt(wins.ToString()));
                            writer.WriteLine(Encryptor.Encrypt(Loses.ToString()));
                            writer.WriteLine(Encryptor.Encrypt(Lorums.ToString()));
                            writer.WriteLine(Encryptor.Encrypt(dead1.ToString()));
                            writer.WriteLine(Encryptor.Encrypt(dead2.ToString()));
                            writer.WriteLine(Encryptor.Encrypt(PointsWin.ToString()));
                            writer.WriteLine(Encryptor.Encrypt(PointsLose.ToString()));
                        }
                    });
        }
    }
}