using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using System.Drawing;
using TiledMapParser;

public class TuttorialScreen : Sprite
{
    private Button _startButton;

    //Sounds
    private Sound _playButtonClick;

    public TuttorialScreen() : base("TuttorialScreen.png")
    {
        _startButton = new Button();
        _startButton.SetXY(game.width - _startButton.width / 2 - 30, game.height - 30);
        AddChild(_startButton);

        //Sounds
        _playButtonClick = new Sound("play.mp3");
    }

    private void startOnButtonPress(Button button)
    {
        if (Input.GetMouseButtonUp(0) && button.HitTestPoint(Input.mouseX, Input.mouseY))
        {
            _playButtonClick.Play();
            Level level = new Level();
            game.AddChild(level);
            this.LateDestroy();
        }
    }

    void Update()
    {
        startOnButtonPress(_startButton);
    }
}
