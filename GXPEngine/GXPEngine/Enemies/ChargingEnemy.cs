﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class ChargingEnemy : AnimationSprite
{
    private Player _targetPlayer;
    private float _movSpeed = 1.0f;
    private int _health = 2;
    private int _randomise;
    private int _randomise2;
    private float _playerDistance;
    private float _stopDuration = 500;

    private Sound _enemyDamage;

    public ChargingEnemy() : base("ChargingEnemy.png", 8, 2)
    {
        _enemyDamage = new Sound("EnemyDamage.wav", false, false);
        this.SetOrigin(this.width / 2, this.height / 2);
        randomizeEnemyPosition();
    }

    private void handleMovement()
    {
        if (_targetPlayer.x > this.x) { x += _movSpeed; } else if (_targetPlayer.x < this.x) { x -= _movSpeed; }
        if (_targetPlayer.y > this.y) { y += _movSpeed; } else if (_targetPlayer.y < this.y) { y -= _movSpeed; }

        _playerDistance = Mathf.Sqrt(Mathf.Pow(this.x - _targetPlayer.x, 2) + Mathf.Pow(this.y - _targetPlayer.y, 2));
        if (_playerDistance <= 150)
        {
            if(_stopDuration > 0)
            {
                _movSpeed = 0.0f;
                _stopDuration -= Time.deltaTime;
            }
            else { _movSpeed = 2.0f; }
        }
        else { _movSpeed = 1.0f; }
    }

    private void randomizeEnemyPosition()
    {
        _randomise = Utils.Random(1, 3);

        if (_randomise == 1)
        {
            x = Utils.Random(-64, game.width + 65);


            if (x <= 0 || x >= game.width)
            {
                y = Utils.Random(1, game.height + 1);
            }
            else
            {
                _randomise2 = Utils.Random(1, 3);
                if (_randomise2 == 1) { y = -64; }
                else { y = game.height + 64; }
            }
        }
        else
        {
            y = Utils.Random(1, game.height + 1);


            if (y <= 0 || y >= game.height + 65)
            {
                x = Utils.Random(1, game.width + 1);
            }
            else
            {
                _randomise2 = Utils.Random(1, 3);
                if (_randomise2 == 1) { x = -64; }
                else { x = game.width + 64; }
            }
        }
    }

    private void handleAnimation()
    {
        if (this.x > _targetPlayer.x && _movSpeed > 0)
        {
            Animate();
            SetCycle(0, 8, 20, true);
        }

        else if (this.x < _targetPlayer.x && _movSpeed > 0)
        {
            Animate();
            SetCycle(8, 8, 20, true);
        }
    }

    void Update()
    {
        handleAnimation();
        handleMovement();
        if (_health <= 0)
        {
            LateDestroy();
            _targetPlayer.score++;
            if (_targetPlayer.karma < 21) { _targetPlayer.karma++; }
        }
    }

    void OnCollision(GameObject other)
    {
        if (other is Melee)
        {
            _health -= 2;
            _enemyDamage.Play();
            other.LateDestroy();
        }

        if (other is Player)
        {
            _health = 0;
        }

        if (other is Projectile)
        {
            _enemyDamage.Play();
            Projectile projectile = (Projectile)other;
            if (projectile.fire)
            {
                _health -= 2;
            }
            else { _health--; }
            other.LateDestroy();
        }

        if (other is DamagingExplosive)
        {
            _enemyDamage.Play();
            _health -= 2;
        }
    }

    public void SetTargetPlayer(Player player)
    {
        _targetPlayer = player;
    }
}
