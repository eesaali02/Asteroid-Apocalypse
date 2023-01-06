using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SpaceShipGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FinalProject
{
    public class Asteroid : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        public Texture2D tex1;
        private Ship ship;
        private Shot shot;
        private double spawnTimer;
        private int astStartX;
        private int speedX;
        public Vector2 speed;
        public Vector2 position;
        private float rotation = 0f;
        private Rectangle srcRect;
        private Vector2 origin;
        private Vector2 stage;
        public float scale = 0.4f;
        private int rotateDir;
        private float distShipAst;
        public bool collide; //Asteroid collides with ship
        private Explosion explosion;
        private SoundEffect explosionSound;

        Random random= new Random();

        public Asteroid(Game game,
            SpriteBatch spritebatch,
            Texture2D tex,
            Vector2 stage, Ship ship, Explosion explosion, SoundEffect soundEffect) : base(game)
        {
            this.spriteBatch = spritebatch;
            this.tex1 = tex;
            this.stage = stage;
            this.ship = ship;
            this.explosion = explosion;
            this.explosionSound = soundEffect;
            astStartX = random.Next(0, Convert.ToInt32(stage.X));
            speedX = random.Next(-2, 2);
            position = new Vector2(astStartX, -10);
            srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            speed = new Vector2(speedX, 3);
            rotateDir = random.Next(0, 2);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //v 6
            spriteBatch.Draw(tex1, position, srcRect, Color.White, rotation, origin, scale,
                SpriteEffects.None, 0);

            spriteBatch.End();

            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            position.Y += speed.Y;
            position.X += speedX;
            if (rotateDir == 0)
            {
                rotation += 0.02f;
            }
            if (rotateDir == 1)
            {
                rotation -= 0.02f;
            }
            distShipAst = Vector2.Distance(position, ship.position);
            if (distShipAst < (tex1.Width/2)*scale) 
            {
                collide = true;
                this.Visible = false;
                this.Enabled= false;
                explosion.Position = new Vector2(position.X - (tex1.Width / 2) * scale, position.Y - (tex1.Width / 2) * scale);
                explosionSound.Play();
                explosion.restart();
            }
        }
    }
}
