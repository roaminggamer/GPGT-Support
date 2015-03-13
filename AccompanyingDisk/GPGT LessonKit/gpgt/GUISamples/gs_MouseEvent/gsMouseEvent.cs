//------------------------------------------------------
// Copyright 2000-2006, GarageGames.com, Inc.
// Written by Ed Maurina, Hall Of Worlds, LLC
//------------------------------------------------------
echo("\c3--------- Loading TestMouseEvent Samples ---------");
//--------------------------------------------------------------------------
// gsMouseEvent.cs
//--------------------------------------------------------------------------

// 
// In this file, we demonstrate, the following GuiChunkedBitmapCtrl features:
//
// 1. Tiling

//--------------------------------------------------------------------------
// Profiles
//--------------------------------------------------------------------------

//--------------------------------------------------------------------------
// Scripts
//--------------------------------------------------------------------------
function gsMouseEvent::onAdd( %this ) {
	%this.lightOn = expandFileName("./lighton");
	%this.lightOff = expandFileName("./lightoff");

	%this.ctrlPress = expandFileName("./ctrl_press");
	%this.ctrlUnpress = expandFileName("./ctrl_unpress");

	%this.altPress = expandFileName("./alt_press");
	%this.altUnpress = expandFileName("./alt_unpress");

	%this.shiftPress = expandFileName("./shift_press");
	%this.shiftUnpress = expandFileName("./shift_unpress");

	%this.lightBarOn = expandFileName("./lightbarOn");
	%this.lightBarOff = expandFileName("./lightbarOff");

}


function gsMouseEvent::onWake( %this ) {
	LeftPressMouse.setVisible(false);
	MiddlePressMouse.setVisible(false);
	RightPressMouse.setVisible(false);

	ctrlKey.setBitmap(gsMouseEvent.ctrlUnpress);
	altKey.setBitmap(gsMouseEvent.altUnpress);
	shiftKey.setBitmap(gsMouseEvent.shiftUnpress);

	onEnterLight.setBitmap(gsMouseEvent.lightOff);
	onLeaveLight.setBitmap(gsMouseEvent.lightOff);
	onMouseMoveLight.setBitmap(gsMouseEvent.lightOff);

	onMouseDragLight.setBitmap(gsMouseEvent.lightOff);
	onMouseRightDragLight.setBitmap(gsMouseEvent.lightOff);

	mouseFollower.setBitmap(gsMouseEvent.lightOff);

	clickBar1.setBitmap(gsMouseEvent.lightBarOff);
	clickBar2.setBitmap(gsMouseEvent.lightBarOff);
	clickBar3.setBitmap(gsMouseEvent.lightBarOff);
	clickBar4.setBitmap(gsMouseEvent.lightBarOff);
	clickBar5.setBitmap(gsMouseEvent.lightBarOff);


	%this.lastSchedule = %this.schedule( 750 , turnOffLights );
}

function gsMouseEvent::onSleep( %this ) {
	cancel(%this.lastSchedule);
}

function gsMouseEvent::turnOffLights( %this ) {
	onEnterLight.setBitmap(gsMouseEvent.lightOff);
	onLeaveLight.setBitmap(gsMouseEvent.lightOff);
	onMouseMoveLight.setBitmap(gsMouseEvent.lightOff);

	ctrlKey.setBitmap(gsMouseEvent.ctrlUnpress);
	altKey.setBitmap(gsMouseEvent.altUnpress);
	shiftKey.setBitmap(gsMouseEvent.shiftUnpress);

	onMouseDragLight.setBitmap(gsMouseEvent.lightOff);
	onMouseRightDragLight.setBitmap(gsMouseEvent.lightOff);

	mouseFollower.setBitmap(gsMouseEvent.lightOff);

	clickBar1.setBitmap(gsMouseEvent.lightBarOff);
	clickBar2.setBitmap(gsMouseEvent.lightBarOff);
	clickBar3.setBitmap(gsMouseEvent.lightBarOff);
	clickBar4.setBitmap(gsMouseEvent.lightBarOff);
	clickBar5.setBitmap(gsMouseEvent.lightBarOff);

	%this.lastSchedule = %this.schedule( 750 , turnOffLights );
}

function TestMouseEvent::moveFollower( %this , %XY ) {

   %tmpControl = %this.getGroup();

   %Offset = %XY;

   while( isObject( %tmpControl ) ) 
   {
      %Offset = vectorSub( %Offset , %tmpControl.position );
      %tmpControl = %tmpControl.getGroup();
   }

   mouseFollower.position = %Offset;

}


function TestMouseEvent::showModifierKeys( %this , %eventModifier , %numMouseClicks ) {

	%ctrlMask = ($EventModifier::LCTRL | $EventModifier::RCTRL | $EventModifier::CTRL);
	%altMask = ($EventModifier::LALT | $EventModifier::RALT | $EventModifier::ALT);
	%shiftMask = ($EventModifier::LSHIFT | $EventModifier::RSHIFT | $EventModifier::SHIFT);

	// CTRL
	if( %ctrlMask & %eventModifier ) {
		ctrlKey.setBitmap(gsMouseEvent.ctrlPress);
	}

	// ALT
	if( %altMask & %eventModifier ) {
		altKey.setBitmap(gsMouseEvent.altPress);
	}

	// SHIFT
	if( %shiftMask & %eventModifier ) {
		shiftKey.setBitmap(gsMouseEvent.shiftPress);
	}

	echo("\c3" @ "%numMouseClicks == " @ %numMouseClicks);

	switch( %numMouseClicks ) {
	case 0:
		return;
	case 1:
		clickBar1.setBitmap(gsMouseEvent.lightBarOn);
	case 2:
		clickBar1.setBitmap(gsMouseEvent.lightBarOn);
		clickBar2.setBitmap(gsMouseEvent.lightBarOn);
	case 3:
		clickBar1.setBitmap(gsMouseEvent.lightBarOn);
		clickBar2.setBitmap(gsMouseEvent.lightBarOn);
		clickBar3.setBitmap(gsMouseEvent.lightBarOn);
	case 4:
		clickBar1.setBitmap(gsMouseEvent.lightBarOn);
		clickBar2.setBitmap(gsMouseEvent.lightBarOn);
		clickBar3.setBitmap(gsMouseEvent.lightBarOn);
		clickBar4.setBitmap(gsMouseEvent.lightBarOn);
	default:
		clickBar1.setBitmap(gsMouseEvent.lightBarOn);
		clickBar2.setBitmap(gsMouseEvent.lightBarOn);
		clickBar3.setBitmap(gsMouseEvent.lightBarOn);
		clickBar4.setBitmap(gsMouseEvent.lightBarOn);
		clickBar5.setBitmap(gsMouseEvent.lightBarOn);
	}

}


function TestMouseEvent::onMouseDown( %this , %eventModifier , %XY, %numMouseClicks ) {
	LeftPressMouse.setVisible(true);
	
	echo("\c3" @ %eventModifier SPC "::" SPC %XY SPC "::" SPC %numMouseClicks );

	%this.showModifierKeys( %eventModifier , %numMouseClicks);
}

function TestMouseEvent::onMouseUp( %this , %eventModifier , %XY, %numMouseClicks ) {
	LeftPressMouse.setVisible(false);

	%this.showModifierKeys( %eventModifier , %numMouseClicks);
}

function TestMouseEvent::onRightMouseDown( %this , %eventModifier , %XY, %numMouseClicks ) {
	RightPressMouse.setVisible(true);

	%this.showModifierKeys( %eventModifier , %numMouseClicks);
}

function TestMouseEvent::onRightMouseUp( %this , %eventModifier , %XY, %numMouseClicks ) {
	RightPressMouse.setVisible(false);

	%this.showModifierKeys( %eventModifier , %numMouseClicks);
}

function TestMouseEvent::onMouseMove( %this , %eventModifier , %XY, %numMouseClicks ) {

	onMouseMoveLight.setBitmap(gsMouseEvent.lightOn);

	mouseFollower.setBitmap(gsMouseEvent.lightOn);

	%this.showModifierKeys( %eventModifier , %numMouseClicks);

	%this.moveFollower( %XY );
}

function TestMouseEvent::onMouseDragged( %this , %eventModifier , %XY, %numMouseClicks ) {
	onMouseDragLight.setBitmap(gsMouseEvent.lightOn);

	%this.showModifierKeys( %eventModifier , %numMouseClicks);
}

function TestMouseEvent::onRightMouseDragged( %this , %eventModifier , %XY, %numMouseClicks ) {
	onMouseRightDragLight.setBitmap(gsMouseEvent.lightOn);

	%this.showModifierKeys( %eventModifier , %numMouseClicks);
}

function TestMouseEvent::onMouseEnter( %this , %eventModifier , %XY, %numMouseClicks ) {
	onEnterLight.setBitmap(gsMouseEvent.lightOn);

	%this.showModifierKeys( %eventModifier , %numMouseClicks);

	%this.moveFollower( %XY );
}

function TestMouseEvent::onMouseLeave( %this , %eventModifier , %XY, %numMouseClicks ) {
	onLeaveLight.setBitmap(gsMouseEvent.lightOn);

	%this.showModifierKeys( %eventModifier , %numMouseClicks);
}

//--------------------------------------------------------------------------
// Load Interface Definition
//--------------------------------------------------------------------------
//--------------------------------------------------------------------------
exec("./gsMouseEvent.gui");

