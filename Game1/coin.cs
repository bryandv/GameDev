using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class coin
    {
        private Texture2D _texture;
        public Rectangle rectangle;
        private Animation _animation;
        private Vector2 Positie;
        public bool OnScreen = true;

        public coin(Texture2D texture,int PositieX,int PositieY)
        {
            _texture = texture;

            _animation = new Animation();
            _animation.AddFrame(new Rectangle(0, 0, 51, 46));
            _animation.AddFrame(new Rectangle(51, 0, 51, 46));
            _animation.AddFrame(new Rectangle(102, 0, 51, 46));
            _animation.AddFrame(new Rectangle(153, 0, 51, 46));
            _animation.AddFrame(new Rectangle(204, 0, 51, 46));
            _animation.AddFrame(new Rectangle(255,0,51, 46));
            _animation.AantalBewegingenPerSeconde = 12;
            
            Positie = new Vector2(PositieX, PositieY);
            
        }

        public void Update(GameTime gametime)
        {
            if (OnScreen)
            {
                _animation.Update(gametime);
                rectangle = new Rectangle((int)Positie.X, (int)Positie.Y, 51, 46);
            }
            else
                rectangle = new Rectangle(0, 0, 0, 0);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if(OnScreen)
            spriteBatch.Draw(_texture, Positie, _animation.CurrentFrame.SourceRectangle, Color.White);
        }


    }
}
