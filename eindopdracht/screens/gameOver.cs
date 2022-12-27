using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Vector2 = Microsoft.Xna.Framework.Vector2;
using Viewport = Microsoft.Xna.Framework.Graphics.Viewport;

namespace eindopdracht.screens
{
    internal class gameOver
    {
        Game1 game;
        Button MenuButton;
        SpriteFont spriteFont;
        Button ExitGameButton;
        Viewport viewport;
        
        public gameOver(Texture2D texture, SpriteFont font, Viewport viewport, Game1 game)
        {
            this.game = game;
            this.spriteFont = font;
            this.viewport = viewport;


            MenuButton = new Button(texture, font)
            {
                Text = "menu",
            };
            MenuButton.Position = new Vector2((viewport.Width - MenuButton.texture.Width) / 2, 200);
            MenuButton.Click += menu;


            ExitGameButton = new Button(texture, font)
            {
                Text = "Exit Game",
            };
            ExitGameButton.Position = new Vector2((viewport.Width - ExitGameButton.texture.Width) / 2, 300);
            ExitGameButton.Click += exit;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            MenuButton.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(spriteFont, "GameOver"  , new Vector2((viewport.Width - ExitGameButton.texture.Width + 70) / 2, 100), Color.Black);
            ExitGameButton.Draw(gameTime, spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            MenuButton.Update(gameTime);

            ExitGameButton.Update(gameTime);
        }

        public void menu(object sender, EventArgs e)
        {

            Game1.Gamestate = GameState.Menu;
        }


        public void exit(object sender, EventArgs e)
        {

            Game1.Gamestate = GameState.exit;

        }
    }

}
