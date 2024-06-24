using UnityEngine;

namespace UnityChanScripts.Manager.Scene
{
    /// <summary>
    /// タイトルシーンでのUnityChanが画面外に出たら反対から戻ってくる処理を記述したクラス
    /// NOTE: Tipsでメンバーが実装する
    /// </summary>
    public class TransferWall : MonoBehaviour
    {
        // ワープ先のゲームオブジェクトを指定
        public GameObject otherWall;
        // UnityChanをワープ先の座標を代入する変数
        Vector2 transferPos;
        // 壁からどれだけx方向にUnityChanをワープさせる距離を代入する変数
        public float offsetX;

        private void Start()
        {
            // ワープ先ゲームオブジェクトの座標を取得
            Vector3 otherWallPos = otherWall.transform.position;
            // ワープ先のx座標を生成して代入
            transferPos = new Vector2(otherWallPos.x + offsetX, 0);
        }

        /// <summary>
        /// 当たり判定の処理を行う関数
        /// </summary>
        void OnTriggerEnter2D(Collider2D other)
        {
            // 接触したゲームオブジェクトのtagがPlayerであれば処理を実行
            if (other.tag == "Player")
            {
                // ワープ先のy座標をPlayerのY座標にする
                transferPos.y = other.transform.position.y;
                // Playerの座標へワープ先座標を代入する
                other.transform.position = transferPos;
            }
        }
    }
}
