using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class bullet
    {
        protected Texture2D _texture;
        public Vector2 Positie;
        public Rectangle rectangle;
        protected int speed = 10;
        private bool Direction;
        

       /* public bullet(Texture2D texture,int posX,int posY)
        {
           _texture = texture;
           Positie = new Vector2(posX,posY);
            
        }*/
 
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Positie, null, Color.White);
        }

    }


    class BulletRight : bullet
    {
        public BulletRight(Texture2D texture,int PosX,int PosY)
        {
            _texture = texture;
            Positie = new Vector2(PosX, PosY);
        }

        public void Update(GameTime gametime)
        {
            Positie.X += speed;
            rectangle = new Rectangle((int)Positie.X, (int)Positie.Y, 19, 12);
        }
    }

    class BulletLeft : bullet
    {
        public BulletLeft(Texture2D texture, int PosX, int PosY)
        {
            _texture = texture;
            Positie = new Vector2(PosX, PosY);
        }

        public void Update(GameTime gametime)
        {
            Positie.X -= speed;
            rectangle = new Rectangle((int)Positie.X, (int)Positie.Y, 19, 12);
        }
    }


}
