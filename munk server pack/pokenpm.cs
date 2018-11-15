//./pokenpm.cs

package pokenpm
{
        function servercmdpoke(%client,%target)
        {
                if($Pokenpmtoggle == 1)
                {                
                        if(%client.sentpoke == 1)
                        {
                                commandToClient(%client,'MessageBoxOK',"No Spamming, Please!","Please do not spam!!!!");
                        }
                        else
                        {
                                %targetclient = findClientByName(%target);
                        
                                if(!isObject(findclientbyname(%target)))
                                {
                                        messageclient(%client,'',"\c6Invalid Player, Sorry.");
                                }
                                else
                                {        
                                        commandToClient(%targetclient,'MessageBoxOK',"Poke!","" @ %client.name @ " has poked you! <br>Say /pm " @ %client.name @ " [Message] To respond,<br> or /poke " @ %client.name @ " to poke them back!");
                                        messageclient(%targetclient,'',"\c3" @ %client.name @ "\c6 has poked you!");
                                        messageclient(%client,'',"\c6You have poked \c3" @ %targetclient.name @ "\c6!");
										%client.sentPoke = 1;
                                        schedule(10000, 0, sentpokeoff, %client);
                                }
                        }
                }
                else
                {
                        messageclient(%client,'',"\c6Poke and Private Messaging is turned off.");
                }
        }

        function serverCmdpm(%client,%target,%c1,%c2,%c3,%c4,%c5,%c6,%c7,%c8,%c9,%c10,%c11,%c12,%c13,%c14,%c16,%c17,%c18,%c19,%c20)
        {
                if($Pokenpmtoggle == 1)
                {        
                        %targetclient = findClientByName(%target);
                
                                for(%a = 1; %a < 21; %a++)
                                {
                                        if(%c[%a] !$= "")
                                        {
                                                %pm = %pm SPC %c[%a];
                                        }
                                }
                                
                        if(!isObject(findClientByName(%target)))
                        {
                                messageclient(%client,'',"\c6Invalid Player, Sorry.");
                        }
                        else
                        {
                                if(%client.sentpoke == 1)
                                {
                                        commandToClient(%client,'MessageBoxOK',"No Spamming, Please!","Please do not spam!!!!");
                                }
                                else
                                {
                                        %pm = stripMLControlChars(trim(%pm));
                                        commandToClient(%targetclient,'MessageBoxOK',"From: " @ %client.name,"" @ %pm);
                                        messageclient(%client,'',"\c2" @ %pm @ "\c6: Was sent to \c3" @ %targetclient.name @ "\c6");
                                        messageclient(%targetclient,'',"\c6From: \c3" @ %client.name @ "\c6 Message:\c2 " @ %pm @ "\c6.");
										%client.sentPoke = 1;
                                        schedule(10000, 0, sentpokeoff, %client);
                                }
                        }
                }
                else
                {
                        messageclient(%client,'',"\c6Poke and Private Messaging is turned off.");
                }
        }
        function sentpokeoff(%client)
        {
                %client.sentpoke = 0;
        }
};
activatePackage(pokenpm);

// +-----------------------------------------------------------
//  RTB Prefrences Stuff
// +-----------------------------------------------------------
if(isFile("Add-Ons/System_ReturnToBlockland/server.cs"))
{
   if(!$RTB::RTBR_ServerControl_Hook)
      exec("Add-Ons/System_ReturnToBlockland/RTBR_ServerControl_Hook.cs");
   RTB_registerPref("Poke and PM Toggle","Munk's Server Pack","Pokenpmtoggle","bool","System_Munk's Server Pack",1,0,0);
}
else
{
        //Change these prefs if you don't have Return to Blockland.
   $Pokenpmtoggle = 1;
}