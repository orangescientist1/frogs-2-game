using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField]
    GameObject Enemy;
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Sword"){
            EnemyDeath();
        }    
    }
    public void EnemyDeath(){
        Destroy(Enemy);
    }
}
