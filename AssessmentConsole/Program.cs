using System;

namespace AssessmentConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            App app = new App();
            while (!exit) {
                Console.WriteLine(@"
                List paginator tool commands
                1. Paginate
                0. Exit
                "
                );
                Console.WriteLine(">");
                string option = Console.ReadLine();
                if(app.ProcessOption(option)){
                     Console.WriteLine("select option valid");
                }
                //exit = app.ProcessOption(option);
            }
        }
    }
}
