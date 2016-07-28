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
    class ChooseModeState : GameState
    {

        private MouseState prev;
        private Word[] arr;
        private Sprite wallpaper;

        Word survivaltext;
        Boolean drawsurvival;

        Word arcadetext;
        Boolean drawarcade;

        private Random dice;
        private Boolean beforect;
        public ChooseModeState(GameStateManager gsm, ContentManager content)
            : base(gsm, content)
        {
            //wallpaper = new Sprite(Sprite.CreateRectangleTexture(300, 500, Color.White));
            wallpaper = new Sprite(Sprite.CreateRectangleTextureWithOutline(300, 500, 1, Color.White, Color.Black));
            wallpaper.setPosition(new Vector2(Consts.WIDTH / 2 - Consts.WIDTH / 5.3f, Consts.HEIGHT / 4 - Consts.HEIGHT / 6));

            prev = Mouse.GetState();

            arcadetext = new Word("In 100 seconds get as much points as you can !");
            survivaltext = new Word("Survive as long as you can !");

            dice = new Random();

            drawsurvival = false;
            drawarcade = false;
            beforect = false;

            arr = new Word[3];
            arr[0] = new Word("1.Survival Mode");
            arr[1] = new Word("2.Arcade Mode");
            arr[2] = new Word("3.Return To Menu");
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].setPosition(new Vector2(Consts.WIDTH / 2 - Consts.WIDTH / 5.3f + 1, Consts.HEIGHT / 4 - Consts.HEIGHT / 8 + 100 * i));
            }
        }
        public override void draw()
        {
            wallpaper.draw();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].draw();
            }
            if (drawsurvival)
            {
                survivaltext.draw();
            }
            if (drawarcade && !drawsurvival)
            {
                arcadetext.draw();
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
                    if (i == 0)
                    {
                        drawsurvival = true;
                    }
                    if (i == 1)
                    {
                        drawarcade = true;
                    }
                    somethingcontains = true;
                    if (!beforect)
                    {
                        beforect = true;
                        int randnumber = dice.Next(1, 12);
                        gsm.playSound(randnumber.ToString());
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        drawsurvival = false;
                    }
                    if (i == 1)
                    {
                        drawarcade = false;
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
                if (rect.Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed && prev.LeftButton == ButtonState.Released)
                {
                    if (i == 0)
                    {
                        gsm.restartPlayState();
                        gsm.setState(Consts.PLAYSTATE);
                    }
                    if (i == 1)
                    {
                        gsm.restartArcadeState();
                        gsm.setState(Consts.ARCADESTATE);
                    }
                    if (i == 2)
                    {
                        gsm.setState(Consts.MENUSTATE);
                    }
                }
            }

            prev = mouse;

        }

    }
}