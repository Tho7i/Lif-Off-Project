using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Projectile : Sprite
{
    private float _mouseDirection;
    private float _mouseAngle;
    private float _projectileSpeed = 15.0f;

    //private Player _targetPlayer;
    //private Level _targetLevel;

    public bool fire = false;

    public Projectile() : base("triangle.png")
    {
        SetScaleXY(0.2f, 0.2f);
        SetOrigin(0, height / 2f);
    }

    public void SetRotation()
    {
        _mouseDirection = Mathf.Atan2(Input.mouseY- this.y, Input.mouseX - this.x);
        _mouseAngle = (_mouseDirection * (180 / Mathf.PI));
        this.rotation = _mouseAngle;
    }

    void Update()
    {
        Move(_projectileSpeed, 0.0f);
        if (this.x > game.width || this.x < 0 || this.y < 0 || this.y > game.height)
        {
            this.LateDestroy();
        }
    }

    void OnCollision(GameObject other)
    {
        if (other is Fire)
        {
            fire = true;
        }
    }

    //public void SetTargetPlayer(Player player)
    //{
    //    _targetPlayer = player;
    //}

    //public void SetTargetLevel(Level level)
    //{
    //    _targetLevel = level;
    //}
}
