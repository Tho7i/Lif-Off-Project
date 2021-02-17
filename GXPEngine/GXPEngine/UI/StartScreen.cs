using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using System.Drawing;
using TiledMapParser;

public class StartScreen : AnimationSprite
{
    private Button _startButton;

    public StartScreen() : base("Try.png", 2, 1)
    {
        _startButton = new Button();
        _startButton.SetXY(game.width / 2, game.height / 3 * 2);
        AddChild(_startButton);
    }

    private void startOnButtonPress(Button button)
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (button.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                Level level = new Level();
                game.AddChild(level);
                this.LateDestroy();
            }
        }
    }

    void Update()
    {
        Animate();
        SetCycle(0, 2, 5, true);
        startOnButtonPress(_startButton);
    }
}
