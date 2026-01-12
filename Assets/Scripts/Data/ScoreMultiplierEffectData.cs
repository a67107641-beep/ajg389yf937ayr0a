using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ElementalTetris/Effects/Score Multiplier")]
//Score‚ð”{‚É‚·‚éƒNƒ‰ƒX
public class ScoreMultiplierEffectData : EffectData
{
    public int multiplier = 2;

    public override void Apply()
    {
        ScoreManager.Instance.MultiplyScore(multiplier);
    }
}
