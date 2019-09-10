## Documentation

### LiveChat Module for Sitefinity

The LiveChat Sitefinity integration allows you to seamlessly integrate LiveChat functionality into your Sitefinity website. Engage with your customers in real-time while they’re visiting your website so that you can solve problems and answer questions instantly. 

### Install module

1. You can install the module by either   
    a. Installing the nuget `Telerik.Sitefinity.LiveChat`, by running the below command  
        `Install-Package Telerik.Sitefinity.LiveChat`  
    b. Or Cloning the project to your machine, build the solution and copy `LiveChat.dll` from the bin folder to your Sitefinity Web App's bin folder. 

2. The module is self-installable thanks to the SitefinityModuleAttribute defined in the project's Assemplyinfo.cs file

### Edit WebSecurity
