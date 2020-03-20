using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MotoresDeJogos.Interfaces;
using MotoresDeJogos.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos.Objects
{
    class Flower : ParentWorldObject
    {
        public Flower(Vector3 position)
        {
            model = WorldObjects.Flower;
            world = Matrix.CreateTranslation(position);
        }

        public new Vector3 Generate()
        {
            int x = 0;
            int z = 0;
            while (x > -20000 && x < 20000)
                x = WorldGeneration.random.Next(-40000, 40000);

            while (z > -20000 && z < 20000)
                z = WorldGeneration.random.Next(-40000, 40000);

            return new Vector3(x, -50, z);
        }
    }
}
