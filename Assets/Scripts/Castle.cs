using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{

    [SerializeField] private Material normalMat;
    [SerializeField] private Material flashMat;
    [SerializeField] private Animator castleAnimator;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip destroySound;
    [SerializeField] private HealthBar healthBar;

    private SpriteRenderer castleRenderer;
    private int maxHealth;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        castleRenderer = GetComponent<SpriteRenderer>();
        maxHealth = 100;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    IEnumerator CastleHitRoutine() 
    {
        castleAnimator.SetTrigger("HitTrigger");
        yield return new WaitForSeconds(0.3f);
        castleAnimator.SetTrigger("HitTrigger");
    }

    IEnumerator FlashRoutine()
    {
        castleRenderer.material = flashMat;
        yield return new WaitForSeconds(0.1f);
        castleRenderer.material = normalMat;
        yield return new WaitForSeconds(0.1f);
        castleRenderer.material = flashMat;
        yield return new WaitForSeconds(0.1f);
        castleRenderer.material = normalMat;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Enemy") 
        {
            currentHealth -= 20;
            healthBar.SetCurrentHealth(currentHealth);
            if (currentHealth > 0)
            {
                StartCoroutine("CastleHitRoutine");
                StartCoroutine("FlashRoutine");
                GameManager.Instance.PlaySound(hitSound);
            }
            if (currentHealth == 0)
            {
                castleAnimator.SetBool("IsDestroyed", true);
                GameManager.Instance.PlaySound(destroySound);
                GameManager.Instance.GameOver();    
            }
            Destroy(other.gameObject);
        }
    }
}
