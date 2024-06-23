using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // ゲームのステータスを表示するUI関連の変数
    public TextMeshProUGUI highScoreText;
    // 音を再生するのに必要なAudioSource
    public AudioSource audioSource;
    // 新しく選択した際に再生するAudioClip
    public AudioClip selectAudioClip;
    // ゲーム開始時のPlayerが持つHPの数を指定する変数
    public int startPlayerHp = 2;
    // 次にロードするステージの名前を指定する
    public string nextStageName;
    // シーンがローディング中かを判定する変数
    bool isLoad = false;

    // Start is called before the first frame update
    void Start()
    {
        // 保存しているハイスコアの値を「HighScore」へ代入する
        int highScore = PlayerPrefs.GetInt("HighScore");
        // highScoreTextへ保存しているハイスコアの値を表示する
        highScoreText.text = highScore.ToString("0000000");

        PlayerController.playerLife = startPlayerHp;

        GameManager.gameScore = 0;
        GameManager.gameCoin = 0;
        
    }

    // Update is called once per frame
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
                // 次のシーンをロードする処理を開始
                SceneManager.LoadScene(nextStageName);
                // 現在の状態をローディング中にする
                isLoad = true;
            }
        }
    }
}
