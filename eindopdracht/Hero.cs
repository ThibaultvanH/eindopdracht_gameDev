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
enum Activity
{
    running,
    standing,
    jumping
}

namespace eindopdracht
{
    internal class Hero : IGameObject, IMovable
    {
        Texture2D heroTexture;
        Animatie animatie;
        private Vector2 positie;
        private Vector2 snelheid;
        private SpriteEffects SpriteDirection;
        private MovementManager movementManager;
        private Vector2 oldpos;
        private Rectangle stangingrect;

        private Activity activity;

        public Hero(Texture2D texture)
        {
            heroTexture = texture;
            animatie = new Animatie();
            positie = new Vector2(0, 0);
            movementManager = new MovementManager();

            animatie.AddFrame(new AnimationFrame(new Rectangle(0, 150, 48, 50)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(46, 150, 48, 50)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(92, 150, 48, 50)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(138, 150, 48, 50)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(184, 150, 48, 50)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(230, 150, 48, 50)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(276, 150, 48, 50)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(322, 150, 48, 50)));
        }

         Vector2 IMovable.Position { get => positie; set => positie = value; }
         Vector2 IMovable.Speed { get => snelheid; set => snelheid = value; }
        

        public void Draw(SpriteBatch spriteBatch)
        {

            snelheid = new Vector2(1, 1);


            
                
            
            switch (activity)
            {
                case Activity.running:
                    spriteBatch.Draw(heroTexture, positie, animatie.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, SpriteDirection, 1);
                    break;
                case Activity.standing:
                    spriteBatch.Draw(heroTexture, positie, new Rectangle(0, 0, 48, 50), Color.White, 0, new Vector2(0, 0), 1, SpriteDirection, 1);

                    break;
                case Activity.jumping:

                default:
                    break;
            }

        }

        public void Update(GameTime gameTime)
        {

            Move();

            

            if (oldpos.X < positie.X)
            {
                SpriteDirection = SpriteEffects.None;
                animatie.Update(gameTime);
                activity = Activity.running;
            }
            else if(oldpos.X > positie.X)
            {
                SpriteDirection = SpriteEffects.FlipHorizontally;
                animatie.Update(gameTime);
                activity=Activity.running;
            }else  if (oldpos.X == positie.X)
            {
                activity = Activity.standing;
            }
            
            oldpos.X = positie.X;

        }
        private void Move()
        {
            movementManager.Move(this);
        }

        
    }


}
