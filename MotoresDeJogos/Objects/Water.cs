using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MotoresDeJogos.Abstracts;
using MotoresDeJogos.Char;
using MotoresDeJogos.Interfaces;
using MotoresDeJogos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos.Objects
{
    class Water : ACollidable, IStatic, IGenerate, IMDrawable
    {
        private WaterModel model;

        public WaterModel Model
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

        private Matrix world;

        public Water(WaterModel model)
        {
            this.model = model;
            this.position = Generate();
            this.world = Matrix.CreateTranslation(position);

            #region Creating Bounds
            boundingBox = GetBounds();
            #endregion
        }

        public void Draw()
        {
            foreach (ModelMesh mesh in model.Model.Meshes)
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
            
        }

        public BoundingBox GetBounds()
        {
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            foreach (ModelMesh mesh in this.Model.Model.Meshes)
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

        public bool IsStatic()
        {
            return true;
        }

        public Vector3 Generate()
        {
            return new Vector3(0,-11000f,0);
        }

        public override void Damage()
        {

        }

        public override bool IsDead()
        {
            return false;
        }

        public override void Destroy()
        {

        }

    }
}
