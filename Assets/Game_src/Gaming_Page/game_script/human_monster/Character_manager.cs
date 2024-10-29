using UnityEngine;



public class Character_manager:MonoBehaviour
{
    public Character_movement character_movement;
    public Character_apperance character_apperance;
    [SerializeField] private DayManager day_manager;


    private int max_people = 7;
    private int current_people = 0;

    private void Start()
    {
        Debug.Log("day_manager.day:"+day_manager.day);
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
        float percentage_monster = 0.8f;
        bool is_monster = Random.Range(0, 100) < percentage_monster * 100;
        character_apperance.ChangeAppearance(is_monster,day);
    }
    
    public void Check_is_monster(bool is_monster)
    {
        if (character_movement.move_to_center || character_movement.move_to_left || character_movement.move_to_right)
        {
            return;
        }
        if (character_apperance.Is_Monster != is_monster)
        {
            
        }
        else{
            
        }

        if (is_monster){
          Exit_Scene_Fail();
        }
        else{
          Exit_Scene_Pass();
        }

        current_people++;
        Check_people();
    }

    private void Check_people()
    {
        if (current_people >= max_people)
        {
            day_manager.DayChange();
            Generate_Character(day_manager.day);
            Enter_Scene();
        }
    }
}
