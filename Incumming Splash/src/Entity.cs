using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incumming_Splash.src
{
    public abstract class Entity
    {
        public Texture2D spriteSheet;
        public Vector2 position;

        public enum currentAnimation
        {
            Idle,
            Run,

        }

        public abstract void Update();
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
