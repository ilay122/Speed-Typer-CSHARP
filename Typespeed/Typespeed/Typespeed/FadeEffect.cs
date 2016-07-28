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
    public class FadeEffect : EffectInstance
    {
        private Sprite spr;
        private Word wrd;

        private double time2bdrawn;
        private double time2fade;
        private double currenttime;
        private Boolean finishedFirst;
        private Color clr;
        public Boolean isdrawn;

        private Boolean issprite;
        public FadeEffect(Boolean isdrawn,double time2bedrawn,double time2fade,Texture2D tex,Vector2 pos)
            : base(isdrawn)
        {
            this.time2bdrawn = time2bedrawn;
            this.time2fade = time2fade;
            this.currenttime = 0;
            this.finishedFirst = false;
            this.clr = Color.White;

            spr = new Sprite(tex);
            spr.setPosition(pos);
            issprite = true;
        }
        public FadeEffect(Boolean isdrawn, double time2bedrawn, double time2fade, Word wrd)
            : base(isdrawn)
        {
            this.time2bdrawn = time2bedrawn;
            this.time2fade = time2fade;
            this.currenttime = 0;
            this.clr = Color.White;
            this.wrd = wrd;

            this.finishedFirst = false;
            issprite = false;
        }
        public override void draw()
        {
            if (!finishedFirst)
            {
                if (issprite)
                {
                    spr.draw();
                }
                else
                {
                    wrd.draw();
                }
            }
            else
            {
                if (issprite)
                {
                    clr *= 0.95f;
                    spr.draw(clr);
                }
                else
                {
                    Color clrr = wrd.getColor();
                    clrr *= 0.95f;
                    wrd.setColor(clrr);
                    wrd.draw();
                }
            }
        }
        public override Boolean update(GameTime gametime)
        {
            currenttime += gametime.ElapsedGameTime.TotalMilliseconds;
            
            if (currenttime > time2bdrawn)
            {
                finishedFirst = true;
                currenttime=0;
            }
            if (finishedFirst)
            {
                return currenttime > time2fade;
            }

            return false;
        }
    }
}
