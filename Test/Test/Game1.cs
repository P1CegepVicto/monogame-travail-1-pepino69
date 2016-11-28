using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Test
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameObject Trump;
        Rectangle fenetre;
        Texture2D Background;
        GameObject mexicain;


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

            this.graphics.PreferredBackBufferWidth = 1500;
            this.graphics.PreferredBackBufferHeight = 1000;
            fenetre = new Rectangle(0, 0, 1500, 1000);
            this.graphics.ApplyChanges();
            
            //fenetre = new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height);
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

            Background = Content.Load<Texture2D>("background.jpg");

            mexicain = new GameObject();
            mexicain.estvivant = true;
            mexicain.position.X = 1000;
            mexicain.position.Y = 400;
            mexicain.sprite = Content.Load<Texture2D>("mexicain.png");




            Trump = new GameObject();
            Trump.estvivant = true;
            Trump.position.X = 0;
            Trump.position.Y = 400;
            Trump.sprite = Content.Load<Texture2D>("trump.png");

            // TODO: use this.Content to load your game content here
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
            if (Trump.position.X <= fenetre.Left)
            {
                Trump.vitesse.X = 0;
                Trump.position.X = fenetre.Left;
            }

            if (Trump.position.X + 174 >= fenetre.Right)
            {
                Trump.vitesse.X = 0;
                Trump.position.X = fenetre.Right -175;
            }

            if (Trump.position.Y + 999 <= fenetre.Bottom)
            {
                Trump.vitesse.Y = 0;
                Trump.position.Y = fenetre.Bottom - 1000;
            }

            if (Trump.position.Y + 1 <= fenetre.Top)
            {
                Trump.vitesse.Y = 0;
                Trump.position.Y = fenetre.Top - 1;
            }




            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Trump.position.X += -4;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Trump.position.X += 4;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Trump.position.Y += 4;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Trump.position.Y += -4;
            }


            UpdateTrump();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public void UpdateTrump()
        {
            Trump.position += Trump.vitesse;
     
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();


            spriteBatch.Draw(Background, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.White);


            spriteBatch.Draw(Trump.sprite, Trump.position, Color.White);

            spriteBatch.Draw(mexicain.sprite, mexicain.position, Color.White);

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
