# Kumakore SDK Release notes


# v0.9.0
## NOTES
### NAMING CHANGES COMING SOON
* going forward we plan to update our naming convention for ALL actions from their "simple names" to "action names"; This will be backwards compatible (unless specified otherwise in the release notes) until v1.0.0, when all obsolete calls will be removed. e.g., ActionAppLog can currently be created via KumakoreApp.logInfo(String message); this will be renamed to KumakoreApp.actLogInfo(String message). This does two things, first it groups all actions by prefix "act" on an instance. Secondly it helps to quickly identify what calls are actions. Please provide feedback on the subject. e.g., maybe you think KumakoreApp.logInfoAct(String message), KumakoreApp.logInfoAction(String message), KumakoreApp.actionLogInfo(String message), KumakoreApp.actions.LogInfo(String message) [via inner class "group"] are better options. (poll!)
* Keep in mind all obsolete memebers are scheduled to be removed in v1.0.0

## CHANGES
### MAJOR RELEASE ASSET STRUCUTRE CHANGES
* Previously we had a fragile asset structure due to undocumented "features" of unity. We decided to change this to a more stable structure based on the unity documentation recommendations and from looking at other plugin examples. 
* [UPGRADE GUIDE - STEP 1] Remove all com.kumakore* folders from your current unity project
* [UPGRADE GUIDE - STEP 2] Download the latest release (v0.9.0)
* [UPGRADE GUIDE - STEP 3] With your current project open, double click the kumakore.unitypackage and import the assets.
* Notice that everything is within the Assets/Plugins folder now.
* NOTE: if you have conflicts please let us know. We can improve some of the "global" assets naming to prevent conflicts, while others we can't because they are outside dependencies (Google Play Services).

### NOT BACKWARDS COMPATIBLE
* renamed Match.getChatMessages() to Match.actGetChatMessages()
* removed Match.getChatMessages(String matchId); instead use Match.actGetChatMessages()
* removed Match.sendChatMessage(String matchId, String message); instead use Match.sendChatMessage(String message)
* Leaderboard map key change to name from id
* app version changed from float to string

### BACKWARDS COMPATIBLE
* marked obsolete Match.getCreatedUTC(); instead use Match.getCreatedAt() 
* marked obsolete Match.getUpdatedUTC(); instead use Match.getUpdatedAt() 
* marked obsolete Match.chatMessages; instead use Match.getChatMessages()
* marked obsolete Match.getOpponentUsername(); instead use Match.getOpponent().getUserName()
* marked obsolete Match.getOpponentId(); instead use Match.getOpponent().getUserId()
* marked obsolete Match.getOpponentFBId(); instead use Match.getOpponent().getFacebookId()
* marked obsolete Match.getOpponentAvatarId(); instead use Match.getOpponent().getAvatarId()
* marked obsolete KumakoreApp(API_KEY, DASHBOARDVERSION); instead use KumakoreApp(API_KEY, APP_VERSION, DASHBOARDVERSION); NOTE: you can get the APP_VERSION = "0.0" by default. See the docs for more details.
* marked obsolete Opponent.getName(); instead use Opponent.getUserName()
* marked obsolete Opponent.getId(); instead use Opponent.getUserId()
* marked obsolete Opponent.getAvatar(); instead use Opponent.getAvatarId()
* marked obsolete Match.getCompleted(); instead use !Match.isActive()
* marked obsolete ChatMessage.getDate(); instead use ChatMessage.getCreatedAt()
* Android push was moved into a new com.kumakore.plugins project (source included)
* matches had a significant refactoring of it's implementation. The interfaces are stable.
* added smart flag to load (default). This will prevent overwriting the dashboardVersion during load if the dashboardVersion form the filesystem is less than the dashboardVersion in current memory.
* default path for saved state is now "kumakoredata" instead of the API key. e.g., load() === load(DATA_PATH + DEFAULT_DATA_FILENAME) === load(Application.persistentDataPath + "//" + "kumakoredata")
* added crypto helpers to KumakoreUtil
* changed the default filename for the kumakore app state to not use the API key.
* added missing Android resources to the Android plugin (Google Play Services)
* refactored fastJSON namespace to prevent conflicts with other apps using fastJSON
* moved all NON-REST API functionality out of the main kumakore dll
* ActionFriendGetOpponents request data corrected.
* added ActionDevicePushUser Device.pushToUser(String userName, String message)
* impovements to status codes
* changed datastore (objects) store from using "objectId" to type and name


## FEATURES
* samples restructured. Still needs cleanup and more work, but removed a ton of duplicate code.
* samples now have KumakoreAppSingleBehav as a "template" example of how to manage your KumakoreApp instance. This is used now used through all the samples. Also see KumakoreBehav for a simple base class (though not required). Feel free to use your own methods to manage the KumakoreApp instance. This is mainly to help everyone ramp up faster =)
* added Opponent.getFacebookId()
* com.kumakore.plugins will now retain "plugins" that might be useful to use our service that we developed. e.g., Push, ...
* We exposed com.kumakore.Command<T, U> from kumakore.dll for developers as requested. See com.kumakore.plugin/android/push/CmdRegisterSender.cs for an example. The main idea is that "commands" can be executed sync/async. More work might be done to this going forward, but this is the general pattern we are using for any blocking calls (e.g., http, GCM push, ...)

## TODO
* refactor json namespace to prevent conflicts

# v0.8.0
* Added Gifting (See reference doc module) 
NOTE: Gifting StatusCode helpers are still being added
* exposed Leaderboard reset At

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
