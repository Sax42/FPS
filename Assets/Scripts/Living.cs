using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Living: MonoBehaviour
{
    public float startHealth = 1;
    public float maxHealth = 1000000f;
    public GameObject deathParticle;
    public GameObject spawnParticle;
    public float health { get; private set; }

    // Start is called before the first frame update
    protected void Start()
    {
        health = startHealth;
        if(spawnParticle != null)

            Instantiate(spawnParticle,transform.position,transform.rotation);
    }

    public void ChangeHealth(float pValueChange)
    {
        health = health + pValueChange <= maxHealth ? health + pValueChange : maxHealth;
        if(health <= 0)
            Destroy(gameObject);
    }

    // Update is called once per frame
    protected void Update()
    {
    }

    private void OnDestroy()
    {
        if(deathParticle != null)
            Instantiate(deathParticle,transform.position,transform.rotation);
    }
}