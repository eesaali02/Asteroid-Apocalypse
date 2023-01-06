 using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpaceShipGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace FinalProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Ship ship;
        private Shot shot;
        private Vector2 shipPos;
        private Vector2 shotPos;
        private Vector2 stage;
        private Texture2D shipTex;
        private Texture2D laserTex;
        private Texture2D asteroidTex;
        private Texture2D asteroidTex2;
        private double astDelay;
        public double astTime = 1.0;
        private double shotDelay;
        private double shotTime = 0.30;
        private Background bg1, bg2;
        private Asteroid asteroid;
        private UI ui;
        private List<Asteroid> asteroids = new List<Asteroid>();
        private List<Shot> shots = new List<Shot>();
        private Explosion explosion;
        private Random rand = new Random();
        private SoundEffect explosionSound;
        private SpriteFont font;
        private Timer timer;
        private bool ChangeStageScreen = false;
        private ChangeStage changeStage;
        private SoundEffect shotSound;
        private Song backgroundMusic;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Load
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            stage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            shipTex = Content.Load<Texture2D>("ship");
            laserTex = Content.Load<Texture2D>("laser");
            asteroidTex = Content.Load<Texture2D>("asteroid");
            asteroidTex2 = Content.Load<Texture2D>("Asteroid2");
            shipPos = new Vector2(stage.X / 2, stage.Y - 60);
            shotPos = new Vector2(stage.X / 2, stage.Y - 75);

            Texture2D bgTex1 = Content.Load<Texture2D>("nebula");
            Texture2D bgTex2 = Content.Load<Texture2D>("stars");


            Vector2 bg1Speed = new Vector2(0, 0.1f);

            Rectangle srcRect1 = new Rectangle(0, 0, bgTex1.Width, bgTex1.Height);
            Vector2 pos1 = new Vector2(stage.X - srcRect1.Width, 0);

            bg1 = new Background(this, _spriteBatch, bgTex1, pos1, srcRect1, bg1Speed);

            Vector2 bg2Speed = new Vector2(0, 0.2f);

            Rectangle srcRect2 = new Rectangle(0, 0, bgTex2.Width, bgTex2.Height);
            Vector2 pos2 = new Vector2(stage.X - srcRect2.Width, 0);

            bg2 = new Background(this, _spriteBatch, bgTex2, pos2, srcRect1, bg2Speed);

            Texture2D explosionTex = this.Content.Load<Texture2D>("explosion");
            explosion = new Explosion(this, _spriteBatch, explosionTex, Vector2.Zero, 3);

            explosionSound = this.Content.Load<SoundEffect>("explosionSound");
            shotSound = this.Content.Load<SoundEffect>("laserSound");
            backgroundMusic = this.Content.Load<Song>("GameMusic");
            MediaPlayer.Play(backgroundMusic);
            font = this.Content.Load<SpriteFont>("font");

            timer = new Timer(this, _spriteBatch, 60, font);
            //Create
            ship = new Ship(this, _spriteBatch, shipTex, shipPos, stage);
            //shot = new Shot(this, _spriteBatch, laserTex, shotPos, stage, ship);
            ui = new UI(this, _spriteBatch, font, shots);
            changeStage = new ChangeStage(this, _spriteBatch, font, stage);

            this.Components.Add(bg1);
            this.Components.Add(bg2);
            this.Components.Add(ship);
            this.Components.Add(explosion);
            this.Components.Add(ui);
            this.Components.Add(timer);
            this.Components.Add(changeStage);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            MouseState mouse = Mouse.GetState();
            Vector2 shotposition = new Vector2(mouse.X, shotPos.Y);
            // TODO: Add your update logic here
            shotDelay += gameTime.ElapsedGameTime.TotalSeconds;
            astDelay += gameTime.ElapsedGameTime.TotalSeconds;
            if(astDelay >= astTime)
            {
                int whichTex = rand.Next(0, 2);
                if (whichTex == 0)
                {
                    asteroid = new Asteroid(this, _spriteBatch, asteroidTex, stage, ship, explosion, explosionSound);
                    if (changeStage.level == 1)
                    {
                        asteroid.speed.Y = 8;
                    }
                    if (changeStage.level == 2)
                    {
                        asteroid.speed.Y = 12;
                    }
                    this.Components.Add(asteroid);
                    asteroids.Add(asteroid);
                }
                if (whichTex == 1)
                {
                    asteroid = new Asteroid(this, _spriteBatch, asteroidTex2, stage, ship, explosion, explosionSound);
                    if (changeStage.level == 1)
                    {
                        asteroid.speed.Y = 8;
                    }
                    if (changeStage.level == 2)
                    {
                        asteroid.speed.Y = 12;
                    }
                    this.Components.Add(asteroid);
                    asteroids.Add(asteroid);
                }
                astDelay = 0;
            }

            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (shotDelay >= shotTime)
                {
                    shot = new Shot(this, _spriteBatch, laserTex, shotposition, stage, ship, asteroids, explosion, explosionSound, shotSound); //added the shot sound for the laser
                    shotDelay = 0;
                    this.Components.Add(shot);
                    shots.Add(shot);
                }
            }
            foreach(Asteroid asteroid in asteroids)
            {
                if (asteroid.collide == true) //if one of the asteroids collided with the ship
                {
                    changeStage.level = 3;
                    timer.timerComplete = true;
                }
            }
            if (timer.timerComplete==true) //bool when timer hits 0;
            {
                timer.countdown = 60; //timer resets
                if (changeStage.level != 3) //level increments if not equal to 3
                {
                    changeStage.level++;
                }
                if(changeStage.level == 1)
                {
                    timer.timerComplete = false;
                    changeStage.displayTimer = 5;
                    astDelay = 0.0001; //delay between asteroids
                    shotTime = 0.25; //fire rate
                }
                if (changeStage.level == 2)
                {
                    timer.timerComplete = false;
                    changeStage.displayTimer = 5;
                    astDelay = 0.0001;
                    shotTime = 0.20;
                }
                if (changeStage.level == 3) //game over screen everything stops
                {
                    timer.countdown = 0;
                    ship.Visible = false;
                    ship.Enabled= false;
                    if (shot != null)
                    {
                        shot.Visible = false;
                        shot.Enabled = false;
                    }
                    asteroid.Visible = false;
                    asteroid.Enabled=false;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


    }
}