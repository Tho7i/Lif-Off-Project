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
    private TuttorialButton tuttorialButton;

    //Sounds
    private Sound _playButtonClick;
    private Sound _music;

    public StartScreen() : base("StartScreen.png", 2, 1)
    {
        _startButton = new Button();
        _startButton.SetXY(game.width / 2, game.height / 3 * 2);
        AddChild(_startButton);
        tuttorialButton = new TuttorialButton();
        tuttorialButton.SetXY(game.width / 2, game.height / 3 * 2 + 100);
        AddChild(tuttorialButton);
        _playButtonClick = new Sound("play.mp3");
        _music = new Sound("BattleMusic.mp3", true, true);
        _music.Play(false, 0, 0.1f);
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
        Animate();
        SetCycle(0, 2, 10, true);
        startOnButtonPress(_startButton);
    }
}
