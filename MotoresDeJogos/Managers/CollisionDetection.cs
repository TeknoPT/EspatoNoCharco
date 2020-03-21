using MotoresDeJogos.Abstracts;
using MotoresDeJogos.Char;
using MotoresDeJogos.Interfaces;
using MotoresDeJogos.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MotoresDeJogos.Managers.Controller;

namespace MotoresDeJogos.Managers
{
    static class CollisionDetection
    {
        private const int MAX_OBJECTS = 10000;

        private static List<ACollidable> worldObjectsAlive;
        private static List<ACollidable> worldObjectsDead;

        private static ACollidable floor;

        public static void InitCollisionDetection()
        {
            worldObjectsAlive = new List<ACollidable>(MAX_OBJECTS);
            worldObjectsDead = new List<ACollidable>(MAX_OBJECTS);
        }

        public static void Update()
        {
            //CheckAllCollisions();
        }

        #region Collision with Object
        public static void CheckAllCollisions()
        {
            for ( int i = 0; i < worldObjectsAlive.Count(); i++)
            {
                if (worldObjectsAlive[i].IsDead())
                    continue;

                if (worldObjectsAlive[i].IsColliding(Player.PlayerCollider()))
                {
                    // TODO
                }

                #region Check If Projectile Hit Wall
                if (worldObjectsAlive[i] is Projectile)
                {
                    if (CheckIfHitBoundaries(worldObjectsAlive[i]))
                    {
                        worldObjectsAlive[i].Destroy();
                        continue;
                    }
                }
                #endregion


                for (int j = 0; j < worldObjectsAlive.Count(); j++)
                {
                    if (i == j) continue;

                    if (!worldObjectsAlive[j].IsDead())
                    {
                        if (worldObjectsAlive[i].IsColliding(worldObjectsAlive[j].BoundingShpere))
                        {
                            if ( worldObjectsAlive[i] is Projectile && worldObjectsAlive[j] is DuckEnemy)
                            {
                                worldObjectsAlive[i].Destroy();
                                worldObjectsAlive[j].Destroy();
                                Player.Data.score += 5;
                                MessageBus.InsertNewMessage(new ConsoleMessage("Score :" + Player.Data.score));
                                break;
                            }
                        }
                    }
                    
                }
            }
        }
        #endregion

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

        #region Add objects with different ways
        public static void AddObjects(List<ACollidable> objects)
        {
            worldObjectsAlive.Union(objects);
        }

        public static void AddObject(ACollidable collidable)
        {
            worldObjectsAlive.Add(collidable);
        }
        #endregion

        #region Set the floor
        public static void SetFloor(ACollidable newFloor)
        {
            floor = newFloor;
        }
        #endregion
    }
}
