using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MotoresDeJogos
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Ship> ships;
        Camera camera;
        Random random;
        ConsoleWriter consoleWriter;

        long initialMemory;
        bool retrieveInitialMemory;
        long lastMemMeasure;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.PreferMultiSampling = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
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
            camera = new Camera(new Vector3(0, 0, 500), graphics);
            random = new Random();
            DebugShapeRenderer.Initialize(GraphicsDevice);
            MessageBus.Initialize();
            consoleWriter = new ConsoleWriter();
            retrieveInitialMemory = true;
            lastMemMeasure = 0;
            ships = new List<Ship>(101);

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

            for (int i = 0; i <= 100; i++)
            {
                Ship ship = new Ship(new Vector3(random.Next(-5000, 5000), random.Next(-5000, 5000), random.Next(-5000, 5000)), Content, random);
                MessageBus.InsertNewMessage(new ConsoleMessage(String.Format("ID - {0} | Ship Z:{1}", i, ship.Position.Z)));
                Console.WriteLine();
                ships.Add(ship);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (Ship ship in ships)
            {
                ship.Update(gameTime);
            }
            consoleWriter.Update();
            MessageBus.Update();

            #region Garbage Collector Check
            if (retrieveInitialMemory)
            {
                initialMemory = GC.GetTotalMemory(false);
                retrieveInitialMemory = false;
            }

            long mem = (GC.GetTotalMemory(false) - initialMemory);

            if (mem > 0 && mem > lastMemMeasure)
            {
                MessageBus.InsertNewMessage(new ConsoleMessage(String.Format("MEM ALERT: {0}k", (mem / 1000))));
            }

            lastMemMeasure = mem;
            #endregion

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (Ship ship in ships)
            {
                ship.Draw(camera);
            }
            
            DebugShapeRenderer.Draw(gameTime, camera.View, camera.Projection);

            base.Draw(gameTime);
        }
    }
}
