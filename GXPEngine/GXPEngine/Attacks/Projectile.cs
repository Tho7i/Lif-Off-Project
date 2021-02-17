using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;

public class Projectile : Sprite
{
    private float _mouseDirection;
    private float _mouseAngle;
    private float _projectileSpeed = 15.0f;
    private Vector2 _direction;

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
        ////Camera camera = Player.mainCamera;
        ////Vector2 transf = camera.TransformPoint(0, 0);
        ////_direction = new Vector2(Input.mouseX - (transf.x + this.x), Input.mouseY - (transf.y + this.y));
        ////float mag = (float)Math.Sqrt(_direction.x * _direction.x + _direction.y * _direction.y);
        ////_direction.x /= mag;
        ////_direction.y /= mag;

        //Vector2 newApproach = new Vector2(Input.mouseX / 960.0f, Input.mouseY / 576.0f);
        //newApproach.x = newApproach.x * 2.0f - 1.0f;
        //newApproach.y = newApproach.y * 2.0f - 1.0f;

        //_direction = newApproach;
        //float mag = (float)Math.Sqrt(_direction.x * _direction.x + _direction.y * _direction.y);
        //_direction.x /= mag;
        //_direction.y /= mag;

        ////960, 576

        ////Console.WriteLine(" Camera : {0} - {1}", transf.x, transf.y);
        //Console.WriteLine(" Mouse : {0} - {1}", Input.mouseX / 960.0f, Input.mouseY / 576.0f);
        ////Console.WriteLine(" This : {0} - {1}", x, y);
        ////Console.WriteLine("{0} {1}", _direction.x, _direction.y);

        ////vec3 = 

        _mouseDirection = Mathf.Atan2(Input.mouseY - this.y, Input.mouseX - this.x);

        _mouseAngle = (_mouseDirection * (180 / Mathf.PI));
        this.rotation = _mouseAngle;
    }

    void Update()
    {
        //Move(_projectileSpeed * _direction.x, _projectileSpeed * _direction.y);
        Move(_projectileSpeed, 0);

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
