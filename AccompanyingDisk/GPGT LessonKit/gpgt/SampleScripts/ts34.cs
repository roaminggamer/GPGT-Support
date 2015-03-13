

////
//		TS34
////
function testFields( %fieldString ) {

   %tmpField = %fieldString;

   echo( %tmpField, "\n" );


   for( %count = 0; %count < getFieldCount( %tmpField ); %count++ )
   {
      %theField = getField( %tmpField , %count );

      echo( "Current field: ", %theField );

      if ( %theField $= "test" ) 
      {
         echo("\c3Replacing fields...");

         %tmpField = setField ( %tmpField , %count , %theField NL "of" TAB "fields." );
      }

   }

   while ( getFieldCount( %tmpField ) )
   {
      %concatFieldString = %concatFieldString SPC getField( %tmpField , 0 );

      %tmpField = removeField( %tmpField , 0 );
   }

   echo( "\n", %concatFieldString );

}


function ts34() {

   testFields( "This" TAB "is" NL "a" TAB "test" );
}

ts34();