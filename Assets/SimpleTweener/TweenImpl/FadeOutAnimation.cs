using SimpleTweener.Core;
using UnityEngine;

namespace SimpleTweener.TweenImpl
{
    public readonly struct FadeOutAnimation : ITween
    {
        private readonly SpriteRenderer _renderer;
        private readonly float _startAlpha;
        private readonly float _endAlpha;

        public FadeOutAnimation(SpriteRenderer renderer, float startAlpha, float endAlpha)
        {
            _startAlpha = startAlpha;
            _endAlpha = endAlpha;
            _renderer = renderer;
        }

        public void UpdatePlaybackTime(float time)
        {
            SetAlpha(Mathf.Lerp(_startAlpha, _endAlpha, time));
        }

        private void SetAlpha(float value)
        {
            var tempColor = _renderer.color;
            tempColor.a = value;
            _renderer.color = tempColor;
        }
    }
}