echo("\c2--------- Loading Maze Runner GUI Sound Profiles  ---------");
//--------------------------------------------------------------------------
// MazeRunnerGUISounds.cs
//--------------------------------------------------------------------------
new AudioDescription( MazeRunnerNonLooping2DADObj )
{
   volume            = 1.0;
   isLooping         = false;
   is3D              = false;
   type              = $GuiAudioType; 
};

new AudioDescription( MazeRunnerLooping2DADObj )
{
   volume            = 1.0;
   isLooping         = true;
   loopCount         = -1;
   is3D              = false;
   type              = $GuiAudioType; 
};
new AudioProfile(MazeRunnerGGSplashScreen)
{
   filename    = "~/data/GPGTBase/sound/gui/GGstartup.ogg";
   description = MazeRunnerNonLooping2DADObj;
};

new AudioProfile(MazeRunnerButtonOver)
{
    filename    = "~/data/GPGTBase/sound/gui/ButtonOver1.ogg";
    description = MazeRunnerNonLooping2DADObj;
    preload     = true;
};

new AudioProfile(MazeRunnerButtonPress)
{
    filename    = "~/data/GPGTBase/sound/gui/ButtonPress.ogg";
    description = MazeRunnerNonLooping2DADObj;
    preload     = true;
};

new AudioProfile(MazeRunnerLevelLoop)
{
   filename    = "~/data/GPGTBase/sound/gui/levelLoop.ogg";
   description = MazeRunnerLooping2DADObj;
};

if(!isObject(MainMenuButtonProfile)) new GuiControlProfile (MainMenuButtonProfile)
{
    opaque = true;
    border = true;
    fontColor = "0 0 0";
    fontColorHL = $fontColorHL;
    fixedExtent = true;
    justify = "center";
    canKeyFocus = false;
    soundButtonOver = "MazeRunnerButtonOver";
    soundButtonDown = "MazeRunnerButtonPress";
};


