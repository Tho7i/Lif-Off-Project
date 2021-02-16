using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Explosive : Sprite
{
    private float _explosiveSpeed = 10.0f;
    private float _mouseDirection;
    private float _mouseAngle;
    private float _mouseX;
    private float _mouseY;
    private float _explosionTime = 3000;

    public Explosive() : base("circle.png")
    {
        SetScaleXY(0.5f, 0.5f);
        SetOrigin(this.width / 2, this.height / 2);
        _mouseX = Input.mouseX;
        _mouseY = Input.mouseY;
    }

    public void SetRotation()
    {
        _mouseDirection = Mathf.Atan2(Input.mouseY - this.y, Input.mouseX - this.x);
        _mouseAngle = (_mouseDirection * (180 / Mathf.PI));
        this.rotation = _mouseAngle;
    }

    void Update()
    {
        Move(_explosiveSpeed, 0.0f);
        if (Mathf.Abs(this.x - _mouseX) < 5 && Mathf.Abs(this.y - _mouseY) < 5)
        {
            _explosiveSpeed = 0.0f;
            SetScaleXY(1, 1);
            _explosionTime -= Time.deltaTime;
            if (_explosionTime <= 0)
            {
                LateDestroy();
            }
        }

        if (x < 0 || x > game.width || y < 0 || y > game.width)
        {
            LateDestroy();
        }
    }
}