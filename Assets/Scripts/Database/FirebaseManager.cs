using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;

public class FirebaseManager : IDatabase
{
    FirebaseApp app;
    FirebaseFirestore db;

    public void Initialize()
    {
        InitializeOptions();
        /*
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            DependencyStatus status = task.Result;
            if (status == DependencyStatus.Available)
            {
                InitializeOptions();
            } else
            {
                Debug.Log("Could not resolve all firebase dependencies: " + status);
            }
        });
        */
    }

    private void InitializeOptions()
    {
        AppOptions options = AppOptions.LoadFromJsonConfig("{\n  \"project_info\": {\n    \"project_number\": \"548301806630\",\n    \"project_id\": \"wastesortinggame\",\n\t\"firebase_url\": \"https://wastesortinggame-default-rtdb.firebaseio.com\",\n    \"storage_bucket\": \"wastesortinggame.appspot.com\"\n  },\n  \"client\": [\n    {\n      \"client_info\": {\n        \"mobilesdk_app_id\": \"1:548301806630:android:8576e8854fd0ebfb2f9ae7\",\n        \"android_client_info\": {\n          \"package_name\": \"com.rizakusuma.WasteSortingGame\"\n        }\n      },\n      \"oauth_client\": [\n        {\n          \"client_id\": \"548301806630-ms9fut3i83m1q98b3d2rt1qpcflm9204.apps.googleusercontent.com\",\n          \"client_type\": 3\n        }\n      ],\n      \"api_key\": [\n        {\n          \"current_key\": \"AIzaSyDrEZE2qzxi8XrQ-6xHPVgM5RR3ZJ5mCyk\"\n        }\n      ],\n      \"services\": {\n        \"appinvite_service\": {\n          \"other_platform_oauth_client\": [\n            {\n              \"client_id\": \"548301806630-ms9fut3i83m1q98b3d2rt1qpcflm9204.apps.googleusercontent.com\",\n              \"client_type\": 3\n            }\n          ]\n        }\n      }\n    },\n    {\n      \"client_info\": {\n        \"mobilesdk_app_id\": \"1:548301806630:android:e18dd25123b4a1412f9ae7\",\n        \"android_client_info\": {\n          \"package_name\": \"com.rizakusuma.wastesortinggame\"\n        }\n      },\n      \"oauth_client\": [\n        {\n          \"client_id\": \"548301806630-ms9fut3i83m1q98b3d2rt1qpcflm9204.apps.googleusercontent.com\",\n          \"client_type\": 3\n        }\n      ],\n      \"api_key\": [\n        {\n          \"current_key\": \"AIzaSyDrEZE2qzxi8XrQ-6xHPVgM5RR3ZJ5mCyk\"\n        }\n      ],\n      \"services\": {\n        \"appinvite_service\": {\n          \"other_platform_oauth_client\": [\n            {\n              \"client_id\": \"548301806630-ms9fut3i83m1q98b3d2rt1qpcflm9204.apps.googleusercontent.com\",\n              \"client_type\": 3\n            }\n          ]\n        }\n      }\n    }\n  ],\n  \"configuration_version\": \"1\"\n}");
        /*
        AppOptions options = new AppOptions()
        {
            ApiKey = "AIzaSyDsrEZE2qzxi8XrQ-6xHPVgM5RR3ZJ5mCyk",
            AppId = "548301806630",
            DatabaseUrl = new System.Uri("https://wastesortinggame-default-rtdb.firebaseio.com"),
            ProjectId = "wastesortinggame",
            StorageBucket = "wastesortinggame.appspot.com"
        };
        */

        app = FirebaseApp.Create(options);
        // app = db.App;

        Debug.Log("ApiKey: " + app.Options.ApiKey);
        Debug.Log("AppId: " + app.Options.AppId);
        Debug.Log("DatabaseUrl: " + app.Options.DatabaseUrl.AbsoluteUri);
        Debug.Log("MessageSender: " + app.Options.MessageSenderId);
        Debug.Log("ProjectId: " + app.Options.ProjectId);
        Debug.Log("StorageBucket: " + app.Options.StorageBucket);

        db = FirebaseFirestore.GetInstance(app);
        // db = FirebaseFirestore.DefaultInstance;

        Debug.Log("Firebase Initialized");
    }

    public bool IsInitialized()
    {
        return db != null;
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
                    Debug.Log("Data Load Fail.");
                }
            });
        } else
        {
            Debug.Log("Firebase not initialied");
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
                    Debug.Log("Data Save Faulted.");
                } else
                {
                    Debug.Log("Unknown error");
                }
            });
        } else
        {
            Debug.Log("Firebase not initialied");
        }
    }
}
