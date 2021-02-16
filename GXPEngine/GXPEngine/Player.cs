using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Player : AnimSprite
{

    private int _xSpawn = 200;
    private int _ySpawn = 200;
    private float _speed = 2.4f;
    private bool _shiva = false;
    private bool _ganesh = true;
    private bool _krishna = false;

    public int score;
    private int _karma;
    private int _health = 3;


    //reload cooldown in miliseonds
    private float _projectileReload= 500;
    private float _meleeReload = 500;
    private float _explosiveReload = 4000;
    private float _lastTimeShotProjectile = 0;
    private float _lastTimeShotMelee = 0;
    private float _lastTimeShotExplosive = 0;

    private Level _targetLevel;

    //Sounds
    private Sound _playerDamage;


    public Player() : base("Char.png", 3, 1)
    {
        //setting the spawn position
        this.x = _xSpawn;
        this.y = _ySpawn;
        //setting the originat the center of the sprite
        this.SetOrigin(this.width / 2, this.height / 2);
        SetFrame(0);
        _playerDamage = new Sound("PlayerDamage.mp3", false, false);

        //Camera camera = new Camera(0, 0, game.width, game.height);
        //AddChild(camera);
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                        handleMovement()
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void handleMovement()
    {
        //-------------------------------------------------------------------------------------------------------------------------------------------
        //                                                         Handling the controls
        //-------------------------------------------------------------------------------------------------------------------------------------------
        if (Input.GetKey(Key.A))
        {
            MoveUntilCollision(-_speed, 0.0f);
        }

        if (Input.GetKey(Key.D))
        {
            MoveUntilCollision(_speed, 0.0f);              //added move untilcollision to the rest
        }

        if (Input.GetKey(Key.S))
        {
            MoveUntilCollision(0.0f, _speed);
        }

        if (Input.GetKey(Key.W))
        {
            MoveUntilCollision(0.0f, -_speed);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------
        //                                                 Phasing (Teleporting a short distance)
        //-------------------------------------------------------------------------------------------------------------------------------------------
        if (Input.GetKeyUp(Key.SPACE) && Input.GetKey(Key.A))
        {
            for (int i = 0; i <150; i++)
            {
                MoveUntilCollision(-1.0f, 0.0f);
            }  
        }

        if (Input.GetKeyUp(Key.SPACE) && Input.GetKey(Key.D))
        {
            for (int i = 0; i < 150; i++)
            {
                MoveUntilCollision(1.0f, 0.0f);
            }
        }

        if (Input.GetKeyUp(Key.SPACE) && Input.GetKey(Key.W))           //added move until collision 
        {
            for (int i = 0; i < 150; i++)
            {
                MoveUntilCollision(0.0f, -1.0f);
            }
        }

        if (Input.GetKeyUp(Key.SPACE) && Input.GetKey(Key.S))
        {
            for (int i = 0; i < 150; i++)
            {
                MoveUntilCollision(0.0f, 1.0f);
            }
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                        handleShooting()
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void handleShooting()
    {
        if (Input.GetMouseButtonDown(0) && _shiva && _lastTimeShotProjectile + _projectileReload < Time.now)
        {
            Projectile projectile = new Projectile();
            //projectile.SetTargetPlayer(this);
            //projectile.SetTargetLevel(_targetLevel);
            this.parent.AddChild(projectile);
            projectile.SetXY(this.x, this.y - this.height / 2 + 20);
            projectile.SetRotation();
            _lastTimeShotProjectile = Time.now;
        }

        else if (Input.GetMouseButtonDown(0) && _ganesh && _lastTimeShotMelee + _meleeReload < Time.now)
        {
            Melee melee = new Melee();
            melee.SetTargetPlayer(this);
            this.parent.AddChild(melee);
            melee.SetXY(this.x, this.y);
            melee.SetRotation();
            _lastTimeShotMelee = Time.now;
        }

        else if (Input.GetMouseButtonDown(0) && _krishna && _lastTimeShotExplosive + _explosiveReload < Time.now)
        {
            Explosive explosive = new Explosive();
            this.parent.AddChild(explosive);
            explosive.SetXY(this.x, this.y - this.height / 2 + 20);
            explosive.SetRotation();
            _lastTimeShotExplosive = Time.now;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                        handleShifting()
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void handleShifting()
    {
        if (Input.GetKeyUp(Key.LEFT_SHIFT) && _shiva)
        {
            _ganesh = true;
            _shiva = false;
            SetFrame(0);
        }

        else if (Input.GetKeyUp(Key.LEFT_SHIFT) && _ganesh)
        {
            _krishna = true;
            _ganesh = false;
            SetFrame(1);
        }

        else if (Input.GetKeyUp(Key.LEFT_SHIFT) && _krishna)
        {
            _shiva = true;
            _krishna = false;
            SetFrame(2);
        }
    }

    void Update()
    {
        handleShifting();
        handleMovement();
        handleShooting();
    }

    void OnCollision(GameObject other)
    {
        if (other is Enemy)
        {
            _health--;
            _playerDamage.Play();
            other.LateDestroy();    //changed to LateDestroy to avoid crash
        }
    }


    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                        pubblic getters/setters
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public int GetKarma()
    {
        return _karma;
    }

    public int GetHealth()
    {
        return _health;
    }

    public void SetTargetLevel(Level level)
    {
        _targetLevel = level;
    }
}