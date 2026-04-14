using UnityEngine;
using System.Collections.Generic;

public static class UserSaveManager
{
    private const string KEY = "USER_DATA";

    // 🔹 SAVE
    public static void Save(UserData data)
    {
        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(KEY, json);
        PlayerPrefs.Save();

        Debug.Log("User Saved: " + json);
    }

    // 🔹 LOAD
    public static UserData Load()
    {
        if (!PlayerPrefs.HasKey(KEY))
        {
            Debug.Log("No User Data Found. Creating New.");

            UserData newUser = CreateDefaultUser();
            Save(newUser);

            return newUser;
        }

        string json = PlayerPrefs.GetString(KEY);
        UserData loadedData = JsonUtility.FromJson<UserData>(json);

        // 🔥 Safety checks (UPDATED)

        if (loadedData.fishFullSaveData == null)
            loadedData.fishFullSaveData = new FishFullSaveData();

        if (loadedData.fishFullSaveData.unlockedFishIDs == null)
            loadedData.fishFullSaveData.unlockedFishIDs = new List<int>();

        if (loadedData.fishFullSaveData.favoriteFishIDs == null)
            loadedData.fishFullSaveData.favoriteFishIDs = new List<int>();

        return loadedData;
    }

    // 🔹 DEFAULT USER (First Time Setup)
    private static UserData CreateDefaultUser()
    {
        UserData user = new UserData
        {
            userName = "Aquamen",
            level = 1,
            progressPercent = 0f,
            totalFishCollected = 0,
            points = 0,
            title = "Beginner"
        };

        user.fishFullSaveData = new FishFullSaveData();

        return user;
    }

    // 🔹 RESET DATA (Optional)
    public static void Reset()
    {
        PlayerPrefs.DeleteKey(KEY);
        Debug.Log("User Data Reset");
    }
}