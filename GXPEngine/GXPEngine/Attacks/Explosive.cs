using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;

public class Explosive : Sprite
{
    private float _explosiveSpeed = 10.0f;
    private float _mouseDirection;
    private float _mouseAngle;
    private float _mouseX;
    private float _mouseY;
    private float _explosionTime = 1500;
    private Vector2 _direction;

    public Explosive() : base("circle.png", false, false)
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

        //Vector2 newApproach = new Vector2(Input.mouseX / 960.0f, Input.mouseY / 576.0f);
        //newApproach.x = newApproach.x * 2.0f - 1.0f;
        //newApproach.y = newApproach.y * 2.0f - 1.0f;

        //_direction = newApproach;
        //float mag = (float)Math.Sqrt(_direction.x * _direction.x + _direction.y * _direction.y);
        //_direction.x /= mag;
        //_direction.y /= mag;
    }

    void Update()
    {
        //Move(_explosiveSpeed * _direction.x, _explosiveSpeed * _direction.y);
        Move(_explosiveSpeed, 0);
        if (Mathf.Abs(this.x - _mouseX) < 5 && Mathf.Abs(this.y - _mouseY) < 5)
        {
            _explosiveSpeed = 0.0f;
            if (_explosionTime <= 500)
            {
                DamagingExplosive explosion = new DamagingExplosive();
                AddChild(explosion);
            }
            
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