using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class ObjectsToCollideWith : Sprite
{
    public ObjectsToCollideWith(float xPos, float yPos) : base("colors.png")
    {
        width = 32;
        height = 32;
        x = xPos;
        y = yPos;
        this.alpha = 0;
    }
}

