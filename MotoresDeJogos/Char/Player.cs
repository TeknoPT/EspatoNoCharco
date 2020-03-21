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
    public class Player
    {
        static MouseState originalMouseState;

        public static Matrix cameraView;
        static Matrix worldPosition;
        static Matrix projection;

        static Model playerModel;
        static BoundingSphere boundingSphere;
        static BoundingFrustum frustum;

        static float currentRotation;
        static float movementSpeed;
        static float moveSoundTimer;

        static Vector3 currentGravityValue;

        static RasterizerState rasterizerStateSolid;

        #region Collision Variable
        static bool collision;

        public static bool Collision
        {
            get { return collision; }
            set { collision = value; }
        }
        #endregion

        public static void Init(float _speed, GraphicsDevice graphics, Model model)
        {
            playerModel = model;
            collision = false;
            originalMouseState = Mouse.GetState();
            cameraView = Matrix.CreateLookAt(Vector3.Zero, Vector3.Forward, Vector3.Up);
            worldPosition = Matrix.CreateTranslation(new Vector3(0, 500f, 10000f));
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60), graphics.Viewport.AspectRatio, 1f, 80000);
            rasterizerStateSolid = new RasterizerState();
            rasterizerStateSolid.CullMode = CullMode.None;
            rasterizerStateSolid.MultiSampleAntiAlias = true;
            rasterizerStateSolid.FillMode = FillMode.Solid;
            rasterizerStateSolid.SlopeScaleDepthBias = 0.1f;
            graphics.RasterizerState = rasterizerStateSolid;
            currentGravityValue = new Vector3(0, -98f, 0);
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

        public static void Move(Keys key, float deltaTime)
        {
            Vector3 moveVector = new Vector3(0, 0, 0);
            switch (key)
            {
                case Keys.Up:
                    moveVector += worldPosition.Forward;
                    break;
                case Keys.Down:
                    moveVector += worldPosition.Backward;
                    break;
                case Keys.Right:
                    moveVector += worldPosition.Right;
                    break;
                case Keys.Left:
                    moveVector += worldPosition.Left;
                    break;
            }
            if (moveSoundTimer <= 0)
            {
                AudioManager.PlaySoundEffect("Movement");
                moveSoundTimer = 0.5f;
            }
            MovePlayer(moveVector, 0, deltaTime);
        }

        public static void Rotate(Keys key, float deltaTime)
        {
            float rotationAmount = 0;

            if (key == Keys.Q)
                rotationAmount = 0.05f;
            else if (key == Keys.E)
                rotationAmount = -0.05f;
            MovePlayer(Vector3.Zero, rotationAmount, deltaTime);
        }

        private static void MovePlayer(Vector3 vectorToAdd, float rotation, float deltaTime)
        {
            if (!collision)
            {
                currentGravityValue += Physics.GravityAmount();
                Vector3 valueToAdd = (vectorToAdd * movementSpeed + currentGravityValue) * deltaTime;
                worldPosition =  Matrix.CreateRotationY(currentRotation + rotation) * Matrix.CreateTranslation(worldPosition.Translation + valueToAdd);
            }
            else
            {
                Vector3 valueToAdd = vectorToAdd * movementSpeed * deltaTime;
                worldPosition = Matrix.CreateRotationY(currentRotation + rotation) * Matrix.CreateTranslation(worldPosition.Translation + valueToAdd);
            }

            currentRotation += rotation;
            boundingSphere.Center = worldPosition.Translation;
        }

        public static float GetRotation()
        {
            return currentRotation;
        }



        public static void Update(float deltaTime)
        {
            if (moveSoundTimer > 0)
            {
                moveSoundTimer -= deltaTime;
            }
            MovePlayer(Vector3.Zero, 0f, deltaTime);

            Vector3 cameraPosition = worldPosition.Translation + (worldPosition.Backward * 1500) + (worldPosition.Up * 700);
            Vector3 cameraTarget = worldPosition.Translation + (worldPosition.Forward * 3000);
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
            DebugShapeRenderer.AddBoundingSphere(boundingSphere, Color.Black);
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
