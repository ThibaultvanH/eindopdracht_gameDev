using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindopdracht.enemys
{
    internal class spike
    {
        Texture2D SpikeTexture;
        public Vector2 position;
        Rectangle spikerect;
        Hero hero;
        double counter = 0;
        bool hit = false;
        public spike(Texture2D spikeTexture,Hero hero)
        {
            SpikeTexture = spikeTexture;
            this.hero = hero;
            
        }

        public void Update(GameTime gameTime)
        {
            spikerect = new Rectangle((int)position.X, (int)position.Y, (int)SpikeTexture.Width, (int)SpikeTexture.Height);
            if (hero.feet.Intersects(spikerect) && !hit)
            {
                hit= true;
               
                hero.health -= 5;
                counter = 0;    
                
                
            }
            if (counter> 4000)
            {
                hit = false;
            }
            if (hit)
            {
                counter += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            
        }

        public void Draw(SpriteBatch spritebach)
        {
            spritebach.Draw(SpikeTexture, spikerect, Color.Aqua);
        }
    }
}
