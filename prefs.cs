//Separate file for prefs just for organization alongside commands for managing said prefs (just for organization)
//When adding new stuff that utilizes prefs, just add the pref in this file

$PrefManagerEnabled = if(isFile("Add-Ons/System_ReturnToBlockland/server.cs") || isFile("Add-Ons/System_BlocklandGlass/server.cs"))

if($PrefManagerEnabled = 1)
{
	if(!$RTB::RTBR_ServerControl_Hook)
	{
		RTB_registerPref("Announce AFK","Status Mod v2","StatusModv2::AnnounceAFK","bool","Server_StatusModv2",1,0,0);
		RTB_registerPref("Announce Time (Seconds)","Status Mod v2","StatusModv2::LoopTime","int 60 1200","StatusModv2",300,0,0);
		RTB_registerPref("Change Shapenames When AFK?","Status Mod v2","StatusModv2::ChangeShapename","bool","Server_StatusModv2",1,0,0);
	}
}
else
{
	$StatusModv2::AnnounceAFK = 1;
	$StatusModv2::LoopTime = 300;
	$StatusModv2::ChangeShapename = 1;
}

function serverCmdAnnounceAFK(%client, %value)
{
	if(%client.isSuperAdmin)
	{
		//to be continued
