using Firebase;
using UnityEngine;
using Firebase.Analytics;
public class FireBaseInit : MonoBehaviour
{
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(
            task =>
            {
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

                var app = FirebaseApp.DefaultInstance;
            });
    }
}
