using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class DamagingExplosive : Sprite
{
    private Sound _explosion;
    public DamagingExplosive() : base("Explosion.png")
    {
        _explosion = new Sound("explosion.mp3");
        _explosion.Play();
        SetOrigin(this.width / 2, this.height / 2);
        SetScaleXY(4.0f, 4.0f);
    }
}
