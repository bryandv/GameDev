using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Level
    {
        public Texture2D texture;
        public byte[,] tileArray = new Byte[,]
        {
            {0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,1,1,0,0 },
            {1,1,1,1,0,1,1,0,1 },
        };

        private Blok[,] blokArray = new Blok[6, 9];

        public void CreateWorld()
        {
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (tileArray[x, y] == 1)
                    {
                        blokArray[x, y] = new Blok(texture, new Vector2(y * 71, x * 71));
                    }
                }
            }
        }


        public void DrawWorld(SpriteBatch spritebatch)
        {
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (blokArray[x, y] != null)
                    {
                        blokArray[x, y].Draw(spritebatch);
                    }
                }
            }

        }
    }
}
