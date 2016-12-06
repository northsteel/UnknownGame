using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace UnknownGame
{
    class Animation
    {
        #region Fields

        private Texture2D animationTexture;

        private Point sheetSize;

        private TimeSpan frameInterval;

        private TimeSpan nextFrame;

        public Point currentFrame;

        public Point frameSize;

        #endregion

        #region Constructors

        public Animation(Texture2D frameSheet, Point frameSize, Point sheetSize, TimeSpan interval)
        {
            animationTexture = frameSheet;
            this.frameSize = frameSize;
            this.sheetSize = sheetSize;
            frameInterval = interval;
        }

        #endregion

        #region Methods

        public bool Update(GameTime gameTime)
        {
            bool progressed;

            if (nextFrame >= frameInterval)
            {
                currentFrame.X++;

                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    currentFrame.Y++;
                }

                if (currentFrame.Y >= sheetSize.Y)
                {
                    currentFrame.Y = 0;
                }

                progressed = true;
                nextFrame = TimeSpan.Zero;
            }
            else
            {
                nextFrame += gameTime.ElapsedGameTime;
                progressed = false;
            }

            return progressed;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffect)
        {
            Draw(spriteBatch, position, 1.0f, spriteEffect);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale, SpriteEffects spriteEffect)
        {
            spriteBatch.Draw(animationTexture, position, new Rectangle(frameSize.X * currentFrame.X, frameSize.Y * currentFrame.Y, frameSize.X, frameSize.Y), Color.White, 0f, Vector2.Zero, scale, spriteEffect, 0);
        }

        #endregion
    }
}