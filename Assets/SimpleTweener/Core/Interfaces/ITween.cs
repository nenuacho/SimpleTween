namespace SimpleTweener.Core
{
    public interface ITween
    {
        void UpdatePlaybackTime(float time);

        bool IsActive { get; set; }
    }
}