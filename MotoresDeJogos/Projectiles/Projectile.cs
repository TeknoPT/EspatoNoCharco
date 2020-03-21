using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MotoresDeJogos.Abstracts;
using MotoresDeJogos.Char;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos.Projectiles
{
    class Projectile : ACollidable
    {
        float speed;
        int damage;
        float rotation;
        const float LIFE = 3f;
        float lifeTime;
        Model model;
        Matrix worldPosition;
        Vector3 valueToAdd;


        public Projectile(Model model)
        {
            this.model = model;
            health = 0;
        }

        public void InitObject(Matrix position, float rotation, float speed, int damage, Model model)
        {
            this.model = model;

            boundingSphere.Radius = 0;
            health = 1;
            worldPosition = position;
            this.speed = speed;
            this.damage = damage;
            this.rotation = rotation;

            boundingSphere = new BoundingSphere(worldPosition.Translation, 250f);
        }

        public void Update(float deltaTime)
        {
            valueToAdd = (worldPosition.Forward * speed) * deltaTime;
            worldPosition = Matrix.CreateRotationY(rotation) * Matrix.CreateTranslation(worldPosition.Translation + valueToAdd);
            boundingSphere.Center = worldPosition.Translation;
            if (lifeTime > 0)
            {
                lifeTime -= deltaTime;
            }
            else
            {
                lifeTime = LIFE;
                Destroy();
                ProjectilePool.EnqueueProjectile(this);
            }
        }

        public override void Destroy()
        {
            lifeTime = LIFE;
            ProjectilePool.EnqueueProjectile(this);
            base.Destroy();
        }

        public void Draw()
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.LightingEnabled = true;
                    effect.EnableDefaultLighting();
                    effect.World = worldPosition;
                    effect.View = Player.cameraView;
                    effect.Projection = Player.Projection();
                }
                mesh.Draw();
            }
            if (Game1.DebugMode)
                DebugShapeRenderer.AddBoundingSphere(boundingSphere, Color.Black);
        }
    }
}
