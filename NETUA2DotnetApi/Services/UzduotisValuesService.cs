namespace NETUA2DotnetApi.Services
{
    public interface IUzduotisValuesService
    {
        List<string> Values { get; set; }
    }
    public class UzduotisValuesService: IUzduotisValuesService
    {
        public List<string> Values { get; set; } = new List<string> { "value1", "value2" };
    }
}
