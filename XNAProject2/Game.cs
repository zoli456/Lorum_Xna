#region Using Statements

using System;
using GameStateManagement;
using Lórum.Screens;
using Microsoft.Xna.Framework;
using Nuclex.Game.Content;

#endregion

namespace Lórum
{
    /// <summary>
    ///     Sample showing how to manage different game states, with transitions
    ///     between menu screens, a loading screen, the game itself, and a pause
    ///     menu. This main game class is extremely simple: all the interesting
    ///     stuff happens in the ScreenManager component.
    /// </summary>
    public class LórumGame : Microsoft.Xna.Framework.Game
    {
        #region Draw

        /// <summary>
        ///     This is called when the game should draw itself.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            Graphics.GraphicsDevice.Clear(Color.Black);
            SetFrameRate(Graphics, 30);
            // The real drawing happens inside the screen manager component.
            base.Draw(gameTime);
        }

        #endregion

        #region Fields

        public static GraphicsDeviceManager Graphics;

        // By preloading any assets used by UI rendering, we avoid framerate glitches
        // when they suddenly need to be loaded in the middle of a menu transition.
        private static readonly string[] PreloadAssets =
        {
            "Content/Images/gradient"
        };

        public static int ResX = 1024;
        public static int ResY = 768;
        public static float scale = (float)ResX / 800;
        public static float scale2 = (float)ResY / 600;
        private readonly ScreenManager _screenManager;

        #endregion

        #region Initialization

        /// <summary>
        ///     The main game constructor.
        /// </summary>
        public LórumGame()
        {
            Content.RootDirectory = "Content";

            Graphics = new GraphicsDeviceManager(this);
            Window.Title = "Lórum";
            Graphics.PreferredBackBufferHeight = ResY;
            Graphics.PreferredBackBufferWidth = ResX;
            // Graphics.IsFullScreen = true;
            IsMouseVisible = true;
            Graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = true;
            Graphics.ApplyChanges();
            // Create the screen manager component.
            _screenManager = new ScreenManager(this);
            Components.Add(_screenManager);
            _screenManager.AddScreen(new PressStartScreen(), null);
        }

        public void SetFrameRate(
            GraphicsDeviceManager manager, int frames)
        {
            var dt = 1000 / (double)frames;
            // manager.SynchronizeWithVerticalRetrace = false;
            TargetElapsedTime = TimeSpan.FromMilliseconds(dt);
            // manager.ApplyChanges();
        }

        protected override void LoadContent()
        {
            var content = new LzmaContentManager(
                Services, "Main.pack", false);
            foreach (var asset in PreloadAssets) content.Load<object>(asset);
        }

        #endregion
    }

    #region Entry Point

    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    internal static class Program
    {
        private static void Main()
        {
            using (var game = new LórumGame())
            {
                game.Run();
            }
        }
    }

    #endregion
}