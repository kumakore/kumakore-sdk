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
			
			// test currentMatch
			matches.SyncCreateNewMatch();
			matches.SyncGetMatchCurrentList();
			matches.SyncCloseMatch();
			
			matches.AsyncCreateNewMatch();
			matches.AsyncGetMatchCurrentList();
			matches.AsyncCloseMatch();
			
			matches.SyncCreateRandomMatch();
			matches.SyncGetStatus();
			matches.AsyncGetStatus();
			matches.SyncGetMoves();
			matches.AsyncGetMoves();
			//matches.SyncSendMoves();
			//matches.SyncSelectItems();
			//matches.SyncResign();
			
			matches.AsyncCreateRandomMatch();
			//matches.AsyncSendMoves();
			//matches.AsyncSelectItems();
			//matches.AsyncResign();
			// completed matches
			matches.SyncGetMatchCompletedList();
			matches.AsyncGetMatchCompletedList();
			
        }
		
		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
			app ().signin(VALID_TEST_EMAIL,VALID_TEST_PASSWORD).sync ();
		}
		
		
		[Test]
        public void SyncCreateNewMatch()
        {
			app ().user ().currentMatch().createNewMatch(VALID_RIVAL).sync (delegate(ActionMatchCreateNew action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
			});
        }

        [Test]
        public void AsyncCreateNewMatch()
        {
            app ().user ().currentMatch().createNewMatch(VALID_RIVAL).async (delegate(ActionMatchCreateNew action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncCreateRandomMatch()
        {
			app ().user ().currentMatch().createRandomMatch().sync (delegate(ActionMatchCreateRandom action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
			});
        }

        [Test]
        public void AsyncCreateRandomMatch()
        {
            app ().user ().currentMatch().createRandomMatch().async (delegate(ActionMatchCreateRandom action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncCloseMatch()
        {
			app().user ().currentMatch().close (app ().user ().currentMatch().match.getMatchId ()).sync (delegate(ActionMatchClose action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
			});
        }

        [Test]
        public void AsyncCloseMatch()
        {
            app().user ().currentMatch().close (app ().user ().currentMatch().match.getMatchId ()).async (delegate(ActionMatchClose action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncGetMatchCompletedList()
        {
			app ().user ().getCompletedMatches().get ().sync (delegate(ActionMatchListCompleted action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
			});
        }

        [Test]
        public void AsyncGetMatchCompletedList()
        {
            app ().user ().getCompletedMatches().get ().async (delegate(ActionMatchListCompleted action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncGetMatchCurrentList()
        {
			app ().user ().getCurrentMatches().get ().sync (delegate(ActionMatchListCurrent action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
			});
        }

        [Test]
        public void AsyncGetMatchCurrentList()
        {
            app ().user ().getCurrentMatches().get ().async (delegate(ActionMatchListCurrent action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncGetStatus()
        {
			app ().user ().currentMatch().getStatus ().sync (delegate(ActionMatchStatusGet action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
			});
        }

        [Test]
        public void AsyncGetStatus()
        {
            app ().user ().currentMatch().getStatus ().async (delegate(ActionMatchStatusGet action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncGetMoves()
        {
			app ().user ().currentMatch().getMoves (0).sync (delegate(ActionMatchMoves action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
			});
        }

        [Test]
        public void AsyncGetMoves()
        {
            app ().user ().currentMatch().getMoves (0).async (delegate(ActionMatchMoves action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
        }
		
		/*[Test]
        public void SyncSendMoves()
        {
			app ().user ().currentMatch().sendMove(0,"dummy",new Dictionary<String,int>(),new Dictionary<String,int>(),0,false,new Dictionary<String,int>()).sync (delegate(ActionMatchSendMove action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
			});
        }

        [Test]
        public void AsyncSendMoves()
        {
            app ().user ().currentMatch().sendMove(0,"dummy",new Dictionary<String,int>(),new Dictionary<String,int>(),0,false,new Dictionary<String,int>()).async (delegate(ActionMatchSendMove action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncSelectItems()
        {
			app ().user ().currentMatch().selectItems(new Dictionary<String,int>(),0).sync (delegate(ActionMatchSelectItems action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
			});
        }

        [Test]
        public void AsyncSelectItems()
        {
            app ().user ().currentMatch().selectItems(new Dictionary<String,int>(),0).async (delegate(ActionMatchSelectItems action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncResign()
        {
			app ().user ().currentMatch().resign().sync (delegate(ActionMatchResign action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
			});
        }

        [Test]
        public void AsyncResign()
        {
            app ().user ().currentMatch().resign().async (delegate(ActionMatchResign action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
        }*/
	}
}