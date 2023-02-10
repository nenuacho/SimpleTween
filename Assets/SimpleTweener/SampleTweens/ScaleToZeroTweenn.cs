﻿using SimpleTweener.Core;
using UnityEngine;

namespace SimpleTweener.TweenImpl
{
    public class ScaleToZeroTweenn : ITween
    {
        private Transform _transform;
        private Vector3 _startScale;

        public void UpdatePlaybackTime(float time)
        {
            _transform.localScale = Vector3.Lerp(_startScale, Vector3.zero, time);
        }
        
        public void WithParameters(Transform transform)
        {
            _startScale = transform.localScale;
            _transform = transform;
        }

        public bool IsActive { get; set; }
    }
}