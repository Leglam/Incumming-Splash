using Incumming_Splash.src;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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

        #region Camera
        private Camera camera;
        private Matrix transformMatrix;
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
        private List<Bullet> bullets;
        private Texture2D bulletTexture;
        private int time_per_bullets;
        private int points = 0;
        #endregion

        private SpriteFont textFont;
        private Vector2 textPos;
        private const string textMsg = "Welcome Traveller";

        private Texture2D background;
        private Rectangle bgRect;

        private Texture2D playerSprite;

        private KeyboardState keyboardState;
        private const int speed = 5;

        public const int ScreenW = 1024, ScreenH = 850;

        #region Managers
        private GameManager gameManager;
        #endregion

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

            gameManager = new GameManager(endRect);

            #region Camera
            camera = new Camera();
            #endregion

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

            #region Bullet
            bullets = new List<Bullet>();
            bulletTexture = Content.Load<Texture2D>("Sprites/1 - Agent_Mike_Bullet (16 x 16)");
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

            #region Camera
            Rectangle target = new Rectangle((int)player.position.X, (int)player.position.Y, 32, 32);
            transformMatrix = camera.Follow(target);
            #endregion

            // for change map
            #region Managers
            if (gameManager.hasGameEnded(player.hitbox))
            {
                Console.WriteLine("game");
            }
            #endregion

            #region Player
            var initPos = player.position;
            player.Update();

            #region Bullet
            if (player.isShooting)
            {
                if (time_per_bullets > 5 && bullets.ToArray().Length < 20)
                {
                    var temp_hitbox = new Rectangle((int)player.position.X+7, (int)player.position.Y+13, (int)bulletTexture.Width, (int)bulletTexture.Height);

                    if (player.effect == SpriteEffects.None)
                    {
                        bullets.Add(new Bullet(bulletTexture, 4, temp_hitbox));
                    }
                    if (player.effect == SpriteEffects.FlipHorizontally)
                    {
                        bullets.Add(new Bullet(bulletTexture, -4, temp_hitbox));
                    }

                    time_per_bullets = 0;
                }
                else
                {
                    time_per_bullets++;
                }
            }

            foreach(var bullet in bullets.ToArray())
            {
                bullet.Update();

                foreach (var rect in collisionRects)
                {
                    if (rect.Intersects(bullet.hitbox))
                    {
                        bullets.Remove(bullet);
                        break;
                    }
                }
                foreach(var enemy in enemies.ToArray())
                {
                    if (bullet.hitbox.Intersects(enemy.hitbox))
                    {
                        bullets.Remove(bullet);
                        enemies.Remove(enemy);
                        points++;
                        break;
                    }
                }
            }
            #endregion

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

            #endregion
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(transformMatrix: transformMatrix);

            tileMapManager.Draw(spriteBatch);

            #region Enemies
            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch, gameTime);
            }
            #endregion

            #region Player
         
            #region Bullets
            foreach (var bullet in bullets.ToArray())
            {
                bullet.Draw(spriteBatch);
            }
            #endregion
            
            player.Draw(spriteBatch, gameTime);

            #endregion
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
