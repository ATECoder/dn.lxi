﻿using Android.App;
using Android.Runtime;

namespace cc.isr.LXI.IEEE488Client.Maui.Concept;

[Application]
public class MainApplication : MauiApplication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
	}

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}