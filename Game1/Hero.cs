using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    
   public class Hero
    {
        Matrix m;

        Matrix rotationYMatrix;

        private Texture2D _textureR;

        private Texture2D _textureL;
        private Texture2D _texture;
        private Rectangle _viewRectangle;
        public Vector2 Positie;
        public Bediening _bediening { get; set; }

        private Animation _animation;
        public Vector2 VelocityX = new Vector2(2, 0);
        public float startY,jumpspeed = 0;
        bool hasJumped;
        public bool IsMoving = false;

    
        public Hero(Texture2D textureR,Texture2D textureL)
        {
            _textureR = textureR;
            _textureL = textureL;
            _texture = _textureR;

            m = new Matrix();
            rotationYMatrix = Matrix.CreateRotationX((float)Math.PI / 2);

            Positie = new Vector2(100, 275);
            _animation = new Animation();
            _animation.AddFrame(new Rectangle(160, 0, 80, 80));
            _animation.AddFrame(new Rectangle(80, 0, 80, 80));
            _animation.AddFrame(new Rectangle(0, 0, 80, 80));
            _animation.AantalBewegingenPerSeconde = 10;
            _bediening = new BedieningPijltjes();
            hasJumped = false;
            // _viewRectangle = new Rectangle(0, 160, 80, 80);
            //  _bediening = new BedieningPijltjes();
        }



        public void Update(GameTime gameTime)
        {
            KeyboardState stateKey = Keyboard.GetState();

            _bediening.Update();


            if (_bediening.left)
            {
                _animation.Update(gameTime);
                _texture = _textureL;
                IsMoving = true;
            }
            else if (_bediening.right)
            {
                _animation.Update(gameTime);
                _texture = _textureR;
                IsMoving = true;
            }
            else
                IsMoving = false;


            if (_bediening.left)
                VelocityX.X = -2;
            //Positie -= VelocityX;
            else if (_bediening.right)
                VelocityX.X = 2;
            //Positie += VelocityX;
            else
                VelocityX.X = 0;
            Positie += VelocityX;
            

            if (hasJumped)
            {
                Positie.Y += jumpspeed;
                jumpspeed += 1;
                if(Positie.Y >= startY)
                {
                    Positie.Y = startY;
                    hasJumped = false;
                }
            }
            else
            {
                if(stateKey.IsKeyDown(Keys.Up))
                {
                    hasJumped = true;
                    jumpspeed = -20;
                    startY = Positie.Y;
                }
            }




            /*if ((stateKey.IsKeyDown(Keys.Space)) && (hasJumped == false))
            {
                Positie.Y -= 10f;
                VelocityX.Y = -5f;
                hasJumped = true;
            }

            if(hasJumped == true)
            {
                float i = 1;
                VelocityX.Y += 0.15f * 1;
            }
            
            if(Positie.Y + _texture.Height >= 450)
            {
                hasJumped = false;
            }

            if (hasJumped == false)
            {
                VelocityX.Y = 0f;
            }*/

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Positie, _animation.CurrentFrame.SourceRectangle, Color.White);
        }
     
    }
}
