using System;

namespace com.kumakore.plugin.util
{
		public static class NamingHelper
		{
				public static string generateUserName (string tag = "kumakore", int randomLength = 5)
				{
						string guid = Guid.NewGuid ().ToString ();
						string username = tag;
						return username + guid.Substring (0, randomLength);
				}
		
				public static string generatePassword (int randomLength = 10, string prefix = "", string postfix = "")
				{
						string guid = Guid.NewGuid ().ToString ();
						string password = prefix + guid.Substring (0, randomLength) + postfix;
						return password;
				}
		}
}

