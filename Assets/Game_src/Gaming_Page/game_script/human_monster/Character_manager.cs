using UnityEngine;



public class Character_manager:MonoBehaviour
{
    public Character_movement character_movement;
    public Character_apperance character_apperance;

    public void Enter_Scene()
    {
        character_movement.MoveToCenter();
    }
    public void Exit_Scene()
    {
        character_movement.MoveToLeft();
    }
    public void Generate_Character(int day)
    {
        character_movement.ReturnToRight();
        float percentage_monster = 0.8f;
        bool is_monster = Random.Range(0, 100) < percentage_monster * 100;
        character_apperance.ChangeAppearance(is_monster,day);
    }
}
