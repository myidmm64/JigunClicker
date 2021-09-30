using System.IO;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private string SAVE_PATH = "";
    private string SAVE_FILENAME = "/SaveFile.txt";
    
    [SerializeField]
    private User user = null;
    public User CurrentUser { get { return user; } }
    private UIManager uiManager = null;
    public UIManager UI { get{ return uiManager; } }
    [SerializeField]
    private HPBarUI hpBarUI = null;
    public HPBarUI barUI { get { return hpBarUI; } }
    [SerializeField]
    private EnemyManager enemyManager = null;
    public EnemyManager CurrentEnemyManager { get { return enemyManager; } }

    private void Awake()
    {
        SAVE_PATH = Application.dataPath + "/Save";
       // SAVE_PATH = Application.persistentDataPath + "/Save";
       //세이브 속도 조절
        if (Directory.Exists(SAVE_PATH) == false)
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
        InvokeRepeating("SaveToJson", 1f , 30f);
        InvokeRepeating("EarnEnergyPerSecond", 0f, 1f);
        LoadFromJson();
        uiManager = GetComponent<UIManager>();
    }
    private void EarnEnergyPerSecond()
    {
        foreach (Soldier soldier in user.soldierList)
        {
            if(soldier.amount>0)
                user.energy += (long)(soldier.ePs + ((soldier.ePs * 0.7f) * (soldier.amount - 1)));
        }   
        UI.UpdateEnergyPanel();
    }

    private void LoadFromJson()
    {
        string json = "";
        if (File.Exists(SAVE_PATH + SAVE_FILENAME) == true)
        {
            json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            user = JsonUtility.FromJson<User>(json);
        }
    }
    private void SaveToJson()
    {
        Debug.Log("저장됩니다.");
        string json = JsonUtility.ToJson(user, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
    }
    private void OnApplicationQuit()
    {
        SaveToJson();
    }
}
