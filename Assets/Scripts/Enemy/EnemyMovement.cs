using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour{

    WaveConfig waveConfig;
    List<Transform> waypoints;

    private float moveSpeed;
    private int waypointIndex = 0;

    void Start(){
        moveSpeed = waveConfig.GetMoveSpeed() * Time.deltaTime;
        waypoints = waveConfig.GetWaypoints();

        transform.position = waypoints[waypointIndex].transform.position;
    }

    void Update(){
        EnemyMove();
    }

    public void SetWaveConfig(WaveConfig waveConfig){
        this.waveConfig = waveConfig;
    }

    private void EnemyMove(){
      if (waypointIndex <= waypoints.Count - 1){
          var targetPosition = waypoints[waypointIndex].transform.position;
          var movementThisFrame = moveSpeed * Time.deltaTime;
          transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

          if (transform.position == targetPosition){
              waypointIndex++;
          }
      } else{
          Destroy(gameObject);
      }
    }
}
