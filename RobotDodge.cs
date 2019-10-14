using System;
using System.Collections.Generic;
using SplashKitSDK;

public class RobotDodge
{
    //variable declaration
    private Player _Player;
    private Window _GameWindow;
    private List<Robot> _Robots = new List<Robot>(); //creating a list of robots
    private bool _success = false;
    public bool Success
    {
        get { return _success; }
        set { _success = value; }
    }
    private List<Lives> _LivesRemaining = new List<Lives>();
    public Timer _Score;
    private Bitmap heart;
    private int bonus = 0;
    public bool Quit
    {
        get
        {
            return _Player.Quit; //checking if the player wants to quit by pressing ESC button
        }
    }

    //constructor method to initialize the variables
    public RobotDodge(Window gameWindow)
    {
        _GameWindow = gameWindow;
        _Player = new Player(_GameWindow);
        _Score = new Timer("Score");
        _Score.Start();
        heart = new Bitmap("Heart","heart.png"); 
    }

    public void HandleInput()
    {
        _Player.HandleInput();              //calling procedure to handle user inputs
        _Player.StayOnWindow(_GameWindow);  //calling procedure to ensure the bitmap stays on the screen by wrapping it around the screen
    }
    public void Draw()
    {
        _GameWindow.Clear(Color.White); //clearing the window
        Score timing = new Score(_GameWindow,_Score);
        timing.Draw(bonus);             
        foreach ( Robot robot in _Robots )
        {
            robot.Draw();  //drawing each robot on the screen
        }
        for ( int i = 1; i <= _LivesRemaining.Count; i++)
        {
            _GameWindow.DrawBitmap(heart, 600 - (i * 50), 415, SplashKit.OptionScaleBmp(.1, .1));
        }
        _Player.Draw();                 //drawing the player on the screen
        Lives life = new Lives(_GameWindow);
        life.Draw();
        DrawLife();
        _GameWindow.Refresh(60);
    }

    //procedure to create a new robot if the player has collided with the robot 
    public void Update()
    {
        foreach ( Robot robot in _Robots )
        {
            robot.Update(); //making each robot move towards the player
        }
        if (_Robots.Count < 5)
        {
            _Robots.Add(RandomRobot()); //adding a random robot to list if another robot collided
        }
        CheckCollisions(); //calling procedure to update the list of robots
    }

    //procedure to create a new random robot
    public Robot RandomRobot()
    {
        Cat1 cat1 = new Cat1(_GameWindow, _Player);
        Cat2 cat2 = new Cat2(_GameWindow, _Player);
        Cat3 cat3 = new Cat3(_GameWindow, _Player);
        Cheese1 cheese1 = new Cheese1(_GameWindow, _Player);
        Cheese2 cheese2 = new Cheese2(_GameWindow, _Player);
        Cheese3 cheese3 = new Cheese3(_GameWindow, _Player);
        Random random = new Random();
        int rnd = random.Next(1,60);
        if ( rnd > 50 && rnd <= 60)
        {
            return cat1;
        }
        else if ( rnd <= 50 && rnd > 40 )
        {
            return cat2;
        }
        else if ( rnd <= 40 && rnd > 30 )
        {
            return cat3;
        }
        else if ( rnd <= 30 && rnd > 20 )
        {
            return cheese1;
        }
        else if ( rnd <= 20 && rnd > 10 )
        {
            return cheese2;
        }
        else
        {
            return cheese3;
        }
    }
    
    //procedure to update the list of robots by removing the unwanted robots
    private void CheckCollisions()
    {
        List<Robot> removeRobots = new List<Robot>(); //creating another list to store unwanted robots
        foreach ( Robot robot in _Robots )
        {
            //adding robots that collided with the player or are off the screen to second list 
            if (_Player.CollidedWith(robot) || robot.IsOffscreen(_GameWindow))
            {
                removeRobots.Add(robot);
            }
            if (_Player.CollidedWith(robot))
            {
                if (robot is Cat1 || robot is Cat2 || robot is Cat3)
                {
                    _GameWindow.DrawText("Avoid Collision", Color.Red, "StencilStd.otf", 100, 400, 300);
                    _GameWindow.Refresh(60);
                    SplashKit.Delay(150);
                }
                else
                {
                    _GameWindow.DrawText("Tasty", Color.Red, "StencilStd.otf", 100, 400, 300);
                    _GameWindow.Refresh(60);
                    SplashKit.Delay(150);
                }
                if (robot is Cat1 || robot is Cat2 || robot is Cat3)
                {
                    if (_LivesRemaining.Count > 1)
                    {
                        _LivesRemaining.RemoveAt(0);
                    }
                    else if (_LivesRemaining.Count == 1)
                    {
                        _LivesRemaining.RemoveAt(0);
                        _GameWindow.Clear(Color.Black);
                        _GameWindow.DrawText("Game Over !!!", Color.Red, "StencilStd.otf", 500, _GameWindow.Width / 2, _GameWindow.Height / 2);
                        _GameWindow.Refresh(60);
                        SplashKit.Delay(4000);
                        _GameWindow.Close();
                    }
                }
                else
                {
                    bonus += 5;
                }
            }
        }
        foreach ( Robot robot in removeRobots )
        {
                _Robots.Remove(robot); //removing robots that are present in second list from 1st list
        }
    }
    
    public void DrawLife()
    {
        for (int k = 0; k < 5 && Success == false; k++)
        {
            Lives remaining = new Lives(_GameWindow);
            _LivesRemaining.Add(remaining);

            if (_LivesRemaining.Count > 5)
            {
                _LivesRemaining.Remove(remaining);
                Success = true;
            }
        }
        _GameWindow.DrawText("Lives: ", Color.Black, "StencilStd.otf", 25, 450, 570);

    }
}