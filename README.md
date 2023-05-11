# SimpleTween #

![GIF](https://github.com/nenuacho/SimpleTween/blob/develop/demo.gif?raw=true)


### Lightweight animation engine ###

* Fast and non-alloc
* Scalable
* Engine agnostic
* Easy API
* Animations batching
* Easing friendly


---



## API ##
### Single animation ###

```csharp

public void StartAnimation()
{
    _tweenProcessor
        .ApplyTween<MoveToPointTween>(new TweenSettings(duration: 20).WithDelay(1))
        .WithParameters(view.Transform, view.Transform.position + Vector3.right * 10f);
}
```
### Batch animations ###
#### If you have multiple animations with same settings (Duration and Delay) it's recommended to use TweenGroup ####
```csharp

public void StartAnimation()
{
    var tweenGroup = _tweenProcessor
        .ApplyTweenGroup()
        .WithSettings(new TweenSettings(duration).WithDelay(delay));

    tweenGroup.AddTween<MoveToPointTween>().WithParameters(view.Transform, movePosition);
    tweenGroup.AddTween<RotateTween>().WithParameters(view.Transform, targetEuler);
    tweenGroup.AddTween<ChangeColorTween>().WithParameters(view.SpriteRenderer, new Color(1f,0.7f,0.7f), new Color(0.5f,0f,0f));
    tweenGroup.AddTween<FadeTween>().WithParameters(view.SpriteRenderer, 0, 1);
    tweenGroup.AddTween<ScaleToZeroTween>().WithParameters(view.Transform);
    tweenGroup.AddTween<SetActiveTween>().WithParameters(view.gameObject);
}
```
### Chaining ###
#### For chaining use callback function ####
```csharp
var tweenGroup = _tweenProcessor
    .ApplyTweenGroup()
    .WithCallback(() => 
    {
        var tweenGroup2 = _tweenProcessor
            .ApplyTweenGroup()
            .WithSettings(new TweenSettings(duration));

        tweenGroup2.AddTween<ScaleToZeroTween>().WithParameters(view.Transform); // Will run after the first tween group is completed
    })
    .WithSettings(new TweenSettings(duration).WithDelay(0));

tweenGroup.AddTween<MoveToPointTween>().WithParameters(view.Transform, movePosition);
```

### Custom animations ###
```csharp

    public class MyMoveToPointTween : ITween
    {
        private Transform _transform;
        private Vector3 _targetPosition;
        private Vector3 _startPosition;

        public void UpdatePlaybackTime(float time)
        {
            _transform.position = Vector3.Lerp(_startPosition, _targetPosition, time);
        }

        public bool IsActive { get; set; }

        public void WithParameters(Transform transform, Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
            _startPosition = transform.position;
            _transform = transform;
        }
    }
```
```csharp

public void StartAnimation()
{
    var tweenGroup = _tweenProcessor
        .ApplyTweenGroup()
        .WithSettings(new TweenSettings(duration).WithDelay(delay));

    tweenGroup.AddTween<MyMoveToPointTween>().WithParameters(view.Transform, movePosition);
}
```
------------
## License
[MIT](https://choosealicense.com/licenses/mit/)
