package com.kumakore.test;

import java.util.HashMap;

import junit.framework.Assert;

import com.kumakore.ActionAppLog;
import com.kumakore.ActionDatastoreCreate;
import com.kumakore.StatusCodes;

public class TestDatastore extends TestBase {

	private static final String NAME = "name";
	private static final String TYPE = "type";
	private static final HashMap<String, Object> DATA = new HashMap<String, Object>();
	private static final String SOME_DATA_KEY = "some_data";
	private static final int SOME_DATA_VALUE = 1;
	private static final String ANOTHER_DATA_KEY = "another_data";
	private static final String ANOTHER_DATA_VALUE = "ANOTHER";
	
	@Override
	protected void setUp() throws Exception {
		super.setUp();
		
		DATA.put(SOME_DATA_KEY, SOME_DATA_VALUE);
		DATA.put(ANOTHER_DATA_KEY, ANOTHER_DATA_VALUE);
	}
	
    public void testSyncDatastoreCreate()
    {
        Assert.assertEquals(StatusCodes.SUCCESS, app().getUser().datastore().create(NAME, TYPE, DATA).sync());
    }

    public void testAsyncDatastoreCreate()
    {
        app().getUser().datastore().create(NAME, TYPE, DATA).async(new ActionDatastoreCreate.IKumakore() {
        	
			@Override
			public void onActionDatastoreCreate(ActionDatastoreCreate action) {
	            Assert.assertEquals(StatusCodes.SUCCESS, action.getStatusCode());
	            Release();				
			}
		});

        Wait();
    }
}
