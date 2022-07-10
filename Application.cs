namespace endpointManager
{
    public class Application : IApplication
    {
        // private declarations
        private List<Endpoint> _endpoints;
        private Menu _menu;

        // implementation
        
        public Application(List<Endpoint> endpoints, Menu menu){
            this._endpoints = endpoints;            
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
                string userInput = ".";
                string[] options = {"yes","y","no","n"};
                while (!options.Contains(userInput)){
                    userInput = ForceUserInput(this._menu.Confirm);
                    userInput = userInput.ToLower();
                }
                if (("yes" == userInput) || ("y" == userInput)){
                    this._endpoints.Remove(e);
                    Pause("Endpoint remove sucessfuly.");
                }else if (("no" == userInput) || ("n" == userInput)){
                    Pause("Operation aborted.");
                }                
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
                string userInput = ".";
                string[] options = {"yes","y","no","n"};
                while (!options.Contains(userInput)){
                    userInput = ForceUserInput(this._menu.Confirm);
                    userInput = userInput.ToLower();
                }
                if (("yes" == userInput) || ("y" == userInput)){
                    e.State = newState;
                    Pause("Endpoint state edited sucessfuly.");
                }else if (("no" == userInput) || ("n" == userInput)){
                    Pause("Operation aborted.");
                }                
            }
            catch (EndpointNotFoundError){
                Pause("Not found an Endpoint with specified Serial Number.");
            }      
        }

        public void ListEndpoints(){
            Console.Clear();
            Console.WriteLine(this._menu.ListEndpoints);
            foreach (Endpoint item in this._endpoints)
            {
                Console.WriteLine(item.Info);
            }
            Pause("");
        }

        private Endpoint FindEndpoint(){
            string serialNumber = ForceUserInput(this._menu.RequestSerialNumber);
            return FindEndpoint(serialNumber);
        }

        private Endpoint FindEndpoint(string serial){
            Endpoint ?e = this._endpoints.FirstOrDefault(i => i.SerialNumber == serial);
            if (null == e)
                throw new EndpointNotFoundError(serial);
            return e;
        }

        public void InsertEndpoint(){
            Console.Clear();
            Console.WriteLine(this._menu.InsertEndpoint);

            string serialNumber = ForceUserInput(this._menu.RequestSerialNumber);
            string model = ForceUserInput(this._menu.RequestModel);
            int number = ForceUserInputInt(this._menu.RequestModelNumber);
            string firmwareVersion = ForceUserInput(this._menu.RequestFirmwareVersion);
            string state = ForceUserInput(this._menu.RequestState);

            bool existsSerialNumber = false;
            try{
                FindEndpoint(serialNumber);
                existsSerialNumber = true;
            }
            catch (System.Exception){}                
            finally{
                if (!existsSerialNumber){
                    Endpoint newEndpoint = new Endpoint(serialNumber, model, number, firmwareVersion, state);
                    this._endpoints.Add(newEndpoint);
                }else
                {
                    throw new SerialNumberAlreadyExists(serialNumber);
                }
            }            
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