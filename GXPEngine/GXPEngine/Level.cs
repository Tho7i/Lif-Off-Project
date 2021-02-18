using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;       //necessary to read the map

public class Level : GameObject
{
    private int _lastTimeSpawned = -5000;
    private int _enemiesToSpawn = 1;

    Enemy _enemy;
    RangedEnemy _rangedEnemy;
    ChargingEnemy _chargingEnemy;
    Player _player;
    HUD _hud;
    TiledLoader _loader;

    public Level() : base()
    {
        setupLevel();             
        _player = new Player();
        AddChild(_player);
        _player.SetTargetLevel(this);

        //Setting up HUD
        _hud = new HUD();
        AddChild(_hud);
        _hud.SetTargetPlayer(_player);
    }

    //level loading method
	public void setupLevel()
	{
		LoadMap("Project3MAP02.tmx");

	}

    //tiles loading(order matters depending on how the tiles are ordered)
    private void LoadMap(string filename)
    {
        _loader = new TiledLoader(filename);



        _loader.addColliders = false;    //disables collision
        _loader.LoadTileLayers(0);

        _loader.addColliders = false;    //disables collision
        _loader.LoadTileLayers(1);

        _loader.addColliders = true;     //enables collision
        _loader.LoadTileLayers(2);





        _loader.autoInstance = true;     //instantiates object layers
        _loader.LoadObjectGroups(0);

        if (_loader.Positions != null)
        {
            for (int i = 0; i < _loader.Positions.Count; i++)
            {
                ObjectsToCollideWith idk = new ObjectsToCollideWith(_loader.Positions[i].x, _loader.Positions[i].y - 32);
                AddChild(idk);
            }
        }


    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                        enemySpawning()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void enemySpawning()
    {
        if (this.FindObjectsOfType(typeof(Enemy)).Length + this.FindObjectsOfType(typeof(RangedEnemy)).Length + this.FindObjectsOfType(typeof(ChargingEnemy)).Length <= 20 && Time.time - _lastTimeSpawned >= 5000)
        {
            for (int i = 0; i < _enemiesToSpawn; i++)
            {
                float randomize = Utils.Random(1, 4);
                if (randomize == 1)
                {
                    _enemy = new Enemy();
                    AddChild(_enemy);
                    _enemy.SetTargetPlayer(_player);
                }
                
                else if (randomize == 2)
                {
                    _rangedEnemy = new RangedEnemy();
                    AddChild(_rangedEnemy);
                    _rangedEnemy.SetTargetPlayer(_player);
                }

                else if (randomize == 3)
                {
                    _chargingEnemy = new ChargingEnemy();
                    AddChild(_chargingEnemy);
                    _chargingEnemy.SetTargetPlayer(_player);
                }
            }
            _enemiesToSpawn++;
            _lastTimeSpawned = Time.time;
        }
    }

    void Update()
    {
        //enemySpawning();
        if (_player.GetHealth() <= 0)
        {
            this.Destroy();
            StartScreen startScreen = new StartScreen();
            game.AddChild(startScreen);
        }
    }
}
