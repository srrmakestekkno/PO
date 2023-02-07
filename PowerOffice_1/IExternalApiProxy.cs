using PowerOffice_1.DataObjects;

namespace PowerOffice_1
{

    public interface IExternalApiProxy
    {
        Task<Data?> GetAsync(string orgno);
    }
}
