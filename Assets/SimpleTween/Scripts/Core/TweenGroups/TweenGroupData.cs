namespace Starbugs.SimpleTween.Scripts.Core.TweenGroups
{
    internal struct TweenGroupData
    {
        private float _step;
        private float _delay;
        private float _lifeTime;
        public float PlaybackTime;
        public bool CanAnimate;

        public static TweenGroupData Map(TweenSettings settings) =>
            new()
            {
                _lifeTime = 0,
                _step = 1 / settings.Duration,
                _delay = settings.Delay
            };

        public void Calculate(float deltaTime)
        {
            if (_delay > 0)
            {
                _delay -= deltaTime;
                return;
            }

            CanAnimate = true;

            _lifeTime += deltaTime;

            PlaybackTime = _lifeTime * _step;
        }
    }
}