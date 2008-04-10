using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using F2D.Graphics;
using F2D.Core;
using F2D;

namespace F2D.Core
{
    public class Cell
    {
        public Vector2 Position;
        public int Size;

        private float scale;
        private ContentManager content;
        private Texture2D texture;

        public List<Renderable> Objects = new List<Renderable>();

        SpriteBatch batch;

        public void Initialize(Vector2 pos, int size)
        {
            this.Position = pos;
            this.Size = size;
            batch = new SpriteBatch(Director.graphicsDevice);
        }

        public void LoadContent(ContentManager contentManager, string filename)
        {
            content = contentManager;
            texture = content.Load<Texture2D>(filename);
            scale = Size / texture.Width;
        }

        public void UnloadContent()
        {
            content.Unload();
        }

        public void Draw()
        {
            Vector2 posBuffer = Position - Camera.Position;
            if (Director.RenderCells)
            {
                Director.SceneBatch.Draw(texture, posBuffer, null, Color.White, 0f, Vector2.Zero, scale,
                           SpriteEffects.None, 1.0f);
            }

            for (int j = 0; j < Objects.Count; j++)
            {
                Objects[j].Draw();
            }
        }
    }
}
