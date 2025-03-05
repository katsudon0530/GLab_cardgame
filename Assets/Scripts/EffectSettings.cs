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
    private CancellationTokenSource _ctsMove;
    public async UniTask StayEffect(Vector3 effectPos)
    {
        _cts = new CancellationTokenSource();
        GameObject effect = (GameObject)Resources.Load("Effect/EffectDemo");
        var effectPrefab =  Instantiate(effect, effectPos, Quaternion.identity);
        await UniTask.Delay(TimeSpan.FromSeconds(_lifeTime), cancellationToken:_cts.Token);
        _onDestroy?.Invoke();
        Destroy(effectPrefab);
    }

    public async UniTask MoveEffect(Vector3 targetPos, Vector3 startPos)
    {
        _ctsMove = new CancellationTokenSource();
        GameObject effect = (GameObject)Resources.Load("Effect/EffectDemo");
        var effectPrefab =  Instantiate(effect, startPos, Quaternion.identity);
        await effectPrefab.transform.DOLocalMove(targetPos, _duration).SetEase(_easing).ToUniTask(cancellationToken:_ctsMove.Token);
        _onDestroy?.Invoke();
        Destroy(effectPrefab);
         
    }

    public void HoldEffect()
    {
        _cts?.Cancel();
    }

    public void Dispose()
    {
        _cts?.Dispose();
        _ctsMove?.Dispose();
    }

    public void StopEffect()
    {
        _ctsMove?.Cancel();
    }


    private void Start()
    {
       MoveEffect(new Vector3(0, 10, 0),Vector3.zero );
    }
}
