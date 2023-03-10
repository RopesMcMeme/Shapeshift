using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;

    //playerdata
    public int playerHealth;
    public Vector3 playerPosition;
    public bool isPlayerWorking;

    //enemydata
    public Vector3 enemyPosition;
    public bool isEnemyAggressive;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SavePlayerData
    {
        public int playerHealth;
        public Vector3 playerPosition;
        public bool isPlayerWorking;
    }
    [System.Serializable]
    class SaveEnemyData
    {
        public Vector3 enemyPosition;
        public bool isEnemyAggressive;
    }
    [System.Serializable]
    class SaveLearnedShapes
    {
        public List<string> shapeNames = new List<string>();
    }

    public void SaveGameData()
    {
        SavePlayerData playerData = new SavePlayerData();
        SaveEnemyData enemyData = new SaveEnemyData();

        playerData.playerHealth = playerHealth;
        playerData.playerPosition = playerPosition;
        playerData.isPlayerWorking = isPlayerWorking;

        enemyData.enemyPosition = enemyPosition;
        enemyData.isEnemyAggressive = isEnemyAggressive;

        string jsonPlayer = JsonUtility.ToJson(playerData);
        string jsonEnemy = JsonUtility.ToJson(enemyData);

        System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile_player.json", jsonPlayer);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile_enemy.json", jsonEnemy);
    }

    public void SaveList(List<string> shapes)
    {
        SaveLearnedShapes learnedShapes = new SaveLearnedShapes();
        learnedShapes.shapeNames = shapes;
        string jsonKnownShapes = JsonUtility.ToJson(learnedShapes);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile_shapes.json", jsonKnownShapes);
    }

    public void LoadGameData()
    {
        string pathPlayer = Application.persistentDataPath + "/savefile_player.json";
        string pathEnemy = Application.persistentDataPath + "/savefile_enemy.json";
        string pathKnownShapes = Application.persistentDataPath + "/savefile_shapes.json";

        if (System.IO.File.Exists(pathPlayer) && System.IO.File.Exists(pathEnemy))
        {
            string jsonPlayer = System.IO.File.ReadAllText(pathPlayer);
            string jsonEnemy = System.IO.File.ReadAllText(pathEnemy);

            SavePlayerData playerData = JsonUtility.FromJson<SavePlayerData>(jsonPlayer);
            SaveEnemyData enemyData = JsonUtility.FromJson<SaveEnemyData>(jsonEnemy);

            playerData.playerHealth = playerHealth;
            playerData.playerPosition = playerPosition;
            playerData.isPlayerWorking = isPlayerWorking;

            enemyData.enemyPosition = enemyPosition;
            enemyData.isEnemyAggressive = isEnemyAggressive;
        }

        if (System.IO.File.Exists(pathKnownShapes))
        {
            string jsonShapes = System.IO.File.ReadAllText(pathKnownShapes);

            SaveLearnedShapes learnedShapes = JsonUtility.FromJson<SaveLearnedShapes>(jsonShapes);
        }
    }

}
