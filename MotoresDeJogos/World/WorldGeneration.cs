using Microsoft.Xna.Framework;
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
                staticObjects.Add(new Flower());
            }
        }

        private void GenerateFlowers()
        {
            for (int i = 0; i < 3000; i++)
            {
                staticObjects.Add(new Flower());
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
