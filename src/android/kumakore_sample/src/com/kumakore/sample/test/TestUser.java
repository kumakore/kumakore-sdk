package com.kumakore.sample.test;

import com.kumakore.ActionUserSignin;
import com.kumakore.ActionUserGet;
import com.kumakore.KumakoreApp;

public class TestUser extends TestBase implements ActionUserSignin.IKumakore {
	private static final String TAG = TestUser.class.getName();

	public TestUser(KumakoreApp app) {
		super(app);
	}
	
	@Override
	protected void onRun() {
		testSignup();			
	}
	
	public void testSignup() {
		// user signup
		// app.signup("chbfiv@gmail.com").async();
		// app.signup("larry2738").async();

		// user signin
		// DEFAULT ACCOUNT
		// app.signin("larry@somewhere.com", "").async();
		// GOOD PASSWORD
		app().signin("ottolb@gmail.com", "qwe").async(TestUser.this);
		//app().signin("chbfiv@gmail.com", "08080808").async(TestUser.this);
				
		// BAD PASSWORD
		// app.signin("chbfiv@gmail.com", "0808").async();
		
		// ## example : user reset password ##
		// app.passwordReset("chbfiv@gmail.com").async();
	}

	@Override
	public void onActionUserSignin(ActionUserSignin action) {
		// ## example : get user ##
		app().getUser().get().async(new ActionUserGet.IKumakore() {

			@Override
			public void onActionUserGet(ActionUserGet action) {

				// ## example : update user ##
				// use params, builder pattern, or both
				// app.user().update("chbfiv").async();
				// app.user().update("chbfiv",
				// "chbfiv@gmail.com").async();
				// app.user().update("chbfiv",
				// "chbfiv@gmail.com",
				// "80808080").async();
				// app.user().update().setPassword("xxx").setName("xxx").setEmail("xxx").async();
				// app.user().update().setName("chbfiv").async();
			}
		});
	}

}
