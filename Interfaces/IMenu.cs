namespace endpointManager
{
    public interface IMenu
    {
        string MenuPrincipal{get;}
        string Confirm {get;}
        string RequestModel {get;}
        string RequestSerialNumber {get;}
        string InsertEndpoint {get;}
        string RequestModelNumber {get;}
        string RequestFirmwareVersion {get;}
        string RequestState {get;}
        string ListEndpoints {get;}
        string DeleteEndpoint {get;}
        string EditEndpoint {get;}
        string NewState {get;}
    }
}