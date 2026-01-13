using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
// テトリミノ（落下するブロックの塊）を制御するクラス
{
    public float fallSpeed = 5f;
    // テトリミノが落下するスピード

    public float bottomY = 0f;
    // 着地と判定されるY座標（床の高さ）

    public float spawnY = 10f;
    // テトリミノが生成される初期Y座標

    bool landed = false;
    // すでに着地したかどうかを判定するフラグ

    void Start()
    // ゲーム開始時（オブジェクト生成時）に1回だけ呼ばれる
    {
        Vector3 pos = transform.position;
        // 現在の位置を取得

        pos.y = spawnY;
        // Y座標をスポーン位置に設定

        transform.position = pos;
        // 変更した位置を反映
    }

    void Update()
    // 毎フレーム呼ばれる処理
    {
        if (landed) return;
        // すでに着地していたら何もしない

        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        // 下方向に fallSpeed 分、時間に応じて移動させる

        if (transform.position.y <= bottomY)
        // Y座標が床以下になったら
        {
            Land();
            // 着地処理を実行
        }
    }

    void Land()
    // テトリミノが着地したときの処理
    {
        landed = true;
        // 着地フラグを立てる

        Vector3 pos = transform.position;
        // 現在の位置を取得

        pos.y = bottomY;
        // Y座標を床の高さに補正

        transform.position = pos;
        // 補正した位置を反映

        foreach (Transform child in transform)
        // テトリミノの子オブジェクト（各ブロック）を順番に処理
        {
            Block block = child.GetComponent<Block>();
            // 子オブジェクトから Block コンポーネントを取得

            if (block == null) continue;
            // Block が付いていなければ次へ

            // block.RegisterToBoard();
            // （コメントアウト）ボード管理クラスに登録する処理

            block.transform.parent = null;
            // 親（テトリミノ）から切り離し、個別のブロックにする
        }

        Destroy(gameObject);
        // テトリミノ本体を削除（子ブロックは残る）
    }
}
