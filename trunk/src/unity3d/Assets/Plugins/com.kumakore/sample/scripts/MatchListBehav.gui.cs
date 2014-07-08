using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.kumakore;

namespace com.kumakore.sample {

	public partial class MatchListBehav : SigninBehav {

		private Vector2 scrollPosition;
		private Vector2 scrollPosition2;
		private string message;
		
		public GUIStyle titleStyle = new GUIStyle();
		public GUIStyle normalStyle = new GUIStyle();

		protected override void OnGUI() {
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
				base.OnGUI();
			}

			// Permanent GUI
			GUI.Label (new Rect(Screen.width*0.1f,Screen.height-180,Screen.width*0.8f,60),message,normalStyle);
			if(GUI.Button(new Rect(Screen.width*0.1f,Screen.height-120,Screen.width*0.8f,100),"Quit")) Application.Quit();
		}
		
		private void MatchListGUI() {
			int min = 0;
			if(finishedCurrentMatches && myTurnMatches != null && theirTurnMatches != null) {
				GUI.Label (new Rect(Screen.width*0.1f,480,Screen.width*0.8f,30),"My Turn",normalStyle);
				int counter = 0;
				foreach(KeyValuePair<string,OpenMatch> pair in myTurnMatches) {
					min = 120*counter;
					string info = pair.Value.getOpponent().getUserName();
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
					string info = pair.Value.getOpponent().getUserName();
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
				if(string.IsNullOrEmpty(match.getOpponent().getUserId())) {
					GUI.Label (new Rect(Screen.width*0.1f,40,150,30),"P1: " + app.getUser ().getName (),normalStyle);
					GUI.Label (new Rect(Screen.width*0.7f,40,150,30),"Waiting for opponent",normalStyle);
				} else {
					GUI.Label (new Rect(Screen.width*0.1f,40,150,30),"P1: " + app.getUser ().getName (),normalStyle);
					GUI.Label (new Rect(Screen.width*0.7f,40,150,30),"P2: " + match.getOpponent().getUserName(),normalStyle);
				}
			} else {
				GUI.Label (new Rect(Screen.width*0.1f,40,150,30),"P1: " + match.getOpponent().getUserName(),normalStyle);
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
			if(match.getChatMessages().Count > 0) {
				for(int ii=0; ii<match.getChatMessages().Count; ii++) {
					ChatMessage msg = match.getChatMessages()[ii];
					GUI.Label (new Rect(Screen.width*0.1f,830 + ii*30,Screen.width*0.8f,30),"user:"+msg.getUserId() + ",message:"+msg.getMsg() + ",date:"+ msg.getCreatedAt());
				}
			}
			GUI.EndScrollView();
		}
	}
}