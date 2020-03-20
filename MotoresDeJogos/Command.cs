using Microsoft.Xna.Framework.Graphics;
using MotoresDeJogos.Char;
using MotoresDeJogos.World;
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

    public class ChangeCharacterCommand : Command
    {
        DuckTypes duckType = DuckTypes.Black;
        public ChangeCharacterCommand(DuckTypes duckType)
        {
            this.duckType = duckType;
        }

        public override void Execute()
        {
            switch (duckType)
            {
                case DuckTypes.Black:
                    duckType = DuckTypes.While;
                    break;
                case DuckTypes.While:
                    duckType = DuckTypes.Red;
                    break;
                case DuckTypes.Red:
                    duckType = DuckTypes.Green;
                    break;
                case DuckTypes.Green:
                    duckType = DuckTypes.Blue;
                    break;
                case DuckTypes.Blue:
                    duckType = DuckTypes.Black;
                    break;
            }

            Player.LoadModel(WorldObjects.Ducks[duckType]);
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
