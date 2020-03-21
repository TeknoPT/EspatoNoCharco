using Microsoft.Xna.Framework;
using MotoresDeJogos.Managers;
using MotoresDeJogos.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos.Projectiles
{
    class ProjectilePool
    {
        static Queue<Projectile> projectilesPool;
        static List<Projectile> projectiles;

        public static void Init()
        {
            projectilesPool = new Queue<Projectile>();
            projectiles = new List<Projectile>();
        }

        public static Projectile GetProjectile()
        {
            if(projectilesPool.Count > 0)
            {
                return projectilesPool.Dequeue();
            }
            else
            {
                Projectile projectile = new Projectile(WorldObjects.Projectile);
                projectiles.Add(projectile);
                CollisionDetection.AddObject(projectile);
                return projectile;
            }
        }

        public static void Update(float deltaTime)
        {
            foreach(Projectile projectile in projectiles )
            {
                if (!projectile.IsDead())
                {
                    projectile.Update(deltaTime);
                }
            }
        }

        public static void Draw()
        {
            foreach (Projectile projectile in projectiles)
            {
                if (!projectile.IsDead())
                {
                    projectile.Draw();
                }
            }
        }

        public static void EnqueueProjectile(Projectile projectile)
        {
            projectilesPool.Enqueue(projectile);
        }
    }
}
