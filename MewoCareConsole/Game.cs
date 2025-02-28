using System;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using System.Timers;

class Game
{
    private static double happiness = 0.0;
    private static double sleepness = 0.0;
    private static double hydration = 0.0;
    private static double fullness = 0.0;
    private static double money = 0;
    private static long unixtime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    private static long lastunix = 0;
    private static bool isRunning = true;
    private static bool sleepon = false;
    private static string displaymewo = @"";
    private static string staticmewo = @"
                                         
                                         
      @@@@@         @@@@@@               
    @@@@@@@@@     @@@@@@@@@@             
    @@@@@@@@@@@@@@@@@@@@@@@@@            
    @@ @@@@@@@@@@@@@@@@@@@ @@            
    @@@@@@@@@@@@@@@@@@@@@@@@@            
    @@@@@@@ @@@@@@@ @@@@@@@@@            
    @@@@@@@ @@@@@@@ @@@@@@@@@            
    @@@@@@@@@@@@@@@@@@@@@@@@@   @@@@@@   
    @@@@@@@@@@@@@@@@@@@@@@@@@  @@@@@@@   
    @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@   
     @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@    
      @@@@@@@@@@@@@@@@@@@@@@@@@@@@@      
        @@@@@@@@@@@@@@@@@                
                                         

";
    private static string happymewo = @"
                                         
                                         
      @@@@@         @@@@@@               
    @@@@@@@@@     @@@@@@@@@@             
    @@@@@@@@@@@@@@@@@@@@@@@@@            
    @@ @@@@@@@@@@@@@@@@@@@ @@            
    @@@@@@@@@@@@@@@@@@@@@@@@@            
    @@@@@@@ @@@@@@@ @@@@@@@@@            
    @@@@@@ @ @@@@@ @ @@@@@@@@            
    @@@@@@@@@@@@@@@@@@@@@@@@@   @@@@@@   
    @@@@@@@@@@@@@@@@@@@@@@@@@  @@@@@@@   
    @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@   
     @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@    
      @@@@@@@@@@@@@@@@@@@@@@@@@@@@@      
        @@@@@@@@@@@@@@@@@                
                                         

";
    private static System.Timers.Timer sleepTimer;
    private static string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    private static string appFolderPath = Path.Combine(appDataPath, "MewoCareDesktop");
    static bool canPressE = true;

    public static void Start()
    {
        if (!Directory.Exists(appFolderPath))
        {
            Directory.CreateDirectory(appFolderPath);
        }
        Console.CursorVisible = false;
        displaymewo = staticmewo;
        happiness = loadDoubleData(Path.Combine(appFolderPath, "happiness.mwcr"));
        sleepness = loadDoubleData(Path.Combine(appFolderPath, "sleepness.mwcr"));
        hydration = loadDoubleData(Path.Combine(appFolderPath, "hydration.mwcr"));
        fullness = loadDoubleData(Path.Combine(appFolderPath, "fullness.mwcr"));
        money = loadDoubleData(Path.Combine(appFolderPath, "money.mwcr"));
        lastunix = loadLongData(Path.Combine(appFolderPath, "lastunix.mwcr"));
        happiness = (double)(happiness - (unixtime - lastunix) / 300);
        sleepness = (double)(sleepness - (unixtime - lastunix) / 300);
        hydration = (double)(hydration - (unixtime - lastunix) / 300);
        fullness = (double)(fullness - (unixtime - lastunix) / 300);

        new Thread(DisplayLoop) { IsBackground = true }.Start();

        sleepTimer = new System.Timers.Timer(1000);
        sleepTimer.Elapsed += (s, e) => sleepness++;

        Random generator = new Random();

        while (isRunning)
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Q:
                    if (!sleepon) {
                        happiness += 0.04;
                        int rand = generator.Next(0, 16);
                        if (rand == 1)
                        {
                            money += 1;
                        }
                    }
                    break;
                case ConsoleKey.W:
                    sleepon = !sleepon;
                    if (sleepon)
                        sleepTimer.Start();
                    else
                        sleepTimer.Stop();
                    break;
                case ConsoleKey.E:
                    if (!sleepon)
                    {
                        fullness += 0.1;
                    }
                    break;
                case ConsoleKey.R:
                    if (!sleepon)
                    {
                        hydration += 0.1;
                    }
                    break;
                case ConsoleKey.Escape:
                    isRunning = false;
                    saveDoubleData(happiness, Path.Combine(appFolderPath, "happiness.mwcr"));
                    saveDoubleData(sleepness, Path.Combine(appFolderPath, "sleepness.mwcr"));
                    saveDoubleData(hydration, Path.Combine(appFolderPath, "hydration.mwcr"));
                    saveDoubleData(fullness, Path.Combine(appFolderPath, "fullness.mwcr"));
                    saveDoubleData(money, Path.Combine(appFolderPath, "money.mwcr"));
                    saveLongData(unixtime, Path.Combine(appFolderPath, "lastunix.mwcr"));
                    break;
            }
        }
    }

    private static void DisplayLoop()
    {
        while (isRunning)
        {
            if (happiness < 0) happiness = 0;
            if (sleepness < 0) sleepness = 0;
            if (hydration < 0) hydration = 0;
            if (fullness < 0) fullness = 0;
            if (money < 0) money = 0;

            unixtime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Happiness: {0} Sleepness: {1} Hydration: {2} Fullness: {3} Lightbulbs: {4} Unix time: {5} Last unix: {6}", Math.Round(happiness), Math.Round(sleepness), Math.Round(hydration), Math.Round(fullness), money, unixtime, lastunix);
            Console.WriteLine(displaymewo);
            Thread.Sleep(50);
        }
    }

    static void saveDoubleData(double data, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine(data);
        }
    }
    static void saveLongData(long data, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine(data);
        }
    }
    static double loadDoubleData(string filePath)
    {
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line = reader.ReadLine();
                return double.Parse(line);
            }
        }
        else
        {
            return 0;
        }
    }
    static long loadLongData(string filePath)
    {
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line = reader.ReadLine();
                return long.Parse(line);
            }
        }
        else
        {
            return 0;
        }
    }
    static bool IsKeyPressed(ConsoleKey key)
{
    return Console.KeyAvailable && Console.ReadKey(true).Key == key;
}
}