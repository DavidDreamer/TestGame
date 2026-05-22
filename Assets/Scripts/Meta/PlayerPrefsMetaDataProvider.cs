using UnityEngine;

public class PlayerPrefsMetaDataProvider : IMetaDataProvider
{
    private const string Key = "MetaData";

    public MetaData Load()
    {
       string json = PlayerPrefs.GetString(Key);
       return json == string.Empty ? new() : JsonUtility.FromJson<MetaData>(json);
    }

    public void Save(MetaData metaData)
    {
        string json = JsonUtility.ToJson(metaData);
        PlayerPrefs.SetString(Key, json);
        PlayerPrefs.Save();
    }
}
