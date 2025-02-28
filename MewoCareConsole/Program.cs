using System;

class Program
{
    static void Main()
    {
        string asciiArtMenu = @"
                                         
                                         
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
        Console.WriteLine("MewoCareConsole v0.1\n");
        Console.WriteLine(asciiArtMenu + "\n");
        Console.WriteLine("Press Enter to start");
        Console.WriteLine("Controls: ");
        Console.WriteLine("Q to pet Mewo");
        Console.WriteLine("W to put Mewo sleep");
        Console.WriteLine("E to fill water bowl");
        Console.WriteLine("R to fill food bowl");

        ConsoleKeyInfo key = Console.ReadKey();
        if (key.Key == ConsoleKey.Enter)
        {
            Console.Clear();
            Game.Start();
        }
    }
}
