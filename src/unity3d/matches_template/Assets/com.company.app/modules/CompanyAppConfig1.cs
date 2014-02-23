using UnityEngine;
using System.Collections;
using com.kumakore.utils;

public class CompanyAppConfig1 : Config {

//	public CompanyAppConfig1(Injector injector) : base(injector) {
//
//	}

//	public override void Load()
//	{
//		//Bind<IAppService> ().To<AppService> ().InSingletonScope ();
//	}

	public override void OnLoad ()
	{
		injector.Bind<IAppService>().ToSingleton<AppService> ();
	}

	public override void OnUnload ()
	{

	}
}
