namespace SimpleTweener.Core
{
    internal struct TweenCommonData
    {
        private float _duration;
        private float _step;
        private float _delay;
        private float _lifeTime;
        
        public bool IsFinished;
        public float PlaybackTime;
        public bool CanAnimate;

        public static TweenCommonData FromSettings(TweenCommonSettings settings) =>
            new()
            {
                _lifeTime = 0,
                _duration = settings.Duration,
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

            if (_lifeTime >= _duration)
            {
                IsFinished = true;
            }
        }
    }
}