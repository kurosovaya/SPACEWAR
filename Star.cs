using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacewar
{
    class Star : Entity2D
    {
        Star(int startX, int startY, Texture2D texture, float scale, float angle = 0f) : base(startX, startY, texture, scale, angle)
        {

        }

        public override void Collision(GraphicsDeviceManager graphics)
        {
            if (starsVectors[i].Y < startsTextures[i].Height / 2)
                starsVectors[i].Y = height - startsTextures[i].Height / 2;
            else if (starsVectors[i].X < startsTextures[i].Width / 2)
                starsVectors[i].X = width - startsTextures[i].Width / 2;
            else if (starsVectors[i].Y > height - startsTextures[i].Height / 2)
                starsVectors[i].Y = 0 + startsTextures[i].Height / 2;
            else if (starsVectors[i].X > width - startsTextures[i].Width / 2)
                starsVectors[i].X = 0 + startsTextures[i].Width / 2;
        }
    }
}
