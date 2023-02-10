using SimpleTweener.Core;
using UnityEngine;

namespace SimpleTweener.TweenImpl
{
    public class MoveToPointTween : ITween
    {
        private Transform _transform;
        private Vector3 _targetPosition;
        private Vector3 _startPosition;

        public void UpdatePlaybackTime(float time)
        {
            _transform.position = Vector3.Lerp(_startPosition, _targetPosition, time);
        }

        public bool IsActive { get; set; }

        public void WithParameters(Transform transform, Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
            _startPosition = transform.position;
            _transform = transform;
        }
    }
}