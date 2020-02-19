using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MotoresDeJogos
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        Random random;
        ConsoleWriter consoleWriter;
        ShipManager shipManager;
        InputManager inputManager;

        long initialMemory;
        bool retrieveInitialMemory;
        long lastMemMeasure;
        long mem;

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
            //camera = new Camera(new Vector3(0, 0, 500), graphics);
            random = new Random();
            ModedCamera.Initialize(graphics.GraphicsDevice);
            DebugShapeRenderer.Initialize(GraphicsDevice);
            MessageBus.Initialize();
            inputManager = new InputManager(this);
            shipManager = new ShipManager( random, Content );
            shipManager.Initialize();
            consoleWriter = new ConsoleWriter();
            retrieveInitialMemory = true;
            lastMemMeasure = 0;
            mem = 0;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            //spriteBatch = new SpriteBatch(GraphicsDevice);
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

            //camera.Update(gameTime);
            ModedCamera.Update(gameTime, graphics.GraphicsDevice);

            #region Garbage Collector Check
            if (retrieveInitialMemory)
            {
                initialMemory = GC.GetTotalMemory(false);
                retrieveInitialMemory = false;
            }

            mem = (GC.GetTotalMemory(false) - initialMemory);

            if (mem > 0 && mem > lastMemMeasure)
            {
                MessageBus.InsertNewMessage(new ConsoleMessage(String.Format("MEM ALERT: {0}k", (mem / 1000))));
            }

            lastMemMeasure = mem;
            #endregion

            inputManager.Update(gameTime);
            shipManager.Update(gameTime);
            consoleWriter.Update();
            MessageBus.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            shipManager.Draw();

            DebugShapeRenderer.Draw(gameTime, ModedCamera.View, ModedCamera.Projection);

            base.Draw(gameTime);
        }
    }
}
