using eindopdracht.blocks;
using eindopdracht.enemys;
using eindopdracht.levels;
using eindopdracht.screens;
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
        private Texture2D ridderTexture;
        private Texture2D SpikeTexture;

        
        
        public static GameState Gamestate = GameState.Menu;
        
        
        level1 level1;
        level2 level2;
        menu menu;
        gameOver gameover;
        

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
            loadgameover();
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
            grassup = new List<Rectangle>();
            grassdown = new List<Rectangle>();
            grassright = new List<Rectangle>();
            grassleft = new List<Rectangle>();
            level1 = new level1(HeroTexture, BlockTexture, buttonTexture, GreenmanTexture,ridderTexture,SpikeTexture, GraphicsDevice, Content.Load<SpriteFont>("font/newfont"));
            
        }
        public void loadlevel2()
        {
            loadTextures();
            grassup = new List<Rectangle>();
            grassdown = new List<Rectangle>();
            grassright = new List<Rectangle>();
            grassleft = new List<Rectangle>();
            level2 = new level2(HeroTexture, BlockTexture, buttonTexture, GreenmanTexture, ridderTexture,SpikeTexture, GraphicsDevice, Content.Load<SpriteFont>("font/newfont"));
            
        }

        public void loadgameover()
        {

            gameover = new gameOver(Content.Load<Texture2D>("control/wooden"), Content.Load<SpriteFont>("font/newfont"), GraphicsDevice.Viewport, this);
        }

        private void loadTextures()
        {
            HeroTexture = Content.Load<Texture2D>("hero");
            BlockTexture = Content.Load<Texture2D>("Tile-svg3");
            GreenmanTexture = Content.Load<Texture2D>("Walk");
            buttonTexture = Content.Load<Texture2D>("control/menu");
            ridderTexture = Content.Load<Texture2D>("Enemy");
            SpikeTexture = Content.Load<Texture2D>("spike");
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
                    level1.Update(gameTime);
                    break;
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
                    level2.Update(gameTime);
                    break;


                case GameState.gameover:
                    gameover.Update(gameTime);
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
                    level1.Draw(gameTime, _spriteBatch);
                    break;
                case GameState.level2:
                    /*
                    hero.Draw(_spriteBatch);
                    greenman1?.Draw(_spriteBatch);
                    menubutton.Draw(gameTime,_spriteBatch);
                    */
                    level2.Draw(gameTime, _spriteBatch);
                    
                    break;


                case GameState.gameover:
                    gameover.Draw(gameTime, _spriteBatch);
                    break;
                default:
                    
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        

        


    }
}