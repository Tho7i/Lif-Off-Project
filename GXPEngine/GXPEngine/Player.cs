﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Player : AnimSprite
{

    private int _xSpawn = 200;
    private int _ySpawn = 200;
    private float _speed;
    private bool _shiva = false;
    private bool _ganesh = true;
    private bool _krishna = false;
    //private bool _soundPlayed = false;

    public int score;
    public int karma = 0;
    public bool invincible = false;
    private int _health = 3;


    //reload cooldown in miliseonds
    private float _projectileReload= 500;
    private float _meleeReload = 500;
    private float _explosiveReload = 2000;
    private float _lastTimeShotProjectile = 0;
    private float _lastTimeShotMelee = 0;
    private float _lastTimeShotExplosive = 0;
    private float _karmaDecreaseTime = 10000;
    private float _invincibilityTime = 3000;

    private Level _targetLevel;

    //Sounds
    private Sound _playerDamage;
    private Sound _steps;
    private Sound _wings;
    private Sound _dash;

    //Animations
    AnimationSprite _krishnaAnimation = new AnimationSprite("krishna.png", 6, 4, 24, false, false);
    AnimationSprite _ganeshAnimation = new AnimationSprite("ganesh.png", 6, 4, 24, false, false);
    AnimationSprite _shivaAnimation = new AnimationSprite("shiva.png", 6, 4, 24, false, false);

    //public static Camera mainCamera;

    public Player() : base("Char.png", 3, 1)
    {
        //setting the spawn position
        this.x = _xSpawn;
        this.y = _ySpawn;
        //setting the originat the center of the sprite
        this.SetOrigin(this.width / 2, this.height / 2);
        SetFrame(0);
        _playerDamage = new Sound("PlayerDamage.mp3");
        _dash = new Sound("dash.mp3");
        //_steps = new Sound("steps.mp3");
        //_wings = new Sound("wings.mp3");
        setupAnimations();
        this.alpha = 0;



        //Camera camera = new Camera(0, 0, game.width, game.height);
        //mainCamera = camera;
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
            MoveUntilCollision(-_speed, 0.0f, _targetLevel.FindObjectsOfType(typeof(ObjectsToCollideWith)));
        }

        if (Input.GetKey(Key.D))
        {
            MoveUntilCollision(_speed, 0.0f, _targetLevel.FindObjectsOfType(typeof(ObjectsToCollideWith)));              //added move untilcollision to the rest
        }

        if (Input.GetKey(Key.S))
        {
            MoveUntilCollision(0.0f, _speed, _targetLevel.FindObjectsOfType(typeof(ObjectsToCollideWith)));
        }

        if (Input.GetKey(Key.W))
        {
            MoveUntilCollision(0.0f, -_speed, _targetLevel.FindObjectsOfType(typeof(ObjectsToCollideWith)));
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------
        //                                                 Phasing (Teleporting a short distance)
        //-------------------------------------------------------------------------------------------------------------------------------------------
        if (Input.GetKeyUp(Key.SPACE) && Input.GetKey(Key.A))
        {
            for (int i = 0; i <150; i++)
            {
                MoveUntilCollision(-1.0f, 0.0f, _targetLevel.FindObjectsOfType(typeof(ObjectsToCollideWith)));
                if (i % 15 == 0 && _krishna)
                {
                    Fire fire = new Fire();
                    fire.SetXY(this.x + fire.width / 2, this.y);
                    _targetLevel.AddChild(fire);
                }
            }  
        }

        if (Input.GetKeyUp(Key.SPACE) && Input.GetKey(Key.D))
        {
            for (int i = 0; i < 150; i++)
            {
                MoveUntilCollision(1.0f, 0.0f, _targetLevel.FindObjectsOfType(typeof(ObjectsToCollideWith)));
                if (i % 15 == 0 && _krishna)
                {
                    Fire fire = new Fire();
                    fire.SetXY(this.x - fire.width / 2, this.y);
                    _targetLevel.AddChild(fire);
                }
            }
        }

        if (Input.GetKeyUp(Key.SPACE) && Input.GetKey(Key.W))           //added move until collision 
        {
            for (int i = 0; i < 150; i++)
            {
                MoveUntilCollision(0.0f, -1.0f, _targetLevel.FindObjectsOfType(typeof(ObjectsToCollideWith)));
                if (i % 15 == 0 && _krishna)
                {
                    Fire fire = new Fire();
                    fire.SetXY(this.x, this.y + fire.height / 2);
                    _targetLevel.AddChild(fire);
                }
            }
        }

        if (Input.GetKeyUp(Key.SPACE) && Input.GetKey(Key.S))
        {
            for (int i = 0; i < 150; i++)
            {
                MoveUntilCollision(0.0f, 1.0f, _targetLevel.FindObjectsOfType(typeof(ObjectsToCollideWith)));
                if (i % 15 == 0 && _krishna)
                {
                    Fire fire = new Fire();
                    fire.SetXY(this.x, this.y - fire.height / 2);
                    _targetLevel.AddChild(fire);
                }
            }
        }

        if (Input.GetKeyUp(Key.SPACE)) { _dash.Play(); }
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

        if (_shiva)
        {
            _speed = 2.4f;

        }
        else if (_ganesh)
        {
            _speed = 2.3f;
        }
        else if (_krishna)
        {
            _speed = 2.5f;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                        handleAnimation()
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void handleAnimation()
    {
        if (Input.GetKey(Key.A))
        {
            _krishnaAnimation.Animate();
            _krishnaAnimation.SetCycle(6, 6, 20, true);
            _shivaAnimation.Animate();
            _shivaAnimation.SetCycle(6, 6, 20, true);
            _ganeshAnimation.Animate();
            _ganeshAnimation.SetCycle(6, 6, 20, true);
        }

        else if (Input.GetKey(Key.D))
        {
            _krishnaAnimation.Animate();
            _krishnaAnimation.SetCycle(12, 6, 20, true);
            _shivaAnimation.Animate();
            _shivaAnimation.SetCycle(12, 6, 20, true);
            _ganeshAnimation.Animate();
            _ganeshAnimation.SetCycle(12, 6, 20, true);
        }

        else if (Input.GetKey(Key.S))
        {
            _krishnaAnimation.Animate();
            _krishnaAnimation.SetCycle(0, 6, 20, true);
            _shivaAnimation.Animate();
            _shivaAnimation.SetCycle(0, 6, 20, true);
            _ganeshAnimation.Animate();
            _ganeshAnimation.SetCycle(0, 6, 20, true);
        }

        else if (Input.GetKey(Key.W))
        {
            _krishnaAnimation.Animate();
            _krishnaAnimation.SetCycle(18, 6, 20, true);
            _shivaAnimation.Animate();
            _shivaAnimation.SetCycle(18, 6, 20, true);
            _ganeshAnimation.Animate();
            _ganeshAnimation.SetCycle(18, 6, 20, true);
        }
        animationVisibility();
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                        setupAnimations()
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void setupAnimations()
    {
        _krishnaAnimation.SetOrigin(width / 2, height / 2);
        AddChild(_krishnaAnimation);
        _ganeshAnimation.SetOrigin(width / 2, height / 2);
        AddChild(_ganeshAnimation);
        _shivaAnimation.SetOrigin(width / 2, height / 2);
        AddChild(_shivaAnimation);
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                        animationVisibility()
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void animationVisibility()
    {
        _shivaAnimation.visible = _shiva;
        _ganeshAnimation.visible = _ganesh;
        _krishnaAnimation.visible = _krishna;
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                        automaticKarmaDecrease()
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void automaticKarmaDecrease()
    {
        _karmaDecreaseTime -= Time.deltaTime;
        if (_karmaDecreaseTime <= 0)
        {
            karma--;
            _karmaDecreaseTime = 10000;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                        Invincibility()
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void invincibility()
    {
        if (karma >= 21)
        {
            invincible = true;
            _invincibilityTime = 3000;
        }
        if (invincible)
        {
            _invincibilityTime -= Time.deltaTime;
            if (_invincibilityTime <= 0)
            {
                invincible = false;
                karma = 0;
            }
        }
    }

    //private void handleSound()
    //{
    //    for (int i = 3; i < 21; i += 3)
    //    {
    //        if (_ganesh && _ganeshAnimation.currentFrame == i && !_soundPlayed|| _krishna && _krishnaAnimation.currentFrame == i && !_soundPlayed)
    //        {
    //            _steps.Play();
    //            _soundPlayed = true;
    //        }

    //        else if (_shiva && _shivaAnimation.currentFrame == i && !_soundPlayed)
    //        {
    //            _wings.Play();
    //            _soundPlayed = true;
    //        }
            
    //        if (_ganeshAnimation.currentFrame != i || _krishnaAnimation.currentFrame != i || _shivaAnimation.currentFrame != i)
    //        {
    //            _soundPlayed = false;
    //        }
    //    }
    //}

    void Update()
    {
        //handleSound();
        invincibility();
        automaticKarmaDecrease();
        handleAnimation();
        handleShifting();
        handleMovement();
        handleShooting();
    }

    void OnCollision(GameObject other)
    {
        if (other is Enemy || other is RangedEnemy || other is ChargingEnemy || other is EnemyProjectile)
        {
            if (!invincible)
            {
                _health--;
                _playerDamage.Play();
            }
            other.LateDestroy();
        }
    }


    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                        pubblic getters/setters
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public int GetHealth()
    {
        return _health;
    }

    public void SetTargetLevel(Level level)
    {
        _targetLevel = level;
    }
}