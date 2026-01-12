using UnityEngine;

[CreateAssetMenu(menuName = "ElementalTetris/Reaction")]
/// <summary>
/// 組み合わせの基底クラス
/// A＋Bの「組み合わせ」を表す ScriptableObject
/// 「EffectData」クラスと併用し、継承して具体的な効果を実装する
/// </summary>
public class ReactionData : ScriptableObject
{
    public ElementData elementA;
    public ElementData elementB;
    public string reactionName;
    public EffectData effect;
}
