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
    public class Ship : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        public Texture2D tex;
        
        public Vector2 position;

        private float rotation = 0f;
        private Rectangle srcRect;
        private Vector2 origin;
        private Vector2 stage;
        public float scale = 0.1f;
        

        public Ship(Game game, SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            Vector2 stage) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            
            this.position = position;

            this.stage = stage;
            srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = new Vector2(tex.Width / 2, 0);
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
            MouseState mouse = Mouse.GetState();
            position.X = mouse.X;

            if (position.X <= 0 + (tex.Width/2)*scale)
            {
                position.X = 0 + (tex.Width / 2) * scale;
            }
            if (position.X >= stage.X - (tex.Width / 2)*scale)
            {
                position.X = stage.X - (tex.Width/2) * scale;
            }
        }
    }
}
