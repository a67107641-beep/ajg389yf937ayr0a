using UnityEngine;
// Unityの基本機能（MonoBehaviour, Debug, Mathf など）を使うために必要

public class ScoreManager : MonoBehaviour
// スコア管理を行うクラス。MonoBehaviourを継承してUnityで使用可能にする
{
    public static ScoreManager Instance;
    // シングルトン用の静的インスタンス（どこからでもアクセス可能）

    public int score = 0;
    // 現在のスコアを保持する変数（初期値は0）

    private void Awake()
    // オブジェクト生成時に最初に呼ばれる関数
    {
        if (Instance == null)
            // まだインスタンスが存在しない場合
            Instance = this;
        // 自分自身をInstanceとして設定
        else
            Destroy(gameObject);
        // すでに存在していたら重複を防ぐためにこのオブジェクトを削除
    }

    public void AddScore(int amount)
    // スコアを加算するための関数
    {
        score += amount;
        // 現在のスコアに指定された値を足す
        Debug.Log("Score: " + score);
        // 現在のスコアをコンソールに表示
    }

    public void MultiplyScore(float multiplier)
    // スコアを倍率で増減させる関数
    {
        score = Mathf.RoundToInt(score * multiplier);
        // スコアに倍率をかけて、小数を四捨五入して整数に変換
        Debug.Log("Score x" + multiplier + " → " + score);
        // 倍率と結果のスコアをコンソールに表示
    }
}
