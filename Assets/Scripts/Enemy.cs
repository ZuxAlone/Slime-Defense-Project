using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private AudioClip hitSound;
    [SerializeField] private Material normalMat;
    [SerializeField] private Material flashMat;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject healthBarObj;
    [SerializeField] private float speed = 2f;

    private SpriteRenderer enemyRenderer;
    private int maxHealth;
    private int currentHealth;
    private int points = 10;

    // Start is called before the first frame update
    void Start()
    {
        enemyRenderer = GetComponent<SpriteRenderer>();
        maxHealth = 60;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBarObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x < -18.5) Destroy(gameObject);
    }

    IEnumerator FlashRoutine() 
    {
        enemyRenderer.material = flashMat;
        yield return new WaitForSeconds(0.1f);
        enemyRenderer.material = normalMat;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Bullet") 
        {
            StartCoroutine("FlashRoutine");
            currentHealth -= 20;
            healthBar.SetCurrentHealth(currentHealth);
            GameManager.Instance.PlaySound(hitSound);
            if (currentHealth == 0) 
            {
                GameManager.Instance.UpdateScore(points);
                Destroy(gameObject);
            }
            if (currentHealth < maxHealth && !healthBarObj.activeSelf) healthBarObj.SetActive(true);
            Destroy(other.gameObject);
        }
    }
}
