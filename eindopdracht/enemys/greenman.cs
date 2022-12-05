using eindopdracht.animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindopdracht.enemys
{
     class greenman : IGameObject
    {

        Animatie walking;
        Texture2D greenmanTexture;
        public Rectangle body = new Rectangle(0,0,0,0);

        private Vector2 positie = new Vector2(50, 50);


        private Vector2 snelheid = new Vector2(1.2f, 1.2f);
        public Vector2 velocity = new Vector2();
        public greenman(Texture2D greenmanTexture)
        {
            walking = new Animatie();

            walking.AddFrame(new AnimationFrame(new Rectangle(0, 0, 44, 35)));
            walking.AddFrame(new AnimationFrame(new Rectangle(46, 0, 44, 35)));
            walking.AddFrame(new AnimationFrame(new Rectangle(94, 0, 44, 35)));
            walking.AddFrame(new AnimationFrame(new Rectangle(142, 0, 44, 35)));
            walking.AddFrame(new AnimationFrame(new Rectangle(190, 0, 44, 35)));
            
            this.greenmanTexture = greenmanTexture;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            body = walking.CurrentFrame.SourceRectangle;
            spriteBatch.Draw(greenmanTexture, positie , body, Color.White, 0, new Vector2(0, 0), 1.2f, SpriteEffects.None, 1);
        }

        public void Update(GameTime gameTime)
        {
           walking.Update(gameTime);
        }
    }
}
