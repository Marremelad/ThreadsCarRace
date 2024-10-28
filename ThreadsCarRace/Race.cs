namespace ThreadsCarRace;

public static class Race
{
    private static readonly object LockObject = new object();
    public const double RaceDistance = 10000.0;
    public static readonly List<string?> Podium = new List<string?>();
    private static readonly List<Car> Crashed = new List<Car>();
    public static readonly List<Car> Cars = new List<Car>()
    {
        new Car("Lightning McQueen"),
        new Car("Chick Hicks"),
        new Car("Strip Weathers"),
    };
    
    public static void Run()
    {
        List<Thread> threads = new List<Thread>();
        
        for (int i = 0; i < 3; i++)
        {
            Car car = Cars[i];
            Thread thread = new Thread(() => Go(car));
            threads.Add(thread);
            thread.Start();
        }
        
        for (int i = 0; i < 3; i++)
        {
            Car car = Cars[i];
            Thread thread = new Thread(() => Events.GetEvent(car));
            threads.Add(thread);
            thread.Start();
        }
        
        Display.DisplayRace(Cars);
        
        foreach (var thread in threads)
        {
            thread.Join();
        }

        foreach (var car in Crashed.OrderByDescending(c => c.DistanceTraveled))
        {
            Podium.Add(car.Name);
        }
        
        Thread.Sleep(3000);
        Display.DisplayPodium();
    }

    private static void Go(Car car)
    {
        while (true)
        {
            Thread.Sleep(100);
            car.DistanceTraveled += car.Speed / 36.0;

            if (car.DistanceTraveled >= RaceDistance)
            {
                car.FinishedRace = true;
                
                lock (LockObject)
                {
                    Podium.Add(car.Name);
                }
                break;
            }

            if (car.HasCrashed)
            {
                lock (LockObject)
                {
                    Crashed.Add(car);
                }
                break;
            }
        }
    }
}