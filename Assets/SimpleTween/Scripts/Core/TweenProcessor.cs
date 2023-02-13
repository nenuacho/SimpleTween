using System;
using Starbugs.SimpleTween.Scripts.Core.Interfaces;
using Starbugs.SimpleTween.Scripts.Core.TweenGroups;

namespace Starbugs.SimpleTween.Scripts.Core
{
    public class TweenProcessor
    {
        private TweenGroup[] _tweenGroups;
        private bool[] _activeMask;

        private const int DefaultStartCapacity = 1024 * 8;

        public TweenProcessor()
        {
            _tweenGroups = new TweenGroup[DefaultStartCapacity];
            for (int i = 0; i < _tweenGroups.Length; i++)
            {
                _tweenGroups[i] = new TweenGroup();
            }

            _activeMask = new bool[DefaultStartCapacity];
        }

        private void Extend()
        {
            var newGroups = new TweenGroup[_tweenGroups.Length * 2];
            Array.Copy(_tweenGroups, newGroups, _tweenGroups.Length);
            _tweenGroups = newGroups;

            var newMask = new bool[_activeMask.Length * 2];
            Array.Copy(_activeMask, newMask, _activeMask.Length);
            _activeMask = newMask;
        }

        public T ApplyTween<T>(TweenSettings settings) where T : ITween
        {
            var tweenGroup = ApplyTweenGroup().WithSettings(settings);
            var tween = tweenGroup.AddTween<T>();
            return (T)tween;
        }

        public TweenGroup ApplyTweenGroup()
        {
            var freeIndex = GetFreeIndex();
            var tGroup = _tweenGroups[freeIndex] ??= new TweenGroup();
            _activeMask[freeIndex] = true;
            return tGroup;
        }

        private int GetFreeIndex()
        {
            for (int i = 0; i < _tweenGroups.Length; i++)
            {
                if (!_activeMask[i])
                {
                    return i;
                }
            }

            var nextFreeIndex = _tweenGroups.Length;

            Extend();

            return nextFreeIndex;
        }

        public void Update(float deltaTime)
        {
            for (int i = 0; i < _tweenGroups.Length; i++)
            {
                if (!_activeMask[i])
                {
                    continue;
                }

                var tweenSet = _tweenGroups[i];
                if (tweenSet is not {IsRunning: true})
                {
                    _activeMask[i] = false;
                    continue;
                }

                tweenSet.Update(deltaTime);
            }
        }
    };
}