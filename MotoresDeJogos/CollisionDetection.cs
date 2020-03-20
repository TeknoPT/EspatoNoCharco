using MotoresDeJogos.Abstracts;
using MotoresDeJogos.Char;
using MotoresDeJogos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos
{
    class CollisionDetection
    {
        private List<DuckEnemy> ducksAlive;
        private List<DuckEnemy> ducksDead;

        private const int MAX_OBJECTS = 100000;

        private List<ACollidable> worldObjects = new List<ACollidable>(MAX_OBJECTS);

        public CollisionDetection(List<DuckEnemy> ducksAlive, List<DuckEnemy> ducksDead)
        {
            this.ducksAlive = ducksAlive;
            this.ducksDead = ducksDead;
        }

        #region Collision with Object
        public void CheckAllCollisions()
        {
            for ( int i = 0; i < worldObjects.Count(); i++)
            {
                for (int j = 0; j < worldObjects.Count(); j++)
                {
                    if (i == j) continue;
                    if (worldObjects[i].IsColliding( worldObjects[j].BoundingShpere ))
                    {
                        worldObjects[i].Destroy();
                        worldObjects[j].Destroy();
                    }
                }
            }
        }
        #endregion

        #region Basic Collision
        public void CheckCollison()
        {
            for ( int i = 0; i < ducksAlive.Count; i++)
            {
                for (int j = 0; j < ducksAlive.Count; j++)
                {
                    if (i == j) continue;
                    if (ducksAlive[i].IsColliding(ducksAlive[j].BoundingShpere))
                    {
                        ducksAlive[i].Destroy();
                        ducksAlive[j].Destroy();
                        MessageBus.InsertNewMessage(new ConsoleMessage("O CARALHO MORREU"));
                    }
                }
            }            
        }
        #endregion

        #region Add objects with different ways
        public void AddObjects(List<ACollidable> objects)
        {
            this.worldObjects.Union(objects);
        }

        public void AddObjects(ACollidable collidable)
        {
            this.worldObjects.Add(collidable);
        }
        #endregion
    }
}
