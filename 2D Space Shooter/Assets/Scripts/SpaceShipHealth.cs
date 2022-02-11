using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipHealth : MonoBehaviour
{
    [SerializeField] public float health = 100;
    [SerializeField] public float maxHealth = 100;
    [SerializeField] Image healthBar;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "EB")
        {
            health -= 1;
            Destroy(other.gameObject);
            if(health <= 0)
            {
                var Instance = Instantiate(explosion,transform.position,Quaternion.identity);
                Destroy(Instance.gameObject,1);
                Destroy(gameObject);
            }
        }

        if(other.gameObject.tag == "Enemy")
        {
            health -= 1;
            Destroy(other.gameObject);
            if(health <= 0)
            {
                var Instance = Instantiate(explosion,transform.position,Quaternion.identity);
                Destroy(Instance.gameObject,1);
                Destroy(gameObject);
            }
        }
    }
}
