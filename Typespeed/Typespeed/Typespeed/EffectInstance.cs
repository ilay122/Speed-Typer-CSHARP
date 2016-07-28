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
    public abstract class EffectInstance
    {
        public Boolean isdrawn;
        public EffectInstance(Boolean isdrawn)
        {
            this.isdrawn = isdrawn;
        }
        public abstract Boolean update(GameTime gametime);

        public abstract void draw();
    }
}
