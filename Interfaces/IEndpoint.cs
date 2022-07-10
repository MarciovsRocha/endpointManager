namespace endpointManager
{
    public interface IEndpoint
    {
        string SerialNumber{ get ; }
        string Model { get ; }
        string State { get ; set ; }
    }
}