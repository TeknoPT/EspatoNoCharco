using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MotoresDeJogos.Abstracts;
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
        DuckEnemy tempDuckEnemy;
        private static int poolCounter = 30;
        public static int poolMaxSize = 30;
        private int timer = 0;

        private List<ACollidable> ducksDead = new List<ACollidable>(poolMaxSize);
        private List<ACollidable> ducksAlive = new List<ACollidable>(poolMaxSize);

        public DuckManager(Random random, ContentManager Content)
        {
            
            this.random = random;
            this.Content = Content;
        }

        public List<ACollidable> GetDuckEnemies()
        {
            return ducksAlive;
        }

        public void Initialize()
        {

            float xSize = 12000;
            float zSize = 12000;

            for (float x = -xSize; x <= xSize; x += xSize/(poolCounter/8))
            {
                for (float z = -zSize; z <= zSize; z += zSize/ (poolCounter / 8))
                {
                    if (x > 8000 || x < -8000 || z > 8000 || z < -8000)
                    {
                        tempDuckEnemy = RandomDuckEnemy(new Vector3(x, 0, z));
                        ducksAlive.Add(tempDuckEnemy);
                        CollisionDetection.AddObject(tempDuckEnemy);
                    }
                }
            }
        }

        private DuckEnemy RandomDuckEnemy(Vector3 position)
        {
            int duckInt = random.Next(0, 5);
            DuckTypes duckType;
            Model model;

            switch (duckInt)
            {
                case (int) DuckTypes.Black:
                    model = WorldObjects.Ducks[DuckTypes.Black];
                    duckType = DuckTypes.Black;
                    break;
                case (int) DuckTypes.White:
                    model = WorldObjects.Ducks[DuckTypes.White];
                    duckType = DuckTypes.White;
                    break;
                case (int) DuckTypes.Red:
                    model = WorldObjects.Ducks[DuckTypes.Red];
                    duckType = DuckTypes.Red;
                    break;
                case (int) DuckTypes.Green:
                    model = WorldObjects.Ducks[DuckTypes.Green];
                    duckType = DuckTypes.Green;
                    break;
                case (int) DuckTypes.Blue:
                    model = WorldObjects.Ducks[DuckTypes.Blue];
                    duckType = DuckTypes.Blue;
                    break;
                default:
                    model = WorldObjects.Ducks[DuckTypes.Red];
                    duckType = DuckTypes.Red;
                    break;
            }


            return new DuckEnemy(position, random, model, duckType);
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
            }
        }
        public void Update(float deltaTime)
        {
           
            foreach (DuckEnemy duck in ducksAlive)
            {
                if ( !duck.IsDead() ) duck.Update(deltaTime);
                else
                {
                    sendToGrave(duck);
                }
            }

            foreach(DuckEnemy duck in ducksDead)
            {
                ducksAlive.Remove(duck);
            }
        }

        public void Draw()
        {
            foreach (DuckEnemy duck in ducksAlive)
            {
                if (Player.InView(duck.BoundingShpere))
                {
                    duck.Draw();
                }
            }
        }
    }
}
