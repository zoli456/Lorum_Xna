#region CPL License

/*
Nuclex Framework
Copyright (C) 2002-2011 Nuclex Development Labs

This library is free software; you can redistribute it and/or
modify it under the terms of the IBM Common Public License as
published by the IBM Corporation; either version 1.0 of the
License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
IBM Common Public License for more details.

You should have received a copy of the IBM Common Public
License along with this library
*/

#endregion

using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Nuclex.Game.Content
{
    /// <summary>Manages global content that is shared within a game</summary>
    /// <remarks>
    ///     This class allows your game to create a custom type of content manager
    ///     (for example, the LzmaContentManager) and share is globally throughout
    ///     your game through the ISharedContentService.
    /// </remarks>
    public class SharedContentManager : Component, ISharedContentService, IDisposable
    {
        /// <summary>Content manager containing the shared assets of the game</summary>
        private ContentManager contentManager;

        /// <summary>
        ///     Game service container the shared content manager is registered to
        /// </summary>
        private GameServiceContainer serviceContainer;

        /// <summary>Initializes a new shared content manager</summary>
        /// <param name="gameServices">
        ///     Game service container to use for accessing other game components
        ///     like the graphics device service. The shared content manager also
        ///     registers itself (under <see cref="ISharedContentService" />) herein.
        /// </param>
        public SharedContentManager(GameServiceContainer gameServices)
        {
            serviceContainer = gameServices;

            // Register ourselfes as the shared content service for the game to allow
            // other component to access the assets managed by us.
            gameServices.AddService(typeof(ISharedContentService), this);
        }

        #region IDisposable Members

        /// <summary>Immediately releases all resources and unregisters the component</summary>
        public virtual void Dispose()
        {
            // Unregister the service if we registered it to the game service container
            if (serviceContainer != null)
            {
                var registeredService = serviceContainer.GetService(
                    typeof(ISharedContentService)
                );
                if (ReferenceEquals(registeredService, this))
                    serviceContainer.RemoveService(typeof(ISharedContentService));
                serviceContainer = null;
            }

            // Release all content stored in the content manager
            if (contentManager != null)
            {
                contentManager.Dispose();
                contentManager = null;
            }
        }

        #endregion

        #region ISharedContentService Members

        /// <summary>Loads or accesses shared game content</summary>
        /// <typeparam name="AssetType">Type of the asset to be loaded or accessed</typeparam>
        /// <param name="assetName">Path and name of the requested asset</param>
        /// <returns>The requested asset from the the shared game content store</returns>
        public AssetType Load<AssetType>(string assetName)
        {
            if (contentManager == null)
                throw new InvalidOperationException(
                    "Cannot load asset: shared content manager not initialized"
                );
            return contentManager.Load<AssetType>(assetName);
        }

        #endregion

        /// <summary>
        ///     Allows the game component to perform any initialization it needs to before
        ///     starting to run. This is where it can query for any required services and
        ///     load content.
        /// </summary>
        public void Initialize()
        {
            // Create a new content manager for the graphics device that just came up
            contentManager = CreateContentManager();
        }

        /// <summary>Creates a new content manager for the shared content provider</summary>
        /// <returns>The newly created content manager</returns>
        /// <remarks>
        ///     Override this method in a derived to call another constructor of the content
        ///     manager or to use a custom content manager implementation if you need to.
        /// </remarks>
        protected virtual ContentManager CreateContentManager()
        {
            return new ContentManager(serviceContainer, "Content");
        }

        /// <summary>Unloads all resources from the shared content manager</summary>
        public void Unload()
        {
            // Unload all content in the content manager to release the graphics device
            // resources again before the device is destroyed
            contentManager.Unload();
        }
    }
}

// namespace Nuclex.Game.Content