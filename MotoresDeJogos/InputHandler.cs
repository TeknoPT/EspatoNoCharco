using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MotoresDeJogos.Char;
using MotoresDeJogos.World;

namespace MotoresDeJogos
{
    public class InputHandler
    {
        // All Inputs Pass throw here
        Command buttonUp, buttonDown, buttonRight, buttonLeft, buttonFire, buttonPause, buttonChangeCharacter, buttonRotRight, buttonRotLeft;
        InputManager inputManager;

        public InputHandler(InputManager inputManager)
        {
            this.inputManager = inputManager;
            // Movement
            buttonUp = new MoveCommand(MoveDirection.UP);
            buttonDown = new MoveCommand(MoveDirection.DOWN);
            buttonRight = new MoveCommand(MoveDirection.RIGHT);
            buttonLeft = new MoveCommand(MoveDirection.LEFT);
            // Rotation
            buttonRotLeft = new MoveCommand(MoveDirection.ROTLEFT);
            buttonRotRight = new MoveCommand(MoveDirection.ROTRIGHT);
            // Attack
            buttonFire = new AttackCommand(Player.WorldPosition(), Player.GetRotation(), Player.duckModelType);
            // PauseButton
            buttonPause = new PauseCommand();
            // Change Character Model
            buttonChangeCharacter = new ChangeCharacterCommand(DuckTypes.Black);
        }

        public void Update(float deltaTime)
        {
            Handler(deltaTime);
        }

        public void Handler(float deltaTime)
        {
            // Take care of all Inputs of the game here
            if (inputManager.IsPressed(Keys.W))
            {
                buttonUp.Execute(deltaTime);
            }
            if (inputManager.IsPressed(Keys.S))
            {
                buttonDown.Execute(deltaTime);
            }
            if (inputManager.IsPressed(Keys.D))
            {
                buttonRight.Execute(deltaTime);
            }
            if (inputManager.IsPressed(Keys.A))
            {
                buttonLeft.Execute(deltaTime);
            }

            if (inputManager.IsPressed(Keys.Q))
            {
                buttonRotLeft.Execute(deltaTime);
            }
            if (inputManager.IsPressed(Keys.E))
            {
                buttonRotRight.Execute(deltaTime);
            }

            if (inputManager.JustPressed(Keys.Space))
            {
                ((AttackCommand)buttonFire).UpdateVariables(Player.WorldPosition(), Player.GetRotation(), Player.duckModelType);
                buttonFire.Execute();
            }


            // Pause
            if (inputManager.JustPressed(Keys.P))
            {
                buttonPause.Execute();
            }

            // Change Character Model
            if (inputManager.JustPressed(Keys.C))
            {
                buttonChangeCharacter.Execute();
            }

            // Test for DataManager
            if (inputManager.JustPressed(Keys.R))
            {
                DataManager.Save(Player.Data);
            }
            if (inputManager.JustPressed(Keys.L))
            {
                Data d = DataManager.Load();
                Console.WriteLine("Score: " + d.score);
            }
        }
    }
}
