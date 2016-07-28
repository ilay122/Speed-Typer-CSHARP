using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typespeed
{
    class Consts
    {
        public const int WIDTH = 800;
        public const int HEIGHT = 600;

        public const int PLAYSTATE = 0;
        public const int ENDGAMESTATE = 1;
        public const int ARCADESTATE = 2;
        public const int MENUSTATE = 3;
        public const int CHOOSEMODESTATE = 4;
        public const int PAUSESTATE = 5;

        public const float WORDMOVMENTSPEED = 0.5f;
        public const int WORDLIMIT = 22;
        public const int MAXWORDLENGTH = 15;
        public const int TIME = 1500;
        public const int ARCADETIME = 1000 * 101;

        public const String TITLE = "Speed Typer";
    }
}