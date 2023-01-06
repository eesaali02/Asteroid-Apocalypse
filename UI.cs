using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace FinalProject
{
    public class UI : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private List<Shot> shots;
        public int score = 0;
        public UI(Game game, SpriteBatch spriteBatch, SpriteFont font, List<Shot> shots) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.shots = shots;
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font,"SCORE: " +score.ToString(),new Vector2(20,20), Color.Red);
            spriteBatch.End();
        }
        public override void Update(GameTime gameTime)
        {
            countScore();
        }
        protected void countScore()
        {
            score = 0;
            foreach (Shot shot in shots)
            {
                if (shot.isHit == true)
                {
                    score += 10;
                }
            }
        }
    }
}
