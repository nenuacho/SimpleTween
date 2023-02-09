using SimpleTweener.Core;
using UnityEngine;

namespace SimpleTweener.TweenImpl
{
    public readonly struct MoveAnimation : ITween
    {
        private readonly Transform _transform;
        private readonly Vector3 _targetPosition;
        private readonly Vector3 _startPosition;

        public MoveAnimation(Transform transform, Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
            _startPosition = transform.position;
            _transform = transform;
        }

        public void UpdatePlaybackTime(float time)
        {
            _transform.position = Vector3.Lerp(_startPosition, _targetPosition, time);
        }
    }
}