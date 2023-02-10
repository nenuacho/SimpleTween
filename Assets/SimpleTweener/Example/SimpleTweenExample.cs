using System;
using System.Collections.Generic;
using SimpleTweener.Core;
using SimpleTweener.TweenImpl;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SimpleTweener.Example
{
    public class SimpleTweenExample : MonoBehaviour
    {
        [field: SerializeField] private ViewExample examplePrefab;
        [field: SerializeField] private int objectAmount = 10000;


        private readonly TweenProcessor _tweenProcessor = new();
        private readonly List<ViewExample> _viewPool = new();

        private int _currentViewIndex;

        private void Start()
        {
            for (int i = 0; i < objectAmount; i++)
            {
                var instance = Instantiate(examplePrefab);
                instance.transform.position = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(0f, 25f);
                instance.transform.eulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));

                //     instance.transform.position = new Vector2(Random.Range(-40, 40), Random.Range(-25, 25));
                _viewPool.Add(instance);
            }
        }


        private void FixedUpdate()
        {
            for (int i = 0; i < 30; i++)
            {
                Animate();
            }
        }

        private void Update()
        {
            _tweenProcessor.Update(Time.deltaTime);
        }

        private void Animate()
        {
            if (_currentViewIndex < objectAmount)
            {
                var view = _viewPool[_currentViewIndex];

                var movePosition = view.Transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), -1).normalized * 10f;
                var targetEuler = view.Transform.eulerAngles + new Vector3(0, 0, Random.Range(-1f, 1f)) * 180f;

                var duration = Random.Range(1f, 5f);
                var delay = Random.Range(0f, 0f);

                var tweenGroup = TweenGroupPool.Default.GetTweenGroup(new TweenSettings(duration).WithDelay(delay));
                tweenGroup.AddTween<MoveToPointTween>().WithParameters(view.Transform, movePosition);
                tweenGroup.AddTween<RotateTween>().WithParameters(view.Transform, targetEuler);
                tweenGroup.AddTween<FadeOutTween>().WithParameters(view.SpriteRenderer, 1, 0);
                tweenGroup.AddTween<ScaleToZeroTweenn>().WithParameters(view.Transform);
                _tweenProcessor.ApplyTweenGroup(tweenGroup);

                _tweenProcessor
                    .ApplyTween<ScaleToZeroTweenn>(new TweenSettings(duration: 10f))
                    .WithParameters(view.Transform);

                _tweenProcessor
                    .ApplyTween<FadeOutTween>(new TweenSettings(duration: 10f).WithDelay(5f))
                    .WithParameters(view.SpriteRenderer, startAlpha: view.SpriteRenderer.color.a, endAlpha: 0);

                _currentViewIndex++;
            }
        }
    }
}