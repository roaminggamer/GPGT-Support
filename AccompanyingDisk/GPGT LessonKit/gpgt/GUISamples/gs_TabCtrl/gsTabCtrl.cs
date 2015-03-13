//------------------------------------------------------
// Copyright 2000-2006, GarageGames.com, Inc.
// Written by Ed Maurina, Hall Of Worlds, LLC
//------------------------------------------------------
echo("\c3--------- Loading Template Sample ---------");

//--------------------------------------------------------------------------
// gsTemplate.cs
//--------------------------------------------------------------------------

//--------------------------------------------------------------------------
// Profiles
//--------------------------------------------------------------------------
if( !isObject(GuiTabBookProfile) ) new GuiControlProfile (GuiTabBookProfile)
{
	fillColor = "255 255 255";
	fillColorHL = "64 150 150";
	fillColorNA = "150 150 150";
	fontColor = "0 0 0";
	fontColorHL = "32 100 100";
	fontColorNA = "0 0 0";
	justify = "center";
	bitmap = expandFileName("./tab");
	tab = true;
	cankeyfocus = true;
	hasBitmapArray = true;
};

if( !isObject(GuiTabPageProfile) ) new GuiControlProfile (GuiTabPageProfile)
{
	bitmap = expandFileName("./darkTabPage");
	tab = true;
	hasBitmapArray = true;
};
//--------------------------------------------------------------------------
// Scripts
//--------------------------------------------------------------------------

//--------------------------------------------------------------------------
// Load Interface Definition
//---------------------------------------------------------------
exec("./gsTabCtrl.gui");

