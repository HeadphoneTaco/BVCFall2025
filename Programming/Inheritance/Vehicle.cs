
namespace GameProgramming;

/// <summary>
/// The base class for all vehicles, demonstrating basic properties and methods.
/// </summary>
public abstract class Vehicle
{
    // Private Member Data (Encapsulated)
    // Protected fields can be accessed by derived class (Car, Motorcycle etc...)
    protected string _model;
    protected string _color;
    protected float _speed;
    
    // Constructor to initialize member data
    // Default Constructor
    public Vehicle()
    {
        _model = "";
        _color = "Red";
        _speed = 0;
    }

    // Parameterized Constructor
    public Vehicle(string model, string color, float speed)
    {
        _model = model;
        _color = color;
        _speed = speed;
    }

    // Public property to access _model
    public virtual string Model
    {
        get { return _model; }
        set { _model = value; }
    }
    
    // Public property to access _color
    public virtual string Color
    {
        get { return _color; }
        set { _color = value; }
    }
    
    // Public property to access _speed with validation
    public virtual float Speed
    {
        get => _speed;
        set
        {
            if (value >= 0f)
            {
                _speed = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Speed cannot be negative.");
            }
        }
    }
    
    // Public Method function
    // A virtual method to display general information
    // 'Virtual' allows derived classes to change its behavior (Polymorphism)
    public virtual void Info()
    {
        Console.WriteLine($"Model: {Model}, Color: {Color}, Speed: {_speed} km/h");
    }

    public virtual void Drive()
    {
        Console.WriteLine($"{Model} is driving!");
    }

    public virtual void Accelerate(int increase)
    {
        Speed += increase;  // Uses property to ensure validation
        Console.WriteLine($"{Model} is moving at {Speed} km/h.");
    }
}