using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        public virtual void Execute(float deltaTime){}
    }

    public enum MoveDirection
    {
        UP,
        DOWN,
        RIGHT,
        LEFT,
        ROTRIGHT,
        ROTLEFT
    }

    public class MoveCommand : Command
    {
        // Needs a reference to the Duck

        MoveDirection direction;

        public MoveCommand(MoveDirection dir)
        {
            direction = dir;
        }

        public override void Execute(float deltaTime)
        {
            MoveDuck(deltaTime);
        }

        public override void Execute()
        {

        }

        void MoveDuck(float deltaTime)
        {
            switch (direction)
            {
                case MoveDirection.UP:
                    Player.Move(Keys.Up, deltaTime);
                    break;
                case MoveDirection.DOWN:
                    Player.Move(Keys.Down, deltaTime);
                    break;
                case MoveDirection.RIGHT:
                    Player.Move(Keys.Right, deltaTime);
                    break;
                case MoveDirection.LEFT:
                    Player.Move(Keys.Left, deltaTime);
                    break;
                case MoveDirection.ROTRIGHT:
                    Player.Rotate(Keys.E, deltaTime);
                    break;
                case MoveDirection.ROTLEFT:
                    Player.Rotate(Keys.Q, deltaTime);
                    break;
            }
        }
    }

    public class AttackCommand : Command
    {
        DuckTypes duckType;
        Matrix world;
        Attack attack;
        float rotation;

        public AttackCommand(Matrix world, float rotation, DuckTypes duckType)
        {
            this.world = world;
            this.rotation = rotation;
            this.duckType = duckType;
            SelectAttack();
        }

        private void SelectAttack()
        {
            switch (duckType)
            {
                case DuckTypes.Black:
                    attack = new SkullAttack(world, rotation, 25, "attack_K");
                    break;
                case DuckTypes.White:
                    attack = new CloudAttack(world, rotation, 25, "attack_W");
                    break;
                case DuckTypes.Red:
                    attack = new HeartAttack(world, rotation, 25, "attack_R");
                    break;
                case DuckTypes.Green:
                    attack = new ToxicAttack(world, rotation, 25, "attack_G");
                    break;
                case DuckTypes.Blue:
                    attack = new BlueFireBallAttack(world, rotation, 25, "attack_B");
                    break;
            }
        }

        public void UpdateVariables(Matrix world, float rotation, DuckTypes duckType)
        {
            this.world = world;
            this.rotation = rotation;
            this.duckType = duckType;
        }

        public override void Execute()
        {
            SelectAttack();

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
            if ( !UIManager.IsActive() ) UIManager.ShowPauseMenu();
            else UIManager.HideEveryThing();
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
                    duckType = DuckTypes.White;
                    break;
                case DuckTypes.White:
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

            Player.duckModelType = duckType;
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
