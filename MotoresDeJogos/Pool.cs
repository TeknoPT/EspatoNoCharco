using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MotoresDeJogos.Interfaces;
using MotoresDeJogos.Models;
using MotoresDeJogos.Objects;
using MotoresDeJogos.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos
{
    class Pool<T , TModel> where TModel : BaseModel where T : BaseObjects,  new()
    {
        Random random;
        ContentManager Content;
        private static int poolCounter = 1000;
        public static int poolMaxSize = 500;
        private int timer = 0;

        CollisionDetection collisionDetection;

        private List<T> listDead = new List<T>(poolMaxSize);
        private List<T> listAlive = new List<T>(poolMaxSize);

        public Pool(Random random, ContentManager Content)
        {
            this.random = random;
            this.Content = Content;
        }

        public void Initialize()
        {
            T objectType;
            
            //K = WorldObjects.Flower;

            for (int i = 0; i < poolMaxSize; i++)
            {
                objectType = new T();
                listAlive.Add(objectType);
            }
            //this.collisionDetection = new CollisionDetection(Ducks, shipsDead);
        }

        /*public void reviveShip(Ship ship)
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
            if (poolCounter > 0)
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
                if (ship.Alive) ship.Update(gameTime);
                else
                {
                    sendToGrave(ship);
                }
            }

            foreach (Ship ship in shipsDead)
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
        }*/
    }
}
