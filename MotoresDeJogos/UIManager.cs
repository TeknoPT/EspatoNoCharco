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
        public static Button startBtn;
        public static Button creditsBtn;
        public static Button exitBtn;
        public static Button resumeBtn;
        public static Button exitPauseBtn;
        public Game1 game;
        //public static GameStates gameState;
        public UIManager(Game game) : base(game)
        {
        }

        public override void InitializeComponent()
        {
            //Main Menu
            startBtn = new Button()
            {
                Text = "Start",
                Size = new Vector2(500, 100),
                BackgroundColor = Color.CadetBlue,
                Location = new Vector2(700,600)
            };

            startBtn.Clicked += startBtn_Clicked;
            Controls.Add(startBtn);

            creditsBtn = new Button()
            {
                Text = "Credits",
                Size = new Vector2(500, 100),
                BackgroundColor = Color.CadetBlue,
                Location = new Vector2(700, 750)
            };
            creditsBtn.Clicked += creditsBtn_Clicked;
            Controls.Add(creditsBtn);

            exitBtn = new Button()
            {
                Text = "Exit",
                Size = new Vector2(500, 100),
                BackgroundColor = Color.CadetBlue,
                Location = new Vector2(700, 900)
            };
            exitBtn.Clicked += exitBtn_Clicked;
            Controls.Add(exitBtn);

            //Pause Menu
            resumeBtn = new Button()
            {
                Text = "Resume",
                Size = new Vector2(500, 100),
                BackgroundColor = Color.CadetBlue,
                Location = new Vector2(700, 650)
            };
            resumeBtn.IsVisible = false;
            resumeBtn.Clicked += resumeBtn_Clicked;
            Controls.Add(resumeBtn);

            exitPauseBtn = new Button()
            {
                Text = "Exit",
                Size = new Vector2(500, 100),
                BackgroundColor = Color.CadetBlue,
                Location = new Vector2(700, 800)
            };
            exitPauseBtn.IsVisible = false;
            exitPauseBtn.Clicked += exitPauseBtn_Clicked;
            Controls.Add(exitPauseBtn);
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

        public static void ShowHideButtons() {
            startBtn.IsVisible = !startBtn.IsVisible;
            creditsBtn.IsVisible = !creditsBtn.IsVisible;
            exitBtn.IsVisible = !exitBtn.IsVisible;
            resumeBtn.IsVisible = !resumeBtn.IsVisible;
            exitPauseBtn.IsVisible = !exitPauseBtn.IsVisible;
        }
    }
}
