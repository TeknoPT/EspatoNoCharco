using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos
{
    class CollisionDetection
    {
        private List<Ship> shipsAlive;
        private List<Ship> shipsDead;

        public CollisionDetection(List<Ship> shipsAlive, List<Ship> shipsDead)
        {
            this.shipsAlive = shipsAlive;
            this.shipsDead = shipsDead;
        }

        public void CheckCollison()
        {
            for ( int i = 0; i < shipsAlive.Count; i++)
            {
                for (int j = 0; j < shipsAlive.Count; j++)
                {
                    if (i == j) continue;
                    if (shipsAlive[i].BoundingShpere.Intersects(shipsAlive[j].BoundingShpere))
                    {
                        shipsAlive[i].Alive = false;
                        shipsAlive[j].Alive = false;
                    }
                }
            }

            
        }
    }
}
