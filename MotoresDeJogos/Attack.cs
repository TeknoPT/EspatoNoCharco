using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
            // Spawn of projectile
            AudioManager.PlaySoundEffect(soundName);
        }
    }
}
