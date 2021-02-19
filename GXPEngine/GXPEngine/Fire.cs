using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Fire : AnimationSprite
{
    private float _duration = 4000;


    public Fire() : base("FireSS.png", 2, 1)
    {
        SetOrigin(width / 2, height / 2);
        SetScaleXY(0.5f, 0.5f);
        SetFrame(0);
    }

    void Update()
    {
        _duration -= Time.deltaTime;
        if(_duration <= 0)
        {
            this.LateDestroy();
        }
    }
}
