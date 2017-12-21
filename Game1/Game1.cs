using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        #region Texture Variabele
        Texture2D Hero_afbeeldingR;
        Texture2D Hero_afbeeldingL;
        Texture2D Enemy_afbeeldingR;
        Texture2D Enemy_afbeeldingL;
        Texture2D MovingTile_afbeelding;
        Texture2D coin_afbeelding;
        Texture2D bullet_afbeeldingRechts;
        Texture2D bullet_afbeeldingLinks;
        Texture2D background;
        #endregion

        #region Wereld Variabele
        Hero _hero;
     
        MovingTilesLeftRight MovingTile;
        MovingTilesUpDown moving;
        Map map;
        //Blok _blok;
        Camera2D camera;
        List<coin> coins = new List<coin>();
        List<Enemy> _Enemys = new List<Enemy>();
        List<BulletRight> bulletsright = new List<BulletRight>();
        List<BulletLeft> bulletsleft = new List<BulletLeft>();
        List<MovingTiles> movingTiles = new List<MovingTiles>();
        KeyboardState pastkey;
        private SpriteFont font;
        public Rectangle mainframe;
        
        #endregion

        //List<ICollide> collideObjecten;
        Level level;

        int afstand = 55;
        Vector2 camPos = new Vector2();
        float rotation = 0;
        float zoom = 1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1500;
            graphics.PreferredBackBufferHeight = 750;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            camera = new Camera2D(GraphicsDevice.Viewport);
            map = new Map();
         
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Hero_afbeeldingR = Content.Load<Texture2D>("charx25R");
            Hero_afbeeldingL = Content.Load<Texture2D>("charx25L");
            _hero = new Hero(Hero_afbeeldingR, Hero_afbeeldingL);

            Enemy_afbeeldingL = Content.Load<Texture2D>("Enemyx2L");
            Enemy_afbeeldingR = Content.Load<Texture2D>("Enemyx2R");
            
            _Enemys.Add(new Enemy(Enemy_afbeeldingR, Enemy_afbeeldingL, 801, 365, 800, 1050));
            _Enemys.Add(new Enemy(Enemy_afbeeldingR, Enemy_afbeeldingL, 1721, 365, 1720, 2150));

            MovingTile_afbeelding = Content.Load<Texture2D>("TileMove");
            MovingTile = new MovingTilesLeftRight(MovingTile_afbeelding,1201,200,1200,1500);
            moving = new MovingTilesUpDown(MovingTile_afbeelding, 675, 496, 497, 600);
            movingTiles.Add(MovingTile);

            coin_afbeelding = Content.Load<Texture2D>("coin2");
            
            coins.Add(new coin(coin_afbeelding, 295, 210));
            coins.Add(new coin(coin_afbeelding, 690, 300));
            coins.Add(new coin(coin_afbeelding, 1500, 150));
            coins.Add(new coin(coin_afbeelding, 1900, 220));
            coins.Add(new coin(coin_afbeelding, 50, 550));
            font = Content.Load<SpriteFont>("Score");

            bullet_afbeeldingRechts = Content.Load<Texture2D>("bulletSprite2");
            bullet_afbeeldingLinks = Content.Load<Texture2D>("bulletSprite2L");

            background = Content.Load<Texture2D>("hillsbackground");
            mainframe = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            Tiles.Content = Content;

            map.Generate(new int[,]{
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,3,2,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {5,3,3,2,2,2,3,3,4,0,0,5,3,3,3,3,3,3,4,0,0,0,0,0,5,3,3,3,3,3,4,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3},
            }, 71);


             //Texture2D blokText = Content.Load<Texture2D>("tilesSpritesheet");
             //_blok = new Blok(blokText, new Vector2(0, 0+afstand));

            /* level = new Level();
             level.texture = blokText;
             level.CreateWorld();

            collideObjecten = new List<ICollide>();
            collideObjecten.Add(_hero);*/
             
            
            


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        public void Collisions()
        {
            // Collisions voor tiles, enemys en coins in de wereld
            #region Tiles
            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                _hero.Collision(tile.Rectangle, map.Width, map.Height);
                for (int i = 0; i < bulletsright.Count; i++)
                {
                    if (bulletsright[i].rectangle.TouchLeftOf(tile.Rectangle))
                    {
                        bulletsright.Remove(bulletsright[i]);
                        
                    }

                }
                for (int i = 0; i < bulletsleft.Count; i++)
                {
 
                        if (bulletsleft[i].rectangle.TouchLeftOf(tile.Rectangle))
                        {
                            bulletsleft.Remove(bulletsleft[i]);

                        }

               

                }
            }
                

            _hero.CollisionMovingTiles(MovingTile.rectangle);
            _hero.CollisionMovingTiles(moving.rectangle);
            #endregion
            #region enemys
            foreach (Enemy enemy in _Enemys)
                _hero.CollisionEnemy(enemy.rectangle);

            foreach (Enemy enemy in _Enemys)
            {
                

                if (_hero.rectangle.TouchTopOf(enemy.rectangle))
                {
                    enemy.isAlive = false;
                    _hero.Score += 10;
                    _hero.Positie.Y -= 5f;
                    _hero.VelocityX.Y = -12f;
                }

                for (int i = 0; i < bulletsright.Count; i++)
                {
                    if (bulletsright[i].rectangle.TouchLeftOf(enemy.rectangle))
                    {
                        bulletsright.Remove(bulletsright[i]);
                        enemy.isAlive = false;
                        _hero.Score += 10;
                    }
                }

                for (int i = 0; i < bulletsleft.Count; i++)
                {
                    if (bulletsleft[i].rectangle.TouchLeftOf(enemy.rectangle))
                    {
                        bulletsleft.Remove(bulletsleft[i]);
                        enemy.isAlive = false;
                        _hero.Score += 10;
                    }
                }

            }
            #endregion 
            #region coins
            foreach (coin coin in coins)
            {
                if (_hero.rectangle.TouchLeftOf(coin.rectangle))
                {
                    coin.OnScreen = false;
                    _hero.Score++;
                }
                if (_hero.rectangle.TouchBottomOf(coin.rectangle))
                {
                    coin.OnScreen = false;
                    _hero.Score++;
                }
                if (_hero.rectangle.TouchRightOf(coin.rectangle))
                {
                    coin.OnScreen = false;
                    _hero.Score++;
                }
                if (_hero.rectangle.TouchTopOf(coin.rectangle))
                {
                    coin.OnScreen = false;
                    _hero.Score++;
                }
            }
            #endregion

        }

        public void shootbulletRight(int x, int y)
        {
            BulletRight b = new BulletRight(bullet_afbeeldingRechts, x+50, y+35);
            bulletsright.Add(b);
            
        }
        public void shootbulletLeft(int x, int y)
        {
            BulletLeft b = new BulletLeft(bullet_afbeeldingLinks, x - 50, y+35);
            bulletsleft.Add(b);
        }
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _hero.Update(gameTime);
            MovingTile.Update(gameTime);
            moving.Update(gameTime);

            foreach(Enemy enemy in _Enemys)
              enemy.Update(gameTime);

            foreach (coin coin in coins)
                coin.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && pastkey.IsKeyUp(Keys.Space))
            {
                if (_hero.direction)
                    shootbulletRight((int)_hero.Positie.X, (int)_hero.Positie.Y);
                else
                    shootbulletLeft((int)_hero.Positie.X, (int)_hero.Positie.Y);
            }
            pastkey = Keyboard.GetState();


            foreach (BulletRight Bullet in bulletsright)
                Bullet.Update(gameTime);
          
            foreach (BulletLeft Bullet in bulletsleft)
                Bullet.Update(gameTime);
           
            Collisions();

            for (int i = 0; i < bulletsright.Count; i++)
            {
                //float oldstate = bulletsright[i].Positie.X;
                if (bulletsright[i].Positie.X > (_hero.Positie.X + 400))
                {
                    bulletsright.Remove(bulletsright[i]);
                }

            }
            for (int i = 0; i < bulletsleft.Count; i++)
            {
                //float oldstate = bulletsright[i].Positie.X;
                if (bulletsleft[i].Positie.X < (_hero.Positie.X - 400))
                {
                    bulletsleft.Remove(bulletsleft[i]);
                }

            }

            if (_hero.IsMoving)
                camPos.X += _hero.VelocityX.X;
            if (_hero.IsDead)
                camPos.X = 0;
             
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

             var viewMatrix = camera.GetViewMatrix();
              camera.Position = camPos;
             camera.Rotation = rotation;
              camera.Zoom = zoom;

            spriteBatch.Begin();
            spriteBatch.Draw(background, mainframe, Color.White);
            spriteBatch.DrawString(font, "Score: " + _hero.Score, new Vector2((GraphicsDevice.Viewport.Width / 2) - 150, 0), Color.Black);

            spriteBatch.End();




            // TODO: Add your drawing code here
            spriteBatch.Begin(transformMatrix:viewMatrix);
           // spriteBatch.Begin();
            _hero.Draw(spriteBatch);

            foreach(coin coin in coins)
            coin.Draw(spriteBatch);

            foreach(Enemy enemy in _Enemys)
            {
                if (enemy.isAlive)
                   enemy.Draw(spriteBatch);
            }

            foreach (BulletRight Bullet in bulletsright)
                Bullet.Draw(spriteBatch);
            foreach (BulletLeft Bullet in bulletsleft)
                Bullet.Draw(spriteBatch);
            map.Draw(spriteBatch);
            MovingTile.Draw(spriteBatch);
            moving.Draw(spriteBatch);
            
            spriteBatch.End();



           


            base.Draw(gameTime);
        }
    }
}
