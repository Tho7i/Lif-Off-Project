using System;									// System contains a lot of default C# libraries 
using System.Drawing;                           // System.Drawing contains a library used for canvas drawing below
using GXPEngine;                                // GXPEngine contains the engine
using TiledMapParser;

public class MyGame : Game
{
	public MyGame() : base(960, 576, false, false)		// Create a window that's 960x576 and NOT fullscreen
	{
		StartScreen startScreen = new StartScreen();
		AddChild(startScreen);
		targetFps = 60;
    }

    void Update()
	{
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}