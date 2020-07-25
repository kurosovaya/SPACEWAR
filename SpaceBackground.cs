using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Spacewar
{
    class SpaceBackground
    {
        private int width;
        private int height;
        private int starsCount;
        private Vector2[] starsVectors;
        public Texture2D[] startsTextures;
        public Texture2D[] starsTypes;
        private int speed = 1;
        private float angle = 270f;

        public SpaceBackground(int width, int height, int starsCount, Texture2D[] starsTypes)
        {
            this.height = height;
            this.width = width;
            this.starsCount = starsCount;
            this.starsTypes = starsTypes;
        }

        public void StarsGenerate()
        {
            starsVectors = new Vector2[starsCount];
            startsTextures = new Texture2D[starsCount];
            Random random = new Random();

            for (int i = 0; i < starsCount; i++) {
                starsVectors[i] = new Vector2(random.Next(width), random.Next(height));
                startsTextures[i] = starsTypes[random.Next(starsTypes.Length)];
            }
        }

        public void Next(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            for (int i = 0; i < starsCount; i++) {
                starsVectors[i].X += (float)(speed * Math.Cos(MathHelper.ToRadians(angle)));
                starsVectors[i].Y -= (float)(speed * Math.Sin(MathHelper.ToRadians(angle)));

                if (starsVectors[i].Y > height)
                    starsVectors[i].Y = 0;

                spriteBatch.Draw(
                    startsTextures[i],
                    new Rectangle((int)starsVectors[i].X,
                    (int)starsVectors[i].Y, 7, 7),
                    null,
                    Color.White,
                    0f,
                    new Vector2(startsTextures[i].Width / 2,
                    startsTextures[i].Height / 2),
                    SpriteEffects.None,
                    0f);
            }
            spriteBatch.End();
        }
    }
}
