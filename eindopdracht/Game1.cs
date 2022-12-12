using eindopdracht.blocks;
using eindopdracht.enemys;
using eindopdracht.levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace eindopdracht
{

    public enum GameState
    {
    Menu,
    level1,
    level2,
    gameover,
    exit   
    }

    internal class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        public static List<Rectangle> grassup = new List<Rectangle>();
        public static List<Rectangle> grassdown = new List<Rectangle>();
        public static List<Rectangle> grassright = new List<Rectangle>();
        public static List<Rectangle> grassleft = new List<Rectangle>();
        public static List<Block> blocks = new List<Block>();

        private Texture2D HeroTexture;
        private Texture2D buttonTexture;
        private Texture2D BlockTexture;
        private Texture2D BG;
        private Texture2D GreenmanTexture;

        
        public static int[,] curentlevel;
        public static GameState Gamestate = GameState.Menu;
        
        
        level level;
        menu menu;
        
        
        
   

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()    
        {
            // TODO: Add your initialization logic here

            loadBG();
            loadMenu();
            
            base.Initialize();

        }

        private void loadBG()
        {
            BG = Content.Load<Texture2D>("BG");
        }

        private void loadMenu()
        {
            menu = new menu(Content.Load<Texture2D>("control/wooden"), Content.Load<SpriteFont>("font/newfont"), GraphicsDevice.Viewport,this);
        }

        public void loadlevel1()
        {
            loadTextures();
            level = new level1(HeroTexture, BlockTexture, buttonTexture, GreenmanTexture, GraphicsDevice);
        }
        public void loadlevel2()
        {
            loadTextures();
            level = new level2(HeroTexture, BlockTexture, buttonTexture, GreenmanTexture, GraphicsDevice);
        }

        private void loadTextures()
        {
            HeroTexture = Content.Load<Texture2D>("hero");
            BlockTexture = Content.Load<Texture2D>("Tile-svg3");
            GreenmanTexture = Content.Load<Texture2D>("Walk");
            buttonTexture = Content.Load<Texture2D>("control/menu");
        }



        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here 
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            switch (Gamestate)
            {
                case GameState.Menu:
                    menu.Update(gameTime);

                    break;


                case GameState.level1:
                    
                case GameState.level2:
                    /*
                    if (greenman1?.health < 0)
                    {
                        greenman1 = null;
                    }
                    hero.Update(gameTime);
                    greenman1?.Update(gameTime);
                    menubutton?.Update(gameTime);
                    */
                    level.Update(gameTime);
                    break;


                case GameState.gameover:
                    break;
                case GameState.exit:
                    Exit();
                    break;
                default:
                    break;
            }
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {

            _spriteBatch.Begin();
            _spriteBatch.Draw(BG, new Rectangle(0, -100, 1000, 750), Color.White);
            switch (Gamestate)
            {
                case GameState.Menu:
                    menu.Draw( gameTime, _spriteBatch);
                    break;


                case GameState.level1:
                case GameState.level2:
                    /*
                    hero.Draw(_spriteBatch);
                    greenman1?.Draw(_spriteBatch);
                    menubutton.Draw(gameTime,_spriteBatch);
                    */
                    level.Draw(gameTime, _spriteBatch);
                    
                    break;


                case GameState.gameover:
                    break;
                default:
                    
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        

        


    }
}