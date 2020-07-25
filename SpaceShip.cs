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
    public class SpaceShip : Entity2D
    {
        float Speed { get; set; }
        private float CurrentSpeed = 0f;
        private float MaxSpeed = 120f;
        private float acceleration = 5f;
        private float DirectionAngle = 90f;
        Texture2D Shell;
        List<Shell> arrayOfShells;
        float RotateSpeed { get; set; }

        public SpaceShip(int startX, int startY, Texture2D texture, Texture2D shell, List<Shell> arrayOfShells, float speed, float rotateSpeed, float angle = 0f) : base(startX, startY, texture, angle)
        {
            Speed = speed;
            RotateSpeed = rotateSpeed;
            Shell = shell;
            this.arrayOfShells = arrayOfShells;            
        }

        public SpaceShip(int startX, int startY, Texture2D texture, Texture2D shell, List<Shell> arrayOfShells, float scale, float speed, float rotateSpeed, float angle = 0f) : base(startX, startY, texture, scale, angle)
        {
            Speed = speed;
            RotateSpeed = rotateSpeed;
            Shell = shell;
            this.arrayOfShells = arrayOfShells;
        }

        public SpaceShip(int startX, int startY, Texture2D texture, Texture2D shell, List<Shell> arrayOfShells, int width, int height, float speed, float rotateSpeed, float angle = 0f) : base(startX, startY, texture, height, width, angle)
        {
            Speed = speed;
            RotateSpeed = rotateSpeed;
            Shell = shell;
            this.arrayOfShells = arrayOfShells;
        }

        public void Move(KeyboardState kstate, GameTime gameTime)
        {
            if (kstate.IsKeyDown(Keys.Up)) {
                float tempAcceleration = acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
                //на время
                SumVectors(Angle, tempAcceleration, DirectionAngle, CurrentSpeed);
            }

            if (kstate.IsKeyDown(Keys.Down)) {
                float tempAcceleration = acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
                //на время
                SumVectors(Angle - 180f, tempAcceleration, DirectionAngle, CurrentSpeed);
            }

            if (kstate.IsKeyDown(Keys.Left)) {
                Angle -= RotateSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Angle = Angle < 0 ? Angle + 360f : Angle;
            }

            if (kstate.IsKeyDown(Keys.Right)) {
                Angle += RotateSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Angle = Angle > 360f ? Angle - 360f : Angle;
            }

            if (kstate.IsKeyDown(Keys.Space)) {
                this.ShootMePls();
            }

            X -= (float)Math.Cos(MathHelper.ToRadians(DirectionAngle)) * CurrentSpeed;
            Y -= (float)Math.Sin(MathHelper.ToRadians(DirectionAngle)) * CurrentSpeed;

            float angle = MathHelper.ToDegrees((float)Math.Atan2(Y - 1080 / 2, X - 1920 / 2));
            SumVectors(angle, 0.01f, DirectionAngle, CurrentSpeed);
        }

        public override void Collision(GraphicsDeviceManager graphics)
        {
            if (Y < -(Height / 2))
                Y = graphics.PreferredBackBufferHeight + Height / 2;
            if (X < -(Height / 2))
                X = graphics.PreferredBackBufferWidth + Height / 2;
            if (Y > graphics.PreferredBackBufferHeight + Height / 2)
                Y = -(Height / 2);
            if (X > graphics.PreferredBackBufferWidth + Height / 2)
                X = -(Height / 2);
        }

        public void ShootMePls()
        {
            //временное решение
            Shell shell = new Shell((int)X, (int)Y, Shell, 0.05f, CurrentSpeed + 1f, Angle, DirectionAngle, CurrentSpeed);
            arrayOfShells.Add(shell);
        }

        public void SumVectors(float angleA, float forceA, float angleB, float forceB)
        {
            float x = (float)Math.Cos(MathHelper.ToRadians(angleA)) * forceA;
            float y = (float)Math.Sin(MathHelper.ToRadians(angleA)) * forceA;

            x += (float)Math.Cos(MathHelper.ToRadians(angleB)) * forceB;
            y += (float)Math.Sin(MathHelper.ToRadians(angleB)) * forceB;

            float angle = MathHelper.ToDegrees((float)Math.Atan2(y, x));
            DirectionAngle = angle;
            CurrentSpeed = (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }
    }
}
