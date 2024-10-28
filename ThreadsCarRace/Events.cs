using System.Runtime.CompilerServices;

namespace ThreadsCarRace;

public static class Events
{
    private const int ThreadTimer = 1000;
    private static readonly object LockObject = new object();
    private static readonly Random Random = new Random();
    
    public static void GetEvent(Car car)
    {

        int timer = 0;
        while (!car.FinishedRace && !car.HasCrashed)
        {
            Thread.Sleep(ThreadTimer);
            timer += 1;
            
            if (car.Speed == 300 && timer == 15)
            {
                timer = 0;

                int randomEvent = Random.Next(1, 101);
                
                if (randomEvent is > 71 and < 99)
                {
                    Display.ListOfEvents?.Add($"\n{car.Name} got some dirt on the windshield!");
                    car.Speed = (int)(car.Speed / 1.2);
                    UndoEvent(car, 5);
                }
                else if (randomEvent is > 61 and < 72)
                {
                    Display.ListOfEvents?.Add($"\n{car.Name} got a flat tire and has to make a pit stop!");
                    car.Speed = 0;
                    car.PitStop = true;
                    UndoEvent(car, 10);
                    car.PitStop = false;
                }
                else if (randomEvent is > 98 and < 101)
                {
                    Display.ListOfEvents?.Add($"\n{car.Name} crashes and is out of the race!");
                    car.Speed = 0;
                    car.HasCrashed = true;
                }
            }
        }
    }

    private static void UndoEvent(Car car, int seconds)
    {
        int timer = 0;
        while (timer != seconds)
        {
            if (car.FinishedRace) break;
            Thread.Sleep(1000);
            timer += 1;
            
        }
        car.Speed = 300;
    }
}