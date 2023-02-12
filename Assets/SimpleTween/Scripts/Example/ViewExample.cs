using UnityEngine;
using Random = UnityEngine.Random;

namespace Starbugs.SimpleTween.Scripts.Example
{
    public class ViewExample : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer { get; private set; }
        public Transform Transform { get; private set; }

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Transform = GetComponent<Transform>();

            SpriteRenderer.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
        }
    }
}