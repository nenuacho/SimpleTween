namespace Starbugs.SimpleTween.Scripts.Core.Interfaces
{
    public interface ITween
    {
        void UpdatePlaybackTime(float time);

        bool IsActive { get; set; }
    }
}