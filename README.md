## Documentation

### LiveChat Module for Sitefinity

The LiveChat Sitefinity integration allows you to seamlessly integrate LiveChat functionality into your Sitefinity website. Engage with your customers in real-time while they’re visiting your website so that you can solve problems and answer questions instantly. 

### Sign Up for LiveChat
1. If you don't have an account for LiveChat, Please [Signup](https://www.livechatinc.com/?a=xjbRsOKZR&utm_source=progress.com)

### Install module

1. You can install the module by either   
    a. Installing the nuget `Telerik.Sitefinity.LiveChat`, by running the below command  
        `Install-Package Telerik.Sitefinity.LiveChat`  
    b. Or Cloning the project to your machine, build the solution and copy `LiveChat.dll` from the bin folder to your Sitefinity Web App's bin folder. 

2. The module is self-installable thanks to the SitefinityModuleAttribute defined in the project's Assemplyinfo.cs file

### Add LiveChat to Trusted Sources

1. For LiveChat to function properly, you need to add `*.livechatinc.com` to your trusted sources.  
2. To do that, Go to Settings -> Web security -> Trusted Sources and click on Edit.
3. Add `*.livechatinc.com` to Scripts, Styles, Fonts, Images, Child Sources, Connect sources. 
4. Click on Done to save the settings. 

### Enable LiveChat on Sitefinity

1. Go to Administration Menu -> Under Tools, Click on Enable LiveChat.  
<img src="https://raw.githubusercontent.com/saiteja09/Telerik.Sitefinity.LiveChat/master/Screenshots/1.png" height="500px"/>


2. Now you should see a screen like below. Click on `SignIn With LiveChat` button to sign in to your livechat account.
<img src="https://raw.githubusercontent.com/saiteja09/Telerik.Sitefinity.LiveChat/master/Screenshots/2.PNG" width="500px"/>


3. Fill in your LiveChat credentials on the pop up window.
<img src="https://raw.githubusercontent.com/saiteja09/Telerik.Sitefinity.LiveChat/master/Screenshots/3.PNG" height="500px"/>


4. Click on Allow, to enable LiveChat on Sitefinity. 
<img src="https://raw.githubusercontent.com/saiteja09/Telerik.Sitefinity.LiveChat/master/Screenshots/4.PNG" height="500px"/>

5. That's it, you have enabled LiveChat on Sitefinity. Go to your home page to see LiveChat in action.
<img src="https://raw.githubusercontent.com/saiteja09/Telerik.Sitefinity.LiveChat/master/Screenshots/6.PNG" width="500px"/>

### Enable LiveChat on Sitefinity - Manual

1. If you have a LicenseId from Livechat, you can manually enable LiveChat on Sitefinity.  
2. Go to Settings->Advanced->LiveChat and paste your LicenseID and Email. 
3. Click on Save Changes to enable LiveChat. If your LicenseID is valid, you should see LiveChat enabled on your website.

### Disable LiveChat on Sitefinity

1. Go to Administration Menu -> Under Tools, Click on Enable LiveChat. You just have to click on `Logout` button to disable LiveChat on Sitefinity. 

