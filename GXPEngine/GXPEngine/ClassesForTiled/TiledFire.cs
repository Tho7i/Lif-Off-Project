using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class TiledFire : Sprite
{
    public TiledFire(float xPos, float yPos) : base("checkers.png")
    {
        width = 32;
        height = 32;
        x = xPos;
        y = yPos;
        this.alpha = 0;
    }
}
