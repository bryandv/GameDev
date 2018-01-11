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
        private Rectangle rectangle;
        public Vector2 Positie;

        public Kanon(Texture2D texture,Vector2 positie)
        {
            _texture = texture;
            Positie = positie;
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
