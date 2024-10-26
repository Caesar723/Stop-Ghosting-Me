using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;

public class Character_apperance:MonoBehaviour
{
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
    // [SerializeField] GameObject Dark_part;
    // [SerializeField] GameObject Gailic_smile_part;


    private bool Is_Monster = false;
    private string Monster_type = "";
    



    private string camera_type="0";//0:General, 1:X_ray, 2:Temperature, 3:High_pitch, 4:Dark, 5:Gailic_smile

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
        new List<string>() { "Image_human/General/hair/Hair1", "Image_human/General/hair/Hair2", "Image_human/General/hair/Hair3", "Image_human/General/hair/Hair4"},
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
    private List<List<string>> Dark_path = new List<List<string>>()
    {
        new List<string>() {},
        new List<string>() {}
    };
    private List<List<string>> Gailic_smile_path = new List<List<string>>()
    {
        new List<string>() { },
        new List<string>() { }
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
    { "Image_human/General/body/Body4", new Vector2(1.5f, -4.5f) }
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
    private string dark_path = "";
    private string gailic_smile_path = "";


    public void Start()
    {
        CheckCameraType(camera_type);
        ChangeAppearance(true);
        
    }

    public void ChangeAppearance(bool is_monster)
    {
        // string body_path = "";
        // string face_path = "";
        // string mouth_path = "";
        // string hair_path = "";
        // string nose_path = "";
        // string x_ray_path = "";
        // string temperature_path = "";
        // string high_pitch_path = "";
        // string dark_path = "";
        // string gailic_smile_path = "";
        Is_Monster = is_monster;
        if(is_monster)
        {
            
            var (path, type) = GetMonsterPath();
            Monster_type = type;
            body_path = type == "body" ? path : GetRandomPath(Body_path, 0).path;
            face_path = type == "face" ? path : GetRandomPath(Face_path, 0).path;
            
            mouth_path = type == "mouth" ? path : GetRandomPath(Mouth_path, 0).path;
            hair_path = type == "hair" ? path : GetRandomPath(Hair_path, 0).path;
            nose_path = type == "nose" ? path : GetRandomPath(Nose_path, 0).path;
            x_ray_path = type == "x_ray" ? path : GetRandomPath(X_ray_path, 0).path;
            temperature_path = type == "temperature" ? path : GetRandomPath(Temperature_path, 0).path;
            high_pitch_path = type == "high_pitch" ? path : GetRandomPath(High_pitch_path, 0).path;
            dark_path = type == "dark" ? path : GetRandomPath(Dark_path, 0).path;
            gailic_smile_path = type == "gailic_smile" ? path : GetRandomPath(Gailic_smile_path, 0).path;
        }
        else
        {
            //human
            Monster_type = "";
            body_path = GetRandomPath(Body_path, 0).path;
            face_path = GetRandomPath(Face_path, 0).path;
            
            mouth_path = GetRandomPath(Mouth_path, 0).path;

            // hair rando (including bald)
            int hair_number = Random.Range(1, 6);
            Debug.Log(hair_number);
            if (hair_number != 5) hair_path = GetRandomPath(Hair_path, 0).path;
            else hair_path = "";

            nose_path = GetRandomPath(Nose_path, 0).path;
            x_ray_path = GetRandomPath(X_ray_path, 0).path;
            temperature_path = GetRandomPath(Temperature_path, 0).path;
            high_pitch_path = GetRandomPath(High_pitch_path, 0).path;
            dark_path = GetRandomPath(Dark_path, 0).path;
            gailic_smile_path = GetRandomPath(Gailic_smile_path, 0).path;
        }

        var (eyes_path_get, eyes_index) = GetRandomPath(Eyes_path, 0);
        eyes_path = eyes_path_get;
        eyes_blink_path =Eyes_blink_path[0][eyes_index];
       
        ChangeImage(body_path, face_path, eyes_path, eyes_blink_path, mouth_path, hair_path, nose_path, x_ray_path, temperature_path, high_pitch_path);//, dark_path, gailic_smile_path);

    }

    private void ChangeImage(string body_path, string face_path, string eyes_path, string eyes_blink_path, string mouth_path, string hair_path, string nose_path, string x_ray_path, string temperature_path, string high_pitch_path)
    {
        LoadSpriteFromPath(body_path, Body_part);
        LoadSpriteFromPath(face_path, Face_part);
        LoadSpriteFromPath(eyes_path, Eyes_part);
        //Eyes.GetComponent<Image>().sprite = Resources.Load<Sprite>(eyes_blink_path);
        LoadSpriteFromPath(mouth_path, Mouth_part);
        LoadSpriteFromPath(hair_path, Hair_part);
        LoadSpriteFromPath(nose_path, Nose_part);
        LoadSpriteFromPath(x_ray_path, X_ray_part);
        LoadSpriteFromPath(temperature_path, Temperature_part);
        LoadSpriteFromPath(high_pitch_path, High_pitch_part);
        //Dark.GetComponent<Image>().sprite = Resources.Load<Sprite>(dark_path);
        //Gailic_smile.GetComponent<Image>().sprite = Resources.Load<Sprite>(gailic_smile_path);

        // positions
        SetPartPosition(body_path, Body_part);
        SetPartPosition(face_path, Face_part);
        SetPartPosition(mouth_path, Mouth_part);
        SetPartPosition(hair_path, Hair_part);
    }

    private void SetPartPosition(string path, SpriteRenderer spriteRenderer)
    {
        if (partPositions.ContainsKey(path))
        {
            spriteRenderer.transform.localPosition = partPositions[path];
        }
        else
        {
            // default position if can't be found in the dictionary
            spriteRenderer.transform.localPosition = Vector2.zero;
        }
    }

    private void LoadSpriteFromPath(string path, SpriteRenderer spriteRenderer)
    {
        // 检查文件是否存在
  
        spriteRenderer.sprite = Resources.Load<Sprite>(path);
        
        
    }
    private (string path, string type) GetMonsterPath()
    {
        
        var (body_path, body_index) = GetRandomPath(Body_path, 1);
        var (face_path, face_index) = GetRandomPath(Face_path, 1);
        var (eyes_path, eyes_index) = GetRandomPath(Eyes_path, 1);
        var (mouth_path, mouth_index) = GetRandomPath(Mouth_path, 1);
        var (hair_path, hair_index) = GetRandomPath(Hair_path, 1);
        var (nose_path, nose_index) = GetRandomPath(Nose_path, 1);
        var (x_ray_path, x_ray_index) = GetRandomPath(X_ray_path, 1);
        var (temperature_path, temperature_index) = GetRandomPath(Temperature_path, 1);
        var (high_pitch_path, high_pitch_index) = GetRandomPath(High_pitch_path, 1);
        var (dark_path, dark_index) = GetRandomPath(Dark_path, 1);
        var (gailic_smile_path, gailic_smile_index) = GetRandomPath(Gailic_smile_path, 1);

        List<(string path, string type)> paths = new List<(string path, string type)>
        {
            (body_path, "body"),
            (face_path, "face"),
            (eyes_path, "eyes"),
            (mouth_path, "mouth"),
            (hair_path, "hair"),
            (nose_path, "nose"),
            (x_ray_path, "x_ray"),
            (temperature_path, "temperature"),
            (high_pitch_path, "high_pitch"),
            (dark_path, "dark"),
            (gailic_smile_path, "gailic_smile")
        };

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
        switch(type)
        {
            case "0":
                General.SetActive(true);
                X_ray.SetActive(false);
                Temperature.SetActive(false);
                High_pitch.SetActive(false);
                Dark.SetActive(false);
                Gailic_smile.SetActive(false);
                break;
            case "1":
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
        //dark_path = parts[10];
        //gailic_smile_path = parts[11];
        ChangeImage(body_path, face_path, eyes_path, eyes_blink_path, mouth_path, hair_path, nose_path, x_ray_path, temperature_path, high_pitch_path);
    }

    public string GetAppearance()
    {
        Debug.Log("GetAppearance: " + body_path + "," + face_path + "," + eyes_path + "," + eyes_blink_path + "," + mouth_path + "," + hair_path + "," + nose_path + "," + x_ray_path + "," + temperature_path + "," + high_pitch_path);
        return body_path + "," + face_path + "," + eyes_path + "," + eyes_blink_path + "," + mouth_path + "," + hair_path + "," + nose_path + "," + x_ray_path + "," + temperature_path + "," + high_pitch_path;
    }
    
    
    
}
