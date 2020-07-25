using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spacewar
{
    class Ship : Entity2D
    {
        float Speed { get; set; }
        float RotateSpeed { get; set; }        

        public Ship(int startX, int startY, Texture2D texture, float speed, float rotateSpeed, float angle = 0f) : base(startX, startY, texture, angle)
        {
            Speed = speed;
            RotateSpeed = rotateSpeed;
        }

        public Ship(int startX, int startY, Texture2D texture, float scale, float speed, float rotateSpeed, float angle = 0f) : base(startX, startY, texture, scale, angle)
        {
            Speed = speed;
            RotateSpeed = rotateSpeed;
        }

        public Ship(int startX, int startY, Texture2D texture, int width, int height, float speed, float rotateSpeed, float angle = 0f) : base(startX, startY, texture, height, width, angle)
        {
            Speed = speed;
            RotateSpeed = rotateSpeed;
        }

        public void Move(KeyboardState kstate, GameTime gameTime)
        {
            if (kstate.IsKeyDown(Keys.Up)) {
                Y -= Speed * (float)Math.Sin(MathHelper.ToRadians(Angle + 90f)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                X -= Speed * (float)Math.Cos(MathHelper.ToRadians(Angle + 90f)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Down)) {
                Y += Speed * (float)Math.Sin(MathHelper.ToRadians(Angle + 90f)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                X += Speed * (float)Math.Cos(MathHelper.ToRadians(Angle + 90f)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Left)) {
                Angle -= RotateSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Angle = Angle < 0 ? Angle + 360f : Angle;
            }

            if (kstate.IsKeyDown(Keys.Right)) {
                Angle += RotateSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Angle = Angle > 360f ? Angle - 360f : Angle;
            }
        }

        public override void Collision(GraphicsDeviceManager graphics)
        {
            if (Y < Height / 2)
                Y = Height / 2;
            if (X < Width / 2)
                X = Width / 2;
            if (Y > graphics.PreferredBackBufferHeight - Height / 2)
                Y = graphics.PreferredBackBufferHeight - Height / 2;
            if (X > graphics.PreferredBackBufferWidth - Width / 2)
                X = graphics.PreferredBackBufferWidth - Width / 2;
        }
    }
}
