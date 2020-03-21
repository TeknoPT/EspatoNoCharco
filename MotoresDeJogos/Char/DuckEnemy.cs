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

        Vector3 valueToAdd;

        private Matrix AttackWorld;

        private DuckTypes duckModelType;

        Command command;

        private Random random;

        private float moveSoundTimer;

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

        private float rotation;

        private Matrix world;

        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        
        public DuckEnemy(Vector3 position, Random random, Model model, DuckTypes duckModelType)
        {
            this.random = random;
            currentGravityValue = Physics.GravityAmount();
            OnGround = false;
            OnWall = false;
            rotation = 0f;
            this.world = Matrix.CreateTranslation(position);
            this.speed = (float)random.Next(1, 20);
            if (this.speed == 0) this.speed = (float)random.Next(1, 20);

            this.duckModelType = duckModelType;
            this.Model = model;
            command = new AttackCommand(world, rotation, duckModelType);
            
            this.health = 100f;
            moveSoundTimer = (float)random.Next(1, 5);

            #region Creating Bounds
            foreach (ModelMesh mesh in this.model.Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(boundingSphere, mesh.BoundingSphere);
            }
            #endregion
        }

        public void AI()
        {

        }

        public void Update(float deltaTime)
        {
            if (!IsDead())
            {
                if (!OnGround)
                {
                    currentGravityValue += Physics.GravityAmount();
                    valueToAdd = (currentGravityValue) * deltaTime;
                    world = Matrix.CreateTranslation(world.Translation + valueToAdd);
                }
                else
                {
                    AI();
                    // valueToAdd =  speed * deltaTime;
                    //worldPosition = Matrix.CreateRotationY(currentRotation + rotation) * Matrix.CreateTranslation(world.Translation + valueToAdd);
                }

                if (moveSoundTimer > 0)
                {
                    moveSoundTimer -= deltaTime;
                }
                else
                {
                    AttackWorld = Matrix.CreateTranslation(world.Translation + world.Forward * 50);
                    ((AttackCommand)command).UpdateVariables(world, rotation, duckModelType);
                    //command.Execute();
                    moveSoundTimer = (float) random.Next(1, 10);
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

                if (Game1.DebugMode)
                    DebugShapeRenderer.AddBoundingSphere(boundingSphere, Color.Black);
            }
        }
        #endregion
    }
}
