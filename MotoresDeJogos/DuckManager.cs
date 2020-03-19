using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MotoresDeJogos.Char;
using MotoresDeJogos.World;
using System;
using System.Collections.Generic;

namespace MotoresDeJogos
{
    class DuckManager
    {
        Random random;
        ContentManager Content;
        private static int poolCounter = 500;
        public static int poolMaxSize = 500;
        private int timer = 0;

        CollisionDetection collisionDetection;

        private List<DuckEnemy> ducksDead = new List<DuckEnemy>(poolMaxSize);
        private List<DuckEnemy> ducksAlive = new List<DuckEnemy>(poolMaxSize);

        public DuckManager(Random random, ContentManager Content)
        {
            
            this.random = random;
            this.Content = Content;
        }

        public void Initialize()
        {
            DuckEnemy duck;
            Model model = WorldObjects.Ducks[DuckTypes.Green];

            for (int i = 0; i < poolMaxSize; i++)
            {
                duck = new DuckEnemy( Content, random, model);
                MessageBus.InsertNewMessage(new ConsoleMessage(String.Format("ID - {0} | Ship Z:{1}", i, duck.Position.Z)));
                ducksAlive.Add(duck);
            }   

            this.collisionDetection = new CollisionDetection(ducksAlive, ducksDead);
        }

       public void reviveShip(DuckEnemy duck)
        {
            if (poolCounter < poolMaxSize)
            {
                poolCounter++;
                ducksAlive.Add(duck);
                ducksDead.Remove(duck);
            }
        }

        public void sendToGrave(DuckEnemy duck)
        {
            if ( poolCounter > 0)
            {
                poolCounter--;
                ducksDead.Add(duck);
                
                //MessageBus.InsertNewMessage(new ConsoleMessage(String.Format("ListID - {0} | Pool {1}", shipsDead.IndexOf(ship), poolCounter)));
            }
        }
        public void Update(GameTime gameTime)
        {
           
            foreach (DuckEnemy duck in ducksAlive)
            {
                if (duck.Alive ) duck.Update(gameTime);
                else
                {
                    sendToGrave(duck);
                }
            }

            foreach(DuckEnemy duck in ducksDead)
            {
                ducksAlive.Remove(duck);
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
            foreach (DuckEnemy duck in ducksAlive)
            {
                if (ModedCamera.frustum.Intersects(duck.BoundingShpere))
                {
                    duck.Draw();
                }
            }
        }
    }
}
