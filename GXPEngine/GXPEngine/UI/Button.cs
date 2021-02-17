using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Button : AnimationSprite
{
    public Button() : base("Play.png", 1, 2)
    {
        SetOrigin(this.width / 2, this.height / 2);
    }

    void Update()
    {
        if (this.HitTestPoint(Input.mouseX, Input.mouseY))
        {
            SetFrame(1);
        }
        else { SetFrame(0); }
    }

}
