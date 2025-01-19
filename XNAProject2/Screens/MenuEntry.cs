#region File Description

//-----------------------------------------------------------------------------
// MenuEntry.cs
//
// XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion

#region Using Statements

using System;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Lórum.Screens
{
    /// <summary>
    ///     Helper class represents a single entry in a MenuScreen. By default this
    ///     just draws the entry text string, but it can be customized to display menu
    ///     entries in different ways. This also provides an event that will be raised
    ///     when the menu entry is selected.
    /// </summary>
    internal class MenuEntry
    {
        #region Initialization

        /// <summary>
        ///     Constructs a new menu entry with the specified text.
        /// </summary>
        public MenuEntry(string text)
        {
            this.text = text;
        }

        #endregion

        #region Fields

        /// <summary>
        ///     The position at which the entry is drawn. This is set by the MenuScreen
        ///     each frame in Update.
        /// </summary>
        private Vector2 position;

        public Rectangle rectangle;

        /// <summary>
        ///     Tracks a fading selection effect on the entry.
        /// </summary>
        /// <remarks>
        ///     The entries transition out of the selection effect when they are deselected.
        /// </remarks>
        private float selectionFade;

        /// <summary>
        ///     The text rendered for this entry.
        /// </summary>
        private string text;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the text of this menu entry.
        /// </summary>
        public string Text
        {
            get => text;
            set => text = value;
        }


        /// <summary>
        ///     Gets or sets the position at which to draw this menu entry.
        /// </summary>
        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        #endregion

        #region Events

        /// <summary>
        ///     Event raised when the menu entry is selected.
        /// </summary>
        public event EventHandler<PlayerIndexEventArgs> Selected;


        /// <summary>
        ///     Method for raising the Selected event.
        /// </summary>
        protected internal virtual void OnSelectEntry(PlayerIndex playerIndex)
        {
            if (Selected != null)
                Selected(this, new PlayerIndexEventArgs(playerIndex));
        }

        #endregion

        #region Update and Draw

        /// <summary>
        ///     Updates the menu entry.
        /// </summary>
        public virtual void Update(MenuScreen screen, bool isSelected, GameTime gameTime)
        {
            // there is no such thing as a selected item on Windows Phone, so we always
            // force isSelected to be false
#if WINDOWS_PHONE
            isSelected = false;
#endif

            // When the menu selection changes, entries gradually fade between
            // their selected and deselected appearance, rather than instantly
            // popping to the new state.
            var fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;

            selectionFade =
                isSelected ? Math.Min(selectionFade + fadeSpeed, 1) : Math.Max(selectionFade - fadeSpeed, 0);
        }


        /// <summary>
        ///     Draws the menu entry. This can be overridden to customize the appearance.
        /// </summary>
        public virtual void Draw(MenuScreen screen, bool isSelected, GameTime gameTime)
        {
            // there is no such thing as a selected item on Windows Phone, so we always
            // force isSelected to be false
#if WINDOWS_PHONE
            isSelected = false;
#endif
            rectangle = new Rectangle((int)position.X - 1, (int)position.Y - 1, text.Length * 14 + 1, 15);
            //Bombok kiválasztó négyzete
            // Draw the selected entry in yellow, otherwise white.
            var color = isSelected ? Color.Yellow : Color.White;

            // Pulsate the size of the selected menu entry.
            var time = gameTime.TotalGameTime.TotalSeconds;

            var pulsate = (float)Math.Sin(time * 6) + 1;

            var scale = 1 + pulsate * 0.05f * selectionFade;

            // Modify the alpha to fade text out during transitions.
            color *= screen.TransitionAlpha;

            // Draw text, centered on the middle of each line.
            var screenManager = GameScreen.ScreenManager;
            var spriteBatch = ScreenManager.SpriteBatch;
            var font = screenManager.Font;

            var origin = new Vector2(0, font.LineSpacing / 2);

            spriteBatch.DrawString(font, text, position, color, 0,
                origin, scale, SpriteEffects.None, 0);
        }


        /// <summary>
        ///     Queries how much space this menu entry requires.
        /// </summary>
        public virtual int GetHeight(MenuScreen screen)
        {
            return GameScreen.ScreenManager.Font.LineSpacing;
        }


        /// <summary>
        ///     Queries how wide the entry is, used for centering on the screen.
        /// </summary>
        public virtual int GetWidth(MenuScreen screen)
        {
            return (int)GameScreen.ScreenManager.Font.MeasureString(Text).X;
        }

        #endregion
    }
}