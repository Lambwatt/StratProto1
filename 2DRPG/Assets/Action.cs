using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface Action {

	void checkIfExecutable(Board board);

	List<Response> execute(Board board); //Responses may be unnecessary

	void checkForConsequences(Board board);

	void applyConsequences(Board board);
}
