using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incumming_Splash.src
{
    internal class GameManager
    {
        private Rectangle endRectangle;
        public GameManager(Rectangle endRectangle)
        {
            this.endRectangle = endRectangle;
        }

        public bool hasGameEnded(Rectangle playerHitbox)
        {
            return endRectangle.Intersects(playerHitbox);
        }
    }
}
