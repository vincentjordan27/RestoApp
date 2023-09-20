namespace RestoApp.Application.Auth
{
    public interface IRestoAuthRepository
    {
        Task<string?> InsertResto(Domain.Entities.Resto resto);
    }
}
