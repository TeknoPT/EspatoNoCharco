﻿using Microsoft.Xna.Framework;
using MotoresDeJogos.Interfaces;
using MotoresDeJogos.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos.World
{
    class WorldGeneration
    {
        public static Random random;
        public static int MAP_SIZE = 50000;
        public const int MAX_STATIC_OBJECTS = 10000;
        List<IMDrawable> staticObjects = new List<IMDrawable>(MAX_STATIC_OBJECTS);
        
        public WorldGeneration(Random random)
        {
            WorldGeneration.random = random;
            CreateWorld();
        }
        
        private void CreateWorld()
        {
            GenerateBasicWorld();
            GenerateTrees();
            GenerateFlowers();
            //GenerateFishes();
        }

        #region Generating
        private void GenerateBasicWorld()
        {
            staticObjects.Add(new Fountain());
            staticObjects.Add(new Terrain());
            staticObjects.Add(new Water());
        }

        private void GenerateTrees()
        {
            for (int i = 0; i < 1000; i++)
            {
                //staticObjects.Add(new Flower(new Vector3(0,0,0)));
            }
        }

        private void GenerateFlowers()
        {
            for (int x = -50000; x <= 50000; x += 1000)
            {
                for (int z = -35000; z <= 35000; z += random.Next(1000, 1500))
                {
                    if (x > 28000 || x < -28000 || z > 18000 || z < -18000)
                    {
                        staticObjects.Add(new Flower(new Vector3(x + random.Next(-500, 500), -50, z)));
                    }
                }
            }
        }

        private void GenerateFishes()
        {
            for(int i = 0; i < 1000; i++)
            {
                staticObjects.Add(new Fish());
            }
        }
        #endregion

        public void Draw()
        {
            foreach (IMDrawable myObject in staticObjects)
            {
                myObject.Draw();
            }
        }
    }
}
