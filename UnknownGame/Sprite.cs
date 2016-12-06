using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UnknownGame
{
    public class Sprite
    {
        public enum Pivot
        {
            TopLeft,
            TopCenter,
            TopRight,
            CenterLeft,
            Center,
            CenterRight,
            BottomLeft,
            BottomCenter,
            BottomRight
        }

        #region Fields

        private static Vector2 WorldOrigin = new Vector2(Game1.WindowWidth / 2, Game1.WindowHeight / 2);

        private Vector2 origin;
        private Pivot pivotPoint;

        #endregion

        #region Constructor

        public Sprite(Texture2D texture, Vector2 position, Pivot pivot)
        {
            Texture = texture;

            DrawRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            PivotPoint = pivot;
        }

        #endregion

        #region Properties

        public Texture2D Texture { get; set; }

        public Pivot PivotPoint
        {
            get
            {
                return pivotPoint;
            }
            set
            {
                switch (value)
                {
                    case Pivot.TopLeft:
                        origin = new Vector2(0, 0);
                        break;
                    case Pivot.TopCenter:
                        origin = new Vector2(Texture.Width / 2, 0);
                        break;
                    case Pivot.TopRight:
                        origin = new Vector2(Texture.Width, 0);
                        break;
                    case Pivot.CenterLeft:
                        origin = new Vector2(0, Texture.Height / 2);
                        break;
                    case Pivot.Center:
                        origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
                        break;
                    case Pivot.CenterRight:
                        origin = new Vector2(Texture.Width, Texture.Height / 2);
                        break;
                    case Pivot.BottomLeft:
                        origin = new Vector2(0, Texture.Height);
                        break;
                    case Pivot.BottomCenter:
                        origin = new Vector2(Texture.Width / 2, Texture.Height);
                        break;
                    case Pivot.BottomRight:
                        origin = new Vector2(Texture.Width, Texture.Height);
                        break;
                    default:
                        break;
                }

                pivotPoint = value;
            }
        }

        private Rectangle drawRectangle;

        public Rectangle DrawRectangle
        {
            get { return drawRectangle; }
            set { drawRectangle = value; }
        }

        #endregion

        #region Methods

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Texture, DrawRectangle, Color.White);
            spriteBatch.Draw(Texture, DrawRectangle, null, Color.White, 0, origin, SpriteEffects.None, 0);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            //spriteBatch.Draw(Texture, DrawRectangle, Color.White);
            spriteBatch.Draw(Texture, position, null, Color.White, 0, origin, Vector2.One, SpriteEffects.None, 0);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float rotation)
        {
            //spriteBatch.Draw(Texture, DrawRectangle, Color.White);
            spriteBatch.Draw(Texture, position, null, Color.White, rotation, origin, Vector2.One, SpriteEffects.None, 0);
        }

        public void Draw(SpriteBatch spriteBatch, float rotation, Color color)
        {
            //spriteBatch.Draw(Texture, DrawRectangle, Color.White);
            spriteBatch.Draw(Texture, DrawRectangle, null, color, rotation, origin, SpriteEffects.None, 0);
        }


        public void Draw(SpriteBatch spriteBatch, Vector2 position, float rotation, Color color = default(Color))
        {
            spriteBatch.Draw(Texture, WorldOrigin + position, null, color, rotation, origin, Vector2.One, SpriteEffects.None, 0);
        }

        public void MoveSprite(int x, int y)
        {
            drawRectangle.X += x;
            drawRectangle.Y += y;
        }

        #endregion
    }
}
