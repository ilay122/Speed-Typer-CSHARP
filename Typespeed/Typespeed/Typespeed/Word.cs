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
    public class Word
    {
        private Vector2 pos;
        private Rectangle boundingBox;
        private String content;
        private Color color;

        public Word()
        {
            this.content = "";
            this.pos = Vector2.Zero;
            this.boundingBox = new Rectangle();
            boundingBox.X = (int)pos.X;
            boundingBox.Y = (int)pos.Y;
            color = Color.Black;
        }
        public Word(String txt)
        {
            this.content = txt;
            this.pos = Vector2.Zero;
            this.boundingBox = new Rectangle();
            boundingBox.X = (int)pos.X;
            boundingBox.Y = (int)pos.Y;
            boundingBox.Width = (int)Game1.font.MeasureString(content).X;
            boundingBox.Height = (int)Game1.font.MeasureString(content).Y;
            color = Color.Black;
        }
        public void setContent(String content)
        {
            this.content = content;
            boundingBox.Width = (int)Game1.font.MeasureString(content).X;
            boundingBox.Height = (int)Game1.font.MeasureString(content).Y;
        }
        public void setPosition(Vector2 pos)
        {
            this.pos = pos;
            boundingBox.X = (int)pos.X;
            boundingBox.Y = (int)pos.Y;
        }
        public Rectangle getBoundingBox()
        {
            return boundingBox;
        }
        public String getContent()
        {
            return content;
        }
        public Vector2 getPosition()
        {
            return pos;
        }
        public Color getColor()
        {
            return color;
        }
        public void setColor(Color clr)
        {
            this.color = clr;
        }
        public void addToContent(String ad)
        {
            setContent(getContent() + ad);
        }
        public void move(Vector2 mv)
        {
            this.pos += mv;
            setPosition(pos);
        }
        public int getLength()
        {
            return content.Length;
        }
        public void draw()
        {
            Game1.spriteBatch.DrawString(Game1.font, content, pos, color);
        }
    }
}
