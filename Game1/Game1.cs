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
        Texture2D bullet_afbeelding;
        #endregion

        #region Wereld Variabele
        Hero _hero;
     
        MovingTiles MovingTile;
        Map map;
        //Blok _blok;
        Camera2D camera;
        List<coin> coins = new List<coin>();
        List<Enemy> _Enemys = new List<Enemy>();
        List<bullet> bullets = new List<bullet>();
        private SpriteFont font;
        
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
            _Enemys.Add(new Enemy(Enemy_afbeeldingR, Enemy_afbeeldingL, 1621, 365, 1620, 2000));

            MovingTile_afbeelding = Content.Load<Texture2D>("TileMove");
            MovingTile = new MovingTiles(MovingTile_afbeelding,1051,200,1050,1500);

            coin_afbeelding = Content.Load<Texture2D>("coin2");
            
            coins.Add(new coin(coin_afbeelding, 295, 210));
            coins.Add(new coin(coin_afbeelding, 690, 300));
            coins.Add(new coin(coin_afbeelding, 1500, 150));
            coins.Add(new coin(coin_afbeelding, 1900, 220));
            font = Content.Load<SpriteFont>("Score");

            bullet_afbeelding = Content.Load<Texture2D>("bulletSprite2");
            

            Tiles.Content = Content;

            map.Generate(new int[,]{
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,3,2,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {5,3,3,2,2,2,3,3,4,0,0,5,3,3,3,3,3,3,4,0,0,0,0,5,3,3,3,3,3,3,4,0,0,0,0},
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
                for (int i = 0; i < bullets.Count; i++)
                {
                    if (bullets[i].rectangle.TouchLeftOf(tile.Rectangle))
                    {
                        bullets.Remove(bullets[i]);
                        
                    }
                }
            }
                

            _hero.CollisionMovingTiles(MovingTile.rectangle);
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
            /*
            foreach (Enemy enemy in _Enemys)   
            {
                foreach (bullet Bullet in bullets)
                {
                    if(Bullet.rectangle.TouchLeftOf(enemy.rectangle))
                    {
                        bullets.Remove(Bullet);
                    }
                }
            }*/
            foreach (Enemy enemy in _Enemys)      
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                        if (bullets[i].rectangle.TouchLeftOf(enemy.rectangle))
                        {
                            bullets.Remove(bullets[i]);
                            enemy.isAlive = false;
                           _hero.Score += 10;
                    }
                }

            }
            for (int i = 0; i < bullets.Count; i++)    
            {
                if (bullets[i].Positie.X > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                {
                    bullets.RemoveAt(i);
                }
                    
            }




        }

        public void shootbullet(int x, int y)
        {
            bullet b = new bullet(bullet_afbeelding, x+50, y+35);
            bullets.Add(b);
            
        }
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _hero.Update(gameTime);
            MovingTile.Update(gameTime);

            foreach(Enemy enemy in _Enemys)
              enemy.Update(gameTime);

            foreach (coin coin in coins)
                coin.Update(gameTime);
           //_bullet.Update(gameTime);

            if(_hero._bediening.shoot)
            {
                 shootbullet((int)_hero.Positie.X, (int)_hero.Positie.Y); 
            }

            foreach(bullet Bullet in bullets)
            {
                Bullet.Update(gameTime);
            }
            Collisions();
           


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

            foreach (bullet Bullet in bullets)
                Bullet.Draw(spriteBatch);
            map.Draw(spriteBatch);
            MovingTile.Draw(spriteBatch);
            
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Score: " + _hero.Score, new Vector2(0, 0), Color.Black);
            spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
