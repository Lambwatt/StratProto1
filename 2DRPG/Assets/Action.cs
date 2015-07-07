using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface Action {

	List<Action> checkIfExecutable(Board board, TurnMetaData data);

	int execute(Board board, TurnMetaData data); 

	void checkForConsequences(Board board);

	void applyConsequences(Board board);
}
