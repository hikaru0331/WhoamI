using UnityEngine;

namespace UnityChanScripts.Block
{
    /// <summary>
    /// 破壊されたCubeの動きを制御するスクリプト
    /// </summary>
    public class BrokenBlockController : MonoBehaviour
    {
        //TODO 配列はまだ学習していないので、配列なしで実装しても良い？
        // バラバラになるゲームオブジェクトのRigidbodyを変数で指定
        public Rigidbody2D rigidbody0;
        public Rigidbody2D rigidbody1;
        public Rigidbody2D rigidbody2;
        public Rigidbody2D rigidbody3;

        void Start()
        {
            // それぞれのRigidbodyへ力を加えて飛び散る表現を実装
            rigidbody0.AddForce(new Vector2(200, 250));
            rigidbody1.AddForce(new Vector2(200, 100));
            rigidbody2.AddForce(new Vector2(-200, 250));
            rigidbody3.AddForce(new Vector2(-200, 100));
            // 3秒後にこのゲームオブジェクトを削除する
            Destroy(gameObject, 3);
        }
    }
}
