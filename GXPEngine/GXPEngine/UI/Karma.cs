using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Karma : AnimationSprite
{
    Player _targetPlayer;

    public Karma() : base("KarmaBar.png", 1, 22)
    {
        SetScaleXY(2.0f, 2.0f);
        SetOrigin(0, 0);
        SetXY(200, 22);
    }

    void Update()
    {
        SetFrame(_targetPlayer.karma);
    }

    public void SetTargetPlayer(Player player)
    {
        _targetPlayer = player;
    }
}
