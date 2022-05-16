using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour{
public float time = 60.0f;
public PlayerMovement pm;
 
void Update(){
 
 time -= Time.deltaTime;
 
 if (time <= 0.0f)
 {
    timerEnded();
 }
 
 }
 
void timerEnded(){
   pm.Frogify();
}
}
