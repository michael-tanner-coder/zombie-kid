using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotate : MonoBehaviour
{
   [SerializeField] public EnemyAttributes enemy;
   private float _rotationTimer;

   void Update()
   {
        _rotationTimer += enemy.RotationFrequency.CurrentValue * Time.deltaTime;

        if (enemy.RotationFrequency.CurrentValue > 0f &&  _rotationTimer >= 1f)
        {
            // rotate the enemy gameobject
            transform.Rotate(0f,0f,90f);

            // reset the timer
            _rotationTimer = 0f;
        }
   }
}
