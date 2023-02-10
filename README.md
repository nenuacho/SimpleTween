# SimpleTweener #

### Lightweight animation engine ###

* Fast and non-alloc
* Scalable
* Zero allocations
* Engine agnostic
* Easy API
* Animations batching





---
## Performance ##

Tested in Unity2022 with 10000 animated objects on scene
<br/>
<br/>
Test1: every object has 1 movement animation
<br/>
Test2: every object has 3 animations at time - movement, rotation, fadeout
<br/>

|              | Test1 (ms) | Test2 (ms)|
|------------- | ------------|--------------|
|Coroutines    | 32.25        |109.49         |
|DOTween       | 23.95        |80.77          |
|SimpleTweener | 11.68        |34.74          |

---



## API ##
<br/>
### Init and start animation ###
---------------------------------------
```csharp

public void StartAnimation()
{
	tweener.Do(TweenSet
				   .WithSettings(new TweenCommonSettings(duration).WithDelay(delay))
				   .WithAnimation(new RotateAnimation(instanceTransform, targetEuler)));
}
```
<br/>
------------------------------------------
<br/>
### Batch animations ###
---------------------------------------
```csharp

public void StartAnimation()
{
	tweener.Do(TweenSet
				   .WithSettings(new TweenCommonSettings(duration).WithDelay(delay))
				   .WithAnimation(new FadeOutAnimation(instanceRenderer, 1, 0))
				   .WithAnimation(new MoveAnimation(instanceTransform, movePosition))
				   .WithAnimation(new RotateAnimation(instanceTransform, targetEuler)));
}
```
<br/>
---------------
<br/>
### You can create your own animation ###
------------
```csharp

public readonly struct MyScaleAnimation : ITween
{
    private readonly Transform _transform;
    private readonly Vector3 _startScale;

    public MyScaleAnimation(Transform transform)
    {
        _startScale = transform.localScale;
        _transform = transform;
    }

    public void UpdatePlaybackTime(float time)
    {
        _transform.localScale = Vector3.Lerp(_startScale, Vector3.zero, time);
    }
}
```

---------------
<br/>
### And just add it to the tween set ###
------------
```csharp

public void StartAnimation()
{
    tweener.Do(TweenSet
                   .WithSettings(new TweenCommonSettings(duration).WithDelay(delay))
                   .WithAnimation(new FadeOutAnimation(instanceRenderer, 1, 0))
                   .WithAnimation(new MoveAnimation(instanceTransform, movePosition))
                   .WithAnimation(new RotateAnimation(instanceTransform, targetEuler))
                   .WithAnimation(new MyScaleAnimation(instanceTransform)));
}
```