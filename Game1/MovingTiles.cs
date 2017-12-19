using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class MovingTiles
    {
        private Texture2D _texture;
        private bool border = true;
        private int min_border_pos, max_border_pos;
        public Rectangle rectangle;
        public Vector2 Positie;
        private int speed = 2;
        public MovingTiles(Texture2D texture, int PositieX, int PositieY,int  MinBorderPos, int MaxBorderPos)
        {
            _texture = texture;
            min_border_pos = MinBorderPos;
            max_border_pos = MaxBorderPos;
            Positie = new Vector2(PositieX, PositieY);
        }

        public void Update(GameTime gameTime)
        {
            if (border)
                Positie.X += speed;

            else
                Positie.X -= speed;

            if ((Positie.X > max_border_pos) || (Positie.X < min_border_pos))
                border = !border;
                
            rectangle = new Rectangle((int)Positie.X, (int)Positie.Y, 70, 40);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Positie, null, Color.White);
        }

    }
}
