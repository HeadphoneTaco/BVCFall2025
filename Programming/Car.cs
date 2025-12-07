namespace GameProgramming;

/// <summary>
/// Car inheriting from Vehicle class, acquiring all its properties and methods
/// </summary>
public class Car : Vehicle
{
    // Constructor to initialize member data
    // Default Constructor
    //public Car() : base()
    // Calls base class constructor

    //Car Specific field
    private int _numberOfDoors;

    public int NumberOfDoors
    {
        get { return _numberOfDoors; } 
        set { _numberOfDoors = value; }
    }

    //Default Constructor for car, calling the base class constructor with default values
    public Car() :base("Unknown Car", "Red", 0f)
    {
        _numberOfDoors = 4;
    }
    
    //Parameterized constructor. 'base' calls the constructor of the parent class (Vehicle)
    public Car(string model, string color, float speed, int numberOfDoors)
        : base(model, color, speed)
    {
        
    }
    
    
    // Public Method function
    // 'override' changes the behavior of the base class method (Polymorphism)
    public override void Info()
    {
        //calls the base class Info method
        base.Info();
        // then adds car specific information
        Console.WriteLine($", Doors: {NumberOfDoors}");
    }

    
    public override void Drive()
    {
        Console.WriteLine($"{Model} is driving!");
    }

    public void Accelerate(int increase)
    {
        Speed += increase;  // Uses property to ensure validation
        Console.WriteLine($"{Model} is moving at {Speed} km/h.");
    }
}