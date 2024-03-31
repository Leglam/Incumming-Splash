﻿using Incumming_Splash.src;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Incumming_Splash
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Player player;

        private SpriteFont textFont;
        private Vector2 textPos;
        private const string textMsg = "Welcome Traveller";

        private Texture2D background;
        private Rectangle bgRect;

        private Texture2D playerSprite;

        private KeyboardState keyboardState;
        private const int speed = 5;

        private const int ScreenW = 1600, ScreenH = 900;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = ScreenW;
            graphics.PreferredBackBufferHeight = ScreenH;
            graphics.ApplyChanges();

            Window.AllowUserResizing = false;
            Window.AllowAltF4 = true;
            Window.Title = "Incumming Splash!";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //background = Content.Load<Texture2D>("Texture/Map");
            bgRect = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);

            textFont = Content.Load<SpriteFont>("Fonts/TextFont");
            textPos = new Vector2(0, 10);

            player = new Player(Content.Load<Texture2D>("Sprites/Player_Spritesheet"),
            Content.Load<Texture2D>("Sprites/Player_IdleSpritesheet"));
            
        }

        protected override void Update(GameTime gameTime)
        {
            player.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            player.Draw(spriteBatch, gameTime);
            
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
