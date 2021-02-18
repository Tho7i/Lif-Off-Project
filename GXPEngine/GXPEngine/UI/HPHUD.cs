using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class HPHUD : AnimationSprite
{
    Player _targetPlayer;
    public HPHUD() : base("HealthBar.png", 4, 1)
    {
        SetScaleXY(2.0f, 2.0f);
        SetOrigin(0, 0);
        SetXY(200, 0);
    }

    void Update()
    {
        SetFrame(_targetPlayer.GetHealth());
    }

    public void SetTargetPlayer(Player player)
    {
        _targetPlayer = player;
    }
}
