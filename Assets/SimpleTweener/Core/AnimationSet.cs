using System;

namespace SimpleTweener.Core
{
    public struct TweenSet
    {
        private ITween[] _tweens;
        private TweenCommonData _data;

        public bool IsFinished => _data.IsFinished;

        private TweenSet(TweenCommonSettings settings)
        {
            _data = TweenCommonData.FromSettings(settings);
            _tweens = null;
        }

        public static TweenSet WithSettings(TweenCommonSettings settings) => new(settings);

        public TweenSet WithAnimation(ITween animation)
        {
            if (_tweens == null)
            {
                _tweens = new ITween[1];
                _tweens[0] = animation;
                return this;
            }

            var newArray = new ITween[_tweens.Length + 1];
            Array.Copy(_tweens, newArray, _tweens.Length);
            newArray[_tweens.Length] = animation;
            _tweens = newArray;
            return this;
        }

        public void Update(float deltaTime)
        {
            _data.Calculate(deltaTime);

            if (!_data.CanAnimate)
            {
                return;
            }

            foreach (var animation in _tweens)
            {
                animation.UpdatePlaybackTime(_data.PlaybackTime);
            }
        }
    }
}