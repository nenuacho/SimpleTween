using SimpleTweener.Core;
using UnityEngine;

namespace SimpleTweener.TweenImpl
{
    public readonly struct RotateAnimation : ITween
    {
        private readonly Transform _transform;
        private readonly Vector3 _targetEuler;
        private readonly Vector3 _startEuler;

        public RotateAnimation(Transform transform, Vector3 euler)
        {
            _targetEuler = euler;
            _startEuler = transform.position;
            _transform = transform;
        }

        public void UpdatePlaybackTime(float time)
        {
            _transform.eulerAngles = Vector3.Lerp(_startEuler, _targetEuler, time);
        }
    }
}