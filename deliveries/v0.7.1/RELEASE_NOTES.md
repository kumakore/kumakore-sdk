# Kumakore SDK Release notes

# v0.7.1
* bug fix for new dispatcher for sync operations
* added isFriend, getFriend, isFriendFacebook, getFriendFacebook to LeaderboardMember. You still need to sync your friends before using these convenience members.
* updated documentation for unity (structure and content)

# v0.7.0.0
* Refactored LeaderboardList into LeaderboardMap (was already a map)
* IMPORTANT! NOT BACKWARDS COMPATIBLE CHANGE! LeaderboardMap, AppAchievementMap, and UserAchievementMap now use their respective unique "name" as the key, instead of "id". 
* IMPORTANT! NOT BACKWARDS COMPATIBLE CHANGE (UNITY3D)! Added a new KumakoreDispatcher for the KumakoreApp; app.getDispatcher()

```
	private void Update() {
		app.getDispatcher().dispatch();	//instance of KumakoreApp app;
	}
```

if you are using Unity 3D, you need to call dispatch once per frame; You do not need to do this in the Update(), but this is a good place for general dispatch notifications to Kumakore. The reason this is required is so that the async() callbacks will be called on the main unity thread to conform with the unity recommendations. By default for Unity 3D, KumakoreDispatcher.immediateDispatch = false. By default for .NET developers KumakoreDispatcher.immediateDispatch = true;

Keep in mind, the KumakoreDispatcher does no thread switching. It allows the developer to decide when and on which threads the callbacks for Actions will execute. You can also queue IKumakoreInvokable's to the KumakoreDispatcher, if you want to syncronize additional generic delegates with the KumakoreDispatcher queue. 

* Added Leaderboard samples to HelloWorld.cs unity sample

# 0.6.2.0

# 0.6.1.0
