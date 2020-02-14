using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MotoresDeJogos
{
    class Ship
    {
        private float speed;

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        private Model model;

        public Model Model
        {
            get { return model; }
            set { model = value; }
        }

        private Matrix world;

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

        private Boolean alive;

        public Boolean Alive
        {
            get { return alive; }
            set { alive = value; }
        }

        public Ship(Vector3 position, ContentManager content, Random random, Model model)
        {
            this.position = position;
            this.world = Matrix.CreateTranslation(position);
            this.speed = (float) random.Next(1, 20);
            if (this.speed == 0) this.speed = (float)random.Next(1, 20);
            this.alive = true;
            this.Model = model;

            foreach (ModelMesh mesh in this.model.Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, mesh.BoundingSphere);
            }
            

        }

      
        public void Update(GameTime gameTime)
        {
            if (alive)
            {
                position.Z -= speed * gameTime.ElapsedGameTime.Milliseconds;
                world = Matrix.CreateTranslation(position);

                if (position.Z <= -50000)
                {
                    position.Z = 5000;
                    alive = false;
                }

                boundingSphere.Center = position;
            }
        }

        public void Draw()
        {
            if (alive)
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
                DebugShapeRenderer.AddBoundingSphere(boundingSphere, Color.Green);
            }

        }

    }
}
