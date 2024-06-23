using System;
using System.Collections;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    // どのような効果音を出すか決めるものAudioClip
    public AudioClip hitClip;
    // 音を流す為のAudioSource
    public AudioSource audioSource;

    public GameObject brokenBlock;

    public bool canBreak;

    /// <summary>
    /// 当たり判定の処理を行う関数
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        // もしtagが「Player」のGameObjectと接触したなら
        if (collision2D.gameObject.tag == "Player")
        {
            if (canBreak == true)
            {
                GameObject broken = Instantiate(brokenBlock, transform.position, transform.rotation);
                broken.transform.localScale = transform.lossyScale;
                Destroy(gameObject);
            }
            else
            {
                // 衝突音を再生する
                audioSource.PlayOneShot(hitClip);
            }

        }
    }
}

