using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Typespeed
{
    class Sprite
    {
        private SpriteBatch batch;
        private GraphicsDeviceManager graphics;
        private Texture2D tex;
        private Rectangle collision;
        public Sprite(Texture2D tex)
        {
            this.batch = Game1.spriteBatch;
            this.graphics = Game1.graphics;
            this.tex = tex;
            collision = new Rectangle((int)(0), (int)(0), tex.Width, tex.Height);
        }

        public void draw()
        {
            batch.Draw(tex, collision, Color.White);

        }
        public void draw(Color col)
        {
            batch.Draw(tex, collision, col);
        }
        public void setX(int x)
        {
            collision.X = x;
        }
        public void moveX(int x)
        {
            collision.X += x;
        }
        public void moveY(int y)
        {
            collision.Y += y;
        }
        public void move(Vector2 mov)
        {
            moveX((int)mov.X);
            moveY((int)mov.Y);
        }
        public void setTexture(Texture2D tex)
        {
            this.tex = tex;
            collision.Width = tex.Width;
            collision.Height = tex.Height;
        }
        public void setY(int y)
        {
            collision.Y = y;
        }
        public void setWidth(int width)
        {
            collision.Width = width;
        }
        public void setHeight(int height)
        {
            collision.Height = height;
        }
        public Rectangle getCollisionRectangle()
        {
            return collision;
        }
        public Vector2 getPosition()
        {
            return new Vector2(collision.X, collision.Y);
        }
        public Vector2 getCenterPos()
        {
            return new Vector2(collision.X + collision.Width / 2, collision.Y + collision.Height / 2);
        }
        public int getHeight()
        {
            return collision.Height;
        }
        public int getWidth()
        {
            return collision.Width;
        }
        public void setPosition(Vector2 pos)
        {
            collision.X = (int)pos.X;
            collision.Y = (int)pos.Y;
        }
        public static Texture2D CreateRectangleTexture(int width, int height, Color colori)
        {
            Texture2D rectangleTexture = new Texture2D(Game1.graphics.GraphicsDevice, width, height);
            Color[] color = new Color[width * height];
            for (int i = 0; i < color.Length; i++)
            {
                color[i] = colori;
            }
            rectangleTexture.SetData(color);
            return rectangleTexture;
        }
        public static Texture2D CreateRectangleTextureWithOutline(int width, int height,int borderwidth, Color reccolor, Color outcolor)
        {
            Texture2D texture = new Texture2D(Game1.graphics.GraphicsDevice, width, height);
            Color[] colors = new Color[texture.Width * texture.Height];

            for (int x = 0; x < texture.Width; x++)
            {
                for (int y = 0; y < texture.Height; y++)
                {
                    bool colored = false;
                    for (int i = 0; i <= borderwidth; i++)
                    {
                        if (x == i || y == i || x == texture.Width - 1 - i || y == texture.Height - 1 - i)
                        {
                            colors[x + y * texture.Width] = outcolor;
                            colored = true;
                            break;
                        }
                    }

                    if (colored == false)
                        colors[x + y * texture.Width] = reccolor;
                }
            }

            texture.SetData(colors);
            return texture;
        }
        public static void DrawLine(Vector2 start, Vector2 end, Color color,int width=2)
        {
            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);
            
            Game1.spriteBatch.Draw(CreateRectangleTexture(width,width,color),
                new Rectangle((int)start.X, (int)start.Y, (int)edge.Length(), width),
                null,
                color,
                angle,
                new Vector2(0, 0),
                SpriteEffects.None,
                0);
        }
    }
}