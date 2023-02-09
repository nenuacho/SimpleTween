using SimpleTweener.Core;
using UnityEngine;

namespace SimpleTweener.Example
{
    public class TweenRunner : MonoBehaviour
    {
        private TweenSystem _tweenSystem;

        private void Awake()
        {
            _tweenSystem = new TweenSystem();
        }

        private void Update()
        {
            _tweenSystem.Update(Time.deltaTime);
        }

        public void Do(TweenSet animationSet)
        {
            _tweenSystem.Add(animationSet);
        }
    }
}