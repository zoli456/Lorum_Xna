﻿using System;
using Lórum.Screens;

namespace Lórum.Game
{
    internal class Computer3
    {
        public static bool Executed;

        public static void GépJáték3()
        {
            if (Executed) return;
            Executed = true;
            var _generator = new Random();
            int n;
            const int i = 8;
            var jelöltMakk = 0;
            var jelöltPiros = 0;
            var jelöltTök = 0;
            var jelöltZöld = 0;
            var pirosak = 0;
            var zöldek = 0;
            var makkok = 0;
            var tökök = 0;
            var lehetSegesKartyak = 0;
            int calculatork;
            for (n = 1; n <= i; n++)
                if (Main.KöverkezőLap(Main.Player4CardId[n]))
                    lehetSegesKartyak++;

            if (lehetSegesKartyak == 0) return;
            for (n = 1; n <= i; n++)
                switch (Main.Player4CardId[n])
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        pirosak++;
                        break;
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                        zöldek++;
                        break;
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 23:
                    case 24:
                        makkok++;
                        break;
                    case 25:
                    case 26:
                    case 27:
                    case 28:
                    case 29:
                    case 30:
                    case 31:
                    case 32:
                        tökök++;
                        break;
                }

            for (n = 1; n <= i; n++)
            {
                if (!Main.KöverkezőLap(Main.Player4CardId[n])) continue;
                switch (Main.Player4CardId[n])
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        jelöltPiros = Main.Player4CardId[n];
                        break;
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                        jelöltZöld = Main.Player4CardId[n];
                        break;
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 23:
                    case 24:
                        jelöltMakk = Main.Player4CardId[n];
                        break;
                    case 25:
                    case 26:
                    case 27:
                    case 28:
                    case 29:
                    case 30:
                    case 31:
                    case 32:
                        jelöltTök = Main.Player4CardId[n];
                        break;
                }
            }

            if ((Main.Piros == 0) & (Main.Zöld == 0) & (Main.Makk == 0) & (Main.Tök == 0))
            {
                Main.FirstPlayer = 4;
                //Gépi Logika Indítás----------------------------------
                calculatork = ComputerAI.Kezdés(pirosak, zöldek, makkok, tökök, jelöltPiros, jelöltZöld,
                    jelöltMakk, jelöltTök, Main.Player4CardId[1], Main.Player4CardId[2], Main.Player4CardId[3],
                    Main.Player4CardId[4], Main.Player4CardId[5], Main.Player4CardId[6], Main.Player4CardId[7],
                    Main.Player4CardId[8], 4);
                GameTable.Játékos4KártyákSzáma--;
                Main.KezdésMegálapítás(calculatork);
                Main.KezdőLap = calculatork;
                ComputerAI.DetectLorum(pirosak, zöldek, makkok, tökök, Main.Player1CardId[1], Main.Player1CardId[2],
                    Main.Player1CardId[3], Main.Player1CardId[4], Main.Player1CardId[5], Main.Player1CardId[6],
                    Main.Player1CardId[7], Main.Player1CardId[8], 1);
                switch (calculatork)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        Main.LórumPiros = false;
                        Main.Piros = calculatork;
                        Main.card[8].Active = true;
                        Main.card[8].image = GameTable.KártyaSzám(calculatork);
                        Main.kitettPiros++;
                        break;
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                        Main.LórumZöld = false;
                        Main.Zöld = calculatork;
                        Main.card[9].Active = true;
                        Main.card[9].image = GameTable.KártyaSzám(calculatork);
                        Main.kitettZöld++;
                        break;
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 23:
                    case 24:
                        Main.LórumMakk = false;
                        Main.Makk = calculatork;
                        Main.card[10].Active = true;
                        Main.card[10].image = GameTable.KártyaSzám(calculatork);
                        Main.kitettMakk++;
                        break;
                    case 25:
                    case 26:
                    case 27:
                    case 28:
                    case 29:
                    case 30:
                    case 31:
                    case 32:
                        Main.LórumTök = false;
                        Main.Tök = calculatork;
                        Main.card[11].Active = true;
                        Main.card[11].image = GameTable.KártyaSzám(calculatork);
                        Main.kitettTök++;
                        break;
                    default:
                        Error:
                        goto Error;
                }

                for (n = 1; n <= i; n++)
                    if (Main.Player4CardId[n] == calculatork)
                        Main.Player4CardId[n] = 0;

                return; // Kézdés 2 Gép VÉGE
            }

            ComputerAI.DetectLorum(pirosak, zöldek, makkok, tökök, Main.Player4CardId[1], Main.Player4CardId[2],
                Main.Player4CardId[3], Main.Player4CardId[4], Main.Player4CardId[5], Main.Player4CardId[6],
                Main.Player4CardId[7], Main.Player4CardId[8], 4);
            calculatork = ComputerAI.Játék(pirosak, zöldek, makkok, tökök, jelöltPiros, jelöltZöld, jelöltMakk,
                jelöltTök, Main.Player4CardId[1], Main.Player4CardId[2], Main.Player4CardId[3], Main.Player4CardId[4],
                Main.Player4CardId[5], Main.Player4CardId[6], Main.Player4CardId[7], Main.Player4CardId[8], 4);
            GameTable.Játékos4KártyákSzáma--;
            switch (calculatork)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    if (GameTable.Játékos4KártyákSzáma == 0) Main.Player4Win();
                    Main.LórumPiros = false;
                    Main.Piros = calculatork;
                    Main.card[8].Active = true;
                    Main.card[8].image = GameTable.KártyaSzám(calculatork);
                    Main.kitettPiros++;
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    if (GameTable.Játékos4KártyákSzáma == 0) Main.Player4Win();
                    Main.LórumZöld = false;
                    Main.Zöld = calculatork;
                    Main.card[9].Active = true;
                    Main.card[9].image = GameTable.KártyaSzám(calculatork);
                    Main.kitettZöld++;
                    break;
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                    if (GameTable.Játékos4KártyákSzáma == 0) Main.Player4Win();
                    Main.LórumMakk = false;
                    Main.Makk = calculatork;
                    Main.card[10].Active = true;
                    Main.card[10].image = GameTable.KártyaSzám(calculatork);
                    Main.kitettMakk++;
                    break;
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                    if (GameTable.Játékos4KártyákSzáma == 0) Main.Player4Win();
                    Main.LórumTök = false;
                    Main.Tök = calculatork;
                    Main.card[11].Active = true;
                    Main.card[11].image = GameTable.KártyaSzám(calculatork);
                    Main.kitettTök++;
                    break;
                default:
                    Error:
                    goto Error;
            }

            for (n = 1; n <= i; n++)
                if (Main.Player4CardId[n] == calculatork)
                    Main.Player4CardId[n] = 0;
        }
    }
}