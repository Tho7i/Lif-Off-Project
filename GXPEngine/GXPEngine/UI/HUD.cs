using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine;
public class HUD : Canvas
{
    private Player _targetPlayer;
    public HUD() : base(128, 64, false)
    {
        SetScaleXY(3.0f);
    }

    void Update()
    {
        if (_targetPlayer != null)
        {
            graphics.Clear(Color.Empty);
            graphics.DrawString("Score: " + _targetPlayer.score, SystemFonts.DefaultFont, Brushes.Black, 0, 0);
            //graphics.DrawString("Karma: " + _targetPlayer.GetKarma(), SystemFonts.DefaultFont, Brushes.White, 100, 100);
        }
    }

    public void SetTargetPlayer(Player player)
    {
        _targetPlayer = player;
    }
}