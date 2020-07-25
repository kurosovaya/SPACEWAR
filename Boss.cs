using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Spacewar.Game1;

namespace Spacewar
{
    public class Boss : Entity2D
    {
        public bool Exist = true;
        float Speed;


        public Boss(int startX, int startY, Texture2D texture, float scale, float speed, float angle = 0f) : base(startX, startY, texture, scale, angle)
        {
            Speed = speed;
        }               

        public void MoveToTarget(SpaceShip target)
        {
            float angle = (float)Math.Atan2(target.Y - Y, target.X - X);
            X += (float)(Speed * Math.Cos(angle));
            Y += (float)(Speed * Math.Sin(angle));
        }

        public void Hit()
        {
            foreach (Shell shell in arrayOfShells) {
                if (shell.X - shell.Width / 2 < X && shell.X + shell.Width / 2 > X) {
                    if (shell.Y - shell.Height / 2 < Y && shell.Y + shell.Height / 2 > Y) {
                        this.Dispose();
                    }
                }
            }
        }

        private void Dispose()
        {
            Exist = false;
            //SoundEffect deadSound = MyContent.Load<SoundEffect>("62ae0708f0");
            //deadSound.Play(0.1f, 0.3f, 0f);
        }
    }
}
