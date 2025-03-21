using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class Character_apperance:MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] NetworkConnection network_connection;
    //this is for the character appearance
    [SerializeField] GameObject General;
    [SerializeField] GameObject X_ray;
    [SerializeField] GameObject Temperature;
    [SerializeField] GameObject High_pitch;
    [SerializeField] GameObject Dark;
    [SerializeField] GameObject Gailic_smile;


    //this is used to change images of each images
    [SerializeField] SpriteRenderer Body_part;
    [SerializeField] SpriteRenderer Face_part;
    [SerializeField] SpriteRenderer Eyes_part;
    [SerializeField] SpriteRenderer Mouth_part;
    [SerializeField] SpriteRenderer Hair_part;
    [SerializeField] SpriteRenderer Nose_part;
    [SerializeField] SpriteRenderer Temperature_part;
    [SerializeField] SpriteRenderer X_ray_part;
    [SerializeField] SpriteRenderer High_pitch_part;
    [SerializeField] SpriteRenderer Monster_part1;
    [SerializeField] SpriteRenderer Monster_part2;
    // [SerializeField] GameObject Dark_part;


    public bool Is_Monster = false;
    public string Monster_type = "";
    

    public bool Sound_Flag = false;
    public bool Light_Flag = false;

    private string camera_type="-1";//0:General, 1:X_ray, 2:Temperature, 3:High_pitch, 4:Dark, 5:Gailic_smile

    //image path of each part
    //is 2d array first is for human second is for monster
    private List<List<string>> Body_path = new List<List<string>>()
    {
        new List<string>() { "Image_human/General/body/Body1", "Image_human/General/body/Body2","Image_human/General/body/Body3","Image_human/General/body/Body4" }, // human
        new List<string>() {}  // monster
    };

    private List<List<string>> Face_path = new List<List<string>>()
    {
        new List<string>() { "Image_human/General/face/Face1", "Image_human/General/face/Face2","Image_human/General/face/Face3","Image_human/General/face/Face4" },
        new List<string>() {}
    };

    private List<List<string>> Eyes_path = new List<List<string>>()
    {
        new List<string>() { "Image_human/General/eyes/Eyes1", "Image_human/General/eyes/Eyes2","Image_human/General/eyes/Eyes3","Image_human/General/eyes/Eyes4","Image_human/General/eyes/Eyes5" },
        new List<string>() { }
    };
    private List<List<string>> Eyes_blink_path = new List<List<string>>()
    {
        new List<string>() { "Image_human/General/eyes/Eyes1.1", "Image_human/General/eyes/Eyes2.1","Image_human/General/eyes/Eyes3.1","Image_human/General/eyes/Eyes4.1","Image_human/General/eyes/Eyes5.1" },
        new List<string>() { }
    };

    private List<List<string>> Mouth_path = new List<List<string>>()
    {
        new List<string>() { "Image_human/General/mouth/Mouth1", "Image_human/General/mouth/Mouth2","Image_human/General/mouth/Mouth3","Image_human/General/mouth/Mouth4","Image_human/General/mouth/Mouth5" },
        new List<string>() { }
    };

    private List<List<string>> Hair_path = new List<List<string>>()
    {
        new List<string>() { "Image_human/General/hair/Hair1", "Image_human/General/hair/Hair2", "Image_human/General/hair/Hair3", "Image_human/General/hair/Hair4",""},
        new List<string>() { }
    };
    private List<List<string>> Nose_path = new List<List<string>>()
    {
        new List<string>() { "Image_human/General/nose/Nose1", "Image_human/General/nose/Nose2","Image_human/General/nose/Nose3","Image_human/General/nose/Nose4" },
        new List<string>() { }
    };
    private List<List<string>> X_ray_path = new List<List<string>>()
    {
        new List<string>() { "Image_human/X-ray/Human_skeleton" },
        new List<string>() { "Image_monster/X-ray/Monster_skeleton1", "Image_monster/X-ray/Monster_skeleton2","Image_monster/X-ray/Monster_skeleton3","Image_monster/X-ray/Monster_skeleton4" }
    };
    private List<List<string>> Temperature_path = new List<List<string>>()
    {
        new List<string>() { "Image_human/Heat/Human_skeleton_heat" },
        new List<string>() { "Image_monster/Heat/Monster_skeleton_heat1", "Image_monster/Heat/Monster_skeleton_heat2" }
    };
    private List<List<string>> High_pitch_path = new List<List<string>>()
    {
        new List<string>() { "Image_human/High-pitch/Human_skeleton_sound" },
        new List<string>() { "Image_monster/High-pitch/Monster_skeleton_sound1", "Image_monster/High-pitch/Monster_skeleton_sound2","Image_monster/High-pitch/Monster_skeleton_sound3" }
    };
    private string High_pitch_path_normal = "Image_human/High-pitch/Human_skeleton_sound";

    private List<List<string>> Monster_part1_path = new List<List<string>>()
    {
        new List<string>() {},
        new List<string>() {"Image_monster/General/monster_parts/Monster1","Image_monster/General/monster_parts/Monster2"}
    };
    private List<List<string>> Monster_part2_path = new List<List<string>>()
    {
        new List<string>() {},
        new List<string>() {"Image_monster/General/monster_parts/Monster3","Image_monster/General/monster_parts/Monster4","Image_monster/General/monster_parts/Monster5"}
    };

    private Dictionary<string, Vector2> partPositions = new Dictionary<string, Vector2>()
{
    // hair positions
    { "Image_human/General/hair/Hair1", new Vector2(-1.5f, 105f) },
    { "Image_human/General/hair/Hair2", new Vector2(1.6f, 104f) },
    { "Image_human/General/hair/Hair3", new Vector2(0f, 109f) },
    { "Image_human/General/hair/Hair4", new Vector2(0f, 98f) },

    // face positions
    { "Image_human/General/face/Face1", new Vector2(0f, 67f) },
    { "Image_human/General/face/Face2", new Vector2(0f, 64f) },
    { "Image_human/General/face/Face3", new Vector2(0f, 62f) },
    { "Image_human/General/face/Face4", new Vector2(0f, 64f) },

    // mouth positions
    { "Image_human/General/mouth/Mouth1", new Vector2(0f, 28.5f) },
    { "Image_human/General/mouth/Mouth2", new Vector2(0f, 28.5f) },
    { "Image_human/General/mouth/Mouth3", new Vector2(0f, 32f) },
    { "Image_human/General/mouth/Mouth4", new Vector2(1.5f, 30.6f) },
    { "Image_human/General/mouth/Mouth5", new Vector2(0f, 30f) },

    // body positions
    { "Image_human/General/body/Body1", new Vector2(0.25f, 0f) },
    { "Image_human/General/body/Body2", new Vector2(-1.5f, -5f) },
    { "Image_human/General/body/Body3", new Vector2(-1.5f, -3f) },
    { "Image_human/General/body/Body4", new Vector2(1.5f, -4.5f) },

    {"Image_monster/General/monster_parts/Monster1", new Vector2(-32f, 131f) },
    // {"Image_monster/General/monster_parts/Monster2", new Vector2(-1.5f, 0f) },
    // {"Image_monster/General/monster_parts/Monster3_0", new Vector2(-1.5f, 0f) },
    // {"Image_monster/General/monster_parts/Monster4_0", new Vector2(-1.5f, 0f) }
};


    private string body_path = "";
    private string face_path = "";
    private string eyes_path = "";
    private string eyes_blink_path = "";
    private string mouth_path = "";
    private string hair_path = "";
    private string nose_path = "";
    private string x_ray_path = "";
    private string temperature_path = "";
    private string high_pitch_path = "";
    //private string dark_path = "";
    //private string gailic_smile_path = "";
    private string monster_part1_path = "";
    private string monster_part2_path = "";


    
    public string textType_temperature, textType_sound, textType_light;



    public void Start()
    {
        CheckCameraType(camera_type);
        ChangeAppearance(true,1);
        Blink();
        
    }
    void AssignTextTypes(int day)
    {
        
        float humanProbability;
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

        if (!Is_Monster)
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
        else if ((Monster_type == "monster_part1") || (Monster_type == "monster_part2"))
        {
            return AssignTextType(monsterProbability, monsterText, neutralText);
        }
        else
        {
            return AssignTextType(monsterProbability, "human", neutralText); // if not that type, works like human
        }
    }

    public void ChangeAppearance(bool is_monster, int day)
    {
        ResetFlag();
        Is_Monster = is_monster;
        if(is_monster)
        {
            
            var (path, type) = GetMonsterPath(day);
            Monster_type = type;
            body_path = type == "body" ? path : GetRandomPath(Body_path, 0).path;
            face_path = type == "face" ? path : GetRandomPath(Face_path, 0).path;
            
            mouth_path = type == "mouth" ? path : GetRandomPath(Mouth_path, 0).path;
            hair_path = type == "hair" ? path : GetRandomPath(Hair_path, 0).path;
            nose_path = type == "nose" ? path : GetRandomPath(Nose_path, 0).path;
            x_ray_path = type == "x_ray" ? path : GetRandomPath(X_ray_path, 0).path;
            temperature_path = type == "temperature" ? path : GetRandomPath(Temperature_path, 0).path;
            high_pitch_path = type == "high_pitch" ? path : GetRandomPath(High_pitch_path, 0).path;
            monster_part1_path = type == "monster_part1" ? path : GetRandomPath(Monster_part1_path, 0).path;
            monster_part2_path = type == "monster_part2" ? path : GetRandomPath(Monster_part2_path, 0).path;
            //dark_path = type == "dark" ? path : GetRandomPath(Dark_path, 0).path;
        }
        else
        {
            //human
            Monster_type = "";
            body_path = GetRandomPath(Body_path, 0).path;
            face_path = GetRandomPath(Face_path, 0).path;
            
            mouth_path = GetRandomPath(Mouth_path, 0).path;

            // hair rando (including bald) !!! DOESN'T WORK, PLEASE FIX, I'M NOT SURE HOW
            // int hair_number = Random.Range(1, 6);
            // Debug.Log(hair_number);
            // if (hair_number != 5) ;
            // else hair_path = "";
            hair_path = GetRandomPath(Hair_path, 0).path;

            nose_path = GetRandomPath(Nose_path, 0).path;
            x_ray_path = GetRandomPath(X_ray_path, 0).path;
            temperature_path = GetRandomPath(Temperature_path, 0).path;
            high_pitch_path = GetRandomPath(High_pitch_path, 0).path;
            monster_part1_path="";
            monster_part2_path="";
            //dark_path = GetRandomPath(Dark_path, 0).path;
        }

        var (eyes_path_get, eyes_index) = GetRandomPath(Eyes_path, 0);
        eyes_path = eyes_path_get;
        eyes_blink_path =Eyes_blink_path[0][eyes_index];
       
        ChangeImage(body_path, face_path, eyes_path, eyes_blink_path, mouth_path, hair_path, nose_path, x_ray_path, temperature_path, high_pitch_path, monster_part1_path, monster_part2_path);//, dark_path);

        AssignTextTypes(day);
    }

    private void ChangeImage(string body_path, string face_path, string eyes_path, string eyes_blink_path, string mouth_path, string hair_path, string nose_path, string x_ray_path, string temperature_path, string high_pitch_path, string monster_part1_path, string monster_part2_path)
    {
        LoadSpriteFromPath(body_path, Body_part);
        LoadSpriteFromPath(face_path, Face_part);
        LoadSpriteFromPath(eyes_path, Eyes_part);
        //Eyes.GetComponent<Image>().sprite = Resources.Load<Sprite>(eyes_blink_path);
        LoadSpriteFromPath(mouth_path, Mouth_part);

        if (hair_path != "")
        {
            Hair_part.enabled = true;
            LoadSpriteFromPath(hair_path, Hair_part);
        }
        else
        {
            Hair_part.enabled = false;
        }

        if (monster_part1_path != "" && camera_type != "-1")
        {
            Monster_part1.enabled = true;
            LoadSpriteFromPath(monster_part1_path, Monster_part1);
        }
        else
        {
            Monster_part1.enabled = false;
        }

        if (monster_part2_path != "" && camera_type != "-1")
        {
            Monster_part2.enabled = true;
            LoadSpriteFromPath(monster_part2_path, Monster_part2);
        }
        else
        {
            Monster_part2.enabled = false;
        }
        LoadSpriteFromPath(nose_path, Nose_part);
        LoadSpriteFromPath(x_ray_path, X_ray_part);
        LoadSpriteFromPath(temperature_path, Temperature_part);
        LoadSpriteFromPath(high_pitch_path, High_pitch_part);
        //Dark.GetComponent<Image>().sprite = Resources.Load<Sprite>(dark_path);

        // positions
        SetPartPosition(body_path, Body_part);
        SetPartPosition(face_path, Face_part);
        SetPartPosition(mouth_path, Mouth_part);
        SetPartPosition(hair_path, Hair_part);
        SetPartPosition(monster_part1_path, Monster_part1);
        SetPartPosition(monster_part2_path, Monster_part2);
    }

    private void SetPartPosition(string path, SpriteRenderer spriteRenderer)
    {
        if (partPositions.ContainsKey(path))
        {
            spriteRenderer.transform.localPosition = partPositions[path];
        }
        // else
        // {
        //     // default position if can't be found in the dictionary
        //     spriteRenderer.transform.localPosition = Vector2.zero;
        // }
    }

    private void LoadSpriteFromPath(string path, SpriteRenderer spriteRenderer)
    {
        // 检查文件是否存在
  
        spriteRenderer.sprite = Resources.Load<Sprite>(path);
        
        
    }
    private (string path, string type) GetMonsterPath(int day)//day 1:monster_part1_path or monster_part2_path, 2:High_pitch_path or monster_part1_path or monster_part2_path ,3 and later:temperature_path or high_pitch_path or monster_part1_path or monster_part2_path
    {
        
        // var (body_path, body_index) = GetRandomPath(Body_path, 1);
        // var (face_path, face_index) = GetRandomPath(Face_path, 1);
        // var (eyes_path, eyes_index) = GetRandomPath(Eyes_path, 1);
        // var (mouth_path, mouth_index) = GetRandomPath(Mouth_path, 1);
        // var (hair_path, hair_index) = GetRandomPath(Hair_path, 1);
        // var (nose_path, nose_index) = GetRandomPath(Nose_path, 1);
        var (x_ray_path, x_ray_index) = GetRandomPath(X_ray_path, 1);
        var (temperature_path, temperature_index) = GetRandomPath(Temperature_path, 1);
        var (high_pitch_path, high_pitch_index) = GetRandomPath(High_pitch_path, 1);
        var (monster_part1_path, monster_part1_index) = GetRandomPath(Monster_part1_path, 1);
        var (monster_part2_path, monster_part2_index) = GetRandomPath(Monster_part2_path, 1);
        //var (dark_path, dark_index) = GetRandomPath(Dark_path, 1);
        List<(string path, string type)> paths;
        if(day == 1)
        {
            paths = new List<(string path, string type)>
            {
                (monster_part1_path, "monster_part1"),
                (monster_part2_path, "monster_part2")
            };
        }   
        else if(day == 2)
        {
            paths = new List<(string path, string type)>
            {
                (high_pitch_path, "high_pitch"),
                (monster_part1_path, "monster_part1"),
                (monster_part2_path, "monster_part2")
            };
        }
        else
        {
            paths = new List<(string path, string type)>
            {
            //     (body_path, "body"),
            // (face_path, "face"),
            // (eyes_path, "eyes"),
            // (mouth_path, "mouth"),
            // (hair_path, "hair"),
            // (nose_path, "nose"),
            (x_ray_path, "x_ray"),
            (temperature_path, "temperature"),
            (high_pitch_path, "high_pitch"),
            //(dark_path, "dark"),
            (monster_part1_path, "monster_part1"),
            (monster_part2_path, "monster_part2")
            };
        }

        paths.RemoveAll(path => string.IsNullOrEmpty(path.path));
        if (paths.Count > 0)
        {
            var randomPath = paths[Random.Range(0, paths.Count)];
            Debug.Log("Path: " + randomPath.path + ", Type: " + randomPath.type);
            return (randomPath.path, randomPath.type);
        }
        return (string.Empty, string.Empty);


    }
    private (string path, int index) GetRandomPath(List<List<string>> pathList, int type)
    {
        if (pathList == null || pathList.Count <= type)
        {
            return (string.Empty, 0);
        }

        List<string> specificTypePaths = pathList[type];
        if (specificTypePaths == null || specificTypePaths.Count == 0)
        {
            return (string.Empty, 0);
        }

        int randomIndex = Random.Range(0, specificTypePaths.Count);
        return (specificTypePaths[randomIndex], randomIndex);
    }


    public void CheckCameraType(string type)
    {
        camera_type=type;
        switch(type)
        {
            case "0":
                General.SetActive(true);
                X_ray.SetActive(false);
                Temperature.SetActive(false);
                High_pitch.SetActive(false);
                Dark.SetActive(false);
                Gailic_smile.SetActive(false);

                Body_part.color = Color.white;
                Face_part.color = Color.white;
                Eyes_part.color = Color.white;
                Mouth_part.color = Color.white;
                Hair_part.color = Color.white;
                Nose_part.color = Color.white;

                // Monster_part1.enabled = false;
                // Monster_part2.enabled = false;
                break;
            case "1":
                //Debug.Log("1");
                X_ray.SetActive(true);
                General.SetActive(false);
                Temperature.SetActive(false);
                High_pitch.SetActive(false);
                Dark.SetActive(false);
                Gailic_smile.SetActive(false);
                break;
            case "2":
                Temperature.SetActive(true);
                General.SetActive(false);
                X_ray.SetActive(false);
                High_pitch.SetActive(false);
                Dark.SetActive(false);
                Gailic_smile.SetActive(false);
                break;
            case "3":
                High_pitch.SetActive(true);
                General.SetActive(false);
                X_ray.SetActive(false);
                Temperature.SetActive(false);
                Dark.SetActive(false);
                Gailic_smile.SetActive(false);
                break;
            case "4":
                Dark.SetActive(true);
                General.SetActive(false);
                X_ray.SetActive(false);
                Temperature.SetActive(false);
                High_pitch.SetActive(false);
                Gailic_smile.SetActive(false);
                break;
            case "5":
                Gailic_smile.SetActive(true);
                General.SetActive(false);
                X_ray.SetActive(false);
                Temperature.SetActive(false);
                High_pitch.SetActive(false);
                Dark.SetActive(false);
                break;
            case "6":
                General.SetActive(true);
                X_ray.SetActive(false);
                Temperature.SetActive(false);
                High_pitch.SetActive(false);
                Dark.SetActive(false);
                Gailic_smile.SetActive(false);
                // change to night mode
                Body_part.color = new Color(0.1f, 0.3f, 0.1f, 1);
                Face_part.color = new Color(0.1f, 0.3f, 0.1f, 1);
                Eyes_part.color = new Color(0.1f, 0.3f, 0.1f, 1);
                Mouth_part.color = new Color(0.1f, 0.3f, 0.1f, 1);
                Hair_part.color = new Color(0.1f, 0.3f, 0.1f, 1);
                Nose_part.color = new Color(0.1f, 0.3f, 0.1f, 1);
                break;
             case "-1"://color change to black
                General.SetActive(true);
                X_ray.SetActive(false);
                Temperature.SetActive(false);
                High_pitch.SetActive(false);
                Dark.SetActive(false);
                Gailic_smile.SetActive(false);

                Body_part.color = new Color(0, 0, 0, 1);
                Face_part.color = new Color(0, 0, 0, 1);
                Eyes_part.color = new Color(0, 0, 0, 1);
                Mouth_part.color = new Color(0, 0, 0, 1);
                Hair_part.color = new Color(0, 0, 0, 1);
                Nose_part.color = new Color(0, 0, 0, 1);

                
                
                break;
            default:
                break;
        }
    }

    public void SetAppearance(string appearance)
    {
        string[] parts = appearance.Split(',');
        Debug.Log("Appearance: " + appearance);
        Debug.Log("Parts: " + parts.Length);
        body_path = parts[0];
        face_path = parts[1];
        eyes_path = parts[2];
        eyes_blink_path = parts[3];
        mouth_path = parts[4];
        hair_path = parts[5];
        nose_path = parts[6];
        x_ray_path = parts[7];
        temperature_path = parts[8];
        high_pitch_path = parts[9];
        monster_part1_path = parts[10];
        monster_part2_path = parts[11];
        //dark_path = parts[10];
        ChangeImage(body_path, face_path, eyes_path, eyes_blink_path, mouth_path, hair_path, nose_path, x_ray_path, temperature_path, high_pitch_path, monster_part1_path, monster_part2_path);
    }


    public void SetSoundFlag(){
        Sound_Flag=true;
        Boardcast_Appearance();
    }

    public void SetLightFlag(){
        Light_Flag=true;
        Boardcast_Appearance();
    }
    private void ResetFlag(){
        Sound_Flag=false;
        Light_Flag=false;
    }

    public string GetAppearance()
    {
        Debug.Log("GetAppearance: " + body_path + "," + face_path + "," + eyes_path + "," + eyes_blink_path + "," + mouth_path + "," + hair_path + "," + nose_path + "," + x_ray_path + "," + temperature_path + "," + high_pitch_path + "," + monster_part1_path + "," + monster_part2_path);
        string high_pitch= high_pitch_path;
        string monster1= monster_part1_path;
        string monster2= monster_part2_path;
        if (!Sound_Flag){
            high_pitch= High_pitch_path_normal;
        }

        if (!Light_Flag){
            monster1= "";
            monster2= "";
        }

        
        return body_path + "," + face_path + "," + eyes_path + "," + eyes_blink_path + "," + mouth_path + "," + hair_path + "," + nose_path + "," + x_ray_path + "," + temperature_path + "," + high_pitch + "," + monster1 + "," + monster2;
    }


    public void Boardcast_Appearance()
    {
        network_connection.SendMessageApparenceToClient(GetAppearance());
        //character_manager.Send_signal_to_character(GetAppearance());
    }


    // public void Blink()
    // {
    //     Transform eyesTransform = this.gameObject.transform.Find("Eyes");
    //     Debug.LogWarning("Blinking");

    //     StartCoroutine(BlinkCoroutine()); 
    // }

    // private IEnumerator BlinkCoroutine()
    // {
    //     LoadSpriteFromPath(eyes_blink_path, Eyes_part);
    //     yield return new WaitForSeconds(0.3f);
    //     LoadSpriteFromPath(eyes_path, Eyes_part);
    // }

    public void Blink()
    {
        StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            LoadSpriteFromPath(eyes_blink_path, Eyes_part);
            yield return new WaitForSeconds(0.3f);
            LoadSpriteFromPath(eyes_path, Eyes_part);
            yield return new WaitForSeconds(Random.Range(4f, 8f));
        }
    }
}
