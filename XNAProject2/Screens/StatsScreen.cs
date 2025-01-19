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

namespace L�rum.Screens
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
            : base("Statisztik�k")
        {
            string rate;
            if (Main.Loses != 0)
                winslose = (decimal)Main.wins / Main.Loses;
            // Create our menu entries.
            var ResetEntry = new MenuEntry("Eredm�nyek t�rl�se");

            var LoseEntry = new MenuEntry("Veres�gek: " + Main.Loses);
            if (Main.Loses == 0)
                rate = "Gyozelem/Veres�g ar�ny: " + Main.wins;
            else
                rate = "Gyozelem/Veres�g ar�ny: " + winslose.ToString("N2");
            var WinsEntry = new MenuEntry("Gyozelmek: " + Main.wins);
            var WinLoseRateEntry = new MenuEntry(rate);
            var LorumEntry = new MenuEntry("L�rumok sz�ma: " + Main.Lorums);
            var DefeatedEntry = new MenuEntry("Legyoz�tt j�t�kosok: " + Main.dead1);
            var DefeatedIEntry = new MenuEntry("Ki�tve: " + Main.dead2);
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
            const string message = "Biztosan szeretn�d a statisztik�k t�rl�s�t?";
            var confirmQuitMessageBox = new MessageBoxScreen(message);
            confirmQuitMessageBox.Accepted += StatsReset;
            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }

        private void StatsReset(object sender, PlayerIndexEventArgs e)
        {
            Main.wins = 0; //Null�z�s
            Main.Loses = 0;
            winslose = 0;
            Main.Lorums = 0;
            Main.dead1 = 0;
            Main.dead2 = 0;
            Main.PointsWin = 0;
            Main.PointsLose = 0; //Sz�vegek �t�r�sa
            Main.Statfriss�t�s();
            OnCancel(e.PlayerIndex);
        }

        #endregion
    }
}