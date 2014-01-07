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
		//private readonly static String VALID_RIVAL = "carlos";
		
	    public static void All()
        {
            TestMatches matches = new TestMatches();
            matches.setup();
			
			// test currentMatch
			/*matches.SyncCreateNewMatch();
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
			
			matches.AsyncCreateRandomMatch();
			matches.SyncGetMatchCompletedList();
			matches.AsyncGetMatchCompletedList();*/
			
        }
		
		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
			app ().signin(TEST_1,PASSWORD).sync ();
		}
		
		
		/*[Test]
        public void SyncCreateNewMatch()
        {
			app().getUser().currentMatch().createNewMatch(VALID_RIVAL).sync (delegate(ActionMatchCreateNew action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
			});
        }

        [Test]
        public void AsyncCreateNewMatch()
        {
            app().getUser().currentMatch().createNewMatch(VALID_RIVAL).async (delegate(ActionMatchCreateNew action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncCreateRandomMatch()
        {
			app().getUser().currentMatch().createRandomMatch().sync (delegate(ActionMatchCreateRandom action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
			});
        }

        [Test]
        public void AsyncCreateRandomMatch()
        {
            app().getUser().currentMatch().createRandomMatch().async (delegate(ActionMatchCreateRandom action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncCloseMatch()
        {
			app().getUser().currentMatch().close (app().getUser().currentMatch().match.getMatchId ()).sync (delegate(ActionMatchClose action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
			});
        }

        [Test]
        public void AsyncCloseMatch()
        {
            app().getUser().currentMatch().close (app().getUser().currentMatch().match.getMatchId ()).async (delegate(ActionMatchClose action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncGetMatchCompletedList()
        {
			app().getUser().getCompletedMatches().get ().sync (delegate(ActionMatchListCompleted action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
			});
        }

        [Test]
        public void AsyncGetMatchCompletedList()
        {
            app().getUser().getCompletedMatches().get ().async (delegate(ActionMatchListCompleted action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncGetMatchCurrentList()
        {
			app().getUser().getCurrentMatches().get ().sync (delegate(ActionMatchListCurrent action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
			});
        }

        [Test]
        public void AsyncGetMatchCurrentList()
        {
            app().getUser().getCurrentMatches().get ().async (delegate(ActionMatchListCurrent action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncGetStatus()
        {
			app().getUser().currentMatch().getStatus ().sync (delegate(ActionMatchStatusGet action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
			});
        }

        [Test]
        public void AsyncGetStatus()
        {
            app().getUser().currentMatch().getStatus ().async (delegate(ActionMatchStatusGet action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncGetMoves()
        {
			app().getUser().currentMatch().getMoves (0).sync (delegate(ActionMatchMoves action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
			});
        }

        [Test]
        public void AsyncGetMoves()
        {
            app().getUser().currentMatch().getMoves (0).async (delegate(ActionMatchMoves action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }*/
		
		/*[Test]
        public void SyncSendMoves()
        {
			app().getUser().currentMatch().sendMove(0,"dummy",new Dictionary<String,int>(),new Dictionary<String,int>(),0,false,new Dictionary<String,int>()).sync (delegate(ActionMatchSendMove action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
			});
        }

        [Test]
        public void AsyncSendMoves()
        {
            app().getUser().currentMatch().sendMove(0,"dummy",new Dictionary<String,int>(),new Dictionary<String,int>(),0,false,new Dictionary<String,int>()).async (delegate(ActionMatchSendMove action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncSelectItems()
        {
			app().getUser().currentMatch().selectItems(new Dictionary<String,int>(),0).sync (delegate(ActionMatchSelectItems action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
			});
        }

        [Test]
        public void AsyncSelectItems()
        {
            app().getUser().currentMatch().selectItems(new Dictionary<String,int>(),0).async (delegate(ActionMatchSelectItems action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncResign()
        {
			app().getUser().currentMatch().resign().sync (delegate(ActionMatchResign action) {
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
			});
        }

        [Test]
        public void AsyncResign()
        {
            app().getUser().currentMatch().resign().async (delegate(ActionMatchResign action) {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }*/
	}
}