# VPNSetup

This windows application needs admin privileges to run. It has following functonalities:

Usage:

Run the .exe file with admin privileges. 
The welcome screen contains two functions to call.
1. #Run initial setup - Performs initial setup required for the hosted network.
 -Starts the necessary services (RemoteAccess and SharedAccess).
 -Check ExpressVPN Adapter (Enable if not)
 -Check for internet connection.
 -Check if machine supports Microsoft Virtual Hosted Network.
 
2. #Configure Sharing - Opens another form to stop/start/view hosted network.

Configure Sharing:
1. Connect your hosted network - This option will start a hosted network for you and add to the express VPN adapter.
2. Stop - Stop your hosted network.
3. Create new hosted netwrok-  Creates a new hosted hosted network for you. It stops the previous hosted network.
4. Change password -  Changes hosted network password.
5. Show password - See the current network's password.

3. #Run troubleshooting - Resets DNS and flushes IP.

The software takes care of the above mentioned steps. Then you need to start Express VPN by selecting the preffered location.
The vpn is shared now. You can connect your TV and phone and other computer to the hosted network and enjoy the benefits of express vpn over the shared network.


Sample Screens:

![alt text](https://github.com/Soumya117/HostExpressVPN/blob/master/Welcome.JPG) <br /><br />

![alt text](https://github.com/Soumya117/HostExpressVPN/blob/master/InitialSetup.JPG) <br /><br />

![alt text](https://github.com/Soumya117/HostExpressVPN/blob/master/configureNetworkJPG.JPG) <br /><br />
