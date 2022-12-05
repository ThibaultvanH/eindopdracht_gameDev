using eindopdracht.animation;
using eindopdracht.blocks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
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
    class Hero : IGameObject, IMovable 
    {
        Texture2D heroTexture;
        Animatie animatie;
        private Vector2 positie;
        


        private Vector2 snelheid = new Vector2(1.2f,1.2f);
        public Vector2 velocity = new Vector2();


        private SpriteEffects SpriteDirection;
        private MovementManager movementManager;
        private Vector2 oldpos;
        private Activity activity;
        private Texture2D Bloktexture;
        
        private int height = 50;
        public Rectangle blokrec = new Rectangle(0, 0, 50, 50);
        public Rectangle feet = new Rectangle(0, 0, 30, 5);
        public Rectangle body = new Rectangle(0, 0, 30, 5);
        public Rectangle fist = new Rectangle(0, 0, 5, 15);
        public Rectangle head = new Rectangle(0, 0, 5, 15);
        public bool isheadtouching = false;


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
        Vector2 IMovable.veloCity { get => velocity; set => velocity = value; }

        public void Draw(SpriteBatch spriteBatch)
        {
            

            switch (activity)
            {
                case Activity.running:

                    
                    feet = new Rectangle((int)positie.X, (int)positie.Y+ height, 40, 2);
                    blokrec = new Rectangle((int)positie.X, (int)positie.Y, 40, height);                    
                    
                    body = animatie.CurrentFrame.SourceRectangle;
                    break;

                case Activity.standing:

                    
                    feet = new Rectangle((int)positie.X, (int)positie.Y + height, 40, 2);

                    blokrec = new Rectangle((int)positie.X, (int)positie.Y, 25, height);
                    
                    body = new Rectangle(0, 0, 48, 50);
                    break;

                case Activity.jumping:
                    break;

                case Activity.crouching:

                    
                    feet = new Rectangle((int)positie.X, (int)positie.Y + height, 40, 2);
                    blokrec = new Rectangle((int)positie.X, (int)positie.Y, 34, height);
                    
                    body = new Rectangle(46, 0, 48, 50);
                    break;

                case Activity.fighting:

                    
                    feet = new Rectangle((int)positie.X, (int)positie.Y + height, 40, 2);
                    blokrec = new Rectangle((int)positie.X, (int)positie.Y, 32, height);
                    fist = new Rectangle((int)positie.X+330, (int)positie.Y + 10, 10, 50);
                    if (SpriteDirection == SpriteEffects.FlipHorizontally)
                    {
                        spriteBatch.Draw(Bloktexture, new Vector2(positie.X , positie.Y + 2), fist, Color.Yellow);
                    }
                    else
                    {
                        spriteBatch.Draw(Bloktexture, new Vector2(positie.X + 40, positie.Y + 2), fist, Color.Yellow);
                    }
                    
                    
                    

                   
                    body = new Rectangle(138, 0, 48, 50);
                    break;

                case Activity.faling:


                    
                    blokrec = new Rectangle((int)positie.X, (int)positie.Y, 32, height);
                    feet = new Rectangle((int)positie.X, (int)positie.Y + height, 40, 2);
                    
                    body = new Rectangle(92, 50, 48, 50);
                    break;

                default:
                    
                    break;
            }
            spriteBatch.Draw(heroTexture, positie, body, Color.White, 0, new Vector2(0, 0), 1, SpriteDirection, 1);

        }

        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            head = new Rectangle((int)positie.X, (int)positie.Y , 10, 50);
            if (activity != Activity.crouching )Move();
            
            if (isTouchingGround())
            {
                velocity.Y = 0;
                isheadtouching = false;
                if (state.IsKeyDown(Keys.Up))
                {
                    velocity.Y -= 400;
                }
            }
            else if(headTouchingGround() && isheadtouching == false)
            {
                if (velocity.Y < 0)
                {
                    velocity.Y = 0;
                }
                
                isheadtouching = true;
            }
            else
            {                
                velocity.Y += 12 ;
            }
           positie.Y += snelheid.Y * velocity.Y * dt;
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
            if (!isTouchingGround())
            {
                activity = Activity.faling;
            }
            
            
        }

        private bool isTouchingGround()
        {
            foreach (var item in Game1.grassup)
            {                              
                if (item.Intersects(feet))
                {                    
                        return true;
                }
            }

            return false;

        }
        private bool headTouchingGround()
        {
            foreach (var item in Game1.grassdown)
            {
                if (item.Intersects(head))
                {
                    return true;
                }
            }

            return false;

        }

    }


}
