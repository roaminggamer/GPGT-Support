//------------------------------------------------------
// Copyright 2000-2006, GarageGames.com, Inc.
// Written by Ed Maurina, Hall Of Worlds, LLC
//------------------------------------------------------
echo("\c3--------- Loading GuiMessageVectorCtrl Samples ---------");
//--------------------------------------------------------------------------
// gsMessageVector.cs
//--------------------------------------------------------------------------
//--------------------------------------------------------------------------
// 
// In this file, we demonstrate, the following GuiMessageVectorCtrl features:
//
// 1. Attach and Detach of MessageVector classes to GuiMessageVectorCtrl controls.
// 2. Adding data to MessageVector
// 3. Clearing MessageVector
// 4. Ability to attach MessageVector to multiple GuiMessageVectorCtrl controls.
//
//
// When you run this example, you will see a screen with two GuiMessageVectorCtrl
// controls, some arrows, buttons, and a text entry field.
// 
// Try typing some data into the text entry field and pressing ENTER.  This will
// put the data you typed in into the current MessageVector.
//
// There are two images on the screen (with green lines threw them).  These are
// representations of the two MessageVectors that we have created (see scripts below).
// There is a little black arrow with a button next to it that says 
// '<-- Select Message Vector'.  Clicking the button will change the arrow and 
// (behind the scenes), attach our text edit field to either TestMessageVector0, 1, or
// both of them.  The left image represents TestMessageVector0 and the right represents
// TestMessageVector1
// 
// At the top of the screen, there are two windows containing GuiScrollCtrl's, which
// each contain a GuiMessageVectorCtrl.  The left one is TestMessageVectorCtrl0, and
// the right one is TestMessageVectorCtrl1.
// 
// Below the above windows is another arrow graphic and a button labelled 
// '<-- Select Attach Mode'.  Pressing the button will change the arrow connectivity,
// and (again behind the scenes) connect the Message Vectors to the GuiMessageVectorCtrl
// controls in any of six differnent ways 0 to 0, 0 to 1, 1 to 0, 1 to 1, 0 to 0 & 1, or
// 1 to 0 & 1.  At no time can two MessageVector classes be attached to the same
// GuiMessageVectorCtrl.
//
// Don't be discouraged if all this is NOT clear at once.  This is a rather more
// involved control and the example reflects this.  Play with this for a bit and 
// it should become clear.
// 

//--------------------------------------------------------------------------
// Profiles
//--------------------------------------------------------------------------

//--------------------------------------------------------------------------
// Scripts
//--------------------------------------------------------------------------

function gsMessageVector::onAdd( %this ) {
	//
	// Find the graphics we'll be using later.  This is done here
	// as it allows us to modify this later without goofing with the 
	// GUI definition itself.
	//
	%this.textAttach[0] = expandFilename("./TextToMessageVector0");
	%this.textAttach[1] = expandFilename("./TextToMessageVector1");
	%this.textAttach[2] = expandFilename("./TextToMessageVector01");

	%this.vectorAttach[0] = expandFilename("./Vector0ToVector0");
	%this.vectorAttach[1] = expandFilename("./Vector0ToVector1");
	%this.vectorAttach[2] = expandFilename("./Vector0ToVector01");
	%this.vectorAttach[3] = expandFilename("./Vector1ToVector0");
	%this.vectorAttach[4] = expandFilename("./Vector1ToVector1");
	%this.vectorAttach[5] = expandFilename("./Vector1ToVector01");
}

function gsMessageVector::onWake( %this ) {
	//
	// For this example, we always want to start with a fresh set of
	// Message Vectors.
	//
	if( isObject(%this.MessageVector0 ) ) %this.MessageVector0.delete();
	if( isObject(%this.MessageVector1 ) ) %this.MessageVector1.delete();

	%this.MessageVector0 = new MessageVector(TestMessageVector0);
	%this.MessageVector1 = new MessageVector(TestMessageVector1);

	//
	// Start with a default attachment of:
	//
	// - Text connected to TestMessageVector0
	// - TestMessageVector0 attached to TestMessageVectorCtrl0
	//
	%this.setTextAttachMode( 0 );
	%this.setVectorAttachMode( 0 );
}

function gsMessageVector::onSleep( %this ) {
	//
	// We don't really need to do this, but it is a good practice to always clean
	// up after yourself.  Reduce your leaks.
	//
	if( %this.MessageVector0 ) %this.MessageVector0.delete();
	if( %this.MessageVector1 ) %this.MessageVector1.delete();
}

// 
// This little method updates our text pipe image and makes sure
// that later text goes to the correct Message Vector
//
function gsMessageVector::setTextAttachMode( %this , %mode) {
	%this.textAttachMode = %mode;
	TextPipe.setBitmap(%this.textAttach[%mode]);
}


//
// This method updates our message pipe and attaches the correct
// Message Vectors to the correct GuiMessageVectorCtrl controls.
//
function gsMessageVector::setVectorAttachMode( %this , %mode) {
	%this.vectorAttachMode = %mode;
	MessageVectorPipe.setBitmap(%this.vectorAttach[%mode]);

	// We need to do this to avoid a 'double detach' error
	if( %this.MessageVector0Attached ) TestMessageVectorCtrl0.detach();
	if( %this.MessageVector1Attached ) TestMessageVectorCtrl1.detach();

	switch (%this.vectorAttachMode) {
	case 0:
		TestMessageVectorCtrl0.attach( %this.MessageVector0);
		%this.MessageVector0Attached = 1;
		%this.MessageVector1Attached = 0;

	case 1:
		TestMessageVectorCtrl1.attach( %this.MessageVector0);
		%this.MessageVector0Attached = 0;
		%this.MessageVector1Attached = 1;

	case 2:
		TestMessageVectorCtrl0.attach( %this.MessageVector0);
		TestMessageVectorCtrl1.attach( %this.MessageVector0);
		%this.MessageVector0Attached = 1;
		%this.MessageVector1Attached = 1;

	case 3:
		TestMessageVectorCtrl0.attach( %this.MessageVector1);
		%this.MessageVector0Attached = 1;
		%this.MessageVector1Attached = 0;

	case 4:
		TestMessageVectorCtrl1.attach( %this.MessageVector1);
		%this.MessageVector0Attached = 0;
		%this.MessageVector1Attached = 1;

	case 5:
		TestMessageVectorCtrl0.attach( %this.MessageVector1);
		TestMessageVectorCtrl1.attach( %this.MessageVector1);
		%this.MessageVector0Attached = 1;
		%this.MessageVector1Attached = 1;
	}

}

// 
// Both attach pipes are advanced through their various modes
// by pressing the appropriate buttons.  The following two 
// methods simply walk through the modes and call the methods above to
// do the actual updating.
//
function gsMessageVector::advanceTextAttachMode( %this ) {
	%this.textAttachMode++;

	if(%this.textAttachMode >= 3) %this.textAttachMode = 0;

	%this.setTextAttachMode( %this.textAttachMode );
}

function gsMessageVector::advanceVectorAttachMode( %this ) {
	%this.vectorAttachMode++;

	if(%this.vectorAttachMode >= 6) %this.vectorAttachMode = 0;

	%this.setVectorAttachMode( %this.vectorAttachMode );
}

// 
// This little method copies our text from the text field to the 
// Message Vector it is currently attached to.		
function gsMessageVector::copyText( %this ) {
	switch (%this.textAttachMode) {
	case 0:
		%this.MessageVector0.pushBackLine(VectorTextInput.getValue(), 0);
	case 1:
		%this.MessageVector1.pushBackLine(VectorTextInput.getValue(), 0);
	case 2:
		%this.MessageVector0.pushBackLine(VectorTextInput.getValue(), 0);
		%this.MessageVector1.pushBackLine(VectorTextInput.getValue(), 0);
	}
}

//--------------------------------------------------------------------------
// Load Interface Definition
//--------------------------------------------------------------------------
exec("./gsMessageVector.gui");

