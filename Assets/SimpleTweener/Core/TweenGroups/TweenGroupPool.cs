using System.Collections.Generic;

namespace SimpleTweener.Core
{
    public class TweenGroupPool
    {
        private static TweenGroupPool _instance;
        public static TweenGroupPool Default => _instance ??= new TweenGroupPool();

        private readonly List<TweenGroup> _tweenGroups = new();

        public TweenGroup GetTweenGroup(TweenSettings settings)
        {
            TweenGroup tGroup;
            var data = TweenGroupData.Map(settings);

            for (int i = 0; i < Default._tweenGroups.Count; i++)
            {
                tGroup = Default._tweenGroups[i];
                if (tGroup.IsFinished)
                {
                    tGroup.WithData(data);
                    return tGroup;
                }
            }

            tGroup = new TweenGroup().WithData(data);
            Default._tweenGroups.Add(tGroup);
            return tGroup;
        }
    }
}