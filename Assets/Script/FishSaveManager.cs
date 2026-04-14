using UnityEngine;
using System.Collections.Generic;

public static class FishSaveManager
{
    /// <summary>Persists unlocked and favourite fish IDs from the runtime list into UserData.</summary>
    //public static void Save(List<FishSpawnData> fishList)
    //{
    //    UserData user = UserSaveManager.Load();

    //    foreach (var fish in fishList)
    //    {
    //        // _fishUnlockPoint == 1 means the fish is unlocked at runtime
    //        if (fish._fishUnlock == 1)
    //        {
    //            if (!user.fishFullSaveData.unlockedFishIDs.Contains(fish.fishID))
    //                user.fishFullSaveData.unlockedFishIDs.Add(fish.fishID);
    //        }

    //        if (fish.fishData.fevarit)
    //        {
    //            if (!user.fishFullSaveData.favoriteFishIDs.Contains(fish.fishID))
    //                user.fishFullSaveData.favoriteFishIDs.Add(fish.fishID);
    //        }
    //    }

    //    UserSaveManager.Save(user);
    //}

    /// <summary>Applies saved unlock and favourite states to the runtime fish list.</summary>
    //public static void Load(List<FishSpawnData> fishList)
    //{
    //    UserData user = UserSaveManager.Load();

    //    if (user.fishFullSaveData == null)
    //        return;

    //    foreach (var fish in fishList)
    //    {
    //        fish._fishUnlock =
    //            user.fishFullSaveData.unlockedFishIDs.Contains(fish.fishID) ? 1 : 0;

    //        fish.fishData.fevarit =
    //            user.fishFullSaveData.favoriteFishIDs.Contains(fish.fishID);
    //    }
    //}


}
