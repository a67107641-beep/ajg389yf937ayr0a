using UnityEngine;

public class EffectExecutor : MonoBehaviour
{
    public static EffectExecutor Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ExecuteEffect(EffectData effect)
    {
        if (effect == null) return;
        effect.Apply();
    }
}
