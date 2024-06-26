﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Incumming_Splash.src
{
    internal class Enemy:Entity
    {
        private Animation enemyAnim;
        private float speed = 2;
        private Rectangle pathway;
        private bool isFacingRight = true;
        
        public Enemy(Texture2D enemySpriteSheet, Rectangle pathway, float speed = 2)
        {
            enemyAnim = new Animation(enemySpriteSheet);
            this.pathway = pathway;
            position = new Vector2(pathway.X, pathway.Y);
            hitbox = new Rectangle(pathway.X, pathway.Y, 32, 32);
            this.speed = speed;
        }

        public override void Update()
        {
            if (!pathway.Contains(hitbox))
            {
                speed = -speed;
                isFacingRight = !isFacingRight;
            }
            position.X += speed;

            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
        }

        public bool hasHit(Rectangle playerRect)
        {
            return hitbox.Intersects(playerRect);
        } 

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (isFacingRight)
            {
                enemyAnim.Draw(spriteBatch, position, gameTime, 100);
            } 
            else
            {
                enemyAnim.Draw(spriteBatch, position, gameTime, 150, SpriteEffects.FlipHorizontally);
            }
        }
    }
}
