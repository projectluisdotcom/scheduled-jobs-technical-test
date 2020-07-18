using Domain;

namespace Api
{
    public class JobsStateResponse
    {
        public State[] States { get; set; }

        public class State
        {
            public string Id { get; set; }
            public JobState JobState { get; set; }
        }
    }
}