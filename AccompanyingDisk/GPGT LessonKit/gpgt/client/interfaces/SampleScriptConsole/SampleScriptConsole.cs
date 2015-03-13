function rlssc()
{
   exec("./SampleScriptConsole.cs");
}

function ScriptIn::EvalCode( %this )
{
   
   cls();

   %content = %this.getText();
   
   %content = strReplace( %content , "<BR>" , "" );
   %content = strReplace( %content , "\n" , "" );
   
   eval( %content );   
}

function evalScript::EvalCode( %this )
{
   %content = %this.getText();
   %content = strReplace( %content , "<BR>" , "" );
   %content = strReplace( %content , "\n" , "" );
   eval( %content );   
}


function ScriptIn::ExecuteCode( %this )
{
   saveSampleScript( expandFileName("~/sampleScripts/ssc_exec.cs") );
   
   cls();

   exec( expandFileName("~/sampleScripts/ssc_exec.cs") );
}

function buildFileArray( %initialFile  )
{
   %filePath = filePath( %initialFile );
   
   if( isObject( SampleScriptConsole.filePathArray ) )
   {
      SampleScriptConsole.filePathArray.delete();
      SampleScriptConsole.currentFileArrayIndex = 0;
   }
   SampleScriptConsole.filePathArray = new ScriptObject( arrayObject );
   
   %fileName = findFirstFile ( %filePath @ "/*.cs" );
   
   while ( "" !$= %fileName ) 
   {
      SampleScriptConsole.filePathArray.addEntry( %fileName );
      %fileName = findNextFile( %filePath @ "/*.cs" );
   }   

   SampleScriptConsole.filePathArray.sort();
   
   for( %count = 0; %count < SampleScriptConsole.filePathArray.getCount(); %count++ )
   {
      //echo("Found: ", SampleScriptConsole.filePathArray.getEntry( %count ) );
      if( %initialFile $= SampleScriptConsole.filePathArray.getEntry( %count ) )
      {
         //echo("Current file is at index: ", %count );
         SampleScriptConsole.currentFileArrayIndex = %count;         
      }
   }
   SampleScriptConsole.maxFileArrayIndex = SampleScriptConsole.filePathArray.getCount();   
}

function loadNextSampleScript()
{
   %val = sampleScriptNum.getValue();
   
   %val++;
   
   if (%val < 0) %val = 0;
   
   %suffix = %val;      
   if(%suffix < 10) %suffix = "0" @ %val;
   
  %fileName = "~/sampleScripts/" @ btts.text @ %suffix @ ".cs";

   
   if( !isFile( %fileName ) ) 
      %val = 0;

   
   sampleScriptNum.setValue(%val);
   
   loadSampleScript( %val );
}

function loadPrevSampleScript()
{
   
   %val = sampleScriptNum.getValue();
   
   %val--;
   
   if (%val < 0) %val = 0;
   
   sampleScriptNum.setValue(%val);
   
   loadSampleScript( %val );
}


function loadSampleScript( %num )
{
   scriptIn.setValue(""); // Clear it
   
   %suffix = %num;      
   if(%suffix < 10) %suffix = "0" @ %num;

   %fileName = "~/sampleScripts/" @ btts.text @ %suffix @ ".cs";

   if( isFile( %fileName ) )

   %file = new FileObject();

   %fileIsOpen = %file.openForRead( %fileName );

   if( %fileIsOpen ) 
   {
      scriptIn.addText( "// File: " @  %fileName @ "\n", true );
      while(!%file.isEOF()) 
      {
      %currentLine = %file.readLine() @ "\n";
      scriptIn.addText( %currentLine, true );
      }
   }

   if( scriptIn.isVisible() ) scriptIn.forceReflow();

	%file.close();
   %file.delete();
   
   ScriptIn.schedule(100, ExecuteCode);
}


function saveSampleScript( %fileName )
{
   %file = new FileObject();

   if(! %file.openforWrite( %fileName ) ) 
   {
      %file.delete();
      return false; 
   }
   
   %file.writeLine( "cls();\n" );

   %content = scriptIn.getText();
   
   %content = strReplace( %content , "<BR>" , "\n" );
   
   %file.writeLine( %content );

   %file.close();
   %file.delete();   
}

function SampleScriptConsole::onWake( %this )
{
	GlobalActionMap.unbind(keyboard, "tilde");
	
	if(!isObject(datablockGroup) ) new SimGroup( datablockGroup );
	exec("~/server/scripts/loadClasses.cs"); 
}

function SampleScriptConsole::onSleep( %this )
{
	GlobalActionMap.bind(keyboard, "tilde", toggleConsole);
	
	if(isObject(datablockGroup) ) 
	{
	   datablockGroup.forEach( delete, true );
	}
}
