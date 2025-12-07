namespace GameProgramming;

internal static class Program
{
    private static void Main(string[] args)
    {
        /*
    Console.WriteLine("===== Car 1 =====");
    // Creating an object
    Car myCar = new Car();

    // Setting properties
    myCar.Model = "Tesla";
    myCar.Color = "Red";
    myCar.Speed = 10f;
    // myCar._speed = -10; // Error: _speed is private and inaccessible

    // Creating an object
    myCar.Info();   // Output - Model: Tesla, Color: Red, Speed: 10 km/h
    myCar.Drive();  // Output - Tesla is driving!
    myCar.Accelerate(5);    // Output - Tesla is moving at 15 km/h

    Console.WriteLine("===== Car 2 =====");
    // Creating an object
    Car myCar2 = new Car("Hyundai", "White", 30f);

    // Creating an object
    myCar2.Info();
    myCar2.Drive();
    myCar2.Accelerate(5);
    */
        
        //Creating and instance of the car class
        Car myCar = new Car("tesla", "White", 50f, 4);
        
        Console.WriteLine("===== Car =====");
        // Calls the *overridden* Info method in Car class
        myCar.Info();
        
        //Calls the *inherited* accelerate method from Vehicle class
        myCar.Accelerate(10);

    }
}