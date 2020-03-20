﻿using MotoresDeJogos.Abstracts;
using MotoresDeJogos.Char;
using MotoresDeJogos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos.Managers
{
    static class CollisionDetection
    {
        private const int MAX_OBJECTS = 100000;

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

        }

        #region Collision with Object
        public static void CheckAllCollisions()
        {
            for ( int i = 0; i < worldObjectsAlive.Count(); i++)
            {
                for (int j = 0; j < worldObjectsAlive.Count(); j++)
                {
                    if (i == j) continue;

                    if (!worldObjectsAlive[i].IsDead())
                    {
                        if (worldObjectsAlive[i].IsColliding(Player.PlayerCollider()))
                        {
                            
                        }

                        if (worldObjectsAlive[i].IsColliding(worldObjectsAlive[j].BoundingShpere))
                        {
                        }
                    }
                    
                }
            }
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
