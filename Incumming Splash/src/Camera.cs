using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incumming_Splash.src
{
    internal class Camera
    {
        public Matrix Transform;

        public Matrix Follow(Rectangle target)
        {
            target.X = MathHelper.Clamp(target.X, (int)(0+Main.ScreenW/2), (int)(1550-Main.ScreenW/2));
            target.Y = Main.ScreenH/2 + 25;
            Vector3 translation = new Vector3(
                    -target.X - target.Width / 2,
                    -target.Y - target.Height / 2,
                    0
                );

            Vector3 offset = new Vector3(Main.ScreenW/2, Main.ScreenH/2, 0);

            Transform = Matrix.CreateTranslation(translation)*Matrix.CreateTranslation(offset);
            return Transform;
        }
    }
}
