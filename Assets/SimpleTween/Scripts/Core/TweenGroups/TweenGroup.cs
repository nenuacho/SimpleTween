using System;
using System.Collections.Generic;
using Starbugs.SimpleTween.Scripts.Core.Interfaces;

namespace Starbugs.SimpleTween.Scripts.Core.TweenGroups
{
    public class TweenGroup
    {
        private readonly Dictionary<Type, ITween> _tweenCache;
        private TweenGroupData _data;

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

        internal TweenGroup WithData(ref TweenGroupData data)
        {
            ResetActives();
            _data = data;
            return this;
        }

        public T AddTween<T>() where T : ITween
        {
            var type = typeof(T);
            T anim;

            if (_tweenCache.ContainsKey(type))
            {
                anim = (T)_tweenCache[type];
            }
            else
            {
                var newTween = Activator.CreateInstance<T>();
                _tweenCache.Add(type, newTween);
                anim = newTween;
            }

            anim.IsActive = true;

            IsRunning = true;

            return anim;
        }

        public void Update(float deltaTime)
        {
            _data.Calculate(deltaTime);

            if (!_data.CanAnimate)
            {
                return;
            }

            foreach (var animation in _tweenCache.Values)
            {
                if (animation.IsActive)
                {
                    animation.UpdatePlaybackTime(_data.PlaybackTime);
                }
            }

            if (_data.PlaybackTime >= 1)
            {
                IsRunning = false;
            }
        }
    }
}