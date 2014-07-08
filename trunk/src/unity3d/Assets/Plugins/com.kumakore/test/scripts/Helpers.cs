using System;

namespace com.kumakore.test
{
	public class Helpers
    {
		private static readonly TestUsers ACTIVE_TEST_USER = TestUsers.AndyWarhol;
        
	    public enum TestUsers
	    {
            AndyWarhol = 0
	    }

        public static String GetApiKey()
        {
            switch(ACTIVE_TEST_USER)
            {
                case TestUsers.AndyWarhol:
					return "XXXXXXXXXXXXXXXX";
                default:
                    return String.Empty;
            }
        }

        public static int GetDashboardVersion()
        {
            switch (ACTIVE_TEST_USER)
            {
                case TestUsers.AndyWarhol:
				return 99999999;
                default:
                    return -1;
            }
        }

        public static String GetGsmSenderId()
        {
            switch (ACTIVE_TEST_USER)
            {
                case TestUsers.AndyWarhol:
				return "XXXXXXXXXXXXX";
                default:
                    return String.Empty;
            }
        }
	}
}