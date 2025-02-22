#region File Description

//-----------------------------------------------------------------------------
// PauseMenuScreen.cs
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
    ///     The pause menu comes up over the top of the game,
    ///     giving the player options to resume or quit.
    /// </summary>
    internal class PauseMenuScreen : MenuScreen
    {
        #region Initialization

        /// <summary>
        ///     Constructor.
        /// </summary>
        public PauseMenuScreen()
            : base("Sz�net")
        {
            // Create our menu entries.
            var resumeGameMenuEntry = new MenuEntry("Vissza a J�t�kba");
            var emenyCardsEntry = new MenuEntry("Ellenf�l k�rty�inak mutat�sa");
            var quitGameMenuEntry = new MenuEntry("Kil�p�s a Jat�kb�l");

            // Hook up menu event handlers.
            resumeGameMenuEntry.Selected += OnCancel;
            emenyCardsEntry.Selected += OnShowEnemyCards;
            quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;

            // Add entries to the menu.
            MenuEntries.Add(resumeGameMenuEntry);
            if (Main.J�t�k_befejezve && !GameTable.VisitEnemies) MenuEntries.Add(emenyCardsEntry);
            MenuEntries.Add(quitGameMenuEntry);
        }

        #endregion

        #region Handle Input

        /// <summary>
        ///     Event handler for when the Quit Game menu entry is selected.
        /// </summary>
        private void QuitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Szeretn�d elhagyni a j�t�kot?";

            var confirmQuitMessageBox = new MessageBoxScreen(message);

            confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }


        /// <summary>
        ///     Event handler for when the user selects ok on the "are you sure
        ///     you want to quit" message box. This uses the loading screen to
        ///     transition from the game back to the main menu screen.
        /// </summary>
        private void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                new MainMenuScreen());
        }

        #endregion
    }
}