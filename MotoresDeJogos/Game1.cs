using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MotoresDeJogos.Char;
using MotoresDeJogos.Managers;
using MotoresDeJogos.Projectiles;
using MotoresDeJogos.World;
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
        DuckManager duckManager;
        InputManager inputManager;
        InputHandler inputHandler;
        WorldGeneration worldGeneration;
        public static GameStates gameState = GameStates.Play;
        SkyBox skyBox;
        Controller controller;

        float deltaTime = 0;

        #region Memory Variables
        long initialMemory;
        bool retrieveInitialMemory;
        long lastMemMeasure;
        long mem;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.PreferMultiSampling = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
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
            random = new Random();

            DebugShapeRenderer.Initialize(GraphicsDevice);
            WorldObjects.InitModels(Content);
            MessageBus.Initialize();

            CollisionDetection.InitCollisionDetection();
            worldGeneration = new WorldGeneration(random);
            controller = new Controller(DuckManager.poolMaxSize);
            duckManager = new DuckManager(random, Content);
            duckManager.Initialize();
            Physics.Init(duckManager, controller);
            inputManager = new InputManager(this);
            inputHandler = new InputHandler(inputManager);
            
            ProjectilePool.Init();
            Player.Init(1500f, GraphicsDevice, WorldObjects.Ducks[DuckTypes.White], DuckTypes.White);
            
            #region Start Memory variables
            consoleWriter = new ConsoleWriter();
            retrieveInitialMemory = true;
            lastMemMeasure = 0;
            mem = 0;
            this.IsMouseVisible = true;
            #endregion

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            AudioManager.Initialize(this);

            skyBox = new SkyBox(GraphicsDevice);
            skyBox.Textures[0] = Content.Load<Texture2D>("skybox/front");
            skyBox.Textures[1] = Content.Load<Texture2D>("skybox/back");
            skyBox.Textures[2] = Content.Load<Texture2D>("skybox/down");
            skyBox.Textures[3] = Content.Load<Texture2D>("skybox/up");
            skyBox.Textures[4] = Content.Load<Texture2D>("skybox/left");
            skyBox.Textures[5] = Content.Load<Texture2D>("skybox/right");
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
            deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
            #region Close Game Key
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            #endregion

            #region Cursor State
            switch (gameState)
            {
                case GameStates.Play:
                    this.IsMouseVisible = false;
                    break;
                case GameStates.Pause:
                case GameStates.Menu:
                    this.IsMouseVisible = true;
                    break;
            }
            #endregion

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
            inputHandler.Update(deltaTime);

            if (gameState == GameStates.Play )
            {
                duckManager.Update(deltaTime);
                consoleWriter.Update();
                MessageBus.Update();
                Player.Update(deltaTime); 
                controller.Update(gameTime);
                ProjectilePool.Update(deltaTime);
                Physics.Update();
                CollisionDetection.Update();
            }

            MessageBus.Update();
            skyBox.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            duckManager.Draw();

            worldGeneration.Draw();

            Player.Draw();

            ProjectilePool.Draw();

            DebugShapeRenderer.Draw(gameTime, Player.cameraView, Player.Projection());

            controller.Draw();

            skyBox.Draw();

            base.Draw(gameTime);
        }
    }
}
