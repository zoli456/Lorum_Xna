#region File Description

//-----------------------------------------------------------------------------
// BackgroundScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion

#region Using Statements

using System;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Nuclex.Game.Content;

#endregion

namespace Lórum.Screens
{
    /// <summary>
    ///     The background screen sits behind all the other menu screens.
    ///     It draws a background image that remains fixed in place regardless
    ///     of whatever transitions the screens on top of it may be doing.
    /// </summary>
    internal class BackgroundScreen : GameScreen
    {
        #region Fields

        private readonly Random random = new Random();
        private readonly Video[] video = new Video[6];
        private readonly int videonumber;
        private LzmaContentManager content;
        private VideoPlayer player;
        private Texture2D videoTexture;

        #endregion

        #region Initialization

        /// <summary>
        ///     Constructor.
        /// </summary>
        public BackgroundScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            videonumber = random.Next(1, 6);
        }


        /// <summary>
        ///     Loads graphics content for this screen. The background texture is quite
        ///     big, so we use our own local ContentManager to load it. This allows us
        ///     to unload before going from the menus into the game itself, wheras if we
        ///     used the shared ContentManager provided by the Game class, the content
        ///     would remain loaded forever.
        /// </summary>
        public override void LoadContent()
        {
            //  if (content == null)
            //      content = new ContentManager(ScreenManager.Game.Services, "Content");
            if (content == null)
                content = new LzmaContentManager(
                    ScreenManager.Game.Services, "Main.pack", true);
            switch (videonumber)
            {
                case 1:
                    video[1] = content.Load<Video>("Content/Videos/Background");
                    break;
                case 2:
                    video[2] = content.Load<Video>("Content/Videos/Menu2");
                    break;
                case 3:
                    video[3] = content.Load<Video>("Content/Videos/Menu3");
                    break;
                case 4:
                    video[4] = content.Load<Video>("Content/Videos/Menu4");
                    break;
                case 5:
                    video[5] = content.Load<Video>("Content/Videos/Background8");
                    break;
            }

            player = new VideoPlayer();
        }


        /// <summary>
        ///     Unloads graphics content for this screen.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }

        #endregion

        #region Update and Draw

        /// <summary>
        ///     Updates the background screen. Unlike most screens, this should not
        ///     transition off even if it has been covered by another screen: it is
        ///     supposed to be covered, after all! This overload forces the
        ///     coveredByOtherScreen parameter to false in order to stop the base
        ///     Update method wanting to transition off.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
            bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
            if (player.State == MediaState.Stopped)
            {
                player.IsLooped = true;
                player.Play(video[videonumber]);
            }
        }


        /// <summary>
        ///     Draws the background screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;
            var viewport = ScreenManager.GraphicsDevice.Viewport;
            var fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            if (player.State != MediaState.Stopped)
                videoTexture = player.GetTexture();

            if (videoTexture != null)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(videoTexture, fullscreen,
                    new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));
                spriteBatch.End();
            }
        }

        #endregion
    }
}