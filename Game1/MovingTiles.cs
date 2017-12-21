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
        protected Texture2D _texture;
        protected bool border = true;
        protected int min_border_pos, max_border_pos;
        public Rectangle rectangle;
        public Vector2 Positie;
        protected int speed = 2;
        public MovingTiles()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Positie, null, Color.White);
        }

    }


    class MovingTilesLeftRight : MovingTiles
    {

        public MovingTilesLeftRight(Texture2D texture, int PositieX, int PositieY, int MinBorderPos, int MaxBorderPos)
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
    }

    class MovingTilesUpDown : MovingTiles
    {
        public MovingTilesUpDown(Texture2D texture, int PositieX, int PositieY, int MinBorderPos, int MaxBorderPos)
        {
            _texture = texture;
            min_border_pos = MinBorderPos;
            max_border_pos = MaxBorderPos;
            Positie = new Vector2(PositieX, PositieY);
        }

        public void Update(GameTime gametime)
        {
            if (border)
                Positie.Y += speed;
            else
                Positie.Y -= speed;

            if (Positie.Y > max_border_pos || Positie.Y < min_border_pos)
                border = !border;

            rectangle = new Rectangle((int)Positie.X, (int)Positie.Y, 70, 40);

        }
    }
}
