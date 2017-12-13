using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Blok
    {
        public Texture2D _texture { get; set; }
        public Vector2 Positie { get; set; }

        public Blok(Texture2D texture,Vector2 pos)
        {
            _texture = texture;
            Positie = pos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Positie,new Rectangle(648,0,71,71), Color.White);
        }

    }
}
//XNA tutorial 40 - Creating a Tile Map(1/3)