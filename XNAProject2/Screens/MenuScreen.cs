#region File Description

//-----------------------------------------------------------------------------
// MenuScreen.cs
//
// XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion

#region Using Statements

using System;
using System.Collections.Generic;
using Lórum;
using Lórum.Game;
using Lórum.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace GameStateManagement
{
    /// <summary>
    ///     Base class for screens that contain a menu of options. The user can
    ///     move up and down to select an entry, or cancel to back out of the screen.
    /// </summary>
    internal class MenuScreen : GameScreen
    {
        #region Initialization

        /// <summary>
        ///     Constructor.
        /// </summary>
        protected MenuScreen(string menuTitle)
        {
            this.menuTitle = menuTitle;

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the list of menu entries, so derived classes can add
        ///     or change the menu contents.
        /// </summary>
        protected IList<MenuEntry> MenuEntries => menuEntries;

        #endregion

        #region Fields

        private readonly List<MenuEntry> menuEntries = new List<MenuEntry>();
        private readonly string menuTitle;
        private MouseState mouseStateCurrent, mouseStatePrevious;
        private Rectangle rectangle2;
        private int selectedEntry;

        #endregion

        #region Handle Input

        /// <summary>
        ///     Responds to user input, changing the selected entry and accepting
        ///     or cancelling the menu.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            mouseStateCurrent = Mouse.GetState();
            // Move to the previous menu entry?
            if (input.IsMenuUp(ControllingPlayer))
            {
                selectedEntry--;
                if (selectedEntry < 0)
                    selectedEntry = menuEntries.Count - 1;
            }

            // Move to the next menu entry?
            if (input.IsMenuDown(ControllingPlayer))
            {
                selectedEntry++;

                if (selectedEntry >= menuEntries.Count)
                    selectedEntry = 0;
            }

            // Accept or cancel the menu? We pass in our ControllingPlayer, which may
            // either be null (to accept input from any player) or a specific index.
            // If we pass a null controlling player, the InputState helper returns to
            // us which player actually provided the input. We pass that through to
            // OnSelectEntry and OnCancel, so they can tell which player triggered them.
            var playerIndex = PlayerIndex.One;
            rectangle2 = new Rectangle(mouseStateCurrent.X, mouseStateCurrent.Y, 1, 1);
            for (var i = 0; i < menuEntries.Count; i++)
                if (rectangle2.Intersects(menuEntries[i].rectangle))
                    if (mouseStateCurrent.LeftButton == ButtonState.Pressed &&
                        mouseStatePrevious.LeftButton == ButtonState.Released)
                        menuEntries[i].OnSelectEntry(playerIndex);
                    else
                        selectedEntry = i;
            mouseStatePrevious = mouseStateCurrent;
            if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
                OnSelectEntry(selectedEntry, playerIndex);
            else if (input.IsMenuCancel(ControllingPlayer, out playerIndex)) OnCancel(playerIndex);
        }


        /// <summary>
        ///     Handler for when the user has chosen a menu entry.
        /// </summary>
        protected virtual void OnSelectEntry(int entryIndex, PlayerIndex playerIndex)
        {
            menuEntries[entryIndex].OnSelectEntry(playerIndex);
        }


        /// <summary>
        ///     Handler for when the user has cancelled the menu.
        /// </summary>
        protected virtual void OnCancel(PlayerIndex playerIndex)
        {
            ExitScreen();
        }

        protected void OnShowEnemyCards(object sender, PlayerIndexEventArgs e)
        {
            GameTable.VisitEnemies = true;
            ExitScreen();
        }

        /// <summary>
        ///     Helper overload makes it easy to use OnCancel as a MenuEntry event handler.
        /// </summary>
        protected void OnCancel(object sender, PlayerIndexEventArgs e)
        {
            OnCancel(e.PlayerIndex);
        }

        protected void OnDealPlayer2(object sender, PlayerIndexEventArgs e)
        {
            OnCancel(e.PlayerIndex);

            GameTable.SoundEffect[7].Play();
        }

        protected void OnDealPlayer3(object sender, PlayerIndexEventArgs e)
        {
            OnCancel(e.PlayerIndex);
        }

        protected void OnDealPlayer4(object sender, PlayerIndexEventArgs e)
        {
            OnCancel(e.PlayerIndex);
        }

        protected void OnTipp(object sender, PlayerIndexEventArgs e)
        {
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
                if (Main.KöverkezőLap(Main.Player1CardId[n]))
                    lehetSegesKartyak++;

            if (Main.Pontok1 - Main.SegítségÁr < 0)
            {
                GameTable.SoundEffect[1].Play();
                return;
            }

            OnCancel(e.PlayerIndex);
            Main.Pontok1 -= Main.SegítségÁr;
            if (lehetSegesKartyak == 0) return;
            for (n = 1; n <= i; n++)
                switch (Main.Player1CardId[n])
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
                if (!Main.KöverkezőLap(Main.Player1CardId[n])) continue;
                switch (Main.Player1CardId[n])
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        jelöltPiros = Main.Player1CardId[n];
                        break;
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                        jelöltZöld = Main.Player1CardId[n];
                        break;
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 23:
                    case 24:
                        jelöltMakk = Main.Player1CardId[n];
                        break;
                    case 25:
                    case 26:
                    case 27:
                    case 28:
                    case 29:
                    case 30:
                    case 31:
                    case 32:
                        jelöltTök = Main.Player1CardId[n];
                        break;
                }
            }

            if (Main.Piros == 0 && Main.Zöld == 0 && Main.Makk == 0 && Main.Tök == 0)
                calculatork = ComputerAI.Kezdés(pirosak, zöldek, makkok, tökök, jelöltPiros, jelöltZöld, jelöltMakk,
                    jelöltTök, Main.Player1CardId[1], Main.Player1CardId[2], Main.Player1CardId[3],
                    Main.Player1CardId[4], Main.Player1CardId[5], Main.Player1CardId[6], Main.Player1CardId[7],
                    Main.Player1CardId[8], 1);
            else
                calculatork = ComputerAI.Kezdés(pirosak, zöldek, makkok, tökök, jelöltPiros, jelöltZöld, jelöltMakk,
                    jelöltTök, Main.Player1CardId[1], Main.Player1CardId[2], Main.Player1CardId[3],
                    Main.Player1CardId[4], Main.Player1CardId[5], Main.Player1CardId[6], Main.Player1CardId[7],
                    Main.Player1CardId[8], 1);
            Main.helpcard = calculatork;
            Main.PointsLose += Main.SegítségÁr;
            Main.Statfrissítés();
            Main.SegítségÁr = 0;
            GameTable.showwish = true;
        }

        #endregion

        #region Update and Draw

        /// <summary>
        ///     Allows the screen the chance to position the menu entries. By default
        ///     all menu entries are lined up in a vertical list, centered on the screen.
        /// </summary>
        protected virtual void UpdateMenuEntryLocations()
        {
            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            var transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            // start at Y = 175; each X value is generated per entry
            var position = new Vector2(0f, 175f);

            // update each menu entry's location in turn
            foreach (var menuEntry in menuEntries)
            {
                // each entry is to be centered horizontally
                position.X = ScreenManager.GraphicsDevice.Viewport.Width / 2 - menuEntry.GetWidth(this) / 2;

                if (ScreenState == ScreenState.TransitionOn)
                    position.X -= transitionOffset * 256;
                else
                    position.X += transitionOffset * 512;

                // set the entry's position
                menuEntry.Position = position;

                // move down for the next entry the size of this entry
                position.Y += menuEntry.GetHeight(this);
            }
        }


        /// <summary>
        ///     Updates the menu.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
            bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            // Update each nested MenuEntry object.
            for (var i = 0; i < menuEntries.Count; i++)
            {
                var isSelected = IsActive && i == selectedEntry;

                menuEntries[i].Update(this, isSelected, gameTime);
            }
        }


        /// <summary>
        ///     Draws the menu.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // make sure our entries are in the right place before we draw them
            UpdateMenuEntryLocations();

            var graphics = ScreenManager.GraphicsDevice;
            var spriteBatch = ScreenManager.SpriteBatch;
            var font = ScreenManager.Font;

            spriteBatch.Begin();

            // Draw each menu entry in turn.
            for (var i = 0; i < menuEntries.Count; i++)
            {
                var menuEntry = menuEntries[i];

                var isSelected = IsActive && i == selectedEntry;

                menuEntry.Draw(this, isSelected, gameTime);
            }

            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            var transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            // Draw the menu title centered on the screen
            var titlePosition = new Vector2(graphics.Viewport.Width / 2, 80);
            var titleOrigin = font.MeasureString(menuTitle) / 2;
            var titleColor = new Color(192, 192, 192) * TransitionAlpha;
            const float titleScale = 1.25f;

            titlePosition.Y -= transitionOffset * 100;

            spriteBatch.DrawString(font, menuTitle, titlePosition, titleColor, 0,
                titleOrigin, titleScale, SpriteEffects.None, 0);

            spriteBatch.End();
        }

        #endregion
    }
}