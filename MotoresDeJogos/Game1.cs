using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MotoresDeJogos.Char;
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
        DuckPlayer Player;
        InputManager inputManager;
        InputHandler inputHandler;
        WorldGeneration worldGeneration;
        public static GameStates gameState = GameStates.Play;
        SkyBox skyBox;
        //Pool managers = new Pool();
       
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
            //camera = new Camera(new Vector3(0, 0, 500), graphics);
            random = new Random();
            ModedCamera.Initialize(graphics.GraphicsDevice);

            DebugShapeRenderer.Initialize(GraphicsDevice);
            WorldObjects.InitModels(Content);
            worldGeneration = new WorldGeneration(random);
            MessageBus.Initialize();
            inputManager = new InputManager(this);
            inputHandler = new InputHandler(this, inputManager);
            duckManager = new DuckManager( random, Content );
            duckManager.Initialize();
            Player = new DuckPlayer(Vector3.Zero, Content, random, WorldObjects.Ducks[DuckTypes.Red]);

            consoleWriter = new ConsoleWriter();
            retrieveInitialMemory = true;
            lastMemMeasure = 0;
            mem = 0;
            this.IsMouseVisible = true;

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
            skyBox.Textures[0] = Content.Load<Texture2D>("skybox/bkg1_front5");
            skyBox.Textures[1] = Content.Load<Texture2D>("skybox/bkg1_back6");
            skyBox.Textures[2] = Content.Load<Texture2D>("skybox/bkg1_bottom4");
            skyBox.Textures[3] = Content.Load<Texture2D>("skybox/bkg1_top3");
            skyBox.Textures[4] = Content.Load<Texture2D>("skybox/bkg1_left2");
            skyBox.Textures[5] = Content.Load<Texture2D>("skybox/bkg1_right1");
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
            #region Close Game Key
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            #endregion

            //camera.Update(gameTime);

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
            inputHandler.Update(gameTime);


            if (gameState == GameStates.Play )
            {
                ModedCamera.Update(gameTime, graphics.GraphicsDevice);
                duckManager.Update(gameTime);
                consoleWriter.Update();
                MessageBus.Update();
                Player.Update(gameTime);
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

            DebugShapeRenderer.Draw(gameTime, ModedCamera.View, ModedCamera.Projection);

            skyBox.Draw();

            base.Draw(gameTime);
        }
    }
}
