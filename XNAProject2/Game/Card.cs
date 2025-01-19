using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lórum.Screens.CardManager
{
    public class Card
    {
        private readonly SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
        public bool Active;
        public int Height;
        public Texture2D image;
        public int PosX, PosY;
        public int Value;
        public int Width;

        public void Initialize(int value, int posx, int posy, int width, int height, bool active)
        {
            Value = value;
            PosX = posx;
            PosY = posy;
            Width = width;
            Height = height;
            Active = active;
            image = GameTable.KártyaSzám(Value);
        }

        public void Draw()
        {
            if (Active)
                spriteBatch.Draw(image,
                    new Rectangle((int)(PosX * LórumGame.scale), (int)(PosY * LórumGame.scale2),
                        (int)(Width * LórumGame.scale2), (int)(Height * LórumGame.scale2)), null,
                    Color.White, 0,
                    new Vector2(0, 0), SpriteEffects.None, 0.0f);
        }
    }
}