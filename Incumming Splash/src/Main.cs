using Incumming_Splash.src;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TiledSharp;

namespace Incumming_Splash
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        #region Enemy
        private Enemy dinosaur;
        private List<Enemy> enemies;
        private List<Rectangle> enemyPathway;
        #endregion

        #region Tilemaps
        private TmxMap map;
        private TileMapManager tileMapManager;
        private Texture2D tileset;
        private List<Rectangle> collisionRects;
        private Rectangle startRect;
        private Rectangle endRect;
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
            map = new TmxMap("Content/Maps/TestMap.tmx");
            tileset = Content.Load<Texture2D>("Tilesets/" + map.Tilesets[0].Name.ToString());
            int tileWidth = map.Tilesets[0].TileWidth;
            int tileHeight = map.Tilesets[0].TileHeight;
            int TileSetTilesWidth = tileset.Width / tileWidth;
            tileMapManager = new TileMapManager(map, tileset, TileSetTilesWidth, tileWidth, tileHeight);
            #endregion

            collisionRects = new List<Rectangle>();
            
            foreach (var o in map.ObjectGroups["Collision"].Objects)
            {
                if (o.Name == "")
                {
                    collisionRects.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
                }

                if (o.Name == "Start")
                {
                    startRect = new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height);
                }
                if (o.Name == "End")
                {
                    endRect = new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height);
                }
            }

            enemyPathway = new List<Rectangle>();
            
            foreach (var o in map.ObjectGroups["EnemyPathways"].Objects)
            {
                enemyPathway.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
            }

            enemies = new List<Enemy>();
            dinosaur = new Enemy(
                Content.Load<Texture2D>("Sprites/Player"),
                enemyPathway[0]
                );
            enemies.Add( dinosaur );

            #region Player
            player = new Player(
                new Vector2(startRect.X, startRect.Y),
                Content.Load<Texture2D>("Sprites/asuji1"),
                Content.Load<Texture2D>("Sprites/asuji2"),
                Content.Load<Texture2D>("Sprites/asuji1"),
                Content.Load<Texture2D>("Sprites/asuji2")
            ) ;
            #endregion

        }

        protected override void Update(GameTime gameTime)
        {
            #region Enemies
            foreach(var enemy in enemies)
            {
                enemy.Update();
            }
            #endregion

            var initPos = player.position;
            player.Update();

            #region Player Collision;
            // y axis
            foreach (var rect in collisionRects)
            {
                if (!player.isJumping)
                    player.isFalling = true;

                if (rect.Intersects(player.playerFallRect))
                {
                    player.isFalling = false;
                    break;
                }
            }

            // x axis
            foreach (var rect in collisionRects)
            {
                if (rect.Intersects(player.hitbox))
                {
                    player.position.X = initPos.X;
                    player.velocity.X = initPos.X;
                    player.position.Y = initPos.Y;
                    player.velocity.Y = initPos.Y;
                    break;
                }
            }
            #endregion

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            tileMapManager.Draw(spriteBatch);

            #region Enemies
            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch, gameTime);
            }
            #endregion

            player.Draw(spriteBatch, gameTime);
            
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
