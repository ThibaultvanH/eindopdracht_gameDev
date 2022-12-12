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
        List<person> persons = new List<person>();

        public level1(Texture2D HeroTexture, Texture2D blockTexture, Texture2D ButtonTexture, Texture2D GreenmanTexture, GraphicsDevice dev) : base(HeroTexture, blockTexture,  ButtonTexture, GreenmanTexture,  dev)
        {
        Level = new int[,]
        {
    { 0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
    { 0,0,0,0,0,0,0,0,1,1,0,0,0,0 },
    { 0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
    { 1,1,1,1,1,0,0,0,0,0,0,0,0,0 },
    { 0,0,0,0,0,0,0,0,0,1,0,0,0,0 },
    { 0,0,0,0,0,0,0,1,1,2,1,0,0,0 },
    { 1,1,1,1,1,1,1,2,2,2,2,1,1,1 },
    { 2,2,2,2,2,2,2,2,2,2,2,2,2,2 }
        };
            
            hero = new Hero(HeroTexture);
            persons.Add(hero);
            greenman1 = new greenman(GreenmanTexture, hero);
            persons.Add(greenman1);
            
            CreateBlocks();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            menubutton.Draw(gameTime, spriteBatch);
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
            
            menubutton.Update(gameTime);
            foreach (person per in persons)
            {
                per.Update(gameTime);
            }
        }
    }

}
