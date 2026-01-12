using UnityEngine;

/// <summary>
/// 効果データの基底クラス
/// ダメージ・回復・バフなどの「効果」を表す ScriptableObject
/// 継承して具体的な効果を実装する
/// </summary>
public abstract class EffectData : ScriptableObject
{
    /// 効果を適用する処理
    /// 継承先クラスで必ず実装する
    public abstract void Apply();
}
