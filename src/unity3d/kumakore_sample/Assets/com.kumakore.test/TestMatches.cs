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
		
		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
			app1 ().signin (TEST_1, PASSWORD).sync ();
		}
		
		[Test]
        public void SyncCreateNewMatch()
        {
			app1().getUser().getOpenMatches().createNewMatch(TEST_2).sync (delegate(ActionMatchCreate action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Assert.GreaterOrEqual(app1 ().getUser().getOpenMatches().Count,1);
			});
        }

        [Test]
        public void AsyncCreateNewMatch()
        {
			app1().getUser().getOpenMatches().createNewMatch(TEST_2).async (delegate(ActionMatchCreate action) {
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
        public void SyncCloseAll()
        {
			SyncCreateRandomMatch ();

			foreach(OpenMatch match in app1 ().getUser().getOpenMatches().Values) {
				match.close ().sync (delegate(ActionMatchClose action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				});
			}

			SyncGetOpenMatchMap();

			Assert.True (app1 ().getUser ().getOpenMatches().Count == 0);
        }

        [Test]
        public void AsyncCloseAll()
		{
			SyncCreateRandomMatch ();

			foreach(OpenMatch match in app1 ().getUser().getOpenMatches().Values) {
				match.close ().async (delegate(ActionMatchClose action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
					Release ();
				});
				Wait ();
			}

			SyncGetOpenMatchMap();

			Assert.True (app1 ().getUser ().getOpenMatches().Count == 0);
        }
		
		[Test]
        public void SyncResignAll()
		{
			SyncCreateRandomMatch ();

			foreach(OpenMatch match in app1 ().getUser().getOpenMatches().Values) {
				match.resign ().sync (delegate(ActionMatchResign action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				});
			}

			SyncGetOpenMatchMap();

			Assert.True (app1 ().getUser ().getOpenMatches().Count == 0);
        }

        [Test]
        public void AsyncResignAll()
		{
			SyncCreateRandomMatch ();

			foreach(OpenMatch match in app1 ().getUser().getOpenMatches().Values) {
				match.resign ().async (delegate(ActionMatchResign action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
					Release ();
				});
				Wait ();
			}

			SyncGetOpenMatchMap();

			Assert.True (app1 ().getUser ().getOpenMatches().Count == 0);
        }
		
		[Test]
        public void SyncRejectAll()
		{
			SyncCreateRandomMatch ();

			foreach(OpenMatch match in app1 ().getUser().getOpenMatches().Values) {
				match.reject ().sync (delegate(ActionMatchReject action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				});
			}

			SyncGetOpenMatchMap();

			Assert.True (app1 ().getUser ().getOpenMatches().Count == 0);
        }

        [Test]
        public void AsyncRejectAll()
		{
			SyncCreateRandomMatch ();

			foreach(OpenMatch match in app1 ().getUser().getOpenMatches().Values) {
				match.reject ().async (delegate(ActionMatchReject action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
					Release ();
				});
				Wait ();
			}

			SyncGetOpenMatchMap();

			Assert.True (app1 ().getUser ().getOpenMatches().Count == 0);
        }
		
		[Test]
        public void SyncGetOpenMatchMap()
        {
			app1().getUser().getOpenMatches().get ().sync (delegate(ActionMatchGetOpen action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
			});
        }

        [Test]
        public void AsyncGetOpenMatchMap()
        {
            app1().getUser().getOpenMatches().get ().async (delegate(ActionMatchGetOpen action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncGetClosedMatchMap()
        {
			app1().getUser().getClosedMatches().get ().sync (delegate(ActionMatchGetClosed action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
			});
        }

        [Test]
        public void AsyncGetClosedMatchMap()
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
			SyncCreateRandomMatch ();

			foreach(OpenMatch match in app1 ().getUser().getOpenMatches().Values) {
				match.getStatus ().sync (delegate(ActionMatchGetStatus action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				});
			}
        }

        [Test]
        public void AsyncGetStatus()
		{
			SyncCreateRandomMatch ();

			foreach(OpenMatch match in app1 ().getUser().getOpenMatches().Values) {
				match.getStatus ().async (delegate(ActionMatchGetStatus action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
					Release ();
				});
				Wait();
			}
        }
		
		[Test]
        public void SyncGetMoves()
		{
			SyncCreateRandomMatch ();

			foreach(OpenMatch match in app1 ().getUser().getOpenMatches().Values) {
				match.getMoves (0).sync (delegate(ActionMatchGetMoves action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				});
			}
        }

        [Test]
        public void AsyncGetMoves()
		{
			SyncCreateRandomMatch ();

            foreach(OpenMatch match in app1 ().getUser().getOpenMatches().Values) {
				match.getMoves (0).async (delegate(ActionMatchGetMoves action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
					Release ();
				});
				Wait ();
			}
        }

		[Test]
		public void SyncSendChatMessage()
		{
			SyncCreateRandomMatch ();

			foreach(OpenMatch match in app1 ().getUser().getOpenMatches().Values) {
				match.sendChatMessage ("TEST").sync (delegate(ActionMatchChatMessage action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				});
			}
		}

		[Test]
		public void AsyncSendChatMessage()
		{
			SyncCreateRandomMatch ();

			foreach(OpenMatch match in app1 ().getUser().getOpenMatches().Values) {
				match.sendChatMessage ("TEST").async (delegate(ActionMatchChatMessage action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
					Release ();
				});
				Wait();
			}
		}

		[Test]
		public void SyncGetChatMessage()
		{
			SyncSendChatMessage ();

			foreach(OpenMatch match in app1 ().getUser().getOpenMatches().Values) {
				match.actGetChatMessages().sync (delegate(ActionMatchGetChatMessage action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
					Assert.True(match.getChatMessages().Count > 0);
				});
			}
		}

		[Test]
		public void AsyncGetChatMessage()
		{
			SyncSendChatMessage ();

			foreach(OpenMatch match in app1 ().getUser().getOpenMatches().Values) {
				match.actGetChatMessages ().async (delegate(ActionMatchGetChatMessage action) {
					Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
					Assert.True(match.getChatMessages().Count > 0);
					Release ();
				});
				Wait();
			}
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