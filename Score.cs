using System;
using System.Collections.Generic;
using SplashKitSDK;

public class Score
{
    protected Window _gameWindow;
    private Timer _score;
    public delegate int Addition(int bonus, int ticks);
    Func <string, string, string> toPrint = ((x, y) => (x + y));
    
    public Score(Window gameWindow,Timer Score)
    {
        _gameWindow = gameWindow;
        _score = Score;
    }
    Addition add = delegate (int bonus, int ticks)
        {
            return ticks + bonus;
        };

    public void Draw(int bonus)
    {
        int finalScore = add(bonus, Convert.ToInt32(_score.Ticks / 1000));
        string scorePrint = toPrint("Score: ", Convert.ToString(finalScore));
        //int finalScore = Convert.ToInt32(_score.Ticks / 1000) + bonus;
        _gameWindow.DrawText(scorePrint, Color.Black, "StencilStd.otf", 20, 10, 570);
    }
}