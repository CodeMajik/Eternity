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

namespace Eternity
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        const float camSpeed = 0.5f;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D floor;
        TerrainManager mgr;
        KeyboardState oldState, newState;
        Camera2d m_camera;
        Vector2 movement;
        int previousScroll, xTiles, yTiles;
        float zoomIncrement;
        Viewport vp;
        public static Entity player;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
            floor = Content.Load<Texture2D>("floor");
            mgr = TerrainManager.GetInstance();

            Texture2D grass = Content.Load<Texture2D>("grass");
            Texture2D tree = Content.Load<Texture2D>("tree");
            Texture2D clear = Content.Load<Texture2D>("clear");
            mgr.AddTexture(ref grass, "Grass");
            mgr.AddTexture(ref tree, "Tree");
            mgr.AddTexture(ref clear, "Empty");
            mgr.Init();

            xTiles = mgr.m_nodesLayer1.GetLength(0);
            yTiles = mgr.m_nodesLayer1.GetLength(1);

            m_camera = new Camera2d(GraphicsDevice.Viewport, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 1.0f, new Vector2(32.0f*(xTiles/2), 32.0f*(yTiles/2)));
            
            float left = mgr.m_nodesLayer1[0, 0].GetWCSLeftX() + (graphics.PreferredBackBufferWidth/2);
            float right = mgr.m_nodesLayer1[0, xTiles - 1].GetWCSRightX() - (graphics.PreferredBackBufferWidth / 2);
            float top = mgr.m_nodesLayer1[0, 0].GetWCSBotY() + (graphics.PreferredBackBufferHeight / 2);
            float bot = mgr.m_nodesLayer1[yTiles - 1, 0].GetWCSTopY() - (graphics.PreferredBackBufferHeight / 2);
            m_camera.InitBounds(left, right, bot, top);


            player = new Entity(new Vector2((xTiles / 2) * 32.0f, (yTiles / 2) * 32.0f), Vector2.Zero);
            Texture2D playertex = Content.Load<Texture2D>("player");
            player.SetTexture(ref playertex);

            previousScroll = 0;
            zoomIncrement = 0.07f;
            movement = Vector2.Zero;
            vp = GraphicsDevice.Viewport;
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

            CameraMovement();
            player.RunInput(newState);

            base.Update(gameTime);
            oldState = newState;
        }

        public void CameraMovement()
        {
            movement = Vector2.Zero;
            if (newState.IsKeyDown(Keys.A))
                movement.X -= camSpeed;
            if (newState.IsKeyDown(Keys.D))
                movement.X += camSpeed;
            if (newState.IsKeyDown(Keys.W))
                movement.Y -= camSpeed;
            if (newState.IsKeyDown(Keys.S))
                movement.Y += camSpeed;

            if (Mouse.GetState().ScrollWheelValue > previousScroll)
                m_camera.Zoom += zoomIncrement;
            else if (Mouse.GetState().ScrollWheelValue < previousScroll)
                m_camera.Zoom -= zoomIncrement;
            previousScroll = Mouse.GetState().ScrollWheelValue;
            m_camera.Pos = player.m_position;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred,
                    null, null, null, null, null,
                    m_camera.GetTransformation());
            mgr.Draw(ref spriteBatch);
           // player.Draw(ref spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
