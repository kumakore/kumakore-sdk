using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

namespace com.kumakore.plugin.util
{
    [DebuggerDisplay("StatusCode = {getCode()}, StatusMessage = {getStatusMessage()}")]
	public class CommandBase<T> where T : struct, IComparable, IConvertible, IFormattable
    {
		private static readonly String TAG = typeof(CommandBase<T>).Name;

		private int _executionCount;
		private List<T> _statusCodeHistory = new List<T>();
		private T _statusCode;
		private readonly T _defaultCode;
        private String _statusMessage = String.Empty;
		private IInvokable _dispatcher;

		private Enum statusCodeEnum {
			get {
				return KumakoreUtil.convertToListStr<T>(_statusCode);
			}
		}

		private Enum defaultCodeEnum {
			get {
				return KumakoreUtil.convertToListStr<T>(_defaultCode);
			}
		}

		static CommandBase()
		{
			if (!typeof(T).IsEnum) {
				string error = typeof(T).Name + " is not a enum type";
				Kumakore.LOGE(TAG, error);
				throw new InvalidOperationException();
			}
		}

		public CommandBase(T code, IInvokable dispatcher = null)
        {
			_dispatcher = dispatcher;
			_statusCode = code;
			_defaultCode = code;
        }
	
		public bool hasDispatcher() {
			return _dispatcher != null;
		}

		public IInvokable getDispatcher() {
			return _dispatcher;
		}

		public int getExecutions() 
		{
			return _executionCount;
		}

        public T getCode() 
		{
			return _statusCode;
        }

		public bool hasCode(T code) {
			return hasCode (code, true);
		}

		public bool hasCode(T code, bool includeHistory) {

			Enum codeEnum = KumakoreUtil.convertToListStr<T> (code);

			if (codeEnum.Equals(statusCodeEnum))
				return true;
			else {
				foreach (T historicCode in _statusCodeHistory) {
					Enum historicCodeEnum = KumakoreUtil.convertToListStr<T> (historicCode);
					if (code.Equals(historicCodeEnum))
						return true;
				}
			}
			return false;
		}

		public void setCode(T code)
		{
			setCode(code, false);
		}

		public void setCode(T code, bool force)
        {
			if (statusCodeEnum.Equals(defaultCodeEnum) || force) {
				// keep a history if forced
				if (force)
					_statusCodeHistory.Add(_statusCode);

				_statusCode = code;
			}
		}

		public void setStatusMessage(String message)
        {
            _statusMessage = message;
        }

        public virtual String getStatusMessage()
        {
			return String.IsNullOrEmpty(_statusMessage) ? _statusCode.ToString() : _statusMessage;
		}

        protected virtual T onSync()
        {
			setCode (onExecute ());
			return getCode();
        }

        protected virtual void onAsync()
        {
			setCode(onExecute ());
        }

		protected virtual T onExecute()
		{
			return _statusCode;
		}

		protected virtual void onExecuted()
		{
			_executionCount++;
		}
		
		protected void onReset() {
			_statusCode = _defaultCode;
			_statusCodeHistory.Clear ();
		}

		/// execute this KumakoreAction synchronously
		public T sync()
		{
			return sync (false);
		}

		/// <summary>
		/// execute this KumakoreAction synchronously
		/// </summary>
		/// <param name="reset">clear/reset this action state</param>
		public T sync(bool reset)
		{
			if (reset)
				onReset ();
            
			T statusCode = onSync();

			onExecuted ();

			return statusCode;
        }

		/// execute this KumakoreAction asynchronously
		public void async()
		{
			async (false);
		}

		/// <summary>
		/// execute this KumakoreAction asynchronously
		/// </summary>
		/// <param name="reset">clear/reset this action state</param>
		public void async(bool reset)
        {
			if (reset)
				onReset ();

			if (hasDispatcher ()) {
					getDispatcher ().invoke (() => {
					onAsync (); onExecuted ();
				});
			} else {
				onAsync ();
				onExecuted ();
			}
        }
	}
}

