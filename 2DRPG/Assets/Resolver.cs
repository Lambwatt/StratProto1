using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Processes the logic of a turn. Needs to be split between a lot of other pieces to handle future development.



public class Resolver {

	private class SequenceTracker{

		public bool complete;
		IEnumerator iterator;

		public SequenceTracker(IEnumerator i){
			iterator = i;
			step();
		}

		public void step(){
			if(!complete)
				complete = iterator.MoveNext();
		}

		public Action getAction(){
			return (Action)iterator.Current;
		}

	}

	public void resolve(Board b, Order o){

		//Create metaData

		List<Command> commands = o.getCommands();
		List<SequenceTracker> actionSequences = new List<SequenceTracker>();
		TurnMetaData data = new TurnMetaData();

		foreach(Command c in commands){
			actionSequences.Add(new SequenceTracker(c.execute().GetEnumerator()));
		}

		bool complete;
		do{

			foreach(SequenceTracker st in actionSequences){
				st.getAction().checkIfExecutable(b, data);
			}

			foreach(SequenceTracker st in actionSequences){
				st.getAction().execute(b, data);
			}


			foreach(SequenceTracker st in actionSequences){
				st.getAction().checkForConsequences(b);
			}


			foreach(SequenceTracker st in actionSequences){
				st.getAction().applyConsequences(b);
			}

			complete = true;
			foreach(SequenceTracker st in actionSequences){
				st.step();
				complete &= st.complete;

			}

			data.clearData();

		}while(!complete);


	}




//	public void resolveMovement(){
//
//	}
	
//	public moveAnimation resolveOrder(Transform transform, MoveOrder order, int numFrames){
//		Vector3 newPosition = transform+order.direction;
//
//		//situations that cancel movement with no consequences
//		if(newPosition.x>board.width || newPosition.x<0 || newPosition.y<0 || newPositions.y>board.height){
//
//			return new moveAnimation(this.transform, new MoveOrder(Direction.NONE), numFrames);//If you hit a wall, go no where.
//
//		//situations that cancel with consequences Work to do here.
//		}else if(board[newPosition.x,newPosition.y].tag == "player"){
//
//
//			return resolveOrder(board[newPosition.x,newPosition.y].transform, order, numFrames);
//
//		}else{
//
//			new moveAnimation(this.transform, manager.conductor.move[moveNumber], numFrames);
//
//		}
//	}
	
	// Update is called once per frame

}
