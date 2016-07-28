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
    class PlayState : GameState
    {


        private Word txt;
        public static WordManager wm;
        

        private Word scoree;
        private Vector2 src;
        private Vector2 dst;

        private Sprite recc;

        private Vector3 scoremissespassed;

        private Random dice;
        public PlayState(GameStateManager gsm, ContentManager content)
            : base(gsm, content)
        {
            //HUD SHIT
            scoree = new Word("");
            scoree.setPosition(new Vector2(Consts.WIDTH / 2 - Game1.font.MeasureString("a").X * 9, Consts.HEIGHT - Game1.font.MeasureString("a").Y));
            
            src = new Vector2(0, Consts.HEIGHT - Game1.font.MeasureString("a").Y);
            dst = new Vector2(Consts.WIDTH, Consts.HEIGHT - Game1.font.MeasureString("a").Y);

            recc = new Sprite(Sprite.CreateRectangleTexture(10, (int)Game1.font.MeasureString("a").Y, Color.White));

            dice = new Random();

            scoremissespassed = Vector3.Zero;
            //END HUD SHIT
            txt = new Word();
            txt.setColor(Color.White);
            txt.setPosition(new Vector2(0, Consts.HEIGHT - Game1.font.MeasureString("a").Y));

            for (int i = 1; i < 12; i++)
            {
                gsm.addSound("data/sound/" + i, i.ToString());
            }
            gsm.addSound("data/sound/kenter", "enter");

                wm = new WordManager();
            lastkeyb = Keyboard.GetState();
        }
        public override void draw()
        {
            scoree.draw();
            recc.draw();

            wm.draw();
            txt.draw();
            Sprite.DrawLine(src, dst, Color.Black);
            
            
        }
        public override void update(GameTime gametime)
        {
            wm.update(gametime);

            KeyboardState keys = Keyboard.GetState();
            Keys[] currentkeys = keys.GetPressedKeys();
            Keys[] last = lastkeyb.GetPressedKeys();

            if (txt.getContent().Length <= Consts.MAXWORDLENGTH)
            {
                foreach (Keys key in currentkeys)
                {
                    if (!last.Contains(key) && key >= Keys.A && key <= Keys.Z)
                    {
                        char chr = Char.ToLower(key.ToString()[0]);
                        txt.addToContent(chr.ToString());
                        int randnumber = dice.Next(1, 12);
                        gsm.playSound(randnumber.ToString());
                    }

                }
            }
            if (!last.Contains(Keys.Back) && currentkeys.Contains(Keys.Back)){
                if(txt.getLength() != 0){
                    txt.setContent(txt.getContent().Substring(0,txt.getLength()-1));
                    int randnumber = dice.Next(1, 12);
                    gsm.playSound(randnumber.ToString());
                }
            }
            if (keys.IsKeyDown(Keys.Enter) && lastkeyb.IsKeyUp(Keys.Enter) || keys.IsKeyDown(Keys.Space) && lastkeyb.IsKeyUp(Keys.Space))
            {
                if (!txt.getContent().Equals(""))
                {
                    Boolean correct = wm.playWord(txt.getContent());
                    txt.setContent("");
                    gsm.playSound("enter");
                }
            }
            if (keys.IsKeyDown(Keys.Escape) && lastkeyb.IsKeyUp(Keys.Escape))
            {
                gsm.setState(Consts.PAUSESTATE);
            }
            scoremissespassed = wm.scoreAndMisses();
            scoree.setContent(String.Format("Score:{0} Misses:{1} Passed:{2} Life Left:{3}", scoremissespassed.X, scoremissespassed.Y, scoremissespassed.Z,(10-scoremissespassed.Z)));
            Rectangle textbox=txt.getBoundingBox();
            recc.setPosition(new Vector2(textbox.X + textbox.Width, textbox.Y));

            if (scoremissespassed.Z >= 10)
            {
                gsm.setState(Consts.ENDGAMESTATE);
            }

            lastkeyb = keys;
        }
        public void restart()
        {
            txt.setContent("");
            scoremissespassed = Vector3.Zero;
            wm.restart();
        }
        
    }
}