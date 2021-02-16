using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Fire : Sprite
{
    public Fire() : base("FireTest.png")
    {
        SetOrigin(width / 2, height / 2);
        SetScaleXY(0.5f, 0.5f);
    }

    void Update()
    {
        x++;
        x--;
        y++;
        y--;
    }
}
