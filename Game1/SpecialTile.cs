using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class SpecialTile
    {
        private Texture2D texture;
        public Rectangle rectangle;
        private Vector2 Positie;
        private Animation animatie;


        public SpecialTile(Texture2D texture,Vector2 positie)
        {
            Positie = positie;
            this.texture = texture;
            animatie = new Animation();
            animatie.AddFrame(new Rectangle(0, 0, 28, 67));
            animatie.AddFrame(new Rectangle(0, 72, 28, 67));
            animatie.AantalBewegingenPerSeconde = 3;
            rectangle = new Rectangle((int)Positie.X, (int)positie.Y, 28, 67);
            
        }
        public void Update(GameTime gametime) { animatie.Update(gametime); }
           

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,Positie, animatie.CurrentFrame.SourceRectangle, Color.White);
        }
    }
}
