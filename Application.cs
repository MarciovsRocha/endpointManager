namespace endpointManager
{
    public class Application : IApplication
    {
        // private declarations
        private Menu _menu;
        private LogicLayer _logicLayer;

        // implementation
        
        public Application(LogicLayer logicLayer, Menu menu){
            this._logicLayer = logicLayer;
            this._menu = menu;
            while(true){
                try { start(); }
                catch (System.Exception e) { Pause(e.Message); }                
            }            
        }

        private void start(){
            Console.Clear();
            Console.Write(this._menu.MenuPrincipal);
            
            int option;
            string ?userInput = Console.ReadLine();            
            if (!string.IsNullOrEmpty(userInput))
                option = Convert.ToInt32(userInput);
            else 
                option = -1;
            
            switch(option){
                case 1:
                    InsertEndpoint();
                    break;
                case 2:
                    EditEndpoint();
                    break;
                case 3:
                    DeleteEndpoint();
                    break;
                case 4:
                    ListEndpoints();
                    break;
                case 5:
                    SearchEndpoint();
                    break;
                case 6:
                    exit();
                    break;                    
                default:
                    InvalidOption("");
                    break;
            }
        }

        public void SearchEndpoint(){
            try{ 
                Endpoint ?e = FindEndpoint(); 
                Pause(e.Info);
            }
            catch (EndpointNotFoundError){
                Console.WriteLine();
                Pause("Not found an Endpoint with specified Serial Number.");
            }                                             
        }

        public void DeleteEndpoint(){                        
            try{                 
                Console.Clear();                
                Console.WriteLine(this._menu.DeleteEndpoint);           
                Endpoint ?e = FindEndpoint();                                 
                Console.WriteLine(e.Info);                
                if (Confirm()){
                    this._logicLayer.Delete(e.SerialNumber);
                    Pause("Endpoint remove sucessfuly.");
                }else
                    Pause("Operation aborted.");            
            }
            catch (EndpointNotFoundError){
                Pause("Not found an Endpoint with specified Serial Number.");
            }            
        }

        public void EditEndpoint(){
            try{                 
                Console.Clear();                
                Console.WriteLine(this._menu.EditEndpoint);           
                Endpoint ?e = FindEndpoint();
                Console.WriteLine(e.Info);
                string newState = ForceUserInput(this._menu.NewState);                
                if (Confirm()){
                    this._logicLayer.Edit(e.SerialNumber, newState);
                    Pause("Endpoint state edited sucessfuly.");
                }else
                    Pause("Operation aborted.");                                
            }
            catch (EndpointNotFoundError){
                Pause("Not found an Endpoint with specified Serial Number.");
            }      
        }

        public void ListEndpoints(){
            Console.Clear();
            Console.WriteLine(this._menu.ListEndpoints);
            Pause(this._logicLayer.List());
        }

        private Endpoint FindEndpoint(){
            string serialNumber = ForceUserInput(this._menu.RequestSerialNumber);
            return this._logicLayer.Search(serialNumber);
        }

        public void InsertEndpoint(){
            Console.Clear();
            Console.WriteLine(this._menu.InsertEndpoint);
            string serialNumber = ForceUserInput(this._menu.RequestSerialNumber);
            string model = ForceUserInput(this._menu.RequestModel);
            int number = ForceUserInputInt(this._menu.RequestModelNumber);
            string firmwareVersion = ForceUserInput(this._menu.RequestFirmwareVersion);
            string state = ForceUserInput(this._menu.RequestState);
            this._logicLayer.Insert(serialNumber, model, number, firmwareVersion, state);
        }

        private string ForceUserInput(string ?message){
            string ?s = "";
            while (string.IsNullOrEmpty(s)){
                if (!string.IsNullOrEmpty(message))
                    Console.Write(message);
                s = Console.ReadLine();
                if (string.IsNullOrEmpty(s))
                    Console.WriteLine("\nPlease enter an valid value.");
            }
            return s;
        }

        private int ForceUserInputInt(string ?message){
            string ?s = "";
            int val = -1;
            while (!int.TryParse(s, out val)){
                if (!string.IsNullOrEmpty(message))
                    Console.Write(message);
                s = Console.ReadLine();
                if (!int.TryParse(s, out val))
                    Console.WriteLine("\nPlease enter an valid Integer value.");
            }
            return val;
        }

        private bool Confirm(){
            string userInput = ".";
            string[] options = {"yes","y","no","n"};
            while (!options.Contains(userInput)){
                userInput = ForceUserInput(this._menu.Confirm);
                userInput = userInput.ToLower();
            }
            return ((userInput == "yes") || (userInput == "y"));
        }

        private void InvalidOption(string ?o){
            if (!string.IsNullOrEmpty(o))
                Console.WriteLine($"Invalid Option '{o}'.\nPress [Enter] to continue.");
            else
                Console.WriteLine("Invalid Option.\nPress [Enter] to continue.");
            Console.ReadLine();
        }

        private void Pause(string ?message){
            if (!string.IsNullOrEmpty(message))
                Console.WriteLine(message);
            Console.Write("Press [Enter] to continue.");
            Console.ReadLine();
        }

        private void exit(){
            Console.Clear();
            Console.WriteLine("Bye!");
            Environment.Exit(0);
        }
    }
}            