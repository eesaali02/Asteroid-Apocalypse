using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Timer : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        public double countdown;
        public bool timerComplete;
        private double elapsedTime;
        private SpriteFont font;
        public Timer(Game game, SpriteBatch _spriteBatch, double countdown, SpriteFont font) : base(game)
        {
            this._spriteBatch= _spriteBatch;
            this.countdown = countdown; //initial countdown
            this.timerComplete = false;
            this.font= font;
        }
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(font ,"TIMER: " + Math.Round(countdown, 1).ToString(),new Vector2(20, 40),Color.Red);
            _spriteBatch.End();
        }
        public override void Update(GameTime gameTime)
        {
            elapsedTime = gameTime.ElapsedGameTime.TotalSeconds; //Time between frames
            countdown -= elapsedTime;
            if (countdown <= 0)
            {
                countdown = 0;
                timerComplete = true;
            }
        }
    }
}
