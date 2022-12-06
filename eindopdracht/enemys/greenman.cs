using eindopdracht.animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindopdracht.enemys
{
     class greenman : person
    {

        Animatie walking;
        Texture2D greenmanTexture;
        
        private Vector2 positie = new Vector2(50, 50);
        private Vector2 snelheid = new Vector2(1.2f, 1.2f);
        private Vector2 velocity = new Vector2();
        private Rectangle going = new Rectangle(0,0,10,10);
        private bool goingright = true;
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
        

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            head = new Rectangle((int)positie.X, (int)positie.Y, 10, 50);
            feet = new Rectangle((int)positie.X, (int)positie.Y + height, 40, 2);
            
            if (isTouchingGround())
            {
                velocity.Y = 0;
                isheadtouching = false;
            }
            else
            {
                velocity.Y += 12;
            }
            positie.Y += snelheid.Y * velocity.Y * dt;
            walkingdirection();
            Move();
            walking.Update(gameTime);
        }

        private void walkingdirection()
        {
            going = new Rectangle((int)positie.X + 30, (int)positie.Y + height, 30, 15);
            if (oldpos.X < positie.X)
            {
                SpriteDirection = SpriteEffects.None;
                
            }
            else if (oldpos.X > positie.X)
            {
                SpriteDirection = SpriteEffects.FlipHorizontally;
                going = new Rectangle((int)positie.X -40, (int)positie.Y + height, 30, 15);
            }
            oldpos.X = positie.X;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            body = walking.CurrentFrame.SourceRectangle;
            spriteBatch.Draw(greenmanTexture, positie, body, Color.White, 0, new Vector2(0, 0), 1.2f, SpriteDirection, 1);
        }

        public override void Move()
        {
            
            if (!onplatform())
            {
                goingright = !goingright;
                going = new Rectangle((int)positie.X - 40, (int)positie.Y + height, 30, 15);
            }
            if (onplatform())
            {
                if (goingright)
                {
                    positie.X += snelheid.X;
                }
                else
                {
                    positie.X -= snelheid.X;
                }
            }
            Debug.WriteLine("plat:");
            Debug.WriteLine(onplatform());
            Debug.WriteLine("dir:");
            Debug.WriteLine(goingright);



        }
        public bool onplatform()
        {
           foreach (var item in Game1.grassup)
            {
               if(item.Intersects(going))
                {
                    return true;
                }
                
            }
           return false;
        }
    }
}
