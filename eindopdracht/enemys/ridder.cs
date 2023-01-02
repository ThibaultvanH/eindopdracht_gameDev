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
    internal class ridder : person
    {
        enum Activity
        {
            walking,
            standing,
            fighting,
            hurt,
            die
        }
        Animatie walking;
        Animatie fighting;
        Animatie die;
        Texture2D ridderTexture;
        private Activity activity;
        double cooldownCounter = 0;
        double fightingcooldown = 0;
        double diecounter = 0;

        public Vector2 positie = new Vector2(50, 50);
        private Vector2 snelheid = new Vector2(1.2f, 1.2f);
        private Vector2 velocity = new Vector2();
        private Rectangle goingRight = new Rectangle(0, 0, 10, 10);
        private Rectangle goingLeft = new Rectangle(0, 0, 10, 10);
        private Rectangle bat = new Rectangle(0, 0, 10, 10);

        Hero hero;

        public ridder(Texture2D ridderTexture, Hero hero)
        {

            walking = new Animatie();
            walking.AddFrame(new AnimationFrame(new Rectangle(0, 175, 50, 55)));
            walking.AddFrame(new AnimationFrame(new Rectangle(60, 175, 50, 55)));
            walking.AddFrame(new AnimationFrame(new Rectangle(120, 175, 50, 55)));
            walking.AddFrame(new AnimationFrame(new Rectangle(180, 175, 50, 55)));
            walking.AddFrame(new AnimationFrame(new Rectangle(240, 175, 50, 55)));

            fighting = new Animatie();
            fighting.AddFrame(new AnimationFrame(new Rectangle(0, 0, 50, 55)));
            fighting.AddFrame(new AnimationFrame(new Rectangle(60, 0, 35, 55)));
            fighting.AddFrame(new AnimationFrame(new Rectangle(100, 0, 50, 55)));
            fighting.AddFrame(new AnimationFrame(new Rectangle(170, 0, 50, 55)));


            die = new Animatie();
            die.AddFrame(new AnimationFrame(new Rectangle(0, 60, 40, 55)));
            die.AddFrame(new AnimationFrame(new Rectangle(44, 60, 50, 55)));
            die.AddFrame(new AnimationFrame(new Rectangle(100, 60, 50, 55)));
            die.AddFrame(new AnimationFrame(new Rectangle(160, 60, 50, 55)));


            height = 55;

            this.ridderTexture = ridderTexture;
            this.hero = hero;
        }
        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            head = new Rectangle((int)positie.X, (int)positie.Y, 10, 50);
            feet = new Rectangle((int)positie.X, (int)positie.Y + 55, 40, 2);
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
            if (GetDistance(hero.positie.X, hero.positie.Y, positie.X, positie.Y) <= 50)
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
        private static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }
        private void walkingdirection(GameTime gameTime)
        {

            goingRight = new Rectangle((int)positie.X + 40, (int)positie.Y + height, 30, 15);


            goingLeft = new Rectangle((int)positie.X - 20, (int)positie.Y + height, 30, 15);




            if (oldpos.X < positie.X)
            {
                SpriteDirection = SpriteEffects.FlipHorizontally;
                walking.Update(gameTime);
                activity = Activity.walking;

            }

            else if (oldpos.X > positie.X)
            {
                SpriteDirection = SpriteEffects.None;

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
                    body = new Rectangle(0, 0, 44, 60);

                    break;
                case Activity.fighting:

                    body = fighting.CurrentFrame.SourceRectangle;
                    break;
                case Activity.hurt:
                    body = new Rectangle(0, 120, 44, 55);
                    break;
                case Activity.die:
                    body = die.CurrentFrame.SourceRectangle;
                    break;
                default:

                    break;
            }

            spriteBatch.Draw(ridderTexture, positie, body, Color.White, 0, new Vector2(0, 0), 1.2f, SpriteDirection, 1);
        }

        public override void Move()
        {
            if (GetDistance(hero.positie.X, hero.positie.Y, positie.X, positie.Y) <= 200)
            {

                if (onplatform(goingRight))
                {
                    if (hero.positie.X - 20 > positie.X)
                    {
                        positie.X += snelheid.X;
                    }
                }
                if (onplatform(goingLeft))
                {
                    if (hero.positie.X + 10 < positie.X)
                    {
                        positie.X -= snelheid.X;
                    }

                }
            }


        }
        public bool onplatform(Rectangle going)
        {
            foreach (var item in Game1.grassup)
            {
                if (item.Intersects(going))
                {
                    return true;
                }

            }
            return false;
        }
    }
}
