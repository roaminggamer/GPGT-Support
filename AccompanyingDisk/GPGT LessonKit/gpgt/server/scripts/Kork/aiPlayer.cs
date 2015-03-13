datablock PlayerData(DemoPlayer : PlayerBody)
{
   shootingDelay = 2000;
};


function korkLoader::onAdd(%this)
{
   %this.schedule(5000, spawnKork );
}

function korkLoader::spawnKork(%this)
{
   %position = VectorAdd( theSpawnPoint.getPosition(), "5 -5 0");
   %kork = AIPlayer::spawn("Kork",%position);
   
   %kork.path = korkPath;
   AIPlayer::followPath(%kork,korkPath,0);
   
}

function AIPlayer::spawn(%name,%spawnPoint)
{
   // Create the demo player object
   %player = new AiPlayer() {
      dataBlock = DemoPlayer;
      path = "";
   };
   MissionCleanup.add(%player);
   %player.setShapeName(%name);
   %player.setTransform(%spawnPoint);
   return %player;
}


//-----------------------------------------------------------------------------
// AIPlayer methods 
//-----------------------------------------------------------------------------

function AIPlayer::followPath(%this,%path,%node)
{
   if (!isObject(%path)) {
      %this.path = "";
      return;
   }

   %this.path = %path;

   if (%node > %path.getCount() - 1)  %node = 0;
   %this.node = %node;
   
   %pathNode = %path.getObject( %node );
   
   %this.setMoveDestination(%pathNode.getTransform());
}

function DemoPlayer::onReachDestination(%this,%obj)
{
   %path = %obj.path;
   
   %obj.node++;   
   if (%obj.node > %path.getCount() - 1) %obj.node = 0;
   
   %node = %obj.node;
      
   %pathNode = %path.getObject( %node );
      
   %obj.setMoveDestination(%pathNode.getTransform());        
}




