using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class EffectSettings : MonoBehaviour
{
    [SerializeField] private AnimationCurve _easing;
    [SerializeField] private float _duration;
    public void EffectInstantiate(Vector3 effectPos)
    {
        GameObject effect = (GameObject)Resources.Load("Effect");
        Instantiate(effect, effectPos, Quaternion.identity);
    }

    public async UniTask EffectMove(Vector3 targetPos)
    {
         await transform.DOLocalMove(targetPos, _duration).SetEase(_easing).ToUniTask();
         Debug.Log("移動完了");
         
    }
}
