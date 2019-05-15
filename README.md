# Button Circle Control Plugin for Xamarin.Forms

Simple but elegant way of display circle buttons with an icon in your Xamarin.Forms projects. 

[![Build status](https://ci.appveyor.com/api/projects/status/1yyib3ysj80mas1w?svg=true)](https://ci.appveyor.com/project/wilsonvargas/buttoncircleplugin) [![NuGet](https://buildstats.info/nuget/Plugins.Forms.ButtonCircle)](https://www.nuget.org/packages/Plugins.Forms.ButtonCircle/) [![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.me/wilsondonations/5)

![image](https://raw.githubusercontent.com/wilsonvargas/ButtonCirclePlugin/master/images/screenshots/image.png)

#### Setup
* Available on NuGet: [Plugins.Forms.ButtonCircle](https://www.nuget.org/packages/Plugins.Forms.ButtonCircle/)
* Install into your PCL or .NET Standard project and Client projects.

### Android

In your Android project call:

```
ButtonCircleRenderer.Init();
```
<img src="https://raw.githubusercontent.com/wilsonvargas/ButtonCirclePlugin/master/images/screenshots/android.png" 
data-canonical-src="https://raw.githubusercontent.com/wilsonvargas/ButtonCirclePlugin/master/images/screenshots/android.png"
 width="450" height="480" />

### iOS

In your iOS project call:

```
ButtonCircleRenderer.Init();
```

And add this key in your Info.plist

```
<key>UIAppFonts</key>
    <array>
      <string>materialicons.ttf</string>
      <string>fontawesome.ttf</string>
    </array>
```

<img src="https://raw.githubusercontent.com/wilsonvargas/ButtonCirclePlugin/master/images/screenshots/ios.png" 
data-canonical-src="https://raw.githubusercontent.com/wilsonvargas/ButtonCirclePlugin/master/images/screenshots/ios.png"
 width="480" height="480" />


### UWP

In your UWP project add materialicons.ttf and fontawesome.ttf files to:

```
Assets/Fonts
```
You can download this file here:

[Material Design icons](https://github.com/wilsonvargas/ButtonCirclePlugin/blob/master/src/ButtonCircle/ButtonCircle.FormsPlugin.UWP/Assets/Fonts/materialicons.ttf)

[FontAwesome](https://github.com/wilsonvargas/ButtonCirclePlugin/blob/master/src/ButtonCircle/ButtonCircle.FormsPlugin.UWP/Assets/Fonts/fontawesome.ttf)

[Ionic](https://github.com/wilsonvargas/ButtonCirclePlugin/blob/master/src/ButtonCircle/ButtonCircle.FormsPlugin.UWP/Assets/Fonts/ionicons.ttf)

Also call Init method:

```
ButtonCircleRenderer.Init();
```
You must do this AFTER you call Xamarin.Forms.Init();

> **Note:** On UWP, the button's fill color on hover will be a lighter shade of the background color set on the CircleButton, unless it is transparent (which will be the assumed default if no BackgroundColor is explicitly set) in which case the BorderColor will be used.

<img src="https://raw.githubusercontent.com/wilsonvargas/ButtonCirclePlugin/master/images/screenshots/windows.png" 
data-canonical-src="https://raw.githubusercontent.com/wilsonvargas/ButtonCirclePlugin/master/images/screenshots/windows.png"
 width="310" height="510" />


**Platform Support**

|Platform|Supported|Version|
| ------------------- | :-----------: | :------------------: |
|Xamarin.iOS|Yes|iOS 7+|
|Xamarin.Android|Yes|API 14+|
|Windows 10 UWP|Yes|Build 105086+
|Xamarin.Mac|No||

#### List of icons
You can see name of icons
for FontAwesome [here](https://github.com/wilsonvargas/ButtonCirclePlugin/blob/master/src/ButtonCircle/ButtonCircle.FormsPlugin.Abstractions/FontAwesome/FontAwesomeCollection.cs)
and for Material design icon [here](https://github.com/wilsonvargas/ButtonCirclePlugin/blob/master/src/ButtonCircle/ButtonCircle.FormsPlugin.Abstractions/Material/MaterialCollection.cs)

#### Usage
Instead of using an Button simply use a CircleButton instead!

You **MUST** set the width & height requests to the same value. Here is a sample:
```
new ButtonImage
{
  BorderColor = Color.Black,
  BorderThickness = 5,
  HeightRequest = 150,
  WidthRequest = 150,
  HorizontalOptions = LayoutOptions.Center,
  FontIcon = Fonts.Material
  Icon = "md-add"
}
```

**XAML:**

First add the xmlns namespace:
```xml
xmlns:local="clr-namespace:ButtonCircle.FormsPlugin.Abstractions;assembly=ButtonCircle.FormsPlugin.Abstractions"
```

Then add the xaml:

```xml
<local:CircleButton 
        FontIcon="Material"
        Icon="md-directions-bike" 
        FontSize="30" TextColor="Black" 
        HeightRequest="70" WidthRequest="70" 
        BorderThickness="5" BorderColor="Black" 
        BackgroundColor="#DCDCDC">
</local:CircleButton>
```

> If you see the replacement character (�) appear instead of the desired icon, make sure that you have followed the setup instructions above and that you have supplied the correct text key for the "Icon" property.

#### License
Licensed under MIT, see license file
