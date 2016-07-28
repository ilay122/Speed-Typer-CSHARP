using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Typespeed
{
    public class WordManager
    {
        private List<Word> words;
        private List<String> WORDS;
        private Random dice;

        private double elapsedTime;
        private float correct;
        private float misses;
        private float passed;
        private Color Green;
        public WordManager()
        {
            correct = 0;
            misses = 0;
            passed = 0;
            this.elapsedTime = 0;
            this.dice=new Random();
            words = new List<Word>();
            WORDS = new List<String>();
            string wordspath = @"Content/data/words.txt";
            using (StreamReader r = new StreamReader(wordspath))
            {
                string line = string.Empty;
                while ((line = r.ReadLine()) != null)
                {
                    WORDS.Add(line);
                }
            }
            Green = new Color(0, 255, 0);
            
        }
        public void update(GameTime gametime)
        {
            elapsedTime += gametime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedTime >= Consts.TIME - correct * 10)
            {
                if (words.Count < Consts.WORDLIMIT)
                {
                    elapsedTime = 0;
                    string wrd = WORDS[dice.Next(WORDS.Count)];
                    Vector2 pos = new Vector2(dice.Next(0, Consts.WIDTH / 4 - Consts.WIDTH / 8), dice.Next((int)(Consts.HEIGHT - Game1.font.MeasureString("a").Y * 2)));
                    words.Add(new Word(wrd));
                    words[words.Count - 1].setPosition(pos);
                    words[words.Count - 1].setColor(Green);
                }
            }
            for (int i = words.Count-1; i >=0; i--)
            {
                words[i].move(new Vector2(Consts.WORDMOVMENTSPEED + correct/50, 0));
                Vector2 pos = words[i].getPosition();
                if (pos.X >= Consts.WIDTH / 2)
                {
                    words[i].setColor(Color.Yellow);
                }
                if (pos.X >= Consts.WIDTH / 2 + Consts.WIDTH / 4)
                {
                    words[i].setColor(Color.Red);
                }
                if (pos.X >= Consts.WIDTH)
                {
                    passed++;
                    words.RemoveAt(i);
                }
                
            }
        }
        public Boolean playWord(String txt)
        {
            for (int i = words.Count - 1; i >= 0; i--)
            {
                if (words[i].getContent().Equals(txt))
                {
                    words.RemoveAt(i);
                    correct++;
                    return true;
                }

            }
            misses++;
            return false;
        }
        public void draw()
        {
            for (int i = words.Count - 1; i >= 0; i--)
            {
                words[i].draw();    

            }
        }
        public Vector3 scoreAndMisses()
        {
            return new Vector3(correct, misses,passed);
        }
        public void restart()
        {
            words.Clear();
            correct = 0;
            misses = 0;
            passed = 0;
            elapsedTime = 0;
        }
    }
}
