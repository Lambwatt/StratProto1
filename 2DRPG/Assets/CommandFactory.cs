using UnityEngine;
using System.Collections;

public interface CommandFactory {

	Command getCommand(string key, Square s, int dir, int mag);
}
