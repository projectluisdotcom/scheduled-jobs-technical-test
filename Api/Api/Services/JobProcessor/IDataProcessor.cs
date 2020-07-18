using System.Threading.Tasks;

namespace Api
{
    public interface IDataProcessor
    {
        Task<string> Process(string data);
    }
}