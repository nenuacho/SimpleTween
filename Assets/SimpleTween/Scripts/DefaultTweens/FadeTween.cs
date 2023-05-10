using Starbugs.SimpleTween.Scripts.Core.Interfaces;
using UnityEngine;

namespace Starbugs.SimpleTween.Scripts.DefaultTweens
{
    public class FadeTween : ITween
    {
        private SpriteRenderer _renderer;
        private float _startAlpha;
        private float _endAlpha;
        
        public void WithParameters(SpriteRenderer renderer, float startAlpha, float endAlpha)
        {
            _startAlpha = startAlpha;
            _endAlpha = endAlpha;
            _renderer = renderer;
        }

        public void UpdatePlaybackTime(float time)
        {
            SetAlpha(Mathf.Lerp(_startAlpha, _endAlpha, time));
        }

        public bool IsActive { get; set; }

        private void SetAlpha(float value)
        {
            var tempColor = _renderer.color;
            tempColor.a = value;
            _renderer.color = tempColor;
        }
    }
    
}