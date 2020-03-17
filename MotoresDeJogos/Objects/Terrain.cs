using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MotoresDeJogos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos.Objects
{
    class Terrain : ICollide, IStatic, IGenerate, IMDrawable
    {
        private Model model;

        public Model Model
        {
            get { return model; }
            set { model = value; }
        }

        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        private BoundingBox boundingBox;

        public BoundingBox BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }

        private Matrix world;


        public Terrain()
        {
            model = World.WorldObjects.Terrain;
            this.position = Generate();
            this.world = Matrix.CreateTranslation(position);

            #region Creating Bounds

            boundingBox = GetBounds();
            #endregion
        }

        public void Draw()
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.LightingEnabled = false;
                    effect.World = world;
                    effect.View = ModedCamera.View;
                    effect.Projection = ModedCamera.Projection;
                }

                mesh.Draw();
            }

            DebugShapeRenderer.AddBoundingBox(boundingBox, Color.Magenta);
        }

        public BoundingBox GetBounds()
        {
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            foreach (ModelMesh mesh in this.Model.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    int vertexStride = meshPart.VertexBuffer.VertexDeclaration.VertexStride;
                    int vertexBufferSize = meshPart.NumVertices * vertexStride;

                    int vertexDataSize = vertexBufferSize / sizeof(float);
                    float[] vertexData = new float[vertexDataSize];
                    meshPart.VertexBuffer.GetData<float>(vertexData);

                    for (int i = 0; i < vertexDataSize; i += vertexStride / sizeof(float))
                    {
                        Vector3 vertex = new Vector3(vertexData[i], vertexData[i + 1], vertexData[i + 2]);
                        min = Vector3.Min(min, vertex);
                        max = Vector3.Max(max, vertex);
                    }
                }
            }

            return new BoundingBox(min, max);
        }

        // Interfaces
        public bool IsColliding(BoundingSphere bounding)
        {
            return boundingBox.Intersects(boundingBox);
        }

        public bool IsColliding(BoundingBox bounding)
        {
            return boundingBox.Intersects(boundingBox);
        }

        public bool IsStatic()
        {
            return true;
        }

        public Vector3 Generate()
        {
            return Vector3.Zero;
        }
    }
}
