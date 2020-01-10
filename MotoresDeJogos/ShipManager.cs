using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace MotoresDeJogos
{
    class ShipManager
    {
        Random random;
        ContentManager Content;
        private static int poolCounter = 100;
        private static int poolMaxSize = 100;

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
            for (int i = 0; i < poolMaxSize; i++)
            {
                ship = new Ship(new Vector3(random.Next(-5000, 5000), random.Next(-5000, 5000), random.Next(-5000, 5000)), Content, random);
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
                
                MessageBus.InsertNewMessage(new ConsoleMessage(String.Format("ListID - {0} | Pool {1}", shipsDead.IndexOf(ship), poolCounter)));
            }
        }
        public void Update(GameTime gameTime)
        {
            foreach(Ship ship in shipsAlive)
            {
                if( ship.Alive ) ship.Update(gameTime);
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

        public void Draw(Camera camera)
        {
            foreach (Ship ship in shipsAlive)
            {
                ship.Draw(camera);
            }
        }
    }
}
