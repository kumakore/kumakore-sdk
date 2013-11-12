using System;
using com.kumakore;
using NUnit.Framework;
using System.Threading;
using System.Collections.Generic;

namespace com.kumakore.test
{
    [TestFixture]
    public class TestDatastore : TestBase
	{
		private readonly static String DATASTORE_TYPE = "myType";
		private readonly static String DATASTORE_NAME = "myName";
		private readonly static String DATASTORE_FIELD = "myField";
		
		
        public static void All()
        {
            TestDatastore datastore = new TestDatastore();
            datastore.setup();
			
			datastore.SyncUpdateDatastore(); // includes Get sync
			datastore.AsyncUpdateDatastore(); // includes Get async
			datastore.SyncGetDatastoreType();
			datastore.AsyncGetDatastoreType();
			datastore.SyncDeleteDatastore();
			datastore.AsyncCreateDatastore();
			datastore.AsyncDeleteDatastore();
        }
		
		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
			app ().signin(VALID_TEST_EMAIL,VALID_TEST_PASSWORD).sync ();
			app ().user().datastore().delete(DATASTORE_TYPE,DATASTORE_NAME).sync ();
			app ().user ().datastore().create (DATASTORE_TYPE,DATASTORE_NAME,new Dictionary<String,Object>()).sync ();
		}
		
		
		[Test]
        public void SyncUpdateDatastore()
        {
			app().user().datastore().get(DATASTORE_TYPE,DATASTORE_NAME).sync(delegate(ActionDatastoreGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
				IDictionary<String,Object> db = new Dictionary<String,Object>();
				db.Add (DATASTORE_FIELD,"a");
				app().user().datastore().update (DATASTORE_TYPE,DATASTORE_NAME,db).sync(delegate(ActionDatastoreUpdate a)
	            {
	                Assert.AreEqual(StatusCodes.SUCCESS, a.getStatusCode());
					foreach(KeyValuePair<String,DatastoreObject> d in app ().user ().datastore()) {
						Kumakore.LOG (LogLevels.WARNING,"",d.Key + " - " + d.Value.getData().Count);
					}
	            });
            });
			
        }

        [Test]
        public void AsyncUpdateDatastore()
        {
            app().user().datastore().get(DATASTORE_TYPE,DATASTORE_NAME).async(delegate(ActionDatastoreGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
				IDictionary<String,Object> db = new Dictionary<String,Object>();
				db.Add (DATASTORE_FIELD,"b");
            	app().user().datastore().update (DATASTORE_TYPE,DATASTORE_NAME,db).async(delegate(ActionDatastoreUpdate a)
	            {
	                Assert.AreEqual(StatusCodes.SUCCESS, a.getStatusCode());
					foreach(KeyValuePair<String,DatastoreObject> d in app ().user ().datastore()) {
						Kumakore.LOG (LogLevels.WARNING,"",d.Key + " - " + d.Value.getData().Count);
					}
					Release ();
	            });
				Wait ();
				Release ();
            });
			
			Wait();
        }
		
		[Test]
        public void SyncDeleteDatastore()
        {
            app().user().datastore().delete (DATASTORE_TYPE,DATASTORE_NAME).sync(delegate(ActionDatastoreDelete action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
            });
        }

        [Test]
        public void AsyncDeleteDatastore()
        {
            app().user().datastore().delete (DATASTORE_TYPE,DATASTORE_NAME).async(delegate(ActionDatastoreDelete action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void AsyncCreateDatastore()
        {
            app().user().datastore().create(DATASTORE_TYPE,DATASTORE_NAME, new Dictionary<String,Object>()).async(delegate(ActionDatastoreCreate action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
        }
		
		[Test]
        public void SyncGetDatastoreType()
        {
            app().user().datastore().get (DATASTORE_TYPE).sync(delegate(ActionDatastoreGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
            });
        }

        [Test]
        public void AsyncGetDatastoreType()
        {
            app().user().datastore().get (DATASTORE_TYPE).async(delegate(ActionDatastoreGet action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getStatusCode());
                Release();
            });

            Wait();
        }
	}
}