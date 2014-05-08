using System;
using com.kumakore;
using NUnit.Framework;
using System.Threading;
using System.Collections.Generic;

namespace com.kumakore.test
{
    [TestFixture]
	public class TestMatches : TestBase
    {
		private readonly static String VALID_RIVAL = "carlos";
		
	    public static void All()
        {
            TestMatches matches = new TestMatches();
            matches.setup();
			
			// test getOpenMatches
			matches.SyncCreateNewMatch();
			matches.SyncGetOpenMatchList();
			matches.SyncCloseMatches();
			
			matches.AsyncCreateNewMatch();
			matches.AsyncGetOpenMatchList();
			matches.AsyncCloseMatches();
			
			matches.SyncCreateRandomMatch();
			//matches.SyncGetStatus();
			//matches.AsyncGetStatus();
			matches.SyncGetMoves();
			matches.AsyncGetMoves();
			matches.SyncResignAll();
			
			matches.SyncCreateRandomMatch();
			matches.AsyncResignAll();
			
			matches.SyncCreateRandomMatch();
			matches.SyncRejectAll();
			
			matches.SyncCreateRandomMatch();
			matches.AsyncRejectAll();
			
			/*matches.SyncCreateRandomMatch();
			matches.SyncSendMoves();
			matches.AsyncSendMoves();
			matches.SyncSelectItems();
			matches.AsyncSelectItems();*/
			
			matches.SyncGetClosedMatchList();
			matches.AsyncGetClosedMatchList();
			
			matches.SyncCloseMatches();
        }
		
		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
			app1 ().signin(TEST_1,PASSWORD).sync ();
		}
		
		
		[Test]
        public void SyncCreateNewMatch()
        {
			app1().getUser().getOpenMatches().createNewMatch(VALID_RIVAL).sync (delegate(ActionMatchCreate action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.GreaterOrEqual(app1 ().getUser().getOpenMatches().Count,1);
			});
        }

        [Test]
        public void AsyncCreateNewMatch()
        {
            app1().getUser().getOpenMatches().createNewMatch(VALID_RIVAL).async (delegate(ActionMatchCreate action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.GreaterOrEqual(app1 ().getUser().getOpenMatches().Count,1);
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncCreateRandomMatch()
        {
			app1().getUser().getOpenMatches().createRandomMatch().sync (delegate(ActionMatchCreateRandom action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.GreaterOrEqual(app1 ().getUser().getOpenMatches().Count,1);
			});
        }

        [Test]
        public void AsyncCreateRandomMatch()
        {
            app1().getUser().getOpenMatches().createRandomMatch().async (delegate(ActionMatchCreateRandom action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.GreaterOrEqual(app1 ().getUser().getOpenMatches().Count,1);
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncCloseMatches()
        {
			List<String> list = new List<String>(app1 ().getUser().getOpenMatches().Keys);
			foreach(String matchId in list) {
				app1().getUser ().getOpenMatches()[matchId].close ().sync (delegate(ActionMatchClose action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				});
			}
			SyncGetOpenMatchList();
			Assert.True (app1 ().getUser ().getOpenMatches().Count == 0);
        }

        [Test]
        public void AsyncCloseMatches()
        {
			List<String> list = new List<String>(app1 ().getUser().getOpenMatches().Keys);
			foreach(String matchId in list) {
				app1().getUser ().getOpenMatches()[matchId].close ().async (delegate(ActionMatchClose action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
					Release ();
				});
				Wait ();
			}
			SyncGetOpenMatchList();
			Assert.True (app1 ().getUser ().getOpenMatches().Count == 0);
        }
		
		[Test]
        public void SyncResignAll()
        {
			List<String> list = new List<String>(app1 ().getUser().getOpenMatches().Keys);
			foreach(String matchId in list) {
				app1().getUser ().getOpenMatches()[matchId].resign ().sync (delegate(ActionMatchResign action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				});
			}
			SyncGetOpenMatchList();
			Assert.True (app1 ().getUser ().getOpenMatches().Count == 0);
        }

        [Test]
        public void AsyncResignAll()
        {
			List<String> list = new List<String>(app1 ().getUser().getOpenMatches().Keys);
			foreach(String matchId in list) {
				app1().getUser ().getOpenMatches()[matchId].resign ().async (delegate(ActionMatchResign action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
					Release ();
				});
				Wait ();
			}
			SyncGetOpenMatchList();
			Assert.True (app1 ().getUser ().getOpenMatches().Count == 0);
        }
		
		[Test]
        public void SyncRejectAll()
        {
			List<String> list = new List<String>(app1 ().getUser().getOpenMatches().Keys);
			foreach(String matchId in list) {
				app1().getUser ().getOpenMatches()[matchId].reject ().sync (delegate(ActionMatchReject action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				});
			}
			SyncGetOpenMatchList();
			Assert.True (app1 ().getUser ().getOpenMatches().Count == 0);
        }

        [Test]
        public void AsyncRejectAll()
        {
			List<String> list = new List<String>(app1 ().getUser().getOpenMatches().Keys);
			foreach(String matchId in list) {
				app1().getUser ().getOpenMatches()[matchId].reject ().async (delegate(ActionMatchReject action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
					Release ();
				});
				Wait ();
			}
			SyncGetOpenMatchList();
			Assert.True (app1 ().getUser ().getOpenMatches().Count == 0);
        }
		
		[Test]
        public void SyncGetOpenMatchList()
        {
			app1().getUser().getOpenMatches().get ().sync (delegate(ActionMatchGetOpen action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
			});
        }

        [Test]
        public void AsyncGetOpenMatchList()
        {
            app1().getUser().getOpenMatches().get ().async (delegate(ActionMatchGetOpen action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncGetClosedMatchList()
        {
			app1().getUser().getClosedMatches().get ().sync (delegate(ActionMatchGetClosed action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
			});
        }

        [Test]
        public void AsyncGetClosedMatchList()
        {
            app1().getUser().getClosedMatches().get ().async (delegate(ActionMatchGetClosed action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncGetStatus()
        {
			List<String> list = new List<String>(app1 ().getUser().getOpenMatches().Keys);
			foreach(String matchId in list) {
				app1().getUser ().getOpenMatches()[matchId].getStatus ().sync (delegate(ActionMatchGetStatus action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				});
			}
			if(app1 ().getUser ().getOpenMatches().Count == 0) Assert.Pass("No open matches found");
        }

        [Test]
        public void AsyncGetStatus()
        {
			List<String> list = new List<String>(app1 ().getUser().getOpenMatches().Keys);
			foreach(String matchId in list) {
				app1().getUser ().getOpenMatches()[matchId].getStatus ().async (delegate(ActionMatchGetStatus action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
					Release ();
				});
				Wait();
			}
			if(app1 ().getUser ().getOpenMatches().Count == 0) Assert.Pass("No open matches found");
        }
		
		[Test]
        public void SyncGetMoves()
        {
			foreach(OpenMatch match in app1 ().getUser().getOpenMatches().Values) {
				match.getMoves (0).sync (delegate(ActionMatchGetMoves action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				});
			}
			if(app1 ().getUser ().getOpenMatches().Count == 0) Assert.Pass("No open matches found");
        }

        [Test]
        public void AsyncGetMoves()
        {
            foreach(OpenMatch match in app1 ().getUser().getOpenMatches().Values) {
				match.getMoves (0).async (delegate(ActionMatchGetMoves action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
					Release ();
				});
				Wait ();
			}
			if(app1 ().getUser ().getOpenMatches().Count == 0) Assert.Pass("No open matches found");
        }
		
		
		/*[Test]
        public void SyncSendMoves()
        {
			foreach(OpenMatch match in app ().getUser().getOpenMatches().getMyTurn().Values) {
				match.sendMove(0,"dummy",new Dictionary<String,int>(),new Dictionary<String,int>(),false,new Dictionary<String,int>()).sync (delegate(ActionMatchMove action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				});
			}
        }

        [Test]
        public void AsyncSendMoves()
        {
            foreach(OpenMatch match in app ().getUser().getOpenMatches().getMyTurn().Values) {
				match.sendMove(1,"dummy",new Dictionary<String,int>(),new Dictionary<String,int>(),false,new Dictionary<String,int>()).async (delegate(ActionMatchMove action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
					Release ();
				});
				Wait ();
			}
        }
		
		[Test]
        public void SyncSelectItems()
        {
			foreach(OpenMatch match in app ().getUser().getOpenMatches().getMyTurn().Values) {
				match.selectItems(new Dictionary<String,int>(),0).sync (delegate(ActionMatchSelectItems action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				});
			}
        }

        [Test]
        public void AsyncSelectItems()
        {
            foreach(OpenMatch match in app ().getUser().getOpenMatches().getMyTurn().Values) {
				match.selectItems(new Dictionary<String,int>(),0).async (delegate(ActionMatchSelectItems action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
					Release ();
				});
				Wait ();
			}
        }*/
	}
}