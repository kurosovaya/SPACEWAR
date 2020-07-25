using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Spacewar
{
    public class Entity2D
    {
        int startX { get; set; }
        int startY { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public int Height { get; private set; }
        public int Width { get; private set; }        
        public Vector2 Possition
        {
            get {
                return new Vector2(X, Y);
            }
        }
        public float Angle = 90f;

        public Texture2D texture { get; set; }

        public Entity2D(int startX, int startY, Texture2D texture, float angle = 0f)
        {
            this.startX = startX;
            this.startY = startY;
            X = startX;
            Y = startY;
            this.texture = texture;
            this.Width = texture.Width;
            this.Height = texture.Height;
            this.Angle = angle;
        }

        public Entity2D(int startX, int startY, Texture2D texture, float scale, float angle = 0f) : this(startX, startY, texture, angle)
        {
            Height = (int)(texture.Height * scale);
            Width = (int)(texture.Width * scale);
        }

        public Entity2D(int startX, int startY, Texture2D texture, int height, int width, float angle = 0f) : this(startX, startY, texture, angle)
        {
            Height = height;
            Width = width;
        }        

        public void Move(float x, float y)
        {
            X += x;
            Y += y;
        }

        public void Teleport(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,
                new Rectangle((int)X, (int)Y, Width, Height),
                null,
                Color.White,
                MathHelper.ToRadians(Angle - 90f),
                new Vector2(texture.Width / 2, texture.Height / 2),
                SpriteEffects.None,
                0f);
        }

        public virtual void Collision(GraphicsDeviceManager graphics)
        {

        }
    }
}
