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

        /// <summary>
        /// método que contém o menu principal e cuidará da seleção de "modulos" para usuário
        /// </summary>
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

        /// <summary>
        /// método que busca um endpoint cadastrado e imprime suas informações
        /// </summary>
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

        /// <summary>
        /// método para cuidar do procedimento de exclusão de um endpoint, 
        /// realiza a pesquisa do Endpoint a ser excluído pelo Número de Série
        /// mostra as informações deste Endpoint na tela
        /// e solicita confirmação do usuário, caso não confirmado aborta a operação
        /// </summary>
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

        /// <summary>
        /// método para cuidar do procedimento de edição de um endpoint, 
        /// realiza a pesquisa do Endpoint a ser editado pelo Número de Série
        /// mostra as informações deste Endpoint na tela
        /// solicita o novo estado que será atribuido ao Endpoint
        /// e solicita confirmação do usuário, caso não confirmado aborta a operação
        /// </summary>
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

        /// <summary>
        /// método para listar todos os Endpoints cadastrados e suas respectivas informações
        /// </summary>
        public void ListEndpoints(){
            Console.Clear();
            Console.WriteLine(this._menu.ListEndpoints);
            Pause(this._logicLayer.List());
        }

        /// <summary>
        /// força o usuário a realizar o input de um número inteiro, mostrando a mensagem customizada de solicitação quando informada
        /// </summary>
        /// <params name="message">mensagem customizada a ser informada a cada solicitação</params>
        private Endpoint FindEndpoint(){
            string serialNumber = ForceUserInput(this._menu.RequestSerialNumber);
            return this._logicLayer.Search(serialNumber);
        }

        /// <summary>
        /// método para cuidar do procedimento de inserção de um novo Endpoint
        /// solicita todas as informações ao usuário, e realiza inserção do Endpoint
        /// </summary>
        /// <exception cref="SerialNumberAlreadyExists">Erro gerado ao tentar cadastrar um Endpoint em que o Serial Number já esteja cadastrado para outro Endpoint</exception>
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

        /// <summary>
        /// força o usuário a realizar o input, mostrando a mensagem customizada de solicitação quando informada
        /// </summary>
        /// <params name="message">mensagem customizada a ser informada a cada solicitação</params>
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

        /// <summary>
        /// força o usuário a realizar o input de um número inteiro, mostrando a mensagem customizada de solicitação quando informada
        /// </summary>
        /// <params name="message">mensagem customizada a ser informada a cada solicitação</params>
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

        /// <summary>
        /// método para realizar confirmação de operações com usuário
        /// </summary>
        /// <returns>
        ///     true: caso o usuário confirme com ([y]es)
        ///     false: caso o usuário confirme com ([n]o)
        /// </returns>
        private bool Confirm(){
            string userInput = ".";
            string[] options = {"yes","y","no","n"};
            while (!options.Contains(userInput)){
                userInput = ForceUserInput(this._menu.Confirm);
                userInput = userInput.ToLower();
            }
            return ((userInput == "yes") || (userInput == "y"));
        }

        /// <summary>
        /// método para tratar opção inválida
        /// </summary>
        private void InvalidOption(string ?o){
            if (!string.IsNullOrEmpty(o))
                Pause($"Invalid Option '{o}'.\nPress [Enter] to continue.");
            else
                Pause("Invalid Option.\nPress [Enter] to continue.");
        }

        /// <summary>
        /// método para "forçar" uma pausa no sistema com mensagens customizadas
        /// </summary>
        /// <param name="message">Mensagem customizada a ser impressa</param>
        private void Pause(string ?message){
            if (!string.IsNullOrEmpty(message))
                Console.WriteLine(message);
            Console.Write("Press [Enter] to continue.");
            Console.ReadLine();
        }

        /// <summary>
        /// realiza finalização da aplicação
        /// </summary>
        private void exit(){
            Console.Clear();
            Console.WriteLine("Bye!");
            Environment.Exit(0);
        }
    }
}            