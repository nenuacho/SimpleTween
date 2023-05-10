using System;
using System.Collections.Generic;
using Starbugs.SimpleTween.Scripts.Core.Interfaces;

namespace Starbugs.SimpleTween.Scripts.Core.TweenGroups
{
    public class TweenGroup
    {
        private readonly Dictionary<Type, ITween> _tweenCache;
        private TweenGroupData _data;
        private Action _callback;

        public bool IsRunning;

        public TweenGroup()
        {
            _tweenCache = new Dictionary<Type, ITween>();
        }

        private void ResetActives()
        {
            foreach (var tweenValue in _tweenCache.Values)
            {
                tweenValue.IsActive = false;
            }
        }

        private TweenGroup WithData(ref TweenGroupData data)
        {
            ResetActives();
            _data = data;
            return this;
        }

        public TweenGroup WithSettings(TweenSettings settings)
        {
            var data = TweenGroupData.Map(settings);
            return WithData(ref data);
        }

        public TweenGroup WithCallback(Action callback)
        {
            _callback = callback;
            return this;
        }

        public T AddTween<T>() where T : ITween, new()
        {
            var type = typeof(T);
            if(!_tweenCache.TryGetValue(type, out var tween))
            {
                tween = new T();
                _tweenCache.Add(type, tween);
            }

            tween.IsActive = true;
            IsRunning = true;
            return (T)tween;

        }

        public void Update(float deltaTime)
        {
            _data.Calculate(deltaTime);

            if (!_data.CanAnimate)
            {
                return;
            }

            foreach (var tween in _tweenCache.Values)
            {
                if (tween.IsActive)
                {
                    tween.UpdatePlaybackTime(_data.PlaybackTime);
                }
            }

            if (_data.PlaybackTime >= 1)
            {
                IsRunning = false;
                _callback?.Invoke();
            }
        }
    }
}