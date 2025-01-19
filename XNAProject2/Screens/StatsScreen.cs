#region File Description

//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion

#region Using Statements

using GameStateManagement;

#endregion

namespace Lórum.Screens
{
    /// <summary>
    ///     The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    internal class StatsScreen : MenuScreen
    {
        #region Initialization

        public static decimal winslose;

        /// <summary>
        ///     Constructor fills in the menu contents.
        /// </summary>
        public StatsScreen()
            : base("Statisztikák")
        {
            string rate;
            if (Main.Loses != 0)
                winslose = (decimal)Main.wins / Main.Loses;
            // Create our menu entries.
            var ResetEntry = new MenuEntry("Eredmények törlése");

            var LoseEntry = new MenuEntry("Vereségek: " + Main.Loses);
            if (Main.Loses == 0)
                rate = "Gyozelem/Vereség arány: " + Main.wins;
            else
                rate = "Gyozelem/Vereség arány: " + winslose.ToString("N2");
            var WinsEntry = new MenuEntry("Gyozelmek: " + Main.wins);
            var WinLoseRateEntry = new MenuEntry(rate);
            var LorumEntry = new MenuEntry("Lórumok száma: " + Main.Lorums);
            var DefeatedEntry = new MenuEntry("Legyozött játékosok: " + Main.dead1);
            var DefeatedIEntry = new MenuEntry("Kiütve: " + Main.dead2);
            var WinPointsIEntry = new MenuEntry("Megszerzett pontok: " + Main.PointsWin);
            var LosePointsIEntry = new MenuEntry("Elvesztett pontok: " + Main.PointsLose);
            var exitMenuEntry = new MenuEntry("Vissza");
            // Hook up menu event handlers.
            ResetEntry.Selected += ResetQuestion;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(WinsEntry);
            MenuEntries.Add(LoseEntry);
            MenuEntries.Add(WinLoseRateEntry);
            MenuEntries.Add(LorumEntry);
            MenuEntries.Add(DefeatedEntry);
            MenuEntries.Add(DefeatedIEntry);
            MenuEntries.Add(WinPointsIEntry);
            MenuEntries.Add(LosePointsIEntry);
            MenuEntries.Add(ResetEntry);
            MenuEntries.Add(exitMenuEntry);
        }

        #endregion

        #region Handle Input

/*
        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        private void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameTable());
        }
*/


/*
        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        private void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }
*/


/*
        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        private void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }
*/

        private void ResetQuestion(object sender, PlayerIndexEventArgs playerIndexEventArgs)
        {
            const string message = "Biztosan szeretnéd a statisztikák törlését?";
            var confirmQuitMessageBox = new MessageBoxScreen(message);
            confirmQuitMessageBox.Accepted += StatsReset;
            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }

        private void StatsReset(object sender, PlayerIndexEventArgs e)
        {
            Main.wins = 0; //Nullázás
            Main.Loses = 0;
            winslose = 0;
            Main.Lorums = 0;
            Main.dead1 = 0;
            Main.dead2 = 0;
            Main.PointsWin = 0;
            Main.PointsLose = 0; //Szövegek átírása
            Main.Statfrissítés();
            OnCancel(e.PlayerIndex);
        }

        #endregion
    }
}