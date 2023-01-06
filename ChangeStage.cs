using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class ChangeStage : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        public string message = "";
        public int level = 0;//What level the game is on
        public double displayTimer = 5; //How long the level text stays on the screen
        public ChangeStage(Game game, SpriteBatch spriteBatch, SpriteFont font, Vector2 stage) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            message = "";
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, message, new Vector2(350, 250), Color.Red);
            spriteBatch.End();
        }
        public override void Update(GameTime gameTime)
        {
            displayTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            if (level == 0)
            {
                message = "Level 1";
                if(displayTimer<=0)
                {
                    message = "";
                }
            }
            if (level == 1)
            {
                message = "Level 2";
                if (displayTimer <= 0)
                {
                    message = "";
                }
            }
            if (level == 2)
            {
                message = "Level 3";
                if (displayTimer <= 0)
                {
                    message = "";
                }
            }
            if (level == 3)
            {
                message = "GAME OVER";
            }
        }
    }
}
