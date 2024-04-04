using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incumming_Splash.src
{
    internal class Bullet
    {
        private Texture2D bulletTexture;
        private float speed = 4;
        public Rectangle hitbox;

        public Bullet(Texture2D bulletTexture, float speed, Rectangle hitbox)
        {
            this.bulletTexture = bulletTexture;
            this.speed = speed;
            this.hitbox = hitbox;
        }

        public void Update()
        {
            hitbox.X += (int)speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bulletTexture, hitbox, Color.White);
        }
    }
}
