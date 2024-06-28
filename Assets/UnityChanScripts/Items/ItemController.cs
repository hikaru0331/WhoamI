using UnityChanScripts.Manager;
using UnityEngine;

namespace UnityChanScripts.Items
{
    /// <summary>
    /// アイテム取得の制御を行うスクリプト
    /// </summary>
    public class ItemController : MonoBehaviour
    {
        // コインを取得した際にサウンドが流れるゲームオブジェクト
        public GameObject coinSound;

        public int score = 100;

        void OnTriggerEnter2D(Collider2D other)
        {
            // もし接触したtagがPlayerだったなら
            if (other.gameObject.tag == "Player")
            {
                GameManager.gameScore += score;
                GameManager.gameCoin += 1;
                // サウンドが流れるゲームオブジェクトを生成して音声を再生する
                Instantiate(coinSound, transform.position, Quaternion.identity);
                // コインのゲームオブジェクトを消去する
                Destroy(this.gameObject);
            }
        }
    }
}