using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Kanon
    {
        private Texture2D _texture;
        public Rectangle rectangle;
        public Vector2 Positie;

        public Kanon(Texture2D texture,Vector2 positie)
        {
            _texture = texture;
            Positie = positie;
            rectangle = new Rectangle((int)Positie.X, (int)Positie.Y, _texture.Width, _texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Positie, null, Color.White);
        }

    }
}
