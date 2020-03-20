using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.UI.Forms;

namespace MotoresDeJogos
{
    class UIManager : ControlManager
    {
        static Button button1;
        static Button button2;
        public Game1 game;
        public static GameStates gameState;
        public UIManager(Game game) : base(game)
        {
        }

        public override void InitializeComponent()
        {
            button1 = new Button()
            {
                Text = "Start",
                Size = new Vector2(500, 100),
                BackgroundColor = Color.Chartreuse,
                Location = new Vector2(750,300)
            };

            button1.Clicked += button1_Clicked;
            Controls.Add(button1);

            button2 = new Button()
            {
                Text = "Exit",
                Size = new Vector2(500, 100),
                BackgroundColor = Color.Chartreuse,
                Location = new Vector2(750, 700)
            };
            button2.Clicked += button2_Clicked;
            Controls.Add(button2);
        }

        private void button1_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button1.IsVisible = false;
            button2.IsVisible = false;
            Game1.gameState = GameStates.Play;
        }

        private void button2_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            System.Environment.Exit(1);
        }

        public static void ShowHideButtons() {
            button1.IsVisible = !button1.IsVisible;
            button2.IsVisible = !button2.IsVisible;
        }
    }
}
