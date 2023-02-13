using System.Collections.Generic;
using Starbugs.SimpleTween.Scripts.Core;
using Starbugs.SimpleTween.Scripts.DefaultTweens;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Starbugs.SimpleTween.Scripts.Example
{
    public class SimpleTweenExample : MonoBehaviour
    {
        [field: SerializeField] private ViewExample examplePrefab;
        [field: SerializeField] private int objectAmount = 10000;

        private TweenProcessor _tweenProcessor;
        private List<ViewExample> _viewPool;

        private int _currentViewIndex;

        private void Start()
        {
            _tweenProcessor = new TweenProcessor();
            _viewPool = new List<ViewExample>();

            for (int i = 0; i < objectAmount; i++)
            {
                var instance = Instantiate(examplePrefab);
                instance.transform.position = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(0f, 25f);
                instance.transform.eulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));
                instance.gameObject.SetActive(false);
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
                var delay = Random.Range(1f, 2f);

                // Single animation
                // _tweenProcessor
                //     .ApplyTween<MoveToPointTween>(new TweenSettings(duration: 20).WithDelay(1))
                //     .WithParameters(view.Transform, view.Transform.position + Vector3.right * 10f);

                // Batch animation
                var tweenGroup = _tweenProcessor
                    .ApplyTweenGroup()
                    .WithSettings(new TweenSettings(duration).WithDelay(delay));

                tweenGroup.AddTween<MoveToPointTween>().WithParameters(view.Transform, movePosition);
                tweenGroup.AddTween<RotateTween>().WithParameters(view.Transform, targetEuler);
                tweenGroup.AddTween<FadeTween>().WithParameters(view.SpriteRenderer, 0, 1);
                tweenGroup.AddTween<ScaleToZeroTween>().WithParameters(view.Transform);
                tweenGroup.AddTween<SetActiveTween>().WithParameters(view.gameObject);
                _currentViewIndex++;
            }
        }
    }
}