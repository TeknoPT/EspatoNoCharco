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
        List<ACollidable> objects;

        DuckManager duckManager;

        Matrix floorPosition;
        BoundingBox floorBoundingBox;

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

            floorPosition = Matrix.CreateTranslation(0, 4f, 0);
            floorBoundingBox = new BoundingBox(new Vector3(-25000f, -1500f, -20000f), new Vector3(25000f, -1499f, 18000f));
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
            DebugShapeRenderer.AddBoundingBox(floorBoundingBox, Color.Gold);
        }

        public BoundingBox FloorCollider()
        {
            return floorBoundingBox;
        }

        public List<ACollidable> GetObjects()
        {
            return objects;
        }
    }
}
