using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MotoresDeJogos.Interfaces;
using MotoresDeJogos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos.Objects
{
    class Fish : ICollide, IStatic, IGenerate, IMDrawable
    {
        private float speed;

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        private FishModel model;

        public FishModel Model
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

        private BoundingSphere boundingSphere;

        public BoundingSphere BoundingShpere
        {
            get { return boundingSphere; }
            set { boundingSphere = value; }
        }

        private Matrix world;

        public Fish(FishModel model)
        {
            this.model = model;
            this.position = Generate();
            this.world = Matrix.CreateTranslation(position);

            #region Creating Bounds
            foreach (ModelMesh mesh in this.model.Model.Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, mesh.BoundingSphere);
            }
            #endregion
        }
        
        public void Update()
        {

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


        // Interfaces
        public bool IsColliding(BoundingSphere bounding)
        {
            throw new NotImplementedException();
        }

        public bool IsColliding(BoundingBox bounding)
        {
            throw new NotImplementedException();
        }

        public bool IsStatic()
        {
            return false;
        }

        public Vector3 Generate()
        {   
            return Vector3.Zero;
        }
    }
}
