using eindopdracht.animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindopdracht
{
    internal class Hero : IGameObject
    {
        Texture2D heroTexture;
        Animatie animatie;
        private Vector2 positie;
        private Vector2 snelheid;
        private SpriteEffects SpriteDirection;

        public Hero(Texture2D texture)
        {
            heroTexture = texture;
            animatie = new Animatie();
            positie = new Vector2(0, 0);

            animatie.AddFrame(new AnimationFrame(new Rectangle(0, 150, 48, 50)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(46, 150, 48, 50)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(92, 150, 48, 50)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(138, 150, 48, 50)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(184, 150, 48, 50)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(230, 150, 48, 50)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(276, 150, 48, 50)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(322, 150, 48, 50)));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        
        snelheid = new Vector2(2, 2);

        spriteBatch.Draw(heroTexture, positie, animatie.CurrentFrame.SourceRectangle, Color.White,0,new Vector2(0,0),1,SpriteDirection,1);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            var direction = Vector2.Zero;
            if (state.IsKeyDown(Keys.Left))
            {
                direction.X -= 1;
                SpriteDirection = SpriteEffects.FlipHorizontally;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                direction.X += 1;
                SpriteDirection = SpriteEffects.None;
            }
            if (state.IsKeyDown(Keys.Up))
            {
                direction.Y -= 1;
            }
            if (state.IsKeyDown(Keys.Down))
            {
                direction.Y += 1;
            }
            direction *= snelheid;
            positie += direction;

            animatie.Update(gameTime);

           

        }
        public void Move()
        {
            
        }
    }
       
            
}
