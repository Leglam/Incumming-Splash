using Incumming_Splash.src;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TiledSharp;

namespace Incumming_Splash
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        #region Tilemaps
        private TmxMap map;
        private TileMapManager tileMapManager;
        private Texture2D tileset;
        #endregion

        #region Player
        private Player player;
        #endregion

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

            #region Tilemap
            map = new TmxMap("Content/Maps/map1.tmx");
            tileset = Content.Load<Texture2D>("Tilesets/" + map.Tilesets[1].Name.ToString());
            int tileWidth = map.Tilesets[0].TileWidth;
            int tileHeight = map.Tilesets[0].TileHeight;
            int TileSetTilesWidth = tileset.Width / tileWidth;
            tileMapManager = new TileMapManager(map, tileset, TileSetTilesWidth, tileWidth, tileHeight);
            #endregion

            #region Player
            player = new Player(
                Content.Load<Texture2D>("Sprites/Player_Spritesheet"),
                Content.Load<Texture2D>("Sprites/Player_IdleSpritesheet")
            );
            #endregion

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

            tileMapManager.Draw(spriteBatch);
            player.Draw(spriteBatch, gameTime);
            
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
