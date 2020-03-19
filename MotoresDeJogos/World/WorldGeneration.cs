using Microsoft.Xna.Framework;
using MotoresDeJogos.Interfaces;
using MotoresDeJogos.Models;
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
            FountainModel fountainModel = new FountainModel(WorldObjects.Fountain);
            TerrainModel terrainModel = new TerrainModel(WorldObjects.Terrain);
            WaterModel waterModel = new WaterModel(WorldObjects.Lake); 
            staticObjects.Add(new Fountain(fountainModel));
            staticObjects.Add(new Terrain(terrainModel));
            staticObjects.Add(new Water(waterModel));
        }

        private void GenerateTrees()
        {
            TreeModel treeModel = new TreeModel(WorldObjects.Tree);
            for (int i = 0; i < 100; i++)
            {
                staticObjects.Add(new Tree(treeModel));
            }
        }

        private void GenerateFlowers()
        {
            FlowerModel flowerModel = new FlowerModel(WorldObjects.Flower);
            for (int i = 0; i < 3000; i++)
            {
                staticObjects.Add(new Flower(flowerModel));
            }
        }

        private void GenerateFishes()
        {
            FishModel fishModel = new FishModel(WorldObjects.Fish);
            for(int i = 0; i < 1000; i++)
            {
                staticObjects.Add(new Fish(fishModel));
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
