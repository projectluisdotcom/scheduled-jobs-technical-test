using System;
using System.Threading.Tasks;

namespace Api
{
    public class FastFakeDataProcessor : IDataProcessor
    {
        public async Task<string> Process(string data)
        {
            var random = new Random().Next(40, 60);
            if (random > 55)
            {
                throw new Exception($"To high {random}");
            } 
            
            await Task.Delay(random);

            return $"data-{random}";
        }
    }
}