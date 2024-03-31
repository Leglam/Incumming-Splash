using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Incumming_Splash.src
{
    internal class TileMapManager
    {
        TmxMap map;
        Texture2D tileset;
        int tilesetTilesWide;
        int tileWidth;
        int tileHeight;
        
        public TileMapManager(TmxMap map, Texture2D tileset, int tilesetTilesWide, int tileWidth, int tileHeight) 
        {
            this.map = map;
            this.tileset = tileset;
            this.tilesetTilesWide = tilesetTilesWide;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < map.TileLayers.Count; i++)
            {
                for (var j = 0; j < map.TileLayers[i].Tiles.Count; j++)
                {
                    int gid = map.TileLayers[i].Tiles[j].Gid;
                    if (gid == 0)
                    {
                       // do nothing 
                    }
                    else
                    {
                        int tileFrame = gid - 1;
                        int column = tileFrame % tilesetTilesWide;
                        int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);
                        float x = (j % map.Width) * map.TileWidth;
                        float y = (float)Math.Floor(j / (double)map.Width) * map.TileHeight;
                        Rectangle tilesetRec = new Rectangle((tileWidth) * column, (tileHeight) * row, tileWidth, tileHeight);
                        spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                    }
                }
            }
        }

    }
}
