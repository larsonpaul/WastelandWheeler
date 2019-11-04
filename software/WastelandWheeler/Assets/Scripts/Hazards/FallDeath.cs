using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDeath : MonoBehaviour
{
    public float duration = .1f;
    public float angle = 90;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Fall(collision));
        }
    }

    IEnumerator Fall(Collider2D player)
    {
        //player_movementFORCE player_sprite = player.GetComponent<player_movement>();
        SpriteRenderer spr = player.GetComponent<SpriteRenderer>();
        Rigidbody2D r_body = player.GetComponent<Rigidbody2D>();

        // Move player more into the hole and spin the sprites
        //r_body.MovePosition(r_body.position + player_sprite.movement * 1.25f);
        //player_sprite.enabled = false;
        //spr.sprite = player_sprite.img1;
        //yield return new WaitForSeconds(.25f);
        //spr.sprite = player_sprite.img2;
        //yield return new WaitForSeconds(.25f);
        //spr.sprite = player_sprite.img3;
        //yield return new WaitForSeconds(.25f);
        //spr.sprite = player_sprite.img4;
        //yield return new WaitForSeconds(.25f);
        //spr.sprite = player_sprite.img5;
        //yield return new WaitForSeconds(.25f);
        //spr.sprite = player_sprite.img6;
        //yield return new WaitForSeconds(.25f);
        //spr.sprite = player_sprite.img7;
        //yield return new WaitForSeconds(.25f);
        //spr.sprite = player_sprite.img8;

        // Destroy the player
        player.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(r_body);

        // Wait half a second and go back to the main menu
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(0);
        Debug.Log("Game Over!");

    }
}
