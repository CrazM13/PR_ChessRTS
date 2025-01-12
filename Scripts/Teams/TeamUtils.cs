using Godot;
using System;

public static class TeamUtils {

	public static Color GetTeamColour(Teams team) {
		return team switch {
			Teams.WHITE => Colors.White,
			Teams.BLACK => Colors.Black,
			Teams.RED => Colors.Red,
			Teams.GREEN => Colors.Green,
			Teams.BLUE => Colors.Blue,
			Teams.YELLOW => Colors.Yellow,
			Teams.CYAN => Colors.Cyan,
			Teams.MAGENTA => Colors.Magenta,
			_ => Colors.White
		};
	}

}
