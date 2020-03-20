using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MotoresDeJogos.Char;
using MotoresDeJogos.World;
using System;
using System.Collections.Generic;

namespace MotoresDeJogos.Managers
{
    class DuckManager
    {
        Random random;
        ContentManager Content;
        private static int poolCounter = 1000;
        public static int poolMaxSize = 1000;
        private int timer = 0;

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
                if ( !duck.IsDead() ) duck.Update(gameTime);
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
            if (timer > 30)
            {
                //CollisionDetection.CheckCollison();
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
