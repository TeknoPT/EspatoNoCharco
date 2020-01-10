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

        public Ship(Vector3 position, ContentManager content, Random random)
        {
            this.position = position;
            this.world = Matrix.CreateTranslation(position);
            this.speed = (float) random.Next(1, 5);
            this.alive = true;
            LoadContent(content);
            foreach (ModelMesh mesh in this.model.Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, mesh.BoundingSphere);
            }
            boundingSphere.Radius *= 1;

        }

        public void LoadContent(ContentManager content)
        {
            model = content.Load<Model>("models\\p1_saucer");
        }

        public void Update(GameTime gameTime)
        {
            if (alive)
            {
                position.Z -= speed * gameTime.ElapsedGameTime.Milliseconds;
                world = Matrix.CreateTranslation(position);

                if (position.Z <= -10000)
                {
                    position.Z = 5000;
                    alive = false;
                }
            }
        }

        public void Draw(Camera camera)
        {
            if (alive)
            {
                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.LightingEnabled = false;
                        effect.World = world * Matrix.CreateScale(0.001f);
                        effect.View = camera.View;
                        effect.Projection = camera.Projection;
                    }
        
                    mesh.Draw();
                }
                DebugShapeRenderer.AddBoundingSphere(boundingSphere, Color.Red);
            }

        }

    }
}
