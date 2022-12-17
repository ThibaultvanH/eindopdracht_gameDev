using eindopdracht.blocks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindopdracht.levels
{
    internal class level2 : level
    {
        Hero hero;
        public level2(Texture2D HeroTexture, Texture2D blockTexture, Texture2D ButtonTexture, Texture2D GreenmanTexture,Texture2D ridderTexture,Texture2D SpikeTexture, GraphicsDevice dev, SpriteFont font) : base(HeroTexture, blockTexture, ButtonTexture, GreenmanTexture, ridderTexture,SpikeTexture, dev , font)
        {
            Level = new int[,]
    {
    { 0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
    { 0,0,0,0,0,0,0,0,1,1,0,1,0,0 },
    { 0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
    { 1,1,1,1,1,0,0,0,0,0,0,0,0,0 },
    { 0,0,0,0,0,0,0,0,0,1,0,0,0,0 },
    { 0,0,0,0,0,0,0,1,1,2,1,0,0,0 },
    { 1,1,1,1,1,1,1,2,2,2,2,1,1,1 },
    { 2,2,2,2,2,2,2,2,2,2,2,2,2,2 }
    };
            hero = new Hero(HeroTexture);
            CreateBlocks();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            hero.Draw(spriteBatch);
            menubutton.Draw(gameTime, spriteBatch);
            foreach (Block block in blocks)
            {
                block?.Draw(spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {

            hero.Update(gameTime);
            menubutton.Update(gameTime);
        }
    }
}
