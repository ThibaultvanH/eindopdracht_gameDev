using eindopdracht.blocks;
using eindopdracht.enemys;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace eindopdracht.levels
{
    internal class level1 : level
    {

        Hero hero;
        greenman greenman1;
        greenman greenman2;
        greenman greenman3;
        ridder ridder1;
        spike spike1;

        List<person> persons = new List<person>();

        public level1(Texture2D HeroTexture, Texture2D blockTexture, Texture2D ButtonTexture, Texture2D GreenmanTexture,Texture2D ridderTexture,Texture2D SpikeTexture , GraphicsDevice dev , SpriteFont font) : base(HeroTexture, blockTexture,  ButtonTexture, GreenmanTexture,ridderTexture, SpikeTexture , dev, font)
        {
        Level = new int[,]
        {
    { 0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
    { 0,0,0,0,0,0,0,0,1,1,0,0,0,0 },
    { 0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
    { 0,1,1,1,1,0,0,0,0,0,0,0,0,0 },
    { 0,0,0,0,0,0,0,0,0,1,0,0,0,0 },
    { 0,0,0,0,0,0,0,1,1,2,1,0,0,0 },
    { 1,1,1,1,1,1,1,2,2,2,2,1,1,1 },
    { 2,2,2,2,2,2,2,2,2,2,2,2,2,2 }
        };
            
            hero = new Hero(HeroTexture);
            persons.Add(hero);
            greenman1 = new greenman(GreenmanTexture, hero);
            greenman2 = new greenman(GreenmanTexture, hero);
            greenman2.positie = new Vector2(500, 10);
            greenman3 = new greenman(GreenmanTexture, hero);
            greenman3.positie = new Vector2(600, 10);
            ridder1 = new ridder(ridderTexture, hero);
            ridder1.positie = new Vector2(200, 200);
            spike1 = new spike(SpikeTexture, hero);
            spike1.position = new Vector2(460, 290);
            persons.Add(greenman1);
            persons.Add(greenman2);
            persons.Add(greenman3);
            persons.Add(ridder1);
            
            CreateBlocks();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            menubutton.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(font, "health: " + hero.health.ToString(), new Vector2(300, 0), Color.Black);
            spike1.Draw(spriteBatch);
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
            if (hero.health <= 0)
            {
                Gameover();
            }
            menubutton.Update(gameTime);
            spike1.Update(gameTime);
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
    }

}
