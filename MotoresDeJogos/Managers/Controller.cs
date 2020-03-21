using Microsoft.Xna.Framework;
using MotoresDeJogos.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos.Managers
{
    class Controller
    {
        public static class WorldBoundaries
        {
            public static Matrix FloorPosition;
            public static BoundingBox FloorBoundingBox;

            public static Matrix CentralOvalPosition;
            public static BoundingSphere CentralOvalSphere;

            public static Matrix LimitFront_Position;
            public static BoundingBox LimitFront_BoundingBox;

            public static Matrix LimitBack_Position;
            public static BoundingBox LimitBack_BoundingBox;

            public static Matrix LimitLeft_Position;
            public static BoundingBox LimitLeft_BoundingBox;

            public static Matrix LimitRight_Position;
            public static BoundingBox LimitRight_BoundingBox;
        }

        List<ACollidable> objects;

        DuckManager duckManager;
        

        public Controller(int objectsPool)
        {
            objects = new List<ACollidable>();

            /*Vector3 position = new Vector3();
            int counter = 0;
            int distanceBetweenRows = 0;
            float startX = -25;
            for (int i = 0; i < objectsPool; i++)
            {
                if (counter == 50)
                {
                    distanceBetweenRows += 3;
                    counter = 0;
                    startX = -25;
                }
                startX += 2.5f;
                position = new Vector3(startX, distanceBetweenRows, distanceBetweenRows);
                //objects.Add(new DuckEnemy(position, i, 0.8f));
                counter++;
            }*/

            SetupWorldColliders();
        }

        public void SetupWorldColliders()
        {
            #region Floor Setup
            WorldBoundaries.FloorPosition = Matrix.CreateTranslation(0, 4f, 0);
            WorldBoundaries.FloorBoundingBox = new BoundingBox(new Vector3(-25000f, -1500f, -20000f), new Vector3(25000f, -1450f, 18000f));
            #endregion

            #region Center Piece
            WorldBoundaries.CentralOvalPosition = Matrix.CreateTranslation(0, -5000f, 0);
            WorldBoundaries.CentralOvalSphere = new BoundingSphere(WorldBoundaries.CentralOvalPosition.Translation, 7000f);
            #endregion

            #region Boundaries Outside the lake
            // FRONT
            WorldBoundaries.LimitFront_Position = Matrix.CreateTranslation(5000f, -7000f, 0);
            WorldBoundaries.LimitFront_BoundingBox = new BoundingBox(new Vector3(-26000f, -1500f, -17000f), new Vector3(26000f, 1500f, -16900f));

            // BACK
            WorldBoundaries.LimitBack_Position = Matrix.CreateTranslation(-5000f, -7000f, 0f);
            WorldBoundaries.LimitBack_BoundingBox = new BoundingBox(new Vector3(-26000f, -1500f, 17000f), new Vector3(26000f, 1500f, 16900f));

            // LEFT SIDE
            WorldBoundaries.LimitLeft_Position = Matrix.CreateTranslation(5000f, -7000f, 0) * Matrix.CreateRotationY(90f);
            WorldBoundaries.LimitLeft_BoundingBox = new BoundingBox(new Vector3(-21000f, -1500f, -26000f), new Vector3(-20900f, 1500f, 26000f));

            // RIGHT SIDE
            WorldBoundaries.LimitRight_Position = Matrix.CreateTranslation(-5000f, -7000f, 0) * Matrix.CreateRotationY(90f);
            WorldBoundaries.LimitRight_BoundingBox = new BoundingBox(new Vector3(21000f, -1500f, -26000f), new Vector3(20900f, 1500f, 26000f));
            #endregion
        }

        public void Update(GameTime gameTime)
        {
            /*foreach (ACollidable _object in objects)
            {
                _object.Update(gameTime);
            }*/
        }

        public void Draw()
        {
            if (Game1.DebugMode)
            {
                DebugShapeRenderer.AddBoundingBox(WorldBoundaries.FloorBoundingBox, Color.Gold);
                DebugShapeRenderer.AddBoundingSphere(WorldBoundaries.CentralOvalSphere, Color.Gray);
                DebugShapeRenderer.AddBoundingBox(WorldBoundaries.LimitFront_BoundingBox, Color.Gray);
                DebugShapeRenderer.AddBoundingBox(WorldBoundaries.LimitBack_BoundingBox, Color.Gray);
                DebugShapeRenderer.AddBoundingBox(WorldBoundaries.LimitLeft_BoundingBox, Color.Gray);
                DebugShapeRenderer.AddBoundingBox(WorldBoundaries.LimitRight_BoundingBox, Color.Gray);
            }
                
        }
        
        public BoundingBox FloorCollider()
        {
            return WorldBoundaries.FloorBoundingBox;
        }

        public List<ACollidable> GetObjects()
        {
            return objects;
        }
    }
}
