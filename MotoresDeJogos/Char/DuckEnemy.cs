using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MotoresDeJogos.Abstracts;
using MotoresDeJogos.Interfaces;
using MotoresDeJogos.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos
{
    class DuckEnemy :  ACollidable
    {
        private Random random;

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
        
        public DuckEnemy(Vector3 position, ContentManager content, Random random, Model model)
        {
            Initialize(this.position, content, random, model);
        }

        public DuckEnemy(ContentManager content, Random random, Model model)
        {
            this.position = new Vector3(random.Next(-WorldGeneration.MAP_SIZE, WorldGeneration.MAP_SIZE), 0, random.Next(-WorldGeneration.MAP_SIZE, WorldGeneration.MAP_SIZE));
            Initialize(this.position, content, random, model);
        }

        private void Initialize(Vector3 position, ContentManager content, Random random, Model model)
        {
            this.random = random;
            this.world = Matrix.CreateTranslation(position);
            this.speed = (float)random.Next(1, 20);
            if (this.speed == 0) this.speed = (float)random.Next(1, 20);
            this.health = 100f;
            this.Model = model;

            #region Creating Bounds
            foreach (ModelMesh mesh in this.model.Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, mesh.BoundingSphere);
            }
            #endregion
        }

        public void Update(GameTime gameTime)
        {
            if (!IsDead())
            {
                RandomMovement(gameTime);
            }
        }

        public void Draw()
        {
            if (!IsDead())
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

        private void RandomMovement(GameTime gameTime)
        {
            // Random movement
            position.Z += random.Next(-10, 10) * speed * gameTime.ElapsedGameTime.Milliseconds;
            position.X += random.Next(-10, 10) * speed * gameTime.ElapsedGameTime.Milliseconds;
            world = Matrix.CreateTranslation(position);

            boundingSphere.Center = position;
        }
    }
}
