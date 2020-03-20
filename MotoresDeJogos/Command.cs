using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos
{
    public abstract class Command
    {
        public abstract void Execute();
    }

    public enum MoveDirection
    {
        UP,
        DOWN,
        RIGHT,
        LEFT
    }

    public class MoveCommand : Command
    {
        // Needs a reference to the Duck

        MoveDirection direction;

        public MoveCommand(MoveDirection dir)
        {
            direction = dir;
        }

        public override void Execute()
        {
            MoveDuck();
        }

        void MoveDuck()
        {
            switch (direction)
            {
                case MoveDirection.UP:

                    break;
                case MoveDirection.DOWN:

                    break;
                case MoveDirection.RIGHT:

                    break;
                case MoveDirection.LEFT:

                    break;
            }
        }
    }

    public class AttackCommand : Command
    {
        Attack attack;

        public AttackCommand(Attack attack)
        {
            this.attack = attack;
        }

        public override void Execute()
        {
            attack.Activate();
        }
    }

    public class PauseCommand : Command
    {
        public PauseCommand()
        {

        }

        public override void Execute()
        {
            if (Game1.gameState == GameStates.Pause)
                Game1.gameState = GameStates.Play;
            else if (Game1.gameState == GameStates.Play)
                Game1.gameState = GameStates.Pause;

            // Trigger menu
        }
    }

    public class DoNothing : Command
    {
        public override void Execute()
        {
            // Nothing
        }
    }
}
