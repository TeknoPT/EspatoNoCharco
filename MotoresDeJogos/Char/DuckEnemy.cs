using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MotoresDeJogos.Abstracts;
using MotoresDeJogos.Char;
using MotoresDeJogos.Interfaces;
using MotoresDeJogos.Managers;
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
        Vector3 currentGravityValue;

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
        
        public DuckEnemy(Vector3 position, Random random, Model model)
        {
            Initialize(position, random, model);
        }

        private void Initialize(Vector3 position, Random random, Model model)
        {
            currentGravityValue = new Vector3(0, -5f, 0);
            this.random = random;
            OnGround = false;
            this.world = Matrix.CreateTranslation(position);
            this.speed = (float)random.Next(1, 20);
            if (this.speed == 0) this.speed = (float)random.Next(1, 20);
            this.health = 100f;
            this.Model = model;

            #region Creating Bounds
            foreach (ModelMesh mesh in this.model.Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(boundingSphere, mesh.BoundingSphere);
            }
            #endregion
        }

        public void Update(float deltaTime)
        {
            if (!IsDead())
            {
                if (!OnGround)
                {
                    currentGravityValue += Physics.GravityAmount();
                    Vector3 valueToAdd = (currentGravityValue) * deltaTime;
                    world = Matrix.CreateTranslation(world.Translation + valueToAdd);
                }
                else
                {
                    //Vector3 valueToAdd =  speed * deltaTime;
                    //worldPosition = Matrix.CreateRotationY(currentRotation + rotation) * Matrix.CreateTranslation(world.Translation + valueToAdd);
                }

                //currentRotation += rotation;
                boundingSphere.Center = world.Translation;
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

        #region Draw
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
                        effect.View = Player.cameraView;
                        effect.Projection = Player.Projection();
                    }

                    mesh.Draw();
                }
                DebugShapeRenderer.AddBoundingSphere(boundingSphere, Color.Black);
            }
        }
        #endregion
    }
}
