using Unity.Netcode;
using UnityEngine;


public class Character_movement : NetworkBehaviour
{ 

    public Camera camera;
    private bool move_to_center = false;
    private bool move_to_left = false;
    private float percentage_to_reach = 0;//1:left 0:right 0.5:center
    private float speed = 0.05f;
    //private float distance_x_offset = 0;
    //private float distance_x_count = 0;

    private void Update()
    {
        if(move_to_center || move_to_left)
        {
            MoveBasedOnCamera();
            CheckToDestination();
        }
    }

    private void MoveBasedOnCamera()
    {
        float time_delay = Time.deltaTime;
        float time_scale = 0.5f;
        
        //float cameraX = Camera.main.transform.position.x;
        float newY = Mathf.Sin(Time.time * 2) * time_scale; // up and down
        percentage_to_reach =percentage_to_reach + time_delay*speed;
        float cameraRight = camera.transform.position.x + camera.orthographicSize * camera.aspect;
        float cameraLeft = camera.transform.position.x - camera.orthographicSize * camera.aspect;
        float distance_x = cameraLeft - cameraRight;
        transform.position = new Vector3(cameraRight+distance_x * percentage_to_reach, transform.position.y+newY, 0);
        
    }

    private void CheckToDestination()
    {
        if(move_to_center)
        {
            if(percentage_to_reach >= 0.5)
            {
                Reset();
                percentage_to_reach = 0.5f;
            }
        }
        else if(move_to_left)
        {
            if(percentage_to_reach >= 1)
            {
                Reset();
                percentage_to_reach = 1f;
            }
        }
    }
    private void Reset(){
        move_to_center = false;
        move_to_left = false;
        
    }

    

    public void MoveToCenter()
    {
        
        
        move_to_center = true;
    }

    public void MoveToLeft()
    {
        
        move_to_left = true;
    }
    public void ReturnToRight()
    {
        
        float cameraRight = camera.transform.position.x + camera.orthographicSize * camera.aspect;
        transform.position = new Vector3(cameraRight, transform.position.y, 0);
    }
}
