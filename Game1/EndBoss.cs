using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class EndBoss
    {
        private Texture2D _texture;
        public Rectangle rectangle;
        private Vector2 positie;
        private bool border= true;
        private int MinBorder = 400, MaxBorder = 590;
        private int speed = 4;
        public bool IsAlive = true;
        Animation animation;

        public EndBoss(Texture2D texture)
        {
            _texture = texture;

            positie = new Vector2(6700, 570);
            animation = new Animation();
            animation.AddFrame(new Rectangle(0, 0, 89, 132));
            animation.AddFrame(new Rectangle(96, 0, 87, 128));
            animation.AddFrame(new Rectangle(193, 0, 86, 128));
            animation.AddFrame(new Rectangle(288,0,87,126));
            animation.AddFrame(new Rectangle(384, 0, 86, 126));
            animation.AddFrame(new Rectangle(480, 0, 86, 126));
            animation.AddFrame(new Rectangle(576, 0, 86, 126));
            animation.AantalBewegingenPerSeconde = 10; 
            
        }

        public void Update(GameTime gameTime)
        {
            if (IsAlive)
            {
                if (border)
                    positie.Y += speed;
                else
                    positie.Y -= speed;
                if ((positie.Y > MaxBorder) || (positie.Y < MinBorder))
                    border = !border;

                rectangle = new Rectangle((int)positie.X, (int)positie.Y, 86, 126);
                animation.Update(gameTime);
            }
            else
            {
                rectangle = new Rectangle(0,0,0,0);
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, positie, animation.CurrentFrame.SourceRectangle, Color.White);
        }


    }
}
