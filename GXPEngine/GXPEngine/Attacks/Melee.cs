using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Melee : Sprite
{

    private float _mouseDirection;
    private float _mouseAngle;
    private float _projectileSpeed = 15.0f;
    private Player _targetPlayer;


    public Melee() : base("triangle.png")
    {
        SetOrigin(this.width / 2, this.height / 2);
    }

    public void SetRotation()
    {
        _mouseDirection = Mathf.Atan2(Input.mouseY - this.y, Input.mouseX - this.x);
        _mouseAngle = (_mouseDirection * (180 / Mathf.PI));
        this.rotation = _mouseAngle;
    }

    public void SetTargetPlayer(Player player)
    {
        _targetPlayer = player;
    }

    private float distanceFromPlayer()
    {
        float distance = Mathf.Sqrt((this.x - _targetPlayer.x) * (this.x - _targetPlayer.x) + (this.y - _targetPlayer.y) * (this.y - _targetPlayer.y));
        return distance;
    }

    void Update()
    {
        Move(_projectileSpeed, 0.0f);

        if (distanceFromPlayer() > 50)
        {
            this.LateDestroy();
        }
    }
}
