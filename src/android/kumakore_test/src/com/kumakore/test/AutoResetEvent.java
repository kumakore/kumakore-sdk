package com.kumakore.test;

public class AutoResetEvent {
	private final Object _monitor = new Object();
	private volatile boolean _isOpen = false;

	public AutoResetEvent(boolean open) {
		_isOpen = open;
	}

	public boolean waitOne() {
		synchronized (_monitor) {
			while (!_isOpen) {
				try {
					_monitor.wait();
				} catch (InterruptedException e) {
					e.printStackTrace();
					_isOpen = false;
					return false;
				}
			}
			_isOpen = false;
			return true;
		}
	}

	public boolean waitOne(long timeout) {
		synchronized (_monitor) {
			long t = System.currentTimeMillis();
			while (!_isOpen) {
				try {
					_monitor.wait(timeout);
				} catch (InterruptedException e) {
					e.printStackTrace();
					_isOpen = false;
					return false;
				}
				// Check for timeout
				if (System.currentTimeMillis() - t >= timeout) {
					_isOpen = false;
					return false;
				}
			}
			_isOpen = false;
			return true;
		}
	}

	public void set() {
		synchronized (_monitor) {
			_isOpen = true;
			_monitor.notify();
		}
	}

	public void reset() {
		_isOpen = false;
	}
}