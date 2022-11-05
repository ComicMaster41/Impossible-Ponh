using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    public GameObject hitSFX;
    public GameObject ball_prefab;
    public int ball_count = 0;


    public BallMovement ballMovement;
    public ScoreManager scoreManager;


    private void Bounce(Collision2D collision)
    {
        Vector3 ballPosition = transform.position;
        Vector3 racketPosition = collision.transform.position;
        float racketHeight = collision.collider.bounds.size.y;

        float positionX;
        if (collision.gameObject.name == "Player 1")
        {
            positionX = 1;
        }

        else
        {
            positionX = -1;
        }

        float positionY = (ballPosition.y - racketPosition.y) / racketHeight;

        ballMovement.IncreaseHitCounter();
        ballMovement.MoveBall(new Vector2(positionX, positionY));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player 1" || collision.gameObject.name == "Player 2")
        {
            Bounce(collision);
            if (ball_count <= 4)
            {
                InvokeRepeating("SpawnMultipleBalls", 1, 1);
            }
            else{
                CancelInvoke("SpawnMultipleBalls");
            }
        }

        else if (collision.gameObject.name == "Right Border")
        {
            scoreManager.Player1Goal();
            ballMovement.player1Start = false;
            StartCoroutine(ballMovement.Launch());
        }

        else if (collision.gameObject.name == "Left Border")
        {
            scoreManager.Player2Goal();
            ballMovement.player1Start = false;
            StartCoroutine(ballMovement.Launch());
        }

        Instantiate(hitSFX, transform.position, transform.rotation);
    }

    void SpawnMultipleBalls(){
        Instantiate(ball_prefab);
        ball_count++;
    }
}
