using System;

namespace SimpleTweener.Core
{
    public class TweenSystem
    {
        private TweenSet[] _tweenSets;
        private bool[] _activeMask;

        private const int DefaultStartCapacity = 512;

        public TweenSystem(int capacity)
        {
            _tweenSets = new TweenSet[capacity];
            _activeMask = new bool[capacity];
        }
        
        public TweenSystem()
        {
            _tweenSets = new TweenSet[DefaultStartCapacity];
            _activeMask = new bool[DefaultStartCapacity];
        }

        private void Extend()
        {
            var newAnimations = new TweenSet[_tweenSets.Length * 2];
            Array.Copy(_tweenSets, newAnimations, _tweenSets.Length);
            _tweenSets = newAnimations;

            var newMask = new bool[_activeMask.Length * 2];
            Array.Copy(_activeMask, newMask, _activeMask.Length);
            _activeMask = newMask;
        }

        public void Add(TweenSet animation)
        {
            var freeIndex = GetFreeIndex();
            _tweenSets[freeIndex] = animation;
            _activeMask[freeIndex] = true;
        }

        private int GetFreeIndex()
        {
            for (int i = 0; i < _tweenSets.Length; i++)
            {
                if (!_activeMask[i])
                {
                    return i;
                }
            }

            var nextFreeIndex = _tweenSets.Length;
            Extend();

            return nextFreeIndex;
        }

        public void Update(float deltaTime)
        {
            for (int i = 0; i < _tweenSets.Length; i++)
            {
                if (!_activeMask[i])
                {
                    continue;
                }

                ref var tweenSet = ref _tweenSets[i];
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