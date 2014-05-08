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

		public static void All ()
		{
			TestDatastore datastore = new TestDatastore ();
			datastore.setup ();

			datastore.SyncDatastore ();

			datastore.AsyncDatastore ();
		}

		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
			app1 ().signin (TEST_1, PASSWORD).sync ();
			app1 ().getUser ().getDatastore ().delete (DATASTORE_TYPE, DATASTORE_NAME).sync (delegate(ActionDatastoreDelete a) {
				//Kumakore.LOG (LogLevels.WARNING,"",a.getCode()+"");
			});
//			
//			app().getUser().getDatastore().create (DATASTORE_TYPE,DATASTORE_NAME,new Dictionary<String, Object>()).sync (delegate(ActionDatastoreCreate a) {
//				//Kumakore.LOG (LogLevels.WARNING,"",a.getCode()+"");
//			});
		}

		[Test]
		public void AsyncDatastore ()
		{
			AsyncCreateDatastore ();
//			AsyncGetDatastoreType();
			AsyncUpdateDatastore (); // includes Get async
			AsyncDeleteDatastore ();
		}

		[Test]
		public void SyncDatastore ()
		{
			SyncCreateDatastore ();
//			SyncGetDatastoreType();
			SyncUpdateDatastore (); // includes Get sync
			SyncDeleteDatastore ();
		}

		public void SyncUpdateDatastore ()
		{
			app1 ().getUser ().getDatastore ().get (DATASTORE_TYPE, DATASTORE_NAME).sync (delegate(ActionDatastoreGet action) {
				Assert.AreEqual (StatusCodes.SUCCESS, action.getCode ());
				IDictionary<String,Object> db = new Dictionary<String,Object> ();
				db.Add (DATASTORE_FIELD, "a");
				app1 ().getUser ().getDatastore ().update (DATASTORE_TYPE, DATASTORE_NAME, db).sync (delegate(ActionDatastoreUpdate a) {
					Assert.AreEqual (StatusCodes.SUCCESS, a.getCode ());
					foreach (KeyValuePair<String,DatastoreObject> d in app1().getUser().getDatastore()) {
						Kumakore.LOG (LogLevels.WARNING, "", d.Key + " - " + d.Value.getData ().Count);
					}
				});
			});
			
		}

		public void AsyncUpdateDatastore ()
		{
			app1 ().getUser ().getDatastore ().get (DATASTORE_TYPE, DATASTORE_NAME).sync (delegate(ActionDatastoreGet action) {
				Assert.AreEqual (StatusCodes.SUCCESS, action.getCode ());
				IDictionary<String,Object> db = new Dictionary<String,Object> ();
				db.Add (DATASTORE_FIELD, "b");
				app1 ().getUser ().getDatastore ().update (DATASTORE_TYPE, DATASTORE_NAME, db).async (delegate(ActionDatastoreUpdate a) {
					Assert.AreEqual (StatusCodes.SUCCESS, a.getCode ());
					foreach (KeyValuePair<String,DatastoreObject> d in app1().getUser().getDatastore()) {
						Kumakore.LOG (LogLevels.WARNING, "", d.Key + " - " + d.Value.getData ().Count);
					}
					Release ();
				});

				Wait ();
			});
		}

		public void SyncDeleteDatastore ()
		{
			app1 ().getUser ().getDatastore ().delete (DATASTORE_TYPE, DATASTORE_NAME).sync (delegate(ActionDatastoreDelete action) {
				Assert.AreEqual (StatusCodes.SUCCESS, action.getCode ());
			});
		}

		public void AsyncDeleteDatastore ()
		{
			app1 ().getUser ().getDatastore ().delete (DATASTORE_TYPE, DATASTORE_NAME).async (delegate(ActionDatastoreDelete action) {
				Assert.AreEqual (StatusCodes.SUCCESS, action.getCode ());
				Release ();
			});

			Wait ();
		}

		public void AsyncCreateDatastore ()
		{
			app1 ().getUser ().getDatastore ().create (DATASTORE_TYPE, DATASTORE_NAME, new Dictionary<String,Object> ()).async (delegate(ActionDatastoreCreate action) {
				Assert.AreEqual (StatusCodes.SUCCESS, action.getCode ());
				Release ();
			});

			Wait ();
		}

		public void SyncCreateDatastore ()
		{
			app1 ().getUser ().getDatastore ().create (DATASTORE_TYPE, DATASTORE_NAME, new Dictionary<String,Object> ()).sync (delegate(ActionDatastoreCreate action) {
				Assert.AreEqual (StatusCodes.SUCCESS, action.getCode ());
			});
		}

		public void SyncGetDatastoreType ()
		{
			app1 ().getUser ().getDatastore ().get (DATASTORE_TYPE).sync (delegate(ActionDatastoreGet action) {
				Assert.AreEqual (StatusCodes.SUCCESS, action.getCode ());
			});
		}

		public void AsyncGetDatastoreType ()
		{
			app1 ().getUser ().getDatastore ().get (DATASTORE_TYPE).async (delegate(ActionDatastoreGet action) {
				Assert.AreEqual (StatusCodes.SUCCESS, action.getCode ());
				Release ();
			});

			Wait ();
		}
	}
}