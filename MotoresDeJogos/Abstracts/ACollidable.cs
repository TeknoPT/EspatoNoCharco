using Microsoft.Xna.Framework;
using MotoresDeJogos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos.Abstracts
{
    abstract class ACollidable : ICollide, IKillable
    {
        protected BoundingSphere boundingSphere;

        public BoundingSphere BoundingShpere
        {
            get { return boundingSphere; }
            set { boundingSphere = value; }
        }

        protected BoundingBox boundingBox;

        public BoundingBox BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }

        protected float health;

        public float Health
        {
            get { return health; }
            set { health = value; }
        }

        public bool IsColliding(BoundingSphere bounding)
        {
            if (boundingBox != null)
                return boundingBox.Intersects(bounding);

            return boundingSphere.Intersects(bounding);
        }

        public bool IsColliding(BoundingBox bounding)
        {
            if (boundingBox != null)
                return boundingBox.Intersects(bounding);

            return boundingSphere.Intersects(bounding);
        }

        public void Damage()
        {
            if ( health > 0)
            {
                health -= 25;
            }
        }

        public bool IsDead()
        {
            return health <= 0;
        }

        public void Destroy()
        {
            health = 0;
        }
    }
}
