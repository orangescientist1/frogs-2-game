using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField]
    GameObject Enemy;
    [SerializeField]
    GameObject ParticleEffect;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Sword"){
            EnemyDeath();
        }    
    }
    public void EnemyDeath(){
        Instantiate(ParticleEffect, Enemy.transform.position, Quaternion.identity);
        Destroy(Enemy);
    }
}
