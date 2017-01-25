namespace Service.Bank.Proxy.ServicesRegister
{
    /// <summary>
    /// Register with transfer service address for each known bank id. 
    /// </summary>
    public interface ITransferServicesRegister
    {
        /// <summary>
        /// Global transfer service login 
        /// </summary>
        string Login { get; }

        /// <summary>
        /// Global transfer service password 
        /// </summary>
        string Password { get; }

        /// <summary>
        /// Gets address of given bank transfer service 
        /// </summary>
        /// <returns> Transfer service address </returns>
        string GetTransferServiceAddress(string bankId);
    }
}