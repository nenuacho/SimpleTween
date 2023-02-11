namespace Starbugs.SimpleTween.Core
{
    public struct TweenSettings
    {
        public float Duration { get; }
        public float Delay { get; private set; }

        public TweenSettings(float duration)
        {
            Duration = duration;
            Delay = 0;
        }

        public TweenSettings WithDelay(float delay)
        {
            Delay = delay;
            return this;
        }
    }
}