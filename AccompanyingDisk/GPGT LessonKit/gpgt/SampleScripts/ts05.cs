
////
//		TS05
////
function ts05( %sampleCase ) {

   //
   // Note: Because this function has been designed to run several dependent examples,
   // it needs to determine which example is running and run the prior 'dependent'
   // code without comments.
   //

   %sampleCase = strlwr( %sampleCase );

   %S0     = new SimObject();
   %Group0 = new SimGroup();
   %Group1 = new SimGroup();
   %Set0   = new SimSet();


   if ( strpos( "ab" , %sampleCase ) >= 0 ) {


      %Set0.add( %S0);

      %Group0.add( %S0);

      if("a" $= %sampleCase )
         echo( "Set 0 contains ", %Set0.getCount() , " objects." );

      if("a" $= %sampleCase )
         echo( "Group 0 contains ", %Group0.getCount() , " objects." );

      if("a" $= %sampleCase )
         echo( "Group 1 contains ", %Group1.getCount() , " objects." );

      %Group1.add( %S0 );

      if("a" $= %sampleCase )
         echo( "Set 0 contains ", %Set0.getCount() , " objects." );

      if("a" $= %sampleCase )
         echo( "Group 0 contains ", %Group0.getCount() , " objects." );

      if("a" $= %sampleCase )
         echo( "Group 1 contains ", %Group1.getCount() , " objects." );	}


   if ( strpos( "b" , %sampleCase ) >= 0 ) {


      if("b" $= %sampleCase )
         echo( "Set 0 contains ", %Set0.getCount() , " objects." );

      %Group1.delete(); // Self deletes, and automatically deletes %S0

      if("b" $= %sampleCase )
         echo( "Set 0 contains ", %Set0.getCount() , " objects." );

   }

}

ts05(a);