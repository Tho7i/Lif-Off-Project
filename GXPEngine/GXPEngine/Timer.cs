using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Timer : GameObject
{
    private int _time;
    private Action _onTimeout;

    public Timer(int time, Action onTimeout)
    {
        _time = time;
        _onTimeout = onTimeout;
    }

    public void Update()
    {
        Console.WriteLine("Test");
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            _onTimeout();
            Destroy();
        }
    }
}
