using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase.Firestore;
using Firebase.Extensions;

public class FirebaseManager : IDatabase
{
    FirebaseFirestore db;

    public void Initialize()
    {
        db = FirebaseFirestore.DefaultInstance;
    }

    public bool IsInitialized()
    {
        return db == null;
    }

    public void LoadData(string collection, string key, OnDatabaseLoad onDatabaseLoad = null, OnDatabaseFail onDatabaseFail = null)
    {
        if (IsInitialized())
        {
            DocumentReference docRef = db.Collection(collection).Document(key);
            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    Dictionary<string, object> data = snapshot.ToDictionary();
                    Debug.Log($"Data Loaded from { collection }:{ key }");
                    onDatabaseLoad?.Invoke(data);
                }
                else
                {
                    onDatabaseFail?.Invoke();
                }
            });
        }
    }

    public void SaveData(string collection, string key, Dictionary<string, object> data, OnDatabaseSave onDatabaseSave = null, OnDatabaseFail onDatabaseFail = null)
    {
        if (IsInitialized())
        {
            DocumentReference docRef = db.Collection(collection).Document(key);
            docRef.SetAsync(data).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log($"Data Saved to { collection }:{ key }");
                    onDatabaseSave?.Invoke(data);
                } else if (task.IsFaulted)
                {
                    onDatabaseFail?.Invoke();
                }
            });
        }
    }
}
