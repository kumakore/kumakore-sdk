# Kumakore SDK Overview
The Kumakore SDK for Android, .NET, and Unity®.

<!-- <img src="diagram.jpg" alt="Drawing" style="width: 600px;" align="middle"/>
-->

<img src="diagram.jpg" width="600px" align="middle"/>

The Kumakore Service is a web service that provides RESTful API's to handle all the backend services; e.g., leaderboards, achievements, application, users, facebook, friends, inventory, matches, push, and datastore. The Kumakore SDK makes it easier to use the Kumakore Services both synchronously or asynchronously through the Kumakore Actions.

Client communication and control happens through Action objects that are mapped to specific Actions to the server. When sending data to the server, the client will create an Action object, set the appropriate data, and send the data either synchronously or asynchronously. When retrieving data from the server, the client will again create and send an action object, but the action will transfer the return data to the app or user data structures. The client will then retrieve the return data from here. Data is stored in either app data ( universal to the app such as achievements and leaderboards) or user data (specific to the user such as achievement progress) space. The important thing to remember is that server communication happens through these Action objects.

## Kumakore SDK startup and shutdown
By now you should have read general documentation and know that an app will have an app key, dashboard version, and potentially an app version. The SDK first needs to be initialized with these values

```csharp
// startup
KumakoreApp app = new KumakoreApp(API_KEY, DASHBOARD_VERSION);

// load KumakoreApp state.
app.load();
```

During the application lifecycle, or before it ends, save the KumakoreApp state for loading in the future.
```csharp
// shutdown
app.save();
```

From here you can begin calling app level data like user, leaderboards, achievements, ...

It's also worth noting that ```KumakoreApp.load/save``` are default mechanisms to save and restore the core application state. These can be overridden and replaced with custom methods to fit the client's needs.

## Making Kumakore API calls
Even using the action objects there are multiple ways of interacting with the Kumakore Service. 

###1) Basic synchronous call
Here's an example of a synchronous signup call.

```
ActionAppSignup signup = app.signup(email);
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

###2) Using callbacks (delegates for .NET and interfaces for Java)
Alteratively the result of the API call could have been handled using a delegate passed to the sync function. Here is an example using signin.

```csharp
// C#
app.signin(email, password).sync(delegate(ActionAppSignin action) {
	if(action.getCode() == StatusCodes.SUCCESS) {
		//Do something
	}
});
```

```java
// Java
app().signin(email, password).sync(new ActionAppSignin.IKumakore() {
	@Override
	public void onActionAppSignin(ActionAppSignin action) {
		if (action.getStatusCode() == StatusCodes.SUCCESS) {
			//Do something
		}
	}
});
```

The callback gets a signin action object where you can query the status of the action's execution. Of course a synchronous call isn't a very interesting use of a callback.

###3) Asynchronous call
You can also make asynchronous (non-blocking) calls. This executes the API request in the background and allows the client application to continue (e.g. render a spinner). Here's an example using signin again with a callback.

```csharp
// C#
app.signin(email, password).async(delegate(ActionAppSignin action) {
	if(action.getCode() == StatusCodes.SUCCESS) {
		//Do something
	}
});
```

```java
// Java
app().signin(email, password).async(new ActionAppSignin.IKumakore() {
	@Override
	public void onActionAppSignin(ActionAppSignin action) {
		if (action.getStatusCode() == StatusCodes.SUCCESS) {
			//Do something
		}
	}
});
```

Aside form the using the async() call the difference is that the application will continue and when the request is complete the delegate will be executed.

###4) Asynchronous call without a delegate
In general you want to provide a delegate to handle the return, but we can also handle this by querying the action itself

```
ActionAppSignup signup = app.signup(email);
signup.async();

while (signup.getCode() == StatusCodes.UNKNOWN) {
	//Draw spinner
}

if(signup.getCode() == StatusCodes.SUCCESS) {
	//Go to main menu
}
```

###5) The SDK uses on the builder pattern for various Actions to enable construction of complex objects.

```
// simple params
app.user().update(usernameOrEmail, password).sync();
// builder params
app.user().update().setName(name).setEmail(email).setPassword(password).sync();
```

###Actions
You have now been exposed to the use of actions to communicate with the Kumakore service. Every server operation will have an individual action object. To stress the point again, the client retrieves data through the app or user data space and issues requests through actions. 

In the following example, the application needs to list the app achievements.

```
for(int i=0; i< app.achievements().Count; i++) {
	print( app.achievements()[i].getName() );
}
```

achievements() returns the internal list of achievements. However, when the app is first opened, the internal list is empty, so the application must request the achievements from the Kumakore service.

```
ActionAppAchievementListGet a = app.achievements().get();
a.sync();
```

When the request returns, the action will handle the return data by copying it into the internal data structures. The next time the app prints the app achievements, there will be data.

Again the above call could be formed multiple ways. Here is an equivalent call.

```
app.achievements().get().sync();
```

Remember the sync() or async() happens on the action.

###Buying multiple items example
Here's a more complex example of buying multple items. The SDK provides a purchase function that allows you to pass in a Dictionary of products. However, using the builder pattern, you can construct a complex action.

```
ActionAppBuyItem a = app.products().buyItem();
a.includeItem("laser", 1);
a.includeItem("shield", 2);
a.sync();
```
	
Which is also equivalent to

```
app.productList().buyItem().includeItem("laser"", 1).includeItem("shield", 2).sync();
```

###Further information
At this point you should be familiar with interacting with the Kumakore SDK. More information about the SDK classes and calls can be found in the reference documentation.

##Matches
Matches are the core of application gameplay, so this section will spend some time illustrating how matches are handled.

###Getting matches
A user will have match lists stored in MatchCurrentList and MatchCompletedList objects corresponding to the user's active matches and completed mathces respectively. You can retrieve them through

```
MatchCurrentList current = app.user().getCurrentMatches();
MatchCompletedList completed = app.user().getCompletedMatches();
```

Again, the lists will be empty if the state of the user's matches have not been retrieved from the server. In this case, actions are used to fetch data into these lists.

```
current.get().sync();
completed.get().sync();
```

Now, there will be data when you iterated through the match lists.

With current matches you can also drill down a level to matches that are your or your opponents turn.

###Creating a match

```
current.createRandomMatch().async(delegate(ActionMatchCreateRandom action){
	if(action.getCode() == StatusCodes.SUCCESS) {
		//do something
	}
});
```
	
###Match status

###Making a move
