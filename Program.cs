namespace endpointManager{
    internal class Program{
        static void Main(string[] args){
            List<Endpoint> endpoints = new List<Endpoint>{};
            Menu menu = new Menu();
            Application app = new Application(endpoints, menu);
        } 
    }
}