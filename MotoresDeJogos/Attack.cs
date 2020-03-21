using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MotoresDeJogos.Char;
using MotoresDeJogos.Projectiles;
using MotoresDeJogos.World;

namespace MotoresDeJogos
{
    public abstract class Attack
    {
        public abstract void Activate();
    }

    public class HeartAttack : Attack
    {
        int damage;
        string soundName;
        Matrix world;
        float rotation;

        public HeartAttack(Matrix world, float rotation, int damageAmount, string soundName)
        {
            this.world = world;
            this.rotation = rotation;
            damage = damageAmount;

            this.soundName = soundName;
        }

        public override void Activate()
        {
            Projectile projectile = ProjectilePool.GetProjectile();
            projectile.InitObject(world, rotation, 6000f, damage, WorldObjects.Projectiles[ProjectileTypes.Heart]);
            AudioManager.PlaySoundEffect(soundName);
        }
    }

    public class CloudAttack : Attack
    {
        int damage;
        string soundName;
        Matrix world;
        float rotation;

        public CloudAttack(Matrix world, float rotation, int damageAmount, string soundName)
        {
            this.world = world;
            this.rotation = rotation;
            damage = damageAmount;

            this.soundName = soundName;
        }

        public override void Activate()
        {
            Projectile projectile = ProjectilePool.GetProjectile();
            projectile.InitObject(world, rotation, 6000f, damage, WorldObjects.Projectiles[ProjectileTypes.Cloud]);
            AudioManager.PlaySoundEffect(soundName);
        }
    }

    public class BlueFireBallAttack : Attack
    {
        int damage;
        string soundName;
        Matrix world;
        float rotation;

        public BlueFireBallAttack(Matrix world, float rotation, int damageAmount, string soundName)
        {
            this.world = world;
            this.rotation = rotation;
            damage = damageAmount;

            this.soundName = soundName;
        }

        public override void Activate()
        {
            Projectile projectile = ProjectilePool.GetProjectile();
            projectile.InitObject(world, rotation, 6000f, damage, WorldObjects.Projectiles[ProjectileTypes.BlueFireBall]);
            AudioManager.PlaySoundEffect(soundName);
        }
    }

    public class SkullAttack : Attack
    {
        int damage;
        string soundName;
        Matrix world;
        float rotation;

        public SkullAttack(Matrix world, float rotation, int damageAmount, string soundName)
        {
            this.world = world;
            this.rotation = rotation;
            damage = damageAmount;

            this.soundName = soundName;
        }

        public override void Activate()
        {
            Projectile projectile = ProjectilePool.GetProjectile();
            projectile.InitObject(world, rotation, 6000f, damage, WorldObjects.Projectiles[ProjectileTypes.Skull]);
            AudioManager.PlaySoundEffect(soundName);
        }
    }

    public class ToxicAttack : Attack
    {
        int damage;
        string soundName;
        Matrix world;
        float rotation;

        public ToxicAttack(Matrix world, float rotation, int damageAmount, string soundName)
        {
            this.world = world;
            this.rotation = rotation;
            damage = damageAmount;

            this.soundName = soundName;
        }

        public override void Activate()
        {
            Projectile projectile = ProjectilePool.GetProjectile();
            projectile.InitObject(world, rotation, 6000f, damage, WorldObjects.Projectiles[ProjectileTypes.Toxic]);
            AudioManager.PlaySoundEffect(soundName);
        }
    }
}
