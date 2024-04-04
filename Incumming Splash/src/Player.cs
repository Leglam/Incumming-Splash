using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Incumming_Splash.src
{
    internal class Player:Entity
    {
        public Vector2 velocity;
        public Rectangle playerFallRect;
        public SpriteEffects effect;

        public float playerSpeed = 5;
        public float fallSpeed = 2;
        public float jumpSpeed = -15;
        public float startY;

        public bool isFalling = true;
        public bool isIntersecting = false;
        public bool isJumping;
        public bool isShooting;

        public Animation[] playerAnimation;
        public currentAnimation playerAnimationController;

        KeyboardState keyboardState;

        public Player(Vector2 position,Texture2D runSprite, Texture2D idleSprite, Texture2D jumpSprite, Texture2D fallSprite) 
        {
            this.position = position;
            velocity = position;

            playerAnimation = new Animation[4];
            playerAnimation[0] = new Animation(idleSprite);
            playerAnimation[1] = new Animation(runSprite);
            playerAnimation[2] = new Animation(jumpSprite);
            playerAnimation[3] = new Animation(fallSprite);
            hitbox = new Rectangle((int)position.X, (int)position.Y, 32, 32);
            playerFallRect = new Rectangle((int)position.X + 3, (int)position.Y + 32, 32, (int)fallSpeed);
        }

        public override void Update()
        {
            keyboardState = Keyboard.GetState();

            playerAnimationController = currentAnimation.Idle;

            isShooting = keyboardState.IsKeyDown(Keys.Enter);

            Move(keyboardState);
            startY = position.Y;
            Jump(keyboardState);

            if (isFalling)
            {
                velocity.Y += fallSpeed;
                playerAnimationController = currentAnimation.Fall;
            }

            position = velocity;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
            playerFallRect.X = (int)position.X;
            playerFallRect.Y = (int)(velocity.Y + 34);
        }

        private void Move(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.D))
            {
                velocity.X += playerSpeed;
                playerAnimationController = currentAnimation.Run;
                effect = SpriteEffects.None;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                velocity.X -= playerSpeed;
                playerAnimationController = currentAnimation.Run;
                effect = SpriteEffects.FlipHorizontally;
            }
        }

        private void Jump(KeyboardState keyboardState)
        {
            if (isJumping)
            {
                velocity.Y += jumpSpeed;
                jumpSpeed += 1;
                Move(keyboardState);
                playerAnimationController = currentAnimation.Jump;

                if(velocity.Y >= startY)
                {
                    velocity.Y = startY;
                    isJumping = false;
                }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Space) && !isFalling)
                {
                    isJumping = true;
                    isFalling = false;
                    jumpSpeed = -15;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch (playerAnimationController)
            {
                case currentAnimation.Idle:
                    playerAnimation[0].Draw(spriteBatch, position, gameTime, 150, effect);
                    break;
                case currentAnimation.Run:
                    playerAnimation[1].Draw(spriteBatch, position, gameTime, 150, effect);
                    break;
                case currentAnimation.Jump:
                    playerAnimation[2].Draw(spriteBatch, position, gameTime, 150, effect);
                    break;
                case currentAnimation.Fall:
                    playerAnimation[3].Draw(spriteBatch, position, gameTime, 600, effect);
                    break;
            }
        }
    }
}
