using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparkleVFX;
    [SerializeField] int blockHealth = 2;
    [SerializeField] Sprite[] hitSprites;



    Level level;
    GameSession gameStatus;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        gameStatus = FindObjectOfType<GameSession>();
        level.CountBreakableBlocks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "Breakable")
        {
            CalculateHealth();
        }
    }

    private void CalculateHealth()
    {
        blockHealth--;
        if (blockHealth < 0)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        GetComponent<SpriteRenderer>().sprite = hitSprites[blockHealth];
    }

    private void DestroyBlock()
    {
            AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
            TriggerSparkleVFX();
            Destroy(gameObject);
            level.BlockDestroyed();
            gameStatus.AddToScore();
    }

    private void TriggerSparkleVFX()
    {
        GameObject sparkleVFX = Instantiate(blockSparkleVFX, transform.position, transform.rotation);
        Destroy(sparkleVFX, 2.0f);
    }
}
