using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spacewar
{
    public class Shell : Entity2D
    {
        float DirectionAngle;
        float Speed;
        public bool Exist = true;
        private Shell instance;

        public Shell(int startX, int startY, Texture2D texture, float speed, float angle, float directonAngle, float shipSpeed) : base(startX, startY, texture, angle)
        {
            SumVectors(angle, speed, directonAngle, shipSpeed);
            //this.instance = instance;
        }

        public Shell(int startX, int startY, Texture2D texture, float scale, float speed, float angle, float directonAngle, float shipSpeed) : base(startX, startY, texture, scale, angle)
        {
            SumVectors(angle, speed, directonAngle, shipSpeed);
            //this.instance = instance;
        }

        public void Move()
        {
            X -= (float)Math.Cos(MathHelper.ToRadians(DirectionAngle)) * Speed;
            Y -= (float)Math.Sin(MathHelper.ToRadians(DirectionAngle)) * Speed;
        }

        public void Delete()
        {
            this.instance = null;
        }

        public override void Collision(GraphicsDeviceManager graphics)
        {

        }

        public void SumVectors(float angleA, float forceA, float angleB, float forceB)
        {
            float x = (float)Math.Cos(MathHelper.ToRadians(angleA)) * forceA;
            float y = (float)Math.Sin(MathHelper.ToRadians(angleA)) * forceA;

            x += (float)Math.Cos(MathHelper.ToRadians(angleB)) * forceB;
            y += (float)Math.Sin(MathHelper.ToRadians(angleB)) * forceB;

            float angle = MathHelper.ToDegrees((float)Math.Atan2(y, x));
            DirectionAngle = angle;
            Speed = (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }
    }
}
