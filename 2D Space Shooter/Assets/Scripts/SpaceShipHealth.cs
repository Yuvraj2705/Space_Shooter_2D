using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpaceShipHealth : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float maxHealth = 100;
    [SerializeField] Image healthBar;
    [SerializeField] CircleCollider2D collider2D;
    [SerializeField] ParticleSystem explosion;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        healthBar.fillAmount = health/maxHealth;
    }

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "EB")
        {
            health -= 1;
            if(health <= 0)
            {
                var Instance = Instantiate(explosion,transform.position,Quaternion.identity);
                Destroy(Instance.gameObject,1);
                Destroy(gameObject);
            }
            collider2D.enabled = false;
            anim.SetBool("hurt",true);
            yield return new WaitForSeconds(4f);
            collider2D.enabled = true;
            anim.SetBool("hurt",false);
        }
    }

    IEnumerator NextScene()
    {
        Debug.Log("next");
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Menu");
    }
}
