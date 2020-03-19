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
    class ParentWorldObject : IStatic, IGenerate, IMDrawable
    {
        protected Model model;

        public Model Model
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

        public ParentWorldObject()
        {
            this.position = Generate();
            this.world = Matrix.CreateTranslation(position);
        }

        public void Draw()
        {
            foreach (ModelMesh mesh in model.Meshes)
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
            return Vector3.Zero;
        }
    }
}
