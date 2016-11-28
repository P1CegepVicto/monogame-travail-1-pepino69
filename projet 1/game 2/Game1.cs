using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace game_2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameObject Hero;
        GameObject[] Enemy = new GameObject[4];
        GameObject ProjectileH;
        GameObject ProjectileE;
        Texture2D Victoire;
        Texture2D Defaite;
        Rectangle fenetre;
        Texture2D Background;
        SpriteFont text;
        Random rng = new Random();
        int rotate = 0;
        int kill = 0;
        int nbenemy = 0;
        SoundEffect sonvictory;
        SoundEffect sonfail;
        SoundEffectInstance victory;
        SoundEffectInstance fail;



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

            //Écran
            this.graphics.PreferredBackBufferHeight = 1000;
            this.graphics.PreferredBackBufferWidth = 1500;
            
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

            //déclaration stats variables
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Background = Content.Load<Texture2D>("halo background.jpg");



            Victoire = Content.Load<Texture2D>("victoire.png");


            Defaite = Content.Load<Texture2D>("defaite.png");



            //hero
            Hero = new GameObject();
            Hero.estvivant = true;
            Hero.position.X = 10;
            Hero.position.Y = 850;
            Hero.sprite = Content.Load<Texture2D>("mchief.png");

            //enemis
            for (int i = 0; i < Enemy.Length;i++)
            {
                Enemy[i] = new GameObject();
                Enemy[i].estvivant = true;
                Enemy[i].position.X = fenetre.Center.X - 100;
                Enemy[i].position.Y = fenetre.Top - 10;         
                Enemy[i].vitesse.X = rng.Next(1, 7);
                if (i == 0)
                {
                    Enemy[i].sprite = Content.Load<Texture2D>("elite.png");
                }

                if (i == 1)
                {
                    Enemy[i].sprite = Content.Load<Texture2D>("hunter.png");
                }

                if (i == 2)
                {
                    Enemy[i].sprite = Content.Load<Texture2D>("brute.png");
                }

                if (i == 3)
                {
                    Enemy[i].sprite = Content.Load<Texture2D>("grunt.png");
                }
            }

            //balle hero
            ProjectileH = new GameObject();
            ProjectileH.estvivant = false;
            ProjectileH.sprite = Content.Load<Texture2D>("grenade.png");
            ProjectileH.vitesse.X = 15;

            //balle ennemis
            ProjectileE = new GameObject();
            ProjectileE.estvivant = false;
            ProjectileE.sprite = Content.Load<Texture2D>("bullet 2.png");
            ProjectileE.vitesse.X = -15;

            //text
            text = Content.Load<SpriteFont>("Font");

            //Sons
            sonvictory = Content.Load<SoundEffect>("Sound\\fail");
            victory = sonvictory.CreateInstance();
            sonfail = Content.Load<SoundEffect>("Sound\\victory");
            fail = sonfail.CreateInstance();
            Song song = Content.Load<Song>("Song\\halo theme");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.05F;
            MediaPlayer.Play(song);

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
        /// 

        //gametime
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //End Game defeat
            if (Hero.estvivant == false)
            {
                MediaPlayer.Stop();
                fail.Play();
                Thread.Sleep(2000);
                this.Exit();
            }
            //End Game victoire
            if (!Enemy[0].estvivant && !Enemy[1].estvivant && !Enemy[2].estvivant && !Enemy[3].estvivant && Hero.estvivant && gameTime.TotalGameTime.Seconds >= 3)
            {
                MediaPlayer.Stop();
                victory.Play();
                Thread.Sleep(2000);
                this.Exit();
            }
                for (int i = Enemy.Length - 1; i >= 0; i--)
                {
                    if (nbenemy * 1 < gameTime.TotalGameTime.Seconds && nbenemy < Enemy.Length)
                    {
                        Enemy[nbenemy].estvivant = true;
                        nbenemy++;
                    }
                }

                //touches héro
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


            //fenetre héro
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


            //fenetre ennemis
            for (int i = 0; i < Enemy.Length;i++) 
            {
                if (Enemy[i].position.X + 150 >= fenetre.Right)
                {
                    Enemy[i].vitesse.X -= 7;
                }
                if (Enemy[i].position.X - 5 <= fenetre.Left)
                {
                    Enemy[i].vitesse.X += 7;
                }

                if (Enemy[i].position.Y + 150 >= fenetre.Bottom)
                {
                    Enemy[i].vitesse.Y -= 7;
                }
                if (Enemy[i].position.Y - 5 <= fenetre.Top)
                {
                    Enemy[i].vitesse.Y += 7;
                }
            }
           
              //fenetre balle ennemis
                for (int i = 0; i < Enemy.Length; i++)
                {
                    if (Enemy[i].estvivant == true && ProjectileE.estvivant == false)
                    {
                        ProjectileE.estvivant = true;
                        ProjectileE.position.X = Enemy[i].position.X;
                        ProjectileE.position.Y = Enemy[i].position.Y;

                    }
                    if (ProjectileE.position.X < fenetre.Left || ProjectileE.position.Y < 0)
                    {
                        ProjectileE.estvivant = false;
                    }
                    if (Hero.GetRect().Intersects(ProjectileE.GetRect()))
                    {
                        Hero.estvivant = false;
                    }
                }
            
            //fenetre balle héro
            if (Hero.estvivant == true && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                ProjectileH.estvivant = true;
                ProjectileH.position.X = Hero.position.X + 100;
                ProjectileH.position.Y = Hero.position.Y + 50;
            }
            if (ProjectileH.position.X >= fenetre.Right)
            {
                ProjectileH.estvivant = false;
            }
            for (int i = 0; i < Enemy.Length; i++)
            {
                if (Enemy[i].GetRect().Intersects(ProjectileH.GetRect()))
                {
                    Enemy[i].estvivant = false;
                }
            }

            //Colision et mort des enemis
            for (int i = Enemy.Length - 1; i >= 0; i--)
            {
                if (this.ProjectileH.GetRect().Intersects(this.Enemy[i].GetRect()) && Enemy[i].estvivant == true)
                {
                    Enemy[i].estvivant = false;
                    ProjectileH.estvivant = false;
                    victory.Play();
                }
            }
            //Colision hero Enemy
            for (int i = Enemy.Length - 1; i >= 0; i--)
            {
                if (Hero.estvivant == true && Enemy[i].estvivant == true)
                {
                    if (this.Hero.GetRect().Intersects(this.Enemy[i].GetRect()))
                    {
                        Hero.estvivant = false;
                        fail.Play();
                    }
                }
            }

         

            rotate += 2;
            // TODO: Add your update logic here

            //update
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
            for (int i = 0; i < Enemy.Length; i++)
            {
                Enemy[i].position += Enemy[i].vitesse;
            }
        }

        public void UpdateProjectileH()
        {
            ProjectileH.position += ProjectileH.vitesse;
        }

        public void UpdateProjectileE()
        {
            if(ProjectileE.estvivant)
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

            //draw background
            spriteBatch.Draw(Background, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.White);

            //draw héro
            spriteBatch.Draw(Hero.sprite, Hero.position, Color.White);
            if (Hero.estvivant == false)
            {
                spriteBatch.Draw(Hero.sprite, Hero.position, Color.Red);
            }

            //draw ennemis
            for (int i = 0; i < Enemy.Length; i++)
            {
                if (Enemy[i].estvivant == true)
                {
                    spriteBatch.Draw(Enemy[i].sprite, Enemy[i].position, Color.White);
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        spriteBatch.Draw(Enemy[i].sprite, Enemy[i].position);
                    }
                }

                if (Enemy[i].estvivant == false && Hero.estvivant == true)
                {
                    kill++;
                }
            }

            for (int i = 0; i < Enemy.Length; i++)
            {
                if (Enemy[i].estvivant == false)
                {
                    spriteBatch.Draw(Enemy[i].sprite, Enemy[i].position, Color.Red);
                }
            }

            //draw balle ennemis
            for (int i = 0; i < Enemy.Length; i++)
            {
                if (ProjectileE.estvivant == true && Enemy[i].estvivant == true)
                {
                    spriteBatch.Draw(ProjectileE.sprite, ProjectileE.position);
                }
            }

            //draw balle héro
            if (ProjectileH.estvivant == true && Hero.estvivant == true)
            {
                spriteBatch.Draw(ProjectileH.sprite, ProjectileH.position, rotation: rotate / 7, origin:new Vector2(ProjectileH.sprite.Width/2,ProjectileH.sprite.Height/2));
            }

            //draw victoire
            for (int i = 0; i < Enemy.Length; i++)
            {
                if (Hero.estvivant == true && kill == 4)
                {
                    spriteBatch.Draw(Victoire, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.White);
                }
            }

            //draw défaite
            for (int i = 0; i < Enemy.Length; i++)
            {
                if (Hero.estvivant == false && Enemy[i].estvivant == true)
                {
                    spriteBatch.Draw(Defaite, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.White);
                }
            }

            //draw temps
            spriteBatch.DrawString(text, gameTime.TotalGameTime.TotalSeconds.ToString(), new Vector2(50, 50), Color.Black);
            if (Hero.estvivant == false)
            {
                spriteBatch.DrawString(text, gameTime.TotalGameTime.TotalSeconds.ToString(), new Vector2(825, 370), Color.Black);
            }
            if (!Enemy[0].estvivant && !Enemy[1].estvivant && !Enemy[2].estvivant && Hero.estvivant && gameTime.TotalGameTime.Seconds >= 3)
            {
                spriteBatch.DrawString(text, gameTime.TotalGameTime.TotalSeconds.ToString(), new Vector2(825, 370), Color.Black);
            }

            spriteBatch.End();


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
