using UnityEngine;

public class ReactionTest : MonoBehaviour
{
    public ElementData elementA;
    public ElementData elementB;

    private void Start()
    {
        // 初期スコアを入れておく
        ScoreManager.Instance.AddScore(100);

        // 反応を取得
        ReactionData reaction =
            ReactionManager.Instance.GetReaction(elementA, elementB);

        if (reaction == null)
        {
            Debug.Log("反応なし");
            return;
        }

        Debug.Log("反応発生: " + reaction.reactionName);

        // 効果を実行
        EffectExecutor.Instance.ExecuteEffect(reaction.effect);
    }
}
