using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine;
public class HUD : Canvas
{
    private Player _targetPlayer;
    Font font = null;
    public HUD() : base(128, 64, false)
    {
        font = new Font("Papyrus", 22);
    }

    void Update()
    {
        if (_targetPlayer != null)
        {
            graphics.Clear(Color.Empty);
            graphics.DrawString("Score:" + _targetPlayer.score, font, Brushes.Black, 0, 0);
            graphics.DrawString("Karma:" + _targetPlayer.karma, font, Brushes.White, 0, 100);
        }
    }

    public void SetTargetPlayer(Player player)
    {
        _targetPlayer = player;
    }
}