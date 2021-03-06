﻿using Microsoft.Xna.Framework;
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

        protected bool onWall;

        public bool OnWall
        {
            get { return onWall; }
            set { onWall = value; }
        }

        protected bool onGround;

        public bool OnGround
        {
            get { return onGround; }
            set { onGround = value; }
        }
        
        public bool IsColliding(BoundingSphere bounding)
        {
            return boundingSphere.Intersects(bounding);
        }

        public bool IsColliding(BoundingBox bounding)
        {
            return boundingSphere.Intersects(bounding);
        }

        public virtual void Damage()
        {
            if ( health > 0)
            {
                health -= 25;
            }
        }

        public virtual void Damage(float damage)
        {
            health -= 25;
        }

        public virtual bool IsDead()
        {
            return health <= 0;
        }

        public virtual void Destroy()
        {
            health = 0;
        }
    }
}
