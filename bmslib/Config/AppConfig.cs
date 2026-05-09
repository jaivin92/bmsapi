namespace bmslib.Config
{
    public class AppConfig
    {
        public ConnectionStrings ConnectionStrings { get; set; }

        public List<CorsPolicy> CorsPolicy { get; set; }
    }
}
