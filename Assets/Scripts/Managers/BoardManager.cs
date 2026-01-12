using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
// テトリスの盤面全体を管理するクラス
// ブロック配置、ライン判定、ライン消去、元素反応の起点を担当する
{
    public static BoardManager Instance;
    // シングルトン用インスタンス（盤面は1つだけ）

    public int width = 10;
    // 盤面の横幅（マス数）

    public int height = 20;
    // 盤面の高さ（マス数）

    private Block[,] grid;
    // 盤面の有無を管理する2次元配列

    void Awake()
    // オブジェクト生成時に最初に呼ばれる盤面のインスタンス
    {
        if (Instance == null)
        {
            Instance = this;
            // 最初の1つをシングルトンとして設定
            Debug.Log("BoardManager Instance セット");
        }
        else
        {
            Destroy(gameObject);
            // すでに存在する場合は重複を防ぐ
            return;
        }

        grid = new Block[width, height];
        // 盤面サイズに合わせてグリッドを初期化
    }

    public void PlaceBlock(Block block, int x, int y)
    // 指定した座標にブロックを配置する
    {
        // ★ 盤面外チェック
        //マイナス → 左や下に飛び出している
        //width / height 以上 → 右や上に飛び出している
        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            Debug.LogWarning($"盤面外: x={x}, y={y}");
            return;
        }

        // グリッドにブロックを登録
        grid[x, y] = block;

        // ★ ブロックの見た目上の位置を盤面座標に合わせる
        //x = マスの横番号
        //y = マスの縦番号
        //z = 奥行き（2Dなので0）
        block.transform.position = new Vector3(x, y, 0);
    }

    // 指定した行がすべて埋まっているかをチェック
    bool IsLineFull(int y)
    {
        //width は盤面の横マス数10）
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null)
                return false;
            // 1マスでも空いていたら未完成
        }
        return true;
        // 全マス埋まっていたら true
    }

    // 行を消す処理（★ライン消去）
    void ClearLine(int y)
    {
        List<Block> clearedBlocks = new List<Block>();
        // 消えたブロックを記録（元素反応用）

        for (int x = 0; x < width; x++)
        {
            Block block = grid[x, y];
            if (block != null)
            {
                clearedBlocks.Add(block);
                // 消えるブロックをリストに追加

                Destroy(block.gameObject);
                // ブロックのゲームオブジェクトを削除

                grid[x, y] = null;
                // グリッド上からも削除
            }
        }

        // ★ ライン消去後、元素反応処理へつなぐ
        OnLineCleared(clearedBlocks);
    }

    // 全行をチェックして、埋まっている行を消す
    public void CheckLines()
    {
        for (int y = 0; y < height; y++)
        {
            if (IsLineFull(y))
            {
                ClearLine(y);
            }
        }
    }

    public ElementData testElement;
    // テスト用の元素データ（Inspectorから設定）

    void Start()
    // テスト用処理（仮）
    {
        // テスト用：1行を強制的に埋める
        for (int x = 0; x < width; x++)
        {
            GameObject go = new GameObject($"Block_{x}");
            go.AddComponent<SpriteRenderer>();

            Block block = go.AddComponent<Block>();
            // ブロックコンポーネントを追加

            block.element = testElement;
            // テスト用の元素を設定

            PlaceBlock(block, x, 0);
            // 一番下の行に配置
        }

        // ★ ラインチェックを実行
        CheckLines();
    }

    public void OnLineCleared(List<Block> clearedBlocks)
    // ラインが消えたときに呼ばれる処理
    {
        List<ElementData> elements = new List<ElementData>();
        // 消えたブロックの元素一覧

        foreach (var block in clearedBlocks)
        {
            if (block.element != null)
            {
                elements.Add(block.element);
            }
        }

        // 元素反応の判定を試みる
        TryTriggerReaction(elements);
    }

    void TryTriggerReaction(List<ElementData> elements)
    // 元素の組み合わせから反応を探す
    {
        List<ElementData> uniqueElements = new List<ElementData>();
        // 重複しない元素リストを作成

        foreach (var e in elements)
        {
            if (!uniqueElements.Contains(e))
                uniqueElements.Add(e);
        }

        // 元素の組み合わせを全パターンチェック
        for (int i = 0; i < uniqueElements.Count; i++)
        {
            for (int j = i + 1; j < uniqueElements.Count; j++)
            {
                var reaction =
                    ReactionManager.Instance.GetReaction(
                        uniqueElements[i],
                        uniqueElements[j]
                    );

                if (reaction != null)
                {
                    Debug.Log("反応発生: " + reaction.reactionName);
                    // 反応名をログ表示

                    EffectExecutor.Instance.ExecuteEffect(reaction.effect);
                    // 対応する効果を実行

                    return;
                    // 最初に見つかった反応だけ実行
                }
            }
        }
    }
}
