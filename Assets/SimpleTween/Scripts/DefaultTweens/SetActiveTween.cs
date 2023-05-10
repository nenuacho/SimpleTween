using Starbugs.SimpleTween.Scripts.Core.Interfaces;
using UnityEngine;

namespace Starbugs.SimpleTween.Scripts.DefaultTweens
{
    public class SetActiveTween : ITween
    {
        private GameObject _go;
        private Vector3 _targetPosition;
        private Vector3 _startPosition;
        private bool _needToSet;

        public void UpdatePlaybackTime(float time)
        {
            if (!_needToSet)
            {
                return;
            }

            _go.SetActive(true);
            _needToSet = false;
        }

        public bool IsActive { get; set; }

        public void WithParameters(GameObject go)
        {
            _go = go;
            _needToSet = true;
        }
    }
}