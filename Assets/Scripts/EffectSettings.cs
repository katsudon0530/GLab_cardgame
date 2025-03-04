using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;

public class EffectSettings : MonoBehaviour
{
    [SerializeField] private AnimationCurve _easing;
    [SerializeField] private float _duration;
    [SerializeField] private float _lifeTime;
    private Action _onDestroy;
    private CancellationTokenSource _cts;
    public async UniTask StayEffect(Vector3 effectPos)
    {
        _cts = new CancellationTokenSource();
        GameObject effect = (GameObject)Resources.Load("Effect");
        var effectPrefab =  Instantiate(effect, effectPos, Quaternion.identity);
        await UniTask.Delay(TimeSpan.FromSeconds(_lifeTime), cancellationToken:_cts.Token);
        _onDestroy?.Invoke();
        Destroy(effectPrefab);
    }

    public async UniTask MoveEffect(Vector3 targetPos, Vector3 startPos)
    {
        GameObject effect = (GameObject)Resources.Load("Effect");
        var effectPrefab =  Instantiate(effect, startPos, Quaternion.identity);
        await effect.transform.DOLocalMove(targetPos, _duration).SetEase(_easing).ToUniTask(cancellationToken:_cts.Token);
        _onDestroy?.Invoke();
        Destroy(effectPrefab);
         
    }

    public void CancellEffect()
    {
        _cts?.Cancel();
    }

    public void Dispose()
    {
        _cts?.Dispose();
    }
}
