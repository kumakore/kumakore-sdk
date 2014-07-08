using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.kumakore;

namespace com.kumakore.sample {

	public partial class MatchListBehav : SigninBehav {
		
//		private static readonly string TAG = typeof(MatchListBehav).Name;	

		#region Fields
		
		private string userId;

		public string[] attackItems;

		private IDictionary<string,OpenMatch> myTurnMatches;
		private IDictionary<string,OpenMatch> theirTurnMatches;
		private IDictionary<string,Match> completedMatches;
		private List<string> completedMatchesLabel = new List<string>();
		private OpenMatch match = null;

		private bool finishedCurrentMatches = false;
		private bool finishedCompletedMatches = false;
		private string opponentName = "opponent name";
		private bool matchLoaded = false;
		private bool isMyTurn = false;
		private bool mystart = false;
		private string chatMessage = "chat line";

		#endregion
		
		#region Actions 

		public void getUserInfo() {
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
					match.actGetMoves().async (); //TODO:chbfiv do something with results?
				} else message = "Error while loading match: " + action.getStatusMessage();
				if(match.getNudged()) message = match.getOpponent().getUserName() + " has nudged you!";
				//kumakore.user ().inventory().get ().async (GetInventoryDelegate);
			});
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
			m.reject().async (delegate(ActionMatchReject action) {
				quitMatch();
			});
		}
		// Send nudge to opponents in a match
		public void SendNudge(OpenMatch m) {
			m.sendNudge().async (delegate(ActionMatchNudge action) {
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
			m.actGetChatMessages().async (delegate(ActionMatchGetChatMessage action) {
			});
			
		}
		public void GetInventoryDelegate(ActionInventoryGet action) {
			
		}
		#endregion
		
		#region Aux
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
				info = pair.Value.getOpponent().getUserName();
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
}