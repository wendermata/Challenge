namespace Application.Common
{
    public class Output
    {
        public List<string> ErrorMessages { get; set; }
        public List<string> Messages { get; set; }
        public bool IsValid => ErrorMessages.Count > 0;
    }
}
