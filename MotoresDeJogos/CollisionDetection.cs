using MotoresDeJogos.Char;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos
{
    class CollisionDetection
    {
        private List<Duck> ducksAlive;
        private List<Duck> ducksDead;

        public CollisionDetection(List<Duck> ducksAlive, List<Duck> ducksDead)
        {
            this.ducksAlive = ducksAlive;
            this.ducksDead = ducksDead;
        }

        #region Collision with Object
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
                        ducksAlive[i].Alive = false;
                        ducksAlive[j].Alive = false;
                    }
                }
            }            
        }
        #endregion
    }
}
