using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<GameObject> platforms;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPos;
 
    private Color rayColor = Color.green;
    private bool canShoot = true;
    private int playerPlatformPos = 1;
    private int playerVision = 25;

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        CheckEnemy();
    }

    void MovePlayer() 
    {
        if (Input.GetKeyDown(KeyCode.W) && playerPlatformPos > 0 && GameManager.Instance.CanPlay())
        {
            playerPlatformPos--;
            ChangePlayerPos();
        }
        if (Input.GetKeyDown(KeyCode.S) && playerPlatformPos < platforms.Count - 1 && GameManager.Instance.CanPlay())
        {
            playerPlatformPos++;
            ChangePlayerPos();
        }
    }

    void CheckEnemy() 
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, playerVision);

        if (hit.collider != null && canShoot)
        {
            StartCoroutine("ShootRoutine");
            rayColor = Color.red;
        }
        else
        {
            rayColor = Color.green;
        }

        Debug.DrawRay(transform.position, Vector2.right * playerVision, rayColor);
    }

    void ChangePlayerPos() 
    {
        gameObject.transform.position = platforms[playerPlatformPos].transform.position;
    }
    
    IEnumerator ShootRoutine() 
    {
        Instantiate(bulletPrefab, shootPos.position, Quaternion.identity);
        GameManager.Instance.PlaySound(shootSound);
        canShoot = false;
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }
}
