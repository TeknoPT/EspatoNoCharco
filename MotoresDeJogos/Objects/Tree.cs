using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    class Tree : IStatic, IGenerate, IMDrawable
    {
        private TreeModel model;

        public TreeModel Model
        {
            get { return model; }
            set { model = value; }
        }

        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        private Matrix world;

        public Tree(TreeModel model)
        {
            this.model = model;
            this.position = Generate();
            this.world = Matrix.CreateTranslation(position);
        }

        public void Draw()
        {
            foreach (ModelMesh mesh in model.Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.LightingEnabled = true;
                    effect.EnableDefaultLighting();
                    effect.World = world;
                    effect.View = ModedCamera.View;
                    effect.Projection = ModedCamera.Projection;
                }

                mesh.Draw();
            }
        }

        public bool IsStatic()
        {
            return true;
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
