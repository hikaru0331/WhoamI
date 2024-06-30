using System.Collections;
using TMPro;
using UnityChanScripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityChanScripts.Manager
{
    /// <summary>
    /// ここの学びは関数にしたい
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        // ゲームのUI表示に利用する変数
        public TextMeshProUGUI timeCountText;   // 残り時間の表示を行うテキスト
        public TextMeshProUGUI coinCountText;   // コイン取得数の表示を行うテキスト
        public TextMeshProUGUI scoreCountText;  // スコア獲得数の表示を行うテキスト
        public TextMeshProUGUI lifeCountText;   // 残りHPの表示を行うテキスト
        // シーン遷移に利用する変数
        public string nextStageName;    // 次にロードするシーンの名前を指定する変数
        string loadSceneName;           // シーンのロード処理を行うシーンの名前を代入する変数
        float sceneLoadTime = 0.8f;     // シーンをロードするまでの時間を指定する変数
        // ゲームの進行に利用する変数
        public int gameTime;    // ゲーム終了までの時間を初期で指定する変数
        float remainingTime;    // 残り時間を保存する変数
        bool isPlay = false;    // ゲームがプレイ中か判定する変数
        public static bool isGameClear = false;    // ゲームがクリアできたか判定する変数
        public static int gameCoin;    // 獲得したゲームコインの数を保存する変数
        public static int gameScore;   // 獲得したゲームスコアを保存する変数
        public PlayerController player; // PlayerControllerが付いたGameObjectを参照するための変数

        void Start()
        {
            // ゲームをプレイ中と判定する
            isPlay = true;
            // 残り時間をゲームステージの初期時間に設定する
            remainingTime = gameTime;
        }

        // Update is called once per frame
        void Update()
        {
            // もしゲームがプレイ中なら
            if (isPlay == true)
            {
                // 残り時間を減らしていく
                remainingTime -= Time.deltaTime;
                // もし残り時間が0以下になったら
                if (remainingTime <= 0)
                {
                    // ゲームオーバーの処理を実行
                    GameOver();
                }
            }
        }

        /// <summary>
        /// シーンをロードするメソッド
        /// </summary>
        IEnumerator LoadScene()
        {
            // sceneLoadTimeの時間ロード処理を停止する
            yield return new WaitForSeconds(sceneLoadTime);
            // loadSceneNameの名前が付いたシーンをロードする
            SceneManager.LoadScene(loadSceneName);
        }

        /// <summary>
        /// ゲームをクリアした際の処理
        /// </summary>
        public void GameClear()
        {
            // ゲームが終了済みならクリア処理を実行しない
            if (isPlay == false) return;
            // ゲームプレイを終了したと判定する
            isPlay = false;
            // playerのクリア音声を出す
            player.PlayGameClearVoice();
            // 次にロードするシーン名を指定する
            loadSceneName = nextStageName;
            // シーンをロードするコルーチンを実行
            StartCoroutine(LoadScene());
            Debug.Log("GameClear");

            int score = (int)remainingTime * 10;

            gameScore += score;

            isGameClear = true;
        }

        /// <summary>
        /// ゲームオーバーになった際の処理
        /// </summary>
        public void GameOver()
        {
            // ゲームが終了済みならクリア処理を実行しない
            if (isPlay == false) return;
            // ゲームプレイを終了したと判定する
            isPlay = false;
            // playerのゲームオーバー音声を出す
            player.PlayGameOverVoice();
            // 次にロードするシーン名を指定する
            loadSceneName = "Result";
            // シーンをロードするコルーチンを実行
            StartCoroutine(LoadScene());

            isGameClear = false;
        }
    }
}
