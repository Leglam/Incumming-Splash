using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incumming_Splash.src
{
    internal class Animation
    {
        Texture2D spriteSheet;
        int frames;
        int rows = 0;
        int c = 0;
        float timeSinceLastFrame = 0;
        public Animation(Texture2D spriteSheet, float width = 32, float height = 32) 
        { 
            this.spriteSheet = spriteSheet;
            frames = (int)(spriteSheet.Width / width);
            Console.WriteLine(frames);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime, float millisecondsFrames = 500, SpriteEffects effect = SpriteEffects.None)
        {
            if (c < frames) 
            { 
                var rect = new Rectangle(40 * c, rows, 64, spriteSheet.Height);
                spriteBatch.Draw(spriteSheet, position, rect, Color.White, 0f, new Vector2(), 1f, effect, 1);
                timeSinceLastFrame += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timeSinceLastFrame > millisecondsFrames)
                {
                    timeSinceLastFrame -= millisecondsFrames;
                    c++;
                }
            } else
            {
                c = 0;
            }
        }
    }
}
