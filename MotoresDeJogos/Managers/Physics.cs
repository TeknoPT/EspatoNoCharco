using Microsoft.Xna.Framework;
using MotoresDeJogos.Abstracts;
using MotoresDeJogos.Char;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MotoresDeJogos.Managers.Controller;

namespace MotoresDeJogos.Managers
{
    class Physics
    {
        static List<ACollidable> objects;
        static Vector3 gravityAmount;
        static Controller controller;

        public static void Init(DuckManager duckManager, Controller _controller)
        {
            controller = _controller;
            objects = duckManager.GetDuckEnemies();
            gravityAmount = new Vector3(0, -98f, 0);
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

                    #region Check if Colliding with walls
                    if (CheckIfHitBoundaries(objects[i]))
                    {
                        // CODE
                    }
                    #endregion
                }

            }

            // Player Physics Stuff
            if (Player.PlayerCollider().Intersects(controller.FloorCollider()))
            {
                Player.Collision = true;
            }
            else
            {
                Player.Collision = false;
            }

            
        }

        #region Check Boundaries
        private static bool CheckIfHitBoundaries(ACollidable obj)
        {
            return obj.IsColliding(WorldBoundaries.CentralOvalSphere) ||
                obj.IsColliding(WorldBoundaries.LimitFront_BoundingBox) ||
                obj.IsColliding(WorldBoundaries.LimitBack_BoundingBox) ||
                obj.IsColliding(WorldBoundaries.LimitLeft_BoundingBox) ||
                obj.IsColliding(WorldBoundaries.LimitRight_BoundingBox);
        }
        #endregion

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
