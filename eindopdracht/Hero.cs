using eindopdracht.animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;

enum Activity
{
    running,
    standing,
    jumping,
    crouching,
    fighting,
    faling
}

namespace eindopdracht
{
    internal class Hero : IGameObject, IMovable
    {
        Texture2D heroTexture;
        Animatie animatie;
        private Vector2 positie;
        private int height = 50;
        private Vector2 snelheid = new Vector2(1,1);
        private SpriteEffects SpriteDirection;
        private MovementManager movementManager;
        private Vector2 oldpos;
        private Activity activity;
        private Texture2D Bloktexture;
        public Rectangle blokrec = new Rectangle(0, 0, 50, 50);
        public Rectangle feet = new Rectangle(0, 0, 30, 5);
        public Rectangle body = new Rectangle(0, 0, 30, 5);
        public bool isFaling = true;
        




        public Hero(Texture2D texture, Texture2D bloktexture)
        {
            heroTexture = texture;
            Bloktexture = bloktexture;


            animatie = new Animatie();
            positie = new Vector2(0, 200);
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
            

            switch (activity)
            {
                case Activity.running:

                    height = 46;
                    feet = new Rectangle((int)positie.X, (int)positie.Y+ height, 40, 2);
                    blokrec = new Rectangle((int)positie.X, (int)positie.Y, 40, height);                    
                    spriteBatch.Draw(Bloktexture, new Vector2(positie.X , positie.Y +10), blokrec, Color.White);
                    body = animatie.CurrentFrame.SourceRectangle;
                    break;

                case Activity.standing:

                    height = 50;
                    feet = new Rectangle((int)positie.X, (int)positie.Y + height, 40, 2);

                    blokrec = new Rectangle((int)positie.X, (int)positie.Y, 25, height);
                    spriteBatch.Draw(Bloktexture,new Vector2(positie.X +10 , positie.Y+2), blokrec, Color.White);
                    body = new Rectangle(0, 0, 48, 50);
                    break;

                case Activity.jumping:
                    break;

                case Activity.crouching:

                    height = 46;
                    feet = new Rectangle((int)positie.X, (int)positie.Y + height, 40, 2);
                    blokrec = new Rectangle((int)positie.X, (int)positie.Y, 34, height);
                    spriteBatch.Draw(Bloktexture, new Vector2(positie.X +5, positie.Y + 17), blokrec, Color.White);
                    body = new Rectangle(46, 0, 48, 50);
                    break;

                case Activity.fighting:

                    height = 46;
                    feet = new Rectangle((int)positie.X, (int)positie.Y + height, 40, 2);
                    blokrec = new Rectangle((int)positie.X, (int)positie.Y, 32, height);
                    spriteBatch.Draw(Bloktexture, new Vector2(positie.X + 10, positie.Y + 2), blokrec, Color.White);
                    body = new Rectangle(138, 0, 48, 50);
                    break;

                case Activity.faling:


                    height = 46;
                    blokrec = new Rectangle((int)positie.X, (int)positie.Y, 32, height);
                    feet = new Rectangle((int)positie.X, (int)positie.Y + height, 40, 2);
                    spriteBatch.Draw(Bloktexture, new Vector2(positie.X + 10, positie.Y + 2), blokrec, Color.White);
                    body = new Rectangle(92, 50, 48, 50);
                    break;

                default:
                    
                    break;
            }
            spriteBatch.Draw(heroTexture, positie, body, Color.White, 0, new Vector2(0, 0), 1, SpriteDirection, 1);

        }

        public void Update(GameTime gameTime)
        {
            if (activity != Activity.crouching )
            Move();
            if (isFaling && activity != Activity.crouching)
            {
                positie.Y += 2;
                Debug.WriteLine(isFaling);
            }
            activitys(gameTime);
            


        }
        private void Move()
        {
            movementManager.Move(this);
        }
        private void activitys(GameTime gameTime)
        {
            if (oldpos.X < positie.X)
            {
                SpriteDirection = SpriteEffects.None;
                animatie.Update(gameTime);
                activity = Activity.running;
            }
            else if (oldpos.X > positie.X)
            {
                SpriteDirection = SpriteEffects.FlipHorizontally;
                animatie.Update(gameTime);
                activity = Activity.running;
            } else if (oldpos.X == positie.X)
            {
                activity = Activity.standing;
            }

            oldpos.X = positie.X;
        
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Down))
            {
                activity = Activity.crouching;
            }
            if (state.IsKeyDown(Keys.Space))
            {
                activity = Activity.fighting; 
            }
            if (isFaling) activity = Activity.faling;
            
        }


}


}
