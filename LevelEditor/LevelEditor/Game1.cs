using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Eternity;
using Microsoft.Xna.Framework.Storage;

namespace LevelEditor
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Saver saver;
        Loader loader;
        TerrainManager mgr;
        int xTiles, yTiles;
        KeyboardState oldState, newState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            saver = new Saver();
            loader = new Loader();

            Texture2D grass = Content.Load<Texture2D>("grass");
            Texture2D tree = Content.Load<Texture2D>("tree");
            Texture2D clear = Content.Load<Texture2D>("clear");
            mgr = TerrainManager.GetInstance();
            mgr.AddTexture(ref grass, "Grass");
            mgr.AddTexture(ref tree, "Tree");
            mgr.AddTexture(ref clear, "Empty");
            mgr.Init();
            xTiles = mgr.m_nodesLayer1.GetLength(0);
            yTiles = mgr.m_nodesLayer1.GetLength(1);

            saver.SaveTerrainToFile();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private bool Released(Keys k)
        {
            return oldState.IsKeyDown(k) && !newState.IsKeyDown(k);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            newState = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Released(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            if (Released(Keys.L))
                loader.LoadTerrain();

            base.Update(gameTime);
            oldState = newState;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            mgr.Draw(ref spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
