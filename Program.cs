namespace endpointManager{
    internal class Program{
        static void Main(string[] args){
            Console.Clear();
            LogicLayer logic = new LogicLayer();
            Menu menu = new Menu();
            Test tests = new Test(logic); // uncomment this will run the unit tests and display results on console
            Application app = new Application(logic, menu); // uncomment this will run the application            
        } 
    }
}