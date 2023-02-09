namespace SimpleTweener.Core
{
    public class TweenCommonSettings
    {
        public float Duration { get; private set; }
        public float Delay { get; private set; }

        public TweenCommonSettings(float duration)
        {
            Duration = duration;
        }

        public TweenCommonSettings WithDelay(float delay)
        {
            Delay = delay;
            return this;
        }
    }
}