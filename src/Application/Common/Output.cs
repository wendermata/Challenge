namespace Application.Common
{
    public class Output
    {
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public List<string> Messages { get; set; } = new List<string>();
        public bool IsValid => ErrorMessages?.Count == 0;
    }
}
