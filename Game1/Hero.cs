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
    
   public class Hero : ICollide
    {
        Matrix m;

        Matrix rotationYMatrix;

        private Texture2D _textureR;
        private Texture2D _textureL;
        private Texture2D _texture;
        private Rectangle _viewRectangle;
        public Rectangle rectangle;
        public Vector2 Positie;
        public Bediening _bediening { get; set; }
        public Rectangle CollisionRectangle;

        private Animation _animation;
        public Vector2 VelocityX = new Vector2(2, 0);
       // public float startY,jumpspeed = 0;
        public bool hasJumped;
        public bool IsMoving = false;
        public bool IsDead = false;
        public int Score = 0;

    
        public Hero(Texture2D textureR,Texture2D textureL)
        {
            _textureR = textureR;
            _textureL = textureL;
            _texture = _textureR;

            m = new Matrix();
            rotationYMatrix = Matrix.CreateRotationX((float)Math.PI / 2);

            Positie = new Vector2(100, 0);
            _animation = new Animation();
            _animation.AddFrame(new Rectangle(160, 0, 80, 80));
            _animation.AddFrame(new Rectangle(80, 0, 80, 80));
            _animation.AddFrame(new Rectangle(0, 0, 80, 80));
            _animation.AantalBewegingenPerSeconde = 10;
            _bediening = new BedieningPijltjes();
            hasJumped = true;
            CollisionRectangle = new Rectangle((int)Positie.X, (int)Positie.Y, 64, 205);
  
        }



        public void Update(GameTime gameTime)
        {
            Positie += VelocityX;
            KeyboardState stateKey = Keyboard.GetState();
            rectangle = new Rectangle((int)Positie.X, (int)Positie.Y, 75,80);
           
            Input(gameTime);
            /*if (VelocityX.Y < 10)
            {
                VelocityX.Y += 0.4f;
            }*/

            if (IsDead == false)
                VelocityX.Y += 0.4f;
            if(IsDead == true)
            {
                Positie = new Vector2(100, 100);
                IsDead = false;
            }



            #region test
            /*

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
                VelocityX.X = -2f;
            //Positie -= VelocityX;
            else if (_bediening.right)
                VelocityX.X = 2f;
            //Positie += VelocityX;
            else
                VelocityX.X = 0f;
           
            

            /*if (hasJumped)
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
                if(stateKey.IsKeyDown(Keys.Up) && hasJumped == false)
                {
                    hasJumped = true;
                    jumpspeed = -20;
                    startY = Positie.Y;
                }
            }

            if (Positie.Y + _texture.Height >= 450)
                hasJumped = false;




            if ((stateKey.IsKeyDown(Keys.Up)) && (hasJumped == false))
            {
                Positie.Y -= 10f;
                VelocityX.Y = -5f;
                hasJumped = true;
            }

            if(hasJumped == true)
            {
                float i = 1;
                VelocityX.Y += 0.15f *i;
            }
            
            if(Positie.Y + _texture.Height >= 400)
            {
                hasJumped = false;
            }

            if (hasJumped == false)
            {
                VelocityX.Y = 0f;
            }
            */

            #endregion
        }

        private void Input(GameTime gameTime)
        {
            _bediening.Update();
            if (_bediening.right)
            {
                _animation.Update(gameTime);
                VelocityX.X = 4f;
                IsMoving = true;
                _texture = _textureR;
            }
            else if (_bediening.left)
            {
                _animation.Update(gameTime);
                VelocityX.X = -4f;
                IsMoving = true;
                _texture = _textureL;
            }
            else
                VelocityX.X = 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && hasJumped == false)
            {
                Positie.Y -= 5f;
                VelocityX.Y = -12f;
                hasJumped = true;
            }
        }

        public void Collision(Rectangle newRectangle, int xoffset, int yoffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                VelocityX.Y = 0f;
                hasJumped = false;
            }

            if (rectangle.TouchLeftOf(newRectangle))
            {
                Positie.X = newRectangle.X - rectangle.Width - 2;
                IsMoving = false;
            }

            if (rectangle.TouchRightOf(newRectangle))
            {
                Positie.X = newRectangle.X + rectangle.Width + 2;
                IsMoving = false;
            }

            if (rectangle.TouchBottomOf(newRectangle))
            {
                VelocityX.Y = 1f;
            }


            if (Positie.X < 0)
            { Positie.X = 0;
                IsMoving = false;
            } 
            if (Positie.X > xoffset - rectangle.Width)
            { Positie.X = xoffset - rectangle.Width;
                IsMoving = false;
            }
            if (Positie.Y < 0)
            {
                VelocityX.Y = 1f;
                
            } 
            if (Positie.Y > yoffset - rectangle.Height)
            {
                //Positie.Y = yoffset - rectangle.Height;
                // IsMoving = false;
                IsDead = true;
                //Positie = new Vector2(100, 100);
                
            } 


        }

        public void CollisionEnemy(Rectangle newRectangle)
        {
           
            if(rectangle.TouchLeftOf(newRectangle))
            {
                IsDead = true;
            }
            if (rectangle.TouchRightOf(newRectangle))
            {
                IsDead = true;
            }
        }

        public void CollisionMovingTiles(Rectangle newRectangle)
        {
            if(rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                VelocityX.Y = 0f;
                hasJumped = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Positie, _animation.CurrentFrame.SourceRectangle, Color.White);
        }


        public Rectangle GetCollisionRectangle()
        {
            return CollisionRectangle;
        }


    }
}
