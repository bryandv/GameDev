using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Enemy
    {
        private Texture2D _textureR;
        private Texture2D _textureL;
        private Texture2D _texture;
        public bool isAlive;
        public Rectangle rectangle;
        public Vector2 Positie;
        private Animation _animation;
        private int speed = 1;
        private bool border = true;
        public Vector2 VelocityX = new Vector2(0, 0);
        int _MinBorderPos, _MaxBorderPos;

        public Enemy(Texture2D textureR, Texture2D textureL,int PositieX, int PositieY,int MinBorderPos,int MaxBorderPos)
        {
            _textureR = textureR;
            _textureL = textureL;
            _texture = _textureR;

            _MinBorderPos = MinBorderPos;
            _MaxBorderPos = MaxBorderPos;

            _animation = new Animation();
            _animation.AddFrame(new Rectangle(120, 0, 60, 60));
            _animation.AddFrame(new Rectangle(60, 0, 60, 60));
            _animation.AddFrame(new Rectangle(0, 0, 60, 60));
            
            _animation.AantalBewegingenPerSeconde = 8;
            Positie = new Vector2(PositieX, PositieY);
            isAlive = true;
           // CollisionRectangle = new Rectangle((int)Positie.X, (int)Positie.Y, 64, 205);

        }
        public void Update(GameTime gametime)
        {
            if (isAlive)
            {
                if (border)
                {
                    Positie.X += speed;
                    _animation.Update(gametime);
                    _texture = _textureR;
                }

                else
                {
                    Positie.X -= speed;
                    _animation.Update(gametime);
                    _texture = _textureL;
                }


                if ((Positie.X > _MaxBorderPos) || (Positie.X < _MinBorderPos))
                    border = !border;

                rectangle = new Rectangle((int)Positie.X, (int)Positie.Y, 60, 60);
            }
            else
            {
               // Positie = new Vector2(0, 0);
                rectangle = new Rectangle(0, 0, 0, 0);
            }
                


        }
        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(_texture, Positie, _animation.CurrentFrame.SourceRectangle, Color.White);
        }


    }
}
