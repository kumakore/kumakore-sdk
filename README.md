# Android and .NET SDK Overview
The Kumakore SDK for Android and .NET (C# and Unity compatible). 

<!-- <img src="diagram.jpg" alt="Drawing" style="width: 600px;" align="middle"/>
-->

<img src="diagram.jpg" width="600px" align="middle"/>

Data are stored in either app data ( universal to the app such as achievements and leaderboards) or user data (specific to the user such as achievement progress) space. The client application will retrieve data from here.

Client communication and control happens through action objects that are mapped to specific actions to the server. 

When sending data to the server, the client will create an action object, set the appropriate data, and send the data either synchronously or asynchronously. 

When retrieving data from the server, the client will again create and send an action object, but the action will transfer the return data to the app or user data structures. The client will then retrieve the return data from here.

The important thing to remember is that server communication happens through the action objects

## Initialzing the SDK
By now you should have read general documentation and know that an app will have an app key, dashboard version, and potentially an app version. The SDK first needs to be initialized with these values

```
KumakoreApp _app = new KumakoreApp(api_key, dashboard);
```

From here you can begin calling app level data like achievements.

## Making API calls
Even using the action objects there are multiple ways of interacting with the Kumakore Service. 

###1) Basic synchronous call
Here's an example of a synchronous signup call.

```
ActionAppSignup signup = _app.signup(email);
signup.sync();
if(signup.getCode() == StatusCodes.SUCCESS) {
	//Do something
}
```	

A signup action object is created with an email. Then sync() makes a syncrhonous (blocking) call.

We could also have simplified this to

```
if(_app.signup(email).sync() == StatusCodes.SUCCESS) {
	//Do something
}
```

###2) Using Delegates
Alteratively the result of the API call could have been handled using a delegate passed to the sync function. Here is an example using signin.

```
_app.signin(email, password).sync(delegate(ActionAppSignin action) {
	if(action.getCode() == StatusCodes.SUCCESS) {
		//Do something
	}
});
```

The delegate gets a signin action object where you can query the status of the request. Of course a synchronous call isn't a very interesting use of a delegate.

###3) Asynchronous call
You can also make asynchronous (non-blocking) calls. This executes the API request in the background and allows the client application to continue (e.g. render a spinner). Here's an example using signin again with a delegate.

```
_app.signin(email, password).async(delegate(ActionAppSignin action) {
	if(action.getCode() == StatusCodes.SUCCESS) {
		//Do something
	}
});
```

Aside form the using the async() call the difference is that the application will continue and when the request is complete the delegate will be executed.

###4) Asynchronous call without a delegate
In general you want to provide a delegate to handle the return, but we can also handle this by querying the action itself

```
ActionAppSignup signup = _app.signup(email);
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
_app.user().update(usernameOrEmail, password).sync();
// builder params
_app.user().update().setName(name).setEmail(email).setPassword(password).sync();
```

###Actions
You have now been exposed to the use of actions to communicate with the Kumakore service. Every server operation will have an individual action object. To stress the point again, the client retrieves data through the app or user data space and issues requests through actions. 

In the following example, the application needs to list the app achievements.

```
for(int i=0; i< _app.achievements().Count; i++) {
	print( _app.achievements()[i].getName() );
}
```

achievements() returns the internal list of achievements. However, when the app is first opened, the internal list is empty, so the application must request the achievements from the Kumakore service.

```
ActionAppAchievementListGet a = _app.achievements().get();
a.sync();
```

When the request returns, the action will handle the return data by copying it into the internal data structures. The next time the app prints the app achievements, there will be data.

Again the above call could be formed multiple ways. Here is an equivalent call.

```
_app.achievements().get().sync();
```

Remember the sync() or async() happens on the action.

###Buying multiple items example
Here's a more complex example of buying multple items. The SDK provides a purchase function that allows you to pass in a Dictionary of products. However, using the builder pattern, you can construct a complex action.

```
ActionAppBuyItem a = _app.products().buyItem();
a.includeItem("laser", 1);
a.includeItem("shield", 2);
a.sync();
```
	
Which is also equivalent to

```
_app.productList().buyItem().includeItem("laser"", 1).includeItem("shield", 2).sync();
```

###Further information
At this point you should be familiar with interacting with the Kumakore SDK. More information about the SDK classes and calls can be found in the reference documentation.

##Matches
Matches are the core of application gameplay, so this section will spend some time illustrating how matches are handled.

###Getting matches
A user will have match lists stored in MatchCurrentList and MatchCompletedList objects corresponding to the user's active matches and completed mathces respectively. You can retrieve them through

```
MatchCurrentList current = _app.user().getCurrentMatches();
MatchCompletedList completed = _app.user().getCompletedMatches();
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
