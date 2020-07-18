using System;
using System.Threading.Tasks;

namespace Api
{
    public class SlowFakeDataProcessor : IDataProcessor
    {
        public async Task<string> Process(string data)
        {
            var random = new Random().Next(400, 600);
            if (random < 420)
            {
                throw new Exception($"Too low {random}");
            } 
            
            await Task.Delay(random);

            return $"data-{random}";
        }
    }
}