namespace ThreadsCarRace;

public static class Display
{
    // private static readonly object LockObject = new object();
    public static readonly List<string?> ListOfEvents = new List<string?>();
    
    public static void DisplayRace(List<Car> cars)
    {
        Console.Clear();

        while (true)
        {
            Console.Clear();
            
            foreach (var car in cars)
            {
                if (car.PitStop) Console.WriteLine($"{car.Name} Pit Stop");
                else if (car.HasCrashed) Console.WriteLine($"{car.Name} DNF");
                else
                {
                    Console.WriteLine(car.DistanceTraveled < Race.RaceDistance
                        ? $"{car.Name} : Speed - {car.Speed}: Distance - {car.DistanceTraveled:F2}" 
                        : $"{car.Name} has crossed the finish line");
                }
            }
            DisplayEvents();
            
            var hasFinishedOrCrashed = Race.Cars.All(car => car.FinishedRace || car.HasCrashed);
            if (hasFinishedOrCrashed) break;
            
            // Bug happens because when a car crashes it does not cross the finish line. And this loop keeps going until all cars cross the finish line.
            
            Thread.Sleep(100);
        }
    }

    
    private static void DisplayEvents()
    {
        Console.Write("\nEvent log:");

        foreach (var e in ListOfEvents)
        {
            Console.Write(e);
        }
    }   

    public static void DisplayPodium()
    {
        Console.Clear();
        
        Console.WriteLine("\n" +
                          $"                          {Race.Podium?[0],15 }\r\n" +
                          $"                         @-----------------------@\r\n" +
                          $"       {Race.Podium?[1],15}    |           @           |\r\n" +
                          $"@-----------------------@|           |           |\r\n" +
                          $"|           @           ||           |           | {Race.Podium?[2],15}\r\n" +
                          $"|           |           ||           |           |@-----------------------@\r\n" +
                          $"|           |           ||           |           ||           @           |"
        );
    }
}