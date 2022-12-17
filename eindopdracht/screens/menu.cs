using eindopdracht.levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace eindopdracht.screens
{
    internal class menu
    {
        Game1 game;
        Button level1Button;
        Button level2Button;
        Button ExitGameButton;
        public menu(Texture2D texture, SpriteFont font, Viewport viewport, Game1 game)
        {
            this.game = game;
            level1Button = new Button(texture, font)
            {
                Text = "Start level 1",
            };
            level1Button.Position = new Vector2((viewport.Width - level1Button.texture.Width) / 2, 100);
            level1Button.Click += level1;

            level2Button = new Button(texture, font)
            {
                Text = "Start level 2",
            };
            level2Button.Position = new Vector2((viewport.Width - level2Button.texture.Width) / 2, 200);
            level2Button.Click += level2;

            ExitGameButton = new Button(texture, font)
            {
                Text = "Exit Game",
            };
            ExitGameButton.Position = new Vector2((viewport.Width - ExitGameButton.texture.Width) / 2, 300);
            ExitGameButton.Click += exit;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            level1Button.Draw(gameTime, spriteBatch);
            level2Button.Draw(gameTime, spriteBatch);
            ExitGameButton.Draw(gameTime, spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            level1Button.Update(gameTime);
            level2Button.Update(gameTime);
            ExitGameButton.Update(gameTime);
        }

        public void level1(object sender, EventArgs e)
        {

            Game1.Gamestate = GameState.level1;
            game.loadlevel1();
        }
        public void level2(object sender, EventArgs e)
        {

            Game1.Gamestate = GameState.level2;
            game.loadlevel2();

        }
        public void exit(object sender, EventArgs e)
        {

            Game1.Gamestate = GameState.exit;

        }
    }
}
