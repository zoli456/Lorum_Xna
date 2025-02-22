#region File Description

//-----------------------------------------------------------------------------
// ScreenManager.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion

#region Using Statements

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Nuclex.Game.Content;

#endregion

namespace GameStateManagement
{
    /// <summary>
    ///     The screen manager is a component which manages one or more GameScreen
    ///     instances. It maintains a stack of screens, calls their Update and Draw
    ///     methods at the appropriate times, and automatically routes input to the
    ///     topmost active screen.
    /// </summary>
    public class ScreenManager : DrawableGameComponent
    {
        #region Fields

        private readonly InputState input = new InputState();
        private readonly List<GameScreen> screens = new List<GameScreen>();
        private readonly List<GameScreen> screensToUpdate = new List<GameScreen>();

        private Texture2D blankTexture;

        private bool isInitialized;

        #endregion

        #region Properties

        /// <summary>
        ///     A default SpriteBatch shared by all the screens. This saves
        ///     each screen having to bother creating their own local instance.
        /// </summary>
        public static SpriteBatch SpriteBatch { get; private set; }


        /// <summary>
        ///     A default font shared by all the screens. This saves
        ///     each screen having to bother loading their own local copy.
        /// </summary>
        public SpriteFont Font { get; private set; }


        /// <summary>
        ///     If true, the manager prints out a list of all the screens
        ///     each time it is updated. This can be useful for making sure
        ///     everything is being added and removed at the right times.
        /// </summary>
        public bool TraceEnabled { get; set; }

        #endregion

        #region Initialization

        /// <summary>
        ///     Constructs a new screen manager component.
        /// </summary>
        public ScreenManager(Game game)
            : base(game)
        {
            // we must set EnabledGestures before we can query for them, but
            // we don't assume the game wants to read them.
            TouchPanel.EnabledGestures = GestureType.None;
        }


        /// <summary>
        ///     Initializes the screen manager component.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            isInitialized = true;
        }


        /// <summary>
        ///     Load your graphics content.
        /// </summary>
        protected override void LoadContent()
        {
            // Load content belonging to the screen manager.
            // ContentManager content = Game.Content;
            var content = new LzmaContentManager(
                Game.Services, "Main.pack", true);
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Font = content.Load<SpriteFont>("Content/Fonts/Font1");
            blankTexture = content.Load<Texture2D>("Content/Images/blank");

            // Tell each of the screens to load their content.
            foreach (var screen in screens) screen.LoadContent();
        }


        /// <summary>
        ///     Unload your graphics content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Tell each of the screens to unload their content.
            foreach (var screen in screens) screen.UnloadContent();
        }

        #endregion

        #region Update and Draw

        /// <summary>
        ///     Allows each screen to run logic.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            // Read the keyboard and gamepad.
            input.Update();
            // Make a copy of the master screen list, to avoid confusion if
            // the process of updating one screen adds or removes others.
            screensToUpdate.Clear();

            foreach (var screen in screens)
                screensToUpdate.Add(screen);

            var otherScreenHasFocus = !Game.IsActive;
            var coveredByOtherScreen = false;

            // Loop as long as there are screens waiting to be updated.
            while (screensToUpdate.Count > 0)
            {
                // Pop the topmost screen off the waiting list.
                var screen = screensToUpdate[screensToUpdate.Count - 1];

                screensToUpdate.RemoveAt(screensToUpdate.Count - 1);

                // Update the screen.
                screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

                if (screen.ScreenState == ScreenState.TransitionOn ||
                    screen.ScreenState == ScreenState.Active)
                {
                    // If this is the first active screen we came across,
                    // give it a chance to handle input.
                    if (!otherScreenHasFocus)
                    {
                        screen.HandleInput(input);

                        otherScreenHasFocus = true;
                    }

                    // If this is an active non-popup, inform any subsequent
                    // screens that they are covered by it.
                    if (!screen.IsPopup)
                        coveredByOtherScreen = true;
                }
            }

            // Print debug trace?
        }


        /// <summary>
        ///     Prints a list of all the screens, for debugging.
        /// </summary>
        /// <summary>
        ///     Tells each screen to draw itself.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            foreach (var screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;

                screen.Draw(gameTime);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Adds a new screen to the screen manager.
        /// </summary>
        public void AddScreen(GameScreen screen, PlayerIndex? controllingPlayer)
        {
            screen.ControllingPlayer = controllingPlayer;
            GameScreen.ScreenManager = this;
            screen.IsExiting = false;

            // If we have a graphics device, tell the screen to load content.
            if (isInitialized) screen.LoadContent();

            screens.Add(screen);

            // update the TouchPanel to respond to gestures this screen is interested in
            TouchPanel.EnabledGestures = screen.EnabledGestures;
        }


        /// <summary>
        ///     Removes a screen from the screen manager. You should normally
        ///     use GameScreen.ExitScreen instead of calling this directly, so
        ///     the screen can gradually transition off rather than just being
        ///     instantly removed.
        /// </summary>
        public void RemoveScreen(GameScreen screen)
        {
            // If we have a graphics device, tell the screen to unload content.
            if (isInitialized) screen.UnloadContent();

            screens.Remove(screen);
            screensToUpdate.Remove(screen);

            // if there is a screen still in the manager, update TouchPanel
            // to respond to gestures that screen is interested in.
            if (screens.Count > 0) TouchPanel.EnabledGestures = screens[screens.Count - 1].EnabledGestures;
        }


        /// <summary>
        ///     Expose an array holding all the screens. We return a copy rather
        ///     than the real master list, because screens should only ever be added
        ///     or removed using the AddScreen and RemoveScreen methods.
        /// </summary>
        public GameScreen[] GetScreens()
        {
            return screens.ToArray();
        }


        /// <summary>
        ///     Helper draws a translucent black fullscreen sprite, used for fading
        ///     screens in and out, and for darkening the background behind popups.
        /// </summary>
        public void FadeBackBufferToBlack(float alpha)
        {
            var viewport = GraphicsDevice.Viewport;

            SpriteBatch.Begin();

            SpriteBatch.Draw(blankTexture,
                new Rectangle(0, 0, viewport.Width, viewport.Height),
                Color.Black * alpha);

            SpriteBatch.End();
        }

        #endregion
    }
}