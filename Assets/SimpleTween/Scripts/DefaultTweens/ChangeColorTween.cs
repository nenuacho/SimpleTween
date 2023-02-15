using System;
using Starbugs.SimpleTween.Scripts.Core.Interfaces;
using UnityEngine;

namespace Starbugs.SimpleTween.Scripts.DefaultTweens
{
    public class ChangeColorTween : ITween
    {
        private SpriteRenderer _renderer;

        private Color _startColor;
        private Color _endColor;

        public void WithParameters(SpriteRenderer renderer, Color startColor, Color endColor)
        {
            _renderer = renderer;
            _startColor = startColor;
            _endColor = endColor;
        }

        public void UpdatePlaybackTime(float time)
        {
            _renderer.color = Color.Lerp(_startColor, _endColor, time);
        }

        public bool IsActive { get; set; }
    }

    public class CameraColorTween : ITween
    {
        private Camera _camera;

        private Color _startColor;
        private Color _endColor;

        public void WithParameters(Camera camera, Color startColor, Color endColor)
        {
            _camera = camera;
            _startColor = startColor;
            _endColor = endColor;
        }

        public void UpdatePlaybackTime(float time)
        {
            _camera.backgroundColor = Color.Lerp(_startColor, _endColor, Mathf.Abs(Mathf.Sin(time * 360 * Mathf.Deg2Rad)));
        }

        public bool IsActive { get; set; }
    }
}