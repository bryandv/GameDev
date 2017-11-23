using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    
   public class Hero
    {
        private Texture2D _textureR;
        private Texture2D _textureL;
        private Texture2D _texture;
        private Rectangle _viewRectangle;
        public Vector2 Positie;
        public Bediening _bediening { get; set; }

        private Animation _animation;
        public Vector2 VelocityX = new Vector2(2, 0);


        public Hero(Texture2D textureR,Texture2D textureL)
        {
            _textureR = textureR;
            _textureL = textureL;
            _texture = _textureR;
            Positie = new Vector2(100, 100);
            _animation = new Animation();
            _animation.AddFrame(new Rectangle(0, 0, 80, 80));
            _animation.AddFrame(new Rectangle(80, 0, 80, 80));
            _animation.AddFrame(new Rectangle(160, 0, 80, 80));
            _animation.AantalBewegingenPerSeconde = 8;


            // _viewRectangle = new Rectangle(0, 160, 80, 80);
            //  _bediening = new BedieningPijltjes();
        }



        public void Update(GameTime gameTime)
        {
            _bediening.Update();

            
            if (_bediening.left)
            {
                _animation.Update(gameTime);
                _texture = _textureL;
            }

            if (_bediening.right)
            {
                _animation.Update(gameTime);
                _texture = _textureR;
            }


            if (_bediening.left)
                Positie -= VelocityX;
            if (_bediening.right)
                Positie += VelocityX;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Positie, _animation.CurrentFrame.SourceRectangle, Color.White);
        }
     
    }
}
