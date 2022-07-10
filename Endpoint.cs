namespace endpointManager
{
    public class Endpoint : IEndpoint
    {
        // private declarations 
        private string _serialNumber;
        private int _number;
        private string _firmwareVersion;
        private int _state;
        private int _model;
        // public declarations 
        public string SerialNumber => _serialNumber;
        public string Model {
            get{
                switch (_model)
                {
                    case 16: 
                        return "NSX1P2W";
                    case 17:
                        return "NSX1P3W";
                    case 18:
                        return "NSX2P3W";
                    case 19:
                        return "NSX3P4W";
                    default:
                        throw new InvalidIDModel(_model);
                }
            }
        }        
        public string State {
            get {
                switch (_state)
                {
                    case 0:
                        return "Disconnected";
                    case 1:
                        return "Connected";
                    case 2:
                        return "Armed";
                    default:
                        throw new InvalidSwitchStateID(_state);
                }
            }
            set {
                switch (value.ToLower())
                {
                    case "disconnected":
                        this._state = 0;
                        break;
                    case "connected":
                        this._state = 1;
                        break;
                    case "armed":
                        this._state = 2;
                        break;
                    default:
                        throw new InvalidSwitchState(value);
                }
            }
        }
        public string Info { 
            get => "---\nEndpoint Info\n"
                +$"  * Model: {this.Model}\n"
                +$"  * Serial Number: {this._serialNumber}\n"
                +$"  * Number: {this._number}\n"
                +$"  * Firmware Version: {this._firmwareVersion}\n"
                +$"  * State: {this.State}\n"; 
        }

        // implementation 

        public Endpoint(string serialNumber, string model, int number, string firmwareVersion, string state){
            this._serialNumber = serialNumber;            
            this._number = number;
            this._firmwareVersion = firmwareVersion;
            this.State = state;
            switch (model)
                {
                    case "NSX1P2W": 
                        this._model = 16;
                        break;
                    case "NSX1P3W":
                        this._model = 17;
                        break;
                    case "NSX2P3W":
                        this._model = 18;
                        break;
                    case "NSX3P4W":
                        this._model = 19;
                        break;
                    default:
                        throw new InvalidModel(model);
                }
        }
    }
}