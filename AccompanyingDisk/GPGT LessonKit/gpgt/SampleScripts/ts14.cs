
////
//		TS14
////
function readFile( %filename ){
   %file = new FileObject();

   if(%file.openForRead(%filename)) 
   {
      while(!%file.isEOF()) 
      {
         %input = %file.readLine();
         echo(%input);
      }
   } else {
      %file.delete();
      return false;
   }

   %file.close();
   %file.delete();
   return true;
}

function ts14() {

   readFile( expandFilename( "~/prefs.cs" ) );

}

ts14();