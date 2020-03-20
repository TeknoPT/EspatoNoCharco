using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MotoresDeJogos.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos.Char
{
    class Player
    {
        static MouseState originalMouseState;

        public static Matrix cameraView;
        static Matrix worldPosition;
        static Matrix projection;

        static Model playerModel;
        static BoundingSphere boundingSphere;

        static float currentRotation;
        static float movementSpeed;

        static Vector3 currentGravityValue;

        private static InputManager inputManager;

        #region Collision Variable
        static bool collision;
        public static bool Collision
        {
            get { return collision; }
            set { collision = value; }
        }
        #endregion

        public static void Init(float _speed, InputManager _inputManager, Model model)
        {
            playerModel = model;
            inputManager = _inputManager;
            collision = false;
            originalMouseState = Mouse.GetState();
            worldPosition = Matrix.CreateTranslation(new Vector3(-6f, 2f, 0));
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.1f, 1500f);
            currentGravityValue = new Vector3(0, 0, 0);
            movementSpeed = _speed;

            #region Creating Bounds
            foreach (ModelMesh mesh in playerModel.Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(boundingSphere, mesh.BoundingSphere);
            }
            #endregion
        }

        public static Matrix WorldPosition()
        {
            return worldPosition;
        }

        public static Matrix Projection()
        {
            return projection;
        }
        
        public static void LoadModel(Model model)
        {
            playerModel = model;
        }

        private static void ProcessInput(float amount)
        {
            // Keyboard controllers to be replaced with Input Manager
            Vector3 moveVector = new Vector3(0, 0, 0);

            if (inputManager.JustPressed(Keys.Up))
                moveVector += worldPosition.Forward;
            if (inputManager.JustPressed(Keys.Down))
                moveVector += worldPosition.Backward;
            if (inputManager.JustPressed(Keys.Right))
                moveVector += worldPosition.Right;
            if (inputManager.JustPressed(Keys.Left))
                moveVector += worldPosition.Left;

            float rotationAmount = 0;

            if (inputManager.JustPressed(Keys.Q))
                rotationAmount = 0.05f;
            else if (inputManager.JustPressed(Keys.E))
                rotationAmount = -0.05f;

            MovePlayer(moveVector, rotationAmount, amount);
        }

        private static void MovePlayer(Vector3 vectorToAdd, float rotation, float deltaTime)
        {
            if (!Player.collision)
            {
                currentGravityValue += Physics.GravityAmount();
                Vector3 valueToAdd = (vectorToAdd * movementSpeed + currentGravityValue) * deltaTime;
                worldPosition = Matrix.CreateRotationY(currentRotation + rotation) * Matrix.CreateTranslation(worldPosition.Translation + valueToAdd);
            }
            else
            {
                Vector3 valueToAdd = vectorToAdd * movementSpeed * deltaTime;
                worldPosition = Matrix.CreateRotationY(currentRotation + rotation) * Matrix.CreateTranslation(worldPosition.Translation + valueToAdd);
            }

            currentRotation += rotation;
            boundingSphere.Center = worldPosition.Translation;
        }



        public static void Update(GameTime gameTime)
        {
            float timeDifference = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
            ProcessInput(timeDifference);

            Vector3 cameraPosition = worldPosition.Translation + (worldPosition.Backward * 7) + (worldPosition.Up * 2);
            Vector3 cameraTarget = worldPosition.Translation + (worldPosition.Forward * 10);
            cameraView = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Up);

            /*foreach (Ship ship in Physics.GetShips())
            {
                ship.SetInFrustumView(InView(ship.Collider()));
            }*/
        }

        public static void Draw()
        {
            foreach (ModelMesh mesh in playerModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = worldPosition;
                    effect.View = cameraView;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }

        public static BoundingSphere PlayerCollider()
        {
            return boundingSphere;
        }

        static bool InView(BoundingSphere boundingSphere)
        {
            return new BoundingFrustum(cameraView * projection).Intersects(boundingSphere);
        }
    }
}
