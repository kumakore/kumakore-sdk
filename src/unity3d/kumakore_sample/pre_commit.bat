rd /s /q "$(ProjectDir)..\..\unity3d\kumakore_sample\Assets\com.kumakore\"
xcopy /y "$(ProjectDir)*.cs" "$(ProjectDir)..\..\unity3d\kumakore_sample\Assets\com.kumakore\"
xcopy /y "$(ProjectDir)fastJson\*.cs" "$(ProjectDir)..\..\unity3d\kumakore_sample\Assets\com.kumakore\fastJson\"


rd /s /q "$(ProjectDir)..\..\unity3d\kumakore_sample\Assets\com.kumakore.test\"
xcopy /y "$(ProjectDir)*.cs" "$(ProjectDir)..\..\unity3d\kumakore_sample\Assets\com.kumakore.test\"
xcopy /y "$(ProjectDir)..\..\..\external\NUnit\nunit.framework.dll" "$(ProjectDir)..\..\unity3d\kumakore_sample\Assets\Plugins\"