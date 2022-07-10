namespace endpointManager{
    internal class Program{
        static void Main(string[] args){
            LogicLayer logic = new LogicLayer();
            Menu menu = new Menu();
            Application app = new Application(logic, menu);
        } 
    }
}