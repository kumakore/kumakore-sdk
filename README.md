# Kumakore SDK Overview
The Kumakore SDK for Android, iOS, .NET, and Unity®.

The Kumakore Service is a web service that provides RESTful API's to handle all the backend services; e.g., leaderboards, achievements, application, users, facebook, friends, inventory, matches, push, and datastore. The Kumakore SDK makes it easier to use the Kumakore Services both synchronously or asynchronously through the Kumakore SDK.


## Kumakore SDK - 2014 Q1 roadmap

###All
* add support for gifts

###.NET SDK
* enchance samples and tests for full coverage of Actions

###iOS SDK
* TODO

###Android SDK
* refactor Actions to match .NET SDK
* improve KumakoreHttp Action lifecycle
* enchance samples and tests for full coverage of Actions


## Basic synchronous action w/ callbacks 
> delegates for .NET/IOS, interfaces for Android

Here's an example of a synchronous signin call. A signin action object is created with an email. Then sync() makes a syncrhonous (blocking) call and when complete executes the delegate/interface with the results.

```csharp
// C#
// startup
KumakoreApp app = new KumakoreApp(API_KEY, DASHBOARD_VERSION);

// load KumakoreApp state.
app.load();

app.signin(email, password).sync(delegate(ActionUserSignin action) {
	if(action.getCode() == StatusCodes.SUCCESS) {
		//Do something
	}
});
```

```java
// Java
// startup
KumakoreApp app = new KumakoreApp(API_KEY, DASHBOARD_VERSION);

// load KumakoreApp state.
app.load();

app().signin(email, password).sync(new ActionUserSignin.IKumakore() {
	@Override
	public void onActionUserSignin(ActionUserSignin action) {
		if (action.getStatusCode() == StatusCodes.SUCCESS) {
			//Do something
		}
	}
});
```

```objc
// Objective-C
// startup
KKApplication *app = [self createKKApplicationInstance];
[app updateDashboardVersion:nil];
    
KKUser *user = [[KKUser alloc] init:app];
KKUserData *userData = [self getValidLoginCredentials];
    
__block KKReturnStatus *rc;
void (^returnBlock)(KKReturnStatus*) = ^(KKReturnStatus* returnCode) {
    rc = returnCode;
};
[user syncSignIn:userData callback:returnBlock];

```

The callback gets a signin action object where you can query the status of the action's execution. Of course a synchronous call isn't a very interesting use of a callback. If you repleace ```sync``` with ```async``` you will now have a non-blocking execution of the action, and when it returns it will using the same delegate/interfaces to notifiy the completion of the action.

## Documentation (v0.6.1.0)
* [Android](http://kumakore.github.io/kumakore-sdk/docs/android/html/index.html)
* [iOS](http://kumakore.github.io/kumakore-sdk/docs/ios/html/index.html)
* [Unity Android](http://kumakore.github.io/kumakore-sdk/docs/unity3d/android/html/index.html)
* [Unity iOS](http://kumakore.github.io/kumakore-sdk/docs/unity3d/ios/html/index.html)
* [Unity x86](http://kumakore.github.io/kumakore-sdk/docs/unity3d/x86/html/index.html)
* [.NET x86](http://kumakore.github.io/kumakore-sdk/docs/x86/html/index.html)
