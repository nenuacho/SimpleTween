using SimpleTweener.Core;
using SimpleTweener.TweenImpl;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SimpleTweener.Example
{
    public class SimpleTweenerExample : MonoBehaviour
    {
        public GameObject examplePrefab;
        public int objectAmount = 1000;

        private void Start()
        {
            var tweener = GetComponent<TweenRunner>();

            for (int i = 0; i < objectAmount; i++)
            {
                var instance = Instantiate(examplePrefab);

                instance.transform.position = new Vector2(Random.Range(-40, 40), Random.Range(-25, 25));

                var instanceRenderer = instance.GetComponent<SpriteRenderer>();
                var instanceTransform = instance.transform;
                var movePosition = instanceTransform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * 10f;
                var targetEuler = instanceTransform.eulerAngles + new Vector3(0, 0, Random.Range(-1f, 1f)) * 180f;

                var duration = Random.Range(5f, 10f);
                var delay = Random.Range(0f, 0f);

                tweener.Do(TweenSet
                               .WithSettings(new TweenCommonSettings(duration).WithDelay(delay))
                               .WithAnimation(new FadeOutAnimation(instanceRenderer, 1, 0))
                               .WithAnimation(new MoveAnimation(instanceTransform, movePosition))
                               .WithAnimation(new RotateAnimation(instanceTransform, targetEuler)));
            }
        }

        // private IEnumerator FadeOutCoroutine(SpriteRenderer iRenderer)
        // {
        //     while (true)
        //     {
        //         var color = iRenderer.color;
        //         SetAlpha(iRenderer, color.a - 0.001f);
        //
        //         if (color.a <= 0)
        //         {
        //             break;
        //         }
        //
        //         yield return null;
        //     }
        // }
        //
        // private IEnumerator MoveCoroutine(Transform iTransform, Vector3 movePos)
        // {
        //     while (true)
        //     {
        //         iTransform.position = Vector3.Lerp(iTransform.position, movePos, Time.deltaTime);
        //
        //         if (Mathf.Approximately(Vector3.Distance(iTransform.position, movePos), 0))
        //         {
        //             break;
        //         }
        //
        //         yield return null;
        //     }
        // }
        //
        // private IEnumerator RotateCoroutine(Transform iTransform, Vector3 targetEuler)
        // {
        //     while (true)
        //     {
        //         iTransform.eulerAngles = Vector3.Lerp(iTransform.eulerAngles, targetEuler, Time.deltaTime);
        //
        //         if (Mathf.Approximately(Vector3.Distance(iTransform.eulerAngles, targetEuler), 0))
        //         {
        //             break;
        //         }
        //
        //         yield return null;
        //     }
        // }


        // private void SetAlpha(SpriteRenderer r, float value)
        // {
        //     var tempColor = r.color;
        //     tempColor.a = value;
        //     r.color = tempColor;
        // }
    }
}