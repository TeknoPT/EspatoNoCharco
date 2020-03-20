using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MotoresDeJogos
{
    public class InputHandler : GameComponent
    {
        // All Inputs Pass throw here
        Command buttonUp, buttonDown, buttonRight, buttonLeft, buttonFire;
        InputManager inputManager;

        public InputHandler(Game game, InputManager inputManager) : base(game)
        {
            this.inputManager = inputManager;
            // Movement
            buttonUp = new MoveCommand(MoveDirection.UP);
            buttonDown = new MoveCommand(MoveDirection.DOWN);
            buttonRight = new MoveCommand(MoveDirection.RIGHT);
            buttonLeft = new MoveCommand(MoveDirection.LEFT);
            // Attack
            buttonFire = new AttackCommand(new RedAttack(20, "attack_R"));
        }

        public override void Update(GameTime gameTime)
        {
            Handler();

            base.Update(gameTime);
        }

        public void Handler()
        {
            // Take care of all Inputs of the game here
            if (inputManager.IsPressed(Keys.W))
            {
                buttonUp.Execute();
            }
            if (inputManager.IsPressed(Keys.S))
            {
                buttonDown.Execute();
            }
            if (inputManager.IsPressed(Keys.D))
            {
                buttonRight.Execute();
            }
            if (inputManager.IsPressed(Keys.A))
            {
                buttonLeft.Execute();
            }
            if (inputManager.JustPressed(Keys.Space))
            {
                buttonFire.Execute();
            }

            // Test for DataManager
            if (inputManager.JustPressed(Keys.R))
            {
                DataManager.Save(new Data() { score = 30, level = 20 });
            }
            if (inputManager.JustPressed(Keys.L))
            {
                Data d = DataManager.Load();
                Console.WriteLine("Score: " + d.score);
            }
        }
    }
}
