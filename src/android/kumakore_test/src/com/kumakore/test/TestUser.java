package com.kumakore.test;

import junit.framework.Assert;

import com.kumakore.ActionUserGet;
import com.kumakore.StatusCodes;

public class TestUser extends TestBase {    

	private static final String TEST_USERNAME2 = "test2";
	private static final String TEST_USERNAME3 = "test3";
	
	public void testSyncUserGet()
    {
        Assert.assertEquals(StatusCodes.SUCCESS, app().getUser().get().sync());
        Assert.assertEquals(TEST_EMAIL, app().getUser().getEmail());
        Assert.assertEquals(TEST_USERNAME, app().getUser().getName());
    }
	

	public void testAsyncUserGet()
    {
		app().getUser().get().async(new ActionUserGet.IKumakore() {
			
			@Override
			public void onActionUserGet(ActionUserGet action) {
		        Assert.assertEquals(StatusCodes.SUCCESS, action.getStatusCode());
		        Assert.assertEquals(TEST_EMAIL, app().getUser().getEmail());
		        Assert.assertEquals(TEST_USERNAME, app().getUser().getName());
		        Release();
			}
		});
		
		Wait();
    }
	
	public void testSyncUserUpdate()
    {
        Assert.assertEquals(StatusCodes.SUCCESS, app().getUser().update().setName(TEST_USERNAME2).sync());
        Assert.assertEquals(TEST_USERNAME2, app().getUser().getName());
    }

//	
//	public void testSyncUserUpdate()
//    {
//		app().user().update().setName(TEST_USERNAME3).sync()
//		
//        Assert.assertEquals(StatusCodes.SUCCESS, );
//        Assert.assertEquals(TEST_USERNAME2, app().user().getName());
//    }
}
