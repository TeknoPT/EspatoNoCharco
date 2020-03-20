using Microsoft.Xna.Framework;
using MotoresDeJogos.Abstracts;
using MotoresDeJogos.Char;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos.Managers
{
    class Physics
    {
        static List<ACollidable> objects;
        static Vector3 gravityAmount;
        static bool collision;
        static Controller controller;

        public static void Init(Controller _controller)
        {
            controller = _controller;
            objects = controller.GetObjects();
            gravityAmount = new Vector3(0, -0.05f, 0);
        }

        public static void Update()
        {
            for (int i = 0; i < objects.Count(); i++)
            {
                if (!objects[i].IsDead())
                {
                    #region Check if Colliding with Player
                    if (objects[i].IsColliding(Player.PlayerCollider()))
                    {
                        objects[i].Destroy();
                    }
                    #endregion

                    #region Check if Colliding Floor
                    if (objects[i].IsColliding(controller.FloorCollider()))
                    {
                        objects[i].OnGround = true;
                    }
                    else
                    {
                        objects[i].OnGround  = false;
                    }
                    #endregion
                }

            }

            if (Player.PlayerCollider().Intersects(controller.FloorCollider()))
            {
                Player.Collision = true;
            }
            else
            {
                Player.Collision = false;
            }
        }

        public static Vector3 GravityAmount()
        {
            return gravityAmount;
        }

        public static List<ACollidable> GetObjects()
        {
            return objects;
        }
    }
}
