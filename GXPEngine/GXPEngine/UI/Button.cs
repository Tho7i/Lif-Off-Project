using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Button : Sprite
{
    public Button() : base("Button.png")
    {
        SetOrigin(this.width / 2, this.height / 2);
    }

}
