using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MotoresDeJogos
{
    class ShipManager
    {
        private int WorldSize = 50000;
        Random random;
        ContentManager Content;
        private static int poolCounter = 1000;
        private static int poolMaxSize = 1000;

        private List<Ship> shipsDead = new List<Ship>(poolMaxSize);
        private List<Ship> shipsAlive = new List<Ship>(poolMaxSize);

        public ShipManager(Random random, ContentManager Content)
        {
            this.random = random;
            this.Content = Content;
        }

        public void Initialize()
        {
            Ship ship;
            Model model = Content.Load<Model>("models\\p1_saucer");
            for (int i = 0; i < poolMaxSize; i++)
            {
                ship = new Ship(new Vector3(random.Next(-WorldSize, WorldSize), random.Next(-WorldSize, WorldSize), random.Next(-WorldSize, WorldSize)), Content, random, model);
                MessageBus.InsertNewMessage(new ConsoleMessage(String.Format("ID - {0} | Ship Z:{1}", i, ship.Position.Z)));
                shipsAlive.Add(ship);
            }

         }

       public void reviveShip(Ship ship)
        {
            if (poolCounter < poolMaxSize)
            {
                poolCounter++;
                shipsAlive.Add(ship);
                shipsDead.Remove(ship);
            }
        }

        public void sendToGrave(Ship ship)
        {
            if ( poolCounter > 0)
            {
                poolCounter--;
                shipsDead.Add(ship);
                
                //MessageBus.InsertNewMessage(new ConsoleMessage(String.Format("ListID - {0} | Pool {1}", shipsDead.IndexOf(ship), poolCounter)));
            }
        }
        public void Update(GameTime gameTime)
        {
            
            foreach(Ship ship in shipsAlive)
            {
                if ( ship.Alive ) ship.Update(gameTime);
                else
                {
                    sendToGrave(ship);
                }
            }

            foreach(Ship ship in shipsDead)
            {
                shipsAlive.Remove(ship);
            }
        }

        public void Draw()
        {
            foreach (Ship ship in shipsAlive)
            {
                ship.Draw();
            }
        }
    }
}
