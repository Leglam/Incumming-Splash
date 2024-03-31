using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Incumming_Splash.src
{
    internal class Player:Entity
    {
        public Vector2 velocity;
        public float playerSpeed = 5;
        public Animation[] playerAnimation;
        public currentAnimation playerAnimationController;

        KeyboardState keyboardState;

        public Player(Texture2D runSprite, Texture2D idleSprite) 
        {
            spriteSheet = runSprite;
            velocity = new Vector2();
            playerAnimation = new Animation[2];
            playerAnimation[0] = new Animation(idleSprite);
            playerAnimation[1] = new Animation(runSprite);
        }

        public override void Update()
        {
            keyboardState = Keyboard.GetState();

            playerAnimationController = currentAnimation.Idle;

            if (keyboardState.IsKeyDown(Keys.D))
            {
                velocity.X += playerSpeed;
                playerAnimationController = currentAnimation.Run;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                velocity.X -= playerSpeed;
                playerAnimationController = currentAnimation.Run;
            }

            position = velocity;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch (playerAnimationController)
            {
                case currentAnimation.Idle:
                    playerAnimation[0].Draw(spriteBatch, position, gameTime, 150);
                    break;
                case currentAnimation.Run:
                    playerAnimation[1].Draw(spriteBatch, position, gameTime, 150);
                    break;
            }
            
            //spriteBatch.Draw(spriteSheet, velocity, Color.White);
        }
    }
}
