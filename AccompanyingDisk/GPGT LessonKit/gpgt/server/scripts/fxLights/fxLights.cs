datablock fxLightData(cyclingLight)
{
	FlareOn		= true;
	FlareBitmap = "~/data/fxLight/corona";

	LightRadius = "5";

	AnimColour	= true;
	MinColour	= "0.25 0.0 0.25";
	MaxColour	= "1.0  0.5 1.0";
	ColourTime	= 3.0;

};


datablock fxLightData(redLight)
{
	FlareOn		= true;
	FlareBitmap = "~/data/fxLight/lightFalloffMono";

	LightRadius = "5";

	AnimColour	= false;
	Colour	= "1 0 0";
};

datablock fxLightData(redLightNoFlare : redLight)
{
	FlareOn		= false;
};

datablock fxLightData(greenLight : redLight)
{
	Colour	= "0 1 0";
};

datablock fxLightData(blueLight : redLight)
{
	Colour	= "0 0 1";
};