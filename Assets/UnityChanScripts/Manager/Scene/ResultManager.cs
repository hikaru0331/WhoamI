using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityChanScripts.Manager.Scene
{
    public class ResultManager : MonoBehaviour
    {
        // ゲームのステータスを表示するUI関連の変数
        public TextMeshProUGUI highScoreText;
        public TextMeshProUGUI gameScoreText;
        public TextMeshProUGUI gameResultText;
        // 音を再生するのに必要なAudioSource
        public AudioSource audioSource;
        // 新しく選択した際に再生するAudioClip
        public AudioClip selectAudioClip;
        // シーンがローディング中かを判定する変数
        bool isLoad = false;

        // Start is called before the first frame update
        void Start()
        {
            // 保存しているハイスコアの値を「HighScore」へ代入する
            int highScore = PlayerPrefs.GetInt("HighScore");
            // GameDataManagerへ保存しているゲームスコアの値を「gameScore」へ代入する
            int gameScore = GameManager.gameScore;
            // gameScoreTextへ獲得したスコアの値を表示する
            gameScoreText.SetText(gameScore.ToString("0000000"));
            // もしゲームで獲得したスコアが今までで獲得したスコアよりも多かったら
            if (highScore < gameScore)
            {
                // 新しくハイスコアの値を今回獲得したスコアの値で保存する
                PlayerPrefs.SetInt("HighScore", gameScore);
                // highScoreTextへ「NEW RECORD」と表示する
                highScoreText.SetText("NEW RECORD");
            }
            else
            {
                // highScoreTextへ保存しているハイスコアの値を表示する
                highScoreText.SetText(highScore.ToString("0000000"));
            }
            if (GameManager.isGameClear == true)
            {
                // gameResultTextへ「GAME CLEAR」と表示する
                gameResultText.SetText("GAME CLEAR");
            }
            else
            {
                // gameResultTextへ「GAME OVER」と表示する
                gameResultText.SetText("GAME OVER");
            }
        }

        void Update()
        {
            // ゲーム開始のキーを押した場合
            if (Input.GetKeyDown(KeyCode.X))
            {
                // ゲームがロード中でなかった場合にロードを実行する
                if (isLoad == false)
                {
                    // スタート用のオーディオクリップを再生する
                    audioSource.PlayOneShot(selectAudioClip);
                    // タイトルシーンをロードする処理を開始
                    SceneManager.LoadScene("Title");
                    // 現在の状態をローディング中にする
                    isLoad = true;
                }
            }
        }
    }
}
