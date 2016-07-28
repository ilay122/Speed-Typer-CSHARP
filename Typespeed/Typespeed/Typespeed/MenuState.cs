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
    class MenuState : GameState
    {

        
        private Word[] arr;
        private Sprite wallpaper;
        private Random dice;
        private Boolean beforect;
        public MenuState(GameStateManager gsm, ContentManager content)
            : base(gsm, content)
        {
            wallpaper = new Sprite(Sprite.CreateRectangleTextureWithOutline(300, 500, 1, Color.White, Color.Black));
            wallpaper.setPosition(new Vector2(Consts.WIDTH / 2 - Consts.WIDTH / 5.3f, Consts.HEIGHT / 4 - Consts.HEIGHT / 6));

            lastmouse = Mouse.GetState();

            beforect = false;
            dice = new Random();
            arr = new Word[2];
            arr[0] = new Word("1.Test Your Speed");
            arr[1] = new Word("2.Exit");
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].setPosition(new Vector2(Consts.WIDTH / 2 - Consts.WIDTH / 5.3f + 1, Consts.HEIGHT / 4 - Consts.HEIGHT / 6 + 400 * i));
            }
        }
        public override void draw()
        {
            wallpaper.draw();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].draw();
            }
        }
        public override void update(GameTime gametime)
        {
            MouseState mouse = Mouse.GetState();
            Boolean somethingcontains = false;
            for (int i = 0; i < arr.Length; i++)
            {
                Rectangle rect = arr[i].getBoundingBox();
                if (rect.Contains(mouse.X, mouse.Y))
                {
                    somethingcontains = true;
                    if (!beforect)
                    {
                        beforect = true;
                        int randnumber = dice.Next(1, 12);
                        gsm.playSound(randnumber.ToString());
                    }
                }

            }
            if (!somethingcontains)
            {
                beforect = false;
            }
            for (int i = 0; i < arr.Length; i++)
            {
                Rectangle rect = arr[i].getBoundingBox();
                if (rect.Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed && lastmouse.LeftButton == ButtonState.Released)
                {
                    if (i == 0)
                    {
                        gsm.setState(Consts.CHOOSEMODESTATE);
                    }
                    if (i == 1)
                    {
                        gsm.Exit();
                    }
                }
            }

            lastmouse = mouse;

        }

    }
}