using eindopdracht.blocks;
using eindopdracht.enemys;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace eindopdracht.levels
{
    internal class level1 : level
    {

        Hero hero;       
        spike spike1;
        spike spike2;
        List<person> persons = new List<person>();
        

        public level1(Texture2D HeroTexture, Texture2D blockTexture, Texture2D ButtonTexture, Texture2D GreenmanTexture,Texture2D ridderTexture,Texture2D SpikeTexture , GraphicsDevice dev , SpriteFont font) : base(HeroTexture, blockTexture,  ButtonTexture, GreenmanTexture,ridderTexture, SpikeTexture , dev, font)
        {
        Level = new int[,]
        {
    { 0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
    { 0,0,0,0,0,0,0,0,1,1,1,1,1,1 },
    { 0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
    { 1,1,1,1,1,0,0,0,0,0,0,0,0,0 },
    { 0,0,0,0,0,0,0,0,0,1,0,0,0,0 },
    { 0,0,0,0,0,0,0,1,1,2,1,0,0,0 },
    { 1,1,1,1,0,1,1,2,2,2,2,1,1,1 },
    { 2,2,2,2,1,2,2,2,2,2,2,2,2,2 }
        };
            
            hero = new Hero(HeroTexture);
            persons.Add(hero);
            
            wavechecker();
            CreateBlocks();
        }

        void wave1()
        {
            wave = 1;
            persons.Add( new greenman(GreenmanTexture, hero));
            persons.Add( new greenman(GreenmanTexture, hero) { positie = new Vector2(500, 10) });
            persons.Add( new greenman(GreenmanTexture, hero) { positie = new Vector2(650, 100) });
            persons.Add( new ridder(ridderTexture, hero) { positie = new Vector2(200, 000) });
            
            
            spike1 = new spike(SpikeTexture, hero) { position = new Vector2(460, 290) };
           
        }

        void wave2()
        {

            wave = 2;
            
            spike1 = new spike(SpikeTexture, hero) { position = new Vector2(200, 160) };           
            persons.Add(new greenman(GreenmanTexture, hero) { positie = new Vector2(500, 10) });
            persons.Add (new greenman(GreenmanTexture, hero) { positie = new Vector2(650, 100) });
            persons.Add(new ridder(ridderTexture, hero) { positie = new Vector2(100, 300) });
            persons.Add(new ridder(ridderTexture, hero) { positie = new Vector2(300, 100) });
        }
        void wave3()
        {

            wave = 3;

            spike1 = new spike(SpikeTexture, hero) { position = new Vector2(200, 160) };
            spike2 = new spike(SpikeTexture, hero) { position = new Vector2(460, 290) };
            persons.Add(new greenman(GreenmanTexture, hero) { positie = new Vector2(500, 10) });
            persons.Add(new greenman(GreenmanTexture, hero) { positie = new Vector2(650, 100) });
            persons.Add(new greenman(GreenmanTexture, hero) { positie = new Vector2(550, 100) });
            persons.Add(new ridder(ridderTexture, hero) { positie = new Vector2(100, 300) });
            persons.Add(new ridder(ridderTexture, hero) { positie = new Vector2(300, 100) });
            persons.Add(new ridder(ridderTexture, hero) { positie = new Vector2(100, 100) });
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            menubutton.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(font, "health: " + hero.health.ToString(), new Vector2(300, 0), Color.Black);
            spriteBatch.DrawString(font, "wave: " + wave.ToString(), new Vector2(400, 0), Color.Black);
            spriteBatch.DrawString(font, wavestr, new Vector2(300, 0), Color.Black);
            spike1.Draw(spriteBatch);
            spike2?.Draw(spriteBatch);
            foreach (Block block in blocks)
            {
                block?.Draw(spriteBatch);
            }
            foreach (person per in persons)
            {
                per.Draw(spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalSeconds;
            if (hero.health <= 0)
            {
                Gameover();
            }
            menubutton.Update(gameTime);
            spike1.Update(gameTime);
            spike2?.Update(gameTime);
            wavechecker();
            person temp = null;
            foreach (person per in persons)
            {
                per.Update(gameTime);
                if (per.dead)
                {
                    temp = per;              
                }
            }
            if (temp != null)
            {
                persons.Remove(temp);
                temp = null;
            }
        }

        void wavechecker()
        {
            if (persons.Count == 1)
            {

                switch (wave)
                {
                    case 0:
                        wave1();
                        break;
                    case 1:
                        wave2();
                        break;
                    case 2:
                        wave3();
                        break;
                    case 3:
                        levelpassed();
                        break;
                    default:
                        break;
                }

            }
        }
    }

}
