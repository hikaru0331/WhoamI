using System.Collections;
using UnityChanScripts.Manager;
using UnityEngine;

namespace UnityChanScripts.Player
{
    /// <summary>
    /// Playerのコントロール用スクリプト
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        // Playerの制御に必要な各種コンポーネント
        // PlayerのAnimator
        public Animator playerAnimator;
        // PlayerのRigidbody
        public Rigidbody2D playerRigidbody2D;
        // AudioSourceを指定する変数
        public AudioSource audioSource;

        // GameManagerを参照する変数
        public GameManager gameManager;

        // UnityChanのセリフ用AudioClip
        public AudioClip damageVoice;
        public AudioClip jumpVoice;
        public AudioClip clearVoice;
        public AudioClip gameOverVoice;
        public AudioClip getSound;

        // 以下変数
        // 移動速度の速さを指定
        public float maxSpeed = 9f;
        // ジャンプする力の大きさを指定
        public float jumpPower = 10f;
        // ノックバック力の大きさを指定
        Vector2 backwardForce = new Vector2(-9.9f, 11.88f);
        // Groundに設置しているかの判定処理
        bool isGround;
        // PlayerSpriteの初期サイズを保存する変数
        Vector3 defaultLocalScale;

        // この以下3つの変数をメンバーが記述
        // Playerがダメージ状態か判定するbool変数
        bool isDamaged = false;
        // Playerライフを保存する変数
        public static int playerLife = 4;

        //チェックポイントを入れる変数
        bool isFlag = false;


        //フラグのオブジェクトを入れる変数
        public GameObject flag;

        //フラグの見た目を変えるための変数
        public SpriteRenderer flagSquare;
        public Sprite flagHurdle;

        //スタート地点の座標
        private Vector3 startPosition = new Vector3(-9.62f, -5.54f, 10.0f);
        public GameObject mainCamera;
        private Vector3 cameraPosition = new Vector3();

        // Start is called before the first frame update
        private void Start()
        {
            // 初期状態でPlayerの大きさを保存
            defaultLocalScale = transform.localScale;

            cameraPosition = new Vector3(startPosition.x + 9.62f, startPosition.y + 5.54f, startPosition.z - 20.0f);
        }

        // Update is called once per frame
        private void Update()
        {
            // PlayerがGroundに接地しているかを判定
            // もしPlayerのy軸方向への加速度が0なら地面に接地していると判定する
            if(playerRigidbody2D.velocity.y == 0)
            {
                // 地面に接地していると判定
                isGround = true;
            }
            else
            {
                // 地面に接地していないと判定
                isGround = false;
            }

            // ダメージ状態でなければ移動処理を実行する
            if (isDamaged == false)
            {
                // 移動の横方向をInputから値で取得
                float horizontalInput = Input.GetAxis("Horizontal");
                // 速度をセットする
                playerRigidbody2D.velocity = new Vector2(horizontalInput * maxSpeed, playerRigidbody2D.velocity.y);
                // もし左右のキーどちらかが押されているなら
                if (horizontalInput != 0)
                {
                    // キャラがどっちに向いているかを判定する
                    float direction = Mathf.Sign(horizontalInput);
                    // キャラの向きをキーの押された方向に指定する
                    transform.localScale =
                        new Vector3(defaultLocalScale.x * direction, defaultLocalScale.y, defaultLocalScale.z);
                }
                // アニメーションの再生
                playerAnimator.SetFloat("Horizontal", horizontalInput);
                playerAnimator.SetFloat("Vertical", playerRigidbody2D.velocity.y);
                playerAnimator.SetBool("isGround", isGround);

                // ジャンプの実行
                if (Input.GetButtonDown("Jump"))
                {
                    //地面にいる場合のみ処理する
                    if (isGround == true)
                    {
                        // ジャンプの処理
                        playerRigidbody2D.AddForce(Vector2.up * jumpPower * 100);
                        // アニメーションの再生
                        playerAnimator.SetTrigger("Jump");
                        // jumpVoiceのSEを再生
                        PlayVoice(jumpVoice);
                    }
                }
            }
        }

        /// <summary>
        /// ダメージを受けたときの非同期処理
        /// </summary>
        IEnumerator OnDamage()
        {
            // ダメージ状態を有効にする
            isDamaged = true;
            // hpの値を減らす処理
// -> この行へダメージ処理を追記するよ！

            // ダメージアニメーション
            if (isGround == true)
            {
                // アニメーションの再生
                playerAnimator.Play("Damage");
            }
            else
            {
                // アニメーションの再生
                playerAnimator.Play("AirDamage");
            }
            // アニメーションの再生
            playerAnimator.Play("Idle");
            // damageVoiceのSEを再生
            PlayVoice(damageVoice);

            // ノックバック
            playerRigidbody2D.velocity = new Vector2(
                transform.right.x * backwardForce.x,
                transform.up.y * backwardForce.y);

            // 0.1秒待機
            yield return new WaitForSeconds(0.1f);
            // プレイヤーが地面に設置するまで処理を待機
            while (isGround == false)
            {
                // isGroundがtrueになったらコルーチンを再開
                yield return null;
            }

            // 点滅アニメーション
            playerAnimator.SetTrigger("Invincible Mode");

            //位置の移動処理
            if (isFlag == true)
            {
                transform.position = flag.transform.position;
                mainCamera.transform.position = cameraPosition;
            }
            else
            {
                transform.position = startPosition;
                mainCamera.transform.position = cameraPosition;
            }     

            // ダメージ状態を終了する
            isDamaged = false;
        }

        /// <summary>
        /// UnityChanのセリフを再生するメソッド
        /// </summary>
        void PlayVoice(AudioClip audioClip)
        {
            // 今流れている音声を停止する
            audioSource.Stop();
            // timeOverVoiceのSEを再生する
            audioSource.PlayOneShot(audioClip);
        }

        /// <summary>
        /// ゲームオーバー時にUnityChanがそれに合わせたセリフを話す
        /// </summary>
        public void PlayGameOverVoice()
        {
            PlayVoice(gameOverVoice);
        }

        /// <summary>
        /// タイムクリア時にUnityChanがそれに合わせたセリフを話す
        /// </summary>
        public void PlayGameClearVoice()
        {
            PlayVoice(clearVoice);
        }

        //--------ここからしたにコードを書いていくよ！--------//
        //--------少しだけこの上に書くコードもあるから注意してね！--------//

        /// <summary>
        /// 当たり判定の処理を行う関数
        /// </summary>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Respawn")
            {
                //Debug.Log("flag!");
                isFlag = true;
                flagSquare.sprite = flagHurdle; 
            }

            // ダメージ状態であればオブジェクトの当たり判定処理を行う
            if (isDamaged == false)
            {
                if (other.gameObject.tag == "Damage")
                {
                    StartCoroutine(OnDamage());
                    playerLife -= 1;

                    if (playerLife <= 0)
                    {
                        playerLife = 0;
                        gameManager.GameOver();
                    }
                }
            }

            if (other.tag == "Goal") 
            {
                gameManager.GameClear();
            }
        }
    }
}