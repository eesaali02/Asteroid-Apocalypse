using Microsoft.VisualBasic.FileIO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceShipGame;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FinalProject
{
    public class Shot : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Ship ship;
        private Vector2 speed;
        public Vector2 position;
        private float rotation = 0f;
        private Rectangle srcRect;
        private Vector2 origin;
        private Vector2 stage;
        private float scale = 0.05f;
        private List<Asteroid> asteroids;
        private float distShotAst;
        private Explosion explosion;
        private SoundEffect explosionSound;
        private SoundEffect shotSound;
        public bool isHit = false;

        public Shot(Game game, SpriteBatch spriteBatch,
           Texture2D tex,
           Vector2 position,
           Vector2 stage, Ship ship, List<Asteroid> asteroids,
           Explosion explosion,
           SoundEffect soundEffect, SoundEffect soundEffect1) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.stage = stage;
            srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = new Vector2(tex.Width / 2, 0);
            speed = new Vector2(0, 15);
            this.ship = ship;
            this.asteroids = asteroids;
            this.explosion = explosion;
            this.explosionSound= soundEffect;
            this.shotSound = soundEffect1;
            shotSound.Play();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //v 6
                spriteBatch.Draw(tex, position, srcRect, Color.White, rotation, origin, scale,
                    SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {

            if (position.X <= 0 + (ship.tex.Width / 2) * ship.scale)
            {
                position.X = 0 + (ship.tex.Width / 2) * ship.scale;
            }
            if (position.X >= stage.X - (ship.tex.Width / 2) * ship.scale)
            {
                position.X = stage.X - (ship.tex.Width / 2) * ship.scale;
            }

            position.Y -= speed.Y;
            if(asteroids!= null)
            {
                foreach (Asteroid asteroid in asteroids)
                {
                    distShotAst = Vector2.Distance(position, asteroid.position);
                    if (distShotAst < ((asteroid.tex1.Width / 2) * asteroid.scale) || (distShotAst < ((asteroid.tex1.Height / 2) * asteroid.scale)))
                    {

                        explosion.Position = new Vector2(asteroid.position.X - (asteroid.tex1.Width / 2) * asteroid.scale, asteroid.position.Y - (asteroid.tex1.Width / 2) * asteroid.scale);
                        asteroid.Visible = false;
                        asteroid.Enabled= false;
                        asteroid.position.X = 1000;
                        asteroid.position.Y = 1000;
                        this.position.X = 2000;
                        this.position.Y = -2000;
                        this.Visible = false;
                        this.Enabled= false;
                        this.isHit = true;
                        explosionSound.Play();
                        explosion.restart();
                    }
                }
            }
        }
    }
}
