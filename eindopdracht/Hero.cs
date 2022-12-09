using eindopdracht.animation;
using eindopdracht.blocks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.Linq;
using System.Security.Policy;
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
    class Hero : person, IMovable
    {
        Texture2D heroTexture;
        Animatie animatie;
        MovementManager movementManager;

        public Vector2 positie;
        private Vector2 snelheid = new Vector2(1.2f, 1.2f);
        public Vector2 velocity = new Vector2();

        private Activity activity;
        private Texture2D Bloktexture;

        private bool left = false;
        private bool right = false;

        public Hero(Texture2D texture, Texture2D bloktexture)
        {
            heroTexture = texture;
            Bloktexture = bloktexture;


            animatie = new Animatie();
            positie = new Vector2(200, 200);
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
        Vector2 IMovable.veloCity { get => velocity; set => velocity = value; }
        

        bool IMovable.Right { get => right; set => right = value; }
        bool IMovable.Left { get => left; set => left = value ; }

        public override void Draw(SpriteBatch spriteBatch)
        {


            switch (activity)
            {
                case Activity.running:



                    blokrec = new Rectangle((int)positie.X , (int)positie.Y, 40, height);

                    body = animatie.CurrentFrame.SourceRectangle;
                    break;

                case Activity.standing:

                    blokrec = new Rectangle((int)positie.X, (int)positie.Y, 40, height);

                    body = new Rectangle(0, 0, 48, 50);
                    break;

                case Activity.jumping:
                    break;

                case Activity.crouching:



                    blokrec = new Rectangle((int)positie.X, (int)positie.Y, 34, height);

                    body = new Rectangle(46, 0, 48, 50);
                    break;

                case Activity.fighting:

                    blokrec = new Rectangle((int)positie.X, (int)positie.Y, 32, height);
                    
                    if (SpriteDirection == SpriteEffects.FlipHorizontally)
                    {
                       
                        fist = new Rectangle((int)positie.X , (int)positie.Y + 2, 10, 50);
                    }
                    else
                    {
                        fist = new Rectangle((int)positie.X + 40, (int)positie.Y + 10, 10, 50);
                       
                    }

                    body = new Rectangle(138, 0, 48, 50);
                    break;

                case Activity.faling:



                    blokrec = new Rectangle((int)positie.X, (int)positie.Y, 32, height);

                    body = new Rectangle(92, 50, 48, 50);
                    break;

                default:

                    break;
            }

            spriteBatch.Draw(heroTexture, positie, body, Color.White, 0, new Vector2(0, 0), 1, SpriteDirection, 1);

        }


        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            head = new Rectangle((int)positie.X, (int)positie.Y, 10, 50);
            feet = new Rectangle((int)positie.X, (int)positie.Y + height, 40, 2);
            if (activity != Activity.crouching) Move();

            if (isTouchingGround())
            {
                velocity.Y = 0;
                isheadtouching = false;
                if (state.IsKeyDown(Keys.Up))
                {
                    velocity.Y -= 400;
                }
            }
            else if (headTouchingGround() && isheadtouching == false)
            {
                if (velocity.Y < 0)
                {
                    velocity.Y = 0;
                }

                isheadtouching = true;
            }
            else
            {
                velocity.Y += 12;
            }
            positie.Y += snelheid.Y * velocity.Y * dt;
            activitys(gameTime);
        }

        public override void Move()
        {

            if (istouchingleft())
            {
                left = true;
            }
            else
            {
                left = false;
            }

            if (istouchingright()) 
            {
                right = true;
            }
            else
            {
                right = false;
            }
            

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
            }
            else if (oldpos.X == positie.X)
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
            if (!isTouchingGround())
            {
                activity = Activity.faling;
            }


        }


    }


}
