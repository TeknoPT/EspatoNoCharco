using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MotoresDeJogos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos.Objects
{
    class Flower : ParentWorldObject
    {

        public Flower()
        {
            model = World.WorldObjects.Flower;
            this.position = Generate();
            this.world = Matrix.CreateTranslation(position);
        }

        public bool IsStatic()
        {
            return true;
        }

        public Vector3 Generate()
        {
            return Vector3.Right;
        }
    }
}
