using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        #endregion

        #region Wereld Variabele
        Hero _hero;
        Enemy _Enemy;
        MovingTiles MovingTile;
        Map map;
        //Blok _blok;
        Camera2D camera;
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
            _Enemy = new Enemy(Enemy_afbeeldingR, Enemy_afbeeldingL);

            MovingTile_afbeelding = Content.Load<Texture2D>("TileMove");
            MovingTile = new MovingTiles(MovingTile_afbeelding,1051,200,1050,1500);

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
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _hero.Update(gameTime);
            MovingTile.Update(gameTime);
            // TODO: Add your update logic here

            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                _hero.Collision(tile.Rectangle, map.Width, map.Height);
            }

            _hero.CollisionEnemy(_Enemy.rectangle);
            _hero.CollisionMovingTiles(MovingTile.rectangle);
           
            if (_hero.rectangle.TouchTopOf(_Enemy.rectangle))
            {
                _Enemy.isAlive = false;
            }
            else
            {
                _Enemy.Update(gameTime);
                _hero.CollisionEnemy(_Enemy.rectangle);
            }



            if (_hero.IsMoving)
                camPos.X += _hero.VelocityX.X;
            if (_hero.IsDead)
            {
                camPos.X = 0;
            } 
            


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

            if(_Enemy.isAlive)
            _Enemy.Draw(spriteBatch);

            map.Draw(spriteBatch);
            MovingTile.Draw(spriteBatch);
            spriteBatch.End();

            

            base.Draw(gameTime);
        }
    }
}
