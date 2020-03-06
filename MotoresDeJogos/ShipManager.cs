using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MotoresDeJogos.World;
using System;
using System.Collections.Generic;

namespace MotoresDeJogos
{
    class ShipManager
    {
        private int WorldSize = 50000;
        Random random;
        ContentManager Content;
        private static int poolCounter = 500;
        public static int poolMaxSize = 500;
        private int timer = 0;

        CollisionDetection collisionDetection;

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
            Model model = WorldObjects.Flower;

            for (int i = 0; i < poolMaxSize; i++)
            {
                ship = new Ship(new Vector3(random.Next(-WorldSize, WorldSize), random.Next(-WorldSize, WorldSize), random.Next(-WorldSize, WorldSize)), Content, random, model);
                MessageBus.InsertNewMessage(new ConsoleMessage(String.Format("ID - {0} | Ship Z:{1}", i, ship.Position.Z)));
                shipsAlive.Add(ship);
            }   
            this.collisionDetection = new CollisionDetection(shipsAlive, shipsDead);
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
           
            foreach (Ship ship in shipsAlive)
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
            
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer > 100)
            {
                collisionDetection.CheckCollison();
                timer = 0;
            }
        }

        public void Draw()
        {
            foreach (Ship ship in shipsAlive)
            {
                if (ModedCamera.frustum.Intersects(ship.BoundingShpere))
                {
                    ship.Draw();
                }
            }
        }
    }
}
