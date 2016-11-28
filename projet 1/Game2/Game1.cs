using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using System;


namespace Game2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameObject Hero;
        GameObject Enemy;
        GameObject ProjectileH;
        GameObject ProjectileE;
        Texture2D Victoire;
        Texture2D Defaite;
        Rectangle fenetre;
        Texture2D Background;
        Random rng = new Random();
        int rotate = 0;

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
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.ToggleFullScreen();
            fenetre = new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height);
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


            
            Victoire = Content.Load<Texture2D>("victoire.jpg");

            
            Defaite = Content.Load<Texture2D>("defaite.jpg");



            
            Hero = new GameObject();
            Hero.estvivant = true;
            Hero.position.X = 830;
            Hero.position.Y = 850;
            Hero.sprite = Content.Load<Texture2D>("trump.png");


            
            Enemy = new GameObject();
            Enemy.estvivant = true;
            Enemy.position.X = fenetre.Center.X - 100;
            Enemy.position.Y = fenetre.Top - 10;
            Enemy.sprite = Content.Load<Texture2D>("mexicain.png");
            Enemy.vitesse.X = 25;


            
            ProjectileH = new GameObject();
            ProjectileH.estvivant = false;
            ProjectileH.sprite = Content.Load<Texture2D>("brick.png");
            ProjectileH.vitesse.Y = -20;


            ProjectileE = new GameObject();
            ProjectileE.estvivant = false;
            ProjectileE.sprite = Content.Load<Texture2D>("visa.png");
            ProjectileE.vitesse.Y = 15;


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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            if (Hero.estvivant == false || Enemy.estvivant == false)
            {
                Thread.Sleep(1500);
                this.Exit();
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Hero.vitesse.X -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Hero.vitesse.X += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Hero.vitesse.Y -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Hero.vitesse.Y += 1;
            }
            


            if (Hero.position.X + 225 >= fenetre.Right)
            {
                Hero.vitesse.X = 0;
                Hero.position.X = fenetre.Right - 250;
            }
            if (Hero.position.X + 80 <= fenetre.Left)
            {
                Hero.vitesse.X = 0;
                Hero.position.X = fenetre.Left - 30;
            }
            if (Hero.position.Y + 200 >= fenetre.Bottom)
            {
                Hero.vitesse.Y = 0;
                Hero.position.Y = fenetre.Bottom - 201;
            }
            if (Hero.position.Y + 80 <= fenetre.Top)
            {
                Hero.vitesse.Y = 0;
                Hero.position.Y = fenetre.Top - 65;
            }
            


            if (Enemy.position.X + 150 >= fenetre.Right)
            {
                Enemy.vitesse.X -= 7;
            }
            if (Enemy.position.X - 5 <= fenetre.Left)
            {
                Enemy.vitesse.X += 7;
            }
           
            if(Enemy.estvivant == true && ProjectileE.estvivant == false)
            {
                ProjectileE.estvivant = true;
                ProjectileE.position.X = Enemy.position.X;
                ProjectileE.position.Y = Enemy.position.Y;
                
            }
            if (ProjectileE.position.Y > fenetre.Bottom || ProjectileE.position.Y < -100)
            {
                ProjectileE.estvivant = false;
            }
            if (Hero.GetRect().Intersects(ProjectileE.GetRect()))
            {
                Hero.estvivant = false;
            }
        

            if (Hero.estvivant == true && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                ProjectileH.estvivant = true;
                ProjectileH.position.X = Hero.position.X + 100;
                ProjectileH.position.Y = Hero.position.Y + 50;
            }
            if (ProjectileH.position.Y <= fenetre.Top)
            {
                ProjectileH.estvivant = false;
            }
            if (Enemy.GetRect().Intersects(ProjectileH.GetRect()))
            {
                Enemy.estvivant = false;
            }


            rotate += 1;

            // TODO: Add your update logic here
            UpdateHero();
            UpdateEnemy();
            UpdateProjectileH();
            UpdateProjectileE();

            base.Update(gameTime);
        }
        public void UpdateHero()
        {
            Hero.position += Hero.vitesse;
        }
        public void UpdateEnemy()
        {
            Enemy.position += Enemy.vitesse;
        }
       
        public void UpdateProjectileH()
        {
            ProjectileH.position += ProjectileH.vitesse;
        }

        public void UpdateProjectileE()
        {
            ProjectileE.position += ProjectileE.vitesse;
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

            spriteBatch.Draw(Hero.sprite, Hero.position, Color.White);
            if (Hero.estvivant == false)
            {
                spriteBatch.Draw(Hero.sprite, Hero.position, Color.Red);
            }
            spriteBatch.Draw(Enemy.sprite, Enemy.position, Color.White);
            if (Enemy.estvivant == false)
            {
                spriteBatch.Draw(Enemy.sprite, Enemy.position, Color.Red);
            }

            if (ProjectileE.estvivant == true && Enemy.estvivant == true)
            {
                spriteBatch.Draw(ProjectileE.sprite, ProjectileE.position, rotation: rotate / 7);
            }

            if (ProjectileH.estvivant == true && Hero.estvivant == true)
            {
                spriteBatch.Draw(ProjectileH.sprite, ProjectileH.position, rotation: rotate / 7);
            }

            if (Hero.estvivant == true && Enemy.estvivant == false)
            {
                spriteBatch.Draw(Victoire, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.White);
            }
            if (Hero.estvivant == false && Enemy.estvivant == true)
            {
                spriteBatch.Draw(Defaite, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.White);
            }


            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
