#region File Description

//-----------------------------------------------------------------------------
// PlayerIndexEventArgs.cs
//
// XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion

#region Using Statements

using System;
using Microsoft.Xna.Framework;

#endregion

namespace L�rum.Screens
{
    /// <summary>
    ///     Custom event argument which includes the index of the player who
    ///     triggered the event. This is used by the MenuEntry.Selected event.
    /// </summary>
    internal class PlayerIndexEventArgs : EventArgs
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        public PlayerIndexEventArgs(PlayerIndex playerIndex)
        {
            this.PlayerIndex = playerIndex;
        }


        /// <summary>
        ///     Gets the index of the player who triggered this event.
        /// </summary>
        public PlayerIndex PlayerIndex { get; }
    }
}