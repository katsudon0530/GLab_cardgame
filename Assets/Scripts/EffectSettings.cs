using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEditor.Build.Reporting;

public class EffectSettings : MonoBehaviour
{
    [Header("エフェクトの動きの波形")]
    [SerializeField] private AnimationCurve _easing;
    [Header("移動にかかる時間")]
    [SerializeField] private float _duration;
    [Header("固定エフェクトの生存時間")]
    [SerializeField] private float _lifeTime;
    private Action _onDestroy;
    private CancellationTokenSource _cts;
    private CancellationTokenSource _ctsMove;
    public async UniTask StayEffect(Vector3 effectPos)
    {
        _cts = new CancellationTokenSource();　//初期化
        GameObject effect = (GameObject)Resources.Load("Effect/EffectDemo");　//Resources.Loadの引数にResources以下のpathを入力
        if (effect == null)
        {
            Debug.Log("指定されたパスが見つかりません");
            return;
        }
        var effectPrefab =  Instantiate(effect, effectPos, Quaternion.identity); 
        await UniTask.Delay(TimeSpan.FromSeconds(_lifeTime), cancellationToken:_cts.Token); //TimeSpan.FromSecondsの引数秒分待つ
        _onDestroy?.Invoke();
        Destroy(effectPrefab);
    }

    public async UniTask MoveEffect(Vector3 targetPos, Vector3 startPos)
    {
        _ctsMove = new CancellationTokenSource();　
        GameObject effect = (GameObject)Resources.Load("Effect/EffectDemo");
        if (effect == null)
        {
            Debug.Log("指定されたパスが見つかりません");
            return;
        }
        var effectPrefab =  Instantiate(effect, startPos, Quaternion.identity);
        await effectPrefab.transform.DOLocalMove(targetPos, _duration).SetEase(_easing).ToUniTask(cancellationToken:_ctsMove.Token); 
        // _durationの時間でtargetPosまでtransformを移動させる(_easingにのっとって)　ToUniTaskでawaitの後に書けるようになる
        _onDestroy?.Invoke();
        Destroy(effectPrefab);
         
    }

    
    /// <summary>
    ///　固定effectを消したくない時に呼びだす
    /// </summary>
    public void HoldEffect()
    {
        _cts?.Cancel();
    }

    
    /// <summary>
    ///　ゲーム終了時に必要に応じて呼び出す
    /// </summary>
    public void Dispose()
    {
        _cts?.Dispose();
        _ctsMove?.Dispose();
    }

    
    /// <summary>
    ///　動くeffectを止める際に呼び出す
    /// </summary>
    public void StopEffect()
    {
        _ctsMove?.Cancel();
    }

}
