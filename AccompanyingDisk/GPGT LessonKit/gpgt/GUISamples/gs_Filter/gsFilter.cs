//------------------------------------------------------
// Copyright 2000-2006, GarageGames.com, Inc.
// Written by Ed Maurina, Hall Of Worlds, LLC
//------------------------------------------------------
echo("\c3--------- Loading GuiFilterCtrl Samples ---------");
//--------------------------------------------------------------------------
// gsFilter.cs
//--------------------------------------------------------------------------
// 
// In this file, we demonstrate, the following GuiFilterCtrl features:
//
// 1. GuiFilterCtrl as input control
// 2. GuiFilterCtrl as output control
// 3. Setting new controlPoints value directly.
// 4. Setting knot values to identity

//--------------------------------------------------------------------------
// Profiles
//--------------------------------------------------------------------------

//--------------------------------------------------------------------------
// Scripts
//--------------------------------------------------------------------------

// 1. GuiFilterCtrl as input control
// 2. GuiFilterCtrl as output control
//

function TestFilter0::onWake( %this ) {
	%this.lastSchedule = %this.schedule( 32, shareValues );
}


function TestFilter0::onSleep( %this ) {
	cancel( %this.lastSchedule );
}


function TestFilter0::shareValues( %this ) {
	TestFilter1.setValue( TestFilter0.getValue() );
	%this.lastSchedule = %this.schedule( 32, shareValues );
}


//
// 3. Setting new controlPoints value directly.
//
// In order to show that you can in fact change the number of control
// points of a filter real-time, I've added four radio buttons that
// use the following methods to change the number of knots in 
// TestFilter0.  Note that when this is updated, the next value
// copy from TestFilter0 to TestFilter1 updates the number of knots
// in TestFilter1.  i.e. By adding more knots values to a setValue()
// call, you can adjust the number of knots in a filter.

function numKnots::onWake( %this ) {
	// Be sure we start with radio button 5 clicked
	if (5 == %this.text) %this.performClick();
}

function numKnots::updateKnots( %this ) {
	TestFilter0.controlPoints = %this.text;
}


//
// 4. Setting knot values to identity
//
// The only code for this is in the command for button resetFilterButton:
// command = "TestFilter0.identity();";

//
// STOP STOP STOP STOP STOP STOP STOP STOP STOP 
//
// The following methods are just used provide an animation
// and a little hint.  i.e. Inputs travel left to right.
// This is not part of the sample and all FilterEnergyStream::
// methods can be ignored, unless you are just curious.
// 

function FilterEnergyStream::onWake( %this ) {
	%this.lastSchedule = %this.schedule( 0, scrollMe );
}

function FilterEnergyStream::onSleep( %this ) {
	cancel( %this.lastSchedule );
}

function FilterEnergyStream::scrollMe( %this ) {
	%this.curX -= 16;

	if( %this.curX <= 0) {
		%this.curX = 128;
		%this.curY = 128;
	}

	%this.setValue( %this.curX ,  -8);
	
	%this.lastSchedule = %this.schedule( 100, scrollMe );
}

//--------------------------------------------------------------------------
// Load Interface Definition
//--------------------------------------------------------------------------
exec("./gsFilter.gui");

