using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace MotoresDeJogos
{
    class Camera
    {
        private Matrix projection;

        private float x = 0;
        private float y = 0;
        private float z = 0;
        private Vector3 lookAtVector = Vector3.Zero;
        private float moveSpeed = 50f;

        public Matrix Projection
        {
            get { return projection; }
            set { projection = value; }
        }

        private Matrix view;

        public Matrix View
        {
            get { return view; }
            set { view = value; }
        }

        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        private MouseState mouse;

        public Camera(Vector3 position, GraphicsDeviceManager graphics)
        {
            mouse = new MouseState();
            view = Matrix.CreateLookAt(Vector3.Zero, lookAtVector, Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(50f), graphics.PreferredBackBufferWidth / graphics.PreferredBackBufferHeight, 0.01f, 100000);
        }

        public void Update(GameTime gameTime)
        {

            
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                lookAtVector.Y += moveSpeed;
                MessageBus.InsertNewMessage(new ConsoleMessage(String.Format("y: {0}", lookAtVector.Y)));
                view = Matrix.CreateLookAt(Vector3.Zero, lookAtVector, Vector3.Up);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                lookAtVector.X += moveSpeed;
                MessageBus.InsertNewMessage(new ConsoleMessage(String.Format("x: {0}", lookAtVector.X)));
                view = Matrix.CreateLookAt(Vector3.Zero, lookAtVector, Vector3.Up);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                lookAtVector.Z += moveSpeed;
                MessageBus.InsertNewMessage(new ConsoleMessage(String.Format("z: {0}", lookAtVector.Z)));
                view = Matrix.CreateLookAt(Vector3.Zero, lookAtVector, Vector3.Up);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                lookAtVector = Vector3.Zero;
                view = Matrix.CreateLookAt(Vector3.Zero, Vector3.Forward, Vector3.Up);
            }

        }
    }
}
