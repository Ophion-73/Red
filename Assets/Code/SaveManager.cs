using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private string metaPath;
    private string runPath;

    void Awake()
    {
        // rutas de los archivos
        metaPath = Application.persistentDataPath + "/metaProgress.json";
        runPath = Application.persistentDataPath + "/currentRun.json";
    }
    
    #region Out Run Methods

    public void SaveMeta(MetaData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(metaPath, json);
        Debug.Log("Out Run Progress Saved");
    }

    public MetaData LoadMeta()
    {
        if (File.Exists(metaPath))
        {
            string json = File.ReadAllText(metaPath);
            return JsonUtility.FromJson<MetaData>(json);
        }
        return new MetaData();
    }

    #endregion

    #region In Run Methods

    public void SaveRun(RunData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(runPath, json);
        Debug.Log("Current Run Saved");
    }

    public RunData LoadRun()
    {
        if (File.Exists(runPath))
        {
            string json = File.ReadAllText(runPath);
            return JsonUtility.FromJson<RunData>(json);
        }
        return null;
    }

    public void DeleteRun()
    {
        if (File.Exists(runPath))
        {
            File.Delete(runPath);
            Debug.Log("Run file deleted");
        }
    }
    #endregion
}
