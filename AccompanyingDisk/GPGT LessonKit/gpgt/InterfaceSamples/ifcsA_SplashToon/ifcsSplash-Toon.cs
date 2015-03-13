//------------------------------------------------------
// Copyright 2000-2006, GarageGames.com, Inc.
// Written by Ed Maurina, Hall Of Worlds, LLC
//------------------------------------------------------
echo("\c3--------- Loading Toon Splash Screen  ---------");

//--------------------------------------------------------------------------
// ifcsSplashToon.cs
//--------------------------------------------------------------------------
//--------------------------------------------------------------------------

//--------------------------------------------------------------------------
// Profiles
//--------------------------------------------------------------------------
new AudioProfile(AudioStartupToon)
{
   filename = "./childrenLaugh.ogg";
   description = "AudioGui";
   preload = true;
};


//--------------------------------------------------------------------------
// Scripts
//--------------------------------------------------------------------------
function ifcsSplashToonFadeinBitmap::onAdd( %this ) {
	%this.done = false;
}

function ifcsSplashToonFadeinBitmap::onRemove( %this ) {
}


function ifcsSplashToonFadeinBitmap::onWake( %this ) {
	%this.done = false;
	
	%this.lastSchedule = %this.schedule( 100, checkIsDone );

	alxPlay(AudioStartupToon);
}

function ifcsSplashToonFadeinBitmap::onSleep( %this ) {
	cancel( %this.lastSchedule );
}

function ifcsSplashToonFadeinBitmap::checkIsDone( %this ) {
	if( %this.done ) {
		Canvas.setContent(ifcsMainMenuToon);
		return;
	}
	%this.lastSchedule = %this.schedule( 100, checkIsDone );
}

function ifcsSplashToonFadeinBitmap::click( %this ) {
	%this.done  = true;
	cancel( %this.lastSchedule );
	Canvas.setContent(ifcsMainMenuToon);
}

//--------------------------------------------------------------------------
// Load Interface Definition
//--------------------------------------------------------------------------
exec("./ifcsSplashToon.gui");



