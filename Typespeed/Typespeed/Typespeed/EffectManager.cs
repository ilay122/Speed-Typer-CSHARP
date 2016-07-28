using System;
using System.Collections.Generic;
using System.Collections;
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
    public class EffectManager
    {
        private List<EffectInstance> todraw;
        private List<EffectInstance> nottodraw;
        public EffectManager()
        {
            //create effect abstract class and fade thing
            todraw = new List<EffectInstance>();
            nottodraw = new List<EffectInstance>();
        }
        public void addEffect(EffectInstance efct)
        {
            if (efct.isdrawn)
            {
                todraw.Add(efct);
            }
            else
            {
                nottodraw.Add(efct);
            }
        }
        public void update(GameTime gametime)
        {
            for (int i = todraw.Count - 1; i >= 0; i--)
            {
                Boolean died = todraw[i].update(gametime);
                if (died)
                {
                    todraw.RemoveAt(i);
                }
            }

            for (int i = nottodraw.Count - 1; i >= 0; i--)
            {
                Boolean died = nottodraw[i].update(gametime);
                if (died)
                {
                    nottodraw.RemoveAt(i);
                }
            }
        }
        public void draw()
        {
            for (int i = 0; i < todraw.Count; i++)
            {
                todraw[i].draw();
            }
        }
    }
}
