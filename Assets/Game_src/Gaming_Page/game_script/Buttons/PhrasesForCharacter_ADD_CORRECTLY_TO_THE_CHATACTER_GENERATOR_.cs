using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public DayManager dayManager; // assign in inspector or use FindObjectOfType<DayManager>() if needed
    public string textType_temperature, textType_sound, textType_light;

    bool is_monster; // this and the one below already exist in the code
    string Monster_type; // "high_pitch", "dark", "temperature" for types

    void Start()
    {
        AssignTextTypes(); // use this where you generate people
    }

    void AssignTextTypes()
    {
        int day = dayManager.day;
        float humanProbability, neutralProbability;
        switch (day)    // probability of getting text prompt
        {
            case 1:
                humanProbability = 1.0f; // 100% "human"
                break;
            case 2:
                humanProbability = 1.0f; // 100% "human"
                break;
            case 3:
                humanProbability = 0.8f; // 80% "human", 20% "neutral"
                break;
            case 4:
                humanProbability = 0.6f; // 60% "human", 40% "neutral"
                break;
            case 5:
                humanProbability = 0.4f; // 40% "human", 60% "neutral"
                break;
            case 6:
                humanProbability = 0.2f; // 20% "human", 80% "neutral"
                break;
            default:
                humanProbability = 0.0f;
                break;
        }

        if (!is_monster)
        {
            textType_temperature = AssignTextType(humanProbability, "human", "neutral");
            textType_sound = AssignTextType(humanProbability, "human", "neutral");
            textType_light = AssignTextType(humanProbability, "human", "neutral");
        }
        else
        {
            textType_temperature = AssignMonsterTextType("temperature", humanProbability, "monster", "neutral");
            textType_sound = AssignMonsterTextType("high_pitch", humanProbability, "monster", "neutral");
            textType_light = AssignMonsterTextType("dark", humanProbability, "monster", "neutral");
        }
    }

    string AssignTextType(float humanProbability, string humanText, string neutralText)
    {
        return Random.value < humanProbability ? humanText : neutralText;
    }

    string AssignMonsterTextType(string correspondingType, float monsterProbability, string monsterText, string neutralText)
    {
        if (Monster_type == correspondingType)
        {
            return AssignTextType(monsterProbability, monsterText, neutralText);
        }
        else
        {
            return AssignTextType(monsterProbability, "human", neutralText); // if not that type, works like human
        }
    }
}
