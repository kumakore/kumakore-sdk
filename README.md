# Kumakore SDK Overview
The Kumakore SDK for Android, iOS, .NET, and Unity®.

The Kumakore Service is a web service that provides RESTful API's to handle all the backend services; e.g., leaderboards, achievements, application, users, facebook, friends, inventory, matches, push, and datastore. The Kumakore SDK makes it easier to use the Kumakore Services both synchronously or asynchronously through the Kumakore Actions.

Client communication and control happens through Action objects that are mapped to specific Actions to the server. When sending data to the server, the client will create an Action object, set the appropriate data, and send the data either synchronously or asynchronously. When retrieving data from the server, the client will again create and send an action object, but the action will transfer the return data to the app or user data structures. The client will then retrieve the return data from here. Data is stored in either app data ( universal to the app such as achievements and leaderboards) or user data (specific to the user such as achievement progress) space. The important thing to remember is that server communication happens through these Action objects.


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

## Kumakore SDK startup and shutdown [Android/.NET]
By now you should have read general documentation and know that an app will have an app key, dashboard version, and potentially an app version. The SDK first needs to be initialized with these values

```csharp
// startup
KumakoreApp app = new KumakoreApp(API_KEY, DASHBOARD_VERSION);

// load KumakoreApp state.
app.load();
```

From here you can begin calling app level data like user, leaderboards, achievements, and so on. During the application lifecycle, or before it ends, save the KumakoreApp state for loading in the future.

```
// shutdown
app.save();
```

It's also worth noting that ```KumakoreApp.load/save``` are default mechanisms to save and restore the core application state. These can be overridden and replaced with custom methods to fit the client's needs.

## Making Kumakore API calls [Android/.NET]
Even using the action objects there are multiple ways of interacting with the Kumakore Service. 

###1) Basic synchronous call
Here's an example of a synchronous signup call.

```
ActionUserSignup signup = app.signup(email);
signup.sync();
if(signup.getCode() == StatusCodes.SUCCESS) {
    //Do something
}
```	

A signup action object is created with an email. Then sync() makes a syncrhonous (blocking) call.

We could also have simplified this to

```
if(app.signup(email).sync() == StatusCodes.SUCCESS) {
	//Do something
}
```

###2) Using callbacks (delegates for .NET and interfaces for Android)
Alteratively the result of the API call could have been handled using a delegate passed to the sync function. Here is an example using signin.

```csharp
// C#
app.signin(email, password).sync(delegate(ActionUserSignin action) {
	if(action.getCode() == StatusCodes.SUCCESS) {
		//Do something
	}
});
```

```java
// Java
app().signin(email, password).sync(new ActionUserSignin.IKumakore() {
	@Override
	public void onActionUserSignin(ActionUserSignin action) {
		if (action.getStatusCode() == StatusCodes.SUCCESS) {
			//Do something
		}
	}
});
```

The callback gets a signin action object where you can query the status of the action's execution. Of course a synchronous call isn't a very interesting use of a callback.

