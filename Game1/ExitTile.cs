using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class ExitTile
    {
        private Texture2D _texture;
        public Rectangle rectangle;
        private Vector2 Positie;


        public ExitTile(Texture2D texture, Vector2 positie)
        {
            Positie = positie;
            _texture = texture;
            rectangle = new Rectangle((int)Positie.X, (int)positie.Y, 71, 71);
        }
        public void Update(GameTime gametime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Positie, null, Color.White);
        }
    }
}
