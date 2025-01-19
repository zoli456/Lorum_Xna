using System.IO;
using EasyStorage;
using GameStateManagement;
using Lórum.Content;
using Lórum.Game;

namespace Lórum.Screens
{
    internal class PressStartScreen : MenuScreen
    {
        private IAsyncSaveDevice saveDevice;

        public PressStartScreen()
            : base("")
        {
            var startMenuEntry = new MenuEntry("Nyomd meg az ENTER gombot!");
            startMenuEntry.Selected += StartMenuEntrySelected;
            MenuEntries.Add(startMenuEntry);
        }

        private void StartMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            PromptMe();
        }

        private void PromptMe()
        {
            // we can set our supported languages explicitly or we can allow the
            // game to support all the languages. the first language given will
            // be the default if the current language is not one of the supported
            // languages. this only affects the text found in message boxes shown
            // by EasyStorage and does not have any affect on the rest of the game.
            EasyStorageSettings.SetSupportedLanguages(Language.Magyar, Language.English);
            // on Windows Phone we use a save device that uses IsolatedStorage
            // on Windows and Xbox 360, we use a save device that gets a
            //shared StorageDevice to handle our file IO.
#if WINDOWS_PHONE
            saveDevice = new IsolatedStorageSaveDevice();
            Global.SaveDevice = saveDevice;
            // we use the tap gesture for input on the phone
            TouchPanel.EnabledGestures = GestureType.Tap;
#else
            // create and add our SaveDevice
            var sharedSaveDevice = new SharedSaveDevice();
            ScreenManager.Game.Components.Add(sharedSaveDevice);
            // make sure we hold on to the device
            saveDevice = sharedSaveDevice;
            // hook two event handlers to force the user to choose a new device if they cancel the
            // device selector or if they disconnect the storage device after selecting it
            sharedSaveDevice.DeviceSelectorCanceled +=
                (s, e) => e.Response = SaveDeviceEventResponse.Force;
            sharedSaveDevice.DeviceDisconnected +=
                (s, e) => e.Response = SaveDeviceEventResponse.Force;
            // prompt for a device on the first Update we can
            sharedSaveDevice.PromptForDevice();
            sharedSaveDevice.DeviceSelected += (s, e) =>
            {
                //Save our save device to the global counterpart, so we can access it
                //anywhere we want to save/load
                Global.SaveDevice = (SaveDevice)s;
                //Once they select a storage device, we can load the main menu.
                //You'll notice I hard coded PlayerIndex.One here. You'll need to
                //change that if you plan on releasing your game. I linked to an
                //example on how to do that but here's the link if you need it.
                //http://blog.nickgravelyn.com/2009/03/basic-handling-of-multiple-controllers/
                //We need to perform a check to see if we're on the Press Start Screen.
                //If a storage device is selected NOT from this page, we don't want to
                //create a new Main Menu screen! (Thanks @FreelanceGames for the mention)
                if (IsActive)
                    ScreenManager.AddScreen(new BackgroundScreen(), null);
                ScreenManager.AddScreen(new MainMenuScreen(), null);
                if (Global.SaveDevice.FileExists(Global.ContainerName,
                        Global.FileNameOptions))
                    Global.SaveDevice.Load(
                        Global.ContainerName,
                        Global.FileNameOptions,
                        stream =>
                        {
                            using (var reader = new StreamReader(stream))
                            {
                                OptionsMenuScreen.fullscreene =
                                    bool.Parse(
                                        Encryptor.Decrypt(reader.ReadLine()));
                                OptionsMenuScreen.hardmode =
                                    bool.Parse(
                                        Encryptor.Decrypt(reader.ReadLine()));
                                OptionsMenuScreen.pakli =
                                    int.Parse(
                                        Encryptor.Decrypt(reader.ReadLine()));
                                Main.Pakli = OptionsMenuScreen.pakli;
                                OptionsMenuScreen.randomStartingplayer =
                                    bool.Parse(
                                        Encryptor.Decrypt(reader.ReadLine()));
                                OptionsMenuScreen.WinnerStartingplayer =
                                    bool.Parse(
                                        Encryptor.Decrypt(reader.ReadLine()));
                                LórumGame.Graphics.PreferredBackBufferWidth =
                                    int.Parse(reader.ReadLine());

                                LórumGame.Graphics.PreferredBackBufferHeight
                                    =
                                    int.Parse(reader.ReadLine());
                            }
                        });
                if (Global.SaveDevice.FileExists(Global.ContainerName,
                        Global.fileName_awards))
                    Global.SaveDevice.Load(
                        Global.ContainerName,
                        Global.fileName_awards,
                        stream =>
                        {
                            using (var reader = new StreamReader(stream))
                            {
                                Main.wins =
                                    int.Parse(
                                        Encryptor.Decrypt(reader.ReadLine()));
                                Main.Loses =
                                    int.Parse(
                                        Encryptor.Decrypt(reader.ReadLine()));
                                Main.Lorums =
                                    int.Parse(
                                        Encryptor.Decrypt(reader.ReadLine()));
                                Main.dead1 =
                                    int.Parse(
                                        Encryptor.Decrypt(reader.ReadLine()));
                                Main.dead2 =
                                    int.Parse(
                                        Encryptor.Decrypt(reader.ReadLine()));
                                Main.PointsWin =
                                    int.Parse(
                                        Encryptor.Decrypt(reader.ReadLine()));
                                Main.PointsLose =
                                    int.Parse(
                                        Encryptor.Decrypt(reader.ReadLine()));
                            }
                        });
                if (!OptionsMenuScreen.fullscreene)
                    LórumGame.Graphics.IsFullScreen = true;
                if (LórumGame.Graphics.PreferredBackBufferHeight == 0 &&
                    LórumGame.Graphics.PreferredBackBufferWidth == 0)
                {
                    LórumGame.Graphics.PreferredBackBufferWidth = 1024;
                    LórumGame.Graphics.PreferredBackBufferHeight = 768;
                }

                LórumGame.scale =
                    (float)LórumGame.Graphics.PreferredBackBufferWidth / 800;
                LórumGame.scale2 =
                    (float)LórumGame.Graphics.PreferredBackBufferHeight / 600;
                LórumGame.Graphics.ApplyChanges();
            };
#endif
#if XBOX
    // add the GamerServicesComponent
            ScreenManager.Game.Components.Add(
                new Microsoft.Xna.Framework.GamerServices.GamerServicesComponent(ScreenManager.Game));
#endif
        }
    }
}