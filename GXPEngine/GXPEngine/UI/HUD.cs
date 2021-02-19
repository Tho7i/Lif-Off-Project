using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine;
public class HUD : Canvas
{
    private Player _targetPlayer;
    private Font _font = new Font("Papyrus", 22);
    public HUD() : base(128, 64, false)
    {
    }

    void Update()
    {
        if (_targetPlayer != null)
        {
            graphics.Clear(Color.Empty);
            graphics.DrawString("Score: " + _targetPlayer.score, _font, Brushes.Black, 0, 0);
        }
    }

    public void SetTargetPlayer(Player player)
    {
        _targetPlayer = player;
    }
}