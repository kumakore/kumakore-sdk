# Kumakore SDK Overview
The Kumakore SDK for Android, iOS, .NET, and Unity®.

The Kumakore Service is a web service that provides RESTful API's to handle all the backend services; e.g., leaderboards, achievements, application, users, facebook, friends, inventory, matches, push, and datastore. The Kumakore SDK makes it easier to use the Kumakore Services both synchronously or asynchronously through the Kumakore SDK.

# See releases tab for latest download of the Kumakore SDK

## Basic synchronous action w/ callbacks 
> delegates for .NET/iOS, interfaces for Android

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

## Documentation 
* [Kumakore Documentation](http://docs.kumakore.com)
