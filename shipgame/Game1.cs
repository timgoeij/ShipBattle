using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace shipgame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D background;
        Vector2 backPosition;

        Player player;
        Camera camera;

        private List<Ship> ships = new List<Ship>();

        Random rand = new Random(DateTime.Now.Millisecond);

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
            Ship.setTexture(Content.Load<Texture2D>("airship"), Content.Load<Texture2D>("bigboat"));
            Bullet.setTexture(Content.Load<Texture2D>("bullet"), Content.Load<Texture2D>("bigbullet"));

            player = new Player(Vector2.Zero, 5, 500);

            camera = new Camera(GraphicsDevice.Viewport);

            for (int i = 0; i < 10; i++ )
            {
                if (i <= 4)
                {
                    ships.Add(new smallShip(rand, player, 0, 200));
                }
                else
                {
                    ships.Add(new BigShip(rand, player, 3, 300));
                }
            }

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
            player.PlayerTex = Content.Load<Texture2D>("boat");
            player.load();

            foreach(Ship ship in ships)
            {
                ship.load();
            }

            background = Content.Load<Texture2D>("water");
            backPosition = new Vector2(-700, -700);
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
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
              //  Exit();

            // TODO: Add your update logic here
            player.Update(gameTime);
            player.Ships = ships;
            camera.update(gameTime, player);

            for (int i = 0; i < ships.Count; i++ )
            {
                ships[i].Update(gameTime);

                if(ships[i].MaxHealth <= 0)
                {
                    ships.RemoveAt(i);
                }
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

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

            spriteBatch.Draw(background, backPosition, null, Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
            
            player.Draw(spriteBatch);

            foreach (Ship ship in ships)
            {
                ship.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
