namespace endpointManager
{
    [Serializable]
    public class EndpointNotFoundError : Exception{
        public EndpointNotFoundError(){}
        public EndpointNotFoundError(string serialNumber)
            : base($"Invalid Endpoint Serial Number: {serialNumber}") {}                
    }

    [Serializable]
    public class SerialNumberAlreadyExists : Exception{
        public SerialNumberAlreadyExists(){}
        public SerialNumberAlreadyExists(string serialNumber)
            : base($"The serial number \"{serialNumber}\" is already registered."){}
    }

    [Serializable]
    public class InvalidModel : Exception{
        public InvalidModel(){}
        public InvalidModel(string model)
            : base($"The model \"{model}\" is not an valid Model."){}
    }

    [Serializable]
    public class InvalidIDModel : Exception{
        public InvalidIDModel(){}
        public InvalidIDModel(int model)
            : base($"The ID model \"{model}\" is not an valid ID Model."){}
    }

    [Serializable]
    public class InvalidSwitchStateID: Exception{
        public InvalidSwitchStateID(){}
        public InvalidSwitchStateID(int state)
            : base($"The Switch State \"{state}\" is not an valid State."){}
    }

    [Serializable]
    public class InvalidSwitchState: Exception{
        public InvalidSwitchState(){}
        public InvalidSwitchState(string state)
            : base($"The Switch State \"{state}\" is not an valid State."){}
    }
}