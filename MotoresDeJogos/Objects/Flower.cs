using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MotoresDeJogos.Char;
using MotoresDeJogos.Interfaces;
using MotoresDeJogos.Models;
using MotoresDeJogos.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos.Objects
{
    class Flower : IStatic, IGenerate, IMDrawable
    {
        protected FlowerModel model;

        public FlowerModel Model
        {
            get { return model; }
            set { model = value; }
        }

        protected Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        protected Matrix world;

        public void Draw()
        {
            foreach (ModelMesh mesh in model.Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.LightingEnabled = true;
                    effect.EnableDefaultLighting();
                    effect.World = world;
                    effect.View = Player.cameraView;
                    effect.Projection = Player.Projection();
                }

                mesh.Draw();
            }
        }

        public bool IsStatic() => true;

        public Flower(Vector3 position, FlowerModel model)
        {
            this.model = model;
            this.position = position;
            world = Matrix.CreateTranslation(position);
        }

        public Vector3 Generate()
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
