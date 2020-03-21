using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.UI.Forms;

namespace MotoresDeJogos
{
    class UIManager : ControlManager
    {
        private static bool isActive;
        public static Button startBtn;
        public static Button creditsBtn;
        public static Button exitBtn;
        public static Button resumeBtn;
        public static Button exitPauseBtn;
        public Game1 game;
        //public static GameStates gameState;
        public UIManager(Game game) : base(game)
        {
            isActive = true;
        }

        public override void InitializeComponent()
        {
            startBtn = new Button()
            {
                Text = "Start",
                Size = new Vector2(500, 100),
                BackgroundColor = Color.CadetBlue,
                Location = new Vector2(750, 600)
            };

            startBtn.Clicked += startBtn_Clicked;
            Controls.Add(startBtn);

            creditsBtn = new Button()
            {
                Text = "Credits",
                Size = new Vector2(500, 100),
                BackgroundColor = Color.CadetBlue,
                Location = new Vector2(750, 750)
            };
            creditsBtn.Clicked += creditsBtn_Clicked;
            Controls.Add(creditsBtn);

            exitBtn = new Button()
            {
                Text = "Exit",
                Size = new Vector2(500, 100),
                BackgroundColor = Color.CadetBlue,
                Location = new Vector2(750, 900)
            };
            exitBtn.Clicked += exitBtn_Clicked;
            Controls.Add(exitBtn);

            resumeBtn = new Button()
            {
                Text = "Resume",
                Size = new Vector2(500, 100),
                BackgroundColor = Color.CadetBlue,
                Location = new Vector2(750, 750)
            };
            resumeBtn.Clicked += resumeBtn_Clicked;
            Controls.Add(resumeBtn);

            exitPauseBtn = new Button()
            {
                Text = "Exit",
                Size = new Vector2(500, 100),
                BackgroundColor = Color.CadetBlue,
                Location = new Vector2(750, 900)
            };
            exitPauseBtn.Clicked += exitPauseBtn_Clicked;
            Controls.Add(exitPauseBtn);

            ScreenOn();
        }

        private void ScreenOn()
        {
            //Main Menu
            if (Game1.gameState == GameStates.Menu)
            {
                ShowMenu();
            }

            //Pause Menu
            if (Game1.gameState == GameStates.Pause)
            {
                ShowPauseMenu();
            }
        }

        private void startBtn_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            startBtn.IsVisible = false;
            creditsBtn.IsVisible = false;
            exitBtn.IsVisible = false;
            Game1.gameState = GameStates.Play;
        }

        private void creditsBtn_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            /*Game1.spriteBatch.Begin();
            Game1.spriteBatch.DrawString(Game1.font, "Ines Oliveira", new Vector2(900, 350), Color.Thistle);
            Game1.spriteBatch.DrawString(Game1.font, "Joao Novo", new Vector2(900, 400), Color.Thistle);
            Game1.spriteBatch.DrawString(Game1.font, "Madalena Barros", new Vector2(900, 450), Color.Thistle);
            Game1.spriteBatch.End();*/
        }

        private void exitBtn_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            System.Environment.Exit(1);
        }

        private void resumeBtn_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            resumeBtn.IsVisible = false;
            exitPauseBtn.IsVisible = false;
            Game1.gameState = GameStates.Play;
        }

        private void exitPauseBtn_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            System.Environment.Exit(1);
        }

        public static void ShowHideButtons()
        {
            startBtn.IsVisible = !startBtn.IsVisible;
            creditsBtn.IsVisible = !creditsBtn.IsVisible;
            exitBtn.IsVisible = !exitBtn.IsVisible;
            resumeBtn.IsVisible = !resumeBtn.IsVisible;
            exitPauseBtn.IsVisible = !exitPauseBtn.IsVisible;
        }

        public static void HideEveryThing()
        {
            isActive = false;
            startBtn.IsVisible = false;
            creditsBtn.IsVisible = false;
            exitBtn.IsVisible = false;
            resumeBtn.IsVisible = false;
            exitPauseBtn.IsVisible = false;
        }

        public static void ShowPauseMenu()
        {
            isActive = true;
            startBtn.IsVisible = false;
            creditsBtn.IsVisible = false;
            exitBtn.IsVisible = false;
            resumeBtn.IsVisible = true;
            exitPauseBtn.IsVisible = true;
        }

        public static void ShowMenu()
        {
            isActive = true;
            startBtn.IsVisible = true;
            creditsBtn.IsVisible = true;
            exitBtn.IsVisible = true;
            resumeBtn.IsVisible = false;
            exitPauseBtn.IsVisible = false;
        }

        public static bool IsActive() => isActive;
    }
}
