using System;
using Starbugs.SimpleTween.Core.Interfaces;
using Starbugs.SimpleTween.Core.TweenGroups;

namespace Starbugs.SimpleTween.Core
{
    public class TweenProcessor
    {
        private TweenGroup[] _tweenGroups;
        private bool[] _activeMask;

        private const int DefaultStartCapacity = 512;

        public TweenProcessor(int capacity)
        {
            _tweenGroups = new TweenGroup[capacity];
            _activeMask = new bool[capacity];
        }

        public TweenProcessor()
        {
            _tweenGroups = new TweenGroup[DefaultStartCapacity];
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

        public void ApplyTweenGroup(TweenGroup tGroup)
        {
            var freeIndex = GetFreeIndex();
            _tweenGroups[freeIndex] = tGroup;
            _activeMask[freeIndex] = true;
        }

        public T ApplyTween<T>(TweenSettings settings, TweenGroupPool pool = null) where T : ITween
        {
            if (pool == null)
            {
                pool = TweenGroupPool.Default;
            }

            var group = pool.GetTweenGroup(settings);
            
            ApplyTweenGroup(group);
            
            var tween = group.AddTween<T>();

            return tween;
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
                if (tweenSet is not {IsFinished: false})
                {
                    _activeMask[i] = false;
                    continue;
                }

                tweenSet.Update(deltaTime);
            }
        }
    };
}