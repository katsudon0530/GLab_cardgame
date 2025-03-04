
using UnityEngine;

public class EffectSettings : MonoBehaviour
{
    public void EffectInstantiate(Vector3 effectPos)
    {
        GameObject effect = (GameObject)Resources.Load("Effect");
        Instantiate(effect, effectPos, Quaternion.identity);
    }

    public void EffectMove(Vector3 targetPos)
    {
        
    }
}
