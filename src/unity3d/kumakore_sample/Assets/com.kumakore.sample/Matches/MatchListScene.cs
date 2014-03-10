using UnityEngine;
using System.Collections;
using com.kumakore;
using System.Collections.Generic;

public class MatchListScene : MonoBehaviour {
	
	#region sample variables
	public string appKey;
	public int dashboardVersion;
	
	public string[] attackItems;
	
	public GUIStyle titleStyle = new GUIStyle();
	public GUIStyle normalStyle = new GUIStyle();
	
	private KumakoreApp app;
	private IDictionary<string,OpenMatch> myTurnMatches;
	private IDictionary<string,OpenMatch> theirTurnMatches;
	private IDictionary<string,Match> completedMatches;
	private List<string> completedMatchesLabel = new List<string>();
	private OpenMatch match = null;
	
	private string useremail = "email";
	private string password = "password";
	private string message;
	private bool finishedCurrentMatches = false;
	private bool finishedCompletedMatches = false;
	private string opponentName = "opponent name";
	private bool matchLoaded = false;
	private bool isMyTurn = false;
	private bool mystart = false;
	private string chatMessage = "chat line";
	private Vector2 scrollPosition;
	private Vector2 scrollPosition2;
	
	private string userId;
	#endregion
	
	#region Kumakore 
	// Code related to Kumakore API calls
	// Initialise KumakoreApp object
	void Awake () {
		app = new KumakoreApp(appKey,dashboardVersion);
		
		//app.delete ();
		
		app.load();
	}
	
	private void Update() {
		app.getDispatcher().dispatch();	
	}
	
	void OnDestroy() {
		app.save ();
	}
	
	// KUMAKORE ACTIONS
	// Sign in with username / email and password
	public void SignIn(string user, string pass) {
		app.signin (user,pass).async (delegate(ActionUserSignin action) {
			message = "Signin delegate: " + action.getStatusMessage();
			if(action.getCode() == StatusCodes.SUCCESS) GetUserInfo();
		});
		
	}
	// Get user information
	public void GetUserInfo() {
		app.getUser ().get().async (delegate(ActionUserGet action) {
			if(action.getCode() == StatusCodes.SUCCESS) {
				// User info has been retrieved, load user matches
				downloadMatches();
				userId = app.getUser ().getId ();
			} else message = "Error getting user info: " + action.getStatusMessage();
		});
	}
	// Get list of current matches
	public void GetCurrentMatches() {
		app.getUser ().getOpenMatches().get ().async(delegate(ActionMatchGetOpen action) {
			if(action.getCode() == StatusCodes.SUCCESS) {
				finishedCurrentMatches = true;
				updateAllMatches();
			}
			message = "Get current matches delegate: " + action.getStatusMessage();
		});
	}
	// Get list of completed matches
	public void GetCompletedMatches() {
		app.getUser ().getClosedMatches().get ().async (delegate(ActionMatchGetClosed action) {
			if(action.getCode() == StatusCodes.SUCCESS) {
				finishedCompletedMatches = true;
				updateAllMatches();
			}
			message = "Get completed matches delegate: " + action.getStatusMessage();
		});
	}
	// Creates a new match against specified opponent
	public void CreateNewMatch(string opponent) {
		if(string.IsNullOrEmpty(opponent)) {
			message = "Opponent cannot be null";
		} else {
			app.getUser ().getOpenMatches().createNewMatch(opponent).async (delegate(ActionMatchCreate action) {
				if(action.getCode() == StatusCodes.SUCCESS) {
					message = "Match created";
					downloadMatches();
				} else message = "Error while creating match: " + action.getStatusMessage();
			});
		}
	}
	// Creates a new match
	public void CreateRandomMatch() {
		app.getUser ().getOpenMatches().createRandomMatch().async (delegate(ActionMatchCreateRandom action) {
			if(action.getCode() == StatusCodes.SUCCESS) {
				message = "Random match created";
				downloadMatches();
			} else message = "Error while creating random match: " + action.getStatusMessage();
		});
	}
	// Gets match status
	public void GetMatchStatus(OpenMatch m) {
		match = m;
		match.getStatus().async (delegate(ActionMatchGetStatus action) {
			if(action.getCode () == StatusCodes.SUCCESS) {
				match = app.getUser ().getOpenMatches()[match.getMatchId()];
				message = "Match selected: " + match.getMatchId();
				userId = app.getUser ().getId ();
				matchLoaded = true;
				// load MatchInfo now
				match.getMoves(0).async (GetMatchMovesDelegate);
			} else message = "Error while loading match: " + action.getStatusMessage();
			if(match.getNudged()) message = match.getOpponentUsername() + " has nudged you!";
			//kumakore.user ().inventory().get ().async (GetInventoryDelegate);
		});
	}
	
	public void GetMatchMovesDelegate(ActionMatchGetMoves action) {
		
	}
	// Resign a match
	public void ResignMatch(OpenMatch m) {
		m.resign().async (delegate(ActionMatchResign action) {
			quitMatch();
		});
	}
	// Accept a match
	public void AcceptMatch(OpenMatch m) {
		m.accept().async (delegate(ActionMatchAccept action) {
			GetMatchStatus(m);
		});
	}
	// Reject a match
	public void RejectMatch(OpenMatch m) {
		match.reject().async (delegate(ActionMatchReject action) {
			quitMatch();
		});
	}
	// Send nudge to opponents in a match
	public void SendNudge(OpenMatch m) {
		match.sendNudge().async (delegate(ActionMatchNudge action) {
		});
		
	}
	// Select items to be used in a move
	public void SelectItems(OpenMatch m, IDictionary<string, int> it) {
		m.selectItems(it,m.getMoveNum()).async (delegate(ActionMatchSelectItems action) {
		});
		
	}
	// Send a move to a match
	public void SendMove(OpenMatch m,string move) {
		IDictionary<string, int> _rewardItems = new Dictionary<string,int>();
		IDictionary<string, int> _attackItems = new Dictionary<string,int>();
		IDictionary<string, int> _selectedItems = new Dictionary<string,int>();
		
		//
		 // _rewardItems.put("bomb_01", 1); _rewardItems.put("amazing_powerup",
		 //* 1);
		 //
		_attackItems.Add(move, 1);
		//*
		 //* _selectedItems.put("bomb_01", 1);
		 //* _selectedItems.put("amazing_powerup",1);
		 //
		
		m.sendMove(match.getMoveNum(), "test attack for move: " + m.getMoveNum(),_rewardItems, _attackItems, false, _selectedItems).async(delegate(ActionMatchMove action) {
			GetMatchStatus(m);
		});
	}
	// Send chat message to match
	public void SendChatMessage(OpenMatch m, string chatM) {
		m.sendChatMessage(chatMessage).async (delegate(ActionMatchChatMessage action) {
			chatMessage = "";
			GetChatMessages(m);
		});
	}
	public void GetChatMessages(OpenMatch m) {
		m.getChatMessages().async (delegate(ActionMatchGetChatMessage action) {
		});
		
	}
	public void GetInventoryDelegate(ActionInventoryGet action) {
		
	}
	#endregion
	
	#region GUI
	// Deals with the graphical representation of the sample
	
	void OnGUI() {
		if(app.getUser ().hasSessionId ()) {
			if(match == null && !matchLoaded) {
				scrollPosition = GUI.BeginScrollView(new Rect(0,0,Screen.width,Screen.height-180),scrollPosition,new Rect(0,0,Screen.width*0.9f,Screen.height*10));
				GUI.Label (new Rect(Screen.width*0.35f,5,Screen.width*0.6f,60),"Match List",titleStyle);
				// Matches list screen
				MatchCreationGUI();
				// List matches
				if(finishedCurrentMatches && finishedCompletedMatches) {
					MatchListGUI();
				}
				GUI.EndScrollView();
			} else {
				// Match GUI
				GUI.Label (new Rect(Screen.width*0.35f,5,Screen.width*0.6f,60),"Match Status",titleStyle);
				MatchInfoGUI ();
			}
		} else {
			// Login screen
			LoginGUI();
		}
		// Permanent GUI
		GUI.Label (new Rect(Screen.width*0.1f,Screen.height-180,Screen.width*0.8f,60),message,normalStyle);
		if(GUI.Button(new Rect(Screen.width*0.1f,Screen.height-120,Screen.width*0.8f,100),"Quit")) Application.Quit();
	}
	
	// GUI Screens
	private void LoginGUI() {
		GUI.Label (new Rect(Screen.width*0.35f,20,Screen.width*0.6f,80),"Enter your email/username and password to sign in",titleStyle);
		useremail = GUI.TextField (new Rect(Screen.width*0.1f,100,Screen.width*0.8f,100),useremail);
		password = GUI.TextField (new Rect(Screen.width*0.1f,220,Screen.width*0.8f,100),password);
		if(GUI.Button (new Rect(Screen.width*0.1f,340,Screen.width*0.8f,100),"Sign in")) SignIn(useremail,password);	
	}
	
	private void MatchListGUI() {
		int min = 0;
		if(finishedCurrentMatches && myTurnMatches != null && theirTurnMatches != null) {
			GUI.Label (new Rect(Screen.width*0.1f,480,Screen.width*0.8f,30),"My Turn",normalStyle);
			int counter = 0;
			foreach(KeyValuePair<string,OpenMatch> pair in myTurnMatches) {
				min = 120*counter;
				string info = pair.Value.getOpponentUsername();
				if(string.IsNullOrEmpty(info)) info = "Random Opponent";
				info += " Move:" + pair.Value.getMoveNum();
				if(GUI.Button (new Rect(Screen.width*0.1f,min+520,Screen.width*0.8f,100),info)) {
					GetMatchStatus(pair.Value);
				}
				counter++;
			}
			GUI.Label (new Rect(Screen.width*0.1f,630+min,Screen.width*0.8f,30),"Their turn",normalStyle);
			//counter = 0;
			foreach(KeyValuePair<string,OpenMatch> pair in theirTurnMatches) {
				min = 120*counter;
				string info = pair.Value.getOpponentUsername();
				if(string.IsNullOrEmpty(info)) info = "Random Opponent";
				info += " Move:" + pair.Value.getMoveNum();
				if(GUI.Button (new Rect(Screen.width*0.1f,min+660,Screen.width*0.8f,100),info)) {
					GetMatchStatus(pair.Value);
				}
				counter++;
			}
		}
		if(finishedCompletedMatches && completedMatchesLabel != null) {
			GUI.Label (new Rect(Screen.width*0.1f,740+min,Screen.width*0.8f,30),"Completed matches",normalStyle);
			for(int ii=0; ii<completedMatchesLabel.Count; ii++) {
				min = 120*ii;
				GUI.Label (new Rect(Screen.width*0.1f,min+770,Screen.width*0.8f,100),completedMatchesLabel[ii]);
			}
		}
		
	}
	
	private void MatchCreationGUI() {
		GUI.Label (new Rect(Screen.width*0.1f,70,Screen.width*0.8f,30),"Match creation",normalStyle);
		opponentName = GUI.TextField (new Rect(Screen.width*0.1f,100,Screen.width*0.8f,100),opponentName);
		if(GUI.Button (new Rect(Screen.width*0.1f,220,Screen.width*0.8f,100),"Create match")) CreateNewMatch (opponentName);
		if(GUI.Button (new Rect(Screen.width*0.1f,340,Screen.width*0.8f,100),"Create random")) CreateRandomMatch();
	}
	
	private void MatchInfoGUI() {
		scrollPosition2 = GUI.BeginScrollView(new Rect(0,0,Screen.width,Screen.height-180),scrollPosition2,new Rect(0,0,Screen.width*0.9f,Screen.height*10));
		// Draw player names
		if(checkFirstPlayer()) {
			if(string.IsNullOrEmpty(match.getOpponentId())) {
				GUI.Label (new Rect(Screen.width*0.1f,40,150,30),"P1: " + app.getUser ().getName (),normalStyle);
				GUI.Label (new Rect(Screen.width*0.7f,40,150,30),"Waiting for opponent",normalStyle);
			} else {
				GUI.Label (new Rect(Screen.width*0.1f,40,150,30),"P1: " + app.getUser ().getName (),normalStyle);
				GUI.Label (new Rect(Screen.width*0.7f,40,150,30),"P2: " + match.getOpponentUsername(),normalStyle);
			}
		} else {
			GUI.Label (new Rect(Screen.width*0.1f,40,150,30),"P1: " + match.getOpponentUsername(),normalStyle);
			GUI.Label (new Rect(Screen.width*0.7f,40,150,30),"P2: " + app.getUser ().getName (),normalStyle);
		}
		GUI.Label (new Rect(Screen.width*0.4f,70,200,40),"Move " + match.getMoveNum());
		
		// Draw buttons
		if(isMyTurn) {
			// in turn
			//if(match.getMoveNum() == 2) if(GUI.Button (new Rect(10,130,100,60),"Close")) match.close().async (CloseMatchDelegate);
			if(!match.getRandomMatch() && !match.getClosed () && match.getMoveNum() == 1 && match.getTurn () == userId && !match.getAccepted()) {
				if(GUI.Button (new Rect(Screen.width*0.1f,110,Screen.width*0.4f,100),"Accept")) AcceptMatch (match);
				if(GUI.Button (new Rect(Screen.width*0.5f,110,Screen.width*0.4f,100),"Reject")) RejectMatch(match);
			}
			if(GUI.Button (new Rect(Screen.width*0.1f,230,Screen.width*0.8f,100),"Select items")) SelectItems(match,new Dictionary<string,int>());
			GUI.Label (new Rect(Screen.width*0.4f,350,200,30),"Send Move",normalStyle);
			for(int ii=0; ii<attackItems.Length; ii++) {
				if(GUI.Button (new Rect(ii*Screen.width/attackItems.Length,380,Screen.width/attackItems.Length,100),"Send Move: "+attackItems[ii])) SendMove(match,attackItems[ii]);
			}
			if(GUI.Button (new Rect(Screen.width*0.1f,500,Screen.width*0.8f,100),"Resign")) ResignMatch (match);
		} else {
			// not in turn
			if(GUI.Button (new Rect(Screen.width*0.1f,130,Screen.width*0.8f,100),"Send nudge")) SendNudge (match);
		}
		// common
		chatMessage = GUI.TextField(new Rect(Screen.width*0.1f,600,Screen.width*0.8f,100),chatMessage);
		if(GUI.Button (new Rect(Screen.width*0.1f,720,Screen.width*0.4f,100),"Send Chat")) SendChatMessage(match,chatMessage);
		if(GUI.Button (new Rect(Screen.width*0.5f,720,Screen.width*0.4f,100),"Get messages")) GetChatMessages(match);
		if(match.chatMessages != null && match.chatMessages.Count > 0) {
			for(int ii=0; ii<match.chatMessages.Count; ii++) {
				GUI.Label (new Rect(Screen.width*0.1f,830 + ii*30,Screen.width*0.8f,30),"user:"+match.chatMessages[ii].getUserId() + ",message:"+match.chatMessages[ii].getMsg() + ",date:"+ match.chatMessages[ii].getDate());
			}
		}
		GUI.EndScrollView();
	}
	#endregion
	
	#region auxiliar functions
	// Supporting functions for the sample
	
	// AUXILIAR FUNCTIONS
	private void downloadMatches() {
		finishedCurrentMatches = finishedCompletedMatches = false;
		GetCurrentMatches();
		GetCompletedMatches();
		message = "Loading, please wait...";
	}
	
	private void updateAllMatches() {
		if(!finishedCurrentMatches || !finishedCompletedMatches) return;
		
		updateCurrentMatchList();
		updateCompletedMatchList();
	}
	
	private void updateCurrentMatchList() {
		myTurnMatches = app.getUser ().getOpenMatches().getMyTurn();
		theirTurnMatches = app.getUser ().getOpenMatches().getTheirTurn();
		completedMatches = app.getUser ().getClosedMatches();
	}
	
	private void updateCompletedMatchList() {
		string info = "";
		foreach(KeyValuePair<string,Match> pair in completedMatches) {
			info = pair.Value.getOpponentUsername();
			if(string.IsNullOrEmpty(info)) info = "Random Opponent";
			info += " Move:" + pair.Value.getMoveNum();
			completedMatchesLabel.Add (info);
		}
	}
	
	public bool checkFirstPlayer() {
		int moveNumber = match.getMoveNum();
		// Check if its current user turn
		if (string.IsNullOrEmpty(match.getTurn()) && match.getRandomMatch())
			isMyTurn = false;
		else
			isMyTurn = match.getTurn().Equals(userId);

		// Debug.Log(moveNumber % 2);
		if (match.getResigned()) {
			if (isMyTurn) {
				if (moveNumber % 2 == 1 && match.getResignedId() != userId) {
					mystart = true;
				}
			} else {
				if (moveNumber % 2 == 0 && match.getResignedId() == userId) {
					mystart = true;
				}
			}
		} else {
			if (match.getClosed())
				moveNumber = 6;
			if (isMyTurn) {
				if (moveNumber % 2 == 0)
					mystart = true;
			} else {
				if (moveNumber % 2 == 1)
					mystart = true;
			}
		}

		return mystart;
	}
	
	private void quitMatch() {
		match = null;
		matchLoaded = false;
		downloadMatches();
	}
	#endregion
}