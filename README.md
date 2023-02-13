# SimpleTween #

![Alt text](https://bitbucket.org/privatevoid/simpletweener/raw/51cd3377d01e5275740f9ea5e60ded7bb46000d7/demo.gif)


### Lightweight animation engine ###

* Fast and non-alloc
* Scalable
* Engine agnostic
* Easy API
* Animations batching


---



## API ##
<br/>
### Single animation ###
---------------------------------------
```csharp

public void StartAnimation()
{
    _tweenProcessor
        .ApplyTween<MoveToPointTween>(new TweenSettings(duration: 20).WithDelay(1))
        .WithParameters(view.Transform, view.Transform.position + Vector3.right * 10f);
}
```
<br/>
------------------------------------------
<br/>
### Batch animations ###
#### If you have multiple animations with same settings (Duration and Delay) it's recommended to use TweenGroup ####
---------------------------------------
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
<br/>
---------------
<br/>
### Custom animations ###
------------
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

---------------
<br/>
------------
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
<br/>
------------
## License
[MIT](https://choosealicense.com/licenses/mit/)