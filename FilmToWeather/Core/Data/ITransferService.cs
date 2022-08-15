namespace Core.Data
{
    public interface ITransferService
    {
        Task<Guid> ValidityCheckedCityForUser(string city);
    }
}
