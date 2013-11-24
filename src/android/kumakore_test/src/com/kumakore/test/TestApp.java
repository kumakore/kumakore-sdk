package com.kumakore.test;

import java.util.UUID;

import junit.framework.Assert;

import com.kumakore.ActionAppGetUserId;
import com.kumakore.ActionAppLog;
import com.kumakore.ActionAppSignup;
import com.kumakore.ActionAppUserPasswordReset;
import com.kumakore.KumakoreUtil;
import com.kumakore.StatusCodes;

public class TestApp extends TestBase {    

	public void testSyncLogInfo()
    {
        Assert.assertEquals(StatusCodes.SUCCESS, app().logInfo("Sync: Info").sync());
    }

    public void testSyncLogDebug()
    {
        Assert.assertEquals(StatusCodes.SUCCESS, app().logDebug("Sync: Debug").sync());
    }

    public void testSyncLogWarning()
    {
        Assert.assertEquals(StatusCodes.SUCCESS, app().logWarning("Sync: Warning").sync());
    }

    public void testSyncLogError()
    {
        Assert.assertEquals(StatusCodes.SUCCESS, app().logError("Sync: Error").sync());
    }

    public void testSyncLogCritical()
    {
        Assert.assertEquals(StatusCodes.SUCCESS, app().logCritical("Sync: Critical").sync());
    }

    public void testAsyncLogInfo()
    {
        app().logInfo("Async: Info").async(new ActionAppLog.IKumakore() {
			
			@Override
			public void onAppLog(ActionAppLog action) {
	            Assert.assertEquals(StatusCodes.SUCCESS, action.getStatusCode());
	            Release();				
			}
		});

        Wait();
    }

    public void testAsyncLogDebug()
    {
        app().logDebug("Async: Debug").async(new ActionAppLog.IKumakore() {
			
			@Override
			public void onAppLog(ActionAppLog action) {
	            Assert.assertEquals(StatusCodes.SUCCESS, action.getStatusCode());
	            Release();				
			}
		});

        Wait();
    }

    public void testAsyncLogWarning()
    {
        app().logWarning("Async: Warning").async(new ActionAppLog.IKumakore() {
			
			@Override
			public void onAppLog(ActionAppLog action) {
	            Assert.assertEquals(StatusCodes.SUCCESS, action.getStatusCode());
	            Release();				
			}
		});

        Wait();
    }

    public void testAsyncLogError()
    {
        app().logError("Async: Error").async(new ActionAppLog.IKumakore() {
			
			@Override
			public void onAppLog(ActionAppLog action) {
	            Assert.assertEquals(StatusCodes.SUCCESS, action.getStatusCode());
	            Release();				
			}
		});

        Wait();
    }

    public void testAsyncLogCritical()
    {
        app().logCritical("Async: Critical").async(new ActionAppLog.IKumakore() {
			
			@Override
			public void onAppLog(ActionAppLog action) {
	            Assert.assertEquals(StatusCodes.SUCCESS, action.getStatusCode());
	            Release();				
			}
		});

        Wait();
    }
    
    public void testSyncSignupViaEmail()
    {
        String email = "email_sync_" + UUID.randomUUID().toString() + "@email.com";
        Assert.assertEquals(StatusCodes.SUCCESS, app().signup(email).sync());
    }


    public void testAsyncSignupViaEmail()
    {
        String email = "email_async_" + UUID.randomUUID().toString() + "@email.com";
        app().signup(email).async(new ActionAppSignup.IKumakore() {
			
			@Override
			public void onActionAppSignup(ActionAppSignup action) {
	            Assert.assertEquals(StatusCodes.SUCCESS, action.getStatusCode());
	            Release();				
			}
		});

        Wait();
    }

    public void testSyncSignupViaUserName()
    {
        String userName = "username_sync_" + UUID.randomUUID().toString();
        Assert.assertEquals(StatusCodes.SUCCESS, app().signup(userName).sync());
    }

    public void testAsyncSignupViaUserName()
    {
        String userName = "username_async_" + UUID.randomUUID().toString();
        app().signup(userName).async(new ActionAppSignup.IKumakore() {
			
			@Override
			public void onActionAppSignup(ActionAppSignup action) {
				Assert.assertEquals(StatusCodes.SUCCESS, action.getStatusCode());
				Release();
			}
		});

        Wait();
    }

    public void testSyncGetUserId()
    {
        app().getUserId(TEST_USERNAME).sync(new ActionAppGetUserId.IKumakore() {
			
			@Override
			public void onActionUserId(ActionAppGetUserId action) {
	            Assert.assertEquals(StatusCodes.SUCCESS, action.getStatusCode());
	            Assert.assertFalse("userId is null or empty", KumakoreUtil.isNullOrEmpty(action.getUserId()));				
			}
		});
    }

    public void testAsyncGetUserId()
    {
        app().getUserId(TEST_USERNAME).async(new ActionAppGetUserId.IKumakore() {
			
			@Override
			public void onActionUserId(ActionAppGetUserId action) {
	            Assert.assertEquals(StatusCodes.SUCCESS, action.getStatusCode());
	            Assert.assertFalse("userId is null or empty", KumakoreUtil.isNullOrEmpty(action.getUserId()));
	            Release();				
			}
		});

        Wait();
    }

    public void testSyncPasswordReset()
    {
        Assert.assertEquals(StatusCodes.SUCCESS, app().passwordReset(TEST_EMAIL).sync());
    }

    public void testAsyncPasswordReset()
    {
        app().passwordReset(TEST_EMAIL).async(new ActionAppUserPasswordReset.IKumakore() {
			
			@Override
			public void onActionUserPasswordReset(ActionAppUserPasswordReset action) {
	            Assert.assertEquals(StatusCodes.SUCCESS, action.getStatusCode());
	            Release();				
			}
		});

        Wait();
    }
}
