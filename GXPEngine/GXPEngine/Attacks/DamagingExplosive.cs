using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class DamagingExplosive : Sprite
{
    public DamagingExplosive() : base("circle.png")
    {
        SetOrigin(this.width / 2, this.height / 2);
        SetScaleXY(2.0f, 2.0f);
    }
}
