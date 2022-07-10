namespace endpointManager
{
    /// <summary>
    /// classe com strings padronizadas e formatadas para utilização na Application
    /// </summary>
    public class Menu : IMenu
    {
        public string MenuPrincipal { 
            get {
                return "[ Endpoint Manager ]\n\n"
                    + "Welcome! What do you need?\n"
                    + " 1 - Insert new Endpoint\n"
                    + " 2 - Edit an Endpoint\n"
                    + " 3 - Delete an Endpoint\n"
                    + " 4 - List all Endpoints\n"
                    + " 5 - Search an Endpoint\n"
                    + " 6 - Exit\n\n"
                    + "Option: ";
            }
        }
        public string Confirm {
            get{
                return "[WARNING] Are you sure you want complete this operation? ([Y]es/[N]o)\n";
            }
        }
        public string RequestModel{
            get{
                return "Enter the Model of Endpoint: ";
            }
        }
        public string RequestSerialNumber{
            get{
                return "Enter the Serial Number of Endpoint: ";
            }
        }
        public string InsertEndpoint {
            get{
                return "[INSERT NEW ENDPOINT]\n\nPlease enter all fields.";
            }
        }
        public string RequestModelNumber{
            get{
                return "Enter the Meter Number: ";
            }
        }
        public string RequestFirmwareVersion{
            get{
                return "Enter the Firmware Version: ";
            }
        }
        public string RequestState{
            get{
                return "Enter the State of Meter: ";
            }
        }
        public string ListEndpoints {
            get{
                return "[LIST ALL ENDPOINTS]\n\n";
            }
        }
        public string DeleteEndpoint {
            get{
                return "[DELETE ENDPOINT]\n\n";
            }
        }
        public string EditEndpoint {
            get{
                return "[EDIT ENDPOINT]\n\n";
            }
        }
        public string NewState{
            get{
                return "Alter state to: ";
            }
        }
        public Menu(){}
    }
}