using UnityEngine;
using System.Collections;
using com.kumakore;
using System.Collections.Generic;

public class MatchListScene : MonoBehaviour {
	
	public string appKey;
	public int dashboardVersion;
	
	public string[] attackItems;
	
	private KumakoreApp kumakore;
	
	private string useremail = "email";
	private string password = "password";
	private string message;
	
	private List<Match> myTurnMatches;
	private List<Match> theirTurnMatches;
	private List<string> myTurnMatchesLabel = new List<string>(); 
	private List<string> theirmMatchesLabel = new List<string>();
	private List<string> completedMatchesLabel = new List<string>();
	private string opponentName = "";
	private bool finishedCurrentMatches = false;
	private bool finishedCompletedMatches = false;
	
	private MatchCurrent currentMatch = null;
	private Match match = null;
	private string userId;
	private bool isMyTurn = false;
	private bool mystart = false;
	private string chatMessage = "chat line";
	
	// Use this for initialization
	void Start () {
		kumakore = new KumakoreApp(appKey,dashboardVersion);
	}
	
	void OnGUI () {
		if(kumakore.user ().hasId()) {
			if(currentMatch == null) {
				// List matches
				if(finishedCurrentMatches && finishedCompletedMatches) {
					MatchListGUI();
				} else {
					if(GUI.Button (new Rect(10,10,200,60),"Load matches")) {
						downloadMatches();
					}
				}
				opponentName = GUI.TextField (new Rect(800,10,200,60),opponentName);
				if(GUI.Button (new Rect(800,90,200,60),"Create match")) kumakore.user ().currentMatch().createNewMatch(opponentName).async (CreateNewMatchDelegate);
				if(GUI.Button (new Rect(800,170,200,60),"Create random")) kumakore.user ().currentMatch().createRandomMatch().async (CreateRandomMatchDelegate);
			} else {
				// Match GUI
				MatchInfoGUI();
			}
			
		} else {
			// Sign in
			GUI.Box (new Rect(5,5,415,255),"Enter your email and password to sign in");
			useremail = GUI.TextField (new Rect(10,30,400,60),useremail);
			password = GUI.TextField (new Rect(10,110,400,60),password);
			if(GUI.Button (new Rect(10,190,400,60),"Sign in")) kumakore.signin (useremail,password).async (SigninDelegate);
		}
		
		GUI.Label (new Rect(800,620,300,60),message);
		if(GUI.Button(new Rect(800,680,150,60),"Quit")) Application.Quit();
	}
	
	private void MatchListGUI() {
		if(finishedCurrentMatches && myTurnMatches != null && theirTurnMatches != null) {
			GUI.Label (new Rect(10,10,200,40),"Current matches (in turn)");
			for(int ii=0; ii<myTurnMatches.Count; ii++) {
				int min = 50*ii;
				if(GUI.Button (new Rect(10,min+50,200,60),myTurnMatchesLabel[ii])) kumakore.user ().currentMatch ().getStatus (myTurnMatches[ii].getMatchId()).async (GetStatusDelegate);
			}
			
			GUI.Label (new Rect(400,10,200,40),"Current matches (their turn)");
			for(int ii=0; ii<theirTurnMatches.Count; ii++) {
				int min = 50*ii;
				if(GUI.Button (new Rect(400,min+50,200,60),theirmMatchesLabel[ii])) kumakore.user ().currentMatch ().getStatus (theirTurnMatches[ii].getMatchId()).async (GetStatusDelegate);
			}
		}
		if(finishedCompletedMatches && completedMatchesLabel != null) {
			GUI.Label (new Rect(600,10,200,40),"Completed matches");
			for(int ii=0; ii<completedMatchesLabel.Count; ii++) {
				int min = 50*ii;
				GUI.Label (new Rect(600,min+50,200,60),completedMatchesLabel[ii]);
			}
		}
		
	}
	
	private void MatchInfoGUI() {
		// Draw player names
		if(checkFirstPlayer()) {
			if(string.IsNullOrEmpty(match.getOpponentId())) {
				GUI.Label (new Rect(10,10,200,40),"Player1: " + kumakore.user ().getName ());
				GUI.Label (new Rect(10,50,200,40),"Waiting for opponent");
			} else {
				GUI.Label (new Rect(10,10,200,40),"Player1: " + kumakore.user ().getName ());
				GUI.Label (new Rect(10,50,200,40),"Player2: "+ match.getOpponentUsername());
			}
		} else {
			GUI.Label (new Rect(10,10,200,40),"Player1: " + match.getOpponentUsername());
			GUI.Label (new Rect(10,50,200,40),"Player2: "+ kumakore.user ().getName ());
		}
		GUI.Label (new Rect(10,90,200,40),"Move " + match.getMoveNum());
		
		// Draw buttons
		if(isMyTurn) {
			// in turn
			if(match.getMoveNum() == 2) if(GUI.Button (new Rect(10,130,100,60),"Close")) currentMatch.close().async (CloseMatchDelegate);
			if(!match.getRandomMatch() && !match.getClosed () && match.getMoveNum() == 1 && match.getTurn () == userId && !match.getAccepted()) {
				if(GUI.Button (new Rect(150,130,100,60),"Accept")) currentMatch.accept().async (AcceptMatchDelegate);
				if(GUI.Button (new Rect(300,130,100,60),"Reject")) currentMatch.reject().async (RejectMatchDelegate);
			}
			if(GUI.Button (new Rect(10,210,100,60),"Select items")) currentMatch.selectItems(new Dictionary<string,int>(),match.getMoveNum()).async (SelectItemsDelegate);
			GUI.Label (new Rect(550,10,200,40),"Select attacking move");
			for(int ii=0; ii<attackItems.Length; ii++) {
				if(GUI.Button (new Rect(550,50+ii*50,140,50),"Send Move: "+attackItems[ii])) SendMove(attackItems[ii]);
			}
		} else {
			// not in turn
			if(GUI.Button (new Rect(10,130,100,60),"Send nudge")) currentMatch.sendNudge().async (SendNudgeDelegate);
		}
		// common
		chatMessage = GUI.TextField(new Rect(10,290,200,60),chatMessage);
		if(GUI.Button (new Rect(250,290,100,60),"Send")) currentMatch.sendChatMessage(chatMessage).async (SendChatMessageDelegate);
		if(GUI.Button (new Rect(10,370,100,60),"Get messages")) currentMatch.getChatMessages().async (GetChatMessagesDelegate);
		if(currentMatch.chatMessages != null && currentMatch.chatMessages.Count > 0) {
			for(int ii=0; ii<currentMatch.chatMessages.Count; ii++) {
				GUI.Label (new Rect(10,450 + ii*40,500,40),"user:"+currentMatch.chatMessages[ii].getUserId() + ",message:"+currentMatch.chatMessages[ii].getMsg() + ",date:"+ currentMatch.chatMessages[ii].getDate());
			}
		}
		if(GUI.Button (new Rect(380,370,100,60),"Resign")) currentMatch.resign().async (ResignDelegate);
		ShowInventory();
	}
	
	private void ShowInventory() {
		// display
		GUI.Box (new Rect(745,5,305,365),"User inventory");
		for(int ii=0; ii< kumakore.user().inventory().Count; ii++) {
			int min = ii * 120;
			ItemBundle item = kumakore.user().inventory ()[ii];
			GUI.Label (new Rect(750,30+min,300,40),"Name: " + item.getName ());
			GUI.Label (new Rect(750,75+min,300,40),"Quantity: " + item.getQuantity ());
		}
		
	}
	
	// AUXILIAR FUNCTIONS
	private void downloadMatches() {
		finishedCurrentMatches = finishedCompletedMatches = false;
		kumakore.user ().getCurrentMatches().get ().async (GetCurrentMatchesDelegate);
		kumakore.user ().getCompletedMatches().get ().async (GetCompletedMatchesDelegate);
	}
	
	private void updateAllMatches() {
		if(!finishedCurrentMatches || !finishedCompletedMatches) return;
		
		updateCurrentMatchList();
		updateCompletedMatchList();
	}
	
	private void updateCurrentMatchList() {
		MatchCurrentList matches = kumakore.user ().getCurrentMatches();
		
		myTurnMatches = matches.getMyTurnMatches();
		
		string info = "";
		foreach(Match match in myTurnMatches) {
			info = match.getOpponentUsername();
			if(string.IsNullOrEmpty(info)) info = "Random Opponent";
			info += " Move:" + match.getMoveNum();
			myTurnMatchesLabel.Add (info);
		}
		
		theirTurnMatches = matches.getTheirTurnMatches();
		
		info = "";
		foreach(Match match in theirTurnMatches) {
			info = match.getOpponentUsername();
			if(string.IsNullOrEmpty(info)) info = "Random Opponent";
			info += " Move:" + match.getMoveNum();
			theirmMatchesLabel.Add (info);
		}
	}
	
	private void updateCompletedMatchList() {
		string info = "";
		foreach(Match match in kumakore.user ().getCompletedMatches ()) {
			info = match.getOpponentUsername();
			if(string.IsNullOrEmpty(info)) info = "Random Opponent";
			info += " Move:" + match.getMoveNum();
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
				if (moveNumber % 2 == 1 && currentMatch.getResignedId() != userId) {
					mystart = true;
				}
			} else {
				if (moveNumber % 2 == 0 && currentMatch.getResignedId() == userId) {
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
		//int rewardCoins = 0;
		IDictionary<string, int> _attackItems = new Dictionary<string,int>();
		IDictionary<string, int> _selectedItems = new Dictionary<string,int>();
		
		/*
		 * _rewardItems.put("bomb_01", 1); _rewardItems.put("amazing_powerup",
		 * 1);
		 */
		_attackItems.Add(move, 1);
		/*
		 * _selectedItems.put("bomb_01", 1);
		 * _selectedItems.put("amazing_powerup",1);
		 */
		
		currentMatch.sendMove(match.getMoveNum(), "test attack for move: " + match.getMoveNum(),_rewardItems, _attackItems, /*rewardCoins,*/ false, _selectedItems).async(SendMoveDelegate);
	}
	private void quitMatch() {
		currentMatch = null;
		downloadMatches();
	}
	
	// DELEGATES
	public void SigninDelegate(ActionAppSignin action) {
		message = "Signin delegate: " + action.getStatusMessage();
	}
	public void GetCurrentMatchesDelegate(ActionMatchListCurrent action) {
		finishedCurrentMatches = true;
		updateAllMatches();
		message = "Get current matches delegate: " + action.getStatusMessage();
	}
	public void GetCompletedMatchesDelegate(ActionMatchListCompleted action) {
		finishedCompletedMatches = true;
		updateAllMatches();
		message = "Get completed matches delegate: " + action.getStatusMessage();
	}
	public void GetStatusDelegate(ActionMatchStatusGet action) {
		message = "Going to match...";
		// load MatchInfo now
		currentMatch = kumakore.user ().currentMatch();
		match = currentMatch.match;
		currentMatch.getMoves (0).async (GetMatchMovesDelegate);
		userId = kumakore.user ().getId ();
		if(match.getNudged()) message = match.getOpponentUsername() + " has nudged you!";
		kumakore.user ().inventory().get ().async (GetInventoryDelegate);
	}
	
	public void CreateNewMatchDelegate(ActionMatchCreateNew action) {
		message = "Match created";
		downloadMatches();
	}
	public void CreateRandomMatchDelegate(ActionMatchCreateRandom action) {
		message = "Random match created";
		downloadMatches();
	}
	
	
	public void GetMatchMovesDelegate(ActionMatchMoves action) {
		
	}
	public void SendNudgeDelegate(ActionMatchSendNudge action) {
		
	}
	public void ResignDelegate(ActionMatchResign action) {
		quitMatch();
	}
	public void CloseMatchDelegate(ActionMatchClose action) {
		quitMatch();
	}
	public void AcceptMatchDelegate(ActionMatchAccept action) {
		quitMatch();
	}
	public void RejectMatchDelegate(ActionMatchReject action) {
		quitMatch();
	}
	public void SelectItemsDelegate(ActionMatchSelectItems action) {
		
	}
	public void SendMoveDelegate(ActionMatchSendMove action) {
		currentMatch.getStatus().async (GetStatusDelegate);
	}
	public void SendChatMessageDelegate(ActionChatMessageSend action) {
		chatMessage = "";
		currentMatch.getChatMessages().async (GetChatMessagesDelegate);
	}
	public void GetChatMessagesDelegate(ActionChatMessageGet action) {
		
	}
	public void GetInventoryDelegate(ActionInventoryGet action) {
		
	}
}