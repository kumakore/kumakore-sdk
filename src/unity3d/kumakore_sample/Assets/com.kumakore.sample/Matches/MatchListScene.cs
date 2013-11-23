using UnityEngine;
using System.Collections;
using com.kumakore;
using System.Collections.Generic;

public class MatchListScene : MonoBehaviour {
	
	public string appKey;
	public int dashboardVersion;
	
	public string[] attackItems;
	
	public GUIStyle titleStyle = new GUIStyle();
	public GUIStyle normalStyle = new GUIStyle();
	
	private KumakoreApp kumakoreApp;
	private OpenMatchMap currentMatches;
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
	
	// Initialise KumakoreApp object
	void Awake () {
		kumakoreApp = new KumakoreApp(appKey,dashboardVersion);
	}
	
	void OnGUI() {
		if(kumakoreApp.user ().hasId ()) {
			if(match == null && !matchLoaded) {
				scrollPosition = GUI.BeginScrollView(new Rect(0,0,Screen.width,Screen.height-180),scrollPosition,new Rect(0,0,Screen.width*0.9f,Screen.height*10));
				GUI.Label (new Rect(Screen.width*0.35f,5,Screen.width*0.6f,60),"Match List",titleStyle);
				// Matches list screen
				DrawMatchCreation();
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
			LoginScreen();
		}
		// Permanent GUI
		GUI.Label (new Rect(Screen.width*0.1f,Screen.height-180,Screen.width*0.8f,60),message,normalStyle);
		if(GUI.Button(new Rect(Screen.width*0.1f,Screen.height-120,Screen.width*0.8f,100),"Quit")) Application.Quit();
	}
	
	// GUI Screens
	private void LoginScreen() {
		GUI.Label (new Rect(Screen.width*0.35f,20,Screen.width*0.6f,80),"Enter your email/username and password to sign in",titleStyle);
		useremail = GUI.TextField (new Rect(Screen.width*0.1f,100,Screen.width*0.8f,100),useremail);
		password = GUI.TextField (new Rect(Screen.width*0.1f,220,Screen.width*0.8f,100),password);
		if(GUI.Button (new Rect(Screen.width*0.1f,340,Screen.width*0.8f,100),"Sign in")) kumakoreApp.signin (useremail,password).async (SigninDelegate);	
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
					pair.Value.getStatus().async (GetStatusDelegate);
					match = pair.Value;
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
					pair.Value.getStatus().async (GetStatusDelegate);
					match = pair.Value;
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
	
	private void DrawMatchCreation() {
		GUI.Label (new Rect(Screen.width*0.1f,70,Screen.width*0.8f,30),"Match creation",normalStyle);
		opponentName = GUI.TextField (new Rect(Screen.width*0.1f,100,Screen.width*0.8f,100),opponentName);
		if(GUI.Button (new Rect(Screen.width*0.1f,220,Screen.width*0.8f,100),"Create match")) currentMatches.createNewMatch(opponentName).async (CreateNewMatchDelegate);
		if(GUI.Button (new Rect(Screen.width*0.1f,340,Screen.width*0.8f,100),"Create random")) currentMatches.createRandomMatch().async (CreateRandomMatchDelegate);
	}
	
	private void MatchInfoGUI() {
		scrollPosition2 = GUI.BeginScrollView(new Rect(0,0,Screen.width,Screen.height-180),scrollPosition2,new Rect(0,0,Screen.width*0.9f,Screen.height*10));
		// Draw player names
		if(checkFirstPlayer()) {
			if(string.IsNullOrEmpty(match.getOpponentId())) {
				GUI.Label (new Rect(Screen.width*0.1f,40,150,30),"P1: " + kumakoreApp.user ().getName (),normalStyle);
				GUI.Label (new Rect(Screen.width*0.7f,40,150,30),"Waiting for opponent",normalStyle);
			} else {
				GUI.Label (new Rect(Screen.width*0.1f,40,150,30),"P1: " + kumakoreApp.user ().getName (),normalStyle);
				GUI.Label (new Rect(Screen.width*0.7f,40,150,30),"P2: " + match.getOpponentUsername(),normalStyle);
			}
		} else {
			GUI.Label (new Rect(Screen.width*0.1f,40,150,30),"P1: " + match.getOpponentUsername(),normalStyle);
			GUI.Label (new Rect(Screen.width*0.7f,40,150,30),"P2: " + kumakoreApp.user ().getName (),normalStyle);
		}
		GUI.Label (new Rect(Screen.width*0.4f,70,200,40),"Move " + match.getMoveNum());
		
		// Draw buttons
		if(isMyTurn) {
			// in turn
			//if(match.getMoveNum() == 2) if(GUI.Button (new Rect(10,130,100,60),"Close")) match.close().async (CloseMatchDelegate);
			if(!match.getRandomMatch() && !match.getClosed () && match.getMoveNum() == 1 && match.getTurn () == userId && !match.getAccepted()) {
				if(GUI.Button (new Rect(Screen.width*0.1f,110,Screen.width*0.4f,100),"Accept")) match.accept().async (AcceptMatchDelegate);
				if(GUI.Button (new Rect(Screen.width*0.5f,110,Screen.width*0.4f,100),"Reject")) match.reject().async (RejectMatchDelegate);
			}
			if(GUI.Button (new Rect(Screen.width*0.1f,230,Screen.width*0.8f,100),"Select items")) match.selectItems(new Dictionary<string,int>(),match.getMoveNum()).async (SelectItemsDelegate);
			GUI.Label (new Rect(Screen.width*0.4f,350,200,30),"Send Move",normalStyle);
			for(int ii=0; ii<attackItems.Length; ii++) {
				if(GUI.Button (new Rect(ii*Screen.width/attackItems.Length,380,Screen.width/attackItems.Length,100),"Send Move: "+attackItems[ii])) SendMove(attackItems[ii]);
			}
			if(GUI.Button (new Rect(Screen.width*0.1f,500,Screen.width*0.8f,100),"Resign")) match.resign().async (ResignDelegate);
		} else {
			// not in turn
			if(GUI.Button (new Rect(Screen.width*0.1f,130,Screen.width*0.8f,100),"Send nudge")) match.sendNudge().async (SendNudgeDelegate);
		}
		// common
		chatMessage = GUI.TextField(new Rect(Screen.width*0.1f,600,Screen.width*0.8f,100),chatMessage);
		if(GUI.Button (new Rect(Screen.width*0.1f,720,Screen.width*0.4f,100),"Send Chat")) match.sendChatMessage(chatMessage).async (SendChatMessageDelegate);
		if(GUI.Button (new Rect(Screen.width*0.5f,720,Screen.width*0.4f,100),"Get messages")) match.getChatMessages().async (GetChatMessagesDelegate);
		if(match.chatMessages != null && match.chatMessages.Count > 0) {
			for(int ii=0; ii<match.chatMessages.Count; ii++) {
				GUI.Label (new Rect(Screen.width*0.1f,830 + ii*30,Screen.width*0.8f,30),"user:"+match.chatMessages[ii].getUserId() + ",message:"+match.chatMessages[ii].getMsg() + ",date:"+ match.chatMessages[ii].getDate());
			}
		}
		GUI.EndScrollView();
	}
	
	
	// KUMAKORE DELEGATES
	public void SigninDelegate(ActionAppSignin action) {
		message = "Signin delegate: " + action.getStatusMessage();
		if(action.getCode() == StatusCodes.SUCCESS) kumakoreApp.user ().get().async (GetUserInfoDelegate);
	}
	
	public void GetUserInfoDelegate(ActionUserGet action) {
		if(action.getCode() == StatusCodes.SUCCESS) {
			downloadMatches();
			userId = kumakoreApp.user ().getId ();
		} else message = "Error getting user info: " + action.getStatusMessage();
	}
	
	public void GetCurrentMatchesDelegate(ActionOpenMatchMap action) {
		if(action.getCode() == StatusCodes.SUCCESS) {
			finishedCurrentMatches = true;
			updateAllMatches();
		}
		message = "Get current matches delegate: " + action.getStatusMessage();
	}
	
	public void GetCompletedMatchesDelegate(ActionClosedMatchMap action) {
		if(action.getCode() == StatusCodes.SUCCESS) {
			finishedCompletedMatches = true;
			updateAllMatches();
		}
		message = "Get completed matches delegate: " + action.getStatusMessage();
	}
	
	public void CreateNewMatchDelegate(ActionMatchCreateNew action) {
		if(action.getCode() == StatusCodes.SUCCESS) {
			message = "Match created";
			downloadMatches();
		} else message = "Error while creating match: " + action.getStatusMessage();
	}
	
	public void CreateRandomMatchDelegate(ActionMatchCreateRandom action) {
		if(action.getCode() == StatusCodes.SUCCESS) {
			message = "Random match created";
			downloadMatches();
		} else message = "Error while creating random match: " + action.getStatusMessage();
	}
	
	public void GetStatusDelegate(ActionMatchStatusGet action) {
		if(action.getCode () == StatusCodes.SUCCESS) {
			match = kumakoreApp.user ().getOpenMatches()[match.getMatchId()];
			message = "Match selected: " + match.getMatchId();
			userId = kumakoreApp.user ().getId ();
			matchLoaded = true;
			// load MatchInfo now
			match.getMoves(0).async (GetMatchMovesDelegate);
		} else message = "Error while loading match: " + action.getStatusMessage();
		//if(match.getNudged()) message = match.getOpponentUsername() + " has nudged you!";
		//kumakore.user ().inventory().get ().async (GetInventoryDelegate);
	}
	
	public void GetMatchMovesDelegate(ActionMatchMoves action) {
		
	}
	
	public void ResignDelegate(ActionMatchResign action) {
		quitMatch();
	}
	
	public void AcceptMatchDelegate(ActionMatchAccept action) {
		
	}
	public void RejectMatchDelegate(ActionMatchReject action) {
		quitMatch();
	}
	
	public void SendNudgeDelegate(ActionMatchSendNudge action) {
		
	}
	public void SelectItemsDelegate(ActionMatchSelectItems action) {
		
	}
	public void SendMoveDelegate(ActionMatchSendMove action) {
		match.getStatus().async (GetStatusDelegate);
	}
	public void SendChatMessageDelegate(ActionChatMessageSend action) {
		chatMessage = "";
		match.getChatMessages().async (GetChatMessagesDelegate);
	}
	public void GetChatMessagesDelegate(ActionChatMessageGet action) {
		
	}
	public void GetInventoryDelegate(ActionInventoryGet action) {
		
	}
	
	// AUXILIAR FUNCTIONS
	private void downloadMatches() {
		finishedCurrentMatches = finishedCompletedMatches = false;
		currentMatches = kumakoreApp.user ().getOpenMatches();
		currentMatches.get ().async(GetCurrentMatchesDelegate);
		kumakoreApp.user ().getClosedMatches().get ().async (GetCompletedMatchesDelegate);
		message = "Loading, please wait...";
	}
	
	private void updateAllMatches() {
		if(!finishedCurrentMatches || !finishedCompletedMatches) return;
		
		updateCurrentMatchList();
		updateCompletedMatchList();
	}
	
	private void updateCurrentMatchList() {
		myTurnMatches = currentMatches.filters().myTurn();
		theirTurnMatches = currentMatches.filters().theirTurn();
		completedMatches = kumakoreApp.user ().getClosedMatches();
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
	
	private void SendMove(string move) {
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
		
		match.sendMove(match.getMoveNum(), "test attack for move: " + match.getMoveNum(),_rewardItems, _attackItems, false, _selectedItems).async(SendMoveDelegate);
	}
	
	private void quitMatch() {
		match = null;
		matchLoaded = false;
		downloadMatches();
	}
}