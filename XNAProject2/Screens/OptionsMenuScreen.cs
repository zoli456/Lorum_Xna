#region File Description

//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion

#region Using Statements

using System.IO;
using GameStateManagement;
using Lórum.Content;
using Lórum.Game;
using Microsoft.Xna.Framework;

#endregion

namespace Lórum.Screens
{
    /// <summary>
    ///     The options screen is brought up over the top of the main menu
    ///     screen, and gives the user a chance to configure the game
    ///     in various hopefully useful ways.
    /// </summary>
    internal class OptionsMenuScreen : MenuScreen
    {
        #region Fields

        private static Ungulate currentUngulate = Ungulate.Dromedary;

        private static readonly string[] languages = { "C#", "French", "Deoxyribonucleic acid" };
        private static int currentLanguage;

        public static bool fullscreene = true;
        public static bool hardmode = true;
        public static int pakli = 1;
        public static bool randomStartingplayer = true;
        public static bool WinnerStartingplayer;
        private readonly MenuEntry RandomStartingPlayerEntry;
        private readonly MenuEntry WinnerStartingplayerEntry;

        private readonly MenuEntry fullscreeneMenuEntry;
        private readonly MenuEntry hardmodeMenuEntry;
        private readonly MenuEntry pakliMenuEntry;

        private enum Ungulate
        {
/*
            BactrianCamel,
*/
            Dromedary,
            Llama
        }

        #endregion

        #region Initialization

        /// <summary>
        ///     Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("Beállítások")
        {
            // Create our menu entries.
            fullscreeneMenuEntry = new MenuEntry(string.Empty);
            pakliMenuEntry = new MenuEntry(string.Empty);
            hardmodeMenuEntry = new MenuEntry(string.Empty);
            RandomStartingPlayerEntry = new MenuEntry(string.Empty);
            WinnerStartingplayerEntry = new MenuEntry(string.Empty);
            // ungulateMenuEntry = new MenuEntry(string.Empty);
            //  languageMenuEntry = new MenuEntry(string.Empty);
            //  elfMenuEntry = new MenuEntry(string.Empty);

            SetMenuEntryText();

            var back = new MenuEntry("Vissza");

            // Hook up menu event handlers.
            fullscreeneMenuEntry.Selected += fullscreeneMenuEntrySelected;
            pakliMenuEntry.Selected += pakliMenuEntrySelected;
            hardmodeMenuEntry.Selected += HardmodeMenuEntrySelected;
            RandomStartingPlayerEntry.Selected += RandomStartingPlayerSelected;
            WinnerStartingplayerEntry.Selected += WinnerStartingPlayerSelected;
            // ungulateMenuEntry.Selected += UngulateMenuEntrySelected;
            // languageMenuEntry.Selected += LanguageMenuEntrySelected;  
            //  elfMenuEntry.Selected += ElfMenuEntrySelected;
            back.Selected += OnCancel;

            // Menüpontok hozzáadása
            MenuEntries.Add(fullscreeneMenuEntry);
            MenuEntries.Add(pakliMenuEntry);
            MenuEntries.Add(hardmodeMenuEntry);
            MenuEntries.Add(RandomStartingPlayerEntry);
            MenuEntries.Add(WinnerStartingplayerEntry);
            //    MenuEntries.Add(ungulateMenuEntry);
            //   MenuEntries.Add(languageMenuEntry);      
            //   MenuEntries.Add(elfMenuEntry);
            MenuEntries.Add(back);
        }

        protected override void OnCancel(PlayerIndex playerIndex)
        {
            // make sure the device is ready
            if (Global.SaveDevice.IsReady)
                // save a file asynchronously. this will trigger IsBusy to return true
                // for the duration of the save process.
                Global.SaveDevice.SaveAsync(
                    Global.ContainerName,
                    Global.FileNameOptions,
                    stream =>
                    {
                        using (var writer = new StreamWriter(stream))
                        {
                            writer.WriteLine(Encryptor.Encrypt(fullscreene.ToString()));
                            writer.WriteLine(Encryptor.Encrypt(hardmode.ToString()));
                            writer.WriteLine(Encryptor.Encrypt(pakli.ToString()));
                            writer.WriteLine(Encryptor.Encrypt(randomStartingplayer.ToString()));
                            writer.WriteLine(Encryptor.Encrypt(WinnerStartingplayer.ToString()));
                            writer.WriteLine(LórumGame.Graphics.PreferredBackBufferWidth.ToString());
                            writer.WriteLine(LórumGame.Graphics.PreferredBackBufferHeight.ToString());
                        }
                    });
            base.OnCancel(playerIndex);
        }

        /// <summary>
        ///     Fills in the latest values for the options screen menu text.
        /// </summary>
        private void SetMenuEntryText()
        {
            fullscreeneMenuEntry.Text = "Teljesképernyő: " + (fullscreene ? "nem" : "igen");
            pakliMenuEntry.Text = "Pakli: " + pakli; //Teljes képernyo
            hardmodeMenuEntry.Text = "Normál mód: " + (hardmode ? "igen" : "nem");
            RandomStartingPlayerEntry.Text = "Véletlen kezdojátékos: " + (randomStartingplayer ? "igen" : "nem");
            WinnerStartingplayerEntry.Text = "Gyoztes a kezdojátékos: " + (WinnerStartingplayer ? "igen" : "nem");
            // ungulateMenuEntry.Text = "Preferred ungulate: " + currentUngulate;
            //    languageMenuEntry.Text = "Language: " + languages[currentLanguage];

            //    elfMenuEntry.Text = "elf: " + elf;
        }

        #endregion

        #region Handle Input

/*
        /// <summary>
        /// Event handler for when the Ungulate menu entry is selected.
        /// </summary>
        private void UngulateMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            currentUngulate++;

            if (currentUngulate > Ungulate.Llama)
                currentUngulate = 0;

            SetMenuEntryText();
        }
*/


/*
        /// <summary>
        /// Event handler for when the Language menu entry is selected.
        /// </summary>
        private void LanguageMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            currentLanguage = (currentLanguage + 1)%languages.Length;

            SetMenuEntryText();
        }
*/

        private void HardmodeMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            hardmode = !hardmode;
            SetMenuEntryText();
        }

        private void WinnerStartingPlayerSelected(object sender, PlayerIndexEventArgs e)
        {
            WinnerStartingplayer = !WinnerStartingplayer;
            if (WinnerStartingplayer)
                randomStartingplayer = false;
            SetMenuEntryText();
        }

        private void RandomStartingPlayerSelected(object sender, PlayerIndexEventArgs e)
        {
            randomStartingplayer = !randomStartingplayer;
            if (randomStartingplayer)
                WinnerStartingplayer = false;
            SetMenuEntryText();
        }

        /// <summary>
        ///     Event handler for when the fullscreene menu entry is selected.
        /// </summary>
        private void fullscreeneMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            fullscreene = !fullscreene;
            if (!fullscreene)
            {
                LórumGame.Graphics.IsFullScreen = true;
                LórumGame.Graphics.ApplyChanges();
            }
            else
            {
                LórumGame.Graphics.IsFullScreen = false;
                LórumGame.Graphics.ApplyChanges();
            }

            SetMenuEntryText();
        }

        private void pakliMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            pakli++;
            if (pakli > 3)
                pakli = 1;
            Main.Pakli = pakli;
            SetMenuEntryText();
        }

        #endregion
    }
}