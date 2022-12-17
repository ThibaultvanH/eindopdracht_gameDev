using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindopdracht.screens
{
    internal class gameOver
    {
        Game1 game;
        Button MenuButton;

        Button ExitGameButton;
        public gameOver(Texture2D texture, SpriteFont font, Viewport viewport, Game1 game)
        {
            this.game = game;
            MenuButton = new Button(texture, font)
            {
                Text = "menu",
            };
            MenuButton.Position = new Vector2((viewport.Width - MenuButton.texture.Width) / 2, 100);
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
