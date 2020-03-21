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

    public class RedAttack : Attack
    {
        int damage;
        string soundName;
        // Projectile

        public RedAttack(int damageAmount, string soundName)
        {
            damage = damageAmount;

            this.soundName = soundName;
        }

        public override void Activate()
        {
            Projectile projectile = ProjectilePool.GetProjectile();
            projectile.InitObject(Player.WorldPosition(), Player.GetRotation(), 6000f, damage, WorldObjects.Projectile);
            AudioManager.PlaySoundEffect(soundName);
        }
    }
}
