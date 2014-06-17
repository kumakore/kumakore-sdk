using System;

namespace com.kumakore.plugin.util
{
	public class Command<T, U> : CommandBase<U> where U : struct, IComparable, IConvertible, IFormattable
		where T : class
	{
		#pragma warning disable 0414
		private static readonly String TAG = typeof(Command<T,U>).Name;
		#pragma warning restore 0414

		private T _target;

		static Command()
		{
			if (!typeof(T).IsSubclassOf(typeof(Delegate))) {
				string error = typeof(T).Name + " is not a delegate type";
				Kumakore.LOGE(TAG, error);
				throw new InvalidOperationException();
			}
        }

		public Command (U code, IInvokable dispatcher = null) 
			: base(code, dispatcher)
		{

		}

		/// <summary>
		/// execute this KumakoreAction synchronously and callback to the provided interface
		/// </summary>
		/// <param name="target">Target.</param>
		public U sync (T target)
		{
			return sync (target, false);
		}

		/// <summary>
		/// execute this KumakoreAction synchronously and callback to the provided interface
		/// </summary>
		/// <param name="target">Target.</param>
		/// <param name="reset">clear/reset this action state</param>
		public U sync(T target, bool reset)
		{
			if (reset)
				onReset ();

            _target = target;

			U statusCode = onSync();
			
			onExecuted ();
			
			return statusCode;
        }

		/// <summary>
		/// execute this KumakoreAction asynchronously and callback to the provided interface
		/// </summary>
		/// <param name="target">Target.</param>
		public void async (T target)
		{
			//HACK:chbfiv keywork conflict for async w/ MSBuilder; use this.async
			this.async (target, false);
		}

		/// <summary>
		/// e				xecute this KumakoreAction asynchronously and callback to the provided interface
		/// </summary>
		/// <param name="target">Target.</param>
		/// <param name="reset">clear/reset this action state</param>
		public void async (T target, bool reset)
		{
			if (reset)
				onReset ();

			_target = target;
			
			if (hasDispatcher ()) {
				getDispatcher ().invoke (() => {
					onAsync (); onExecuted ();
				});
			} else {
				onAsync ();
				onExecuted ();
			}
		}
	
		protected bool hasTarget ()
		{
			return _target != null;
		}
	
		protected T getTarget ()
		{
			return _target;
		}
	}
}

