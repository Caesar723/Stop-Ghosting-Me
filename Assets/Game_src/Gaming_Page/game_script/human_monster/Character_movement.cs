using Unity.Netcode;
using UnityEngine;


public class Character_movement : NetworkBehaviour
{ 

    public Camera camera;
    public bool move_to_center = false;
    public bool move_to_left = false;
    public bool move_to_right = false;
    private float percentage_to_reach = 0;//1:left 0:right 0.5:center
    private float speed = 0.08f;
    [SerializeField] private Character_manager character_manager;
    //private float distance_x_offset = 0;
    //private float distance_x_count = 0;

    private void Update()
    {
        if(move_to_center || move_to_left || move_to_right)
        {
            MoveBasedOnCamera();
            CheckToDestination();
        }
    }

    private void MoveBasedOnCamera()
    {
        float time_delay = Time.deltaTime;
        float time_scale = 0.005f;
        
        //float cameraX = Camera.main.transform.position.x;
        float newY = Mathf.Sin(Time.time * 5f) * time_scale; // up and down
        //percentage_to_reach =percentage_to_reach + time_delay*speed;
        if(move_to_right)
        {
            percentage_to_reach =percentage_to_reach - time_delay*speed;
        }
        else{
          percentage_to_reach =percentage_to_reach + time_delay*speed;
        }
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
                
                percentage_to_reach = 0.5f;
                Reset();
            }
        }
        else if(move_to_left)
        {
            if(percentage_to_reach >= 1)
            {
                

                
                percentage_to_reach = 1f;
                Reset();
                character_manager.Reset_Scene();
                ReturnToRight();
                character_manager.Enter_Scene();
            }
        }
        else if(move_to_right)
        {
            if(percentage_to_reach <= 0)
            {
                
                
                percentage_to_reach = 0f;
                Reset();
                character_manager.Reset_Scene();
                ReturnToRight();
                character_manager.Enter_Scene();
            }
        }
    }
    private void Reset(){
        move_to_center = false;
        move_to_left = false;
        move_to_right = false;

        
    }

    

    public void MoveToCenter()
    {
        
        
        move_to_center = true;
    }

    public void MoveToLeft()
    {
        
        move_to_left = true;
    }
    public void MoveToRight()
    {
        
        move_to_right = true;
    }
    public void ReturnToRight()
    {
        percentage_to_reach = 0f;
        // float cameraRight = camera.transform.position.x + camera.orthographicSize * camera.aspect;
        // transform.position = new Vector3(cameraRight, transform.position.y, 0);
    }
}
