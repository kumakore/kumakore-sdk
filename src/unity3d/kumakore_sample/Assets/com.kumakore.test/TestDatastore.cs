using System;
using com.kumakore;
using NUnit.Framework;
using System.Threading;
using System.Collections.Generic;

namespace com.kumakore.test
{
    public class TestObject {
        public String _type, _name, _key, _value, _update;

        public TestObject(String t, String n, String k, String v, String u) {
            _type = t; _name = n; _key = k; _value = v; _update = u;
        }
    }

	[TestFixture]
	public class TestDatastore : TestBase
	{
        private readonly static String DATASTORE_TYPE1 = "type1";
        private readonly static String DATASTORE_TYPE2 = "type2";

        private IDictionary<String, TestObject> testmap;
        private List<TestObject> testlist;

		public static void All ()
		{
			TestDatastore datastore = new TestDatastore ();
			datastore.setup ();

            datastore.DeleteAll();
            datastore.SyncDataCreate();
		}

		[TestFixtureSetUp]
		public override void setup ()
		{
            testmap = new Dictionary<string, TestObject>();
            testmap.Add("a", new TestObject(DATASTORE_TYPE1, "name1", "key1", "val1", "up1"));
            testmap.Add("b", new TestObject(DATASTORE_TYPE1, "name2", "key2", "val2", "up2"));
            testmap.Add("c", new TestObject(DATASTORE_TYPE2, "name3", "key3", "val3", "up3"));

            testlist = new List<TestObject>() { testmap["a"], testmap["b"], testmap["c"] };

			base.setup ();
			app1 ().signin (TEST_1, PASSWORD).sync ();

//            DeleteAll();

//			app1 ().getUser ().getDatastore ().delete (DATASTORE_TYPE, DATASTORE_NAME).sync (delegate(ActionDatastoreDelete a) {
//                if(a.getCode() != StatusCodes.SUCCESS)
//                    Kumakore.LOG (LogLevels.WARNING,"",a.getCode().ToString());
//			});

//			
//			app().getUser().getDatastore().create (DATASTORE_TYPE,DATASTORE_NAME,new Dictionary<String, Object>()).sync (delegate(ActionDatastoreCreate a) {
//				//Kumakore.LOG (LogLevels.WARNING,"",a.getCode()+"");
//			});

		}

        public void DeleteAll() {
            foreach(KeyValuePair<String, TestObject> pair in testmap) {
                app1().getUser().getDatastore().delete(pair.Value._type, pair.Value._name).sync(delegate(ActionDatastoreDelete a) {
                    if(a.getCode() == StatusCodes.SUCCESS)
                        Console.WriteLine("Deleteall " + pair.Value._type + " " + pair.Value._name);
                    else
                        Console.WriteLine("Deleteall failed " + pair.Value._type + " " + pair.Value._name);
                });
            }
        }

        [Test]
        public void SyncDataCreate() {
			for (int i=0; i < 1000; i++) {
					DeleteAll ();
					SyncCreateDatastore (testlist);
			}
        }

        [Test]
        public void AsyncDataCreate() {
            DeleteAll();
            AsyncCreateDatastore(testlist);
        }

        [Test]
        public void SyncDataGetAll() {
            DeleteAll();

            SyncCreateDatastore(testlist);
            SyncGetDatastore(testlist);
        }

        [Test]
        public void SyncDataGetType() {
            DeleteAll();

            SyncCreateDatastore(testlist);
            app1().getUser().getDatastore().Clear();

            List<TestObject> l = new List<TestObject>() { testmap["a"], testmap["b"] };
            SyncGetDatastore(DATASTORE_TYPE1, l);

            Assert.IsTrue(app1().getUser().getDatastore().ContainsKey(DATASTORE_TYPE1));
            Assert.IsFalse(app1().getUser().getDatastore()[DATASTORE_TYPE1].ContainsKey(testmap["c"]._name));
            Assert.IsFalse(app1().getUser().getDatastore().ContainsKey(DATASTORE_TYPE2));
        }

        [Test]
        public void SyncDataGetOnType() {
            DeleteAll();

            SyncCreateDatastore(testlist);
            app1().getUser().getDatastore().Remove(DATASTORE_TYPE2);

            List<TestObject> l = new List<TestObject>() { testmap["a"], testmap["b"] };
            SyncGetDatastoreOnType(DATASTORE_TYPE1, l);

            Assert.IsTrue(app1().getUser().getDatastore().ContainsKey(DATASTORE_TYPE1));
            Assert.IsFalse(app1().getUser().getDatastore()[DATASTORE_TYPE1].ContainsKey(testmap["c"]._name));
            Assert.IsFalse(app1().getUser().getDatastore().ContainsKey(DATASTORE_TYPE2));
        }

        [Test]
        public void SyncDataDelete() {
            DeleteAll();

            SyncCreateDatastore(testlist);
            SyncDeleteDatastore(testlist);
        }

        [Test]
        public void SyncDataUpdate() {
            DeleteAll();

            SyncCreateDatastore(testlist);
            SyncUpdateDatastore(testlist);
        }

        [Test]
        public void SyncDataUpdateOnType() {
            DeleteAll();

            SyncCreateDatastore(testlist);
            SyncUpdateDatastore(testlist);
        }

        [Test]
        public void SyncDataUpdateOnObject() {
            DeleteAll();

            SyncCreateDatastore(testlist);
            SyncUpdateDatastoreOnObject(testlist);

            DeleteAll();

            SyncCreateDatastore(testlist);
            SyncUpdateDatastoreOnObject1(testlist);
        }

        /*
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
  */      
        void VerifyValue(TestObject tobj) {
            DatastoreObject obj = app1().getUser().getDatastore()[tobj._type][tobj._name];
            Assert.AreEqual(tobj._type, obj.getObjectType());
            Assert.AreEqual(tobj._name, obj.getObjectName());
            Assert.AreEqual(obj.getData()[tobj._key], tobj._value);
        }

        void VerifyUpdate(TestObject tobj) {
            DatastoreObject obj = app1().getUser().getDatastore()[tobj._type][tobj._name];
            Assert.AreEqual(tobj._type, obj.getObjectType());
            Assert.AreEqual(tobj._name, obj.getObjectName());
            Assert.AreEqual(obj.getData()[tobj._key], tobj._update);
        }

        public void SyncDeleteDatastore (List<TestObject> objList)
		{
            foreach(TestObject tobj in objList) {
                app1().getUser().getDatastore().delete(tobj._type, tobj._name).sync(delegate(ActionDatastoreDelete action) {
                    Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                    Assert.IsFalse(app1().getUser().getDatastore()[tobj._type].ContainsKey(tobj._name));
                });
            }
		}
        /*
        public void AsyncDeleteDatastore (String objname)
		{
            TestObject tobj = testmap[objname];

            app1 ().getUser ().getDatastore ().actionDelete (tobj._type, tobj._name).async (delegate(ActionDatastoreDelete action) {
				Assert.AreEqual (StatusCodes.SUCCESS, action.getCode ());
				Release ();
			});

			Wait ();
		}

        public void AsyncCreateDatastore (String objname)
		{
            TestObject tobj = testmap[objname];

            app1 ().getUser ().getDatastore ().actionCreate (tobj._type, tobj._name, new Dictionary<String,Object> ()).async (delegate(ActionDatastoreCreate action) {
				Assert.AreEqual (StatusCodes.SUCCESS, action.getCode ());
				Release ();
			});

			Wait ();
		}
  */      

        public void SyncCreateDatastore (List<TestObject> objList)
		{
            foreach(TestObject tobj in objList) {
                Dictionary<String,Object> tmp = new Dictionary<string, object>();
                tmp.Add(tobj._key, tobj._value);

                app1().getUser().getDatastore().create(tobj._type, tobj._name, tmp).sync(delegate(ActionDatastoreCreate action) {
                    Console.WriteLine("Create " + tobj._type + " / " + tobj._name);
                    Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                    VerifyValue(tobj);
                });
            }
		}

        public void AsyncCreateDatastore (List<TestObject> objList)
        {
            foreach(TestObject tobj in objList) {
                Dictionary<String,Object> tmp = new Dictionary<string, object>();
                tmp.Add(tobj._key, tobj._value);

                app1().getUser().getDatastore().create(tobj._type, tobj._name, tmp).async(delegate(ActionDatastoreCreate action) {
                    Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());

                    VerifyValue(tobj);
                    Release();
                });

                Wait();
            }
        }

        public void SyncGetDatastore (List<TestObject> objList)
        {
            app1 ().getUser ().getDatastore ().get ().sync (delegate(ActionDatastoreGet action) {
                Assert.AreEqual (StatusCodes.SUCCESS, action.getCode ());

            });

            foreach(TestObject tobj in objList) {
                VerifyValue(tobj);
            }
        }

        public void SyncGetDatastore (String type, List<TestObject> objList)
		{
            app1 ().getUser ().getDatastore ().get (type).sync (delegate(ActionDatastoreGet action) {
				Assert.AreEqual (StatusCodes.SUCCESS, action.getCode ());

			});

            foreach(TestObject tobj in objList) {
                VerifyValue(tobj);
            }
		}

        public void SyncGetDatastoreOnType (String type, List<TestObject> objList)
        {
            app1().getUser().getDatastore()[type].Clear();
            app1().getUser().getDatastore()[type].get().sync(delegate(ActionDatastoreGet action) {
                Assert.AreEqual (StatusCodes.SUCCESS, action.getCode ());

            });

            foreach(TestObject tobj in objList) {
                VerifyValue(tobj);
            }
        }

        /*
        public void AsyncGetDatastoreType (String objname)
		{
            TestObject tobj = testmap[objname];

            app1 ().getUser ().getDatastore ().actionGet (tobj._type).async (delegate(ActionDatastoreGet action) {
				Assert.AreEqual (StatusCodes.SUCCESS, action.getCode ());

                DatastoreObject obj = app1().getUser().getDatastore()[tobj._type][tobj._name];
                Assert.AreEqual(tobj._type, obj.getObjectType());
                Assert.AreEqual(tobj._name, obj.getObjectName());
                Assert.AreEqual(obj.getData()[tobj._key], tobj._value);

				Release ();
			});

			Wait ();
		}
  */      

        public void SyncUpdateDatastore(List<TestObject> objList) {
            foreach(TestObject tobj in objList) {
                Dictionary<String,Object> tmp = new Dictionary<string, object>();
                tmp.Add(tobj._key, tobj._update);

                app1().getUser().getDatastore().update(tobj._type, tobj._name, tmp).sync(delegate(ActionDatastoreUpdate action) {
                    Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());

                    VerifyUpdate(tobj);
                });
            }
        }

        public void SyncUpdateDatastoreOnType(List<TestObject> objList) {
            foreach(TestObject tobj in objList) {
                Dictionary<String,Object> tmp = new Dictionary<string, object>();
                tmp.Add(tobj._key, tobj._update);

                app1().getUser().getDatastore()[tobj._type].update(tobj._name, tmp).sync(delegate(ActionDatastoreUpdate action) {
                    Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());

                    VerifyUpdate(tobj);
                });
            }
        }

        public void SyncUpdateDatastoreOnObject(List<TestObject> objList) {
            foreach(TestObject tobj in objList) {
                DatastoreObject o = app1().getUser().getDatastore()[tobj._type][tobj._name];
                o.getData()[tobj._key] = tobj._update;

                o.update().sync(delegate(ActionDatastoreUpdate action) {
                    Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());

                    VerifyUpdate(tobj);
                });
            }
        }

        public void SyncUpdateDatastoreOnObject1(List<TestObject> objList) {
            foreach(TestObject tobj in objList) {
                DatastoreObject o = app1().getUser().getDatastore()[tobj._type][tobj._name];

                Dictionary<String,Object> tmp = new Dictionary<string, object>();
                tmp.Add(tobj._key, tobj._update);

                o.update(tmp).sync(delegate(ActionDatastoreUpdate action) {
                    Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());

                    VerifyUpdate(tobj);
                });
            }
        }
    }
}