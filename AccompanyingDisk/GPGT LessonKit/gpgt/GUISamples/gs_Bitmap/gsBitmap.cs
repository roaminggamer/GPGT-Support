//------------------------------------------------------
// Copyright 2000-2006, GarageGames.com, Inc.
// Written by Ed Maurina, Hall Of Worlds, LLC
//------------------------------------------------------
echo("\c3--------- Loading GuiBitmapButtonCtrl Samples ---------");

//--------------------------------------------------------------------------
// gsBitmapButton.cs
//--------------------------------------------------------------------------
//--------------------------------------------------------------------------
// 
// In this file, we demonstrate, the following GuiBitmapButtonCtrl features:
//
// 1. Wrapping
// 2. setValue() 
// 3. setBitmap()

//--------------------------------------------------------------------------
// Profiles
//--------------------------------------------------------------------------

//--------------------------------------------------------------------------
// Scripts
//--------------------------------------------------------------------------

// *************************************************************************
//  Wrapping Sample 
// *************************************************************************
//
// No code required for this sample.  See GUI definition.
//


// *************************************************************************
//  Scrolling Sample - setValue() 
// *************************************************************************
// This sample scrolls a bitmap diagonally over time.  This is accomplished
// by using the onWake() callback to start a scrolling action which then
// re-schedules itself continually.
//
// We want to save processing power, so when the control goes to sleep it
// should stop scrolling.  This can be accomplished by cancelling the
// last schedule in onSleep().  
// 
// There is no need to cancel in onRemove() because the schedule is 
// tied to the 'existence' of this control and will automatically be
// cancelled when the control object is destroyed.
//
function TestBitmap2::onWake( %this ) 
{
   %this.scrollMe();
}

function TestBitmap2::onSleep( %this ) 
{
   cancel( %theConrol.lastSchedule );
}

function TestBitmap2::scrollMe( %this ) 
{
   // Prevent overlapping scrolls
   if( isEventPending(%this.lastSchedule) )
      return;

	%this.curX += 2;
	%this.curY += 2;

	if( %this.curX >= 256) 
   {
		%this.curX = 0;
		%this.curY = 0;
	}

	%this.setValue( %this.curX , %this.curY );

   %this.lastSchedule = %this.schedule( 32 , scrollMe );
}


// *************************************************************************
//  setBimap() Sample 
// *************************************************************************
// This sample changes the bitmap used by the control over time. This is 
// accomplished by using the various callbacks to first initialize and
// subsequently to change the bitmaps.
//
// The scheduling behavior for this sample is quite similar to the prior 
// scrolling sample.
//

function TestBitmap3::onAdd( %this ) 
{
	//
	// Here is a good example of the expandFilename() function.
	// We are storing the paths of the files we want to use later, in
	// our bitmapGUI as an array of dynamic fields.
	//
   %tmp = 0;
	%this.Image[%tmp]   = expandFilename("./GGRTSPackImages/rtsKitLogo");
	%this.Image[%tmp++] = expandFilename("./GGRTSPackImages/rtsKit1");
	%this.Image[%tmp++] = expandFilename("./GGRTSPackImages/rtsKit2");
	%this.Image[%tmp++] = expandFilename("./GGRTSPackImages/rtsKit3");
	%this.Image[%tmp++] = expandFilename("./GGRTSPackImages/rtsKit4");
	%this.Image[%tmp++] = expandFilename("./GGRTSPackImages/rtsKit5");
	%this.Image[%tmp++] = expandFilename("./GGRTSPackImages/rtsKit6");
	%this.Image[%tmp++] = expandFilename("./GGRTSPackImages/rtsKit7");
	%this.Image[%tmp++] = expandFilename("./GGRTSPackImages/rtsKit8");
	%this.Image[%tmp++] = expandFilename("./GGRTSPackImages/rtsKit9");
	%this.Image[%tmp++] = expandFilename("./GGRTSPackImages/rtsKit10");

	%this.currentImage = 0;
	%this.totalImages  = %tmp++;
}

function TestBitmap3::onWake( %this ) 
{
	// Always start on first frame
	%this.currentImage = 0;
	%this.setBitmap(%this.Image[%this.currentImage]);

   %this.lastSchedule = %this.schedule( 2000 , flipMe );
}

function TestBitmap3::onSleep( %this ) 
{
	cancel( %theConrol.lastSchedule );
}

function TestBitmap3::flipMe( %this ) 
{
   // Prevent overlapping scrolls
   if( isEventPending(%this.lastSchedule) )
      return;

   %this.currentImage++;

	if(%this.currentImage >= %this.totalImages) 
   {
		%this.currentImage = 0;
	}


	%this.setBitmap(%this.Image[%this.currentImage]);

   //echo("Loading new image ==> ",%this.Image[%this.currentImage]);

   %this.lastSchedule = %this.schedule( 2000 , flipMe );
}


//--------------------------------------------------------------------------
// Load Interface Definition
//---------------------------------------------------------------
exec("./gsBitmap.gui");

