namespace Application.PipelineBehaviours.Contracts
{
    public interface ICacheRemoval
    {
        public List<string?> CacheKeys { get; set; }
    }
}
