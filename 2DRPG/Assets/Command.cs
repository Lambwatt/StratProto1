using UnityEngine;
using System.Collections.Generic;

//Consider whether all commands should be folded into an ActionSequence factory
public interface Command {

	List<Action> execute();
}
