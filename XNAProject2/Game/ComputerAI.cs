using System;
using System.Collections.Generic;
using System.Linq;
using Lórum.Screens;

namespace Lórum.Game
{
    internal class ComputerAI
    {
        private static string[] Válaszok = new string[2];
        public static Eredmény[] eredmény = new Eredmény[9];
        public static int győztes_sim;
        public static int győztes_pontok_sim;
        public static int[] lórumlapok = new int[5];
        private static readonly int[] lapok = new int[9];
        private static readonly List<int> numbers = new List<int>();
        private static readonly Random _generator = new Random();
        public static int LórumJelölt;
        public static int laptávolság1;
        public static int laptávolság2;
        public static int laptávolság3;
        public static int laptávolság4;
        public static int temp;
        private static int lépés;
        private static int Találat;
        public static int[] counter = new int[8];
        public static int Randomizer;
        public static bool Randomcard;
        public static int lórumcard;
        public static bool Loselórum;
        public static int piros_simulated;
        public static int zöld_simulated;
        public static int makk_simulated;
        public static int tök_simulated;

        public static void DetectLorum(int pirosak, int zöldek, int makkok, int tökök, int lap1, int lap2, int lap3,
            int lap4, int lap5, int lap6, int lap7, int lap8, int játékos)
        {
            lapok[1] = lap1;
            lapok[2] = lap2;
            lapok[3] = lap3;
            lapok[4] = lap4;
            lapok[5] = lap5;
            lapok[6] = lap6;
            lapok[7] = lap7;
            lapok[8] = lap8;
            switch (játékos)
            {
                case 1:
                    if ((Main.Kezdőjátékos == 1) | (GameTable.JátékosKártyákSzáma < 8))
                        return;
                    if (Main.FirstPlayer == 1)
                        return;
                    break;
                case 2:
                    if ((Main.Kezdőjátékos == 2) | (GameTable.Játékos2KártyákSzáma < 8))
                        return;
                    if (Main.FirstPlayer == 2)
                        return;
                    break;
                case 3:
                    if ((Main.Kezdőjátékos == 3) | (GameTable.Játékos3KártyákSzáma < 8))
                        return;
                    if (Main.FirstPlayer == 3)
                        return;
                    break;
                case 4:
                    if ((Main.Kezdőjátékos == 4) | (GameTable.Játékos4KártyákSzáma < 8))
                        return;
                    if (Main.FirstPlayer == 4)
                        return;
                    break;
            }

            if ((Main.LórumPiros == false) & (Main.LórumTök == false) & (Main.LórumMakk == false) &
                (Main.LórumTök == false))
                return;
            if ((Main.KezdőLap == 1) | (Main.KezdőLap == 2) | (Main.KezdőLap == 3) | (Main.KezdőLap == 4) |
                (Main.KezdőLap == 5) |
                (Main.KezdőLap == 6) | (Main.KezdőLap == 7) | (Main.KezdőLap == 8))
            {
                if (zöldek == 1)
                    for (byte i = 1; i <= 8; i++)
                    {
                        if (lapok[i] != Main.KezdőLap + 8) continue;
                        switch (játékos)
                        {
                            case 1:
                                lórumlapok[1] = Main.KezdőLap + 8;
                                break;
                            case 2:
                                lórumlapok[2] = Main.KezdőLap + 8;
                                break;
                            case 3:
                                lórumlapok[3] = Main.KezdőLap + 8;
                                break;
                            case 4:
                                lórumlapok[4] = Main.KezdőLap + 8;
                                break;
                        }
                    }

                if (makkok == 1)
                    for (byte i = 1; i <= 8; i++)
                    {
                        if (lapok[i] != Main.KezdőLap + 16) continue;
                        switch (játékos)
                        {
                            case 1:
                                lórumlapok[1] = Main.KezdőLap + 16;
                                break;
                            case 2:
                                lórumlapok[2] = Main.KezdőLap + 16;
                                break;
                            case 3:
                                lórumlapok[3] = Main.KezdőLap + 16;
                                break;
                            case 4:
                                lórumlapok[4] = Main.KezdőLap + 16;
                                break;
                        }
                    }

                if (tökök == 1)
                    for (byte i = 1; i <= 8; i++)
                    {
                        if (lapok[i] != Main.KezdőLap + 24) continue;
                        switch (játékos)
                        {
                            case 1:
                                lórumlapok[1] = Main.KezdőLap + 24;
                                break;
                            case 2:
                                lórumlapok[2] = Main.KezdőLap + 24;
                                break;
                            case 3:
                                lórumlapok[3] = Main.KezdőLap + 24;
                                break;
                            case 4:
                                lórumlapok[4] = Main.KezdőLap + 24;
                                break;
                        }
                    }
            }

            if ((Main.KezdőLap == 9) | (Main.KezdőLap == 10) | (Main.KezdőLap == 11) | (Main.KezdőLap == 12) |
                (Main.KezdőLap == 13) | (Main.KezdőLap == 14) | (Main.KezdőLap == 15) | (Main.KezdőLap == 16))
            {
                if (pirosak == 1)
                    for (byte i = 1; i <= 8; i++)
                    {
                        if (lapok[i] != Main.KezdőLap - 8) continue;
                        switch (játékos)
                        {
                            case 1:
                                lórumlapok[1] = Main.KezdőLap - 8;
                                break;
                            case 2:
                                lórumlapok[2] = Main.KezdőLap - 8;
                                break;
                            case 3:
                                lórumlapok[3] = Main.KezdőLap - 8;
                                break;
                            case 4:
                                lórumlapok[4] = Main.KezdőLap - 8;
                                break;
                        }
                    }

                if (makkok == 1)
                    for (byte i = 1; i <= 8; i++)
                    {
                        if (lapok[i] != Main.KezdőLap + 8) continue;
                        switch (játékos)
                        {
                            case 1:
                                lórumlapok[1] = Main.KezdőLap + 8;
                                break;
                            case 2:
                                lórumlapok[2] = Main.KezdőLap + 8;
                                break;
                            case 3:
                                lórumlapok[3] = Main.KezdőLap + 8;
                                break;
                            case 4:
                                lórumlapok[4] = Main.KezdőLap + 8;
                                break;
                        }
                    }

                if (tökök == 1)
                    for (byte i = 1; i <= 8; i++)
                    {
                        if (lapok[i] != Main.KezdőLap + 16) continue;
                        switch (játékos)
                        {
                            case 1:
                                lórumlapok[1] = Main.KezdőLap + 16;
                                break;
                            case 2:
                                lórumlapok[2] = Main.KezdőLap + 16;
                                break;
                            case 3:
                                lórumlapok[3] = Main.KezdőLap + 16;
                                break;
                            case 4:
                                lórumlapok[4] = Main.KezdőLap + 16;
                                break;
                        }
                    }
            }

            if ((Main.KezdőLap == 17) | (Main.KezdőLap == 18) | (Main.KezdőLap == 19) | (Main.KezdőLap == 20) |
                (Main.KezdőLap == 21) | (Main.KezdőLap == 22) | (Main.KezdőLap == 23) | (Main.KezdőLap == 24))
            {
                if (pirosak == 1)
                    for (byte i = 1; i <= 8; i++)
                    {
                        if (lapok[i] != Main.KezdőLap - 16) continue;
                        switch (játékos)
                        {
                            case 1:
                                lórumlapok[1] = Main.KezdőLap - 16;
                                break;
                            case 2:
                                lórumlapok[2] = Main.KezdőLap - 16;
                                break;
                            case 3:
                                lórumlapok[3] = Main.KezdőLap - 16;
                                break;
                            case 4:
                                lórumlapok[4] = Main.KezdőLap - 16;
                                break;
                        }
                    }

                if (zöldek == 1)
                    for (byte i = 1; i <= 8; i++)
                    {
                        if (lapok[i] != Main.KezdőLap - 8) continue;
                        switch (játékos)
                        {
                            case 1:
                                lórumlapok[1] = Main.KezdőLap - 8;
                                break;
                            case 2:
                                lórumlapok[2] = Main.KezdőLap - 8;
                                break;
                            case 3:
                                lórumlapok[3] = Main.KezdőLap - 8;
                                break;
                            case 4:
                                lórumlapok[4] = Main.KezdőLap - 8;
                                break;
                        }
                    }

                if (tökök == 1)
                    for (byte i = 1; i <= 8; i++)
                    {
                        if (lapok[i] != Main.KezdőLap + 8) continue;
                        switch (játékos)
                        {
                            case 1:
                                lórumlapok[1] = Main.KezdőLap + 8;
                                break;
                            case 2:
                                lórumlapok[2] = Main.KezdőLap + 8;
                                break;
                            case 3:
                                lórumlapok[3] = Main.KezdőLap + 8;
                                break;
                            case 4:
                                lórumlapok[4] = Main.KezdőLap + 8;
                                break;
                        }
                    }
            }

            if ((Main.KezdőLap == 25) | (Main.KezdőLap == 26) | (Main.KezdőLap == 27) | (Main.KezdőLap == 28) |
                (Main.KezdőLap == 29) | (Main.KezdőLap == 30) | (Main.KezdőLap == 31) | (Main.KezdőLap == 32))
            {
                if (pirosak == 1)
                    for (byte i = 1; i <= 8; i++)
                    {
                        if (lapok[i] != Main.KezdőLap - 24) continue;
                        switch (játékos)
                        {
                            case 1:
                                lórumlapok[1] = Main.KezdőLap - 24;
                                break;
                            case 2:
                                lórumlapok[2] = Main.KezdőLap - 24;
                                break;
                            case 3:
                                lórumlapok[3] = Main.KezdőLap - 24;
                                break;
                            case 4:
                                lórumlapok[4] = Main.KezdőLap - 24;
                                break;
                        }
                    }

                if (zöldek == 1)
                    for (byte i = 1; i <= 8; i++)
                    {
                        if (lapok[i] != Main.KezdőLap - 16) continue;
                        switch (játékos)
                        {
                            case 1:
                                lórumlapok[1] = Main.KezdőLap - 16;
                                break;
                            case 2:
                                lórumlapok[2] = Main.KezdőLap - 16;
                                break;
                            case 3:
                                lórumlapok[3] = Main.KezdőLap - 16;
                                break;
                            case 4:
                                lórumlapok[4] = Main.KezdőLap - 16;
                                break;
                        }
                    }

                if (makkok == 1)
                    for (byte i = 1; i <= 8; i++)
                    {
                        if (lapok[i] != Main.KezdőLap - 8) continue;
                        switch (játékos)
                        {
                            case 1:
                                lórumlapok[1] = Main.KezdőLap - 8;
                                break;
                            case 2:
                                lórumlapok[2] = Main.KezdőLap - 8;
                                break;
                            case 3:
                                lórumlapok[3] = Main.KezdőLap - 8;
                                break;
                            case 4:
                                lórumlapok[4] = Main.KezdőLap - 8;
                                break;
                        }
                    }
            }
        }

        public static int Kezdés(int pirosak, int zöldek, int makkok, int tökök, int jelöltPiros, int jelöltZöld,
            int jelöltMakk, int jelöltTök, int lap1, int lap2, int lap3, int lap4, int lap5,
            int lap6, int lap7, int lap8, int játékos)
        {
            lapok[1] = lap1;
            lapok[2] = lap2;
            lapok[3] = lap3;
            lapok[4] = lap4;
            lapok[5] = lap5;
            lapok[6] = lap6;
            lapok[7] = lap7;
            lapok[8] = lap8;
            laptávolság1 = 0;
            laptávolság2 = 0;
            laptávolság3 = 0;
            laptávolság4 = 0;

            lap1 = 0;
            lap2 = 0;
            lap3 = 0;
            lap4 = 0;
            lórumcard = 0;
            LórumJelölt = 0;
            int s;
            int t;
            if (pirosak == 1)
                for (s = 1; s <= 8; s++)
                {
                    if (jelöltPiros != s) continue;
                    for (t = 1; t <= 8; t++)
                    {
                        if (lapok[t] == s + 8)
                        {
                            LórumJelölt = s;
                            lap2 = jelöltPiros + 8;
                            lórumcard = LórumJelölt;
                        }

                        if (lapok[t] == s + 16)
                        {
                            LórumJelölt = s;
                            lap3 = jelöltPiros + 16;
                            lórumcard = LórumJelölt;
                        }

                        if (lapok[t] != s + 24) continue;
                        LórumJelölt = s;
                        lap4 = jelöltPiros + 24;
                        lórumcard = LórumJelölt;
                    }
                }

            if (zöldek == 1)
                for (s = 9; s <= 16; s++)
                {
                    if (jelöltZöld != s) continue;
                    for (t = 1; t <= 8; t++)
                    {
                        if (lapok[t] == s - 8)
                        {
                            LórumJelölt = s;
                            lap1 = s - 8;
                            lórumcard = LórumJelölt;
                        }

                        if (lapok[t] == s + 8)
                        {
                            LórumJelölt = s;
                            lap3 = s + 8;
                            lórumcard = LórumJelölt;
                        }

                        if (lapok[t] != s + 16) continue;
                        LórumJelölt = s;
                        lap4 = s + 16;
                        lórumcard = LórumJelölt;
                    }
                }

            if (makkok == 1)
                for (s = 17; s <= 24; s++)
                {
                    if (jelöltMakk != s) continue;
                    for (t = 1; t <= 8; t++)
                    {
                        if (lapok[t] == s - 16)
                        {
                            LórumJelölt = s;
                            lap1 = s - 16;
                            lórumcard = LórumJelölt;
                        }

                        if (lapok[t] == s - 8)
                        {
                            LórumJelölt = s;
                            lap2 = s - 8;
                            lórumcard = LórumJelölt;
                        }

                        if (lapok[t] != s + 8) continue;
                        LórumJelölt = s;
                        lap4 = s + 8;
                        lórumcard = LórumJelölt;
                    }
                }

            if (tökök == 1)
                for (s = 25; s <= 32; s++)
                {
                    if (jelöltTök != s) continue;
                    for (t = 1; t <= 8; t++)
                    {
                        if (lapok[t] == s - 24)
                        {
                            LórumJelölt = s;
                            lap1 = s - 24;
                            lórumcard = LórumJelölt;
                        }

                        if (lapok[t] == s - 16)
                        {
                            LórumJelölt = s;
                            lap2 = s - 16;
                            lórumcard = LórumJelölt;
                        }

                        if (lapok[t] != s - 8) continue;
                        LórumJelölt = s;
                        lap3 = s - 8;
                        lórumcard = LórumJelölt;
                    }
                }

            switch (játékos)
            {
                case 1:
                    lórumlapok[1] = lórumcard;
                    break;
                case 2:
                    lórumlapok[2] = lórumcard;
                    break;
                case 3:
                    lórumlapok[3] = lórumcard;
                    break;
                case 4:
                    lórumlapok[4] = lórumcard;
                    break;
            }

            counter[0] = 0;
            counter[1] = 0;
            counter[2] = 0;
            counter[3] = 0;
            counter[4] = 0;
            counter[5] = 0;
            counter[6] = 0;
            counter[7] = 0;
            temp = 0;
            HezagokSzamitasKezdes(pirosak, zöldek, makkok, tökök, lap1);
            counter[0] = laptávolság1 + laptávolság2 + laptávolság3 + laptávolság4;
            HezagokSzamitasKezdes(pirosak, zöldek, makkok, tökök, lap2);
            counter[1] = laptávolság1 + laptávolság2 + laptávolság3 + laptávolság4;
            HezagokSzamitasKezdes(pirosak, zöldek, makkok, tökök, lap3);
            counter[2] = laptávolság1 + laptávolság2 + laptávolság3 + laptávolság4;
            HezagokSzamitasKezdes(pirosak, zöldek, makkok, tökök, lap4);
            counter[3] = laptávolság1 + laptávolság2 + laptávolság3 + laptávolság4;
            numbers.Clear();
            if (counter[0] != 0) numbers.Add(counter[0]);
            if (counter[1] != 0) numbers.Add(counter[1]);
            if (counter[2] != 0) numbers.Add(counter[2]);
            if (counter[3] != 0) numbers.Add(counter[3]);
            if (numbers.Count != 0)
                temp = numbers.Min();
            //Ha Lórum lehetséges melyik lap legyen az első
            if (numbers.Count != 0)
            {
                if (((temp == counter[0]) & (zöldek == 1)) | (makkok == 1) | (tökök == 1))
                    if (lap1 != 0)
                        return lap1;

                if (((temp == counter[1]) & (pirosak == 1)) | (makkok == 1) | (tökök == 1))
                    if (lap2 != 0)
                        return lap2;

                if (((temp == counter[2]) & (zöldek == 1)) | (pirosak == 1) | (tökök == 1))
                    if (lap3 != 0)
                        return lap3;

                if (((temp == counter[3]) & (zöldek == 1)) | (makkok == 1) | (pirosak == 1))
                    if (lap4 != 0)
                        return lap4;
            }

            lórumcard = 0;
            HezagokSzamitasKezdes(pirosak, zöldek, makkok, tökök, lapok[1]);
            counter[0] = laptávolság1 + laptávolság2 + laptávolság3 + laptávolság4;
            HezagokSzamitasKezdes(pirosak, zöldek, makkok, tökök, lapok[2]);
            counter[1] = laptávolság1 + laptávolság2 + laptávolság3 + laptávolság4;
            HezagokSzamitasKezdes(pirosak, zöldek, makkok, tökök, lapok[3]);
            counter[2] = laptávolság1 + laptávolság2 + laptávolság3 + laptávolság4;
            HezagokSzamitasKezdes(pirosak, zöldek, makkok, tökök, lapok[4]);
            counter[3] = laptávolság1 + laptávolság2 + laptávolság3 + laptávolság4;
            HezagokSzamitasKezdes(pirosak, zöldek, makkok, tökök, lapok[5]);
            counter[4] = laptávolság1 + laptávolság2 + laptávolság3 + laptávolság4;
            HezagokSzamitasKezdes(pirosak, zöldek, makkok, tökök, lapok[6]);
            counter[5] = laptávolság1 + laptávolság2 + laptávolság3 + laptávolság4;
            HezagokSzamitasKezdes(pirosak, zöldek, makkok, tökök, lapok[7]);
            counter[6] = laptávolság1 + laptávolság2 + laptávolság3 + laptávolság4;
            HezagokSzamitasKezdes(pirosak, zöldek, makkok, tökök, lapok[8]);
            counter[7] = laptávolság1 + laptávolság2 + laptávolság3 + laptávolság4;
            numbers.Clear();
            if (counter[0] != 0) numbers.Add(counter[0]);
            if (counter[1] != 0) numbers.Add(counter[1]);
            if (counter[2] != 0) numbers.Add(counter[2]);
            if (counter[3] != 0) numbers.Add(counter[3]);
            if (counter[4] != 0) numbers.Add(counter[4]);
            if (counter[5] != 0) numbers.Add(counter[5]);
            if (counter[6] != 0) numbers.Add(counter[6]);
            if (counter[7] != 0) numbers.Add(counter[7]);
            temp = numbers.Min();
            Randomcard = false;
            while (Randomcard == false)
            {
                var randomizer = _generator.Next(1, 9);
                if ((temp == counter[0]) & (randomizer == 1))
                {
                    Randomcard = true;
                    return lapok[1];
                }

                if ((temp == counter[1]) & (randomizer == 2))
                {
                    Randomcard = true;
                    return lapok[2];
                }

                if ((temp == counter[2]) & (randomizer == 3))
                {
                    Randomcard = true;
                    return lapok[3];
                }

                if ((temp == counter[3]) & (randomizer == 4))
                {
                    Randomcard = true;
                    return lapok[4];
                }

                if ((temp == counter[4]) & (randomizer == 5))
                {
                    Randomcard = true;
                    return lapok[5];
                }

                if ((temp == counter[5]) & (randomizer == 6))
                {
                    Randomcard = true;
                    return lapok[6];
                }

                if ((temp == counter[6]) & (randomizer == 7))
                {
                    Randomcard = true;
                    return lapok[7];
                }

                if ((temp == counter[7]) & (randomizer == 8))
                {
                    Randomcard = true;
                    return lapok[8];
                }
            }

            return 0;
        }

        public static int Játék(int pirosak, int zöldek, int makkok, int tökök, int jelöltPiros, int jelöltZöld,
            int jelöltMakk, int jelöltTök, int lap1, int lap2, int lap3, int lap4, int lap5,
            int lap6, int lap7, int lap8, int játékos)
        {
            switch (játékos)
            {
                case 1:
                    lórumcard = lórumlapok[1];
                    break;
                case 2:
                    lórumcard = lórumlapok[2];
                    break;
                case 3:
                    lórumcard = lórumlapok[3];
                    break;
                case 4:
                    lórumcard = lórumlapok[4];
                    break;
            }

            lapok[1] = lap1;
            lapok[2] = lap2;
            lapok[3] = lap3;
            lapok[4] = lap4;
            lapok[5] = lap5;
            lapok[6] = lap6;
            lapok[7] = lap7;
            lapok[8] = lap8;
            counter[0] = 0;
            counter[1] = 0;
            counter[2] = 0;
            counter[3] = 0;
            Loselórum = false;
            Randomcard = false;
            if ((jelöltPiros == lórumcard) & (jelöltZöld == 0) & (jelöltMakk == 0) & (jelöltTök == 0)) Loselórum = true;
            if ((jelöltZöld == lórumcard) & (jelöltPiros == 0) & (jelöltMakk == 0) & (jelöltTök == 0)) Loselórum = true;
            if ((jelöltMakk == lórumcard) & (jelöltZöld == 0) & (jelöltPiros == 0) & (jelöltTök == 0)) Loselórum = true;
            if ((jelöltTök == lórumcard) & (jelöltZöld == 0) & (jelöltMakk == 0) & (jelöltPiros == 0)) Loselórum = true;
            //ha van más lehetőség ------------------
            if (jelöltPiros != 0)
            {
                Hézag(pirosak, zöldek, makkok, tökök, jelöltPiros);
                counter[0] = laptávolság1;
            }

            if (jelöltZöld != 0)
            {
                Hézag(pirosak, zöldek, makkok, tökök, jelöltZöld);
                counter[1] = laptávolság1;
            }

            if (jelöltMakk != 0)
            {
                Hézag(pirosak, zöldek, makkok, tökök, jelöltMakk);
                counter[2] = laptávolság1;
            }

            if (jelöltTök != 0)
            {
                Hézag(pirosak, zöldek, makkok, tökök, jelöltTök);
                counter[3] = laptávolság1;
            }

            var ints = new[] { counter[0], counter[1], counter[2], counter[3] };
            temp = ints.Max();
            if (Loselórum == false)
                while (Randomcard == false)
                {
                    Randomizer = _generator.Next(1, 5);
                    if ((temp == counter[0]) & (jelöltPiros != lórumcard) & (Randomizer == 1) & (jelöltPiros != 0))
                    {
                        Randomcard = true;
                        return jelöltPiros;
                    }

                    if ((temp == counter[1]) & (jelöltZöld != lórumcard) & (Randomizer == 2) & (jelöltZöld != 0))
                    {
                        Randomcard = true;
                        return jelöltZöld;
                    }

                    if ((temp == counter[2]) & (jelöltMakk != lórumcard) & (Randomizer == 3) & (jelöltMakk != 0))
                    {
                        Randomcard = true;
                        return jelöltMakk;
                    }

                    if ((temp == counter[3]) & (jelöltTök != lórumcard) & (Randomizer == 4) & (jelöltTök != 0))
                    {
                        Randomcard = true;
                        return jelöltTök;
                    }
                }

            if (Loselórum) return lórumcard;
            return 0;
        }

        public static void HezagokSzamitasKezdes(int pirosak, int zöldek, int makkok, int tökök, int alany)
        {
            laptávolság1 = 0;
            laptávolság2 = 0;
            laptávolság3 = 0;
            laptávolság4 = 0;
            if (alany == 0) return;
            //Lap távolságok vizsgálat
            //piros lapok
            if ((alany == 1) | (alany == 2) | (alany == 3) | (alany == 4) | (alany == 5) | (alany == 6) | (alany == 7) |
                (alany == 8))
            {
                Találat = 0;
                lépés = alany;
                if (pirosak > 1)
                    while (Találat != pirosak)
                    {
                        lépés++;
                        if (lépés == 9) lépés = 1;
                        if ((lapok[1] == lépés) | (lapok[2] == lépés) |
                            (lapok[3] == lépés) | (lapok[4] == lépés) |
                            (lapok[5] == lépés) | (lapok[6] == lépés) |
                            (lapok[7] == lépés) | (lapok[8] == lépés))
                        {
                            Találat++;
                            laptávolság1++;
                        }
                        else
                        {
                            laptávolság1 += 2;
                        }
                    }

                Találat = 0;
                lépés = alany + 8;
                if (zöldek > 0)
                    while (Találat != zöldek)
                    {
                        lépés++;
                        if (lépés == 17) lépés = 9;
                        if ((lapok[1] == lépés) | (lapok[2] == lépés) |
                            (lapok[3] == lépés) | (lapok[4] == lépés) |
                            (lapok[5] == lépés) | (lapok[6] == lépés) |
                            (lapok[7] == lépés) | (lapok[8] == lépés))
                        {
                            Találat++;
                            laptávolság2++;
                        }
                        else
                        {
                            laptávolság2 += 2;
                        }
                    }

                Találat = 0;
                lépés = alany + 16;
                if (makkok > 0)
                    while (Találat != makkok)
                    {
                        lépés++;
                        if (lépés == 25) lépés = 17;
                        if ((lapok[1] == lépés) | (lapok[2] == lépés) |
                            (lapok[3] == lépés) | (lapok[4] == lépés) |
                            (lapok[5] == lépés) | (lapok[6] == lépés) |
                            (lapok[7] == lépés) | (lapok[8] == lépés))
                        {
                            Találat++;
                            laptávolság3++;
                        }
                        else
                        {
                            laptávolság3 += 2;
                        }
                    }

                Találat = 0;
                lépés = alany + 24;
                if (tökök > 0)
                    while (Találat != tökök)
                    {
                        lépés++;
                        if (lépés == 33) lépés = 25;
                        if ((lapok[1] == lépés) | (lapok[2] == lépés) |
                            (lapok[3] == lépés) | (lapok[4] == lépés) |
                            (lapok[5] == lépés) | (lapok[6] == lépés) |
                            (lapok[7] == lépés) | (lapok[8] == lépés))
                        {
                            Találat++;
                            laptávolság4++;
                        }
                        else
                        {
                            laptávolság4 += 2;
                        }
                    }

                return;
            }

            //zöld lapok
            if ((alany == 9) | (alany == 10) | (alany == 11) | (alany == 12) | (alany == 13) | (alany == 14) |
                (alany == 15) |
                (alany == 16))
            {
                Találat = 0;
                lépés = alany - 8;
                if (pirosak > 0)
                    while (Találat != pirosak)
                    {
                        lépés++;
                        if (lépés == 9) lépés = 1;
                        if ((lapok[1] == lépés) | (lapok[2] == lépés) |
                            (lapok[3] == lépés) | (lapok[4] == lépés) |
                            (lapok[5] == lépés) | (lapok[6] == lépés) |
                            (lapok[7] == lépés) | (lapok[8] == lépés))
                        {
                            Találat++;
                            laptávolság1++;
                        }
                        else
                        {
                            laptávolság1 += 2;
                        }
                    }

                Találat = 0;
                lépés = alany;
                if (zöldek > 1)
                    while (Találat != zöldek)
                    {
                        lépés++;
                        if (lépés == 17) lépés = 9;
                        if ((lapok[1] == lépés) | (lapok[2] == lépés) |
                            (lapok[3] == lépés) | (lapok[4] == lépés) |
                            (lapok[5] == lépés) | (lapok[6] == lépés) |
                            (lapok[7] == lépés) | (lapok[8] == lépés))
                        {
                            Találat++;
                            laptávolság2++;
                        }
                        else
                        {
                            laptávolság2 += 2;
                        }
                    }

                Találat = 0;
                lépés = alany + 8;
                if (makkok > 0)
                    while (Találat != makkok)
                    {
                        lépés++;
                        if (lépés == 25) lépés = 17;
                        if ((lapok[1] == lépés) | (lapok[2] == lépés) |
                            (lapok[3] == lépés) | (lapok[4] == lépés) |
                            (lapok[5] == lépés) | (lapok[6] == lépés) |
                            (lapok[7] == lépés) | (lapok[8] == lépés))
                        {
                            Találat++;
                            laptávolság3++;
                        }
                        else
                        {
                            laptávolság3 += 2;
                        }
                    }

                Találat = 0;
                lépés = alany + 16;
                if (tökök > 0)
                    while (Találat != tökök)
                    {
                        lépés++;
                        if (lépés == 33) lépés = 25;
                        if ((lapok[1] == lépés) | (lapok[2] == lépés) |
                            (lapok[3] == lépés) | (lapok[4] == lépés) |
                            (lapok[5] == lépés) | (lapok[6] == lépés) |
                            (lapok[7] == lépés) | (lapok[8] == lépés))
                        {
                            Találat++;
                            laptávolság4++;
                        }
                        else
                        {
                            laptávolság4 += 2;
                        }
                    }

                return;
            }

            //mak lapok
            if ((alany == 17) | (alany == 18) | (alany == 19) | (alany == 20) | (alany == 21) | (alany == 22) |
                (alany == 23) |
                (alany == 24))
            {
                Találat = 0;
                lépés = alany - 16;
                if (pirosak > 0)
                    while (Találat != pirosak)
                    {
                        lépés++;
                        if (lépés == 9) lépés = 1;
                        if ((lapok[1] == lépés) | (lapok[2] == lépés) |
                            (lapok[3] == lépés) | (lapok[4] == lépés) |
                            (lapok[5] == lépés) | (lapok[6] == lépés) |
                            (lapok[7] == lépés) | (lapok[8] == lépés))
                        {
                            Találat++;
                            laptávolság1++;
                        }
                        else
                        {
                            laptávolság1 += 2;
                        }
                    }

                Találat = 0;
                lépés = alany - 8;
                if (zöldek > 0)
                    while (Találat != zöldek)
                    {
                        lépés++;
                        if (lépés == 17) lépés = 9;
                        if ((lapok[1] == lépés) | (lapok[2] == lépés) |
                            (lapok[3] == lépés) | (lapok[4] == lépés) |
                            (lapok[5] == lépés) | (lapok[6] == lépés) |
                            (lapok[7] == lépés) | (lapok[8] == lépés))
                        {
                            Találat++;
                            laptávolság2++;
                        }
                        else
                        {
                            laptávolság2 += 2;
                        }
                    }

                Találat = 0;
                lépés = alany;
                if (makkok > 1)
                    while (Találat != makkok)
                    {
                        lépés++;
                        if (lépés == 25) lépés = 17;
                        if ((lapok[1] == lépés) | (lapok[2] == lépés) |
                            (lapok[3] == lépés) | (lapok[4] == lépés) |
                            (lapok[5] == lépés) | (lapok[6] == lépés) |
                            (lapok[7] == lépés) | (lapok[8] == lépés))
                        {
                            Találat++;
                            laptávolság3++;
                        }
                        else
                        {
                            laptávolság3 += 2;
                        }
                    }

                Találat = 0;
                lépés = alany + 8;
                if (tökök > 0)
                    while (Találat != tökök)
                    {
                        lépés++;
                        if (lépés == 33) lépés = 25;
                        if ((lapok[1] == lépés) | (lapok[2] == lépés) |
                            (lapok[3] == lépés) | (lapok[4] == lépés) |
                            (lapok[5] == lépés) | (lapok[6] == lépés) |
                            (lapok[7] == lépés) | (lapok[8] == lépés))
                        {
                            Találat++;
                            laptávolság4++;
                        }
                        else
                        {
                            laptávolság4 += 2;
                        }
                    }

                return;
            }

            //tök lapok
            if ((alany == 25) | (alany == 26) | (alany == 27) | (alany == 28) | (alany == 29) | (alany == 30) |
                (alany == 31) |
                (alany == 32))
            {
                Találat = 0;
                lépés = alany - 24;
                if (pirosak > 0)
                    while (Találat != pirosak)
                    {
                        lépés++;
                        if (lépés == 9) lépés = 1;
                        if ((lapok[1] == lépés) | (lapok[2] == lépés) |
                            (lapok[3] == lépés) | (lapok[4] == lépés) |
                            (lapok[5] == lépés) | (lapok[6] == lépés) |
                            (lapok[7] == lépés) | (lapok[8] == lépés))
                        {
                            Találat++;
                            laptávolság1++;
                        }
                        else
                        {
                            laptávolság1 += 2;
                        }
                    }

                Találat = 0;
                lépés = alany - 16;
                if (zöldek > 0)
                    while (Találat != zöldek)
                    {
                        lépés++;
                        if (lépés == 17) lépés = 9;
                        if ((lapok[1] == lépés) | (lapok[2] == lépés) |
                            (lapok[3] == lépés) | (lapok[4] == lépés) |
                            (lapok[5] == lépés) | (lapok[6] == lépés) |
                            (lapok[7] == lépés) | (lapok[8] == lépés))
                        {
                            Találat++;
                            laptávolság2++;
                        }
                        else
                        {
                            laptávolság2 += 2;
                        }
                    }

                Találat = 0;
                lépés = alany - 8;
                if (makkok > 0)
                    while (Találat != makkok)
                    {
                        lépés++;
                        if (lépés == 25) lépés = 17;
                        if ((lapok[1] == lépés) | (lapok[2] == lépés) |
                            (lapok[3] == lépés) | (lapok[4] == lépés) |
                            (lapok[5] == lépés) | (lapok[6] == lépés) |
                            (lapok[7] == lépés) | (lapok[8] == lépés))
                        {
                            Találat++;
                            laptávolság3++;
                        }
                        else
                        {
                            laptávolság3 += 2;
                        }
                    }

                Találat = 0;
                lépés = alany;
                if (tökök > 1)
                    while (Találat != tökök)
                    {
                        lépés++;
                        if (lépés == 33) lépés = 25;
                        if ((lapok[1] == lépés) | (lapok[2] == lépés) |
                            (lapok[3] == lépés) | (lapok[4] == lépés) |
                            (lapok[5] == lépés) | (lapok[6] == lépés) |
                            (lapok[7] == lépés) | (lapok[8] == lépés))
                        {
                            Találat++;
                            laptávolság4++;
                        }
                        else
                        {
                            laptávolság4 += 2;
                        }
                    }
            }
        }

        private static void Hézag(int pirosak, int zöldek, int makkok, int tökök, int alany)
        {
            laptávolság1 = 0;
            if (alany == 0) return;
            if ((alany == 1) | (alany == 2) | (alany == 3) | (alany == 4) | (alany == 5) | (alany == 6) | (alany == 7) |
                (alany == 8))
            {
                if (pirosak == 1)
                {
                    laptávolság1 = 0;
                    return;
                }

                Találat = 0;
                lépés = Main.Piros;
                if (pirosak > 1)
                    while (Találat != pirosak)
                    {
                        lépés++;
                        if (lépés == 9) lépés = 1;
                        if ((lapok[1] == lépés) | (lapok[2] == lépés) |
                            (lapok[3] == lépés) | (lapok[4] == lépés) |
                            (lapok[5] == lépés) | (lapok[6] == lépés) |
                            (lapok[7] == lépés) | (lapok[8] == lépés))
                        {
                            Találat++;
                            laptávolság1++;
                        }
                        else
                        {
                            laptávolság1 += 2;
                        }
                    }

                return;
            }

            if ((alany == 9) | (alany == 10) | (alany == 11) | (alany == 12) | (alany == 13) | (alany == 14) |
                (alany == 15) |
                (alany == 16))
            {
                if (zöldek == 1)
                {
                    laptávolság1 = 0;
                    return;
                }

                Találat = 0;
                lépés = Main.Zöld;
                if (zöldek > 1)
                    while (Találat != zöldek)
                    {
                        lépés++;
                        if (lépés == 17) lépés = 9;
                        if ((lapok[1] == lépés) | (lapok[2] == lépés) |
                            (lapok[3] == lépés) | (lapok[4] == lépés) |
                            (lapok[5] == lépés) | (lapok[6] == lépés) |
                            (lapok[7] == lépés) | (lapok[8] == lépés))
                        {
                            Találat++;
                            laptávolság1++;
                        }
                        else
                        {
                            laptávolság1 += 2;
                        }
                    }

                return;
            }

            if ((alany == 17) | (alany == 18) | (alany == 19) | (alany == 20) | (alany == 21) | (alany == 22) |
                (alany == 23) |
                (alany == 24))
            {
                if (makkok == 1)
                {
                    laptávolság1 = 0;
                    return;
                }

                Találat = 0;
                lépés = Main.Makk;
                if (makkok > 1)
                    while (Találat != makkok)
                    {
                        lépés++;
                        if (lépés == 25) lépés = 17;
                        if ((lapok[1] == lépés) | (lapok[2] == lépés) |
                            (lapok[3] == lépés) | (lapok[4] == lépés) |
                            (lapok[5] == lépés) | (lapok[6] == lépés) |
                            (lapok[7] == lépés) | (lapok[8] == lépés))
                        {
                            Találat++;
                            laptávolság1++;
                        }
                        else
                        {
                            laptávolság1 += 2;
                        }
                    }

                return;
            }

            if ((alany == 25) | (alany == 26) | (alany == 27) | (alany == 28) | (alany == 29) | (alany == 30) |
                (alany == 31) |
                (alany == 32))
            {
                if (tökök == 1)
                {
                    laptávolság1 = 0;
                    return;
                }

                Találat = 0;
                lépés = Main.Tök;
                if (tökök > 1)
                    while (Találat != tökök)
                    {
                        lépés++;
                        if (lépés == 33) lépés = 25;
                        if ((lapok[1] == lépés) | (lapok[2] == lépés) |
                            (lapok[3] == lépés) | (lapok[4] == lépés) |
                            (lapok[5] == lépés) | (lapok[6] == lépés) |
                            (lapok[7] == lépés) | (lapok[8] == lépés))
                        {
                            Találat++;
                            laptávolság1++;
                        }
                        else
                        {
                            laptávolság1 += 2;
                        }
                    }
            }
        }

        public static string JátékSzimulálás(int játékos, int kezdlap)
        {
            var kezd_simulated = kezdlap;
            var mindenkátya = new int[33];
            var lapszám1 = 8;
            var lapszám2 = 8;
            var lapszám3 = 8;
            var lapszám4 = 8;
            var kitettpiros = 0;
            var kitettzöld = 0;
            var kitettmakk = 0;
            var kitetttök = 0;
            var cardtemp = 0;
            var lehetSegesKartyak = 0;
            var jelöltMakk = 0;
            var jelöltPiros = 0;
            var jelöltTök = 0;
            var jelöltZöld = 0;
            var pirosak = 0;
            var zöldek = 0;
            var makkok = 0;
            var tökök = 0;
            var játékos_sim = 0;
            piros_simulated = 0;
            zöld_simulated = 0;
            makk_simulated = 0;
            tök_simulated = 0;
            győztes_sim = 0;
            győztes_pontok_sim = 0;
            mindenkátya[1] = Main.Player1CardId[1];
            mindenkátya[2] = Main.Player1CardId[2];
            mindenkátya[3] = Main.Player1CardId[3];
            mindenkátya[4] = Main.Player1CardId[4];
            mindenkátya[5] = Main.Player1CardId[5];
            mindenkátya[6] = Main.Player1CardId[6];
            mindenkátya[7] = Main.Player1CardId[7];
            mindenkátya[8] = Main.Player1CardId[8];
            mindenkátya[9] = Main.Player2CardId[1];
            mindenkátya[10] = Main.Player2CardId[2];
            mindenkátya[11] = Main.Player2CardId[3];
            mindenkátya[12] = Main.Player2CardId[4];
            mindenkátya[13] = Main.Player2CardId[5];
            mindenkátya[14] = Main.Player2CardId[6];
            mindenkátya[15] = Main.Player2CardId[7];
            mindenkátya[16] = Main.Player2CardId[8];
            mindenkátya[17] = Main.Player3CardId[1];
            mindenkátya[18] = Main.Player3CardId[2];
            mindenkátya[19] = Main.Player3CardId[3];
            mindenkátya[20] = Main.Player3CardId[4];
            mindenkátya[21] = Main.Player3CardId[5];
            mindenkátya[22] = Main.Player3CardId[6];
            mindenkátya[23] = Main.Player3CardId[7];
            mindenkátya[24] = Main.Player3CardId[8];
            mindenkátya[25] = Main.Player4CardId[1];
            mindenkátya[26] = Main.Player4CardId[2];
            mindenkátya[27] = Main.Player4CardId[3];
            mindenkátya[28] = Main.Player4CardId[4];
            mindenkátya[29] = Main.Player4CardId[5];
            mindenkátya[30] = Main.Player4CardId[6];
            mindenkátya[31] = Main.Player4CardId[7];
            mindenkátya[32] = Main.Player4CardId[8];
            játékos_sim = játékos;
            for (byte n = 1; n <= 32; n++)
                if (mindenkátya[n] == kezdlap)
                    mindenkátya[n] = 0;
            switch (játékos)
            {
                case 1:
                    lapszám1--;
                    break;
                case 2:
                    lapszám2--;
                    break;
                case 3:
                    lapszám3--;
                    break;
                case 4:
                    lapszám4--;
                    break;
            }

            switch (kezdlap)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    piros_simulated = kezd_simulated;
                    kitettpiros++;
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    zöld_simulated = kezd_simulated;
                    kitettzöld++;
                    break;
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                    makk_simulated = kezd_simulated;
                    kitettmakk++;
                    break;
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                    tök_simulated = kezd_simulated;
                    kitetttök++;
                    break;
            }

            KezdésMegálapítás_Sim(kezd_simulated);
            ////////////////////////////////////////////////////////////////////////////////////////////
            következő_játékos:
            játékos_sim++;
            pirosak = 0;
            zöldek = 0;
            makkok = 0;
            tökök = 0;
            lehetSegesKartyak = 0;
            if (játékos_sim == 5)
                játékos_sim = 1;
            switch (játékos_sim)
            {
                case 1:
                    for (byte n = 1; n <= 8; n++)
                        if (KöverkezőLap_sim(mindenkátya[n]))
                            lehetSegesKartyak++;
                    if (lehetSegesKartyak == 0) goto következő_játékos;
                    for (byte n = 1; n <= 8; n++)
                    {
                        switch (mindenkátya[n])
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

                        if (!KöverkezőLap_sim(mindenkátya[n])) continue;
                        switch (mindenkátya[n])
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                                jelöltPiros = mindenkátya[n];
                                break;
                            case 9:
                            case 10:
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15:
                            case 16:
                                jelöltZöld = mindenkátya[n];
                                break;
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                                jelöltMakk = mindenkátya[n];
                                break;
                            case 25:
                            case 26:
                            case 27:
                            case 28:
                            case 29:
                            case 30:
                            case 31:
                            case 32:
                                jelöltTök = mindenkátya[n];
                                break;
                        }
                    }

                    cardtemp = Játék(pirosak, zöldek, makkok, tökök, jelöltPiros, jelöltZöld, jelöltMakk,
                        jelöltTök, mindenkátya[1], mindenkátya[2], mindenkátya[3], mindenkátya[4], mindenkátya[5],
                        mindenkátya[6], mindenkátya[7],
                        mindenkátya[8], 1);
                    lapszám1--;
                    for (byte i = 1; i <= 8; i++)
                        if (mindenkátya[i] == cardtemp)
                            mindenkátya[i] = 0;
                    break;
                case 2:
                    for (byte n = 9; n <= 16; n++)
                        if (KöverkezőLap_sim(mindenkátya[n]))
                            lehetSegesKartyak++;
                    if (lehetSegesKartyak == 0) goto következő_játékos;
                    for (byte n = 9; n <= 16; n++)
                    {
                        switch (mindenkátya[n])
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

                        if (!KöverkezőLap_sim(mindenkátya[n])) continue;
                        switch (mindenkátya[n])
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                                jelöltPiros = mindenkátya[n];
                                break;
                            case 9:
                            case 10:
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15:
                            case 16:
                                jelöltZöld = mindenkátya[n];
                                break;
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                                jelöltMakk = mindenkátya[n];
                                break;
                            case 25:
                            case 26:
                            case 27:
                            case 28:
                            case 29:
                            case 30:
                            case 31:
                            case 32:
                                jelöltTök = mindenkátya[n];
                                break;
                        }
                    }

                    cardtemp = Játék(pirosak, zöldek, makkok, tökök, jelöltPiros, jelöltZöld, jelöltMakk,
                        jelöltTök, mindenkátya[9], mindenkátya[10], mindenkátya[11], mindenkátya[12],
                        mindenkátya[13], mindenkátya[14], mindenkátya[15],
                        mindenkátya[16], 2);
                    lapszám2--;
                    for (byte i = 9; i <= 16; i++)
                        if (mindenkátya[i] == cardtemp)
                            mindenkátya[i] = 0;
                    break;
                case 3:
                    for (byte n = 17; n <= 24; n++)
                        if (KöverkezőLap_sim(mindenkátya[n]))
                            lehetSegesKartyak++;

                    if (lehetSegesKartyak == 0) goto következő_játékos;
                    for (byte n = 17; n <= 24; n++)
                    {
                        switch (mindenkátya[n])
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

                        if (!KöverkezőLap_sim(mindenkátya[n])) continue;
                        switch (mindenkátya[n])
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                                jelöltPiros = mindenkátya[n];
                                break;
                            case 9:
                            case 10:
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15:
                            case 16:
                                jelöltZöld = mindenkátya[n];
                                break;
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                                jelöltMakk = mindenkátya[n];
                                break;
                            case 25:
                            case 26:
                            case 27:
                            case 28:
                            case 29:
                            case 30:
                            case 31:
                            case 32:
                                jelöltTök = mindenkátya[n];
                                break;
                        }
                    }

                    cardtemp = Játék(pirosak, zöldek, makkok, tökök, jelöltPiros, jelöltZöld, jelöltMakk,
                        jelöltTök, mindenkátya[17], mindenkátya[18], mindenkátya[19], mindenkátya[20],
                        mindenkátya[21], mindenkátya[22], mindenkátya[23],
                        mindenkátya[24], 3);
                    lapszám3--;
                    for (byte i = 17; i <= 24; i++)
                        if (mindenkátya[i] == cardtemp)
                            mindenkátya[i] = 0;
                    break;
                case 4:
                    for (byte n = 25; n <= 32; n++)
                        if (KöverkezőLap_sim(mindenkátya[n]))
                            lehetSegesKartyak++;

                    if (lehetSegesKartyak == 0) goto következő_játékos;
                    for (byte n = 25; n <= 32; n++)
                    {
                        switch (mindenkátya[n])
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

                        if (!KöverkezőLap_sim(mindenkátya[n])) continue;
                        switch (mindenkátya[n])
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                                jelöltPiros = mindenkátya[n];
                                break;
                            case 9:
                            case 10:
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15:
                            case 16:
                                jelöltZöld = mindenkátya[n];
                                break;
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                                jelöltMakk = mindenkátya[n];
                                break;
                            case 25:
                            case 26:
                            case 27:
                            case 28:
                            case 29:
                            case 30:
                            case 31:
                            case 32:
                                jelöltTök = mindenkátya[n];
                                break;
                        }
                    }

                    cardtemp = Játék(pirosak, zöldek, makkok, tökök, jelöltPiros, jelöltZöld, jelöltMakk,
                        jelöltTök, mindenkátya[25], mindenkátya[26], mindenkátya[27], mindenkátya[28],
                        mindenkátya[29], mindenkátya[30], mindenkátya[31],
                        mindenkátya[32], 4);

                    lapszám4--;
                    for (byte i = 25; i <= 32; i++)
                        if (mindenkátya[i] == cardtemp)
                            mindenkátya[i] = 0;
                    break;
            }

            if (lapszám1 == 0)
                if (kitettpiros == 0 ||
                    kitettmakk == 0 || kitetttök == 0 || kitettzöld == 0)
                {
                    győztes_sim = 1;
                    győztes_pontok_sim = 2 * (lapszám2 + lapszám3 + lapszám4);
                }
                else
                {
                    győztes_sim = 1;
                    győztes_pontok_sim = lapszám2 + lapszám3 + lapszám4;
                }

            if (lapszám2 == 0)
                if (kitettpiros == 0 ||
                    kitettmakk == 0 || kitetttök == 0 || kitettzöld == 0)
                {
                    győztes_sim = 2;
                    győztes_pontok_sim = 2 * (lapszám1 + lapszám3 + lapszám4);
                }
                else
                {
                    győztes_sim = 2;
                    győztes_pontok_sim = lapszám1 + lapszám3 + lapszám4;
                }

            if (lapszám3 == 0)
                if (kitettpiros == 0 ||
                    kitettmakk == 0 || kitetttök == 0 || kitettzöld == 0)
                {
                    győztes_sim = 3;
                    győztes_pontok_sim = 2 * (lapszám2 + lapszám1 + lapszám4);
                }
                else
                {
                    győztes_sim = 3;
                    győztes_pontok_sim = lapszám1 + lapszám2 + lapszám4;
                }

            if (lapszám4 == 0)
                if (kitettpiros == 0 ||
                    kitettmakk == 0 || kitetttök == 0 || kitettzöld == 0)
                {
                    győztes_sim = 4;
                    győztes_pontok_sim = 2 * (lapszám2 + lapszám1 + lapszám3);
                }
                else
                {
                    győztes_sim = 4;
                    győztes_pontok_sim = lapszám1 + lapszám3 + lapszám2;
                }

            switch (cardtemp)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    piros_simulated = cardtemp;
                    kitettpiros++;
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    zöld_simulated = cardtemp;
                    kitettzöld++;
                    break;
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                    makk_simulated = cardtemp;
                    kitettmakk++;
                    break;
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                    tök_simulated = cardtemp;
                    kitetttök++;
                    break;
            }

            if (győztes_pontok_sim == 0)
                goto következő_játékos;
            return győztes_sim + " " + győztes_pontok_sim;
        }

        public static void KezdésMegálapítás_Sim(int kezdőLap) //Kezdőlapok beállítása
        {
            switch (kezdőLap)
            {
                case 1:
                    zöld_simulated = 16;
                    makk_simulated = 24;
                    tök_simulated = 32;
                    break;
                case 2:
                    zöld_simulated = 9;
                    makk_simulated = 17;
                    tök_simulated = 25;
                    break;
                case 3:
                    zöld_simulated = 10;
                    makk_simulated = 18;
                    tök_simulated = 26;
                    break;
                case 4:
                    zöld_simulated = 11;
                    makk_simulated = 19;
                    tök_simulated = 27;
                    break;
                case 5:
                    zöld_simulated = 12;
                    makk_simulated = 20;
                    tök_simulated = 28;
                    break;
                case 6:
                    zöld_simulated = 13;
                    makk_simulated = 21;
                    tök_simulated = 29;
                    break;
                case 7:
                    zöld_simulated = 14;
                    makk_simulated = 22;
                    tök_simulated = 30;
                    break;
                case 8:
                    zöld_simulated = 15;
                    makk_simulated = 23;
                    tök_simulated = 31;
                    break;
                case 9:
                    piros_simulated = 8;
                    makk_simulated = 16;
                    tök_simulated = 32;
                    break;
                case 10:
                    piros_simulated = 1;
                    makk_simulated = 17;
                    tök_simulated = 25;
                    break;
                case 11:
                    piros_simulated = 2;
                    makk_simulated = 18;
                    tök_simulated = 26;
                    break;
                case 12:
                    piros_simulated = 3;
                    makk_simulated = 19;
                    tök_simulated = 27;
                    break;
                case 13:
                    piros_simulated = 4;
                    makk_simulated = 20;
                    tök_simulated = 28;
                    break;
                case 14:
                    piros_simulated = 5;
                    makk_simulated = 21;
                    tök_simulated = 29;
                    break;
                case 15:
                    piros_simulated = 6;
                    makk_simulated = 22;
                    tök_simulated = 30;
                    break;
                case 16:
                    piros_simulated = 7;
                    makk_simulated = 23;
                    tök_simulated = 31;
                    break;
                case 17:
                    piros_simulated = 8;
                    zöld_simulated = 16;
                    tök_simulated = 32;
                    break;
                case 18:
                    piros_simulated = 1;
                    zöld_simulated = 9;
                    tök_simulated = 25;
                    break;
                case 19:
                    piros_simulated = 2;
                    zöld_simulated = 10;
                    tök_simulated = 26;
                    break;
                case 20:
                    piros_simulated = 3;
                    zöld_simulated = 11;
                    tök_simulated = 27;
                    break;
                case 21:
                    piros_simulated = 4;
                    zöld_simulated = 12;
                    tök_simulated = 28;
                    break;
                case 22:
                    piros_simulated = 5;
                    zöld_simulated = 13;
                    tök_simulated = 29;
                    break;
                case 23:
                    piros_simulated = 6;
                    zöld_simulated = 14;
                    tök_simulated = 30;
                    break;
                case 24:
                    piros_simulated = 7;
                    zöld_simulated = 15;
                    tök_simulated = 31;
                    break;
                case 25:
                    piros_simulated = 8;
                    zöld_simulated = 16;
                    makk_simulated = 24;
                    break;
                case 26:
                    piros_simulated = 1;
                    zöld_simulated = 9;
                    makk_simulated = 17;
                    break;
                case 27:
                    piros_simulated = 2;
                    zöld_simulated = 10;
                    makk_simulated = 18;
                    break;
                case 28:
                    piros_simulated = 3;
                    zöld_simulated = 11;
                    makk_simulated = 19;
                    break;
                case 29:
                    piros_simulated = 4;
                    zöld_simulated = 12;
                    makk_simulated = 20;
                    break;
                case 30:
                    piros_simulated = 5;
                    zöld_simulated = 13;
                    makk_simulated = 21;
                    break;
                case 31:
                    piros_simulated = 6;
                    zöld_simulated = 14;
                    makk_simulated = 22;
                    break;
                case 32:
                    piros_simulated = 7;
                    zöld_simulated = 15;
                    makk_simulated = 23;
                    break;
            }
        }

        public static bool KöverkezőLap_sim(int kártyaszám)
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
                    if (piros_simulated == 0) return true;
                    if ((piros_simulated + 1 == kártyaszám) & (kártyaszám != 8)) return true;
                    if ((kártyaszám == 1) & (piros_simulated == 8)) return true;
                    if ((piros_simulated == 7) & (kártyaszám == 8)) return true;
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    if (zöld_simulated == 0) return true;
                    if ((zöld_simulated + 1 == kártyaszám) & (kártyaszám != 16)) return true;
                    if ((kártyaszám == 9) & (zöld_simulated == 16)) return true;
                    if ((zöld_simulated == 15) & (kártyaszám == 16)) return true;
                    break;
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                    if (makk_simulated == 0) return true;
                    if ((makk_simulated + 1 == kártyaszám) & (kártyaszám != 24)) return true;
                    if ((kártyaszám == 17) & (makk_simulated == 24)) return true;
                    if ((makk_simulated == 23) & (kártyaszám == 24)) return true;
                    break;
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                    if (tök_simulated == 0) return true;
                    if ((tök_simulated + 1 == kártyaszám) & (kártyaszám != 32)) return true;
                    if ((kártyaszám == 25) & (tök_simulated == 32)) return true;
                    if ((tök_simulated == 31) & (kártyaszám == 32)) return true;
                    break;
            }

            return false;
        }

        public static byte Segítség()
        {
            byte max = 0;
            byte index = 0;
            const byte játékos = 1;
            Válaszok = JátékSzimulálás(játékos, Main.Player1CardId[1]).Split(' ');
            eredmény[1].győztes = Convert.ToByte(Válaszok[0]);
            eredmény[1].nyeremény = Convert.ToByte(Válaszok[1]);
            Válaszok = JátékSzimulálás(játékos, Main.Player1CardId[2]).Split(' ');
            eredmény[2].győztes = Convert.ToByte(Válaszok[0]);
            eredmény[2].nyeremény = Convert.ToByte(Válaszok[1]);
            Válaszok = JátékSzimulálás(játékos, Main.Player1CardId[3]).Split(' ');
            eredmény[3].győztes = Convert.ToByte(Válaszok[0]);
            eredmény[3].nyeremény = Convert.ToByte(Válaszok[1]);
            Válaszok = JátékSzimulálás(játékos, Main.Player1CardId[4]).Split(' ');
            eredmény[4].győztes = Convert.ToByte(Válaszok[0]);
            eredmény[4].nyeremény = Convert.ToByte(Válaszok[1]);
            Válaszok = JátékSzimulálás(játékos, Main.Player1CardId[5]).Split(' ');
            eredmény[5].győztes = Convert.ToByte(Válaszok[0]);
            eredmény[5].nyeremény = Convert.ToByte(Válaszok[1]);
            Válaszok = JátékSzimulálás(játékos, Main.Player1CardId[6]).Split(' ');
            eredmény[6].győztes = Convert.ToByte(Válaszok[0]);
            eredmény[6].nyeremény = Convert.ToByte(Válaszok[1]);
            Válaszok = JátékSzimulálás(játékos, Main.Player1CardId[7]).Split(' ');
            eredmény[7].győztes = Convert.ToByte(Válaszok[0]);
            eredmény[7].nyeremény = Convert.ToByte(Válaszok[1]);
            Válaszok = JátékSzimulálás(játékos, Main.Player1CardId[8]).Split(' ');
            eredmény[8].győztes = Convert.ToByte(Válaszok[0]);
            eredmény[8].nyeremény = Convert.ToByte(Válaszok[1]);
            for (byte i = 1; i <= 8; i++)
                if (eredmény[i].győztes == játékos)
                {
                    if (max == 0)
                    {
                        index = i;
                        max = Convert.ToByte(eredmény[i].nyeremény);
                    }
                    else
                    {
                        if (max < eredmény[i].nyeremény)
                        {
                            index = i;
                            max = Convert.ToByte(eredmény[i].nyeremény);
                        }
                    }
                }

            return Convert.ToByte(Main.Player1CardId[index]);
        }

        public struct Eredmény
        {
            public int győztes;
            public int nyeremény;
        }
    }
}