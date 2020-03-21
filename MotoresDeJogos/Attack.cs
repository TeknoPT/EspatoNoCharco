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
        // Projectile

        public HeartAttack(int damageAmount, string soundName)
        {
            damage = damageAmount;

            this.soundName = soundName;
        }

        public override void Activate()
        {
            Projectile projectile = ProjectilePool.GetProjectile();
            projectile.InitObject(Player.WorldPosition(), Player.GetRotation(), 6000f, damage, WorldObjects.Projectiles[ProjectileTypes.Heart]);
            AudioManager.PlaySoundEffect(soundName);
        }
    }

    public class CloudAttack : Attack
    {
        int damage;
        string soundName;
        // Projectile

        public CloudAttack(int damageAmount, string soundName)
        {
            damage = damageAmount;

            this.soundName = soundName;
        }

        public override void Activate()
        {
            Projectile projectile = ProjectilePool.GetProjectile();
            projectile.InitObject(Player.WorldPosition(), Player.GetRotation(), 6000f, damage, WorldObjects.Projectiles[ProjectileTypes.Cloud]);
            AudioManager.PlaySoundEffect(soundName);
        }
    }

    public class BlueFireBallAttack : Attack
    {
        int damage;
        string soundName;
        // Projectile

        public BlueFireBallAttack(int damageAmount, string soundName)
        {
            damage = damageAmount;

            this.soundName = soundName;
        }

        public override void Activate()
        {
            Projectile projectile = ProjectilePool.GetProjectile();
            projectile.InitObject(Player.WorldPosition(), Player.GetRotation(), 6000f, damage, WorldObjects.Projectiles[ProjectileTypes.BlueFireBall]);
            AudioManager.PlaySoundEffect(soundName);
        }
    }

    public class SkullAttack : Attack
    {
        int damage;
        string soundName;
        // Projectile

        public SkullAttack(int damageAmount, string soundName)
        {
            damage = damageAmount;

            this.soundName = soundName;
        }

        public override void Activate()
        {
            Projectile projectile = ProjectilePool.GetProjectile();
            projectile.InitObject(Player.WorldPosition(), Player.GetRotation(), 6000f, damage, WorldObjects.Projectiles[ProjectileTypes.Skull]);
            AudioManager.PlaySoundEffect(soundName);
        }
    }

    public class ToxicAttack : Attack
    {
        int damage;
        string soundName;
        // Projectile

        public ToxicAttack(int damageAmount, string soundName)
        {
            damage = damageAmount;

            this.soundName = soundName;
        }

        public override void Activate()
        {
            Projectile projectile = ProjectilePool.GetProjectile();
            projectile.InitObject(Player.WorldPosition(), Player.GetRotation(), 6000f, damage, WorldObjects.Projectiles[ProjectileTypes.Toxic]);
            AudioManager.PlaySoundEffect(soundName);
        }
    }
}
