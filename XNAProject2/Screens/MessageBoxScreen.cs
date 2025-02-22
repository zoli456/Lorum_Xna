#region File Description

//-----------------------------------------------------------------------------
// MessageBoxScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion

#region Using Statements

using System;
using L�rum.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nuclex.Game.Content;

#endregion

namespace GameStateManagement
{
    /// <summary>
    ///     A popup message box screen, used to display "are you sure?"
    ///     confirmation messages.
    /// </summary>
    internal class MessageBoxScreen : GameScreen
    {
        #region Handle Input

        /// <summary>
        ///     Responds to user input, accepting or cancelling the message box.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            PlayerIndex playerIndex;

            // We pass in our ControllingPlayer, which may either be null (to
            // accept input from any player) or a specific index. If we pass a null
            // controlling player, the InputState helper returns to us which player
            // actually provided the input. We pass that through to our Accepted and
            // Cancelled events, so they can tell which player triggered them.
            if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
            {
                // Raise the accepted event, then exit the message box.
                if (Accepted != null)
                    Accepted(this, new PlayerIndexEventArgs(playerIndex));

                ExitScreen();
            }
            else if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
            {
                // Raise the cancelled event, then exit the message box.
                if (Cancelled != null)
                    Cancelled(this, new PlayerIndexEventArgs(playerIndex));

                ExitScreen();
            }
        }

        #endregion

        #region Draw

        /// <summary>
        ///     Draws the message box.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;
            var font = ScreenManager.Font;

            // Darken down any other screens that were drawn beneath the popup.
            ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);

            // Center the message text in the viewport.
            var viewport = ScreenManager.GraphicsDevice.Viewport;
            var viewportSize = new Vector2(viewport.Width, viewport.Height);
            var textSize = font.MeasureString(message);
            var textPosition = (viewportSize - textSize) / 2;

            // The background includes a border somewhat larger than the text itself.
            const int hPad = 32;
            const int vPad = 16;

            var backgroundRectangle = new Rectangle((int)textPosition.X - hPad,
                (int)textPosition.Y - vPad,
                (int)textSize.X + hPad * 2,
                (int)textSize.Y + vPad * 2);

            // Fade the popup alpha during transitions.
            var color = Color.White * TransitionAlpha;

            spriteBatch.Begin();

            // Draw the background rectangle.
            spriteBatch.Draw(gradientTexture, backgroundRectangle, color);

            // Draw the message box text.
            spriteBatch.DrawString(font, message, textPosition, color);

            spriteBatch.End();
        }

        #endregion

        #region Fields

        private readonly string message;
        private Texture2D gradientTexture;

        #endregion

        #region Events

        public event EventHandler<PlayerIndexEventArgs> Accepted;
        public event EventHandler<PlayerIndexEventArgs> Cancelled;

        #endregion

        #region Initialization

        /// <summary>
        ///     Constructor automatically includes the standard "A=ok, B=cancel"
        ///     usage text prompt.
        /// </summary>
        public MessageBoxScreen(string message)
            : this(message, true)
        {
        }


        /// <summary>
        ///     Constructor lets the caller specify whether to include the standard
        ///     "A=ok, B=cancel" usage text prompt.
        /// </summary>
        public MessageBoxScreen(string message, bool includeUsageText)
        {
            const string usageText = "\nA gomb, Space, Enter = ok" +
                                     "\nB gomb, Esc = m�gsem";

            if (includeUsageText)
                this.message = message + usageText;
            else
                this.message = message;

            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);
        }


        /// <summary>
        ///     Loads graphics content for this screen. This uses the shared ContentManager
        ///     provided by the Game class, so the content will remain loaded forever.
        ///     Whenever a subsequent MessageBoxScreen tries to load this same content,
        ///     it will just get back another reference to the already loaded data.
        /// </summary>
        public override void LoadContent()
        {
            //   ContentManager content = ScreenManager.Game.Content;
            var content = new LzmaContentManager(
                ScreenManager.Game.Services, "Main.pack");
            gradientTexture = content.Load<Texture2D>("Content/Images/gradient");
        }

        #endregion
    }
}