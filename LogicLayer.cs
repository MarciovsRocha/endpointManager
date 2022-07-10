namespace endpointManager
{
    public class LogicLayer : ILogicLayer
    {
        // private declarations
        private List<Endpoint> _endpoints;
        // implementation
        public LogicLayer(){
            this._endpoints = new List<Endpoint>{};
        }

        /// <summary>Inserir um novo Endpoint</summary>
        /// <param name="serialNumber">Número de série do Endpoit</param>
        /// <param name="model">Modelo do Endpoint</param>
        /// <param name="number">Número do Medidor</param>
        /// <param name="firmwareVersion">Versão do firmware do Endpoint</param>
        /// <param name="state">Estado do Switch do Endpoint</param>
        /// <exception cref="SerialNumberAlreadyExists">Erro gerado ao tentar cadastrar um Endpoint em que o Serial Number já esteja cadastrado para outro Endpoint</exception>
        public void Insert(string serialNumber, string model, int number, string firmwareVersion, string state){
            /// <summary>Verificar se existe um Endpoint cadastrado com o Número de Série informado</summary>
            /// <param name="serialNumber">Número de série do Endpoit</param>
            /// <returns>
            ///    'true': se existir um Endpoint com o Número de série informado
            ///    'false': se não existir um Endpoint com o número de série informado
            /// </returns>
            Func<string, bool> ExistsSerialNumber = serial => {
                try{
                    Search(serial);
                    return true;
                }catch(EndpointNotFoundError){ return false; }
            };
            if (!ExistsSerialNumber(serialNumber))
                this._endpoints.Add(new Endpoint(serialNumber, model, number, firmwareVersion, state));
            else
                throw new SerialNumberAlreadyExists(serialNumber);
            
        }

        /// <summary>Editar um Endpoint Cadastrado, só é possível alterar o 'ESTADO' do Endpoint</summary>
        /// <param name="serialNumber">Número de série do Endpoit</param>
        /// <param name="state">Estado do Switch do Endpoint</param>
        /// <exception cref="EndpointNotFoundError">Erro gerado ao tentar selecionar um Endpoint via Serial Number e não foi encontrado um resultado</exception>
        public void Edit(string serialNumber, string state){
            Endpoint ?e = Search(serialNumber);
            e.State = state;
        }

        /// <summary>Editar um Endpoint Cadastrado, só é possível alterar o 'ESTADO' do Endpoint</summary>
        /// <param name="serialNumber">Número de série do Endpoit</param>
        /// <param name="state">Estado do Switch do Endpoint</param>
        /// <exception cref="EndpointNotFoundError">Erro gerado ao tentar selecionar um Endpoint via Serial Number e não foi encontrado um resultado</exception>
        public void Delete(string serialNumber){
            Endpoint ?e = Search(serialNumber);
            this._endpoints.Remove(e);
        }

        /// <summaery>Listar todos os Endpoints Cadastrados e suas informações</summary>
        /// <returns>
        ///    string s: texto com todos os endpoints e suas informações em forma de lista 
        /// </returns>
        public string List(){
            string s = "";
            foreach (Endpoint item in this._endpoints){
                s+=item.Info;
            }
            return s;
        }

        /// <summaery>Procurar um Endpoint Cadastrado</summary>
        /// <param name="serialNumber">Número de série do Endpoit</param>
        /// <returns>
        ///     Endpoint: retorna o Endpoint que possui o Número de Série informado
        /// </returns>
        /// <exception cref="EndpointNotFoundError">Erro gerado ao tentar selecionar um Endpoint via Serial Number e não foi encontrado um resultado</exception>
        public Endpoint Search(string serialNumber){
            Endpoint ?e = this._endpoints.FirstOrDefault(ep => ep.SerialNumber == serialNumber);
            if (null == e)
                throw new EndpointNotFoundError(serialNumber);
            return e;
        }
    }
}