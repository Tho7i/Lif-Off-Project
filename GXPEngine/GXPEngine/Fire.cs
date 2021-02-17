using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Fire : Sprite
{
    private float _duration = 4000;


    public Fire() : base("FireTest.png")
    {
        SetOrigin(width / 2, height / 2);
        SetScaleXY(0.5f, 0.5f);
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
