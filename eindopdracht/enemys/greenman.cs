﻿using eindopdracht.animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eindopdracht.enemys
{

    enum Activity
    {
        walking,
        standing,
        fighting,
        hurt,
        die
    }
     class greenman : person
    {

        Animatie walking;
        Animatie fighting;
        Animatie die;
        Texture2D greenmanTexture;
        private Activity activity;
        double cooldownCounter = 0;
        double fightingcooldown = 0;
        double diecounter = 0;

        public Vector2 positie = new Vector2(50, 50);
        private Vector2 snelheid = new Vector2(1.2f, 1.2f);
        private Vector2 velocity = new Vector2();
        private Rectangle goingRight = new Rectangle(0,0,10,10);
        private Rectangle goingLeft = new Rectangle(0,0,10,10);
        private Rectangle bat = new Rectangle(0,0,10,10);
        
        Hero hero;
        public greenman(Texture2D greenmanTexture, Hero hero)
        {
            walking = new Animatie();
            walking.AddFrame(new AnimationFrame(new Rectangle(0, 0, 44, 35)));
            walking.AddFrame(new AnimationFrame(new Rectangle(46, 0, 44, 35)));
            walking.AddFrame(new AnimationFrame(new Rectangle(94, 0, 44, 35)));
            walking.AddFrame(new AnimationFrame(new Rectangle(142, 0, 44, 35)));
            walking.AddFrame(new AnimationFrame(new Rectangle(190, 0, 44, 35)));

            fighting = new Animatie();
            fighting.AddFrame(new AnimationFrame(new Rectangle(0, 112, 44, 35)));
            fighting.AddFrame(new AnimationFrame(new Rectangle(46, 112, 44, 35)));
            fighting.AddFrame(new AnimationFrame(new Rectangle(94, 112, 44, 35)));
            fighting.AddFrame(new AnimationFrame(new Rectangle(142, 112, 44, 35)));
            fighting.AddFrame(new AnimationFrame(new Rectangle(190, 112, 44, 35)));
            fighting.AddFrame(new AnimationFrame(new Rectangle(236, 112, 44, 35)));

            die = new Animatie();
            die.AddFrame(new AnimationFrame(new Rectangle(0, 37, 44, 35)));
            die.AddFrame(new AnimationFrame(new Rectangle(46, 37, 44, 35)));
            die.AddFrame(new AnimationFrame(new Rectangle(94, 37, 44, 35)));
            die.AddFrame(new AnimationFrame(new Rectangle(142, 37, 44, 35)));
            die.AddFrame(new AnimationFrame(new Rectangle(190, 37, 44, 35)));
            die.AddFrame(new AnimationFrame(new Rectangle(236, 37, 44, 35)));

            health = 100;
            
            this.greenmanTexture = greenmanTexture;
            this.hero = hero;
            

        }
        

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            head = new Rectangle((int)positie.X, (int)positie.Y, 10, 50);
            feet = new Rectangle((int)positie.X, (int)positie.Y + 44, 40, 2);
            blokrec = new Rectangle((int)positie.X, (int)positie.Y, 44, height);
            
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
            walkingdirection(gameTime);
            Move();
            checkhits();
            fight();
            fighting.Update(gameTime);
            
            died(gameTime);
            
            cooldownCounter += gameTime.ElapsedGameTime.TotalMilliseconds;
            fightingcooldown += gameTime.ElapsedGameTime.TotalMilliseconds;

        }

        private void died(GameTime gameTime)
        {
            if (health <= 0)
            {
                die.Update(gameTime);
                activity = Activity.die;
                diecounter += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (diecounter > 500)
                {
                    dead = true;
                }
            }
            
        }

        private void fight()
        {
            if (GetDistance((double)hero.positie.X, (double)hero.positie.Y, (double)positie.X, (double)positie.Y) <= 50)
            {
                

                if (fighting.counter != 0)
                {
                    activity = Activity.fighting;
                    bat = new Rectangle((int)positie.X, (int)positie.Y, 10, height);
                }
                else
                {
                    fighting.counter = 0;
                    fighting.secondCounter = 0;
                    bat = new Rectangle(-200, 0, 0, 0);
                    if (cooldownCounter >= 4000) // 1000 milliseconds = 1 second
                    {
                        fighting.counter = 1;
                        cooldownCounter = 0;
                    }
                }

                    
                
            }
        }

        private void checkhits()
        {
            if (fightingcooldown >= 200)
            {   



                if (hero.fist.Intersects(blokrec))
                {

                    hero.fist = new Rectangle(0, -100, 0, 0);
                    health -= 5;
                    isHit = true;
                    activity = Activity.hurt;
                }

                if (bat.Intersects(hero.blokrec))
                {
                    hero.isHit = true;
                    hero.health -= 1;
                    bat = new Rectangle(-100, 0, 0, 0);
                }

                fightingcooldown = 0;
            }
            
            
            
        }

        private static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        private void walkingdirection(GameTime gameTime)
        {
            
                goingRight = new Rectangle((int)positie.X + 25, (int)positie.Y + height, 30, 15);
            
            
                goingLeft = new Rectangle((int)positie.X - 20, (int)positie.Y + height, 30, 15);
           
            


            if (oldpos.X < positie.X)
            {
                SpriteDirection = SpriteEffects.None;
                walking.Update(gameTime);
                activity = Activity.walking;

            }

            else if (oldpos.X > positie.X)
            {
                SpriteDirection = SpriteEffects.FlipHorizontally;
                
                walking.Update(gameTime);
                activity = Activity.walking;
            }
            else
            {
                activity = Activity.standing;
            }
            oldpos.X = positie.X;
        }

        public override void Draw(SpriteBatch spriteBatch)     
        {
            switch (activity)
            {
                case Activity.walking:
                    body = walking.CurrentFrame.SourceRectangle;
                    break;
                case Activity.standing:
                    body = new Rectangle(0, 112, 44, 55);
                    
                    break;
                case Activity.fighting:
                    
                    body = fighting.CurrentFrame.SourceRectangle;
                    break;
                case Activity.hurt:
                    body = new Rectangle(44, 72, 44, 42);
                    break;
                case Activity.die:
                    body = die.CurrentFrame.SourceRectangle;   
                    break;
                default:
                   
                    break;
            }
            
            spriteBatch.Draw(greenmanTexture, positie, body, Color.White, 0, new Vector2(0, 0), 1.2f, SpriteDirection, 1);
        }

        public override void Move()
        {
            
            if (GetDistance((double)hero.positie.X, (double)hero.positie.Y, (double)positie.X, (double)positie.Y) <= 200)
                {

                if (onplatform(goingRight)) {
                    if (hero.positie.X > positie.X)
                    {
                        positie.X += snelheid.X;
                    }
                }
                if (onplatform(goingLeft))
                {
                    if (hero.positie.X < positie.X)
                    {
                        positie.X -= snelheid.X;
                    }

                }
                

                /*
                if ()
                {
                    if (hero.positie.X > positie.X+5)
                    {
                        positie.X += snelheid.X;
                        goingright = true;
                        
                    }
                    else if (hero.positie.X < positie.X-5)
                    {
                        positie.X -= snelheid.X;
                        goingright = false;                        
                    }                
                }
                else 
                {               
                    if (hero.positie.X > positie.X )
                    {
                        positie.X += snelheid.X;
                    }
                    else if (hero.positie.X < positie.X)
                    {
                        positie.X -= snelheid.X;
                    }
                    else
                    {

                    }

                }
                */
            }



        }
        

            public bool onplatform(Rectangle going)
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
