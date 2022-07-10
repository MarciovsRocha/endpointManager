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
        public void Insert(string serialNumber, string model, int number, string firmwareVersion, string state){
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
        public void Edit(string serialNumber, string state){
            Endpoint ?e = Search(serialNumber);
            e.State = state;
        }
        public void Delete(string serialNumber){
            Endpoint ?e = Search(serialNumber);
            this._endpoints.Remove(e);
        }
        public string List(){
            string s = "";
            foreach (Endpoint item in this._endpoints){
                s+=item.Info;
            }
            return s;
        }
        public Endpoint Search(string serialNumber){
            Endpoint ?e = this._endpoints.FirstOrDefault(ep => ep.SerialNumber == serialNumber);
            if (null == e)
                throw new EndpointNotFoundError(serialNumber);
            return e;
        }
    }
}