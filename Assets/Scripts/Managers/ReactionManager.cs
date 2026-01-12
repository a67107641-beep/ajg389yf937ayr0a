using UnityEngine;

/// 元素反応を管理するクラス
/// 2つの ElementData から対応する ReactionData を取得する

public class ReactionManager : MonoBehaviour
{
    // シングルトン用インスタンス
    public static ReactionManager Instance;

    // 登録されている全ての反応データ
    // Inspector から設定する
    [SerializeField]
    private ReactionData[] reactions;

    /// 初期化処理

    private void Awake()
    {
        // Instance が未設定なら自分を設定
        if (Instance == null)
        {
            Instance = this;
        }
        // 既に存在している場合は重複を防ぐため破棄
        else
        {
            Destroy(gameObject);
        }
    }

  
    /// 2つの元素データから対応する反応データを取得する
    /// 元素の順番は考慮しない（A+B でも B+A でも可）
    ///
    /// <param name="a">元素データ1</param>
    /// <param name="b">元素データ2</param>
    /// <returns>
    /// 対応する ReactionData
    /// 存在しない場合は null
    /// </returns>
    public ReactionData GetReaction(ElementData a, ElementData b)
    {
        // 登録されている全ての反応データをチェック
        foreach (var r in reactions)
        {
            // 元素の組み合わせが一致するか判定
            if (
                (r.elementA == a && r.elementB == b) ||
                (r.elementA == b && r.elementB == a)
            )
            {
                // 一致した反応データを返す
                return r;
            }
        }

        // 該当する反応が見つからなかった場合
        return null;
    }
}
