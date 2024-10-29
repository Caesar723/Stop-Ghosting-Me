using UnityEngine;



public class Character_manager:MonoBehaviour
{
    public Character_movement character_movement;
    public Character_apperance character_apperance;
    [SerializeField] private DayManager day_manager;
    [SerializeField] private GameObject light_object;
    [SerializeField] private NetworkConnection networkConnection;


    private int max_people = 7;
    private int current_people = 0;

    private void Start()
    {
        //Debug.Log("day_manager.day:"+day_manager.day);
        Generate_Character(day_manager.day);
        Enter_Scene();
    }

    public void Enter_Scene()
    {
        character_movement.MoveToCenter();
    }
    public void Exit_Scene_Pass()
    {
        character_movement.MoveToLeft();
    }
    public void Exit_Scene_Fail()
    {
        character_movement.MoveToRight();
    }
    public void Reset_Scene()
    {
        Generate_Character(day_manager.day);
        
        
    }
    private void Generate_Character(int day)
    {
        character_movement.ReturnToRight();
        float percentage_monster = 0.65f;
        bool is_monster = Random.Range(0, 100) < percentage_monster * 100;
        character_apperance.ChangeAppearance(is_monster,day);
    }
    
    public void Check_is_monster(bool is_monster)
    {
        if (character_movement.move_to_center || character_movement.move_to_left || character_movement.move_to_right)
        {
            return;
        }
        day_manager.timer=0;
        if (is_monster){
          day_manager.ghosting+=1;
        }
        if (character_apperance.Is_Monster != is_monster)
        {
            day_manager.money-=20;
        }
        else{
            day_manager.money+=20;
        }

        if (is_monster){
          Exit_Scene_Fail();
        }
        else{
          Exit_Scene_Pass();
        }
        Reset();

        current_people++;
        Check_people();
    }

    private void Check_people()
    {
        if (current_people >= max_people)
        {
            current_people=0;
            day_manager.DayChange();
            Generate_Character(day_manager.day);
            Enter_Scene();
        }
    }

    public void Set_Sound_Flag()
    {
        
        character_apperance.SetSoundFlag();
    }
    public void Set_Light_Flag()
    {
        light_object.SetActive(false);
        character_apperance.SetLightFlag();
    }
    public void Reset()
    {
        character_apperance.Sound_Flag=false;
        character_apperance.Light_Flag=false;
        networkConnection.close_all_camera();
        light_object.SetActive(true);
    }


}
