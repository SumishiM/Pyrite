﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite
{
    public interface IPyriteGame
    {/// <summary>
     /// Called once, when the executable for the game starts and initializes.
     /// </summary>
        public void Initialize() { }

        /// <summary>
        /// This loads all the content within the game. Called after <see cref="Initialize"/>.
        /// </summary>
        public Task LoadContentAsync() => Task.CompletedTask;

        /// <summary>
        /// Called after each update.
        /// </summary>
        public void OnUpdate() { }

        /// <summary>
        /// Called when a scene is unavailable due to loading of assets.
        /// Only assets at <see cref="GameDataManager.PreloadContent"/> are available.
        /// </summary>
        /// <param name="context">Borrows the RenderContext from the world (currently busy loading).</param>
        //public void OnLoadingDraw(RenderContext context) { }

        /// <summary>
        /// Called after each draw.
        /// </summary>
        public void OnDraw() { }

        /// <summary>
        /// Called before a scene transition.
        /// </summary>
        public void OnSceneTransition() { }

        /// <summary>
        /// Called once the game exits.
        /// </summary>
        public void OnExit() { }

        /// <summary>
        /// Creates save data for the game.
        /// </summary>
        //public SaveData CreateSaveData(string name) => new(name, Version);

        /// <summary>
        /// Creates the client custom sound player.
        /// </summary>
        //public ISoundPlayer CreateSoundPlayer() => new SoundPlayer();

        /// <summary>
        /// Creates a custom game profile for the game.
        /// </summary>
        //public GameProfile CreateGameProfile() => new();

        /// <summary>
        /// Creates a custom game preferences for the game.
        /// </summary>
        //public GamePreferences CreateGamePreferences() => new();

        /// <summary>
        /// Creates a custom render context for the game.
        /// </summary>
        //public RenderContext CreateRenderContext(GraphicsDevice graphicsDevice, Camera2D camera, RenderContextFlags settings) =>
        //    new(graphicsDevice, camera, settings);

        public bool HasCursor => true;

        /// <summary>
        /// This is the name of the game, used when creating assets and loading save data.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// This is the version of the game, used when checking for save compatibility.
        /// </summary>
        public float Version => 0;
    }
}
