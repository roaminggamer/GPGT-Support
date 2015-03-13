//------------------------------------------------------
// Copyright 2000-2006, GarageGames.com, Inc.
// Written by Ed Maurina, Hall Of Worlds, LLC
//------------------------------------------------------
echo("\c3--------- Loading Game GUIs HUDS 0 - Counters ---------");

//--------------------------------------------------------------------------
// ifcsHUDs0.cs
//--------------------------------------------------------------------------
//--------------------------------------------------------------------------

//--------------------------------------------------------------------------
// Profiles
//--------------------------------------------------------------------------

//--------------------------------------------------------------------------
// Scripts
//--------------------------------------------------------------------------


function guiControl::getCounterValue( %this ) {
	return %this.currentCount;
}


function guiControl::setCounterValue( %this , %newCount ) {

	// Check to be sure that the required fields have been set:
	//
	// numDigits     - Number of digits in this counter
	// digitTileName - Prefix for tile names used in this counter (i.e. names of the controls)
	// digitPath     - Path to tiles used in this counter
	//
	if( "" $= %this.numDigits )  return false;
	if( "" $= %this.digitTileName )  return false;
	if( "" $= %this.digitPath )  return false;
	
	// Store the currentCount
	%this.currentCount = %newCount;
	
	%newCountDigits = strlen( %newCount );
	
	if ( %newCountDigits > %this.numDigits ) { // Overflow
		for( %count = 0 ; %count < %this.numDigits ; %count++ ) {
			%tmpDigit[%count] = 9;
		}
	} else {

		// Pad with zeros so our 'newCount' string is exactly %this.numDigits wide

		%tmpNewCount = "";

		for( %count = %this.numDigits - %newCountDigits ; %count > 0 ; %count-- ) {
			%tmpNewCount = %tmpNewCount @ "0";
		}

		%tmpNewCount = %tmpNewCount @ %NewCount; 

		//echo("%tmpNewCount == " @ %tmpNewCount );

		// Get digits in reverse order and store them aside
		for( %count = 0 ; %count < %this.numDigits ; %count++ ) {
			%tmpDigit[%count] = getSubStr( %tmpNewCount , %this.numDigits - 1 - %count , 1 );

			//echo(%count @ " == " @ getSubStr( %tmpNewCount , %this.numDigits - 1 - %count , 1 ));
		}

	}

	// Change the bitmaps for each digit in the display
	for( %count = 0 ; %count < %this.numDigits ; %count++ ) {
		(%this.digitTileName  @ %count).setBitmap( %this.digitBitmap[%tmpDigit[%count]] );
		//echo((%this.digitTileName  @ %count) @ " == " @ %tmpDigit[%count]);
	}

	return true;
}

function guiControl::initializeBitmaps( %this ) {
	if( "" $= %this.digitPath )  return false;

	for( %count = 0; %count < 10 ; %count++ ) {
		%this.digitBitmap[%count] = expandFilename( %this.digitPath @ %count );

		//echo(%count @ " == " @ %this.digitBitmap[%count]);

	}
}



//
// skinnyFrame6DigitCounter
//
function skinnyFrame6DigitCounter::onAdd( %this ) {
	%this.initializeBitmaps();
	%this.maxValue = 999999;
	
}


//
// FatFrame6DigitCounter
//
function FatFrame6DigitCounter::onAdd( %this ) {
	%this.initializeBitmaps();
	%this.maxValue = 999999;
}

//
// arcade5DigitCounter
//
function arcade5DigitCounter::onAdd( %this ) {
	%this.initializeBitmaps();
	%this.maxValue = 99999;
}

//--------------------------------------------------------------------------
// Load Interface Definition
//--------------------------------------------------------------------------
exec("./ifcsHUDs0.gui");


/// STOP  STOP  STOP  STOP  STOP  STOP  STOP  STOP  STOP  STOP  STOP  STOP  STOP  
//
// All the following scripts are used to demonstrate the various HUDS in this sample.
// You do not need these specific version of the onWake(), onSleep(), etc. methods
// for your game(s).
//
/// STOP  STOP  STOP  STOP  STOP  STOP  STOP  STOP  STOP  STOP  STOP  STOP  STOP  



//
// skinnyFrame6DigitCounter
//
function skinnyFrame6DigitCounter::onWake( %this ) {
	%this.lastSchedule = %this.schedule( 0, advanceCounter, 1 );
}

function skinnyFrame6DigitCounter::onSleep( %this ) {
	cancel(%this.lastSchedule);
}


//
// FatFrame6DigitCounter
//
function FatFrame6DigitCounter::onWake( %this ) {
	%this.lastSchedule = %this.schedule( 0, advanceCounter, 12357 );
}

function FatFrame6DigitCounter::onSleep( %this ) {
	cancel(%this.lastSchedule);
}

//
// arcade5DigitCounter
//
function arcade5DigitCounter::onWake( %this ) {
	%this.lastSchedule = %this.schedule( 0, advanceCounter, 200 );
}

function arcade5DigitCounter::onSleep( %this ) {
	cancel(%this.lastSchedule);
}




function guiControl::advanceCounter( %this , %increment ) {
	if( %this.maxValue < %this.getCounterValue() ) { // Roll counter over

		%this.setCounterValue( %this.maxValue - %this.getCounterValue()); 

	} else { // Advance the counter

		%this.setCounterValue( %this.getCounterValue() + %increment );
	}
	
	%this.lastSchedule = %this.schedule( 100, advanceCounter, getRandom( 10, 1000 ) );
}


