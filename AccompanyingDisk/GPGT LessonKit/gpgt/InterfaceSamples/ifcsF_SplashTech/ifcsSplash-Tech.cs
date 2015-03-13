//------------------------------------------------------
// Copyright 2000-2006, GarageGames.com, Inc.
// Written by Ed Maurina, Hall Of Worlds, LLC
//------------------------------------------------------
echo("\c3--------- Loading Tech Splash Screen  ---------");

//--------------------------------------------------------------------------
// ifcsSplashTech.cs
//--------------------------------------------------------------------------
//--------------------------------------------------------------------------

//--------------------------------------------------------------------------
// Profiles
//--------------------------------------------------------------------------

//--------------------------------------------------------------------------
// Scripts
//--------------------------------------------------------------------------
function ifcsSplashTechFadeinBitmap::onAdd( %this ) {
	%this.done = false;
}

function ifcsSplashTechFadeinBitmap::onRemove( %this ) {
}


function ifcsSplashTechFadeinBitmap::onWake( %this ) {
	%this.done = false;
	
	%this.lastSchedule = %this.schedule( 100, checkIsDone );
}

function ifcsSplashTechFadeinBitmap::onSleep( %this ) {
	cancel( %this.lastSchedule );
}

function ifcsSplashTechFadeinBitmap::checkIsDone( %this ) {
	if( %this.done ) {
		Canvas.setContent(ifcsMainMenuTech);
		return;
	}
	%this.lastSchedule = %this.schedule( 100, checkIsDone );
}

function ifcsSplashTechFadeinBitmap::click( %this ) {
	%this.done  = true;
	cancel( %this.lastSchedule );
	Canvas.setContent(ifcsMainMenuTech);
}



//--------------------------------------------------------------------------
// Load Interface Definition
//--------------------------------------------------------------------------
exec("./ifcsSplashTech.gui");



