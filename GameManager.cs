using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public Transform ParentCoins;


    public int CoinCollected;
    private int CoinParentChildCount;

    public int TotalCoins;
    public Transform PlayerTransform;
    [SerializeField] private Stack<GameObject> activatedChecpointStack = new Stack<GameObject>();

    private string SavePath;


    // singleton yapılacak
    //GMinstance != null → daha önce bir GameManager var mı?
    //GMinstance != this → bu obje o eski instance değil mi?
    public static GameManager GMinstance;


    void Awake()
    {
        if (GMinstance != null && GMinstance != this)
        {
            Destroy(gameObject);
            return;
        }
        GMinstance = this;
        DontDestroyOnLoad(gameObject);

        SavePath = Path.Combine(Application.persistentDataPath, "checpointSave.json");
    }

    void Start()
    {

        CoinParentChildCount = ParentCoins.childCount;

        /* print(PlayerPrefs.GetInt("TotalCoin", 0).ToString()); */

        LoadGame();

    }

    public void SaveGame()
    {

        ChecpointDataModel data = new ChecpointDataModel();
        data.totalCoin = TotalCoins;
        foreach (var cp in activatedChecpointStack)
        {
            TerrainChecPoint tCp = cp.GetComponent<TerrainChecPoint>();
            data.checkpoints.Add(new CheckPointData(cp.transform.position, tCp.checpointIndex));
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
        Debug.Log("Checpoints  at: " + SavePath);
    }
    public void LoadGame()
    {



        if (!File.Exists(SavePath)) return;

        string json = File.ReadAllText(SavePath);
        ChecpointDataModel data = JsonUtility.FromJson<ChecpointDataModel>(json);

        TotalCoins = data.totalCoin;
        activatedChecpointStack.Clear();

        foreach (var cpData in data.checkpoints)
        {
            TerrainChecPoint cpObj = FindCheckpointByIndex(cpData.index);
            if (cpObj != null)
            {
                activatedChecpointStack.Push(cpObj.gameObject);
            }
        }


        if (activatedChecpointStack.Count > 0)
        {
            PlayerTransform.position = activatedChecpointStack.Peek().transform.position;
        }
        Debug.Log("Game loaded. TotalCoins: " + TotalCoins);
    }

    private TerrainChecPoint FindCheckpointByIndex(int index)
    {
        TerrainChecPoint[] allCp = GameObject.FindObjectsByType<TerrainChecPoint>(FindObjectsSortMode.InstanceID);
        foreach (var cp in allCp)
        {
            if (cp.checpointIndex == index)
                return cp;
        }
        return null;
    }

    void Update()
    {

    }
    public void DiePlayer()
    {

        Time.timeScale = 0;

    }


    public void CoinIncrease()
    {
        CoinCollected++;
        // TotalCoins++;

        UIManager.UIinstance.CoinTextCollectedCoinCount(CoinCollected);

        // PlayerPrefs.SetInt("TotalCoin", TotalCoins);
        // PlayerPrefs.Save();

        if (AllCoinCollected())
        {
            print("Tüm coinler toplandı");

        }
    }

    public bool AllCoinCollected()
    {

        return CoinParentChildCount == CoinCollected;

    }

    public void AddActivatedChecpoint(GameObject checpoint)
    {
        if(checpoint != null) return; 
        if (activatedChecpointStack.Contains(checpoint)) return;

        activatedChecpointStack.Push(checpoint);

    }

    public Vector3 GetLastChecpointPosition()
    {
        if (activatedChecpointStack.Count == 0)
        {
            return Vector3.zero;
        }

        return activatedChecpointStack.Peek().transform.position;
    }

    public int LastActivatedChecpointIndex()
    {
        if (activatedChecpointStack.Count == 0) return -1;
        TerrainChecPoint lastCp = activatedChecpointStack.Peek().GetComponent<TerrainChecPoint>();
        return lastCp.checpointIndex;

    }

    // İsteğe bağlı Son checpointi kaldırmak için
    public void RemoveLastChecpoin()
    {
        if (activatedChecpointStack.Count > 0)
        {
            activatedChecpointStack.Pop();
        }
    }



}
