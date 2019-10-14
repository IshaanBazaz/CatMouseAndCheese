using System;
using SplashKitSDK;

public abstract class Robot
{
    //variable declaration
    public double X { get; private set; } //variable declaration for x value of bitmap using auto property
    public double Y { get; private set; }  //variable declaration for y value of bitmap using auto property
    public Color MainColor { get; private set; }
    private Vector2D Velocity { get; set;} //creating a vector to make the robot move
    public int Width 
    {
        get
        {
            return 50; //getting the width of the bitmap image
        }
    }
    public int Height 
    {
        get
        {
            return 50; //getting the height of the bitmap image
        }
    }
    public Circle CollisionCircle
    {
        get
        {
            return SplashKit.CircleAt(X + (Width/2), Y + (Width/2), 20); //creating a circle boundary around the robot 
        }
    }

    public Robot(Window gameWindow, Player player) //constructor
    {
        MainColor = Color.RandomRGB(200); //asigning a random color for drawing the robot
        X = SplashKit.Rnd(gameWindow.Width); //seting X value to any random position within the screen bounds
        Y = -Height; //making the robot appear from top side of screen
        const int SPEED = 4;
        Point2D fromPt = new Point2D() //getting a point for the robot
        {
            X = X, Y = Y
        };
        Point2D toPt = new Point2D() //getting a point for the player
        {
            X = X, Y = Convert.ToDouble(gameWindow.Height)
        };
        //calculating the direction for the robot to head
        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));
        //setting the speed and direction to assign it to the 'Velocity'
        Velocity = SplashKit.VectorMultiply(dir, SPEED);
    }

    public abstract void Draw(); //abstract method as a placeholder for overriding methods of children class

    //procedure to make the robot move by the amount in its velocity property
    public void Update()
    {
        X = X + Velocity.X;
        Y = Y + Velocity.Y;
    }

    //procedure to check whether the robot is off the screen
    public bool IsOffscreen(Window screen)
    {
        return (X < -Width || X > screen.Width || Y < -Height || Y > screen.Height);
    }
}

public class Cat1 : Robot
{
    public Cat1(Window gameWindow, Player player) : base(gameWindow, player)//constructor
    {
    }
    public override void Draw() //drawing the robot bitmap on the window
    {
        Bitmap cat1 = new Bitmap("cat1", "cat1.png");
        cat1.Draw(X,Y);
    }
}

public class Cat2 : Robot
{
    public Cat2(Window gameWindow, Player player) : base(gameWindow, player)//constructor
    {
    }
    public override void Draw() //drawing the robot bitmap on the window
    {
        Bitmap cat2 = new Bitmap("cat2", "cat2.png");
        cat2.Draw(X,Y);
    }
}
public class Cat3 : Robot
{
    public Cat3(Window gameWindow, Player player) : base(gameWindow, player)//constructor
    {
    }
    public override void Draw() //drawing the robot bitmap on the window
    {
        Bitmap cat3 = new Bitmap("cat3", "cat3.png");
        cat3.Draw(X,Y);
    }
}
public class Cheese1 : Robot
{
    public Cheese1(Window gameWindow, Player player) : base(gameWindow, player)//constructor
    {
    }
    public override void Draw() //drawing the robot bitmap on the window
    {
        Bitmap cheese1 = new Bitmap("cheese1", "cheese1.png");
        cheese1.Draw(X,Y);
    }
}
public class Cheese2 : Robot
{
    public Cheese2(Window gameWindow, Player player) : base(gameWindow, player)//constructor
    {
    }
    public override void Draw() //drawing the robot bitmap on the window
    {
        Bitmap cheese2 = new Bitmap("cheese2", "cheese2.png");
        cheese2.Draw(X,Y);
    }
}
public class Cheese3 : Robot
{
    public Cheese3(Window gameWindow, Player player) : base(gameWindow, player)//constructor
    {
    }
    public override void Draw() //drawing the robot bitmap on the window
    {
        Bitmap cheese3 = new Bitmap("cheese3", "cheese3.png");
        cheese3.Draw(X,Y);
    }
}