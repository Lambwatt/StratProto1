using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface Action {

	void checkIfExecutable(Board board, TurnMetaData data);

	bool execute(Board board, TurnMetaData data); //Responses may be unnecessary

	void checkForConsequences(Board board);

	void applyConsequences(Board board);
}
