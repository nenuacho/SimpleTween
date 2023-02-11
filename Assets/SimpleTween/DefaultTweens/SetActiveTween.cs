using Starbugs.SimpleTween.Core.Interfaces;
using UnityEngine;

namespace SimpleTween.DefaultTweens
{
    public class SetActiveTween : ITween
    {
        private GameObject _go;
        private Vector3 _targetPosition;
        private Vector3 _startPosition;

        public void UpdatePlaybackTime(float time)
        {
            _go.SetActive(time is > 0 and < 1);
        }

        public bool IsActive { get; set; }

        public void WithParameters(GameObject go)
        {
            _go = go;
        }
    }
}