using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos
{
    class InputHandler
    {
        // All Inputs Pass throw here
        Command buttonUp, buttonDown, buttonRight, buttonLeft;
        InputManager inputManager;

        public void Init(InputManager inputManager)
        {
            buttonUp = new MoveCommand(MoveDirection.UP);
            buttonDown = new MoveCommand(MoveDirection.DOWN);
            buttonRight = new MoveCommand(MoveDirection.RIGHT);
            buttonLeft = new MoveCommand(MoveDirection.LEFT);
        }

        public void Update()
        {
            
        }

        public void Handler()
        {
            // Take care of all Inputs of the game here
            if (inputManager.IsPressed(Microsoft.Xna.Framework.Input.Keys.W))
            {
                buttonUp.Execute();
            }
            if (inputManager.IsPressed(Microsoft.Xna.Framework.Input.Keys.S))
            {
                buttonDown.Execute();
            }
            if (inputManager.IsPressed(Microsoft.Xna.Framework.Input.Keys.D))
            {
                buttonRight.Execute();
            }
            if (inputManager.IsPressed(Microsoft.Xna.Framework.Input.Keys.A))
            {
                buttonLeft.Execute();
            }
        }
    }
}
