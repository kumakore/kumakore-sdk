package com.kumakore.test;

import junit.framework.Assert;
import android.test.ActivityInstrumentationTestCase2;
import android.test.AndroidTestCase;

import com.kumakore.KumakoreApp;
import com.kumakore.StatusCodes;

public class TestBase extends AndroidTestCase
{

	private static final String TAG = TestBase.class.getName();
    private KumakoreApp _app;
    private AutoResetEvent _resetEvent = new AutoResetEvent(false);
    private static final int MAX_TEST_TIMEOUT = 5000;
    protected static final String TEST_EMAIL = "test@sinuouscode.com";
    protected static final String TEST_USERNAME = "test";

    @Override
    protected void setUp() throws Exception {
    	super.setUp();

//        Intent intent = new Intent(getInstrumentation().getTargetContext(),
//        		TestActivity.class);
//        startActivity(intent, null, null);
        
        _app = new KumakoreApp(Helpers.GetApiKey(),
                Helpers.GetDashboardVersion(), Helpers.GetAppVersion(), getContext());

         // create test account
         if (app().signup(TEST_EMAIL).sync() == StatusCodes.SUCCESS)
         {
             app().user().update().setName(TEST_USERNAME).sync();
         }
    }

	protected KumakoreApp app ()
	{
		return _app;
	}

    protected void Release()
    {
        _resetEvent.set();
    }

    protected void Wait()
    {
        Assert.assertTrue("AutoResetEvent timeout " + MAX_TEST_TIMEOUT + " exceeded", _resetEvent.waitOne(MAX_TEST_TIMEOUT));
    }
}
