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

    RangedEnemy enemy;
    Player player;
    HUD hud;
    TiledLoader loader;

    public Level() : base()
    {
        setupLevel();             
        player = new Player();
        AddChild(player);
        player.SetTargetLevel(this);

        //Setting up HUD
        hud = new HUD();
        AddChild(hud);
        hud.SetTargetPlayer(player);
    }

    //level loading method
	public void setupLevel()
	{
		LoadMap("Project3MAP02.tmx");

	}

    //tiles loading(order matters depending on how the tiles are ordered)
    private void LoadMap(string filename)
    {
        loader = new TiledLoader(filename);



        loader.addColliders = false;    //disables collision
        loader.LoadTileLayers(0);

        loader.addColliders = false;    //disables collision
        loader.LoadTileLayers(1);

        loader.addColliders = true;     //enables collision
        loader.LoadTileLayers(2);





        loader.autoInstance = true;     //instantiates object layers
        loader.LoadObjectGroups();



    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                        enemySpawning()
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void enemySpawning()
    {
        if (Time.time - _lastTimeSpawned >= 5000)
        {
            for (int i = 0; i < _enemiesToSpawn; i++)
            {
                enemy = new RangedEnemy();
                AddChild(enemy);
                enemy.SetTargetPlayer(player);
                _lastTimeSpawned = Time.time;
            }
            if (_enemiesToSpawn < 8)
            {
                _enemiesToSpawn++;
            }
                    
        }
    }

    void Update()
    {
        enemySpawning();
        if (player.GetHealth() <= 0)
        {
            this.Destroy();
            StartScreen startScreen = new StartScreen();
            game.AddChild(startScreen);
        }
    }
}
