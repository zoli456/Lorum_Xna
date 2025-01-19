﻿using EasyStorage;

namespace Lórum.Game
{
    public class Global
    {
        // A generic EasyStorage save device
        public static IAsyncSaveDevice SaveDevice;

        //We can set up different file names for different things we may save.
        //In this example we're going to save the items in the 'Options' menu.
        //I listed some other examples below but commented them out since we
        //don't need them. YOU CAN HAVE MULTIPLE OF THESE
        public static string FileNameOptions = "Lorum_Options";

        //public static string fileName_game = "YourGame_Game";
        public static string fileName_awards = "Lorum_Stats";

        //This is the name of the save file you'll find if you go into your memory
        //options on the Xbox. If you name it something like 'MyGameSave' then
        //people will have no idea what it's for and might delete your save.
        //YOU SHOULD ONLY HAVE ONE OF THESE
        public static string ContainerName = "Data";
    }
}