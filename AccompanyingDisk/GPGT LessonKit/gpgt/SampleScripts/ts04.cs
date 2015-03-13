
////
//		TS04
////
function ts04( %sampleCase ) {

   //
   // Note: Because this function has been designed to run several dependent examples,
   // it needs to determine which example is running and run the prior 'dependent'
   // code without comments.
   //

   %sampleCase = strlwr( %sampleCase );

   %S0   = new SimObject();
   %Set0 = new SimSet();
   %Set1 = new SimSet();

   ////
   // TS04A
   ////
   if ( strpos( "abcdefg" , %sampleCase ) >= 0 ) {

      %Set0.add( %S0);
      %Set0.add( %S0);
      if("a" $= %sampleCase )
         echo( "Set 0 contains ", %Set0.getCount() , " objects." );

      %Set1.add( %S0 );
      %Set1.add( %Set0 );
      if("a" $= %sampleCase )
         echo( "Set 1 contains ", %Set1.getCount() , " objects." );

   }

   ////
   // TS04B
   ////
   if ( strpos( "bcdefg" , %sampleCase ) >= 0 ) {

      %Set1.delete(); // Self delete

      if("b" $= %sampleCase )
         echo( "Set 0 contains ", %Set0.getCount() , " objects." );

   }

   ////
   // TS04C
   ////
   if ( strpos( "cdefg" , %sampleCase ) >= 0 ) {

      %S1   = new SimObject();
      %S2   = new SimObject();
      %Set2 = new SimSet();
      %Set2.add( %S1 );
      %Set2.add( %S2 );

      if("c" $= %sampleCase )
         echo( "The ID of S1 is: ", %S1.getID() );

      if("c" $= %sampleCase )
         echo( "The ID of S2 is: ", %S2.getID() );

      if("c" $= %sampleCase )
         echo( "Object at front of Set 2 is ", %Set2.getObject(0) );

      if("c" $= %sampleCase )
         echo( "Object at back  of Set 2 is ", %Set2.getObject(1) );

   }

   ////
   // TS04D
   ////
   if ( strpos( "defg" , %sampleCase ) >= 0 ) {

      if("d" $= %sampleCase )
         echo( "The ID of S1 is: ", %S1.getID() );

      if("d" $= %sampleCase )
         echo( "The ID of S2 is: ", %S2.getID() );


      %Set2.bringToFront( %S2 );

      if("d" $= %sampleCase )
         echo( "Object at front of Set 2 is ", %Set2.getObject(0) );

      if("d" $= %sampleCase )
         echo( "Object at back  of Set 2 is ", %Set2.getObject(1) );


      %Set2.pushToBack( %S2 );

      if("d" $= %sampleCase )
         echo( "Object at front of Set 2 is ", %Set2.getObject(0) );

      if("d" $= %sampleCase )
         echo( "Object at back  of Set 2 is ", %Set2.getObject(1) );

   }
   ////
   // TS04E
   ////
   if ( strpos( "efg" , %sampleCase ) >= 0 ) {

      %Set0.remove( %S0 ); // Take %S0 our of SimSet 1

      if("e" $= %sampleCase )
         echo( "Set 0 contains ", %Set0.getCount() , " objects." );

   }
   ////
   // TS04F
   ////
   if ( strpos( "fg" , %sampleCase ) >= 0 ) {

      if("f" $= %sampleCase )
         echo( "Set 2 contains ", %Set2.getCount() , " objects." );

      %Set2.clear(); // Remove all objects from SimSet 2

      if("f" $= %sampleCase )
         echo( "Set 2 contains ", %Set2.getCount() , " objects." );

   }
   ////
   // TS04G
   ////
   if ( strpos( "g" , %sampleCase ) >= 0 ) {

      %S3   = new SimObject();
      %S4   = new SimObject();

      if("g" $= %sampleCase )
         echo( "The ID of S3 is: ", %S3.getID() );

      if("g" $= %sampleCase )
         echo( "The ID of S4 is: ", %S4.getID() );

      %Set3 = new SimSet();
      %Set3.add( %S3 );
      %Set3.add( %S4 );

      if("g" $= %sampleCase )
         %Set3.listObjects();

   }



}

ts04(a);