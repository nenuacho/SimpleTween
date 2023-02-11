using Starbugs.SimpleTween.Core.Interfaces;
using UnityEngine;

namespace SimpleTween.DefaultTweens
{
    public class RotateTween : ITween
    {
        private Transform _transform;
        private Vector3 _targetEuler;
        private Vector3 _startEuler;

        public void WithParameters(Transform transform, Vector3 euler)
        {
            _targetEuler = euler;
            _startEuler = transform.position;
            _transform = transform;
        }

        public void UpdatePlaybackTime(float time)
        {
            _transform.eulerAngles = Vector3.Lerp(_startEuler, _targetEuler, time);
        }

        public bool IsActive { get; set; }
    }
}